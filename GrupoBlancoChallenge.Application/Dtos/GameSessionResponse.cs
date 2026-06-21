namespace GrupoBlancoChallenge.Application.Dtos
{
    public class GameSessionResponse
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;

        public int Rentability { get; set; }
        public int Clients { get; set; }
        public int OrganizationalClimate { get; set; }
        public int BrandImage { get; set; }
        public int OperationalEfficiency { get; set; }

        public int CurrentScenarioOrder { get; set; }
        public bool IsFinished { get; set; }
        public int FinalScore { get; set; }
        public string FinalRating { get; set; } = string.Empty;

        public ScenarioResponse? CurrentScenario { get; set; }
    }
}
