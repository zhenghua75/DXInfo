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
    
    
    public class tbBillOfReceiveLogConfiguration : EntityTypeConfiguration<tbBillOfReceiveLog>
    {
        
        public tbBillOfReceiveLogConfiguration()
        {
            this.HasKey(k => k.cnnReceiveSerialNo);
            this.Property(o => o.cnnReceiveSerialNo).IsRequired();
            this.Property(o => o.cnvcReceiveDeptID).HasMaxLength(20);
            this.Property(o => o.cnvcGroup).HasMaxLength(20);
            this.Property(o => o.cnvcClass).HasMaxLength(20);
            this.Property(o => o.cnvcMaterialInchargeOperID).HasMaxLength(20);
            this.Property(o => o.cnvcStorageInchargeOperID).HasMaxLength(20);
            this.Property(o => o.cnvcSendOperID).HasMaxLength(20);
            this.Property(o => o.cnvcReceiveType).HasMaxLength(20);
            this.Property(o => o.cnvcBillState).HasMaxLength(20);
        }
    }
}
