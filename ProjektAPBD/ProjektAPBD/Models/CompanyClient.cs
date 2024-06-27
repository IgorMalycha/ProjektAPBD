using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;


public class CompanyClient
{
    [Key]
    public int CompanyId { get; set; }
    [MaxLength(100)]
    public string Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public int PhoneNumber { get; set; }
    [MaxLength(30)]
    public string CompanyName { get; set; }
    private string krsNumber;

    public ICollection<Agreement> Agreements { get; set; }  = new HashSet<Agreement>();
    
    public string KRSNumber
    {
        get { return krsNumber; }
        set
        {
            if (string.IsNullOrEmpty(krsNumber))
            {
                krsNumber = value;
            }
            else
            {
                throw new InvalidOperationException("KRS cannot be changed");
            }
        }
    }
}