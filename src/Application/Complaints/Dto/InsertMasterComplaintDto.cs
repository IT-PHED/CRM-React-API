namespace Application.Complaints.Dto;

public sealed record InsertMasterComplaintDto(
        string ComplaintId,
        string ComplaintTypeId,
        string ComplaintSubTypeId,
        string ConsumerId,
        string Status,
        string Source,
        string Ticket,
        DateTime DateGenerated,
        DateTime? DateResolved,
        string Priority,
        string Remark,
        string Dtr,
        string ConsName,
        string ConsMeterNo,
        string ConsTelephoneNo,
        string ConMaxDemand,
        string ConsCategory,
        string ConsAddr1,
        string ConsAddr2,
        string ConsAddr3,
        string ConsDivisionCode, // IBC
        string ConsSectionCode,  // BSC
        string ConRouteNumber,
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

        string ConstraintId,
        decimal CorrectMeterReading,
        DateTime MonthFrom,
        DateTime MonthTo,
        string? FilePath = null,

        string? OminiName = null,
        string? OminiPhone = null,
        string? OminiEmail = null,
        string? DepartmentId = null,
        string? AssignTo = null,
        string? MediaLink = null,

        // Control Flags
        bool InsertConstraint = true,
        bool AutoAllocate = true,
        bool PostToOmini = false
    );

public record ComplaintTransactionResponse(int StatusCode, string StatusMessage);
