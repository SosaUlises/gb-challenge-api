namespace GrupoBlancoChallenge.Domain.Entities
{
    public class GameSessionScenario
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid GameSessionId { get; private set; }
        public GameSession GameSession { get; private set; } = null!;

        public Guid ScenarioId { get; private set; }
        public Scenario Scenario { get; private set; } = null!;

        public int Position { get; private set; }

        public int Month { get; private set; }

        private GameSessionScenario() { }

        public GameSessionScenario(
            Guid gameSessionId,
            Guid scenarioId,
            int position,
            int month)
        {
            GameSessionId = gameSessionId;
            ScenarioId = scenarioId;
            Position = position;
            Month = month;
        }
    }
}
