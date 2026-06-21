namespace GrupoBlancoChallenge.Domain.Entities
{
    public class RankingEntry
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string PlayerName { get; private set; } = string.Empty;

        public int FinalScore { get; private set; }

        public DateTime PlayedAt { get; private set; } = DateTime.UtcNow;
        public string FinalRating { get; private set; } = string.Empty;

        private RankingEntry() { }

        public RankingEntry(string playerName, int finalScore, string finalRating)
        {
            PlayerName = playerName;
            FinalScore = finalScore;
            FinalRating = finalRating;
        }
    }
}
