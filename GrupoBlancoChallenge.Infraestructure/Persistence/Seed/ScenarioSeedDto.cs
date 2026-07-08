using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Seed
{
    public class ScenarioSeedDto
    {
        public int Order { get; set; }

        public int Month { get; set; }

        public string Quarter { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public List<DecisionOptionSeedDto> Options { get; set; } = new();
    }

    public class DecisionOptionSeedDto
    {
        public string Text { get; set; } = string.Empty;

        public string Consequence { get; set; } = string.Empty;

        public int RentabilityImpact { get; set; }

        public int ClientsImpact { get; set; }

        public int OrganizationalClimateImpact { get; set; }

        public int BrandImageImpact { get; set; }

        public int OperationalEfficiencyImpact { get; set; }
    }
}
