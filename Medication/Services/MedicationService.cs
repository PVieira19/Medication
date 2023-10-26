namespace Medication.Services;

using AutoMapper;
using Medication.Data;
using Medication.Dto;
using Medication.Models;
using Microsoft.EntityFrameworkCore;

public class MedicationService : IMedicationService
{
    private readonly IMapper mapper;
    private readonly DataContext context;

    public MedicationService(IMapper mapper, DataContext context)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<MedicationDto>> GetAllMedication()
    {
        try
        {
            var medications = await context.Medication.ToListAsync();
            var medicationsDto = medications.Select(c => mapper.Map<MedicationDto>(c));
        
            return medicationsDto.ToList();
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException("Failed To get");
        }
    }

    public async Task InsertMedication(MedicationDto medication)
    {
        try
        {
            medication.Created_date = DateTime.Now.ToString();
            var medicationModel = mapper.Map<Medication>(medication);
            medicationModel.Id = Guid.NewGuid();
            await context.Medication.AddAsync(medicationModel);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException($"Failed To Insert"); 
        }
    }

    public async Task DeleteMedication(Guid id)
    {
        try
        {
            var medication = await context.Medication.FirstAsync(x => x.Id == id);
            context.Medication.Remove(medication);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException($"Failed To Delete"); 
        }
    }
}