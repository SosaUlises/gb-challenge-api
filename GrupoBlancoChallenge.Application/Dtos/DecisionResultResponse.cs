namespace GrupoBlancoChallenge.Application.Dtos
{
    public class DecisionResultResponse
    {
        public string Consequence { get; set; } = string.Empty;

        public GameSessionResponse GameSession { get; set; } = null!;
    }
}
