namespace FastFoodWorkshop.Controllers
{
    using Common.WebConstants;
    using Service.Contracts;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ServiceModels.User;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Data;
    using Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using System.Text.Encodings.Web;
    using Common.CustomValidations;

    public class UserController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly ILogger<UserController> logger;
        private readonly IFacebookService facebookService;
        private readonly UserManager<FastFoodUser> userManager;
        private readonly SignInManager<FastFoodUser> signInManager;
        private readonly IUserService userService;
        private readonly IRestaurantService restaurantService;
        private readonly IRepository<FastFoodUser> userRepository;

        public UserController(
            IEmailSender emailSender,
            ILogger<UserController> logger,
            IFacebookService facebookService,
            UserManager<FastFoodUser> userManager,
            IRepository<FastFoodUser> userRepository,
            IUserService userService,
            IRestaurantService restaurantService,
            SignInManager<FastFoodUser> signInManager)
        {
            this.emailSender = emailSender;
            this.logger = logger;
            this.facebookService = facebookService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            this.restaurantService = restaurantService;
            this.userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel model, [FromForm(Name = StringConstants.Picture)] IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                await this.userService.RegisterUserAsync(model);
                TempData[TempDataKeys.TempDataSuccessRegistrationKey] = SuccessMessages.RegistrationSuccessMessage;
            }

            return Redirect(Routes.UserLogIn);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.userService.LoginUserAsync(model);
                TempData[TempDataKeys.TempDataSuccessLoginKey] = SuccessMessages.LoginSuccessMessage;
            }

            return Redirect(Routes.Home);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.userService.LogoutUserAsync();

            return Redirect(Routes.Home);
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = this.User;
            var model = await this.userService.GetUserDetailsAsync(user);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = this.User;
            var model = await this.userService.GetUserDetailsAsync(user);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.User;
                await this.userService.EditUserAsync(user, model);
                TempData[TempDataKeys.TempDataSuccessEditKey] = SuccessMessages.EditSuccessMessage;
            }

            return this.RedirectToAction(StringConstants.MyProfile);
        }

        [Authorize]
        public async Task<IActionResult> Complaint()
        {
            var restaurants = await restaurantService.GetRestaurantsAsync();
            ViewBag.Restaurants = new SelectList(restaurants, StringConstants.Id, StringConstants.Name);
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complaint(UserComplaintViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.User;
                await this.userService.AddComplaintAsync(model, user);

                TempData[TempDataKeys.TempDataSuccessComplaintKey] = SuccessMessages.ComplaintSuccessMessage;
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), StringConstants.User, new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                this.logger.LogInformation(string.Format(LogMessages.UserExternalLoggedIn, info.Principal.Identity.Name, info.LoginProvider));
                return Redirect(Routes.Home);
            }
            else
            {
                ViewData[StringConstants.ViewDataReturnUrl] = returnUrl;
                ViewData[StringConstants.ViewDataLoginProvider] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View(StringConstants.ExternalLogin, new UserExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(UserExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var info = await this.userService.GetExternalLoginConfirmationAsync(model, returnUrl);

                this.logger.LogInformation(string.Format(LogMessages.UserCreatedExternalLogin, info.LoginProvider));
                return Redirect(Routes.Home);
            }

            return this.View(model);   
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(UserForfotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string code = null;

                try
                {
                    code = await this.userService.GetUserForgotPasswordCodeAsync(model);
                }
                catch (Exception)
                {
                    TempData[TempDataKeys.TempDataNonExistentEmailKey] = string.Format(ErrorMessages.UserWithEmailDoesNotExist, model.Email);
                }

                var callbackUrl = Url.Page(
                    Routes.UserResetPassword,
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(
                    model.Email,
                    StringConstants.ResetPassword,
                    string.Format(SuccessMessages.ForgotPassword, HtmlEncoder.Default.Encode(callbackUrl)));

                return Redirect(StringConstants.ForgotPasswordConfirmation);
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }
    }
}
