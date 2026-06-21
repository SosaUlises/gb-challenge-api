namespace GrupoBlancoChallenge.Domain.Entities
{
    public class GameSession
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string PlayerName { get; private set; } = string.Empty;

        public int Rentability { get; private set; } = 50;
        public int Clients { get; private set; } = 50;
        public int OrganizationalClimate { get; private set; } = 50;
        public int BrandImage { get; private set; } = 50;
        public int OperationalEfficiency { get; private set; } = 50;

        public int CurrentScenarioOrder { get; private set; } = 1;

        public bool IsFinished { get; private set; }

        public int FinalScore { get; private set; }
        public string FinalRating { get; private set; } = string.Empty;

        private GameSession() { }

        public GameSession(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("El nombre del jugador es obligatorio.");

            PlayerName = playerName.Trim();
        }

        public void ApplyDecision(DecisionOption option, bool isLastScenario)
        {
            if (IsFinished)
                throw new InvalidOperationException("La partida ya finalizó.");

            Rentability = Clamp(Rentability + option.RentabilityImpact);
            Clients = Clamp(Clients + option.ClientsImpact);
            OrganizationalClimate = Clamp(OrganizationalClimate + option.OrganizationalClimateImpact);
            BrandImage = Clamp(BrandImage + option.BrandImageImpact);
            OperationalEfficiency = Clamp(OperationalEfficiency + option.OperationalEfficiencyImpact);

            if (isLastScenario)
            {
                Finish();
                return;
            }

            CurrentScenarioOrder++;
        }

        private void Finish()
        {
            IsFinished = true;

            FinalScore = (int)Math.Round(
                Rentability * 0.30 +
                Clients * 0.20 +
                BrandImage * 0.20 +
                OrganizationalClimate * 0.15 +
                OperationalEfficiency * 0.15
            );

            FinalRating = CalculateFinalRating(FinalScore);
        }

        private static string CalculateFinalRating(int finalScore)
        {
            return finalScore switch
            {
                >= 90 => "Visionario Empresarial",
                >= 80 => "Director Estratégico",
                >= 70 => "Gestor Competitivo",
                >= 60 => "Administrador Conservador",
                _ => "Gestión en Crisis"
            };
        }

        private static int Clamp(int value)
        {
            if (value < 0) return 0;
            if (value > 100) return 100;
            return value;
        }
    }
}
