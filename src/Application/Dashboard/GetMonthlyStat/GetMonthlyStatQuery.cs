using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetMonthlyStat;

public sealed record GetMonthlyStatQuery(string departmentId, string? regionId = null) : IQuery<ComplaintMonthlyStat>;
