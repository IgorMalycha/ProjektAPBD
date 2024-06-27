using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Context;
using ProjektAPBD.DTOs;
using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public class ClientsRepository : IClientsRepository
{
    private readonly DatabaseContext _databaseContext;

    public ClientsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<IndividualClient?> GetIdividualClientByPesel(string pesel)
    {
        return await _databaseContext.IndividualClients.FirstOrDefaultAsync(e => e.Pesel == pesel);
    }

    public async Task AddIdividualClient(IndividualClient individualClient)
    {
        await _databaseContext.IndividualClients.AddAsync(individualClient);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<IndividualClient?> GetIdividualClientById(int individualClientid)
    {
        return await _databaseContext.IndividualClients.FirstOrDefaultAsync(e => e.IndividualPersonId == individualClientid);
    }

    public async Task RemoveIdividualClient(IndividualClient individualClient)
    {
        individualClient.IsDeleted = true;
        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateIdividualClient(IndividualClient individualClient, UpdateIndividualClientDTO updateIndividualClientDto)
    {
        individualClient.FirstName = updateIndividualClientDto.FirstName;
        individualClient.SecondName = updateIndividualClientDto.SecondName;
        individualClient.Address = updateIndividualClientDto.Address;
        individualClient.Email = updateIndividualClientDto.Email;
        individualClient.PhoneNumber = updateIndividualClientDto.PhoneNumber;
        await _databaseContext.SaveChangesAsync();
    }

    public async Task AddCompanyClient(CompanyClient newCompanyClient)
    {
        await _databaseContext.CompanyClients.AddAsync(newCompanyClient);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateCompanyClient(CompanyClient companyClient, UpdateCompanyClientDTO updateCompanyClientDto)
    {
        companyClient.Address = updateCompanyClientDto.Address;
        companyClient.Email = updateCompanyClientDto.Email;
        companyClient.PhoneNumber = updateCompanyClientDto.PhoneNumber;
        companyClient.CompanyName = updateCompanyClientDto.CompanyName;
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<CompanyClient?> GetCompanyClientById(int companyClientid)
    {
        return await _databaseContext.CompanyClients.FirstOrDefaultAsync(e => e.CompanyId == companyClientid);
    }

    public async Task<CompanyClient?> GetCompanyClientByKRS(string krsNumber)
    {
        return await _databaseContext.CompanyClients.FirstOrDefaultAsync(e => e.KRSNumber == krsNumber);
    }

    public async Task<bool> IsReturningClient(int clientid, bool isCompanyClient)
    {
        if (isCompanyClient)
        {
            return await _databaseContext.Agreements.Where(e => e.Signed==true).AnyAsync(e => e.CompanyClient.CompanyId == clientid);
        }
        return await _databaseContext.Agreements.Where(e => e.Signed==true).AnyAsync(e => e.IndividualClient.IndividualPersonId == clientid);
    }
}