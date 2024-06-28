using Moq;
using ProjektAPBD.Context;
using ProjektAPBD.DTOs.AgreementDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;
using ProjektAPBD.Services;

namespace TestProject2;

public class AgreementServiceTest
{
    private Mock<IAgreementsRepository> _agreementsRepositoryMock;
    private Mock<ISoftwareRepository> _softwareRepositoryMock;
    private Mock<IClientsRepository> _clientsRepositoryMock;
    private AgreementService _agreementService;

    [SetUp]
    public void Setup()
    {
        _agreementsRepositoryMock = new Mock<IAgreementsRepository>();
        _softwareRepositoryMock = new Mock<ISoftwareRepository>();
        _clientsRepositoryMock = new Mock<IClientsRepository>();
        _agreementService = new AgreementService(_agreementsRepositoryMock.Object, _softwareRepositoryMock.Object, _clientsRepositoryMock.Object);
    }

    [Test]
    public async Task MakeNewAgreement_ShouldThrowException_WhenSoftwareDoesNotExist()
    {
        
        var addAgreementDto = new AddAgreementDTO { SoftwareId = 1, IsCompanyClient = true, Clientid = 1 };
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(It.IsAny<int>())).ReturnsAsync(false);


        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.MakeNewAgreement(addAgreementDto));

    }

    [Test]
    public async Task MakeNewAgreement_ShouldThrowException_WhenClientDoesNotExist()
    {

        var addAgreementDto = new AddAgreementDTO { SoftwareId = 1, IsCompanyClient = true, Clientid = 1 };
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(It.IsAny<int>())).ReturnsAsync(true);
        _clientsRepositoryMock.Setup(x => x.DoesCompanyClientExist(It.IsAny<int>())).ReturnsAsync(false);

        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.MakeNewAgreement(addAgreementDto));

    }

    // [Test]
    // public async Task MakeNewAgreement_ShouldAddNewAgreement_WhenValidData()
    // {
    //     // Arrange
    //     var addAgreementDto = new AddAgreementDTO { SoftwareId = 1, IsCompanyClient = true, Clientid = 1, actualizationYears = 2, BeginDate = DateTime.Today, EndDate = DateTime.Today.AddYears(1) };
    //     var software = new Software() { SoftwareId = 1, Version = "1.0", Price = 1000 };
    //     var discounts = new List<Discount> { new Discount { Value = 10 } };
    //
    //     _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(It.IsAny<int>())).ReturnsAsync(true);
    //     _clientsRepositoryMock.Setup(x => x.DoesCompanyClientExist(It.IsAny<int>())).ReturnsAsync(true);
    //     _softwareRepositoryMock.Setup(x => x.GetAvailableDiscountsBySoftwareId(It.IsAny<int>())).ReturnsAsync(discounts);
    //     _softwareRepositoryMock.Setup(x => x.GetSoftwarePrice(It.IsAny<int>())).ReturnsAsync(software);
    //     _agreementsRepositoryMock.Setup(x => x.IsThereAlreadyAgreementOnSoftware(It.IsAny<Software>(), It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(false);
    //
    //     // Act
    //     await _agreementService.MakeNewAgreement(addAgreementDto);
    //
    //     // Assert
    //     _agreementsRepositoryMock.Verify(x => x.AddNewAgreement(It.IsAny<Agreement>()), Times.Once);
    // }

    [Test]
    public async Task PayForAgreement_ShouldThrowException_WhenAgreementDoesNotExist()
    {

        var agreementId = 1;
        var paymentValue = 500;
        _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(false);


        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.PayForAgreemnt(agreementId, paymentValue));
    }

    [Test]
    public async Task PayForAgreement_ShouldThrowException_WhenAgreementIsAlreadySigned()
    {

        var agreementId = 1;
        var paymentValue = 500;
        var agreement = new Agreement { AgreementId = agreementId, Signed = true };
        _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetAgreementById(It.IsAny<int>())).ReturnsAsync(agreement);


        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.PayForAgreemnt(agreementId, paymentValue));
    }

    // [Test]
    // public async Task PayForAgreement_ShouldThrowException_WhenPaymentIsAfterEndDate()
    // {
    //     // Arrange
    //     var agreementId = 1;
    //     var paymentValue = 500;
    //     var agreement = new Agreement { AgreementId = agreementId, Signed = false, EndDate = DateTime.Today.AddDays(-1) };
    //     _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(true);
    //     _agreementsRepositoryMock.Setup(x => x.GetAgreementById(It.IsAny<int>())).ReturnsAsync(agreement);
    //
    //     // Act & Assert
    //     var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _agreementService.PayForAgreemnt(agreementId, paymentValue));
    //     Assert.AreEqual($"Payment is after ending date of agreement : {agreement.EndDate}", ex.Message);
    // }
    //
    // [Test]
    // public async Task PayForAgreement_ShouldAddPayment_WhenValidData()
    // {
    //     // Arrange
    //     var agreementId = 1;
    //     var paymentValue = 500;
    //     var agreement = new Agreement { AgreementId = agreementId, Signed = false, EndDate = DateTime.Today.AddDays(1), Price = 1000, Payment = 0 };
    //     _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(true);
    //     _agreementsRepositoryMock.Setup(x => x.GetAgreementById(It.IsAny<int>())).ReturnsAsync(agreement);
    //
    //     // Act
    //     await _agreementService.PayForAgreemnt(agreementId, paymentValue);
    //
    //     // Assert
    //     _agreementsRepositoryMock.Verify(x => x.AddPayment(It.IsAny<Agreement>(), It.IsAny<decimal>()), Times.Once);
    // }
    
    
}