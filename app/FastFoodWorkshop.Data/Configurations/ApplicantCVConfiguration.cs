namespace FastFoodWorkshop.Data.Configurations
{
    using FastFoodWorkshop.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicantCVConfiguration : IEntityTypeConfiguration<ApplicantCV>
    {
        public void Configure(EntityTypeBuilder<ApplicantCV> builder)
        {
            builder.HasMany(e => e.Schools)
                .WithOne(e => e.ApplicantCV)
                .HasForeignKey(e => e.ApplicantCVId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.PreviousJobs)
                .WithOne(e => e.ApplicantCV)
                .HasForeignKey(e => e.ApplicantCVId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
