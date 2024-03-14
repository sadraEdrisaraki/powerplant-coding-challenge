using EnGieCodingChallenge.Core.Enums;
using EnGieCodingChallenge.Core.Wrappers.v1;
using System.Text.Json;

namespace EngieCodingChallenge.WebApi.Deserializer
{
    internal static class PowerPlantDeserializer
    {
        public static PowerPlantRequest Deserialize(JsonElement data)
        {
            PowerPlantRequest powerPlantRequest = JsonSerializer.Deserialize<PowerPlantRequest>(data);
            JsonElement.ArrayEnumerator jsonArray = data.GetProperty("powerplants").EnumerateArray();
            for (int i = 0; i < jsonArray.Count(); i++)
            {
                powerPlantRequest.PowerPlants[i].Type = PowerPlantType.FromDisplayName<PowerPlantType>(jsonArray.ElementAt(i).GetProperty("type").GetString());
            }
            return powerPlantRequest;
        }
    }
}
