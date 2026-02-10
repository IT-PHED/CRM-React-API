namespace Application.ComplaintResolution.Dto;

public class CalculatedHighEstimateComplaintDto
{
    public double RevisedAmount { get; set; }
    public double AmountCalculated { get; set; }
    public double NetAdjustment { get; set; }
    public double CurrentReading { get; set; }
}
