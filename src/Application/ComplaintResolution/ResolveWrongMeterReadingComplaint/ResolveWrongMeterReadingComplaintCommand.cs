using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;

namespace Application.ComplaintResolution.ResolveWrongMeterReadingComplaint;

public sealed record ResolveWrongMeterReadingComplaintCommand(
    Guid ComplaintId,
    int CorrectMeterReading,
    int BillingMonth,
    int BillingYear) : ICommand<CalculatedWrongMeterComplaintDto>;

