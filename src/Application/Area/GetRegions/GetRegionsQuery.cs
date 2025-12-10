using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Area.GetRegions;

public sealed record GetRegionsQuery() : IQuery<IEnumerable<EnumsResponseDto>>;
