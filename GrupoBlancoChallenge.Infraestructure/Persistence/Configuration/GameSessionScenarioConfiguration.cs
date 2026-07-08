using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class GameSessionScenarioConfiguration : IEntityTypeConfiguration<GameSessionScenario>
    {
        public void Configure(EntityTypeBuilder<GameSessionScenario> builder)
        {
            builder.ToTable("game_session_scenarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Position)
                .IsRequired();

            builder.Property(x => x.Month)
                .IsRequired();

            builder.HasOne(x => x.GameSession)
                .WithMany()
                .HasForeignKey(x => x.GameSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Scenario)
                .WithMany()
                .HasForeignKey(x => x.ScenarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.GameSessionId, x.Position })
                .IsUnique();

            builder.HasIndex(x => new { x.GameSessionId, x.Month })
                .IsUnique();
        }
    }
}
