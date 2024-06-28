using NUnit.Framework;
using Moq;
using ProjektAPBD.Repository;
using ProjektAPBD.Services;

public class RevenueServiceTest
{
    private Mock<IAgreementsRepository> _agreementsRepositoryMock;
    private Mock<ISoftwareRepository> _softwareRepositoryMock;
    private RevenueService _revenueService;

    [SetUp]
    public void Setup()
    {
        _agreementsRepositoryMock = new Mock<IAgreementsRepository>();
        _softwareRepositoryMock = new Mock<ISoftwareRepository>();
        _revenueService = new RevenueService(_agreementsRepositoryMock.Object, _softwareRepositoryMock.Object);
    }

    [Test]
    public async Task GetCompanyRevenueReturnCorrectValue()
    {
        _agreementsRepositoryMock.Setup(x => x.GetCompanyRevenue()).ReturnsAsync(1000);
        
        var result = await _revenueService.GetCompanyRevenue(estimatedRevenue: false);
        
        Assert.AreEqual(1000, result.Value);
    }

    [Test]
    public async Task GetCompanyRevenueReturnEstimatedValue()
    {

        _agreementsRepositoryMock.Setup(x => x.GetCompanyEstimatedRevenue()).ReturnsAsync(2000);
        
        var result = await _revenueService.GetCompanyRevenue(estimatedRevenue: true);
        
        Assert.AreEqual(2000, result.Value);
    }

    [Test]
    public async Task GetProductRevenueEeturnCorrectValue()
    {
        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetProductRevenue(productId)).ReturnsAsync(500);
        
        var result = await _revenueService.GetProductRevenue(productId, estimatedRevenue: false);


        Assert.AreEqual(productId, result.ProductId);
        Assert.AreEqual(500, result.Value);
    }

    [Test]
    public async Task GetProductRevenueThrowExceptionSoftwareDoesNotExist()
    {

        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(false);
        
        Assert.ThrowsAsync<ArgumentException>(async () => await _revenueService.GetProductRevenue(productId, estimatedRevenue: false));
    }

    [Test]
    public async Task GetProductRevenueReturnEstimatedValue()
    {

        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetProductEstimatedRevenue(productId)).ReturnsAsync(800);

        var result = await _revenueService.GetProductRevenue(productId, estimatedRevenue: true);
        
        Assert.AreEqual(productId, result.ProductId);
        Assert.AreEqual(800, result.Value);
    }
}