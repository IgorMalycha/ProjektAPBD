using ProjektAPBD.DTOs;
using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public interface IClientsRepository
{
    Task<IndividualClient?> GetIdividualClientByPesel(string pesel);
    Task AddIdividualClient(IndividualClient individualClient);
    Task<IndividualClient?> GetIdividualClientById(int individualClientid);

    Task RemoveIdividualClient(IndividualClient individualClient);

    Task UpdateIdividualClient(IndividualClient individualClient, UpdateIndividualClientDTO updateIndividualClientDto);
    Task AddCompanyClient(CompanyClient newCompanyClient);
    Task UpdateCompanyClient(CompanyClient companyClientid, UpdateCompanyClientDTO updateCompanyClientDto);
    Task<CompanyClient?> GetCompanyClientById(int companyClientid);
    Task<CompanyClient?> GetCompanyClientByKRS(string krsNumber);
    Task<bool> IsReturningClient(int clientid, bool isCompanyClient);
}