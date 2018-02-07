using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DXInfo.Models;

namespace DXInfo.Data.Configuration
{
    public class vwBillConfiguration : EntityTypeConfiguration<vwBill>
    {
        public vwBillConfiguration()
        {
            this.HasKey(k => k.iSerial);
        }
    }
}
