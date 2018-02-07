using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class VouchsModels
    {
    }
    public class VouchsModel
    {
        public Guid? Id{get;set;}
        public Guid? RdId { get; set; }
        public Guid? SVId { get; set; }
        public Guid? TVId { get; set; }
        public Guid? CVId { get; set; }
        public Guid? ALVId { get; set; }
        public Guid? MVId { get; set; }
        public Guid? InvId { get; set; }
        //public Guid MainUnit { get; set; }
        //public Guid STUnit { get; set; }
        //public Decimal ExchRate { get; set; }
        //public Decimal LastBanaceQuantity { get; set; }
        //public Decimal LastBalanceLocatorQuantity { get; set; }
        //public Decimal Quantity { get; set; }
        //public Decimal BalanceQuantity { get; set; }
        //public Decimal BalanceLocatorQuantity { get; set; }
        //public Decimal LastBalanceNum { get; set; }
        //public Decimal LastBalanceLocatorNum { get; set; }
        public Decimal? Num { get; set; }
        public Decimal? CNum { get; set; }
        public Decimal? AddInNum { get; set; }
        public Decimal? AddOutNum { get; set; }
        //public Decimal BalanceNum { get; set; }
        //public Decimal BalanceLocatorNum { get; set; }
        public Decimal? Price { get; set; }
        //public Decimal LastBalanceAmount { get; set; }
        //public Decimal LastBalanceLocatorAmount { get; set; }
        public Decimal? Amount { get; set; }
        //public Decimal BalanceAmount { get; set; }
        //public Decimal BalanceLocatorAmount { get; set; }
        public String Batch { get; set; }
        public DateTime? MadeDate { get; set; }
        public Int32? ShelfLife { get; set; }
        public Int32? ShelfLifeType { get; set; }
        public DateTime? InvalidDate { get; set; }
        public Guid? Locator { get; set; }
        //public Decimal DueQuantity { get; set; }
        public Decimal? DueNum { get; set; }
        //public Decimal DueAmount { get; set; }
        public String Memo { get; set; }
        //public Guid? SourceId { get; set; }
    }
    public class VouchsGridModel : VouchModel
    {
        public List<VouchsModel> lVouchs { get; set; }
        public StockManageJQGrid VouchsGrid { get; set; }
        public VouchsGridModel(string vt)
        {
            VouchsGrid = new StockManageJQGrid(vt);
        }
    }
}