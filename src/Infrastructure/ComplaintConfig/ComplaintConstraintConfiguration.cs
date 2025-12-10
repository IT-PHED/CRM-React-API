using Domain.Complaint;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ComplaintConfig;

internal sealed class ComplaintConstraintConfiguration : IEntityTypeConfiguration<ComplaintConstraint>
{
    public void Configure(EntityTypeBuilder<ComplaintConstraint> builder)
    {
        builder.ToTable("TBL_CRM_COMPLAINTCONSTRAINT");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id)
                   .HasColumnName("ID")
                   .HasMaxLength(60)
                   .IsRequired();

        builder.Property(e => e.ComplaintId)
               .HasColumnName("COMPLAINTID")
               .HasMaxLength(60);

        builder.Property(e => e.CorrectMeterReading)
                   .HasColumnName("CORRECTMETERREADING")
                   .HasPrecision(38, 0);

        builder.Property(e => e.CreatedBy)
               .HasColumnName("CREATEDBY")
               .HasMaxLength(60);

        builder.Property(e => e.CreatedDate)
               .HasColumnName("CREATEDDATE")
               .HasColumnType("DATE");

        builder.Property(e => e.ModifiedBy)
               .HasColumnName("MODIFIEDBY")
               .HasMaxLength(60);

        builder.Property(e => e.ModifiedDate)
               .HasColumnName("MODIFIEDDATE")
               .HasColumnType("DATE");

        builder.Property(e => e.MonthFrom)
               .HasColumnName("MONTHFROM")
               .HasColumnType("DATE");

        builder.Property(e => e.MonthTo)
               .HasColumnName("MONTHTO")
               .HasColumnType("DATE");

        builder.Property(e => e.FilePath)
               .HasColumnName("FILEPATH")
               .HasMaxLength(300);

        builder.Property(e => e.CreatedDate)
                   .HasDefaultValueSql("SYSDATE");

        builder.Property(e => e.ModifiedDate)
               .HasDefaultValueSql("SYSDATE");
    }
}
