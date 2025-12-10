using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Configuration.GetPriority;

public sealed record GetPriorityQuery() : IQuery<IEnumerable<EnumsResponseDto>>;
