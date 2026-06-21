namespace GrupoBlancoChallenge.Domain.Entities
{
    public class Scenario
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public int Order { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public string Topic { get; private set; } = string.Empty;

        public List<DecisionOption> Options { get; private set; } = new();

        private Scenario() { }

        public Scenario(int order, string title, string description, string topic)
        {
            Order = order;
            Title = title;
            Description = description;
            Topic = topic;
        }
    }
}
