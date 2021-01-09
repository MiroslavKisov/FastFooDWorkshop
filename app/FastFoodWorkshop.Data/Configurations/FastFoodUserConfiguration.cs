namespace FastFoodWorkshop.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FastFoodUserConfiguration : IEntityTypeConfiguration<FastFoodUser>
    {
        public void Configure(EntityTypeBuilder<FastFoodUser> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Recepies)
                .WithOne(e => e.FastFoodUser)
                .HasForeignKey(e => e.FastFoodUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Orders)
                .WithOne(e => e.FastFoodUser)
                .HasForeignKey(e => e.FastFoodUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Complaints)
                .WithOne(e => e.FastFoodUser)
                .HasForeignKey(e => e.FastFoodUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
