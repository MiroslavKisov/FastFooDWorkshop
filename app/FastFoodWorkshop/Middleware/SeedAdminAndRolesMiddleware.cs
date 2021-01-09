namespace FastFoodWorkshop.Middleware
{
    using Common.WebConstants;
    using Models;
    using Microsoft.AspNetCore.Identity;
    using Service.Contracts;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class SeedAdminAndRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedAdminAndRolesMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<FastFoodUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IUserService userService)
        {

            if(!roleManager.RoleExistsAsync(StringConstants.ManagerRole).Result)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(StringConstants.ManagerRole));
            }
            if (!roleManager.RoleExistsAsync(StringConstants.UserRole).Result)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(StringConstants.UserRole));
            }

            var user = await userService.GetUserInRoleAsync(StringConstants.ManagerRole);

            if (user == null)
            {
                user = await userService.CreateManagerAsync(
                      Configuration[Security.AdminInfoManagerFirstName],
                      Configuration[Security.AdminInfoManagerLastName],
                      Configuration[Security.AdminInfoManagerName],
                      Configuration[Security.AdminInfoManagerBirthDate],
                      Configuration[Security.AdminInfoManagerAddress],
                      Configuration[Security.AdminInfoManagerEmail],
                      Configuration[Security.AdminInfoManagerPhoneNumber]);

                await userManager.CreateAsync(user, Configuration[Security.AdminInfoManagerPassword]);
                await userManager.AddToRoleAsync(user, StringConstants.ManagerRole);
            }

            await this.next(context);
        }
    }
}
