using ProjektAPBD.DTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class ClientService : IClientService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task AddIdividualClient(AddIndividualClientDTO addIndividualClientDto)
    {
        IndividualClient? individualClient = await _clientsRepository.GetIdividualClientByPesel(addIndividualClientDto.Pesel);
        DoesClientWithGivenPeselExist(addIndividualClientDto, individualClient);

        IndividualClient newIndividualClient = new IndividualClient()
        {
            Address = addIndividualClientDto.Address,
            Email = addIndividualClientDto.Email,
            PhoneNumber = addIndividualClientDto.PhoneNumber,
            FirstName = addIndividualClientDto.FirstName,
            SecondName = addIndividualClientDto.SecondName,
            Pesel = addIndividualClientDto.Pesel
        };

        await _clientsRepository.AddIdividualClient(newIndividualClient);
    }

    public async Task RemoveIdividualClient(int individualClientid)
    {
        IndividualClient? individualClient = await _clientsRepository.GetIdividualClientById(individualClientid);
        DoesClientWithGivenIdExist(individualClientid, individualClient);

        await _clientsRepository.RemoveIdividualClient(individualClient);
    }

    public async Task UpdateIdividualClient(int individualClientid, UpdateIndividualClientDTO updateIndividualClientDto)
    {
        IndividualClient? individualClient = await _clientsRepository.GetIdividualClientById(individualClientid);
        DoesClientWithGivenIdExist(individualClientid, individualClient);
        
        await _clientsRepository.UpdateIdividualClient(individualClient, updateIndividualClientDto);
    }
    
    public async Task AddCompanyClient(AddCompanyClientDTO addCompanyClientDto)
    {
        CompanyClient? companyClient = await _clientsRepository.GetCompanyClientByKRS(addCompanyClientDto.KRSNumber);
        DoesCompanyWithGivenKRSExist(addCompanyClientDto, companyClient);

        CompanyClient newCompanyClient = new CompanyClient()
        {
            Address = addCompanyClientDto.Address,
            Email = addCompanyClientDto.Email,
            PhoneNumber = addCompanyClientDto.PhoneNumber,
            CompanyName = addCompanyClientDto.CompanyName,
            KRSNumber = addCompanyClientDto.KRSNumber
        };

        await _clientsRepository.AddCompanyClient(newCompanyClient);
    }
    

    public async Task UpdateCompanyClient(int companyClientid, UpdateCompanyClientDTO updateCompanyClientDto)
    {
        CompanyClient? companyClient = await _clientsRepository.GetCompanyClientById(companyClientid);
        DoesCompanyWithGivenIdExist(companyClientid, companyClient);
        
        await _clientsRepository.UpdateCompanyClient(companyClient, updateCompanyClientDto);
    }

    // ---
    public async Task DoesClientWithGivenPeselExist(AddIndividualClientDTO addIndividualClientDto, IndividualClient? client)
    {
        if (client == null)
        {
            throw new Exception($"Client with given pesel: {addIndividualClientDto.Pesel} does not exist");
        }
    }
    
    private void DoesClientWithGivenIdExist(int individualClientid, IndividualClient? client)
    {
        if (client == null)
        {
            throw new Exception($"Client with given Id: {individualClientid} does not exist");
        }
    }
    private void DoesCompanyWithGivenKRSExist(AddCompanyClientDTO addCompanyClientDto, CompanyClient? companyClient)
    {
        if (companyClient == null)
        {
            throw new Exception($"Company with given KRS: {addCompanyClientDto.KRSNumber} does not exist");
        }
    }
    
    private void DoesCompanyWithGivenIdExist(int companyClientid, CompanyClient? updateCompanyClientDto)
    {
        if (updateCompanyClientDto == null)
        {
            throw new Exception($"Company with given Id: {companyClientid} does not exist");
        }
    }
    
}