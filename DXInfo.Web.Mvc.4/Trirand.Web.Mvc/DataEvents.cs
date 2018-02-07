using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trirand.Web.Mvc
{
    public enum DataEventType
    {
        No,
        Click,
        Change,
        KeyPress,
        KeyUp
    }
    public class DataEvent
    {
        public DataEventType Type { get; set; }
        public string Function { get; set; }
        public DataEvent()
        {
            this.Type = DataEventType.No;
            this.Function = "";
        }
    }
}
