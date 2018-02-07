using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DXInfo.Web.Models
{
    public class DailyCashQueryModels
    {
    }
    public class DailyCashQueryGridModel
    {
        //public JQGrid DailyCashQueryGrid { get; set; }
        public DailyCashQueryGridModel()
        {
            //DailyCashQueryGrid = new JQGrid();
        }
    }
    public class DailyCashQueryModel
    {
        [Display(Name = "门店")]
        public string vcDeptId { get; set; }
        [Display(Name = "操作员")]
        public string vcOperName { get; set; }
        [Display(Name = "开始时间")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束时间")]
        public DateTime EndDate { get; set; }
        public List<DailyCashQueryCountResult> CountResult { get; set; }
        public List<DailyCashQueryFeeResult> FeeResult { get; set; }
        public List<string> lOperName { get; set; }
        public List<string> lConsType { get; set; }
    }
    public class DailyCashQueryResult
    {
        public string vcOperName { get; set; }
        public string vcConsType { get; set; }
        public int ConsCount { get; set; }
        public decimal ConsFee { get; set; }
    }
    public class DailyCashQueryCountResult
    {
        public string vcOperName { get; set; }
        public string vcConsType { get; set; }
        public int ConsCount { get; set; }
    }
    public class DailyCashQueryFeeResult
    {
        public string vcOperName { get; set; }
        public string vcConsType { get; set; }
        public decimal ConsFee { get; set; }
    }
}