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
    
    
    public class tbMonthlyBalanceConfiguration : EntityTypeConfiguration<tbMonthlyBalance>
    {
        
        public tbMonthlyBalanceConfiguration()
        {
            this.HasKey(k => new { k.cnnYear,k.cnnMonth });
            this.Property(o => o.cnnYear).IsRequired();
            this.Property(o => o.cnnMonth).IsRequired();
            this.Property(o => o.cnbIsBalance).IsRequired();
            this.Property(o => o.cnvcCreater).IsRequired();
            this.Property(o => o.cnvcCreater).IsUnicode();
            this.Property(o => o.cnvcCreater).HasMaxLength(50);
            this.Property(o => o.cnvcCreaterName).IsRequired();
            this.Property(o => o.cnvcCreaterName).IsUnicode();
            this.Property(o => o.cnvcCreaterName).HasMaxLength(200);
            this.Property(o => o.cndCreateDate).IsRequired();
            this.Property(o => o.cnvcModifier).IsUnicode();
            this.Property(o => o.cnvcModifier).HasMaxLength(50);
            this.Property(o => o.cnvcModifierName).IsUnicode();
            this.Property(o => o.cnvcModifierName).HasMaxLength(50);
            this.Property(o => o.cnvcBalancer).IsUnicode();
            this.Property(o => o.cnvcBalancer).HasMaxLength(50);
            this.Property(o => o.cnvcBalancerName).IsUnicode();
            this.Property(o => o.cnvcBalancerName).HasMaxLength(200);
        }
    }
}
