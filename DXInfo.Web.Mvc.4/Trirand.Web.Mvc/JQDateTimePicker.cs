using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trirand.Web.Mvc
{
    public class JQDateTimePicker
    {
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string ID { get; set; }
        public JQDateTimePicker()
        {
            this.DateFormat = "yy-mm-dd";
            this.TimeFormat = "hh:mm";
        }
    }

    public class JQTimePicker
    {
        public string TimeFormat { get; set; }
        public string ID { get; set; }
        public JQTimePicker()
        {
            this.TimeFormat = "HH:mm";
        }
    }
}
