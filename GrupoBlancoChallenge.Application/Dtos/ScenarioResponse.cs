namespace GrupoBlancoChallenge.Application.Dtos
{
    public class ScenarioResponse
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public int Month { get; set; }

        public string Quarter { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public List<DecisionOptionResponse> Options { get; set; } = new();
    }
}
