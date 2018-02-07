using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ynhnTransportManage.Models
{
    public class OutFactoryModels
    {
        [Required]
        [Display(Name = "进厂ID")]
        [DataType(DataType.Text)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "卡号字段是必需的")]
        [Display(Name = "卡号")]
        [DataType(DataType.Text)]
        public string CardNo { get; set; }

        [Required]
        [Display(Name = "卡")]
        public Guid Card { get; set; }

        [Required]
        [Display(Name = "车量")]
        public Guid Vehicle { get; set; }

        //[Required]
        [Display(Name = "车牌号")]
        [DataType(DataType.Text)]
        public string PlateNo { get; set; }

        //[Required]
        [Display(Name = "品牌型号")]
        [DataType(DataType.Text)]
        public string BrandModel { get; set; }

        //[Required]
        [Display(Name = "发动机号")]
        [DataType(DataType.Text)]
        public string MotorNo { get; set; }

        //[Required]
        [Display(Name = "车主")]
        [DataType(DataType.Text)]
        public string OwnerName { get; set; }

        //[Required]
        [Display(Name = "装车时间")]
        [DataType(DataType.Date)]
        public DateTime? Load_Date { get; set; }

        //[Required]
        [Display(Name = "存货")]
        public string Load_Inventory { get; set; }
        [Display(Name = "计量单位")]
        public string UnitName { get; set; }
        [Display(Name = "规格型号")]
        public string Specs { get; set; }
        //[Required]
        [Display(Name = "装车数量")]
        [DataType(DataType.Text)]
        public decimal? Load_Quantity { get; set; }

        //[Required]
        [Display(Name = "卸货时间")]
        [DataType(DataType.Date)]
        public DateTime? Shipment_Date { get; set; }

        //[Required]
        [Display(Name = "卸货数量")]
        [DataType(DataType.Text)]
        public decimal? Shipment_Quantity { get; set; }

        //[Required]
        [Display(Name = "签收")]
        public string Shipment_CheckUser { get; set; }
        //[Required]
        [Display(Name = "备注")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
    }
}