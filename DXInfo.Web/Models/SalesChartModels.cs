using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DXInfo.Web.Models
{
    public class SalesChartModels
    {
    }

    public class SalesChartModel
    {
        [Display(Name = "门店")]
        public string dept { get; set; }
        [Display(Name = "月份")]
        public string month { get; set; }
    }
}