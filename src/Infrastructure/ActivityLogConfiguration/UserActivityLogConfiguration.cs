using Domain.ActivityLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ActivityLogConfiguration;

internal sealed class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
{
    public void Configure(EntityTypeBuilder<UserActivityLog> builder)
    {
        builder.ToTable("TBL_USERACTIVITY");

        builder.HasKey(e => e.UserId);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("USERID");

        builder.Property(e => e.Action)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("ACTION");

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("STATUS");

        builder.Property(e => e.Module)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("MODULE");

        builder.Property(e => e.Desci)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("DESCI");

        builder.Property(e => e.ConsumerNo)
            .HasMaxLength(20)
            .HasColumnName("CONSUMERNO");

        builder.Property(e => e.OldValue)
          .HasMaxLength(4000)
          .HasColumnName("OLDVALUE");

        builder.Property(e => e.NewValue)
            .HasMaxLength(1000)
            .HasColumnName("NEWVALUE");

        builder.Property(e => e.ReferenceId)
            .HasMaxLength(100)
            .HasColumnName("REFERENCEID");

        builder.Property(e => e.PageName)
            .HasMaxLength(100)
            .HasColumnName("PAGENAME");

        builder.Property(e => e.EmailAddr)
            .HasMaxLength(150)
            .HasColumnName("EMAILADDR");

        builder.Property(e => e.CheckIn)
            .HasColumnName("CHECKIN")
            .HasColumnType("DATE");

        builder.Property(e => e.CheckOut)
            .HasColumnName("CHECKOUT")
            .HasColumnType("DATE");

        builder.Property(e => e.AddOn)
            .HasColumnName("ADDON")
            .HasColumnType("DATE");

        builder.Property(e => e.EmailSent)
            .HasColumnName("EMAILSENT")
            .HasConversion<int>();
    }
}
