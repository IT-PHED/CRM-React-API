using Application.Configuration.Dto;

namespace Application.Abstractions.Factory.Configuration;

public interface IConfigurationService
{
    Task<IReadOnlyList<ComplaintTypeResponseDto>> GetComplaintTypesAsync();
    Task<IReadOnlyList<ComplaintSubTypeResponseDto>> GetComplaintSubTypesAsync();
    Task<IReadOnlyList<ComplaintTypeResponseDto>> ComplaintTypesAsyncV2();
    Task<IReadOnlyList<EnumsResponseDto>> GetSources();
    Task<IReadOnlyList<EnumsResponseDto>> GetPriorities();
}
