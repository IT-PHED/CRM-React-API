using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetSmsType;

internal sealed class GetSmsTypeQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetSmsTypeQuery, IEnumerable<SmsInfoDto>>
{
    public async Task<Result<IEnumerable<SmsInfoDto>>> Handle(GetSmsTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<SmsInfoDto> smsType = await employeeManagementService.GetSmsType();

            if (smsType is null)
            {
                return Result.Failure<IEnumerable<SmsInfoDto>>(Domain.Common.CommonErrors.CustomErrorMessage("sms type does not exist!"));
            }

            return smsType.ToList();
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return Result.Failure<IEnumerable<SmsInfoDto>>(Domain.Common.CommonErrors.CustomErrorMessage("Failed to fetch sms type"));
        }
    }
}
