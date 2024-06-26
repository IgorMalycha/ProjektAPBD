using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Models;

namespace ProjektAPBD.Context;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<SoftwareCategory> SoftwareCategories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<SoftwareDiscount> SoftwareDiscounts { get; set; }
    
    
    
    
}