using Application.ComplaintResolution.Dto;

namespace Application.Abstractions.Factory.ComplaintResolution;

public interface IComplaintResolutionService
{
    Task UpdateComplaintV2(UpdateComplaintDto payload);
    Task<WrongMeterReadingResolutionDto> GetBillingInfoByConsumerIdAndCorrectMeterReadingGreaterLessThanCurrentMeterReading(string complaintId, double correctMeterReading);
    double CalculateAmount(double unitConsumed, TariffInWrongMeterReadingResolutionDto tariff);
    Task<TariffInWrongMeterReadingResolutionDto> GetConsumerTariff(string consumerId);
    Task CloseConsumerComplaint(string id, string closedBy, string status, string closedByRemark);
}
