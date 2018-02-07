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
    
    
    public class CardsConfiguration : EntityTypeConfiguration<Cards>
    {
        
        public CardsConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.CardNo).IsRequired();
            this.Property(o => o.CardNo).IsUnicode();
            this.Property(o => o.CardNo).HasMaxLength(50);
            this.Property(o => o.SecondCardNo).IsUnicode();
            this.Property(o => o.SecondCardNo).HasMaxLength(50);
            this.Property(o => o.Member).IsRequired();
            this.Property(o => o.CardType).IsRequired();
            this.Property(o => o.CardLevel).IsRequired();
            this.Property(o => o.Balance).HasPrecision(24,2);
            this.Property(o => o.Balance).IsRequired();
            this.Property(o => o.CreateDate).IsRequired();
            this.Property(o => o.LossUserId).IsRequired();
            this.Property(o => o.FoundUserId).IsRequired();
            this.Property(o => o.UserId).IsRequired();
            this.Property(o => o.DeptId).IsRequired();
            this.Property(o => o.Status).IsRequired();
            this.Property(o => o.Comment).IsUnicode();
            this.Property(o => o.Comment).HasMaxLength(50);
            this.Property(o => o.AddReason).IsUnicode();
            this.Property(o => o.AddReason).HasMaxLength(50);
            this.Property(o => o.CardPwd).IsUnicode();
            this.Property(o => o.CardPwd).HasMaxLength(50);
        }
    }
}
