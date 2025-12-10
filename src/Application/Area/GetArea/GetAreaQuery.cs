using Application.Abstractions.Messaging;
using Application.Area.Dto;

namespace Application.Area.GetArea;

public sealed record GetAreaQuery() : IQuery<AreaResponseDto>;
