using Application.Abstractions.Messaging;

namespace Application.EmployeeManagement.UpdateUserCrmRole;

public sealed record UpdateUserCrmRoleCommand(string StaffId, string NewCRMRole) : ICommand<object>;
