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
    
    
    public class tbBillConfiguration : EntityTypeConfiguration<tbBill>
    {
        
        public tbBillConfiguration()
        {
            this.HasKey(k => k.iSerial);
            this.Property(o => o.iSerial).IsRequired();
            this.Property(o => o.vcCardID).IsRequired();
            this.Property(o => o.vcCardID).HasMaxLength(10);
            this.Property(o => o.nTRate).HasPrecision(8,2);
            this.Property(o => o.nFee).HasPrecision(10,2);
            this.Property(o => o.nPay).HasPrecision(10,2);
            this.Property(o => o.nBalance).HasPrecision(10,2);
            this.Property(o => o.vcConsType).HasMaxLength(10);
            this.Property(o => o.vcOperName).HasMaxLength(10);
            this.Property(o => o.vcDeptID).HasMaxLength(5);
        }
    }
}
