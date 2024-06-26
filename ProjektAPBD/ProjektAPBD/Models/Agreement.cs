using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;

public class Agreement
{
    [Key]
    public int AgreementId { get; set; }
    public DateTime BeginDate { get; set; }
    private DateTime endDate;
    private int actulizationYears;
    public bool Singed { get; set; }
    public bool IsPaid { get; set; }
    public decimal Price { get; set; }
    public string SoftwareVersion { get; set; }
    
    public int SoftwareId { get; set; }
    public int PaymentId { get; set; }
    
    public int? CompanyClientId { get; set; }
    public int? IndividualClientId { get; set; }
    
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; }
    [ForeignKey(nameof(CompanyClientId))]
    public CompanyClient? CompanyClient { get; set; }
    [ForeignKey(nameof(IndividualClientId))]
    public IndividualClient? IndividualClient { get; set; }
    [ForeignKey(nameof(PaymentId))]
    public Payment Payment { get; set; }
    
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
    public int ActualizationYears
    {
        get => actulizationYears;
        set
        {
            if (value != 1 && value != 2 && value != 3)
            {
                throw new ArgumentException($"Years of actualization are: {value} and should be 1, 2 or 3.");
            }
            else
            {
                actulizationYears = value;
            }
        }
    }
}