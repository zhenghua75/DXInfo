using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DXInfo.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Data.Configuration
{
    public class SaleCheckConfiguration : EntityTypeConfiguration<SaleCheck>
    {
        public SaleCheckConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.vcDeptId).IsRequired();
            this.Property(o => o.vcDeptId).HasMaxLength(5);
            this.Property(o => o.CheckDate).IsRequired();
            this.Property(o => o.vcGoodsId).IsRequired();
            this.Property(o => o.vcGoodsId).HasMaxLength(10);
            this.Property(o => o.Quantity).IsRequired();
            this.Property(o => o.vcOperId).IsRequired();
            this.Property(o => o.vcOperId).HasMaxLength(10);
            this.Property(o => o.CreateDate).IsRequired();            
        }
    }
}
