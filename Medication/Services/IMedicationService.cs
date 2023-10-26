namespace Medication.Services;

using Medication.Dto;

public interface IMedicationService
{
    Task<List<MedicationDto>> GetAllMedication();
    Task InsertMedication(MedicationDto medication);
    Task DeleteMedication(Guid id);
}