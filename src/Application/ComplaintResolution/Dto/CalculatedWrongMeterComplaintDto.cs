namespace Application.ComplaintResolution.Dto;

public class CalculatedWrongMeterComplaintDto
{
    public double? RevisedAmount { get; set; } = 0;
    public double? AmountCalculated { get; set; } = 0;
    public double? NetAdjustment { get; set; } = 0;
}
