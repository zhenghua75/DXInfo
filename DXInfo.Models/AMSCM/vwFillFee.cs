using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class vwFillFee
    {
        public Int64 iSerial { get; set; }
        public string vcCardID { get; set; }
        public decimal nFillFee { get; set; }
        public decimal nFillProm { get; set; }
        public decimal nFeeLast { get; set; }
        public decimal nFeeCur { get; set; }
        public DateTime dtFillDate { get; set; }
        public string vcComments { get; set; }
        public string vcOperName { get; set; }
        public string vcDeptID { get; set; }
    }
}
