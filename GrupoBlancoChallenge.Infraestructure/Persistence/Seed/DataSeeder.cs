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
            if (await context.Scenarios.AnyAsync())
                return;

            var filePath = Path.Combine(
                environment.ContentRootPath,
                "..",
                "GrupoBlancoChallenge.Infrastructure",
                "Persistence",
                "Seed",
                "scenarios.json"
            );

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

            var scenarios = scenarioDtos.Select(dto =>
            {
                var scenario = new Scenario(
                    dto.Order,
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

            await context.Scenarios.AddRangeAsync(scenarios);
            await context.SaveChangesAsync();
        }
    }
}
