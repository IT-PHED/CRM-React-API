using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Configuration.GetComplaintSubType;

public sealed record GetComplaintSubTypeQuery() : IQuery<IEnumerable<ComplaintSubTypeResponseDto>>;
