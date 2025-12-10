using Application.Abstractions.Messaging;
using Application.Configuration.Dto;

namespace Application.Configuration.GetSources;

public sealed class GetSourcesQuery() : IQuery<IEnumerable<EnumsResponseDto>>;
