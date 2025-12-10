using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Configuration.GetComplaintType;

public sealed record GetComplaintTypeQuery() : IQuery<IEnumerable<ComplaintTypeResponseDto>>;
