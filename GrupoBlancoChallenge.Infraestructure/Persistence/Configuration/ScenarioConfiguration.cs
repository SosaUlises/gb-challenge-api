using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class ScenarioConfiguration : IEntityTypeConfiguration<Scenario>
    {
        public void Configure(EntityTypeBuilder<Scenario> builder)
        {
            builder.ToTable("scenarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Order)
                .IsRequired();

            builder.Property(x => x.Month)
                .IsRequired();

            builder.Property(x => x.Quarter)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Topic)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Options)
                .WithOne(x => x.Scenario)
                .HasForeignKey(x => x.ScenarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
