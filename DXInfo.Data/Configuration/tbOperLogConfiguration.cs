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
    
    
    public class tbOperLogConfiguration : EntityTypeConfiguration<tbOperLog>
    {
        
        public tbOperLogConfiguration()
        {
            this.HasKey(k => k.cnnOperSerialNo);
            this.Property(o => o.cnnOperSerialNo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.cnnOperSerialNo).IsRequired();
            this.Property(o => o.cnvcOperType).HasMaxLength(20);
            this.Property(o => o.cnvcOperID).HasMaxLength(50);
            this.Property(o => o.cnvcDeptID).HasMaxLength(50);
        }
    }
}
