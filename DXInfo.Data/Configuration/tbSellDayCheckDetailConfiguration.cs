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
    
    
    public class tbSellDayCheckDetailConfiguration : EntityTypeConfiguration<tbSellDayCheckDetail>
    {
        
        public tbSellDayCheckDetailConfiguration()
        {
            this.HasKey(k => new { k.cnnCheckSerialNo,k.cnvcProductCode });
            this.Property(o => o.cnnCheckSerialNo).IsRequired();
            this.Property(o => o.cnvcProductCode).IsRequired();
            this.Property(o => o.cnvcProductCode).HasMaxLength(20);
            this.Property(o => o.cnvcProductName).HasMaxLength(20);
            this.Property(o => o.cnnProductPrice).HasPrecision(10,4);
            this.Property(o => o.cnvcUnit).HasMaxLength(20);
            this.Property(o => o.cnnOriginalStorage).HasPrecision(10,2);
            this.Property(o => o.cnnOrderCount).HasPrecision(10,2);
            this.Property(o => o.cnnMoveOutCount).HasPrecision(10,2);
            this.Property(o => o.cnnMoveInCount).HasPrecision(10,2);
            this.Property(o => o.cnnLoseCount).HasPrecision(10,2);
            this.Property(o => o.cnnFreeCount).HasPrecision(10,2);
            this.Property(o => o.cnnUseCount).HasPrecision(10,2);
            this.Property(o => o.cnnSellCount).HasPrecision(10,2);
            this.Property(o => o.cnnSystemCount).HasPrecision(10,2);
            this.Property(o => o.cnnRealCount).HasPrecision(10,2);
            this.Property(o => o.cnnDifferentCount).HasPrecision(10,2);
            this.Property(o => o.cnnDifferentSum).HasPrecision(10,2);
            this.Property(o => o.cnnReceiveCount).HasPrecision(10,2);
        }
    }
}