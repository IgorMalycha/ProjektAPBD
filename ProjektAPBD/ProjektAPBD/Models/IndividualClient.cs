using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

public class IndividualClient
{
    [Key] 
    public int IndividualPersonId { get; set; }
    [MaxLength(100)]
    public string Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public int PhoneNumber { get; set; }
    [MaxLength(30)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string SecondName { get; set; }
    public bool IsDeleted { get; set; }
    [MaxLength(11)] 
    private string pesel;
    
    public ICollection<Agreement> Agreements { get; set; }  = new HashSet<Agreement>();

    public string Pesel
    {
        get => pesel;
        set
        {
            if (string.IsNullOrEmpty(pesel))
            {
                pesel = value;
            }
            else
            {
                throw new InvalidOperationException("KRS cannot be changed");
            }
        }
        
    }
}