namespace Medication.Tests.Controllers;

using System.Net;
using Medication.Controllers;
using Medication.Dto;
using Medication.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class MedicationControllerTests
{
    private Mock<IMedicationService> medicationService;
    private MedicationController controller;

    [SetUp]
    public void SetUp()
    {
        this.medicationService = new Mock<IMedicationService>();

        this.controller = new MedicationController(this.medicationService.Object);
    }

    [Test]
    public async Task MedicationController_GetAllMedication_ReturnsOk()
    {
        // Arrange
        var medication = new MedicationDto
        {
            Name = "test",
            Quantity = 2,
        };

        var medications = new List<MedicationDto> {medication};
        
        this.medicationService
            .Setup(m => m.GetAllMedication())
            .ReturnsAsync(medications);

        // Act
        var result = await this.controller.GetAllMedication();

        // Assert
        Assert.IsNotNull(result);
        Assert.That(((OkObjectResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
    }
    
    [Test]
    public async Task MedicationController_InsertMedication_ReturnsCreated()
    {
        // Arrange
        var medication = new MedicationDto
        {
            Name = "test",
            Quantity = 2,
        };
        
        this.medicationService
            .Setup(m => m.InsertMedication(medication))
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.controller.InsertMedication(medication);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(((CreatedResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
    }
    
    [TestCase(0)]
    [TestCase(-1)]
    public async Task MedicationController_InsertMedicationWithInvalidQuantity_ReturnsErrorMessage(int quantity)
    {
        // Arrange
        var medication = new MedicationDto
        {
            Name = "test",
            Quantity = quantity,
        };
        
        // Act
        var result = await this.controller.InsertMedication(medication);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(((BadRequestObjectResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public async Task MedicationController_InsertMedicationWithInvalidName_ReturnsErrorMessage(string name)
    {
        // Arrange
        var medication = new MedicationDto
        {
            Name = name,
            Quantity = 2,
        };
        
        // Act
        var result = await this.controller.InsertMedication(medication);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(((BadRequestObjectResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }
    
    [Test]
    public async Task MedicationController_DeleteMedication_ReturnsNoContent()
    {
        // Arrange
        this.medicationService
            .Setup(m => m.DeleteMedication(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.controller.DeleteMedication(Guid.NewGuid());

        // Assert
        Assert.IsNotNull(result);
        Assert.That(((NoContentResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.NoContent));
    }
}