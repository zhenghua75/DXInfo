using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DXInfo.WcfRestService
{
    public class ImgInfo
    {
        public string FileName { get; set; }
        public string ModifyDate { get; set; }
    }
    public class OrderBookDeskInfo
    {
        public Guid OrderBookId { get; set; }
        public Guid OrderBookDeskId { get; set; }
        public DateTime BookBeginDate { get; set; }
        public DateTime BookEndDate { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public string Customer { get; set; }
        public string LinkPhone { get; set; }
        public int Quantity { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string DeskNo { get; set; }
        public Guid DeskId { get; set; }
        //public int Status { get; set; }
    }
    public class PackageInfo
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Guid InventoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public string OptionalGroup { get; set; }
        public bool IsOptional { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderDeskInfo
    {
        public Guid DeskId { get; set; }
        public string DeskNo { get; set; }

        public Guid? OrderDishId { get; set; }
        public Guid? OrderDeskId { get; set; }                
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class OrderMenuInfo
    {
        public Guid? OrderMenuId { get; set; }
        public Guid InvId { get; set; }
        public string InvCode { get; set; }
        public string InvName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public string EnglishName { get; set; }
        public bool IsPackage { get; set; }
        public Guid? PackageId { get; set; }
        public int PackageSn { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDelete { get; set; }
    }
    public class OrderInfo
    {
        public OrderInfo()
        {
            OrderDesk = new OrderDeskInfo();
            lOrderMenu = new List<OrderMenuInfo>();
            lLackMenu = new List<DXInfo.Models.MenuStatus>();
        }
        public OrderDeskInfo OrderDesk { get; set; }
        public List<OrderMenuInfo> lOrderMenu { get; set; }
        public List<DXInfo.Models.MenuStatus> lLackMenu { get; set; }
    }
}
