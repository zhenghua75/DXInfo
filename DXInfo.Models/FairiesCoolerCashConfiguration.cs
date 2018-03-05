using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Models
{
    //[NotMapped]
    public class FairiesCoolerCashConfiguration
    {
        public bool IsOpen { get; set; }
        public bool IsStickerPrint { get; set; }
        public bool IsTicket1 { get; set; }
        public bool IsTicket2 { get; set; }
        public bool IsTicket3 { get; set; }
        public bool IsThree { get; set; }
        public bool IsPrintOrder { get; set; }
    }
}
