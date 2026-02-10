using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.ComplaintResolution;
using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;
using Application.Complaints.GetComplaintById;
using SharedKernel;

namespace Application.ComplaintResolution.ResolveHighEstimatedReadingComplaint;

internal sealed class ResolveHighEstimatedReadingComplaintCommandHandler(
    IComplaintService complaintService,
    IComplaintResolutionService complaintResolutionService) : ICommandHandler<ResolveHighEstimatedReadingComplaintCommand, CalculatedHighEstimateComplaintDto>
{
    public async Task<Result<CalculatedHighEstimateComplaintDto>> Handle(ResolveHighEstimatedReadingComplaintCommand command, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<BillingInfoInWrongMeterReadingResolutionDto> billInfos = await complaintService.GetBillingInfoByMonthFromAndMonthTo(command.MonthFrom, command.MonthTo, command.consumerId);
            if (billInfos is null || !billInfos.Any())
            {
                return Result.Failure<CalculatedHighEstimateComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("Month range not valid!"));
            }

            TariffInWrongMeterReadingResolutionDto tariff = await complaintResolutionService.GetConsumerTariff(command.consumerId);

            double unitCalculated = command.EstimatedReading * billInfos.Count;
            double amountCalculated = complaintResolutionService.CalculateAmount(unitCalculated, tariff);
            double revisedAmount = billInfos.Sum(x => x.CurrentAmount);
            double netAdjustment = revisedAmount - amountCalculated;

            BillingInfoInWrongMeterReadingResolutionDto? firstBillingInfo = billInfos.FirstOrDefault(x => x.BILLMONTH == command.MonthFrom);
            int previousReading = firstBillingInfo?.PREVIOUSMETERREADING ?? 0;
            double currentReading = previousReading + unitCalculated;

            var result = new CalculatedHighEstimateComplaintDto
            {
                RevisedAmount = Math.Round(revisedAmount, 2),
                AmountCalculated = Math.Round(amountCalculated, 2),
                NetAdjustment = Math.Round(netAdjustment, 2),
                CurrentReading = Math.Round(currentReading, 2)
            };

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<CalculatedHighEstimateComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("failed to resolve complaint"));
        }
    }
}
