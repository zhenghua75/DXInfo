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
    
    
    public class tbEmployeeConfiguration : EntityTypeConfiguration<tbEmployee>
    {
        
        public tbEmployeeConfiguration()
        {
            this.HasKey(k => new { k.vcCardID,k.vcFlag });
            this.Property(o => o.vcCardID).IsRequired();
            this.Property(o => o.vcCardID).HasMaxLength(10);
            this.Property(o => o.vcEmpName).IsRequired();
            this.Property(o => o.vcEmpName).HasMaxLength(30);
            this.Property(o => o.vcSex).HasMaxLength(5);
            this.Property(o => o.vcEmpNbr).HasMaxLength(20);
            this.Property(o => o.vcDegree).HasMaxLength(2);
            this.Property(o => o.vcLinkPhone).HasMaxLength(25);
            this.Property(o => o.vcAddress).HasMaxLength(100);
            this.Property(o => o.vcPwd).HasMaxLength(20);
            this.Property(o => o.vcOfficer).HasMaxLength(5);
            this.Property(o => o.vcDeptID).IsRequired();
            this.Property(o => o.vcDeptID).HasMaxLength(5);
            this.Property(o => o.vcFlag).IsRequired();
            this.Property(o => o.vcFlag).HasMaxLength(2);
            this.Property(o => o.vcComments).HasMaxLength(256);
        }
    }
}
