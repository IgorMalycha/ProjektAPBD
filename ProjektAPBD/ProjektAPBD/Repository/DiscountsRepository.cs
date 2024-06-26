using ProjektAPBD.Context;

namespace ProjektAPBD.Repository;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly DatabaseContext _databaseContext;

    public DiscountsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    
}