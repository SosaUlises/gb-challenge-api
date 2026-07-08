using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.ToTable("game_sessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlayerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Rentability).IsRequired();
            builder.Property(x => x.Clients).IsRequired();
            builder.Property(x => x.OrganizationalClimate).IsRequired();
            builder.Property(x => x.BrandImage).IsRequired();
            builder.Property(x => x.OperationalEfficiency).IsRequired();
            builder.Property(x => x.CurrentScenarioOrder).IsRequired();
            builder.Property(x => x.IsFinished).IsRequired();
            builder.Property(x => x.FinalScore).IsRequired();
            builder.Property(x => x.FinalRating)
            .IsRequired()
            .HasMaxLength(100);
            builder.Property(x => x.WasCompanySold).IsRequired();
            builder.Property(x => x.FinishedAtMonth).IsRequired();
        }
    }
}
