//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXInfo.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DXInfo.Models;
    
    
    public interface IAMSCMUow : IUow
    {
        
        IRepository<tbAssignDetail> tbAssignDetail
        {
            get;
        }
        
        IRepository<tbAssignDetailLog> tbAssignDetailLog
        {
            get;
        }
        
        IRepository<tbAssignLog> tbAssignLog
        {
            get;
        }
        
        
        
        
        
        IRepository<tbBespeakLog> tbBespeakLog
        {
            get;
        }
        
        IRepository<tbBill> tbBill
        {
            get;
        }
        
        IRepository<tbBillHis> tbBillHis
        {
            get;
        }
        
        IRepository<tbBillOfEnterStorageDetail> tbBillOfEnterStorageDetail
        {
            get;
        }
        
        IRepository<tbBillOfEnterStorageLog> tbBillOfEnterStorageLog
        {
            get;
        }
        
        IRepository<tbBillOfMaterials> tbBillOfMaterials
        {
            get;
        }
        
        IRepository<tbBillOfReceiveDetail> tbBillOfReceiveDetail
        {
            get;
        }
        
        IRepository<tbBillOfReceiveLog> tbBillOfReceiveLog
        {
            get;
        }
        
        
        
        
        IRepository<tbBusiLog> tbBusiLog
        {
            get;
        }
        
        IRepository<tbBusiLogHis> tbBusiLogHis
        {
            get;
        }
        
        IRepository<tbBusiLogOther> tbBusiLogOther
        {
            get;
        }
        
        IRepository<tbCheckSerial> tbCheckSerial
        {
            get;
        }
        
        IRepository<tbCheckSerialLog> tbCheckSerialLog
        {
            get;
        }
        
        
        IRepository<tbComputationGroup> tbComputationGroup
        {
            get;
        }
        
        IRepository<tbComputationUnit> tbComputationUnit
        {
            get;
        }
        
        IRepository<tbConsItem> tbConsItem
        {
            get;
        }
        
        IRepository<tbConsItemHis> tbConsItemHis
        {
            get;
        }
        
        
        
        IRepository<tbConsSerialNo> tbConsSerialNo
        {
            get;
        }
        
        IRepository<tbCurrentStock> tbCurrentStock
        {
            get;
        }
        
        IRepository<tbDataSoftUpdateLog> tbDataSoftUpdateLog
        {
            get;
        }
        
        IRepository<tbDept> tbDept
        {
            get;
        }
        
        IRepository<tbDosage> tbDosage
        {
            get;
        }
        
        IRepository<tbEmployee> tbEmployee
        {
            get;
        }
        
        IRepository<tbEmpSign> tbEmpSign
        {
            get;
        }
        
        IRepository<tbFillFee> tbFillFee
        {
            get;
        }
        
        IRepository<tbFillFeeHis> tbFillFeeHis
        {
            get;
        }
        
        IRepository<tbFillFeeOther> tbFillFeeOther
        {
            get;
        }
        
        IRepository<tbFormula> tbFormula
        {
            get;
        }
        
        
        
        
        
        IRepository<tbGroupGoods> tbGroupGoods
        {
            get;
        }
        
        IRepository<tbGroupMake> tbGroupMake
        {
            get;
        }
        
        IRepository<tbIntegralLog> tbIntegralLog
        {
            get;
        }
        
        IRepository<tbIntegralLogHis> tbIntegralLogHis
        {
            get;
        }
        
        IRepository<tbIntegralLogOther> tbIntegralLogOther
        {
            get;
        }
        
        IRepository<tbInventory> tbInventory
        {
            get;
        }
        
        
        
        IRepository<tbMacAddress> tbMacAddress
        {
            get;
        }
        
        IRepository<tbMakeDetail> tbMakeDetail
        {
            get;
        }
        
        IRepository<tbMakeLog> tbMakeLog
        {
            get;
        }
        
        IRepository<tbMaterial> tbMaterial
        {
            get;
        }
        
        IRepository<tbMaterialEnter> tbMaterialEnter
        {
            get;
        }
        
        IRepository<tbMaterialOut> tbMaterialOut
        {
            get;
        }
        
        IRepository<tbMaterialPara> tbMaterialPara
        {
            get;
        }
        
        IRepository<tbMonthlyBalance> tbMonthlyBalance
        {
            get;
        }
        
        IRepository<tbMonthlyBalanceLog> tbMonthlyBalanceLog
        {
            get;
        }
        
        IRepository<tbMoveDetail> tbMoveDetail
        {
            get;
        }
        
        IRepository<tbMoveLog> tbMoveLog
        {
            get;
        }
        
        IRepository<tbNameCode> tbNameCode
        {
            get;
        }
        
        IRepository<tbOperLog> tbOperLog
        {
            get;
        }
        
        IRepository<tbOperStandard> tbOperStandard
        {
            get;
        }
        
        IRepository<tbOrderAddLog> tbOrderAddLog
        {
            get;
        }
        
        IRepository<tbOrderBook> tbOrderBook
        {
            get;
        }
        
        IRepository<tbOrderBookDetail> tbOrderBookDetail
        {
            get;
        }
        
        IRepository<tbOrderReduceLog> tbOrderReduceLog
        {
            get;
        }
        
        IRepository<tbOrderSerialNo> tbOrderSerialNo
        {
            get;
        }
        
        IRepository<tbProduceCheckLog> tbProduceCheckLog
        {
            get;
        }
        
        IRepository<tbProduceDetail> tbProduceDetail
        {
            get;
        }
        
        IRepository<tbProduceDetailAdd> tbProduceDetailAdd
        {
            get;
        }
        
        IRepository<tbProduceDetailReduce> tbProduceDetailReduce
        {
            get;
        }
        
        IRepository<tbProduceLog> tbProduceLog
        {
            get;
        }
        
        IRepository<tbProduceOrderLog> tbProduceOrderLog
        {
            get;
        }
        
        IRepository<tbProductClass> tbProductClass
        {
            get;
        }
        
        IRepository<tbProductLostSerial> tbProductLostSerial
        {
            get;
        }
        
        IRepository<tbProductLostSerialLog> tbProductLostSerialLog
        {
            get;
        }
        
        IRepository<tbProductSerial> tbProductSerial
        {
            get;
        }
        
        IRepository<tbProductSerialLog> tbProductSerialLog
        {
            get;
        }
        
        IRepository<tbRepAssConsDaily> tbRepAssConsDaily
        {
            get;
        }
        
        IRepository<tbRepAssCount> tbRepAssCount
        {
            get;
        }
        
        IRepository<tbRepAssDailyIGCharge> tbRepAssDailyIGCharge
        {
            get;
        }
        
        IRepository<tbRepAssFill> tbRepAssFill
        {
            get;
        }
        
        IRepository<tbRepAssLarg> tbRepAssLarg
        {
            get;
        }
        
        IRepository<tbRepAssSpecCons> tbRepAssSpecCons
        {
            get;
        }
        
        IRepository<tbSellDayCheckDetail> tbSellDayCheckDetail
        {
            get;
        }
        
        IRepository<tbSellDayCheckLog> tbSellDayCheckLog
        {
            get;
        }
        
        IRepository<tbSellLoseLog> tbSellLoseLog
        {
            get;
        }
        
        IRepository<tbSignList> tbSignList
        {
            get;
        }
        
        IRepository<tbStockDetail> tbStockDetail
        {
            get;
        }
        
        IRepository<tbStockDetailLog> tbStockDetailLog
        {
            get;
        }
        
        IRepository<tbStockMain> tbStockMain
        {
            get;
        }
        
        IRepository<tbStockMainLog> tbStockMainLog
        {
            get;
        }
        
        IRepository<tbStockPlan> tbStockPlan
        {
            get;
        }
        
        IRepository<tbStockPlanDetail> tbStockPlanDetail
        {
            get;
        }
        
        IRepository<tbStorage> tbStorage
        {
            get;
        }
        
        IRepository<tbStorageLog> tbStorageLog
        {
            get;
        }
        
        IRepository<tbSupplier> tbSupplier
        {
            get;
        }
        
        IRepository<tbSysErrorLog> tbSysErrorLog
        {
            get;
        }
        
        IRepository<tbUnitInvert> tbUnitInvert
        {
            get;
        }
        
        IRepository<tbWarehouse> tbWarehouse
        {
            get;
        }
    }
}