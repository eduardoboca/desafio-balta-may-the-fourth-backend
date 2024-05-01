using MayTheFourth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.DTOs
{
    public class VehicleDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("vehicle_class")]
        public string VehicleClass { get; set; } = string.Empty;

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; } = string.Empty;

        [JsonPropertyName("length")]
        public string Length { get; set; } = string.Empty;

        [JsonPropertyName("cost_in_credits")]
        public string CostInCredits { get; set; } = string.Empty;

        [JsonPropertyName("crew")]
        public string Crew { get; set; } = string.Empty;

        [JsonPropertyName("passengers")]
        public string Passengers { get; set; } = string.Empty;

        [JsonPropertyName("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; } = string.Empty;

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

        public Vehicle ToVehicle()
        {
            var vehicle = new Vehicle
            {
                Name = Name,
                Slug = Name.ToLower().Replace(" ", "-"),
                Model = Model,
                VehicleClass = VehicleClass,
                Manufacturer = Manufacturer,
                Length = int.TryParse(Length!, out int parsedLength) ? parsedLength : 0,
                Crew = Crew,
                Passengers = int.TryParse(Passengers, out int parsedPassengers) ? parsedPassengers : 0,
                MaxAtmospheringSpeed = int.TryParse(MaxAtmospheringSpeed, out int parsedMaxAtmospheringSpeed) ? parsedMaxAtmospheringSpeed : 0,
                CargoCapacity = int.TryParse(CargoCapacity, out int parsedCargoCapacity) ? parsedCargoCapacity : 0,
                Consumables = Consumables,
                Url = Url,
                Created = Created,
                Edited = Edited
            };
            return vehicle;
        }
    }

}
