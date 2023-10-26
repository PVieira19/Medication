namespace Medication.Models;

public class Medication
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Created_date { get; set; }
}