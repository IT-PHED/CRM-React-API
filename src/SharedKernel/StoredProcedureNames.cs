namespace SharedKernel;


public static class UserStoreProcedureNames
{
    public const string GET_USER_BY_EMAIL = "SP_GETUSERBYEMAIL";
    public const string UPDATE_USER_OTP = "SP_UPDATE_USER_PASSWORD";
}

public static class ConfigurationStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string INSERT_COMPLAINTTYPE_CONFIGURATION = Prefix + "INSERT_COMPLAINTTYPE_CONFIGURATION";
    public const string UPDATE_COMPLAINTTYPE_CONFIGURATION = Prefix + "UPDATE_COMPLAINTTYPE_CONFIGURATION";
    public const string DELETE_COMPLAINTTYPE_CONFIGURATION = Prefix + "DELETE_COMPLAINTTYPE_CONFIGURATION";
    public const string GET_COMPLAINTTYPE_CONFIGURATIONS = Prefix + "GET_COMPLAINTTYPE_CONFIGURATIONS";
    public const string GET_COMPLAINTTYPE_CONFIGURATIONS_BY_DESIGNATION = Prefix + "GET_COMPLAINTTYPE_CONFIGURATIONS_BY_DESIGNATION";
    public const string GET_COMPLAINTTYPE_CONFIGURATIONS_BY_COMPLAINTTYPE = Prefix + "GET_COMPLAINTTYPE_CONFIGURATIONS_BY_COMPLAINTTYPE";
    public const string GET_COMPLAINTTYPE_CONFIGURATIONS_BY_COMPLAINTSUBTYPE = Prefix + "GET_COMPLAINTTYPE_CONFIGURATIONS_BY_COMPLAINTSUBTYPE";

    public const string INSERT_COMPLAINTTYPE = Prefix + "INSERT_COMPLAINTTYPE";
    public const string UPDATE_COMPLAINTTYPE = Prefix + "UPDATE_COMPLAINTTYPE";
    public const string DELETE_COMPLAINTTYPE = Prefix + "DELETE_COMPLAINTTYPE";
    public const string GET_COMPLAINTTYPE = Prefix + "GET_COMPLAINTTYPE";

    public const string INSERT_COMPLAINTSUBTYPE = Prefix + "INSERT_COMPLAINTSUBTYPE";
    public const string UPDATE_COMPLAINTSUBTYPE = Prefix + "UPDATE_COMPLAINTSUBTYPE";
    public const string DELETE_COMPLAINTSUBTYPE = Prefix + "DELETE_COMPLAINTSUBTYPE";
    public const string GET_COMPLAINTSUBTYPE = Prefix + "GET_COMPLAINTSUBTYPE";
    public const string GET_DATA_FOR_CONFIGURATION = "SPN_CRM_SLA_GET_DATA_FOR_CONFIGURATION";
}
public static class ConsumerStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string GET_CUSTOMER_BY_ID = Prefix + "GET_CUSTOMER_BY_ID";
    public const string GET_CUSTOMER_BY_METER_NUMBER = Prefix + "GET_CUSTOMER_BY_METER_NUMBER";
    public const string GET_CUSTOMER_BY_MOBILE_NUMBER = Prefix + "GET_CUSTOMER_BY_MOBILE_NUMBER";
    public const string GET_CUSTOMER_BY_SEARCH_CRITERIA = Prefix + "GET_CUSTOMER_BY_SEARCH_CRITERIA";
    public const string GET_BILLINGINFO_CONSUMERCOMPLAIN_BY_CONSUMERID = Prefix + "GET_BILLINGINFO_CONSUMERCOMPLAIN_BY_CONSUMERID";
    public const string GET_CUSTOMER_FULL_DETAIL_BY_COMPLAINTID = Prefix + "GET_CUSTOMER_FULL_DETAIL_BY_COMPLAINTID";
    public const string GET_CUSTOMER_FULL_DETAIL_BY_ID = Prefix + "GET_CUSTOMER_FULL_DETAIL_BY_ID";
    public const string GET_LAST_BILLINFO_BY_CONSUMER_ID = Prefix + "GET_LAST_BILLINFO_BY_CONSUMER_ID";
    public const string GET_TARIFF_BY_CONSUMER_ID = Prefix + "GET_TARIFF_BY_CONSUMER_ID";
    public const string GET_BILLINGINFO_BY_CONSUMERID = Prefix + "GET_BILLINGINFO_BY_CONSUMERID";
    public const string GET_BILLINGINFO_BY_MONTHFROM_AND_MONTHTO = Prefix + "GET_BILLINGINFO_BY_MONTHFROM_AND_MONTHTO";
    public const string GET_CONSUMER_ADDRESSES = Prefix + "GET_CONSUMER_ADDRESSES";
    public const string UPDATE_CONSUMER = Prefix + "UPDATE_CONSUMER";
    public const string UPDATE_CON_METER = Prefix + "UPDATE_CON_METER";
    public const string GET_CONSUMER_360_INFO = Prefix + "GETCONSUMER_360INFO";

    public const string GET_BirthDayConsumers = Prefix + "GET_BirthDayConsumers";
    public const string GET_OutageConsumers = Prefix + "GET_OutageConsumers";
    public const string GET_OutageSchedule = Prefix + "GET_OutageSchedule";
    public const string Update_OutageSchedule = Prefix + "Update_OutageSchedule";
    public const string spCRM_Desk = Prefix + "Desk";
}

public static class ComplaintStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string INSERT_COMPLAINT_CONSTRAINT = Prefix + "INSERT_COMPLAINT_CONSTRAINT";
    public const string UPDATE_COMPLAINT_CONSTRAINT = Prefix + "UPDATE_COMPLAINT_CONSTRAINT";
    public const string INSERT_CONSUMER_COMPLAINT = Prefix + "INSERT_CONSUMER_COMPLAINT";
    public const string INSERT_CONSUMER_COMPLAINT_V2 = Prefix + "INSERT_CONSUMER_COMPLAINT_V2";
    public const string UPDATE_CONSUMER_COMPLAINT_STATUS = Prefix + "UPDATE_CONSUMER_COMPLAINT_STATUS";
    public const string CLOSE_CONSUMER_COMPLAINT_STATUS = Prefix + "CLOSE_CONSUMER_COMPLAINT_STATUS";
    public const string UPDATE_CONSUMER_COMPLAINT = Prefix + "UPDATE_CONSUMER_COMPLAINT";
    public const string UPDATE_CONSUMER_COMPLAINT_V2 = Prefix + "UPDATE_CONSUMER_COMPLAINT_V2";
    public const string ALLOCATE_CONSUMER_COMPLAINT = "SPN_CRM_AUTOMATIC_ALLOCATE";
    public const string ALLOCATE_CONSUMER_COMPLAINT_V2 = "SPN_CRM_AUTOMATIC_ALLOCATE_V3";
    public const string GET_COMPLAINT_BY_ID = Prefix + "GET_COMPLAINT_BY_ID";
    public const string GET_RESOLVED_COMPLAINT_BY_ID = Prefix + "GET_RESOLVED_COMPLAINT_BY_ID";
    public const string GET_COMPLAINT_BY_TICKETNUMBER = Prefix + "GET_COMPLAINT_BY_TICKETNUMBER";
    public const string GET_COMPLAINT_BY_DATERANGE = Prefix + "GET_COMPLAINT_BY_DATERANGE";
    public const string GET_COMPLAINT_BY_IBC = Prefix + "GET_COMPLAINT_BY_IBC";
    public const string GET_COMPLAINT_BY_IBC_BSC = Prefix + "GET_COMPLAINT_BY_IBC_BSC";
    public const string GET_COMPLAINT_BY_IBC_BSC_BOOK = Prefix + "GET_COMPLAINT_BY_IBC_BSC_BOOK";
    public const string GET_COMPLAINT_TYPES = Prefix + "GET_COMPLAINT_TYPES";
    public const string GET_COMPLAINT_TYPE_BY_ID = Prefix + "GET_COMPLAINT_TYPE_BY_ID";
    public const string GET_COMPLAINT_TYPE_WITH_SUBTYPE_BY_ID = Prefix + "GET_COMPLAINT_TYPE_WITH_SUBTYPE_BY_ID";
    public const string GET_COMPLAINT_TYPE_WITH_SUBTYPE = Prefix + "GET_COMPLAINT_TYPE_WITH_SUBTYPE";
    public const string GET_BILLINGINFO_BY_CONSUMERID_AND_CORRECTMETERREADING_LT_CURRENTMETERREADING = Prefix + "GET_BILLINGINFO_BY_CONSUMERID_AND_CORRECTMETERREADING_LT_CURRENTMETERREADING";
    public const string GET_COMPLAINT_BILLINFO_DETAIL_FOR_WRONG_METER_READING = Prefix + "GET_COMPLAINT_BILLINFO_DETAIL_FOR_WRONG_METER_READING";
    public const string GET_COMPLAINT_BILLINFO_DETAIL_FOR_HIGH_ESTIMATE_READING = Prefix + "GET_COMPLAINT_BILLINFO_DETAIL_FOR_HIGH_ESTIMATE_READING";
    public const string GET_PREPAID_TO_NOMETER_LATEST_RECHARGE = Prefix + "GET_PREPAID_TO_NOMETER_LATEST_RECHARGE";
    public const string GET_PREPAID_TO_NOMETER_APPROVAL_DETAILS = Prefix + "GET_PREPAID_TO_NOMETER_APPROVAL_DETAILS";
    public const string GET_POSTPAID_TO_NOMETER_LATEST_BILL = Prefix + "GET_POSTPAID_TO_NOMETER_LATEST_BILL";
    public const string GET_CONSUMER_METER_PHASE = Prefix + "GET_CONSUMER_METER_PHASE";
    public const string GET_POSTPAID_TO_NOMETER_APPROVAL_DETAILS = Prefix + "GET_POSTPAID_TO_NOMETER_APPROVAL_DETAILS";
    public const string GET_POSTPAID_TO_PREPAID_METER_DETAIL = Prefix + "GET_POSTPAID_TO_PREPAID_METER_DETAIL";
    public const string GET_POSTPAID_TO_POSTPAID_METER_DETAIL = Prefix + "GET_POSTPAID_TO_POSTPAID_METER_DETAIL";
    public const string GET_TARIFF_CHANGE_DETAIL = Prefix + "GET_TARIFF_CHANGE_DETAIL";
    public const string GET_NAME_ADDRESS_CHANGE_DETAIL = Prefix + "GET_NAME_ADDRESS_CHANGE_DETAIL";
    public const string GET_METER_DIGIT_CHANGE_DETAIL = Prefix + "GET_METER_DIGIT_CHANGE_DETAIL";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_ALLOCATION = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_ALLOCATION";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_RESOLUTION = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_RESOLUTION";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_ALLOCATION_VACATION = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_ALLOCATION_VACATION";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_RESOLUTION_VACATION = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_RESOLUTION_VACATION";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_WITHOUT_IBC_BSC = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_WITHOUT_IBC_BSC";
    public const string INSERT_TICKET_FOR_FIELDAGENT = Prefix + "INSERT_TICKET_FOR_FIELDAGENT";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_APPROVER = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_APPROVER";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_IAD = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_IAD";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_VACATION_FINANCE = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_VACATION_FINANCE";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_CM = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_CM";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_GCM = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_GCM";
    public const string GET_COMPLAINT_BY_SEARCH_CRITERIA_INCIDENT = Prefix + "GET_COMPLAINT_BY_SEARCH_CRITERIA_INCIDENT";
    public const string GET_PAYMENT_PURPOSE_ID_PRIORITY = Prefix + "GET_PAYMENT_PURPOSE_ID_PRIORITY";
    public const string GET_METER_RELATED_INSTALLERS = Prefix + "GET_METER_RELATED_INSTALLERS";
    public const string GET_METER_RELATED_METER_DETAIL_BY_INSTALLERID = Prefix + "GET_METER_RELATED_METER_DETAIL_BY_INSTALLERID";
    public const string SP_SAVE_CAAD = "SP_SAVE_CAAD";
    public const string SP_UpdateIncident = "SP_UpdateIncident";
    public const string SP_UpdateNERCComplaint = Prefix + "UPDATE_NERC_CONSUMER_COMPLAINT";
    public const string SP_SetNercComplaintInProgress = Prefix + "SET_NERC_COMPLAINT_IN_PROGRESS";
    public const string SPN_CommentNERCComplaint = "SPN_CRM_Insert_Complaint_Comment";
    public const string SPN_GetComments = "SPN_CRM_Get_Complaint_Comments";
    public const string INSERT_MASTER_COMPLAINT = "SP_CRM_MASTER_COMPLAINT_TRANSACTION";

    public const string GET_COMPLAINT_HISTORY = Prefix + "GET_COMPLAINT_HISTORY";
    public const string GET_SIXMONTH_BILLDETAILS = Prefix + "GET_SIXMONTH_BILLDETAILS";
    public const string GET_COLLECTION_DETAILS = Prefix + "GET_COLLECTION_DETAILS";
    public const string GET_CRDR_DETAILS = Prefix + "GET_CRDR_DETAILS";

    public const string spComplaintsCAAD = Prefix + "CompaintsCAAD";
    public const string spComplaintLog = Prefix + "CompaintLog";
    public const string spComplaintDocs = Prefix + "CompaintDocs";
    public const string spCRM_Reports = Prefix + "Reports";

    public const string SP_SAVE_COMPLAINT_DATA = "SPN_SAVE_COMPLAINT_DATA";

    public const string SPN_CRM_GET_COMPLAINTS_BY_GROUPID = "SPN_CRM_GET_COMPLAINTS_BY_GROUDID";
    public const string REASSIGN_TICKET = "SP_CRM_ReassignTicket";
    public const string GET_COMPLAINTS_BY_GROUPID_STATUS = "SPN_CRM_GET_COMPLAINTS_BY_GROUDID_STATUS";
    public const string QUERY_COMPLAINTS_BY_GROUP = "SPN_CRM_QUERY_COMPLAINTS_BY_GROUD";
    public const string QUERY_ALL_COMPLAINTS = "SPN_CRM_QUERY_ALL_COMPLAINTS_NEW";
    public const string SPN_CRM_GET_COMPLAINTS_BY_DEPARTMENTID = "SPN_CRM_GET_COMPLAINTS_BY_DEPARTMENTID";
    public const string GET_COMPLAINTS_BY_DEPARTMENTID_STATUS = "SPN_CRM_GET_COMPLAINTS_BY_DEPARTMENTID_STATUS";
    public const string QUERY_COMPLAINTS_BY_DEPARTMENT = "SPN_CRM_QUERY_COMPLAINTS_BY_DEPARTMENT";
    public const string GET_MONTHLY_COMPLAINTS_STAT = "spn_crm_get_complaint_stats_monthly";
    public const string QUERY_COMPLAINTS_BY_STATUS_REGION_DEPT = "SPN_CRM_QUERY_COMPLAINTS_BY_STATUS_REGION_DEPT";
    public const string GET_COMPLAINT_BY_TICKETNUMBER_V2 = Prefix + "GET_COMPLAINT_BY_TICKETNUMBER_V2";
    public const string SP_CRM_GENERATE_TICKET = Prefix + "GENERATE_TICKET";
}

public static class AppprovalStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string INSERT_LEVEL_APPROVERS = Prefix + "INSERT_LEVEL_APPROVERS";
    public const string INSERT_AMOUNT_RANGE_LEVEL = Prefix + "INSERT_AMOUNT_RANGE_LEVEL";
    public const string INSERT_APPROVAL_AMOUNT_RANGE = Prefix + "INSERT_APPROVAL_AMOUNT_RANGE";
    public const string INSERT_APPROVAL_LEVEL = Prefix + "INSERT_APPROVAL_LEVEL";
    public const string GET_ALL_APPROVAL_LEVEL = Prefix + "GET_ALL_APPROVAL_LEVEL";
    public const string GET_ALL_APPROVAL_AMOUNT_RANGE = Prefix + "GET_ALL_APPROVAL_AMOUNT_RANGE";
    public const string GET_ALL_APPROVERS_IN_LEVEL = Prefix + "GET_ALL_APPROVERS_IN_LEVEL";
    public const string GET_ALL_LEVELS_IN_APPROVAL_AMOUNT_RANGE = Prefix + "GET_ALL_LEVELS_IN_APPROVAL_AMOUNT_RANGE";
    public const string GET_APPROVAL_LEVEL_BY_ID = Prefix + "GET_APPROVAL_LEVEL_BY_ID";
    public const string GET_UNASSIGNED_APPROVERS = Prefix + "GET_UNASSIGNED_APPROVERS";
    public const string GET_AMOUNTRANGE_BY_ID = Prefix + "GET_AMOUNTRANGE_BY_ID";
    public const string GET_AMOUNTRANGE_BY_START_END_AMOUNT = Prefix + "GET_AMOUNTRANGE_BY_START_END_AMOUNT";
    public const string GET_APPROVAL_LEVEL_BY_NAME = Prefix + "GET_APPROVAL_LEVEL_BY_NAME";
    public const string GET_LEVEL_APPROVER_BY_LEVELID_APROVER = Prefix + "GET_LEVEL_APPROVER_BY_LEVELID_APROVER";
    public const string GET_LEVEL_APPROVER_BY_APROVER = Prefix + "GET_LEVEL_APPROVER_BY_APROVER";
    public const string GET_AMOUNT_RANGE_LEVEL_BY_LEVEL = Prefix + "GET_AMOUNT_RANGE_LEVEL_BY_LEVEL";
    public const string GET_APPROVAL_AMOUNTRANGE_LEVEL_BY_LEVEL_ID = Prefix + "GET_APPROVAL_AMOUNTRANGE_LEVEL_BY_LEVEL_ID";
    public const string CAN_APPROVER_APPROVE = Prefix + "CAN_APPROVER_APPROVE";
    public const string CAN_FINALIZE_APPROVAL = Prefix + "CAN_FINALIZE_APPROVAL";
    public const string GET_APPROVER_HOLD_REMARK = Prefix + "GET_APPROVER_HOLD_REMARK";
    public const string GET_NERC_EMAIL_COMPLAINTS = Prefix + "GET_NERC_EMAIL_COMPLAINTS";
    public const string GET_All_NERC_EMAIL_COMPLAINTS = Prefix + "GET_ALL_NERC_EMAIL_COMPLAINTS";

    public const string GET_MASTER_NERC_EMAIL_COMPLAINTS = Prefix + "GET_MASTER_NERC_EMAIL_COMPLAINTS";


    public const string GET_APPROVER_REJECT_REMARK = Prefix + "GET_APPROVER_REJECT_REMARK";
    public const string GET_PRIORITY = Prefix + "GET_PRIORITY";
    public const string GET_SOURCE = Prefix + "GET_SOURCE";
    public const string GET_TARIFF = Prefix + "GET_TARIFF";
    public const string GET_METER_DIGIT = Prefix + "GET_METER_DIGIT";
    public const string GET_METER_MAKE = Prefix + "GET_METER_MAKE";
    public const string GET_PURPOSE = Prefix + "GET_PURPOSE";

    public const string GET_COMPLAINT_METER_MAKE_DIGIT_PHASE_TYPE = Prefix + "GET_COMPLAINT_METER_MAKE_DIGIT_PHASE_TYPE";
    public const string GET_CONSUMER_TARIFF_PURPOSE = Prefix + "GET_CONSUMER_TARIFF_PURPOSE";
    public const string GET_METER_STATUS = Prefix + "GET_METER_STATUS";

    public const string GET_METER_TYPES = Prefix + "GET_METER_TYPES";
    public const string GET_CONSUMER_METER_MAKE_DIGIT = Prefix + "GET_CONSUMER_METER_MAKE_DIGIT";
    public const string GET_CONSUMER_METER_REPLACEMENT_TYPE = Prefix + "GET_CONSUMER_METER_REPLACEMENT_TYPE";
    public const string VALIDATE_PREPAID_TO_PREPAID_METER = Prefix + "VALIDATE_PREPAID_TO_PREPAID_METER";
    public const string CAN_REGISTER_CONSUMER_COMPLAINT = Prefix + "CAN_REGISTER_CONSUMER_COMPLAINT";

    public const string DELETE_APPROVAL_AMOUNT_RANGE = Prefix + "DELETE_APPROVAL_AMOUNT_RANGE";
    public const string DELETE_AMOUNT_RANGE_LEVEL = Prefix + "DELETE_AMOUNT_RANGE_LEVEL";
    public const string DELETE_APPROVAL_LEVEL = Prefix + "DELETE_APPROVAL_LEVEL";
    public const string DELETE_LEVEL_APPROVERS = Prefix + "DELETE_LEVEL_APPROVERS";
    public const string UPDATE_APPROVAL_AMOUNT_RANGE = Prefix + "UPDATE_APPROVAL_AMOUNT_RANGE";
    public const string UPDATE_LEVEL_APPROVER = Prefix + "UPDATE_LEVEL_APPROVER";
}

public static class AreaStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string GET_IBC_BSC_BOOK_CATEGORY = Prefix + "GET_IBC_BSC_BOOK_CATEGORY";
    public const string GET_IBC_BSC_CATEGORY = Prefix + "GET_IBC_BSC_CATEGORY";
    public const string GET_Regions = Prefix + "Regions";
}

public static class DashboardStoreProcedureNames
{
    private const string Prefix = "SP_CRM_";
    public const string GET_DIVISION = Prefix + "DIVISIONS";
    public const string GET_TICKET_SUMMARY = Prefix + "GETTICKETSUMMARY";
    public const string GET_CATEGORY_TICKETS = Prefix + "GETCATEGORYWISETICKETS";
    public const string GET_DATEWISE_TICKETS = Prefix + "GETDATEWISETICKETS";
    public const string GET_LOCATIONWISE_TICKETS = Prefix + "GETLOCATIONWISETICKETS";
    public const string GET_SLADURATION_TICKETS = Prefix + "GETSLADURATION";
    public const string GET_SLA_COUNT_SUMMARY = Prefix + "GETSLASUMMARY";
    public const string GET_SLA_DIVCOUNT_SUMMARY = Prefix + "GETDIVWISESLASUMMARY";
}
