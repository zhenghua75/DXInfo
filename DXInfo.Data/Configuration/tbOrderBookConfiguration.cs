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
    
    
    public class tbOrderBookConfiguration : EntityTypeConfiguration<tbOrderBook>
    {
        
        public tbOrderBookConfiguration()
        {
            this.HasKey(k => k.cnnOrderSerialNo);
            this.Property(o => o.cnnOrderSerialNo).IsRequired();
            this.Property(o => o.cnvcOrderDeptID).HasMaxLength(20);
            this.Property(o => o.cnvcProduceDeptID).HasMaxLength(20);
            this.Property(o => o.cnvcOrderType).HasMaxLength(20);
            this.Property(o => o.cnvcOrderOperID).HasMaxLength(20);
            this.Property(o => o.cnvcLinkPhone).HasMaxLength(20);
            this.Property(o => o.cnvcOrderState).HasMaxLength(1);
        }
    }
}