using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.EmployeeManagement.UpdateUserCrmRole;

internal sealed class UpdateUserCrmRoleCommandHandler(IEmployeeManagementService employeeManagementService) : ICommandHandler<UpdateUserCrmRoleCommand, object>
{
    public async Task<Result<object>> Handle(UpdateUserCrmRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            object employee = await employeeManagementService.UpdateUserCRMRole(command.StaffId, command.NewCRMRole);
            return employee;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<object>(Domain.Common.CommonErrors.CustomErrorMessage("Failed to update crm role menu"));
        }
    }
}
