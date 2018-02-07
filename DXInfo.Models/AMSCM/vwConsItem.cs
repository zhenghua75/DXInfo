using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class vwConsItem
    {
        public Int64 iSerial { get; set; }
        public string vcGoodsID { get; set; }
        public string vcCardID { get; set; }
        public decimal nPrice { get; set; }
        public int iCount { get; set; }
        public decimal nTRate { get; set; }
        public decimal nFee { get; set; }
        public string vcComments { get; set; }
        public string cFlag { get; set; }
        public DateTime dtConsDate { get; set; }
        public string vcOperName { get; set; }
        public string vcDeptID { get; set; }
    }
}
