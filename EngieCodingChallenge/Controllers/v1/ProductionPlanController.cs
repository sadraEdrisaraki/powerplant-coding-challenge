namespace EngieCodingChallenge.WebApi.Controllers.v1
{
    using Asp.Versioning;
    using EngieCodingChallenge.Core.v1.UnitCommitCalculation;
    using EngieCodingChallenge.WebApi.Deserializer;
    using EnGieCodingChallenge.Core.Dtos.v1;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;
    using Serilog;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private IUnitCommitCalculation unitCommitCalculation;


        public ProductionPlanController(IUnitCommitCalculation unitCommitCalculation)
        {
            this.unitCommitCalculation = unitCommitCalculation;
        }

        [HttpPost]
        public ActionResult<IList<UnitCommitmentDTO>> CalculateUnitCommitment(JsonElement data)
        {
            try
            {
                return Ok(unitCommitCalculation.CalculateUnitCommitment(PowerPlantDeserializer.Deserialize(data)));
            }
            catch(Exception e)
            {
                Log.Error($"{e.Message} {e.StackTrace}");
                return Problem(e.Message);
            }
        }
    }
}
