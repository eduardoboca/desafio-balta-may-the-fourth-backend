using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.DTOs
{
    public class SwapiDTO
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("next")]
        public string Next { get; set; } = string.Empty;
        [JsonPropertyName("previous")]
        public string Previous { get; set; } = string.Empty ;
        [JsonPropertyName("results")]
        public List<JsonElement>? Results { get; set; }
    }
}
