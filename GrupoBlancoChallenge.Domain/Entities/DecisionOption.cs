namespace GrupoBlancoChallenge.Domain.Entities
{
    public class DecisionOption
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid ScenarioId { get; private set; }

        public Scenario Scenario { get; private set; } = null!;

        public string Text { get; private set; } = string.Empty;

        public string Consequence { get; private set; } = string.Empty;

        public int RentabilityImpact { get; private set; }
        public int ClientsImpact { get; private set; }
        public int OrganizationalClimateImpact { get; private set; }
        public int BrandImageImpact { get; private set; }
        public int OperationalEfficiencyImpact { get; private set; }

        private DecisionOption() { }

        public DecisionOption(
            string text,
            string consequence,
            int rentabilityImpact,
            int clientsImpact,
            int organizationalClimateImpact,
            int brandImageImpact,
            int operationalEfficiencyImpact)
        {
            Text = text;
            Consequence = consequence;
            RentabilityImpact = rentabilityImpact;
            ClientsImpact = clientsImpact;
            OrganizationalClimateImpact = organizationalClimateImpact;
            BrandImageImpact = brandImageImpact;
            OperationalEfficiencyImpact = operationalEfficiencyImpact;
        }
    }
}
