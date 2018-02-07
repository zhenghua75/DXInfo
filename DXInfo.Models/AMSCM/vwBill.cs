using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class vwBill
    {
        public Int64 iSerial { get; set; }
        public string vcCardID { get; set; }
        public decimal nTRate { get; set; }
        public decimal nFee { get; set; }
        public decimal nPay { get; set; }
        public decimal nBalance { get; set; }
        public int iIgValue { get; set; }
        public string vcConsType { get; set; }
        public string vcOperName { get; set; }
        public DateTime dtConsDate { get; set; }
        public string vcDeptID { get; set; }
    }
}
