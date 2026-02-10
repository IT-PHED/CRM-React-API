using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;

namespace Application.ComplaintResolution.ResolveHighEstimatedReadingComplaint;

public sealed record ResolveHighEstimatedReadingComplaintCommand(string consumerId, double EstimatedReading, DateTime MonthFrom, DateTime MonthTo) : ICommand<CalculatedHighEstimateComplaintDto>;
