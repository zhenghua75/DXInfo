using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class vwBusiLog
    {
        public Int64 iSerial { get; set; }
        public string vcCardID { get; set; }
        public string vcOperType { get; set; }
        public string vcOperName { get; set; }
        public DateTime dtOperDate { get; set; }
        public string vcComments { get; set; }
        public string vcDeptID { get; set; }
    }
}
