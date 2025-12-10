using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Configuration.GetComplaintType;

internal sealed class GetComplaintTypeQueryHandler(IConfigurationService configurationService) : IQueryHandler<GetComplaintTypeQuery, IEnumerable<ComplaintTypeResponseDto>>
{
    public async Task<Result<IEnumerable<ComplaintTypeResponseDto>>> Handle(GetComplaintTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ComplaintTypeResponseDto> subTypes = await configurationService.GetComplaintTypesAsync();

            return subTypes.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<ComplaintTypeResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Complaint types"));
        }
    }
}
