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
    
    
    public class tbSignListConfiguration : EntityTypeConfiguration<tbSignList>
    {
        
        public tbSignListConfiguration()
        {
            this.HasKey(k => new { k.vcSignDate,k.vcCardID,k.vcClass });
            this.Property(o => o.vcSignDate).IsRequired();
            this.Property(o => o.vcSignDate).HasMaxLength(10);
            this.Property(o => o.vcCardID).IsRequired();
            this.Property(o => o.vcCardID).HasMaxLength(10);
            this.Property(o => o.vcClass).IsRequired();
            this.Property(o => o.vcClass).HasMaxLength(20);
            this.Property(o => o.vcEmpName).HasMaxLength(30);
            this.Property(o => o.vcDept).HasMaxLength(20);
            this.Property(o => o.vcOfficer).HasMaxLength(20);
            this.Property(o => o.vcSignState).HasMaxLength(10);
            this.Property(o => o.vcSignResult).HasMaxLength(50);
            this.Property(o => o.vcComments).HasMaxLength(150);
        }
    }
}
