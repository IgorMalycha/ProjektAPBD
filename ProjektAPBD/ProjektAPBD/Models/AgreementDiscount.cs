using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

[Table("Agreement_Discount")]
public class AgreementDiscount
{
    [Key]
    public int AgreementDiscountId { get; set; }
    public int DiscountId { get; set; }
    public int AgreementId { get; set; }
    
    [ForeignKey(nameof(DiscountId))]
    public Discount Discount { get; set; }
    [ForeignKey(nameof(AgreementId))]
    public Agreement Agreement { get; set; }
}