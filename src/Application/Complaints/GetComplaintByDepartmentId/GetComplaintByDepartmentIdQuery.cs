using Application.Abstractions.Messaging;
using Application.Complaints.Dto;

namespace Application.Complaints.GetComplaintByDepartmentId;

public sealed record GetComplaintByDepartmentIdQuery(
    string departmentId,
    int? pageNumber = 1,
    int? pageSize = 2000,
    DateTime? dateFrom = null,
    DateTime? dateTo = null) : IQuery<PagedResponse<CrmComplaintDto>>;
