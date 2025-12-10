using Domain.Complaint;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ComplaintConfig;

internal sealed class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
{
    public void Configure(EntityTypeBuilder<Complaint> builder)
    {
        builder.ToTable("TBL_CRM_CONSUMERCOMPLAINT");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("ID").HasMaxLength(60);

        builder.Property(e => e.ConsumerId).HasColumnName("CONSUMERID").HasMaxLength(60);
        builder.Property(e => e.ComplaintSubTypeId).HasColumnName("COMPLAINTSUBTYPEID").HasMaxLength(60);
        builder.Property(e => e.Source).HasColumnName("SOURCE").HasMaxLength(200);
        builder.Property(e => e.Ticket).HasColumnName("TICKET").HasMaxLength(60);
        builder.Property(e => e.DateGenerated).HasColumnName("DATEGENERATED");
        builder.Property(e => e.DateResolved).HasColumnName("DATERESOLVED");
        builder.Property(e => e.Priority).HasColumnName("PRIORITY").HasMaxLength(60);
        builder.Property(e => e.ComplaintTypeId).HasColumnName("COMPLAINTTYPEID").HasMaxLength(60);
        builder.Property(e => e.Remark).HasColumnName("REMARK").HasMaxLength(300);
        builder.Property(e => e.CreatedBy).HasColumnName("CREATEDBY").HasMaxLength(20);
        builder.Property(e => e.CreatedDate).HasColumnName("CREATEDDATE");
        builder.Property(e => e.ModifiedBy).HasColumnName("MODIFIEDBY").HasMaxLength(20);
        builder.Property(e => e.ModifiedDate).HasColumnName("MODIFIEDDATE");
        builder.Property(e => e.Status).HasColumnName("STATUS").HasMaxLength(20);
        builder.Property(e => e.Name).HasColumnName("NAME").HasMaxLength(60);
        builder.Property(e => e.MeterNo).HasColumnName("METERNO").HasMaxLength(60);
        builder.Property(e => e.TelephoneNo).HasColumnName("TELEPHONENO").HasMaxLength(60);
        builder.Property(e => e.MaxDemand).HasColumnName("MAXDEMAND").HasMaxLength(60);
        builder.Property(e => e.Category).HasColumnName("CATEGORY").HasMaxLength(60);
        builder.Property(e => e.Address1).HasColumnName("ADDRESS1").HasMaxLength(300);
        builder.Property(e => e.Address2).HasColumnName("ADDRESS2").HasMaxLength(300);
        builder.Property(e => e.Address3).HasColumnName("ADDRESS3").HasMaxLength(300);
        builder.Property(e => e.Ibc).HasColumnName("IBC").HasMaxLength(60);
        builder.Property(e => e.Bsc).HasColumnName("BSC").HasMaxLength(60);
        builder.Property(e => e.RouteNumber).HasColumnName("ROUTENUMBER").HasMaxLength(60);
        builder.Property(e => e.Email).HasColumnName("EMAIL").HasMaxLength(60);
        builder.Property(e => e.MobileNo).HasColumnName("MOBILENO").HasMaxLength(60);
        builder.Property(e => e.Type).HasColumnName("TYPE").HasMaxLength(60);
        builder.Property(e => e.Dtr).HasColumnName("DTR").HasMaxLength(60);
        builder.Property(e => e.Purpose).HasColumnName("PURPOSE").HasMaxLength(200);
        builder.Property(e => e.MeterDigit).HasColumnName("METERDIGIT").HasMaxLength(20);
        builder.Property(e => e.MeterMake).HasColumnName("METERMAKE").HasMaxLength(20);
        builder.Property(e => e.SmsFlag).HasColumnName("SMSFLAG");
        builder.Property(e => e.EmailFlag).HasColumnName("EMAIL_FLAG");
        builder.Property(e => e.RegionId).HasColumnName("REGIONID").HasMaxLength(20);
        builder.Property(e => e.AdviceType).HasColumnName("ADVICETYPE").HasMaxLength(20);
        builder.Property(e => e.CDeskId).HasColumnName("CDESKID");
        builder.Property(e => e.SlaLevel).HasColumnName("SLA_LEVEL").HasMaxLength(4);
        builder.Property(e => e.Region_Id).HasColumnName("REGION_ID").HasMaxLength(20);
        builder.Property(e => e.GroupId).HasColumnName("GROUP_ID");
        builder.Property(e => e.AssignedTo).HasColumnName("ASSIGNEDTO").HasMaxLength(50);
        builder.Property(e => e.Feedback).HasColumnName("FEEDBACK").HasMaxLength(4000);
        builder.Property(e => e.DepartmentId).HasColumnName("DEPARTMENTID").HasMaxLength(50);
        builder.Property(e => e.ResolvedBy).HasColumnName("RESOLVEDBY").HasMaxLength(50);
        builder.Property(e => e.MediaUrl).HasColumnName("MEDIAURL").HasMaxLength(4000);
        builder.Property(e => e.ClosedBy).HasColumnName("CLOSEDBY").HasMaxLength(60);
        builder.Property(e => e.ClosedByRemark).HasColumnName("CLOSEDBY_REMARK").HasMaxLength(4000);
        builder.Property(e => e.ClosedDate).HasColumnName("CLOSED_DATE");
    }
}
