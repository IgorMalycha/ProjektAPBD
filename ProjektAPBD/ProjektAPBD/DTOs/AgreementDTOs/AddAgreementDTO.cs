namespace ProjektAPBD.DTOs.AgreementDTOs;

public class AddAgreementDTO
{
    public bool IsCompanyClient { get; set; }
    public int Clientid { get; set; }
    public int SoftwareId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public int actualizationYears { get; set; }
    
    
}