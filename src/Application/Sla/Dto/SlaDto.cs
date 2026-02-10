namespace Application.Sla.Dto;

public class SlaDto
{
    public string TICKETCODE { get; set; }
    public string CONSUMERID { get; set; }

    public string CATEGORY_NAME { get; set; }
    public string SUBCATEGORY_NAME { get; set; }

    public string DIV_NAME { get; set; }
    public string SEC_NAME { get; set; }



    //EMailSLA

    public string EMAILID { get; set; }
    public string CONFIGID { get; set; }

    public string CREATEDDATE { get; set; }
    public string CREATEDBY { get; set; }


    //complainttypeconfigure

    public string DESIGNATIONID { get; set; }
    public string DesignationName { get; set; }
    public string LEVELNAME { get; set; }
    public string PERIOD { get; set; }
    public string PERIODTYPE { get; set; }
}
