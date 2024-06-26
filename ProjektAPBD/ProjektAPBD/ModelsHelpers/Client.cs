using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;



public class Client
{
    
    [MaxLength(100)]
    public string Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public int PhoneNumber { get; set; }
   
}