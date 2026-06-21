namespace GrupoBlancoChallenge.Application.Dtos
{
    public class RankingResponse
    {
        public string PlayerName { get; set; } = string.Empty;

        public int FinalScore { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
