using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public decimal ToBePaid { get; set; }
    public decimal AlreadyPaid { get; set; }
    public bool CountedAsRevenue { get; set; }
    public int AgreementId { get; set; }
    [ForeignKey(nameof(AgreementId))]
    public Agreement Agreement { get; set; }
}