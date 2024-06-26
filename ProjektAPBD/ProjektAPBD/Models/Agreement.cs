using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

public class Agreement
{
    public DateTime BeginDate { get; set; }
    private DateTime endDate;
    public string Actualization { get; set; }
    public bool Singed { get; set; }
    
    public int SoftwareId { get; set; }
    public int? CompanyClientId { get; set; }
    public int? IndividualClientId { get; set; }
    
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    [ForeignKey(nameof(CompanyClientId))]
    public CompanyClient? CompanyClient { get; set; }
    [ForeignKey(nameof(IndividualClientId))]
    public IndividualClient? IndividualClient { get; set; }

    public ICollection<AgreementDiscount> AgreementDiscounts { get; set; } = new HashSet<AgreementDiscount>();
    
    
    public DateTime EndDate
    {
        get => endDate;
        set
        {
            TimeSpan difference = value - BeginDate;
            if (difference.Days >= 3 && difference.Days <= 30)
            {
                endDate = value;
            }
            else
            {
                throw new InvalidOperationException("Diffrence between begin date and end date must be between 3 and 30 days.");
            }
        }
        
    }
}