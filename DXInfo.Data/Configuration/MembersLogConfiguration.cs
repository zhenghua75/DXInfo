//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXInfo.Data.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity.ModelConfiguration;
    using DXInfo.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    
    
    public class MembersLogConfiguration : EntityTypeConfiguration<MembersLog>
    {
        
        public MembersLogConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.MemberId).IsRequired();
            this.Property(o => o.MemberName).IsUnicode();
            this.Property(o => o.MemberName).HasMaxLength(200);
            this.Property(o => o.IdCard).IsUnicode();
            this.Property(o => o.IdCard).HasMaxLength(200);
            this.Property(o => o.LinkPhone).IsUnicode();
            this.Property(o => o.LinkPhone).HasMaxLength(200);
            this.Property(o => o.LinkAddress).IsUnicode();
            this.Property(o => o.LinkAddress).HasMaxLength(200);
            this.Property(o => o.Email).IsUnicode();
            this.Property(o => o.Email).HasMaxLength(200);
            this.Property(o => o.Comments).IsUnicode();
            this.Property(o => o.Comments).HasMaxLength(200);
            this.Property(o => o.UserId).IsRequired();
            this.Property(o => o.DeptId).IsRequired();
            this.Property(o => o.CreateDate).IsRequired();
            this.Property(o => o.Sex).IsUnicode();
            this.Property(o => o.Sex).HasMaxLength(50);
        }
    }
}
