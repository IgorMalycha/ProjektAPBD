using ProjektAPBD.DTOs;

namespace ProjektAPBD.Services;

public interface IClientService
{
    Task AddIdividualClient(AddIndividualClientDTO addIndividualClientDto);
    Task RemoveIdividualClient(int individualClientid);
    Task UpdateIdividualClient(int individualClientid, UpdateIndividualClientDTO updateIndividualClientDto);
    Task UpdateCompanyClient(int companyClientid, UpdateCompanyClientDTO updateCompanyClientDto);
    Task AddCompanyClient(AddCompanyClientDTO addCompanyClientDto);

}