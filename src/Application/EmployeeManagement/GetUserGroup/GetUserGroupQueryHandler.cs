using Application.Abstractions.Authentication;
using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetUserGroup;

internal sealed class GetUserGroupQueryHandler(IEmployeeManagementService employeeManagementService, IUserContext userContext) : IQueryHandler<GetUserGroupQuery, UserGroupDto>
{
    public async Task<Result<UserGroupDto>> Handle(GetUserGroupQuery query, CancellationToken cancellationToken)
    {
        string userId = query.userId ?? userContext.UserId;
        try
        {
            UserGroupDto userGroup = await employeeManagementService.GetUserGroup(userId);

            if (userGroup == null)
            {
                return Result.Failure<UserGroupDto>(Domain.Common.CommonErrors.CustomErrorMessage("No user group id was found!"));
            }

            return userGroup;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<UserGroupDto>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch employee data"));
        }
    }
}
