using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ynhnTransportManage.Models
{
    public class LoadModels
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
        [Display(Name = "车辆")]
        public Guid Vehicle { get; set; }

        //[Required(ErrorMessage = "车牌号字段是必需的")]
        [Display(Name = "车牌号")]
        [DataType(DataType.Text)]
        public string PlateNo { get; set; }

        //[Required(ErrorMessage = "品牌型号字段是必需的")]
        [Display(Name = "品牌型号")]
        [DataType(DataType.Text)]
        public string BrandModel { get; set; }

        //[Required(ErrorMessage = "发动机号字段是必需的")]
        [Display(Name = "发动机号")]
        [DataType(DataType.Text)]
        public string MotorNo { get; set; }

        //[Required]
        [Display(Name = "车主")]
        [DataType(DataType.Text)]
        public string OwnerName { get; set; }

        //[Required]
        [Display(Name = "进厂时间")]
        [DataType(DataType.Date)]
        public DateTime? InFactory_Date { get; set; }

        [Required(ErrorMessage = "存货字段是必需的")]
        [Display(Name = "存货")]
        [DataType(DataType.Date)]
        public Guid Load_Inventory { get; set; }

        [Display(Name="计量单位")]
        public string UnitName { get; set; }
        [Display(Name = "规格型号")]
        public string Specs { get; set; }

        [Required(ErrorMessage = "数量字段是必需的")]
        [Display(Name = "数量")]
        [DataType(DataType.Text)]
        public decimal Load_Quantity { get; set; }

        //[Required]
        [Display(Name = "备注")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
    }
}