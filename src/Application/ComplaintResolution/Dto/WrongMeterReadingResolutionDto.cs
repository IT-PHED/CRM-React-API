namespace Application.ComplaintResolution.Dto;

public class WrongMeterReadingResolutionDto
{
    public List<BillingInfoInWrongMeterReadingResolutionDto> BillingInfo { get; set; }
    public TariffInWrongMeterReadingResolutionDto? Tariff { get; set; }
}

public class BillingInfoInWrongMeterReadingResolutionDto
{
    private double slabec1;
    public double SLABEC1
    {
        get => slabec1;
        set
        {
            slabec1 = value;
            CurrentAmount = Math.Round(value + ED, 2);
        }
    }

    private double ed;
    public double ED
    {
        get => ed;
        set
        {
            ed = value;
            CurrentAmount = Math.Round(value + SLABEC1, 2);
        }
    }

    public string CONSUMERNO { get; set; }
    public string BILLBASIS { get; set; }
    public DateTime BILLDATE { get; set; }
    public DateTime BILLMONTH { get; set; }
    public double SLABEC2 { get; set; }
    public double ARR_EC_DF { get; set; }
    public double ARR_ED_DF { get; set; }
    public double UNITCONSUMED { get; set; }
    public int CURRENTMETERREADING { get; set; }
    public int PREVIOUSMETERREADING { get; set; }
    public string TARIFFCODE { get; set; }
    public double CurrentAmount { get; private set; }
    public string TotalAmount { get; set; }
}

public class TariffInWrongMeterReadingResolutionDto
{
    public string TARIFF_CODE { get; set; }
    public double EC_SLAB_1 { get; set; }
    public double EC_SLAB_2 { get; set; }
    public double EC_URBAN_RATE_1 { get; set; }
    public double EC_URBAN_RATE_2 { get; set; }
    public double VAT { get; set; }

    public double TariffRate => EC_SLAB_2 > 50 ? EC_URBAN_RATE_2 : EC_URBAN_RATE_1;
}
