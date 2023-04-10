using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using WebApp.DataServices;

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiAccountController : Controller
        {
            private readonly IConfiguration _config;
            private readonly SignInManager<BlitzerUser> _signInManager;
            private readonly UserManager<BlitzerUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly BlitzerCore.Models.IDbContext mContext;

            [AllowAnonymous]
            [HttpGet]
            public ActionResult Login()
            {
                //new SalesForce(_config).Login();

                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login([FromForm] LoginViewModel model)
            {
                Logger.LogInfo(model.Email + " called SignIn");
                const string ERRORMSG = "Invalid UserName and/or Password";
                IActionResult response = Unauthorized("Invalid email/password.");
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    Logger.LogError("User Email Addr Not Found");
                    ModelState.AddModelError("Login", ERRORMSG);
                    return View(model);
                }
                else if (user.EmailConfirmed == true)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                    if (model.Latitude != null && model.Longitude != null)
                    {
                        UserLocation lLocation = new UserLocation() { UserID = user.Id, When = DateTime.Now, Latitude = model.Latitude, Longitude = model.Longitude };
                        mContext.UserLocations.Add(lLocation);
                        mContext.SaveChanges();
                    }

                        var jsonwebtokenmodel = GenerateJSONWebTokenAsync(user);
                        await _userManager.UpdateAsync(user);
                        Logger.LogInfo(model.Email + " Sucessfully Logged Into Blitzer");
                        return Ok(jsonwebtokenmodel);
                    }
                    else
                    {
                        Logger.LogError(model.Email + " user password was incorret");
                        ModelState.AddModelError("Login", ERRORMSG);
                        return View(model);
                    }
                }
                else
                {
                    Logger.LogError("Login failed because Email Addr was not confirmed");
                    ModelState.AddModelError("Login", ERRORMSG);
                    return View(model);
                }
            }
            public ApiAccountController(IConfiguration config, SignInManager<BlitzerUser> signInManager, UserManager<BlitzerUser> userManager, RoleManager<IdentityRole> roleManager, BlitzerCore.Models.IDbContext aDBContext)
            {
                _config = config;
                _signInManager = signInManager;
                _userManager = userManager;
                _roleManager = roleManager;
                mContext = aDBContext;
            }

            [AllowAnonymous]
            [HttpGet]
            public ActionResult Register()
            {
                return View();
            }


            [AllowAnonymous]
            [HttpPost]
            public ActionResult Register([FromForm] RegisterViewModel aModel)
            {
                return View();
            }

            [HttpPost]
            [Route(nameof(ChangePassword))]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
            {
                if (!ModelState.IsValid)
                    return Ok(new JsonModel(JsonType.Error, GetErrorStringFromModelState(ModelState)));


                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword,
                    model.NewPassword);

                if (!result.Succeeded)
                    return Ok(new JsonModel(JsonType.Error, string.Join(",", result.Errors.Select(s => s.Description))));

                return Ok(new JsonModel(JsonType.Success, "Password has changed successfully."));
            }


        [AllowAnonymous]
        [HttpPost]
            public async Task<IActionResult> SignIn(APILoginViewModel model)
            {
                Logger.LogInfo(model.Email + " called SignIn");
                IActionResult response = Unauthorized("Invalid email/password.");
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    Logger.LogError("User Email Addr Not Found");
                    return Unauthorized();
                }
                else if (user.EmailConfirmed == true)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        UserLocation lLocation = new UserLocation() { UserID = user.Id, When = DateTime.Now, Latitude = model.Latitude, Longitude = model.Longitude };
                        mContext.UserLocations.Add(lLocation);
                        mContext.SaveChanges();

                        var jsonwebtokenmodel = GenerateJSONWebTokenAsync(user);
                        await _userManager.UpdateAsync(user);
                        Logger.LogInfo(model.Email + " Sucessfully Logged Into Blitzer");
                        return Ok(jsonwebtokenmodel);
                    }
                    else
                    {
                        Logger.LogError(model.Email + " user password was incorret");
                        return Unauthorized("Invalid email/password.");
                    }
                }
                else
                {
                    Logger.LogError("Login failed because Email Addr was not confirmed");
                    return Unauthorized();
                }
            }

            //[HttpPost]
            //public IActionResult Post(string aUser, RegisterMerchantModel model)
            //{
            //    Logger.LogInfo(aUser);
            //    Logger.LogInfo((model == null).ToString());
            //    if (ModelState.IsValid)
            //    {

            //        return Ok(new JsonModel(JsonType.Success, "Updated lead successfully."));
            //    }

            //    // If execution got this far, something failed, redisplay the form.
            //    return Ok(new JsonModel(JsonType.Error, GetErrorStringFromModelState(ModelState)));
            //}

            //[HttpPost]
            //public async Task<IActionResult> RegisterMerchant(RegisterMerchantModel model)
            //{

            //    Logger.LogInfo("RegisterMerchant called with Email=" + model.Email + " First Name=" + model.FirstName + " Last Name=" + model.LastName);
            //    try
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            var user = new User { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber, EmailConfirmed = false };
            //            var result = await _userManager.CreateAsync(user, model.Password);

            //            if (result.Succeeded)
            //            {
            //                var role = await _roleManager.FindByNameAsync(Defines.CLIENT_ROLE);
            //                if (role == null)
            //                {
            //                    await _roleManager.CreateAsync(new IdentityRole { Name = Defines.CLIENT_ROLE });
            //                }

            //                await _userManager.AddToRoleAsync(user, Defines.CLIENT_ROLE);
            //                var lMerchant = new Merchant(model) { Id = user.Id };
            //                new MerchantBusiness(mContext, _config).Save(lMerchant);

            //                Logger.LogInfo("Sucessfully RegisterMerchant");
            //                return Ok(new JsonModel(JsonType.Success, "Registered merchant successfully."));
            //            }

            //            Logger.LogError("Failed to RegisterMerchant : " + string.Join(",", result.Errors.Select(s => s.Description)));
            //            return Ok(new JsonModel(JsonType.Error, string.Join(",", result.Errors.Select(s => s.Description))));
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Logger.LogException("Failed to RegisterClient", e);
            //    }

            //    // If execution got this far, something failed, redisplay the form.
            //    return Ok(new JsonModel(JsonType.Error, GetErrorStringFromModelState(ModelState)));
            //}


            [HttpPost]
            public async Task<IActionResult> RegisterClient(RegisterViewModel model)
            {
                Logger.LogInfo("RegisterClient called with Email=" + model.Email + " First Name=" + model.FirstName + " Last Name=" + model.LastName);
                try
                {
                    if (ModelState.IsValid)
                    {
                        var user = new BlitzerUser { UserName = Guid.NewGuid().ToString(), Email = model.Email.First(), FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber, EmailConfirmed = false };
                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            var role = await _roleManager.FindByNameAsync(Defines.CLIENT_ROLE);
                            if (role == null)
                            {
                                await _roleManager.CreateAsync(new IdentityRole { Name = Defines.CLIENT_ROLE });
                            }

                            await _userManager.AddToRoleAsync(user, Defines.CLIENT_ROLE);
                            //var lLead = new Lead(model) { UserId = user.Id, Id = user.Id };
                            //new LeadBusiness(mContext, new DataServices.ConcreteFactory().GetLeadDataService(mContext)).Save(lLead);

                            Logger.LogInfo("Sucessfully RegisterClient");
                            return Ok(new JsonModel(JsonType.Success, "Registered user successfully."));
                        }

                        Logger.LogError("Failed to RegisterClient : " + string.Join(",", result.Errors.Select(s => s.Description)));
                        return Ok(new JsonModel(JsonType.Error, string.Join(",", result.Errors.Select(s => s.Description))));
                    }
                }
                catch (Exception e)
                {
                    Logger.LogException("Failed to RegisterClient", e);
                }

                // If execution got this far, something failed, redisplay the form.
                return Ok(new JsonModel(JsonType.Error, GetErrorStringFromModelState(ModelState)));
            }

            private JsonWebTokenModel GenerateJSONWebTokenAsync(BlitzerUser user)
            {
                try
                {
                    var lKey = _config.GetSection("Identity").GetValue<string>("SecretKey");
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(lKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };


                    var jsonWebTokenModel = new JsonWebTokenModel
                    {
                        Expired = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config.GetSection("Identity").GetValue<string>("ExpireMinute"))),
                        UserId = user.Id
                    };

                    string lIssuer = _config.GetSection("Identity").GetValue<string>("Issuer");
                    var token = new JwtSecurityToken(lIssuer,
                        lIssuer, claims,
                        expires: jsonWebTokenModel.Expired,
                        signingCredentials: credentials);

                    jsonWebTokenModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    return jsonWebTokenModel;
                }
                catch (Exception e)
                {
                    Logger.LogException("GenerateJSONWebTokenAsync failed ", e);
                }

                return null;
            }

            private string GetErrorStringFromModelState(ModelStateDictionary dictionary)
            {
                var error = dictionary.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
                if (error != null)
                    return string.Join(",", error);

                return string.Empty;
            }


        }
    }
