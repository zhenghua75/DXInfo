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
    
    
    public class tbRepAssFillConfiguration : EntityTypeConfiguration<tbRepAssFill>
    {
        
        public tbRepAssFillConfiguration()
        {
            this.HasKey(k => new { k.vcDeptID,k.vcDate,k.vcAssBelongDept });
            this.Property(o => o.vcDeptID).IsRequired();
            this.Property(o => o.vcDeptID).HasMaxLength(5);
            this.Property(o => o.vcDate).IsRequired();
            this.Property(o => o.vcDate).HasMaxLength(8);
            this.Property(o => o.AssCount).IsRequired();
            this.Property(o => o.FillFee).HasPrecision(38,2);
            this.Property(o => o.FillFee).IsRequired();
            this.Property(o => o.PromFee).HasPrecision(38,2);
            this.Property(o => o.PromFee).IsRequired();
            this.Property(o => o.FillCount).IsRequired();
            this.Property(o => o.vcAssBelongDept).IsRequired();
            this.Property(o => o.vcAssBelongDept).HasMaxLength(5);
        }
    }
}
