using MayTheFourth.Core.Entities;
using MayTheFourth.Core.Interfaces.Repositories;
using MayTheFourth.DataImporter.DTOs;
using MayTheFourth.DataImporter.Services.Contexts.SharedContext;
using MayTheFourth.Infra.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace MayTheFourth.DataImporter.Services.Contexts.SpeciesContext
{
    public class SpeciesImportService : IImportService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IPlanetRepository _planetRepository;
        private List<SpeciesDTO> _species = new();
        public SpeciesImportService(ISpeciesRepository speciesRepository, IPlanetRepository planetRepository)
        {
            _speciesRepository = speciesRepository;
            _planetRepository = planetRepository;
        }
        public void Add(SpeciesDTO species) => _species.Add(species);

        public void LoadList(string jsonList)
            => _species = JsonSerializer.Deserialize<List<SpeciesDTO>>(jsonList)!;

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            // Cadastrar 
            foreach (var speciesDTO in _species)
            {
                var species = speciesDTO.ToSpecies();
                if (!speciesDTO.Homeworld.IsNullOrEmpty())
                {
                    // Encontrar homeworld (planet) pela url
                    var planet = await _planetRepository.GetByUrlAsync(speciesDTO.Homeworld, cancellationToken);
                    species.HomeworldId = planet!.Id;
                }
                await _speciesRepository.SaveAsync(species, cancellationToken);
            }
        }

        public async Task<bool> IsEmpty() => !await _speciesRepository.AnyAsync();

    }
}
