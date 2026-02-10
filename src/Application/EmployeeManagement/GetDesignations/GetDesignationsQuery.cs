using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetDesignations;

public sealed record GetDesignationsQuery : IQuery<IEnumerable<DesignationDto>>;
