using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrupoBlancoChallenge.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<GameSession> GameSessions { get; }
        DbSet<Scenario> Scenarios { get; }
        DbSet<DecisionOption> DecisionOptions { get; }
        DbSet<RankingEntry> RankingEntries { get; }
        DbSet<GameDecision> GameDecisions { get; }
        DbSet<GameSessionScenario> GameSessionScenarios { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
