namespace FastFoodWorkshop.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasMany(e => e.Complaints)
                .WithOne(e => e.Restaurant)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.DeliveryCars)
                .WithOne(e => e.Restaurant)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Orders)
                .WithOne(e => e.Restaurant)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
