namespace Domain.Common;

public enum EUserLogAction
{
    VIEW = 1,
    INSERT = 2,
    UPDATE = 3,
    DELETE = 4,
    CHECKIN = 5,
    CHECKOUT = 6,
    GETDATA = 7,
    RESET = 8,
    ACTIVATE = 9,
    DEACTIVATE = 10,
    UPLOAD = 11,
    VERIFY_OTP = 12,
    LOG_COMPLAINT = 13,
    REASSIGN_COMPLAINT = 14,
    APPROVE_COMPLAINT = 15,
    CLOSE_COMPLAINT = 16
}

public enum EUserLogStatus
{
    SUCCESS = 1,
    FAILURE = 2
}

public enum EUserLogPageName
{
    LOGIN = 1,
    COMPLAINT = 2
}

public enum EUserLogModule
{
    LOGIN = 1,
    HOME = 2,
    CONFIGURATION = 3,
    REGULARISATION = 4,
    CONNECTION = 5,
    COLLECTION = 6,
    BILLING = 7,
    UTLITIES = 8,
    REPORT = 9,
    LOGOUT = 10,
    NSC = 11,
    LOG_COMPLAINT = 12,
    REASSIGN_COMPLAINT = 13,
    APPROVE_COMPLAINT = 14,
    CLOSE_COMPLAINT = 15
}
