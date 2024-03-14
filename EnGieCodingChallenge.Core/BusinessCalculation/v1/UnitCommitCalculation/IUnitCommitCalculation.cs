using EnGieCodingChallenge.Core.Dtos.v1;
using EnGieCodingChallenge.Core.Wrappers.v1;

namespace EngieCodingChallenge.Core.v1.UnitCommitCalculation
{
    public interface IUnitCommitCalculation
    {
        IList<UnitCommitmentDTO> CalculateUnitCommitment(PowerPlantRequest powerPlantRequest);
    }
}
