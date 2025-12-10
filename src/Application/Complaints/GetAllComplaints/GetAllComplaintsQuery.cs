using Application.Abstractions.Messaging;
using Application.Complaints.Dto;

namespace Application.Complaints.GetAllComplaints;

public sealed record GetAllComplaintsQuery(
   int pageNumber = 1,
   int? PageSize = 2000,
   string? searchTerm = null,
   DateTime? DateFrom = null,
   DateTime? DateTo = null) : IQuery<PagedResponse<CrmComplaintDto>>;
