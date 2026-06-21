using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class GameDecisionConfiguration : IEntityTypeConfiguration<GameDecision>
    {
        public void Configure(EntityTypeBuilder<GameDecision> builder)
        {
            builder.ToTable("game_decisions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.GameSession)
                .WithMany()
                .HasForeignKey(x => x.GameSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Scenario)
                .WithMany()
                .HasForeignKey(x => x.ScenarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DecisionOption)
                .WithMany()
                .HasForeignKey(x => x.DecisionOptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
