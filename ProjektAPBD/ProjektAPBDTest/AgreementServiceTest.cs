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
    public async Task MakeNewAgreementThrowExceptionSoftwareDoesNotExist()
    {
        
        var addAgreementDto = new AddAgreementDTO { SoftwareId = 1, IsCompanyClient = true, Clientid = 1 };
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(It.IsAny<int>())).ReturnsAsync(false);


        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.MakeNewAgreement(addAgreementDto));

    }

    [Test]
    public async Task MakeNewAgreementThrowExceptionClientDoesNotExist()
    {

        var addAgreementDto = new AddAgreementDTO { SoftwareId = 1, IsCompanyClient = true, Clientid = 1 };
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(It.IsAny<int>())).ReturnsAsync(true);
        _clientsRepositoryMock.Setup(x => x.DoesCompanyClientExist(It.IsAny<int>())).ReturnsAsync(false);

        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.MakeNewAgreement(addAgreementDto));

    }

   
    [Test]
    public async Task PayForAgreemenThrowExceptionAgreementDoesNotExist()
    {

        var agreementId = 1;
        var paymentValue = 500;
        _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(false);


        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.PayForAgreemnt(agreementId, paymentValue));
    }

    [Test]
    public async Task PayForAgreementThrowExceptionWhenAgreementIsAlreadySigned()
    {

        var agreementId = 1;
        var paymentValue = 500;
        var agreement = new Agreement { AgreementId = agreementId, Signed = true };
        _agreementsRepositoryMock.Setup(x => x.DoesAgreemntExistById(It.IsAny<int>())).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetAgreementById(It.IsAny<int>())).ReturnsAsync(agreement);
        

        Assert.ThrowsAsync<ArgumentException>(async () => await _agreementService.PayForAgreemnt(agreementId, paymentValue));
    }
    
}