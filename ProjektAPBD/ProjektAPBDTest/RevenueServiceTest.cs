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
    public async Task GetCompanyRevenue_ShouldReturnCorrectValue()
    {
        // Arrange
        _agreementsRepositoryMock.Setup(x => x.GetCompanyRevenue()).ReturnsAsync(1000);

        // Act
        var result = await _revenueService.GetCompanyRevenue(estimatedRevenue: false);

        // Assert
        Assert.AreEqual(1000, result.Value);
    }

    [Test]
    public async Task GetCompanyRevenue_ShouldReturnEstimatedValue()
    {
        // Arrange
        _agreementsRepositoryMock.Setup(x => x.GetCompanyEstimatedRevenue()).ReturnsAsync(2000);

        // Act
        var result = await _revenueService.GetCompanyRevenue(estimatedRevenue: true);

        // Assert
        Assert.AreEqual(2000, result.Value);
    }

    [Test]
    public async Task GetProductRevenue_ShouldReturnCorrectValue()
    {
        // Arrange
        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetProductRevenue(productId)).ReturnsAsync(500);

        // Act
        var result = await _revenueService.GetProductRevenue(productId, estimatedRevenue: false);

        // Assert
        Assert.AreEqual(productId, result.ProductId);
        Assert.AreEqual(500, result.Value);
    }

    [Test]
    public async Task GetProductRevenue_ShouldThrowException_WhenSoftwareDoesNotExist()
    {
        // Arrange
        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(false);

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _revenueService.GetProductRevenue(productId, estimatedRevenue: false));
        Assert.AreEqual($"Software with id: {productId} does not exist", ex.Message);
    }

    [Test]
    public async Task GetProductRevenue_ShouldReturnEstimatedValue()
    {
        // Arrange
        int productId = 1;
        _softwareRepositoryMock.Setup(x => x.DoesSoftwareExistById(productId)).ReturnsAsync(true);
        _agreementsRepositoryMock.Setup(x => x.GetProductEstimatedRevenue(productId)).ReturnsAsync(800);

        // Act
        var result = await _revenueService.GetProductRevenue(productId, estimatedRevenue: true);

        // Assert
        Assert.AreEqual(productId, result.ProductId);
        Assert.AreEqual(800, result.Value);
    }
}