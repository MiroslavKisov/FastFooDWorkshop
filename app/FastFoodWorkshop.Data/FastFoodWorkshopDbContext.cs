namespace FastFoodWorkshop.Data
{
    using Configurations;
    using FastFoodWorkshop.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FastFoodWorkshopDbContext : IdentityDbContext<FastFoodUser, IdentityRole<int> ,int>
    {
        public FastFoodWorkshopDbContext(DbContextOptions<FastFoodWorkshopDbContext> options)
            : base(options)
        {
        }

        public FastFoodWorkshopDbContext()
        {
        }

        public DbSet<ApplicantCV> ApplicantsCVs { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<DeliveryCar> DeliveryCars { get; set; }
        public DbSet<Education> Schools { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }       

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder.UseLazyLoadingProxies());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FastFoodUserConfiguration());
            builder.ApplyConfiguration(new FastFoodUserLoginConfiguration());
            builder.ApplyConfiguration(new FastFoodUserRoleConfiguration());
            builder.ApplyConfiguration(new FastFoodUserTokenConfiguration());
            builder.ApplyConfiguration(new RestaurantConfiguration());
            builder.ApplyConfiguration(new ApplicantCVConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
