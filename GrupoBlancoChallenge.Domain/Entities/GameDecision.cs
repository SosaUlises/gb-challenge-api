namespace GrupoBlancoChallenge.Domain.Entities
{
    public class GameDecision
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid GameSessionId { get; private set; }
        public GameSession GameSession { get; private set; } = null!;

        public Guid ScenarioId { get; private set; }
        public Scenario Scenario { get; private set; } = null!;

        public Guid DecisionOptionId { get; private set; }
        public DecisionOption DecisionOption { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private GameDecision() { }

        public GameDecision(
            Guid gameSessionId,
            Guid scenarioId,
            Guid decisionOptionId)
        {
            GameSessionId = gameSessionId;
            ScenarioId = scenarioId;
            DecisionOptionId = decisionOptionId;
        }
    }
}
