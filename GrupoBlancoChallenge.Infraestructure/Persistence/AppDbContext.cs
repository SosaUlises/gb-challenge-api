using GrupoBlancoChallenge.Application.Interfaces;
using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrupoBlancoChallenge.Infraestructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameDecision> GameDecisions => Set<GameDecision>();
        public DbSet<GameSession> GameSessions => Set<GameSession>();
        public DbSet<Scenario> Scenarios => Set<Scenario>();
        public DbSet<DecisionOption> DecisionOptions => Set<DecisionOption>();
        public DbSet<RankingEntry> RankingEntries => Set<RankingEntry>();
        public DbSet<GameSessionScenario> GameSessionScenarios => Set<GameSessionScenario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
