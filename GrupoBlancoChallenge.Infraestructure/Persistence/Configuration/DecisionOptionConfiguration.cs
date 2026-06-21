using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class DecisionOptionConfiguration : IEntityTypeConfiguration<DecisionOption>
    {
        public void Configure(EntityTypeBuilder<DecisionOption> builder)
        {
            builder.ToTable("decision_options");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Consequence)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.RentabilityImpact).IsRequired();
            builder.Property(x => x.ClientsImpact).IsRequired();
            builder.Property(x => x.OrganizationalClimateImpact).IsRequired();
            builder.Property(x => x.BrandImageImpact).IsRequired();
            builder.Property(x => x.OperationalEfficiencyImpact).IsRequired();
        }
    }
}
