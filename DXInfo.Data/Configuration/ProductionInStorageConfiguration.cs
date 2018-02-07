using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using DXInfo.Models;

namespace DXInfo.Data.Configuration
{
    public class ProductionInStorageConfiguration : EntityTypeConfiguration<ProductionInStorage>
    {
        public ProductionInStorageConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.vcDeptId).IsRequired();
            this.Property(o => o.vcDeptId).HasMaxLength(5);
            this.Property(o => o.InDate).IsRequired();
            this.Property(o => o.vcGoodsId).IsRequired();
            this.Property(o => o.vcGoodsId).HasMaxLength(10);
            this.Property(o => o.Quantity).IsRequired();
            this.Property(o => o.vcOperId).IsRequired();
            this.Property(o => o.vcOperId).HasMaxLength(10);
            this.Property(o => o.CreateDate).IsRequired();            
        }
    }
}
