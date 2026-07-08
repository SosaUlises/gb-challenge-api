namespace GrupoBlancoChallenge.Domain.Entities
{
    public class RankingEntry
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string PlayerName { get; private set; } = string.Empty;

        public int FinalScore { get; private set; }

        public DateTime PlayedAt { get; private set; } = DateTime.UtcNow;
        public string FinalRating { get; private set; } = string.Empty;
        public bool WasCompanySold { get; private set; }
        public int FinishedAtMonth { get; private set; }

        private RankingEntry() { }

        public RankingEntry(
            string playerName,
            int finalScore,
            string finalRating,
            bool wasCompanySold,
            int finishedAtMonth)
        {
            PlayerName = playerName;
            FinalScore = finalScore;
            FinalRating = finalRating;
            WasCompanySold = wasCompanySold;
            FinishedAtMonth = finishedAtMonth;
        }
    }
}
