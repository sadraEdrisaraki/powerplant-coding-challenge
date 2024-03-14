using EnGieCodingChallenge.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnGieCodingChallenge.Core.Dtos.v1
{
    public record PowerPlantDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }
        
        public PowerPlantType Type { get; set; }
        [JsonPropertyName("efficiency")]
        public decimal Efficiency { get; init; }
        [JsonPropertyName("pmin")]
        public int Pmin { get; init; }
        [JsonPropertyName("pmax")]
        public int Pmax { get; init; }
    }
}
