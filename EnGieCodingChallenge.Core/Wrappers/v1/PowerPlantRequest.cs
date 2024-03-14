using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EnGieCodingChallenge.Core.Dtos.v1;

namespace EnGieCodingChallenge.Core.Wrappers.v1
{
    public record PowerPlantRequest
    {
        [JsonPropertyName("load")]
        public decimal Load { get; init; }
        [JsonPropertyName("fuels")]
        public FuelsParametersDTO Fuels { get; init; }
        [JsonPropertyName("powerplants")]
        public IList<PowerPlantDTO> PowerPlants { get; init; }

    }
}
