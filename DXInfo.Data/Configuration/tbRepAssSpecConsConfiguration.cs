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
    
    
    public class tbRepAssSpecConsConfiguration : EntityTypeConfiguration<tbRepAssSpecCons>
    {
        
        public tbRepAssSpecConsConfiguration()
        {
            this.HasKey(k => new { k.vcDeptID,k.vcDate,k.vcConsType });
            this.Property(o => o.vcDeptID).IsRequired();
            this.Property(o => o.vcDeptID).HasMaxLength(5);
            this.Property(o => o.vcDate).IsRequired();
            this.Property(o => o.vcDate).HasMaxLength(8);
            this.Property(o => o.SpecConsTimes).IsRequired();
            this.Property(o => o.GoodsCount).IsRequired();
            this.Property(o => o.vcConsType).IsRequired();
            this.Property(o => o.vcConsType).HasMaxLength(20);
        }
    }
}
