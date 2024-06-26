using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjektAPBD.Models;

public class Discount
{
    public int DiscountId { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
    [Precision(4, 2)]
    public decimal Value { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public ICollection<AgreementDiscount> AgreementDiscounts { get; set; }
}