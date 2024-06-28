using Moq;
using ProjektAPBD.Context;
using ProjektAPBD.DTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;
using ProjektAPBD.Services;

namespace TestProject2;
using ProjektAPBD;
public class Tests
{

    private Mock<IClientsRepository> _clientsRepositoryMock;
    private ClientService _clientService;
    
    [SetUp]
    public void Setup()
    {
        _clientsRepositoryMock = new Mock<IClientsRepository>();
        _clientService = new ClientService(_clientsRepositoryMock.Object);
    }

    [Test]
    public void AddIdividualClientThrowExceptionClientExist()
    {
        
        var clientDto = new AddIndividualClientDTO
        {
            Address = "AdresIKlienta1",
            Email = "malpainna@wp.pl",
            PhoneNumber = 111456789,
            FirstName = "Jan",
            SecondName = "Kowalski",
            Pesel = "03333333333"
        };
        var existingClient = new IndividualClient() { Pesel = "03333333333" };

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientByPesel(clientDto.Pesel))
            .ReturnsAsync(existingClient);

        
        Assert.ThrowsAsync<Exception>(async () => await _clientService.AddIdividualClient(clientDto));
    }
    
    
    [Test]
    public async Task AddIndividualClientAddsClientClientWithPeselDoesNotExist()
    {

        var clientDto = new AddIndividualClientDTO
        {
            Address = "AdresIKlienta1",
            Email = "malpainna@wp.pl",
            PhoneNumber = 111456789,
            FirstName = "Jan",
            SecondName = "Kowalski",
            Pesel = "03333333333"
        };

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientByPesel(clientDto.Pesel))
            .ReturnsAsync((IndividualClient)null);


        await _clientService.AddIdividualClient(clientDto);


        _clientsRepositoryMock.Verify(repo => repo.AddIdividualClient(It.IsAny<IndividualClient>()), Times.Once);
    }

    [Test]
    public async Task RemoveIndividualClientThrowsExceptionClientDoesNotExist()
    {

        var clientId = 1;

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientById(clientId))
            .ReturnsAsync((IndividualClient)null);


        Assert.ThrowsAsync<Exception>(async () => await _clientService.RemoveIdividualClient(clientId));
        
    }

    [Test]
    public async Task RemoveIndividualClientRemovesClientClientExists()
    {

        var clientId = 1;
        var existingClient = new IndividualClient { IndividualPersonId = clientId };

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientById(clientId))
            .ReturnsAsync(existingClient);


        await _clientService.RemoveIdividualClient(clientId);


        _clientsRepositoryMock.Verify(repo => repo.RemoveIdividualClient(existingClient), Times.Once);
    }

    [Test]
    public async Task UpdateIndividualClientThrowsExceptionClientDoesNotExist()
    {
        var clientId = 1;
        var updateDto = new UpdateIndividualClientDTO
        {
            Address = "NowyAdres",
            Email = "nowyemail@wp.pl",
            PhoneNumber = 222456789,
            FirstName = "Janusz",
            SecondName = "Nowak"
        };

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientById(clientId))
            .ReturnsAsync((IndividualClient)null);

        Assert.ThrowsAsync<Exception>(async () => await _clientService.UpdateIdividualClient(clientId, updateDto));
    }

    [Test]
    public async Task UpdateIndividualClientUpdatesClientClientExists()
    {
        var clientId = 1;
        var updateDto = new UpdateIndividualClientDTO
        {
            Address = "NowyAdres",
            Email = "nowyemail@wp.pl",
            PhoneNumber = 222456789,
            FirstName = "Janusz",
            SecondName = "Nowak"
        };
        var existingClient = new IndividualClient { IndividualPersonId = clientId };

        _clientsRepositoryMock
            .Setup(repo => repo.GetIdividualClientById(clientId))
            .ReturnsAsync(existingClient);
        
        await _clientService.UpdateIdividualClient(clientId, updateDto);
        
        _clientsRepositoryMock.Verify(repo => repo.UpdateIdividualClient(existingClient, updateDto), Times.Once);
    }
}