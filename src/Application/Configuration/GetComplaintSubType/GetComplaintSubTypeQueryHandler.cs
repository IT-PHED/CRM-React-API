using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Configuration.GetComplaintSubType;

internal sealed class GetComplaintSubTypeQueryHandler(IConfigurationService configurationService) : IQueryHandler<GetComplaintSubTypeQuery, IEnumerable<ComplaintSubTypeResponseDto>>
{
    public async Task<Result<IEnumerable<ComplaintSubTypeResponseDto>>> Handle(GetComplaintSubTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ComplaintSubTypeResponseDto> subTypes = await configurationService.GetComplaintSubTypesAsync();

            return subTypes.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<ComplaintSubTypeResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Complaint Sub types"));
        }
    }
}
