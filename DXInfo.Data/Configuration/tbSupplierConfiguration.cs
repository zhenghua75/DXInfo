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
    
    
    public class tbSupplierConfiguration : EntityTypeConfiguration<tbSupplier>
    {
        
        public tbSupplierConfiguration()
        {
            this.HasKey(k => k.cnvcCode);
            this.Property(o => o.cnvcCode).IsRequired();
            this.Property(o => o.cnvcCode).IsUnicode();
            this.Property(o => o.cnvcCode).HasMaxLength(50);
            this.Property(o => o.cnvcName).IsRequired();
            this.Property(o => o.cnvcName).IsUnicode();
            this.Property(o => o.cnvcName).HasMaxLength(200);
            this.Property(o => o.cnvcAddress).IsUnicode();
            this.Property(o => o.cnvcAddress).HasMaxLength(200);
            this.Property(o => o.cnvcPostCode).IsUnicode();
            this.Property(o => o.cnvcPostCode).HasMaxLength(50);
            this.Property(o => o.cnvcPhone).IsUnicode();
            this.Property(o => o.cnvcPhone).HasMaxLength(50);
            this.Property(o => o.cnvcFax).IsUnicode();
            this.Property(o => o.cnvcFax).HasMaxLength(50);
            this.Property(o => o.cnvcEmail).IsUnicode();
            this.Property(o => o.cnvcEmail).HasMaxLength(50);
            this.Property(o => o.cnvcLinkName).IsUnicode();
            this.Property(o => o.cnvcLinkName).HasMaxLength(50);
            this.Property(o => o.cnvcCreateOper).IsRequired();
            this.Property(o => o.cnvcCreateOper).IsUnicode();
            this.Property(o => o.cnvcCreateOper).HasMaxLength(200);
            this.Property(o => o.cndCreateDate).IsRequired();
            this.Property(o => o.cnbInvalid).IsRequired();
        }
    }
}
