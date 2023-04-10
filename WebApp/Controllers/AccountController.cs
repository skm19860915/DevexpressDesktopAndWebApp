using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using WebApp.SrvUtilities;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WebApp.DataServices;

namespace WebApp.Controllers
{

    [ApiVersion("1.0")]
    public class AccountController : Controller
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
            var lModel = new BlitzerCore.Models.UI.LoginViewModel();
            lModel.ENV = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return RedirectToAction("Index", "Home");
        }

        //http://www.dotnetawesome.com/2017/04/complete-login-registration-system-asp-mvc.html
        [HttpPost]
        public async Task<IActionResult> Login(string aReturnUrl, [FromForm] BlitzerCore.Models.UI.LoginViewModel model )
        {
            Logger.LogInfo(model.Email + " called SignIn");
            IActionResult response = Unauthorized("Invalid email/password.");
            var user = await _userManager.FindByEmailAsync (model.Email.ToUpper().Trim());

             if (user == null)
            {
                Logger.LogError("User Email Addr Not Found");
                model.ErrorMsgs.Add(new BlitzerDataAccess(mContext).GetErrorMsg(DataHelper.ErrorCodes.LoginFailure));
                return View(model);
            }
            else if (user.EmailConfirmed == true)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    Logger.LogInfo(model.Email + " Sucessfully Logged Into Blitzer");
                    HttpContext.Session.SetString(Defines.USER_ID, user.Id);
                    return RedirectToAction("Index", "Portal");
                }
                else
                {
                    Logger.LogError(model.Email + " user password was incorret");
                    model.ErrorMsgs.Add(new BlitzerDataAccess(mContext).GetErrorMsg(DataHelper.ErrorCodes.LoginFailure));
                    return View(model);
                }
            }
            else
            {
                Logger.LogError("Login failed because Email Addr was not confirmed");
                model.ErrorMsgs.Add(new BlitzerDataAccess(mContext).GetErrorMsg(DataHelper.ErrorCodes.LoginFailure));
                return View(model);
            }
        }
        public AccountController(IConfiguration config, SignInManager<BlitzerUser> signInManager, UserManager<BlitzerUser> userManager, RoleManager<IdentityRole> roleManager, BlitzerCore.Models.IDbContext aDBContext)
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



        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel aModel)
        {
            Logger.LogInfo("RegisterClient called with Email=" + aModel.Email + " First Name=" + aModel.FirstName + " Last Name=" + aModel.LastName);

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(aModel.Email[0]);
                    if (user != null)
                    {
                        Logger.LogError("User Attempting to register with existing email address : " + aModel.Email);
                        ModelState.AddModelError("Login", "Email has already been registered");
                        return View(aModel);
                    }


                    user = new BlitzerUser { UserName = aModel.Email[0], Email = aModel.Email[0], FirstName = aModel.FirstName, LastName = aModel.LastName, PhoneNumber = aModel.PhoneNumber, EmailConfirmed = false };
                    var result = await _userManager.CreateAsync(user, aModel.Password);

                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(Defines.CLIENT_ROLE);
                        if (role == null)
                        {
                            await _roleManager.CreateAsync(new IdentityRole { Name = Defines.CLIENT_ROLE });
                        }

                        await _userManager.AddToRoleAsync(user, Defines.CLIENT_ROLE);
                        var lContact = new BlitzerCore.Models.Client() { Id = user.Id };
                        //new ClientBusiness(mContext, new DataServices.ConcreteFactory().GetClientDataService(mContext)).Save(lContact);

                        var lUrl = "";
                        if (Url != null)
                            lUrl = Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = aModel.Email });

                        var lResult = new WebApp.SrvUtilities.EmailHelper(_config).SendConfirmation(0, aModel.Email[0], lUrl, aModel.FirstName + " " + aModel.LastName);
                        if (!lResult)
                        {
                            Logger.LogError("Registration Internal Server Error sending confirmation email");
                            //return Json(new { Status = true, Url = "/internalservererror" }, JsonRequestBehavior.AllowGet);
                            return new BadRequestResult();
                        }

                        Logger.LogInfo("Sucessfully RegisterClient");
                        return RedirectToAction("Login");
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ConfirmEmail(int Token, string Email)
        {
                //try
                //{
                //    if (Token > 0)
                //    {
                //        var user = EmployeeHelper.GetEmployeeById(Token, lScope);
                //        if (user.Email == Email)
                //        {
                //            user.EmailConfirmed = true;
                //            Layer.DataAccess.DBSaver lSaver = new Layer.DataAccess.DBSaver();
                //            lSaver.saveEmployee(user);
                //            lScope.SaveChanges();
                //        }
                //        return RedirectToAction("Login", "Account");
                //    }
                //}
                //catch (Exception e)
                //{
                //    Logger.LogException("Failed to confirm email", e);
                //}
                return RedirectToAction("ConfirmationMessage", "Account");
            }

        [HttpPost]
        [Route(nameof(ChangePassword))]
        [Authorize]
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


        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm]LoginViewModel model)
        {
            Logger.LogInfo(model.Email + " called SignIn");
            IActionResult response = Unauthorized("Invalid email/password.");
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null )
            {
                Logger.LogError("User Email Addr Not Found");
                return Unauthorized();
            }
            else if ( user.EmailConfirmed == true)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    UserLocation lLocation = new UserLocation() { UserID = user.Id, When = DateTime.Now, Latitude = model.Latitude, Longitude = model.Longitude };
                    mContext.UserLocations.Add(lLocation);
                    mContext.SaveChanges();

                    //var jsonwebtokenmodel = GenerateJSONWebTokenAsync(user);
                    await _userManager.UpdateAsync(user);
                    Logger.LogInfo(model.Email + " Sucessfully Logged Into Blitzer");
                    return Ok();
                    //return Ok(jsonwebtokenmodel);
                }
                else
                {
                    Logger.LogError(model.Email + " user password was incorret");
                    return Unauthorized("Invalid email/password.");
                }
            } else
            {
                Logger.LogError("Login failed because Email Addr was not confirmed");
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Post(string aUser, RegisterMerchantModel model)
        {
            Logger.LogInfo(aUser);
            Logger.LogInfo((model == null).ToString());
            if (ModelState.IsValid)
            {

                return Ok(new JsonModel(JsonType.Success, "Updated Client successfully."));
            }

            // If execution got this far, something failed, redisplay the form.
            return Ok(new JsonModel(JsonType.Error, GetErrorStringFromModelState(ModelState)));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMerchant(RegisterMerchantModel model)
        {

            Logger.LogInfo("RegisterMerchant called with Email=" + model.Emails[0].Address + " First Name=" + model.FirstName + " Last Name=" + model.LastName);
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new BlitzerUser { UserName = model.Emails[0].Address, Email = model.Emails[0].Address, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber, EmailConfirmed = false };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(Defines.CLIENT_ROLE);
                        if (role == null)
                        {
                            await _roleManager.CreateAsync(new IdentityRole { Name = Defines.CLIENT_ROLE });
                        }

                        await _userManager.AddToRoleAsync(user, Defines.CLIENT_ROLE);
                        //var lMerchant = new Merchant(model) { Id = user.Id };
                        //new MerchantBusiness(mContext, _config).Save(lMerchant);

                       // Logger.LogInfo("Sucessfully RegisterMerchant");
                       // return Ok(new JsonModel(JsonType.Success, "Registered merchant successfully."));
                    }

                    Logger.LogError("Failed to RegisterMerchant : " + string.Join(",", result.Errors.Select(s => s.Description)));
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


        [HttpPost]
        public async Task<IActionResult> RegisterClient([FromForm]RegisterViewModel model)
        {
            Logger.LogInfo("RegisterClient called with Email=" + model.Email + " First Name=" + model.FirstName + " Last Name=" + model.LastName);
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new BlitzerUser { UserName = model.Email[0], Email = model.Email[0], FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber, EmailConfirmed = false };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(Defines.CLIENT_ROLE);
                        if (role == null)
                        {
                            await _roleManager.CreateAsync(new IdentityRole { Name = Defines.CLIENT_ROLE });
                        }

                        await _userManager.AddToRoleAsync(user, Defines.CLIENT_ROLE);
                        var lClient = new BlitzerCore.Models.Client() { Id = user.Id };
                        //new ClientBusiness(mContext, new DataServices.ConcreteFactory().GetClientDataService(mContext)).Save(lClient);

                        //Logger.LogInfo("Sucessfully RegisterClient");
                        //return Ok(new JsonModel(JsonType.Success, "Registered user successfully."));
                    }

                    Logger.LogError("Failed to RegisterClient : " + string.Join(",", result.Errors.Select(s => s.Description)));
                    return Ok(new JsonModel(JsonType.Error, string.Join(",", result.Errors.Select(s => s.Description))));
                }
            } catch ( Exception e)
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