namespace DXInfo.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DXInfo.Data.Contracts;
    using DXInfo.Models;
    
    
    public class AMSCMUow : Uow<AMSCMDbContext>, IAMSCMUow
    {
        public IRepository<tbAssignDetail> tbAssignDetail
        {
            get
            {
                return GetRepository<tbAssignDetail>();
            }
        }
        
        public IRepository<tbAssignDetailLog> tbAssignDetailLog
        {
            get
            {
                return GetRepository<tbAssignDetailLog>();
            }
        }
        
        public IRepository<tbAssignLog> tbAssignLog
        {
            get
            {
                return GetRepository<tbAssignLog>();
            }
        }
        
        
        
        
        
        public IRepository<tbBespeakLog> tbBespeakLog
        {
            get
            {
                return GetRepository<tbBespeakLog>();
            }
        }
        
        public IRepository<tbBill> tbBill
        {
            get
            {
                return GetRepository<tbBill>();
            }
        }
        
        public IRepository<tbBillHis> tbBillHis
        {
            get
            {
                return GetRepository<tbBillHis>();
            }
        }
        
        public IRepository<tbBillOfEnterStorageDetail> tbBillOfEnterStorageDetail
        {
            get
            {
                return GetRepository<tbBillOfEnterStorageDetail>();
            }
        }
        
        public IRepository<tbBillOfEnterStorageLog> tbBillOfEnterStorageLog
        {
            get
            {
                return GetRepository<tbBillOfEnterStorageLog>();
            }
        }
        
        public IRepository<tbBillOfMaterials> tbBillOfMaterials
        {
            get
            {
                return GetRepository<tbBillOfMaterials>();
            }
        }
        
        public IRepository<tbBillOfReceiveDetail> tbBillOfReceiveDetail
        {
            get
            {
                return GetRepository<tbBillOfReceiveDetail>();
            }
        }
        
        public IRepository<tbBillOfReceiveLog> tbBillOfReceiveLog
        {
            get
            {
                return GetRepository<tbBillOfReceiveLog>();
            }
        }
        
        
        
        
        
        public IRepository<tbBusiLog> tbBusiLog
        {
            get
            {
                return GetRepository<tbBusiLog>();
            }
        }
        
        public IRepository<tbBusiLogHis> tbBusiLogHis
        {
            get
            {
                return GetRepository<tbBusiLogHis>();
            }
        }
        
        public IRepository<tbBusiLogOther> tbBusiLogOther
        {
            get
            {
                return GetRepository<tbBusiLogOther>();
            }
        }
        
        public IRepository<tbCheckSerial> tbCheckSerial
        {
            get
            {
                return GetRepository<tbCheckSerial>();
            }
        }
        
        public IRepository<tbCheckSerialLog> tbCheckSerialLog
        {
            get
            {
                return GetRepository<tbCheckSerialLog>();
            }
        }
        
        
        public IRepository<tbComputationGroup> tbComputationGroup
        {
            get
            {
                return GetRepository<tbComputationGroup>();
            }
        }
        
        public IRepository<tbComputationUnit> tbComputationUnit
        {
            get
            {
                return GetRepository<tbComputationUnit>();
            }
        }
        
        public IRepository<tbConsItem> tbConsItem
        {
            get
            {
                return GetRepository<tbConsItem>();
            }
        }
        
        public IRepository<tbConsItemHis> tbConsItemHis
        {
            get
            {
                return GetRepository<tbConsItemHis>();
            }
        }
        
        
        
        public IRepository<tbConsSerialNo> tbConsSerialNo
        {
            get
            {
                return GetRepository<tbConsSerialNo>();
            }
        }
        
        public IRepository<tbCurrentStock> tbCurrentStock
        {
            get
            {
                return GetRepository<tbCurrentStock>();
            }
        }
        
        public IRepository<tbDataSoftUpdateLog> tbDataSoftUpdateLog
        {
            get
            {
                return GetRepository<tbDataSoftUpdateLog>();
            }
        }
        
        public IRepository<tbDept> tbDept
        {
            get
            {
                return GetRepository<tbDept>();
            }
        }
        
        public IRepository<tbDosage> tbDosage
        {
            get
            {
                return GetRepository<tbDosage>();
            }
        }
        
        public IRepository<tbEmployee> tbEmployee
        {
            get
            {
                return GetRepository<tbEmployee>();
            }
        }
        
        public IRepository<tbEmpSign> tbEmpSign
        {
            get
            {
                return GetRepository<tbEmpSign>();
            }
        }
        
        public IRepository<tbFillFee> tbFillFee
        {
            get
            {
                return GetRepository<tbFillFee>();
            }
        }
        
        public IRepository<tbFillFeeHis> tbFillFeeHis
        {
            get
            {
                return GetRepository<tbFillFeeHis>();
            }
        }
        
        public IRepository<tbFillFeeOther> tbFillFeeOther
        {
            get
            {
                return GetRepository<tbFillFeeOther>();
            }
        }
        
        public IRepository<tbFormula> tbFormula
        {
            get
            {
                return GetRepository<tbFormula>();
            }
        }
        
        
        
        
        
        public IRepository<tbGroupGoods> tbGroupGoods
        {
            get
            {
                return GetRepository<tbGroupGoods>();
            }
        }
        
        public IRepository<tbGroupMake> tbGroupMake
        {
            get
            {
                return GetRepository<tbGroupMake>();
            }
        }
        
        public IRepository<tbIntegralLog> tbIntegralLog
        {
            get
            {
                return GetRepository<tbIntegralLog>();
            }
        }
        
        public IRepository<tbIntegralLogHis> tbIntegralLogHis
        {
            get
            {
                return GetRepository<tbIntegralLogHis>();
            }
        }
        
        public IRepository<tbIntegralLogOther> tbIntegralLogOther
        {
            get
            {
                return GetRepository<tbIntegralLogOther>();
            }
        }
        
        public IRepository<tbInventory> tbInventory
        {
            get
            {
                return GetRepository<tbInventory>();
            }
        }
        
        
        
        public IRepository<tbMacAddress> tbMacAddress
        {
            get
            {
                return GetRepository<tbMacAddress>();
            }
        }
        
        public IRepository<tbMakeDetail> tbMakeDetail
        {
            get
            {
                return GetRepository<tbMakeDetail>();
            }
        }
        
        public IRepository<tbMakeLog> tbMakeLog
        {
            get
            {
                return GetRepository<tbMakeLog>();
            }
        }
        
        public IRepository<tbMaterial> tbMaterial
        {
            get
            {
                return GetRepository<tbMaterial>();
            }
        }
        
        public IRepository<tbMaterialEnter> tbMaterialEnter
        {
            get
            {
                return GetRepository<tbMaterialEnter>();
            }
        }
        
        public IRepository<tbMaterialOut> tbMaterialOut
        {
            get
            {
                return GetRepository<tbMaterialOut>();
            }
        }
        
        public IRepository<tbMaterialPara> tbMaterialPara
        {
            get
            {
                return GetRepository<tbMaterialPara>();
            }
        }
        
        public IRepository<tbMonthlyBalance> tbMonthlyBalance
        {
            get
            {
                return GetRepository<tbMonthlyBalance>();
            }
        }
        
        public IRepository<tbMonthlyBalanceLog> tbMonthlyBalanceLog
        {
            get
            {
                return GetRepository<tbMonthlyBalanceLog>();
            }
        }
        
        public IRepository<tbMoveDetail> tbMoveDetail
        {
            get
            {
                return GetRepository<tbMoveDetail>();
            }
        }
        
        public IRepository<tbMoveLog> tbMoveLog
        {
            get
            {
                return GetRepository<tbMoveLog>();
            }
        }
        
        public IRepository<tbNameCode> tbNameCode
        {
            get
            {
                return GetRepository<tbNameCode>();
            }
        }
        
        
        
        public IRepository<tbOperLog> tbOperLog
        {
            get
            {
                return GetRepository<tbOperLog>();
            }
        }
        
        public IRepository<tbOperStandard> tbOperStandard
        {
            get
            {
                return GetRepository<tbOperStandard>();
            }
        }
        
        public IRepository<tbOrderAddLog> tbOrderAddLog
        {
            get
            {
                return GetRepository<tbOrderAddLog>();
            }
        }
        
        public IRepository<tbOrderBook> tbOrderBook
        {
            get
            {
                return GetRepository<tbOrderBook>();
            }
        }
        
        public IRepository<tbOrderBookDetail> tbOrderBookDetail
        {
            get
            {
                return GetRepository<tbOrderBookDetail>();
            }
        }
        
        public IRepository<tbOrderReduceLog> tbOrderReduceLog
        {
            get
            {
                return GetRepository<tbOrderReduceLog>();
            }
        }
        
        public IRepository<tbOrderSerialNo> tbOrderSerialNo
        {
            get
            {
                return GetRepository<tbOrderSerialNo>();
            }
        }
        
        public IRepository<tbProduceCheckLog> tbProduceCheckLog
        {
            get
            {
                return GetRepository<tbProduceCheckLog>();
            }
        }
        
        public IRepository<tbProduceDetail> tbProduceDetail
        {
            get
            {
                return GetRepository<tbProduceDetail>();
            }
        }
        
        public IRepository<tbProduceDetailAdd> tbProduceDetailAdd
        {
            get
            {
                return GetRepository<tbProduceDetailAdd>();
            }
        }
        
        public IRepository<tbProduceDetailReduce> tbProduceDetailReduce
        {
            get
            {
                return GetRepository<tbProduceDetailReduce>();
            }
        }
        
        public IRepository<tbProduceLog> tbProduceLog
        {
            get
            {
                return GetRepository<tbProduceLog>();
            }
        }
        
        public IRepository<tbProduceOrderLog> tbProduceOrderLog
        {
            get
            {
                return GetRepository<tbProduceOrderLog>();
            }
        }
        
        public IRepository<tbProductClass> tbProductClass
        {
            get
            {
                return GetRepository<tbProductClass>();
            }
        }
        
        public IRepository<tbProductLostSerial> tbProductLostSerial
        {
            get
            {
                return GetRepository<tbProductLostSerial>();
            }
        }
        
        public IRepository<tbProductLostSerialLog> tbProductLostSerialLog
        {
            get
            {
                return GetRepository<tbProductLostSerialLog>();
            }
        }
        
        public IRepository<tbProductSerial> tbProductSerial
        {
            get
            {
                return GetRepository<tbProductSerial>();
            }
        }
        
        public IRepository<tbProductSerialLog> tbProductSerialLog
        {
            get
            {
                return GetRepository<tbProductSerialLog>();
            }
        }
        
        public IRepository<tbRepAssConsDaily> tbRepAssConsDaily
        {
            get
            {
                return GetRepository<tbRepAssConsDaily>();
            }
        }
        
        public IRepository<tbRepAssCount> tbRepAssCount
        {
            get
            {
                return GetRepository<tbRepAssCount>();
            }
        }
        
        public IRepository<tbRepAssDailyIGCharge> tbRepAssDailyIGCharge
        {
            get
            {
                return GetRepository<tbRepAssDailyIGCharge>();
            }
        }
        
        public IRepository<tbRepAssFill> tbRepAssFill
        {
            get
            {
                return GetRepository<tbRepAssFill>();
            }
        }
        
        public IRepository<tbRepAssLarg> tbRepAssLarg
        {
            get
            {
                return GetRepository<tbRepAssLarg>();
            }
        }
        
        public IRepository<tbRepAssSpecCons> tbRepAssSpecCons
        {
            get
            {
                return GetRepository<tbRepAssSpecCons>();
            }
        }
        
        public IRepository<tbSellDayCheckDetail> tbSellDayCheckDetail
        {
            get
            {
                return GetRepository<tbSellDayCheckDetail>();
            }
        }
        
        public IRepository<tbSellDayCheckLog> tbSellDayCheckLog
        {
            get
            {
                return GetRepository<tbSellDayCheckLog>();
            }
        }
        
        public IRepository<tbSellLoseLog> tbSellLoseLog
        {
            get
            {
                return GetRepository<tbSellLoseLog>();
            }
        }
        
        public IRepository<tbSignList> tbSignList
        {
            get
            {
                return GetRepository<tbSignList>();
            }
        }
        
        public IRepository<tbStockDetail> tbStockDetail
        {
            get
            {
                return GetRepository<tbStockDetail>();
            }
        }
        
        public IRepository<tbStockDetailLog> tbStockDetailLog
        {
            get
            {
                return GetRepository<tbStockDetailLog>();
            }
        }
        
        public IRepository<tbStockMain> tbStockMain
        {
            get
            {
                return GetRepository<tbStockMain>();
            }
        }
        
        public IRepository<tbStockMainLog> tbStockMainLog
        {
            get
            {
                return GetRepository<tbStockMainLog>();
            }
        }
        
        public IRepository<tbStockPlan> tbStockPlan
        {
            get
            {
                return GetRepository<tbStockPlan>();
            }
        }
        
        public IRepository<tbStockPlanDetail> tbStockPlanDetail
        {
            get
            {
                return GetRepository<tbStockPlanDetail>();
            }
        }
        
        public IRepository<tbStorage> tbStorage
        {
            get
            {
                return GetRepository<tbStorage>();
            }
        }
        
        public IRepository<tbStorageLog> tbStorageLog
        {
            get
            {
                return GetRepository<tbStorageLog>();
            }
        }
        
        public IRepository<tbSupplier> tbSupplier
        {
            get
            {
                return GetRepository<tbSupplier>();
            }
        }
        
        public IRepository<tbSysErrorLog> tbSysErrorLog
        {
            get
            {
                return GetRepository<tbSysErrorLog>();
            }
        }
        
        public IRepository<tbUnitInvert> tbUnitInvert
        {
            get
            {
                return GetRepository<tbUnitInvert>();
            }
        }
        
        public IRepository<tbWarehouse> tbWarehouse
        {
            get
            {
                return GetRepository<tbWarehouse>();
            }
        }
    }
}
