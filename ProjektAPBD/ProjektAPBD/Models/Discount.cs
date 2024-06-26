using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjektAPBD.Models;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
    public int Value { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public ICollection<SoftwareDiscount> AgreementDiscounts { get; set; }
}