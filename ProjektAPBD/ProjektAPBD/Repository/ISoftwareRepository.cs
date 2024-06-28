using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public interface ISoftwareRepository
{
    Task<List<Discount>> GetAvailableDiscountsBySoftwareId(int softwareId);
    Task<Software> GetSoftwarePrice(int softwareId);
    Task<bool> DoesSoftwareExistById(int softwareId);
}