using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class ClientRetailOutStock
    {
        public Guid UserId { get; set; }
        public Guid DeptId { get; set; }
        public Guid WhId { get; set; }
        public List<ClientRetailOutStockDetail> Detail { get; set; }
    }
    public class ClientRetailOutStockDetail
    {
        public Guid InvId { get; set; }
        public decimal Num { get; set; }
        public decimal Price { get; set; }
        public string Batch { get; set; }
    }
}
