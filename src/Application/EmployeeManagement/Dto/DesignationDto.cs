using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Application.EmployeeManagement.Dto;

public class DesignationDto : IAuditableBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DepartmentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public string ConsumerId { get; set; }
}
