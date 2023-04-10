using BlitzerCore.Models;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApp.DataServices;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        const string ClassName = "LoginModel::";

        private readonly UserManager<BlitzerUser> _userManager;
        private readonly SignInManager<BlitzerUser> _signInManager;
        private readonly IDbContext DbContext;

        public LoginModel(SignInManager<BlitzerUser> signInManager,
            UserManager<BlitzerUser> userManager,
            IDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            DbContext = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async System.Threading.Tasks.Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async System.Threading.Tasks.Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            const string FuncName = "OnPostASync - ";

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {  
                    Logger.LogInfo($"{FuncName} {Input.Email} just logged in.");
                    // Resolve the user via their email
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    // Get the roles for the user
                    var roles = await _userManager.GetRolesAsync(user);
                    if (returnUrl == "/")
                    {
                        if (roles.Contains("Administrator") == true || roles.Contains("Agent") == true)
                        {
                            Logger.LogInfo($"{FuncName} Redirecting to Portal");
                            return LocalRedirect("/Portal");
                        }
                        else
                        {
                            Logger.LogInfo($"{FuncName} Redirecting to Client");
                            return LocalRedirect("/Client");
                        }
                    }
                    else
                    {
                        Logger.LogInfo($"{FuncName} Returning to Redirect URL");
                        return LocalRedirect(returnUrl);
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    Logger.LogInfo($"{FuncName} Login::Redirectoring because requires2Factor Authenications");
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    Logger.LogInfo($"{FuncName} User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    Logger.LogInfo($"{FuncName} Invalid login attempt. Returning to login page");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    Logger.LogWarning($"Invalid login attempt for {Input.Email}");
                    return Page();
                }
            }

            Logger.LogInfo($"{FuncName} Login::Model State is Invalid.  Returning to Login Page");
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
