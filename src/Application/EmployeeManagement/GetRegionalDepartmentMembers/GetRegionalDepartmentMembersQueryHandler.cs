using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetRegionalDepartmentMembers;

internal sealed class GetRegionalDepartmentMembersQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetRegionalDepartmentMembersQuery, IEnumerable<UserRegionalProfileDto>>
{
    public async Task<Result<IEnumerable<UserRegionalProfileDto>>> Handle(GetRegionalDepartmentMembersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<Domain.User.UserProfile> employees = await employeeManagementService.GetRegionalDepartmentMember(query.DepartmentId, query.AccountNumber, query.RegionId);

            if (employees == null)
            {
                return Result.Failure<IEnumerable<UserRegionalProfileDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No regional department member was found!"));
            }

            return employees.Select(x => (UserRegionalProfileDto)x).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<UserRegionalProfileDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch regional department member data"));
        }
    }
}
