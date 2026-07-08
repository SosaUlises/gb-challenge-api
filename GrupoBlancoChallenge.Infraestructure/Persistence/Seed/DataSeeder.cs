using GrupoBlancoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBlancoChallenge.Infraestructure.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IHostEnvironment environment)
        {
            var filePath = GetSeedFilePath(environment);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No se encontró el archivo de escenarios en: {filePath}");

            var json = await File.ReadAllTextAsync(filePath);

            var scenarioDtos = JsonSerializer.Deserialize<List<ScenarioSeedDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (scenarioDtos is null || scenarioDtos.Count == 0)
                throw new InvalidOperationException("El archivo scenarios.json no contiene escenarios válidos.");

            ValidateScenarioBank(scenarioDtos);

            var existingScenarioKeys = await context.Scenarios
                .Select(x => new { x.Month, x.Title })
                .ToListAsync();

            var existingKeys = existingScenarioKeys
                .Select(x => BuildScenarioKey(x.Month, x.Title))
                .ToHashSet();

            var scenarios = scenarioDtos
                .Where(dto => !existingKeys.Contains(BuildScenarioKey(dto.Month, dto.Title)))
                .Select(dto =>
            {
                var scenario = new Scenario(
                    dto.Order,
                    dto.Month,
                    dto.Quarter,
                    dto.Title,
                    dto.Description,
                    dto.Topic
                );

                foreach (var optionDto in dto.Options)
                {
                    scenario.Options.Add(new DecisionOption(
                        optionDto.Text,
                        optionDto.Consequence,
                        optionDto.RentabilityImpact,
                        optionDto.ClientsImpact,
                        optionDto.OrganizationalClimateImpact,
                        optionDto.BrandImageImpact,
                        optionDto.OperationalEfficiencyImpact
                    ));
                }

                return scenario;
            }).ToList();

            if (scenarios.Count == 0)
                return;

            await context.Scenarios.AddRangeAsync(scenarios);
            await context.SaveChangesAsync();
        }

        private static string GetSeedFilePath(IHostEnvironment environment)
        {
            var sourcePath = Path.Combine(
                environment.ContentRootPath,
                "..",
                "GrupoBlancoChallenge.Infraestructure",
                "Persistence",
                "Seed",
                "scenarios.json"
            );

            if (File.Exists(sourcePath))
                return sourcePath;

            return Path.Combine(
                environment.ContentRootPath,
                "Persistence",
                "Seed",
                "scenarios.json"
            );
        }

        private static void ValidateScenarioBank(List<ScenarioSeedDto> scenarioDtos)
        {
            for (var month = 1; month <= 12; month++)
            {
                if (!scenarioDtos.Any(x => x.Month == month))
                    throw new InvalidOperationException($"El archivo scenarios.json no tiene escenarios para el mes {month}.");
            }

            foreach (var scenario in scenarioDtos)
            {
                if (scenario.Month < 1 || scenario.Month > 12)
                    throw new InvalidOperationException($"El escenario '{scenario.Title}' tiene un mes inválido.");

                if (string.IsNullOrWhiteSpace(scenario.Quarter))
                    throw new InvalidOperationException($"El escenario '{scenario.Title}' no tiene trimestre configurado.");

                if (scenario.Options.Count != 3)
                    throw new InvalidOperationException($"El escenario '{scenario.Title}' debe tener exactamente 3 opciones.");
            }
        }

        private static string BuildScenarioKey(int month, string title)
        {
            return $"{month}|{title.Trim().ToUpperInvariant()}";
        }
    }
}
