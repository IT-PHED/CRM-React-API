using Application.Abstractions.Factory.ComplaintResolution;
using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;
using SharedKernel;

namespace Application.ComplaintResolution.ResolveWrongMeterReadingComplaint;

internal sealed class ResolveWrongMeterReadingComplaintCommandHandler(IComplaintResolutionService complaintResolutionService) : ICommandHandler<ResolveWrongMeterReadingComplaintCommand, CalculatedWrongMeterComplaintDto>
{
    public async Task<Result<CalculatedWrongMeterComplaintDto>> Handle(ResolveWrongMeterReadingComplaintCommand command, CancellationToken cancellationToken)
    {
        string complaintId = command.ComplaintId.ToString();

        try
        {
            WrongMeterReadingResolutionDto wrongMeterReadingResolution = await complaintResolutionService.GetBillingInfoByConsumerIdAndCorrectMeterReadingGreaterLessThanCurrentMeterReading(complaintId, command.CorrectMeterReading);

            if (wrongMeterReadingResolution is null)
            {
                return Result.Failure<CalculatedWrongMeterComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch meter reading info!"));
            }

            List<BillingInfoInWrongMeterReadingResolutionDto> billingInfos = wrongMeterReadingResolution.BillingInfo;
            var orderedBillingInfosByBillingMonth = billingInfos.OrderBy(x => x.BILLMONTH).ToList();

            BillingInfoInWrongMeterReadingResolutionDto? firstBillingInfo = billingInfos.FirstOrDefault(x => x.BILLMONTH.Month == command.BillingMonth && x.BILLMONTH.Year == command.BillingYear);

            if (firstBillingInfo is null)
            {
                return Result.Failure<CalculatedWrongMeterComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("Wrong first billing month!"));
            }

            BillingInfoInWrongMeterReadingResolutionDto? billingInfoWithCorrectMeterReading = orderedBillingInfosByBillingMonth.FirstOrDefault(x => x.BILLMONTH.Month == command.BillingMonth &&
                                                                                                   x.BILLMONTH.Year == command.BillingYear &&
                                                                                                   x.CURRENTMETERREADING >= command.CorrectMeterReading &&
                                                                                                   x.PREVIOUSMETERREADING <= command.CorrectMeterReading);
            if (billingInfoWithCorrectMeterReading is null)
            {
                return Result.Failure<CalculatedWrongMeterComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("Wrong first billing month!"));
            }

            int index = orderedBillingInfosByBillingMonth.IndexOf(billingInfoWithCorrectMeterReading);
            IEnumerable<BillingInfoInWrongMeterReadingResolutionDto> calculationBillingInfo = orderedBillingInfosByBillingMonth.Skip(index);
            int unitCalculated = Math.Abs(command.CorrectMeterReading - billingInfoWithCorrectMeterReading.PREVIOUSMETERREADING);
            double amountCalculated = 0.0;
            if (wrongMeterReadingResolution.Tariff is not null)
            {
                amountCalculated = complaintResolutionService.CalculateAmount(unitCalculated, wrongMeterReadingResolution.Tariff);
            }

            double revisedAmount = calculationBillingInfo.Sum(x => x.CurrentAmount);
            double netAdjustment = revisedAmount - amountCalculated;

            var result = new CalculatedWrongMeterComplaintDto
            {
                RevisedAmount = Math.Round(revisedAmount, 2),
                AmountCalculated = Math.Round(amountCalculated, 2),
                NetAdjustment = Math.Round(netAdjustment, 2)
            };

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<CalculatedWrongMeterComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("Failed to fetch meter reading!"));
        }

    }
}
