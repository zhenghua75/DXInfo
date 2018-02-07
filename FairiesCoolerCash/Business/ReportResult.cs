using System;

namespace FairiesCoolerCash.Business
{
    public class ReportResult
    {
        public Guid Id { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        public string InventoryName { get; set; }
        public Guid? PayType { get; set; }
        public string PayTypeName { get; set; }
        public int Cup { get; set; }
        public string CupName { get; set; }
        public int ConsumeType { get; set; }
        public string ConsumeTypeName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeptName { get; set; }
        public decimal Sum { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string OperatorsOnDuty { get; set; }
    }
}
