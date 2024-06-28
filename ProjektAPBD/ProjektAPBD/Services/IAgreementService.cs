using ProjektAPBD.DTOs.AgreementDTOs;

namespace ProjektAPBD.Services;

public interface IAgreementService
{

    Task MakeNewAgreement(AddAgreementDTO addAgreementDto);
    Task<OutputPaymentDTO> PayForAgreemnt(int agreemntId, decimal paymentValue);
}