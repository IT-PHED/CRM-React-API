using Application.Abstractions.Messaging;
using Application.Complaints.Dto;

namespace Application.Complaints.QueryDepartmentById;

public sealed record QueryDepartmentByIdQuery(
    string departmentId,
    string searchTerm,
    int? pageNumber = 1,
    int? pageSize = 2000,
    DateTime? dateFrom = null,
    DateTime? dateTo = null) : IQuery<PagedResponse<CrmComplaintDto>>;
