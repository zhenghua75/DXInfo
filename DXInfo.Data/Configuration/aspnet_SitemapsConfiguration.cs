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
    
    
    public class aspnet_SitemapsConfiguration : EntityTypeConfiguration<aspnet_Sitemaps>
    {
        
        public aspnet_SitemapsConfiguration()
        {
            this.HasKey(k => k.Code);
            this.Property(o => o.Code).IsRequired();
            this.Property(o => o.Code).IsUnicode();
            this.Property(o => o.Code).HasMaxLength(256);
            this.Property(o => o.Name).IsUnicode();
            this.Property(o => o.Name).HasMaxLength(256);
            this.Property(o => o.Title).IsRequired();
            this.Property(o => o.Title).IsUnicode();
            this.Property(o => o.Title).HasMaxLength(256);
            this.Property(o => o.Description).IsRequired();
            this.Property(o => o.Description).IsUnicode();
            this.Property(o => o.Description).HasMaxLength(256);
            this.Property(o => o.Controller).IsUnicode();
            this.Property(o => o.Controller).HasMaxLength(256);
            this.Property(o => o.Action).IsUnicode();
            this.Property(o => o.Action).HasMaxLength(256);
            this.Property(o => o.ParaId).IsUnicode();
            this.Property(o => o.ParaId).HasMaxLength(256);
            this.Property(o => o.Url).IsUnicode();
            this.Property(o => o.Url).HasMaxLength(256);
            this.Property(o => o.ParentCode).IsRequired();
            this.Property(o => o.ParentCode).IsUnicode();
            this.Property(o => o.ParentCode).HasMaxLength(256);
            this.Property(o => o.IsAuthorize).IsRequired();
            this.Property(o => o.IsMenu).IsRequired();
            this.Property(o => o.IsClient).IsRequired();
            this.Property(o => o.Sort).IsRequired();
        }
    }
}