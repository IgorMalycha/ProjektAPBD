using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

[Table("Software_Discount")]
public class SoftwareDiscount
{
    [Key]
    public int SoftwareDiscountId { get; set; }
    public int DiscountId { get; set; }
    public int SoftwareId { get; set; }
    
    [ForeignKey(nameof(DiscountId))]
    public Discount Discount { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
}