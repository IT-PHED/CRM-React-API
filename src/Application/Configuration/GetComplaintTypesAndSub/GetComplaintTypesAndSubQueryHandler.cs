using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Configuration.GetComplaintTypesAndSub;

internal class GetComplaintTypesAndSubQueryHandler(IConfigurationService configurationService) : IQueryHandler<GetComplaintTypesAndSubQuery, IEnumerable<ComplaintTypeResponseDto>>
{
    public async Task<Result<IEnumerable<ComplaintTypeResponseDto>>> Handle(GetComplaintTypesAndSubQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ComplaintTypeResponseDto> subTypes = await configurationService.ComplaintTypesAsyncV2();

            return subTypes.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<ComplaintTypeResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Complaint types"));
        }
    }
}
