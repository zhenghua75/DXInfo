using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DXInfo.Models;

namespace DXInfo.Data.Configuration
{
    public class vwFillFeeConfiguration : EntityTypeConfiguration<vwFillFee>
    {
        public vwFillFeeConfiguration()
        {
            this.HasKey(k => new { k.iSerial, k.vcDeptID });
        }
    }
}
