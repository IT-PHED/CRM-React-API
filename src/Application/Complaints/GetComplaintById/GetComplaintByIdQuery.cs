using Application.Abstractions.Messaging;

namespace Application.Complaints.GetComplaintById;

public sealed record GetComplaintByIdQuery(Guid Id) : IQuery<GetComplaintByIdQueryResponse>;

