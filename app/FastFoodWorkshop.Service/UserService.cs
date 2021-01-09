namespace FastFoodWorkshop.Service
{
    using AutoMapper;
    using Contracts;
    using Extensions;
    using FastFoodWorkshop.Data;
    using ServiceModels.User;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Common.WebConstants;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly IFacebookService facebookService;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IRepository<Complaint> complaintRepository;
        private readonly UserManager<FastFoodUser> userManager;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IRepository<FastFoodUser> userRepository;
        private readonly IRepository<IdentityRole<int>> roleRepository;
        private readonly IRepository<IdentityUserRole<int>> userRoleRepository;
        private readonly SignInManager<FastFoodUser> signInManager;

        public UserService(
            IFacebookService facebookService,
            IRepository<FastFoodUser> userRepository,
            IRepository<IdentityUserRole<int>> userRoleRepository,
            IRepository<IdentityRole<int>> roleRepository,
            RoleManager<IdentityRole<int>> roleManager,
            ILogger<UserService> logger,
            IRepository<Complaint> complaintRepository,
            UserManager<FastFoodUser> userManager,
            SignInManager<FastFoodUser> signInManager,
            IMapper mapper)
        {
            this.facebookService = facebookService;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
            this.roleManager = roleManager;
            this.logger = logger;
            this.userManager = userManager;
            this.complaintRepository = complaintRepository;
            this.mapper = mapper;
        }

        public async Task LoginUserAsync(UserLoginViewModel model)
        {
            var result = await this.signInManager.PasswordSignInAsync(
                model.Username, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                this.logger.LogInformation(LogMessages.UserLoggedIn);
            }
            else
            {
                this.logger.LogWarning(ErrorMessages.InvalidLoginAttempt);
            }
        }

        public async Task RegisterUserAsync(UserRegisterViewModel model)
        {
            FastFoodUser user = null;

            try
            {
                user = this.mapper.Map<FastFoodUser>(model);

                user.Picture = await model.Picture.UploadAsync();

                var result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    await this.userManager.AddToRoleAsync(user, StringConstants.UserRole);
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation(LogMessages.UserWasCreated);
                }

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task LogoutUserAsync()
        {
            try
            {
                await this.signInManager.SignOutAsync();
                this.logger.LogInformation(LogMessages.UserSignedOut);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<FastFoodUser> GetUserInRoleAsync(string role)
        {
            var userRole = await this.roleManager.FindByNameAsync(role);

            if (userRole == null)
            {
                throw new ApplicationException(ErrorMessages.RoleDoesNotExist);
            }

            var roleId = roleRepository.All().SingleOrDefault(e => e.Name == userRole.Name).Id;

            var identityUserRole = this.userRoleRepository.All().FirstOrDefault(e => e.RoleId == roleId);

            if (identityUserRole == null)
            {
                this.logger.LogWarning(ErrorMessages.RoleDoesNotExist);
                return null;
            }

            var user = this.userRepository.All().FirstOrDefault(e => e.Id == identityUserRole.UserId);

            return user;
        }

        public Task<FastFoodUser> CreateManagerAsync(
            string firstName,
            string lastName,
            string userName,
            string dateOfBirth,
            string address,
            string email,
            string phoneNumber)
        {
            DateTime birthDate;
            FastFoodUser manager = null;

            try
            {
                birthDate = DateTime.ParseExact(dateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                manager = new FastFoodUser()
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Address = address,
                    Email = email,
                    PhoneNumber = phoneNumber,
                };
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return Task.FromResult<FastFoodUser>(manager);
        }

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(ClaimsPrincipal user)
        {
            UserDetailsViewModel viewModel = null;

            var currentUser = await this.userManager.GetUserAsync(user);

            if (currentUser == null)
            {
                this.logger.LogWarning(ErrorMessages.UnableToLoadUser);
                throw new ApplicationException(ErrorMessages.UnableToLoadUser);
            }

            try
            {
                viewModel = this.mapper.Map<UserDetailsViewModel>(currentUser);

                if (currentUser.Picture != null && currentUser.Picture.Length <= NumericConstants.PictureSizeLimit)
                {
                    if (currentUser.PasswordHash == null)
                    {
                        viewModel.Picture = Encoding.ASCII.GetString(currentUser.Picture);
                    }
                    else
                    {
                        viewModel.Picture = Convert.ToBase64String(currentUser.Picture);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return viewModel;
        }

        public async Task EditUserAsync(ClaimsPrincipal user, UserDetailsViewModel viewModel)
        {
            var currentUser = await this.userManager.GetUserAsync(user);

            if (currentUser == null)
            {
                this.logger.LogWarning(ErrorMessages.UnableToLoadUser);
                throw new ApplicationException(ErrorMessages.UnableToLoadUser);
            }

            try
            {
                currentUser.UserName = viewModel.Username;
                currentUser.FirstName = viewModel.FirstName;
                currentUser.LastName = viewModel.LastName;
                currentUser.BirthDate = viewModel.BirthDate;
                currentUser.Address = viewModel.Address;
                currentUser.PhoneNumber = viewModel.PhoneNumber;

                await this.userManager.UpdateAsync(currentUser);

                logger.LogInformation(string.Format(LogMessages.UserWasUpdated, currentUser.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task AddComplaintAsync(UserComplaintViewModel model, ClaimsPrincipal user)
        {
            Complaint complaint = null;

            var currentUser = await this.userManager.GetUserAsync(user);

            if (currentUser == null)
            {
                this.logger.LogWarning(ErrorMessages.UnableToLoadUser);
                throw new ApplicationException(ErrorMessages.UnableToLoadUser);
            }

            try
            {
                complaint = this.mapper.Map<Complaint>(model);

                complaint.FastFoodUserId = currentUser.Id;
                complaint.RestaurantId = int.Parse(model.RestaurantId);

                await this.complaintRepository.AddAsync(complaint);

                await this.complaintRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.UserAddedComplaint, currentUser.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<string> GetUserForgotPasswordCodeAsync(UserForfotPasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new ApplicationException(ErrorMessages.UnableToLoadUser);
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);

            return code;
        }

        public async Task<ExternalLoginInfo> GetExternalLoginConfirmationAsync(UserExternalLoginViewModel model, string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var facebookData = await this.facebookService.GetFacebookInfoAsync();
            byte[] pictureInBytes = Encoding.ASCII.GetBytes(facebookData[2]);

            var user = new FastFoodUser { UserName = model.Email, Email = model.Email, FirstName = facebookData[0], LastName = facebookData[1], Picture = pictureInBytes };

            var result = await userManager.CreateAsync(user);
            await this.userManager.AddToRoleAsync(user, StringConstants.UserRole);

            if (result.Succeeded)
            {
                result = await userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            return info;
        }
    }
}
