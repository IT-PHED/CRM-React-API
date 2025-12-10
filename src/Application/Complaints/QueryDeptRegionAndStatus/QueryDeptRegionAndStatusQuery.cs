using Application.Abstractions.Messaging;
using Application.Complaints.Dto;

namespace Application.Complaints.QueryDeptRegionAndStatus;

public sealed record QueryDeptRegionAndStatusQuery(
    string departmentId,
    string status,
    string? region = "-1") :
    IQuery<PagedResponse<CrmComplaintDto>>;
