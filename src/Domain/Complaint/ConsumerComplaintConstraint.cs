using Domain.Common;

namespace Domain.Complaint;

public class ConsumerComplaintConstraint : AuditableBase
{
    public ConsumerComplaint Complaint { get; protected set; }
    public double CorrectMeterReading { get; protected set; }
    public DateTime MonthFrom { get; protected set; }
    public DateTime MonthTo { get; protected set; }
    public string FilePath { get; set; }

    internal ConsumerComplaintConstraint(ConsumerComplaint complaint,
                                             double correctMeterReading,
                                             DateTime monthFrom,
                                             DateTime monthTo,
                                             string filePath,
                                             string createdBy,
                                             DateTime createdDate,
                                             string? modifiedBy = null,
                                             DateTime? modifiedDate = null,
                                             string? id = null)
    {
        Complaint = complaint;
        CorrectMeterReading = correctMeterReading;
        MonthFrom = monthFrom;
        MonthTo = monthTo;
        FilePath = filePath;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        ModifiedBy = modifiedBy;
        ModifiedDate = modifiedDate;
    }
}
