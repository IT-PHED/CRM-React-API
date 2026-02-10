using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Application.Dashboard.GetCategoryWiseSummary;
using Application.Dashboard.GetDateWiseSummary;
using Application.Dashboard.GeteLocationWiseSummary;
using Application.Dashboard.GetMonthlyStat;
using Application.Dashboard.GetSlaCountSummary;
using Application.Dashboard.GetSlaDIVCountSummary;
using Application.Dashboard.GetSlaDurationSummary;
using Application.Dashboard.GetTicketSummary;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Dashboard;

internal sealed class Dashboard : IEndpoint
{
    internal sealed record Params(DateTime fromDate, DateTime toDate, string? divId = null);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("dashboard/category-wise-summary", async ([AsParameters] Params fields, IQueryHandler<GetCategoryWiseSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetCategoryWiseSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All Category Wise Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the Category Wise Summary");

        app.MapGet("dashboard/date-wise-summary", async ([AsParameters] Params fields, IQueryHandler<GetDateWiseSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetDateWiseSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All Date Wise Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the Date Wise Summary");

        app.MapGet("dashboard/location-wise-summary", async ([AsParameters] Params fields, IQueryHandler<GeteLocationWiseSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GeteLocationWiseSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All Location Wise Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the Location Wise Summary");

        app.MapGet("dashboard/sla-count-summary", async ([AsParameters] Params fields, IQueryHandler<GetSlaCountSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSlaCountSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All SLA Count Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the SLA Count Summary");

        app.MapGet("dashboard/sla-div-count-summary", async ([AsParameters] Params fields, IQueryHandler<GetSlaDivCountSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSlaDivCountSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All SLA Div Count Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the SLA Div Count Summary");

        app.MapGet("dashboard/sla-duration-summary", async ([AsParameters] Params fields, IQueryHandler<GetSlaDurationSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSlaDurationSummaryQuery(fields.fromDate, fields.toDate, fields.divId);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All SLA Duration Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the SLA Duration Summary");

        app.MapGet("dashboard/ticket-summary", async ([AsParameters] Params fields, IQueryHandler<GetTicketSummaryQuery, IEnumerable<DashboardDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetTicketSummaryQuery(fields.divId, fields.fromDate, fields.toDate);

            Result<IEnumerable<DashboardDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DashboardDto>>.Success(value, "All SLA Ticket Summary")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch all the SLA Ticket Summary");

        app.MapGet("dashboard/crm-reports/monthly-stat", async ([FromQuery] string departmentId, IQueryHandler<GetMonthlyStatQuery, ComplaintMonthlyStat> handler, CancellationToken cancellationToken, [FromQuery] string? regionId = null) =>
        {
            var query = new GetMonthlyStatQuery(departmentId, regionId);

            Result<ComplaintMonthlyStat> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<ComplaintMonthlyStat>.Success(value, "All Department Complaint monthly Stats")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Dashboard).WithDescription("Fetch the monthly department Stats");
    }
}
