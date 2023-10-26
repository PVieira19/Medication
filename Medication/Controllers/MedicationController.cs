using Microsoft.AspNetCore.Mvc;
namespace Medication.Controllers;

using Medication.Dto;
using Medication.Services;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class MedicationController : ControllerBase
{
    private readonly IMedicationService medicationService;
    
    public MedicationController(IMedicationService medicationService)
    {
        this.medicationService = medicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMedication()
    {
        return this.Ok(await medicationService.GetAllMedication());
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> InsertMedication(MedicationDto medication)
    {
        if (String.IsNullOrWhiteSpace(medication.Name))
        {
            return BadRequest("Name should be null or empty");
        }

        if (medication.Quantity < 1)
        {
            return BadRequest("Quantity should be more than 0");
        }

        await medicationService.InsertMedication(medication);
        
        return Created("", null);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedication(Guid id)
    {
        await medicationService.DeleteMedication(id);

        return NoContent();
    }
}