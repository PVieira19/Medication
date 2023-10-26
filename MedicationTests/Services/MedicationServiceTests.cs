namespace MedicationTests.Services;

using AutoMapper;
using Medication.Data;
using Medication.Services;
using Moq;

[TestFixture]
public class MedicationServiceTests
{
    private Mock<IMapper> mapper;
    private Mock<DataContext> context;
    private MedicationService medicationService;
    
    [SetUp]
    public void SetUp()
    {
        this.mapper = new Mock<IMapper>();
        this.context = new Mock<DataContext>();

        this.medicationService = new MedicationService(this.mapper.Object,this.context.Object );
    }
}