using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class VouchModels
    {
    }
    public class VouchModel
    {
        [Display(Name = "入库单号"), Required()]
        public string Code { get; set; }

        [Display(Name = "入库日期"), Required()]
        public DateTime? RdDate { get; set; }

        [Display(Name = "仓库"), Required()]
        public Guid? WhId { get; set; }

        [Display(Name = "到货单号")]
        public string ARVCode { get; set; }

        [Display(Name = "供货单位")]
        public Guid? VenId { get; set; }
        [Display(Name = "业务员"), Required()]
        public Guid? Salesman { get; set; }
        [Display(Name = "到货日期")]
        public DateTime? ARVDate { get; set; }
        [Display(Name = "业务类型"), Required()]
        public string BusType { get; set; }
        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }
        [Display(Name = "备注")]
        public string Memo { get; set; }

        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public string PTCode { get; set; }
        public string STCode { get; set; }
        public string VouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsShelfLife { get; set; }
        public bool IsLocator { get; set; }
        public bool InvInit { get; set; }
        public int RdFlag { get; set; }

        public DateTime? MakeTime { get; set; }

        public int InvType { get; set; }
        public string RdCode { get; set; }

        [Display(Name = "日期"), Required()]
        public DateTime? SVDate { get; set; }

        [Display(Name = "转出仓库"), Required()]
        public Guid? OutWhId { get; set; }

        [Display(Name = "转入仓库"), Required()]
        public Guid? InWhId { get; set; }

        [Display(Name = "单据日期"), Required()]
        public DateTime? TVDate { get; set; }

        [Display(Name = "盘点日期"), Required()]
        public DateTime? CVDate { get; set; }

        [Display(Name = "日期"), Required()]
        public DateTime? ALVDate { get; set; }

        [Display(Name = "日期"), Required()]
        public DateTime? MVDate { get; set; }

        public Guid DeptId { get; set; }
        public Guid Maker { get; set; }
        public Guid? Verifier { get; set; }
        public DateTime? VerifyTime { get; set; }
        public Guid? OrganizationId { get; set; }
    }

    public class VouchGridModel:VouchModel
    {
        public StockManageSearchJQGrid VouchGrid { get; set; }
        public VouchGridModel(string vt)
        {
            VouchGrid = new StockManageSearchJQGrid(vt);
        }
    }
}