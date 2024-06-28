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

    public DbSet<User> Users { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<SoftwareCategory> SoftwareCategories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<SoftwareDiscount> SoftwareDiscounts { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CompanyClient>().HasData(new List<CompanyClient>
        {
            new CompanyClient() {
                CompanyId = 1,
                Address = "AdresCKlienta1",
                Email = "malpa@wp.pl",
                PhoneNumber = 123456789,
                CompanyName = "KowalskispZoo",
                KRSNumber = "12345678"
            }
        });

        modelBuilder.Entity<IndividualClient>().HasData(new List<IndividualClient>
        {
            new IndividualClient() {
                IndividualPersonId = 1,
                Address = "AdresIKlienta1",
                Email = "malpainna@wp.pl",
                PhoneNumber = 111456789,
                FirstName = "Jan",
                SecondName = "Kowalski",
                IsDeleted = false,
                Pesel = "03333333333"
            }
        });

        modelBuilder.Entity<SoftwareCategory>().HasData(new List<SoftwareCategory>
        {
            new SoftwareCategory()
            {
                SoftwareCategoryId = 1,
                Type = "finanse"
            }
        });

        modelBuilder.Entity<Discount>().HasData(new List<Discount>
        {
            new Discount()
            {
                DiscountId = 1,
                Name = "Whitethursday",
                Value = 5,
                DateFrom = new DateTime(2024, 6, 26, 14, 30, 0),
                DateTo = new DateTime(2023, 7, 27, 14, 30, 0)
            }
        });

        modelBuilder.Entity<Software>().HasData(new List<Software>
        {
            new Software()
            {
               SoftwareId = 1,
               Name = "opraogramowanieChad",
               Description = "description",
               Version = "3.14",
               Price = 1000,
               SoftwareCategoryId = 1,
               IsSubscription = false,
               IsBoughtInOneTransaction = true
            }
        });
        
        modelBuilder.Entity<SoftwareDiscount>().HasData(new List<SoftwareDiscount>
        {
            new SoftwareDiscount()
            {
                SoftwareDiscountId = 1,
                DiscountId = 1,
                SoftwareId = 1
            }
        });
        
        
    }
    
}
    
    
