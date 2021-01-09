namespace FastFoodWorkshop.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasMany(e => e.Products)
                .WithOne(e => e.Menu)
                .HasForeignKey(e => e.MenuId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
