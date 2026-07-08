namespace GrupoBlancoChallenge.Application.Dtos
{
    public class RankingResponse
    {
        public string PlayerName { get; set; } = string.Empty;

        public int FinalScore { get; set; }
        public string FinalRating { get; set; } = string.Empty;
        public bool WasCompanySold { get; set; }
        public int FinishedAtMonth { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
