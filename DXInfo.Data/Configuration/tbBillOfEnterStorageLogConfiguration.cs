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
    
    
    public class tbBillOfEnterStorageLogConfiguration : EntityTypeConfiguration<tbBillOfEnterStorageLog>
    {
        
        public tbBillOfEnterStorageLogConfiguration()
        {
            this.HasKey(k => new { k.cnnEnterSerialNo,k.cnvcProviderCode });
            this.Property(o => o.cnnEnterSerialNo).IsRequired();
            this.Property(o => o.cnvcProviderCode).IsRequired();
            this.Property(o => o.cnvcProviderCode).HasMaxLength(20);
            this.Property(o => o.cnvcDeliverMan).HasMaxLength(20);
            this.Property(o => o.cnvcValidateOperID).HasMaxLength(20);
            this.Property(o => o.cnvcSafeOperID).HasMaxLength(20);
            this.Property(o => o.cnvcStorageOperID).HasMaxLength(20);
            this.Property(o => o.cnvcBillOperID).HasMaxLength(20);
            this.Property(o => o.cnvcEnterType).HasMaxLength(20);
        }
    }
}
