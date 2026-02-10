using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetGroupMembers;

internal sealed class GetGroupMembersQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetGroupMembersQuery, IEnumerable<UserRegionalProfileDto>>
{
    public async Task<Result<IEnumerable<UserRegionalProfileDto>>> Handle(GetGroupMembersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<Domain.User.UserProfile> employees = await employeeManagementService.GetGroupMembers(query.GroupId);

            if (employees == null)
            {
                return Result.Failure<IEnumerable<UserRegionalProfileDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No group members was found!"));
            }

            return employees.Select(x => (UserRegionalProfileDto)x).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<UserRegionalProfileDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch group members data"));
        }
    }
}
