using EnGieCodingChallenge.Core.Dtos.v1;
using EnGieCodingChallenge.Core.Enums;
using EnGieCodingChallenge.Core.Helpers.DataStructure;
using EnGieCodingChallenge.Core.Wrappers.v1;

namespace EngieCodingChallenge.Core.v1.UnitCommitCalculation
{
    public class UnitCommitCalculation : IUnitCommitCalculation
    {

        private Queue<PowerPlantDTO> sortedPowerPlantsQueue = new Queue<PowerPlantDTO>();

        public IList<UnitCommitmentDTO> CalculateUnitCommitment(PowerPlantRequest powerPlantRequest)
        {
            sortedPowerPlantsQueue = new Queue<PowerPlantDTO>(powerPlantRequest.PowerPlants);
            SortPowerPlants(powerPlantRequest.Fuels, powerPlantRequest.Load);

            return ProcessLoadsAssignment(powerPlantRequest);
        }

        private List<UnitCommitmentDTO> ProcessLoadsAssignment(PowerPlantRequest powerPlantRequest)
        {
            decimal currentLoad = powerPlantRequest.Load;
            List<UnitCommitmentDTO> unitCommitments = new List<UnitCommitmentDTO>();


            for (int i = 0; i < powerPlantRequest.PowerPlants.Count; i++)
            {
                UnitCommitmentDTO? uc;
                uc = GetMostEfficientUC(powerPlantRequest, unitCommitments, currentLoad);
                if (uc.HasValue)
                {
                    UnitCommitmentDTO ucValue = uc.Value;
                    currentLoad -= ucValue.P;
                    unitCommitments.Add(ucValue);
                }
                else
                    break;
            }

            return unitCommitments;
        }

        private void SortPowerPlants(FuelsParametersDTO fuels, decimal load)
        {
            SortedList<decimal, PowerPlantDTO> powerplantsOrdering = new SortedList<decimal, PowerPlantDTO>(new DecimalComparer());
            SortedList<decimal, PowerPlantDTO> windTurbine = new SortedList<decimal, PowerPlantDTO>(new ReverseDecimalComparer());

            foreach (PowerPlantDTO powerPlant in sortedPowerPlantsQueue)
            {
                switch (powerPlant.Type)
                {
                    case var type when type.Equals(PowerPlantType.GasFired):
                        powerplantsOrdering.Add(Math.Max(powerPlant.Pmin, load) * (fuels.Gas / powerPlant.Efficiency), powerPlant);
                        break;
                    case var type when type.Equals(PowerPlantType.TurboJet):
                        powerplantsOrdering.Add(Math.Max(powerPlant.Pmin, load) * (fuels.Kerosine / powerPlant.Efficiency), powerPlant);
                        break;
                    case var type when type.Equals(PowerPlantType.WindTurbine):
                        if (fuels.Wind > 0 && fuels.Wind <= 100)
                            windTurbine.Add(Math.Min(powerPlant.Pmax, load) * ((fuels.Wind/100m) * powerPlant.Efficiency), powerPlant);
                        else if(fuels.Wind > 100)
                            windTurbine.Add(Math.Min(powerPlant.Pmax, load) * powerPlant.Efficiency, powerPlant);
                        break;
                }
            }

            sortedPowerPlantsQueue = new Queue<PowerPlantDTO>([.. windTurbine.Values, .. powerplantsOrdering.Values]);

        }

        private UnitCommitmentDTO? GetMostEfficientUC(PowerPlantRequest powerPlantRequest, List<UnitCommitmentDTO> unitCommitments, decimal currentLoad)
        {

            if (sortedPowerPlantsQueue.Count == 0)
                return null;
            if (sortedPowerPlantsQueue.First().Pmin > currentLoad)
                SortPowerPlants(powerPlantRequest.Fuels, currentLoad);

            PowerPlantDTO powerPlantDTO = sortedPowerPlantsQueue.Dequeue();
            if(powerPlantDTO.Type.Equals(PowerPlantType.WindTurbine))
                return new UnitCommitmentDTO() { Name = powerPlantDTO.Name, P = Math.Min(Math.Min(powerPlantDTO.Pmax, currentLoad) * ((powerPlantRequest.Fuels.Wind / 100m) * powerPlantDTO.Efficiency), currentLoad) };
            return new UnitCommitmentDTO() { Name = powerPlantDTO.Name, P = Math.Min(powerPlantDTO.Pmax, currentLoad) };
        }
    }
}
