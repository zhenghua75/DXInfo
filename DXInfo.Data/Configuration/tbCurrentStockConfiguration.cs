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
    
    
    public class tbCurrentStockConfiguration : EntityTypeConfiguration<tbCurrentStock>
    {
        
        public tbCurrentStockConfiguration()
        {
            this.HasKey(k => k.cnnAutoID);
            this.Property(o => o.cnnAutoID).IsRequired();
            this.Property(o => o.cnvcWhCode).IsRequired();
            this.Property(o => o.cnvcWhCode).HasMaxLength(40);
            this.Property(o => o.cnvcInvCode).IsRequired();
            this.Property(o => o.cnvcInvCode).HasMaxLength(20);
            this.Property(o => o.cnnQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnOutQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnInQuantity).HasPrecision(18,4);
            this.Property(o => o.cnvcStopFlag).HasMaxLength(5);
            this.Property(o => o.cnnTransInQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnTransOutQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnPlanQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnDisableQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnAvaQuantity).HasPrecision(18,4);
            this.Property(o => o.cnvcMassUnit).HasMaxLength(10);
            this.Property(o => o.cnnStopQuantity).HasPrecision(18,4);
            this.Property(o => o.cnnStopNum).HasPrecision(18,2);
        }
    }
}
