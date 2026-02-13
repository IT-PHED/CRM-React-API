using Application.ComplaintResolution.Dto;
using Application.Complaints.Dto;
using Domain.Complaint;

namespace Application.Abstractions.Factory.Complaint;

public interface IComplaintService
{
    Task<string> GetTicket();
    Task<ComplaintType> GetComplaintTypeByIdAsync(string complaintTypeId);
    Task<T> GetComplaintById<T>(string complaintId);
    Task<bool> CanRegisterConsumerComplaint(string consumerId, string complaintTypeId, string complaintSubTypeId);
    Task<ComplaintTransactionResponse> InsertMasterComplaintTransactionAsync(InsertMasterComplaintDto data);
    Task<ComplaintTransactionResponse2> InsertMasterComplaintTransactionWithoutAccountNoAsync(InsertMasterComplaintDto2 data);
    Task<ConsumerComplaintDto> GetComplaintByTicketNumber(string ticketNumber);
    Task<IReadOnlyList<CrmComplaintDto>> GetAllComplaints(int pageNumber, int pageSize, string? searchTerm = null, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<IReadOnlyList<CrmComplaintDto>> GetAllDepartmentComplaint(string departmentId, int? pageNumber, int? pageSize, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<IReadOnlyList<CrmComplaintDto>> GetAllDepartmentComplaintAndStatus(string departmentId, string status, int? pageNumber, int? pageSize, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<IReadOnlyList<CrmComplaintDto>> QueryComplaintRegionAndStatus(string departmentId, string region, string status);
    Task<string> ReassignComplaint(string ticketId, string consumerId, string reassignStaffId, string? actorId = null);
    Task<IReadOnlyList<BillingInfoInWrongMeterReadingResolutionDto>> GetBillingInfoByMonthFromAndMonthTo(DateTime monthFrom, DateTime monthTo, string consumerId);
    Task<IReadOnlyList<CrmComplaintDto>> QueryComplaintByDepartment(string departmentId, string searchTerm, int pageNumber, int pageSize, DateTime? dateFrom = null, DateTime? dateTo = null);
}
