namespace GrupoBlancoChallenge.Domain.Entities
{
    public class Scenario
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public int Order { get; private set; }

        public int Month { get; private set; }

        public string Quarter { get; private set; } = string.Empty;

        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public string Topic { get; private set; } = string.Empty;

        public List<DecisionOption> Options { get; private set; } = new();

        private Scenario() { }

        public Scenario(int order, string title, string description, string topic)
            : this(order, order, GetQuarterFromMonth(order), title, description, topic)
        {
        }

        public Scenario(
            int order,
            int month,
            string quarter,
            string title,
            string description,
            string topic)
        {
            Order = order;
            Month = month;
            Quarter = quarter;
            Title = title;
            Description = description;
            Topic = topic;
        }

        private static string GetQuarterFromMonth(int month)
        {
            return month switch
            {
                >= 1 and <= 3 => "Q1",
                >= 4 and <= 6 => "Q2",
                >= 7 and <= 9 => "Q3",
                >= 10 and <= 12 => "Q4",
                _ => string.Empty
            };
        }
    }
}
