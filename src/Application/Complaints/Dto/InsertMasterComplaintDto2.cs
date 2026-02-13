namespace Application.Complaints.Dto;

public sealed record InsertMasterComplaintDto2(
        string ComplaintId,
        string ComplaintTypeId,
        string ComplaintSubTypeId,
        string Status,
        string Source,
        string Ticket,
        DateTime DateGenerated,
        DateTime? DateResolved,
        string Priority,
        string Remark,
        string ConsName,
        string ConsTelephoneNo,
        string ConMaxDemand,
        string ConsCategory,
        string ConsAddr1,
        string ConEmailId,
        string ConMobileNo,
        string ConsType,
        string Purpose,
        string MeterMake,
        int MeterDigit,
        string CreatedBy,
        DateTime CreatedDate,
        string? ModifiedBy,
        DateTime? ModifiedDate,
        string DepartmentId,
        string ConstraintId,
        decimal CorrectMeterReading,
        DateTime MonthFrom,
        DateTime MonthTo,
        string RegionId,
        string? FilePath = null,

        string? OminiName = null,
        string? OminiPhone = null,
        string? OminiEmail = null,
        string? AssignTo = null,
        string? MediaLink = null,

        // Control Flags
        bool InsertConstraint = true,
        bool AutoAllocate = true,
        bool PostToOmini = false
      
    );

public record ComplaintTransactionResponse2(int StatusCode, string StatusMessage);
