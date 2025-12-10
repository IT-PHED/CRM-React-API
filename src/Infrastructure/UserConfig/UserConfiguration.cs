using System;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.UserConfig;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .HasColumnName("USERID")
            .IsRequired();

        builder.Property(u => u.UserName)
            .HasColumnName("USERNAME")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.UserPassword)
            .HasColumnName("USERPASSWORD")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.GroupId)
            .HasColumnName("GROUPID")
            .IsRequired();

        builder.Property(u => u.EmpId)
            .HasColumnName("EMPID");

        builder.Property(u => u.Access_Auth)
            .HasColumnName("ACCESS_AUTH")
            .HasMaxLength(2);

        builder.Property(u => u.AccessCa)
            .HasColumnName("ACCESS_CA")
            .HasMaxLength(1);

        builder.Property(u => u.Area_Type)
            .HasColumnName("AREA_TYPE")
            .HasMaxLength(12);

        builder.Property(u => u.Region_Id)
            .HasColumnName("REGION_ID")
            .HasMaxLength(20) // Assuming conversion from NUMBER(10)
            .IsRequired();

        builder.Property(u => u.Area_Code)
            .HasColumnName("AREA_CODE")
            .HasMaxLength(10);

        builder.Property(u => u.PhoneNo)
            .HasColumnName("PHONENO")
            .HasMaxLength(12);

        builder.Property(u => u.EmailId)
            .HasColumnName("EMAILID")
            .HasMaxLength(100);

        builder.Property(u => u.Status)
            .HasColumnName("STATUS")
            .HasMaxLength(1);

        builder.Property(u => u.CreatedBy)
            .HasColumnName("CREATEDBY")
            .HasMaxLength(50);

        builder.Property(u => u.CreatedDate)
            .HasColumnName("CREATEDDATE")
            .HasMaxLength(20); // Storing as string from DATE

        builder.Property(u => u.ModifiedBy)
            .HasColumnName("MODIFIEDBY")
            .HasMaxLength(50);

        builder.Property(u => u.ModifiedDate)
            .HasColumnName("MODIFIEDDATE");

        builder.Property(u => u.LstPswdUpdatedDate)
            .HasColumnName("LSTPSWDUPDATEDDATE");

        builder.Property(u => u.LastIpAddress)
            .HasColumnName("LSTIPADDRESS")
            .HasMaxLength(100);

        builder.Property(u => u.LstLoginDate)
            .HasColumnName("LSTLOGINDATE")
            .HasMaxLength(20); // Storing as string from DATE

        builder.Property(u => u.LstLogIpAddress)
            .HasColumnName("LSTLOGIPADDRESS")
            .HasMaxLength(100);

        builder.Property(u => u.UserFName)
            .HasColumnName("USERFNAME")
            .HasMaxLength(50);

        builder.Property(u => u.smf_role_id)
            .HasColumnName("SMF_ROLE_ID");

        builder.Property(u => u.TeamId)
            .HasColumnName("TEAMID")
            .HasMaxLength(20); // Assuming conversion from NUMBER

        builder.Property(u => u.DepartmentName)
            .HasColumnName("DEPARTMENTNAME")
            .HasMaxLength(255);

        builder.Property(u => u.DEPARTMENT_ID)
            .HasColumnName("DEPARTMENT_ID");

        builder.Property(u => u.is_verified)
            .HasColumnName("IS_VERIFIED")
            .HasMaxLength(1)
            .HasConversion(
                v => v == "1" ? "1" : "0",
                v => v
            );
    }
}
