using MayTheFourth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.DTOs
{
    public class StarshipDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("starship_class")]
        public string StarshipClass { get; set; } = string.Empty;

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; } = string.Empty;

        [JsonPropertyName("cost_in_credits")]
        public string CostInCredits { get; set; } = string.Empty;

        [JsonPropertyName("length")]
        public string Length { get; set; } = string.Empty;

        [JsonPropertyName("crew")]
        public string Crew { get; set; } = string.Empty;

        [JsonPropertyName("passengers")]
        public string Passengers { get; set; } = string.Empty;

        [JsonPropertyName("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; } = string.Empty;

        [JsonPropertyName("hyperdrive_rating")]
        public string HyperdriveRating { get; set; } = string.Empty;

        [JsonPropertyName("MGLT")]
        public string MGLT { get; set; } = string.Empty;

        [JsonPropertyName("cargo_capacity")]
        public string CargoCapacity { get; set; } = string.Empty;

        [JsonPropertyName("consumables")]
        public string Consumables { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [JsonPropertyName("films")]
        public List<string> Films { get; set; } = [];

        [JsonPropertyName("pilots")]
        public List<string> Pilots { get; set; } = [];

        public Starship ToStarship()
        {
            var starship = new Starship
            {
                Name = Name,
                Slug = Name.ToLower().Replace(" ", "-"),
                Model = Model,
                StarshipClass = StarshipClass,
                Manufacturer = Manufacturer,
                CostInCredits = int.TryParse(CostInCredits!, out int parsedCostInCredits) ? parsedCostInCredits : 0,
                Length = double.TryParse(Length!, out double parsedLength) ? parsedLength : 0,
                Crew = int.TryParse(Crew!, out int parsedCrew) ? parsedCrew : 0,
                Passengers = int.TryParse(Passengers, out int parsedPassengers) ? parsedPassengers : 0,
                MaxAtmospheringSpeed = int.TryParse(MaxAtmospheringSpeed, out int parsedMaxAtmospheringSpeed) ? parsedMaxAtmospheringSpeed : 0,
                HyperdriveRating = HyperdriveRating,
                MGLT = MGLT,
                CargoCapacity = int.TryParse(CargoCapacity, out int parsedCargoCapacity) ? parsedCargoCapacity : 0,
                Consumables = Consumables,
                Url = Url,
                Created = Created,
                Edited = Edited
            };
            return starship;
        }
    }
}
