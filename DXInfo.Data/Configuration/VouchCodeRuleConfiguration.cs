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
    
    
    public class VouchCodeRuleConfiguration : EntityTypeConfiguration<VouchCodeRule>
    {
        
        public VouchCodeRuleConfiguration()
        {
            this.HasKey(k => k.VouchType);
            this.Property(o => o.VouchType).IsRequired();
            this.Property(o => o.VouchType).HasMaxLength(50);
            this.Property(o => o.Prefix).IsRequired();
            this.Property(o => o.Prefix).HasMaxLength(10);
            this.Property(o => o.Middle).IsRequired();
            this.Property(o => o.Middle).HasMaxLength(20);
            this.Property(o => o.Suffix).IsRequired();
            this.Property(o => o.Suffix).HasMaxLength(20);
        }
    }
}