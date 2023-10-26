namespace Medication.Dto;

public class MedicationDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; } 
    public string? Created_date { get; set; }
}