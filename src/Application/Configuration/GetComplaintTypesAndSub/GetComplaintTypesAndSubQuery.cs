using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Configuration.GetComplaintTypesAndSub;

public sealed record GetComplaintTypesAndSubQuery() : IQuery<IEnumerable<ComplaintTypeResponseDto>>;
