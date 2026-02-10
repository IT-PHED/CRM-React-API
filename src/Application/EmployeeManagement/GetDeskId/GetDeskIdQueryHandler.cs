using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetDeskId;

internal sealed class GetDeskIdQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetDeskIdQuery, IEnumerable<DeskIdDto>>
{
    public async Task<Result<IEnumerable<DeskIdDto>>> Handle(GetDeskIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<DeskIdDto> users = await employeeManagementService.GetDeskId();

            if (users == null)
            {
                return Result.Failure<IEnumerable<DeskIdDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No user data was found!"));
            }

            return users.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<DeskIdDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch desk id data"));
        }
    }
}
