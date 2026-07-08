using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Configuration
{
    public class RankingEntryConfiguration : IEntityTypeConfiguration<RankingEntry>
    {
        public void Configure(EntityTypeBuilder<RankingEntry> builder)
        {
            builder.ToTable("ranking_entries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlayerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FinalScore)
                .IsRequired();

            builder.Property(x => x.PlayedAt)
                .IsRequired();

            builder.Property(x => x.FinalRating)
            .IsRequired()
            .HasMaxLength(100);
            builder.Property(x => x.WasCompanySold).IsRequired();
            builder.Property(x => x.FinishedAtMonth).IsRequired();
        }
    }
}
