namespace DXInfo.Data.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity.ModelConfiguration;
    using DXInfo.Models;
    using System.ComponentModel.DataAnnotations.Schema;


    public class ReceiptHisConfiguration : EntityTypeConfiguration<ReceiptHis>
    {

        public ReceiptHisConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.LinkId).IsRequired();
            this.Property(o => o.Member).IsRequired();
            this.Property(o => o.Sn).HasMaxLength(200);
            this.Property(o => o.Content).IsUnicode();
            this.Property(o => o.Content).HasMaxLength(2000);
            this.Property(o => o.Status).IsRequired();
            this.Property(o => o.Comment).IsUnicode();
            this.Property(o => o.Comment).HasMaxLength(200);
            this.Property(o => o.UserId).IsRequired();
            this.Property(o => o.DeptId).IsRequired();
            this.Property(o => o.CreateDate).IsRequired();
            this.Property(o => o.ReceiptType).IsRequired();
        }
    }
}
