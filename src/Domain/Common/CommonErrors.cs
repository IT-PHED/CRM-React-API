using SharedKernel;

namespace Domain.Common;
public static class CommonErrors
{
    public static Error ErrorFetchingStaffAttendance(string message) => Error.Conflict(
       "Common.ErrorFetchingStaffAttendance", $"Error fetching staff attendance for message -- {message}");

    public static Error CustomErrorMessage(string message) => Error.Conflict(
        "Common.CustomErrorMessage", $"Error: -- {message}");
}

