using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ynhnTransportManage.Models
{
    public class InFactoryModels
    {
        [Required(ErrorMessage = "卡号字段是必需的")]
        [Display(Name = "卡号")]
        [DataType(DataType.Text)]
        public string CardNo { get; set; }

        //[Required]
        [Display(Name = "卡")]
        public Guid Card { get; set; }

        //[Required]
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

        //[Required(ErrorMessage = "车主字段是必需的")]
        [Display(Name = "车主")]
        [DataType(DataType.Text)]
        public string OwnerName { get; set; }

        [Display(Name = "驾驶员")]
        public Guid Driver { get; set; }

        [Display(Name = "结算方式")]
        public Guid BalanceType { get; set; }

        [Display(Name="约定运价")]        
        public decimal? AgreeFreightPrice { get; set; }

        [Display(Name = "实际运价")]
        public decimal? FreightPrice { get; set; }

        [Display(Name = "托运人")]
        public string Shipper { get; set; }

        [Display(Name = "托运人电话")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [RegularExpression(@"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)",ErrorMessage="请输入电话号码")]        
        public string Shipper_Telephone { get; set; }

        [Display(Name = "承运人")]
        public string Carrier { get; set; }

        [Display(Name = "承运人电话")]
        [RegularExpression(@"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)", ErrorMessage = "请输入电话号码")]
        public string Carrier_Telephone { get; set; }

        [Display(Name = "运输路线")]
        public Guid? Lines { get; set; }

        [Display(Name = "里程(公里)")]
        public decimal? Mileage { get; set; }
        
        [Display(Name = "备注")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
    }
}