using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Models;
using System.Data.Entity.ModelConfiguration;

namespace DXInfo.Data.Configuration
{
    public class vwConsItemConfiguration : EntityTypeConfiguration<vwConsItem>
    {
        public vwConsItemConfiguration()
        {
            this.HasKey(k => new { k.iSerial, k.vcGoodsID });
        }
    }
}
