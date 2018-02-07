using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DXInfo.Models;

namespace DXInfo.Data.Configuration
{
    public class vwBusiLogConfiguration : EntityTypeConfiguration<vwBusiLog>
    {
        public vwBusiLogConfiguration()
        {
            this.HasKey(k => new { k.iSerial, k.vcDeptID });
        }
    }
}
