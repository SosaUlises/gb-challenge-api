using GrupoBlancoChallenge.Application.Dtos;
using GrupoBlancoChallenge.Application.Interfaces;
using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrupoBlancoChallenge.Application.Services
{
    public class GameService : IGameService
    {
        private const int TotalScenariosPerGame = 12;

        private readonly IApplicationDbContext _context;

        public GameService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GameSessionResponse> StartGameAsync(StartGameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PlayerName))
                throw new ArgumentException("El nombre del jugador es obligatorio.");

            var gameSession = new GameSession(request.PlayerName);
            var selectedScenarios = await SelectMonthlyScenariosAsync();

            _context.GameSessions.Add(gameSession);
            _context.GameSessionScenarios.AddRange(
                selectedScenarios.Select((scenario, index) =>
                    new GameSessionScenario(
                        gameSession.Id,
                        scenario.Id,
                        index + 1,
                        scenario.Month
                    )));

            await _context.SaveChangesAsync();

            return await BuildGameSessionResponseAsync(gameSession);
        }

        public async Task<GameSessionResponse?> GetGameAsync(Guid gameSessionId)
        {
            var gameSession = await _context.GameSessions
                .FirstOrDefaultAsync(x => x.Id == gameSessionId);

            if (gameSession is null)
                return null;

            return await BuildGameSessionResponseAsync(gameSession);
        }

        public async Task<DecisionResultResponse> ChooseOptionAsync(ChooseOptionRequest request)
        {
            var gameSession = await _context.GameSessions
                .FirstOrDefaultAsync(x => x.Id == request.GameSessionId);

            if (gameSession is null)
                throw new InvalidOperationException("La partida no existe.");

            if (gameSession.IsFinished)
                throw new InvalidOperationException("La partida ya finalizó.");

            var currentScenario = await GetCurrentScenarioAsync(gameSession);

            if (currentScenario is null)
                throw new InvalidOperationException("No se encontró el escenario actual.");

            var option = currentScenario.Options
                .FirstOrDefault(x => x.Id == request.OptionId);

            if (option is null)
                throw new InvalidOperationException("La opción seleccionada no pertenece al escenario actual.");

            var isLastScenario = gameSession.CurrentScenarioOrder >= TotalScenariosPerGame;

            gameSession.ApplyDecision(option, isLastScenario);

            var gameDecision = new GameDecision(
                gameSession.Id,
                currentScenario.Id,
                option.Id
            );

            _context.GameDecisions.Add(gameDecision);

            if (gameSession.IsFinished)
            {
                var rankingEntry = new RankingEntry(
                     gameSession.PlayerName,
                     gameSession.FinalScore,
                     gameSession.FinalRating
                 );

                _context.RankingEntries.Add(rankingEntry);
            }

            await _context.SaveChangesAsync();

            return new DecisionResultResponse
            {
                Consequence = option.Consequence,
                GameSession = await BuildGameSessionResponseAsync(gameSession)
            };
        }

        public async Task<List<RankingResponse>> GetRankingAsync()
        {
            return await _context.RankingEntries
                .OrderByDescending(x => x.FinalScore)
                .ThenBy(x => x.PlayedAt)
                .Take(20)
                .Select(x => new RankingResponse
                {
                    PlayerName = x.PlayerName,
                    FinalScore = x.FinalScore,
                    FinalRating = x.FinalRating,
                    PlayedAt = x.PlayedAt
                })
                .ToListAsync();
        }

        private async Task<GameSessionResponse> BuildGameSessionResponseAsync(GameSession gameSession)
        {
            ScenarioResponse? currentScenario = null;

            if (!gameSession.IsFinished)
            {
                var scenario = await GetCurrentScenarioAsync(gameSession);

                if (scenario is not null)
                {
                    currentScenario = new ScenarioResponse
                    {
                        Id = scenario.Id,
                        Order = scenario.Order,
                        Month = scenario.Month,
                        Quarter = scenario.Quarter,
                        Title = scenario.Title,
                        Description = scenario.Description,
                        Topic = scenario.Topic,
                        Options = scenario.Options.Select(option => new DecisionOptionResponse
                        {
                            Id = option.Id,
                            Text = option.Text
                        }).ToList()
                    };
                }
            }

            return new GameSessionResponse
            {
                Id = gameSession.Id,
                PlayerName = gameSession.PlayerName,
                Rentability = gameSession.Rentability,
                Clients = gameSession.Clients,
                OrganizationalClimate = gameSession.OrganizationalClimate,
                BrandImage = gameSession.BrandImage,
                OperationalEfficiency = gameSession.OperationalEfficiency,
                CurrentScenarioOrder = gameSession.CurrentScenarioOrder,
                IsFinished = gameSession.IsFinished,
                FinalScore = gameSession.FinalScore,
                FinalRating = gameSession.FinalRating,
                CurrentScenario = currentScenario
            };
        }

        private async Task<List<Scenario>> SelectMonthlyScenariosAsync()
        {
            var scenarioPool = await _context.Scenarios
                .Where(x =>
                    x.Month >= 1 &&
                    x.Month <= TotalScenariosPerGame &&
                    x.Order >= 100)
                .ToListAsync();

            if (scenarioPool.Count == 0)
            {
                scenarioPool = await _context.Scenarios
                    .Where(x => x.Month >= 1 && x.Month <= TotalScenariosPerGame)
                    .ToListAsync();
            }

            var selectedScenarios = new List<Scenario>();

            for (var month = 1; month <= TotalScenariosPerGame; month++)
            {
                var monthScenarios = scenarioPool
                    .Where(x => x.Month == month)
                    .ToList();

                if (monthScenarios.Count == 0)
                    throw new InvalidOperationException($"No hay escenarios cargados para el mes {month}.");

                selectedScenarios.Add(monthScenarios[Random.Shared.Next(monthScenarios.Count)]);
            }

            return selectedScenarios;
        }

        private async Task<Scenario?> GetCurrentScenarioAsync(GameSession gameSession)
        {
            var gameSessionScenario = await _context.GameSessionScenarios
                .Include(x => x.Scenario)
                .ThenInclude(x => x.Options)
                .FirstOrDefaultAsync(x =>
                    x.GameSessionId == gameSession.Id &&
                    x.Position == gameSession.CurrentScenarioOrder);

            if (gameSessionScenario is not null)
                return gameSessionScenario.Scenario;

            return await _context.Scenarios
                .Include(x => x.Options)
                .FirstOrDefaultAsync(x => x.Order == gameSession.CurrentScenarioOrder);
        }
    }
}
