using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business.AIFilters;
using BlitzerCore.Business;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quartz.Xml.JobSchedulingData20;
using BlitzerCore.Business.JobScheduler;
using WebApp.SrvUtilities;

namespace WebApp
{
    public class Startup
    {
        public IWebHostEnvironment _env;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.Init("BlitzerWebServer");
            Logger.InitConsummer();
            Logger.ConnectionFactory = new DataServices.ConcreteFactory();
            services.AddTransient<IDbContext, WebApp.DataServices.RepositoryContext>();
            string lDB = Configuration["ConnectionString:Blitzer"];
            services.AddSingleton<IBlitzer, WebApp.Services.BlitzerServices>();
            services.AddTransient<BlitzerCore.Models.IBlazorService, WebApp.Services.BlazorService>();

            //string lDB = Configuration["ConnectionString:TEST"];
            Logger.ConnectionString = lDB;
            Logger.LogInfo("Connection String = " + lDB);
            services.AddDbContext<WebApp.DataServices.RepositoryContext>(options =>
               options.UseSqlServer(lDB));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddDefaultIdentity<WebApp.DataServices.BlitzerUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<WebApp.DataServices.RepositoryContext>()
            .AddDefaultTokenProviders();

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddControllersWithViews();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            services.AddServerSideBlazor();
            services.AddControllersWithViews();

            // Services used to Run QuoteRequests in the Background Que
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });
            var lIssuer = Configuration["Identity:Issuer"];
            services.AddAuthentication()
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Identity/Account/Login/";
            //    options.AccessDeniedPath = "/Identity/Account/Login/";
            //})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Identity:Issuer"],
                    ValidAudience = Configuration["Identity:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Identity:SecretKey"]))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json; charset=utf-8";
                        var message = _env.IsDevelopment() ? context.Exception.ToString() : "Unauthorized user.";
                        var result = JsonConvert.SerializeObject(new { message });
                        return context.Response.WriteAsync(result);
                    }

                };

            });

            // Server Side Blazor doesn't register HttpClient by default
            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri)
                    };
                });
            }

            // https://www.thecodehubs.com/creating-a-quartz-net-hosted-service-with-asp-net-core/

            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<EsclateDeadline>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(BlitzerCore.Business.JobScheduler.EsclateDeadline),
                cronExpression: "0 0 1 ? * *"));


            services.AddSingleton<CreatePrintDocsTask>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(BlitzerCore.Business.JobScheduler.CreatePrintDocsTask),
                cronExpression: "0 15 1 ? * *"));

            services.AddSingleton<SaveTrips>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(BlitzerCore.Business.JobScheduler.SaveTrips),
                cronExpression: "0 30 1 ? * *"));

            services.AddSingleton<RegisterBooking>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(BlitzerCore.Business.JobScheduler.RegisterBookings),
                cronExpression: "0 45 1 ? * *"));


            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostEnvironment env, IDbContext aDbConext, IBlitzer aBlitzer)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseTrackingMiddleware();
            app.UseStatusCodePagesWithReExecute("/Home");
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });

            // Register AI Filters
            //AIFilterRegistry.UpdateDataBaseWithAIFilters(aDbConext, AIFilterRegistry.RegisterFilters());
            //TourOperatorRegistry.UpdateDataBaseWithTourOperators(aDbConext, TourOperatorRegistry.RegisterWebBots(aDbConext));
            //var lConfigStr = Configuration["ConnectionString:Blitzer"];
            //aBlitzer.IsProdDb = lConfigStr.Contains("blitzersrv.database.windows.net") && lConfigStr.Contains("database=Blitzer");
        }
    }
}
