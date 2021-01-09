namespace FastFoodWorkshop.Service.Contracts
{
    using ServiceModels.User;
    using Models;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;

    public interface IUserService
    {
        Task<FastFoodUser> CreateManagerAsync
            (string firstName, 
            string lastName, 
            string userName, 
            string dateOfBirth, 
            string address, 
            string email,
            string phoneNumber);

        Task<ExternalLoginInfo> GetExternalLoginConfirmationAsync(UserExternalLoginViewModel model, string returnUrl = null);

        Task RegisterUserAsync(UserRegisterViewModel model);

        Task<string> GetUserForgotPasswordCodeAsync(UserForfotPasswordViewModel model);

        Task LoginUserAsync(UserLoginViewModel model);

        Task LogoutUserAsync();

        Task<UserDetailsViewModel> GetUserDetailsAsync(ClaimsPrincipal user);

        Task AddComplaintAsync(UserComplaintViewModel model, ClaimsPrincipal user);

        Task EditUserAsync(ClaimsPrincipal user, UserDetailsViewModel viewModel);

        Task<FastFoodUser> GetUserInRoleAsync(string role);
    }
}