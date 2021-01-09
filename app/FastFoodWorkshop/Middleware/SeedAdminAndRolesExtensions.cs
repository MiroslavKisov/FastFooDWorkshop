namespace FastFoodWorkshop.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedAdminAndRolesExtensions
    {
        public static IApplicationBuilder UseSeedAdminAndRoles(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedAdminAndRolesMiddleware>();
        }
    }
}
