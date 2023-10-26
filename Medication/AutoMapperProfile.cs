namespace Medication;

using AutoMapper;
using Medication.Dto;
using Medication.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Medication,MedicationDto>();
        CreateMap<MedicationDto,Medication>();
    }
}
