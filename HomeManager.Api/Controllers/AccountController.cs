using HomeManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HomeManager.WebApplication.ViewModels;
using HomeManager.WebApplication.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HomeManager.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager, 
            ILogger<AccountController> logger, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View("~/Pages/Account/AccessDenied.cshtml");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var model = new StatusMessageModel();

            if (userId == null || code == null)
            {
                return RedirectToAction("/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            model.StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return View("~/Pages/Account/ConfirmEmail.cshtml", model);
        }

        public async Task<IActionResult> ConfirmEmailChange(string userId, string email, string code)
        {
            var model = new StatusMessageModel();

            if (userId == null || email == null || code == null)
            {
                return RedirectToAction("/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                model.StatusMessage = "Error changing email.";
                return View("~/Pages/Account/ConfirmEmailChange.cshtml", model);
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                model.StatusMessage = "Error changing user name.";
                return View("~/Pages/Account/ConfirmEmailChange.cshtml", model);
            }

            await _signInManager.RefreshSignInAsync(user);
            model.StatusMessage = "Thank you for confirming your email change.";
            return View("~/Pages/Account/ConfirmEmailChange.cshtml", model);
        }

        public async Task<IActionResult> ExternalLogin()
        {
            return RedirectToAction("~/Pages/Account/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("~/Pages/Account/CallbackAsync.cshtml", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> CallbackAsync(ExternalLoginModel model, string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                model.ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction("~/Pages/Account/Login.cshtml", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                model.ErrorMessage = "Error loading external login information.";
                return RedirectToAction("~/Pages/Account/Login.cshtml", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction("~/Pages/Account/Lockout.cshtml");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                model.ReturnUrl = returnUrl;
                model.ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    model.Input = new ExternalLoginInputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return View("~/Pages/Account/CallbackAsync.cshtml", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmationAsync(ExternalLoginModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                model.ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToAction("~/Pages/Account/Login.cshtml", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Input.Email, Email = model.Input.Email };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action("~/Pages/Account/ConfirmEmail.cshtml", values: new { userId = userId, code = code });

                        await _emailSender.SendEmailAsync(model.Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToAction("~/Pages/Account/RegisterConfirmation.cshtml", new { Email = model.Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.ProviderDisplayName = info.ProviderDisplayName;
            model.ReturnUrl = returnUrl;
            return View("~/Pages/Account/ConfirmationAsync.cshtml", model);
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View("~/Pages/Account/ForgotPassword.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("~/Pages/Account/ForgotPasswordConfirmation.cshtml");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("~/Pages/Account/ResetPassword.cshtml", values: new { code });

                await _emailSender.SendEmailAsync(
                    model.Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("~/Pages/Account/ForgotPasswordConfirmation.cshtml");
            }
            return View("~/Pages/Account/ForgotPassword.cshtml");
        }

        public async Task<IActionResult> ForgotPasswordConfirmation()
        {
            return View("~/Pages/Account/ForgotPasswordConfirmation.cshtml");
        }

        public async Task<IActionResult> Lockout()
        {
            return View("~/Pages/Account/Lockout.cshtml");
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var model = new LoginModel();

            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            model.ReturnUrl = returnUrl;

            return View("~/Pages/Account/Login.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var mailCheck = new EmailAddressAttribute();

                var result = new Microsoft.AspNetCore.Identity.SignInResult();

                if (mailCheck.IsValid(model.Input.Email))
                {
                    var userName = _userManager.FindByEmailAsync(model.Input.Email).Result.UserName;

                    result = await _signInManager.PasswordSignInAsync(userName, model.Input.Password, model.Input.RememberMe, lockoutOnFailure: false);
                }
                else
                {
                    result = await _signInManager.PasswordSignInAsync(model.Input.Email, model.Input.Password, model.Input.RememberMe, lockoutOnFailure: false);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true               
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("~/Pages/Account/LoginWith2fa.cshtml", new { ReturnUrl = returnUrl, RememberMe = model.Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction("~/Pages/Account/Lockout.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View("~/Pages/Account/Login.cshtml", model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Pages/Account/Login.cshtml", model);
        }

        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            var model = new LoginWith2faModel();
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            model.ReturnUrl = returnUrl;
            model.RememberMe = rememberMe;

            return View("~/Pages/Account/LoginWith2fa.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Pages/Account/LoginWith2fa.cshtml", model);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = model.Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.Input.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToAction("~/Pages/Account/Lockout.cshtml");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View("~/Pages/Account/LoginWith2fa.cshtml", model);
            }
        }

        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            var model = new LoginWithRecoveryCodeModel();

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            model.ReturnUrl = returnUrl;

            return View("~/Pages/Account/LoginWithRecoveryCode.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Pages/Account/LoginWithRecoveryCode.cshtml", model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToAction("~/Pages/Account/Lockout.cshtml");
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View("~/Pages/Account/LoginWithRecoveryCode.cshtml", model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            return View("~/Pages/Account/Logout.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("~/Pages/Account/Logout.cshtml");
            }
        }

        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var register = new RegisterModel();
            register.ReturnUrl = returnUrl;
            register.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View("~/Pages/Account/Register.cshtml", register);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Input.UserName, Email = model.Input.Email, FirstName = model.Input.Name, LastName = model.Input.Lastname };
                var result = await _userManager.CreateAsync(user, model.Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action("~/Pages/Account/ConfirmEmail.cshtml", values: new { userId = user.Id, code = code, returnUrl = returnUrl });

                    await _emailSender.SendEmailAsync(model.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("~/Pages/Account/RegisterConfirmation.cshtml", new { email = model.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Pages/Account/Register.cshtml", model);
        }

        public async Task<IActionResult> RegisterConfirmation(string email, string returnUrl = null)
        {
            var model = new RegisterConfirmationModel();

            if (email == null)
            {
                return RedirectToAction("/");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            model.Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            model.DisplayConfirmAccountLink = true;
            if (model.DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                model.EmailConfirmationUrl = Url.Action("~/Pages/Account/ConfirmEmail.cshtml", values: new { userId = userId, code = code, returnUrl = returnUrl });
            }
            return View("~/Pages/Account/RegisterConfirmation.cshtml");
        }

        public async Task<IActionResult> ResendEmailConfirmation()
        {
            return View("~/Pages/Account/ResendEmailConfirmation.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Pages/Account/ResendEmailConfirmation.cshtml", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return View("~/Pages/Account/ResendEmailConfirmation.cshtml", model);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("~/Pages/Account/ConfirmEmail.cshtml", values: new { userId = userId, code = code });
            await _emailSender.SendEmailAsync(
                model.Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return View("~/Pages/Account/ResendEmailConfirmation.cshtml", model);
        }

        public async Task<IActionResult> ResetPassword(string code = null)
        {
            var model = new ResetPasswordModel();

            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                model.Input = new ResetPasswordInputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return View("~/Pages/Account/ResetPassword.cshtml", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Pages/Account/ResetPassword.cshtml", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("~/Pages/Account/ResetPasswordConfirmation.cshtml");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Input.Code, model.Input.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("~/Pages/Account/ResetPasswordConfirmation.cshtml");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("~/Pages/Account/ResetPassword.cshtml", model);
        }

        public async Task<IActionResult> ResetPasswordConfirmation()
        {
            return View("~/Pages/Account/ResetPasswordConfirmation.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirmation(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Pages/Account/ResetPasswordConfirmation.cshtml", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return View("~/Pages/Account/ResetPasswordConfirmation.cshtml", model);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("~/Pages/Account/ConfirmEmail.cshtml", values: new { userId = userId, code = code });
            await _emailSender.SendEmailAsync(
                model.Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");

            return View("~/Pages/Account/ResetPasswordConfirmation.cshtml", model);
        }
    }
}
