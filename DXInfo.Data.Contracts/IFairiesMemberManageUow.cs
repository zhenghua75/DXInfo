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
    
    
    public interface IFairiesMemberManageUow : IUow
    {
        
        IRepository<AdjustLocatorVouch> AdjustLocatorVouch
        {
            get;
        }
        
        IRepository<AdjustLocatorVouchs> AdjustLocatorVouchs
        {
            get;
        }
        
        IRepository<aspnet_Applications> aspnet_Applications
        {
            get;
        }
        
        IRepository<aspnet_AuthorizationRules> aspnet_AuthorizationRules
        {
            get;
        }
        
        IRepository<aspnet_CustomProfile> aspnet_CustomProfile
        {
            get;
        }
        
        IRepository<aspnet_Membership> aspnet_Membership
        {
            get;
        }
        
        IRepository<aspnet_Paths> aspnet_Paths
        {
            get;
        }
        
        IRepository<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers
        {
            get;
        }
        
        IRepository<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser
        {
            get;
        }
        
        IRepository<aspnet_Profile> aspnet_Profile
        {
            get;
        }
        
        IRepository<aspnet_Roles> aspnet_Roles
        {
            get;
        }
        
        IRepository<aspnet_SchemaVersions> aspnet_SchemaVersions
        {
            get;
        }
        
        IRepository<aspnet_Sitemaps> aspnet_Sitemaps
        {
            get;
        }
        
        IRepository<aspnet_Users> aspnet_Users
        {
            get;
        }
        
        IRepository<aspnet_UsersInRoles> aspnet_UsersInRoles
        {
            get;
        }
        
        IRepository<aspnet_WebEvent_Events> aspnet_WebEvent_Events
        {
            get;
        }
        
        IRepository<BillDonateInvLists> BillDonateInvLists
        {
            get;
        }
        
        IRepository<BillInvLists> BillInvLists
        {
            get;
        }
        
        IRepository<BillOfMaterials> BillOfMaterials
        {
            get;
        }
        
        IRepository<Bills> Bills
        {
            get;
        }
        
        IRepository<Books> Books
        {
            get;
        }
        
        IRepository<BusType> BusType
        {
            get;
        }
        
        IRepository<CardDonateInventory> CardDonateInventory
        {
            get;
        }
        
        IRepository<CardLevels> CardLevels
        {
            get;
        }
        
        IRepository<CardPoints> CardPoints
        {
            get;
        }
        
        IRepository<Cards> Cards
        {
            get;
        }
        
        IRepository<CardsLog> CardsLog
        {
            get;
        }
        
        IRepository<CardTypes> CardTypes
        {
            get;
        }
        
        IRepository<CategoryDepts> CategoryDepts
        {
            get;
        }
        
        IRepository<CheckDifferences> CheckDifferences
        {
            get;
        }
        
        IRepository<CheckVouch> CheckVouch
        {
            get;
        }
        
        IRepository<CheckVouchs> CheckVouchs
        {
            get;
        }
        
        IRepository<Consume> Consume
        {
            get;
        }
        IRepository<ConsumeInvPrice> ConsumeInvPrice
        {
            get;
        }
        IRepository<OrderInvPrice> OrderInvPrice
        {
            get;
        }
        IRepository<ConsumeDonateInv> ConsumeDonateInv
        {
            get;
        }
        
        IRepository<ConsumeList> ConsumeList
        {
            get;
        }
        IRepository<ConsumeListRds> ConsumeListRds
        {
            get;
        }
        IRepository<ConsumePackages> ConsumePackages
        {
            get;
        }
        
        IRepository<ConsumePoints> ConsumePoints
        {
            get;
        }
        
        IRepository<ConsumeTastes> ConsumeTastes
        {
            get;
        }
        
        IRepository<CurrentInvLocator> CurrentInvLocator
        {
            get;
        }
        
        IRepository<CurrentStock> CurrentStock
        {
            get;
        }
        
        IRepository<Depts> Depts
        {
            get;
        }
        
        IRepository<Desks> Desks
        {
            get;
        }
        
        IRepository<Drivers> Drivers
        {
            get;
        }
        
        IRepository<ekey> ekey
        {
            get;
        }
        
        IRepository<EnumType> EnumType
        {
            get;
        }
        
        IRepository<EnumTypeDescription> EnumTypeDescription
        {
            get;
        }
        
        IRepository<InvDepts> InvDepts
        {
            get;
        }
        
        IRepository<Inventory> Inventory
        {
            get;
        }
        IRepository<InvPrice> InvPrice
        {
            get;
        }
        IRepository<InventoryCategory> InventoryCategory
        {
            get;
        }
        
        IRepository<InventoryDeptPrice> InventoryDeptPrice
        {
            get;
        }
        
        IRepository<InvLocator> InvLocator
        {
            get;
        }
        
        IRepository<IPads> IPads
        {
            get;
        }
        
        IRepository<KitchenBill> KitchenBill
        {
            get;
        }
        
        IRepository<KitchenMenuDesk> KitchenMenuDesk
        {
            get;
        }
        
        IRepository<KitchenMiss> KitchenMiss
        {
            get;
        }
        
        IRepository<Lines> Lines
        {
            get;
        }
        
        IRepository<Locator> Locator
        {
            get;
        }
        
        IRepository<MeasurementUnitGroup> MeasurementUnitGroup
        {
            get;
        }
        
        IRepository<Members> Members
        {
            get;
        }
        
        IRepository<MembersLog> MembersLog
        {
            get;
        }
        
        IRepository<MenuStatus> MenuStatus
        {
            get;
        }
        
        IRepository<MixVouch> MixVouch
        {
            get;
        }
        
        IRepository<MixVouchs> MixVouchs
        {
            get;
        }
        
        IRepository<MonthBalance> MonthBalance
        {
            get;
        }
        
        IRepository<NameCode> NameCode
        {
            get;
        }
        
        IRepository<OrderBookDeskes> OrderBookDeskes
        {
            get;
        }
        
        IRepository<OrderBookDeskesHis> OrderBookDeskesHis
        {
            get;
        }
        
        IRepository<OrderBooks> OrderBooks
        {
            get;
        }
        
        IRepository<OrderBooksHis> OrderBooksHis
        {
            get;
        }
        
        IRepository<OrderDeskes> OrderDeskes
        {
            get;
        }
        
        IRepository<OrderDeskesHis> OrderDeskesHis
        {
            get;
        }
        
        IRepository<OrderDishes> OrderDishes
        {
            get;
        }
        
        IRepository<OrderDishesHis> OrderDishesHis
        {
            get;
        }
        
        IRepository<OrderHurry> OrderHurry
        {
            get;
        }
        
        IRepository<OrderIpads> OrderIpads
        {
            get;
        }
        
        IRepository<OrderMenus> OrderMenus
        {
            get;
        }
        
        IRepository<OrderMenusHis> OrderMenusHis
        {
            get;
        }
        
        IRepository<OrderPackages> OrderPackages
        {
            get;
        }
        
        IRepository<OrderPackagesHis> OrderPackagesHis
        {
            get;
        }
        
        IRepository<OrderSequences> OrderSequences
        {
            get;
        }
        
        IRepository<Organizations> Organizations
        {
            get;
        }
        
        IRepository<Packages> Packages
        {
            get;
        }
        
        IRepository<PayTypes> PayTypes
        {
            get;
        }
        
        IRepository<Period> Period
        {
            get;
        }
        
        IRepository<PlayLists> PlayLists
        {
            get;
        }
        
        IRepository<PTType> PTType
        {
            get;
        }
        
        IRepository<RdRecord> RdRecord
        {
            get;
        }
        
        IRepository<RdRecords> RdRecords
        {
            get;
        }
        
        IRepository<RdType> RdType
        {
            get;
        }
        
        IRepository<RechargeDonations> RechargeDonations
        {
            get;
        }
        
        IRepository<Recharges> Recharges
        {
            get;
        }
        
        IRepository<Rooms> Rooms
        {
            get;
        }
        IRepository<Receipts> Receipts
        {
            get;
        }
        IRepository<ReceiptHis> ReceiptHis
        {
            get;
        }
        IRepository<schema_info> schema_info
        {
            get;
        }
        
        IRepository<scope_config> scope_config
        {
            get;
        }
        
        IRepository<scope_info> scope_info
        {
            get;
        }
        
        IRepository<ScrapVouch> ScrapVouch
        {
            get;
        }
        
        IRepository<ScrapVouchs> ScrapVouchs
        {
            get;
        }
        
        IRepository<STType> STType
        {
            get;
        }
        
        IRepository<Tastes> Tastes
        {
            get;
        }
        
        IRepository<Transports> Transports
        {
            get;
        }
        
        IRepository<TransportsLog> TransportsLog
        {
            get;
        }
        
        IRepository<TransVouch> TransVouch
        {
            get;
        }
        
        IRepository<TransVouchs> TransVouchs
        {
            get;
        }
        
        IRepository<UnitOfMeasures> UnitOfMeasures
        {
            get;
        }
        
        IRepository<Vehicles> Vehicles
        {
            get;
        }
        
        IRepository<Vendor> Vendor
        {
            get;
        }
        
        IRepository<VouchAuthority> VouchAuthority
        {
            get;
        }
        
        IRepository<VouchCodeRule> VouchCodeRule
        {
            get;
        }
        
        IRepository<VouchCodeSn> VouchCodeSn
        {
            get;
        }
        
        IRepository<VouchType> VouchType
        {
            get;
        }
        
        IRepository<Warehouse> Warehouse
        {
            get;
        }
        
        IRepository<WarehouseDept> WarehouseDept
        {
            get;
        }
        
        IRepository<WarehouseInventory> WarehouseInventory
        {
            get;
        }
        
        IRepository<WRCardLevels> WRCardLevels
        {
            get;
        }
        //AMSApp
        IRepository<tbLogin> tbLogin
        {
            get;
        }

        IRepository<tbCommCode> tbCommCode
        {
            get;
        }

        IRepository<tbFunc> tbFunc
        {
            get;
        }

        IRepository<tbOperFunc> tbOperFunc
        {
            get;
        }

        IRepository<tbGoods> tbGoods
        {
            get;
        }

        IRepository<tbOper> tbOper
        {
            get;
        }


        IRepository<tbNotice> tbNotice
        {
            get;
        }

        IRepository<tbAssociator> tbAssociator
        {
            get;
        }

        IRepository<tbAssociatorLog> tbAssociatorLog
        {
            get;
        }

        IRepository<tbAssociatorSync> tbAssociatorSync
        {
            get;
        }

        IRepository<vwBill> vwBill
        {
            get;
        }

        IRepository<vwConsItem> vwConsItem
        {
            get;
        }

        IRepository<vwFillFee> vwFillFee
        {
            get;
        }
        IRepository<vwBusiLog> vwBusiLog
        {
            get;
        }

        IRepository<tbBusiIncomeReport> tbBusiIncomeReport
        {
            get;
        }

        IRepository<tbConsItemOther> tbConsItemOther
        {
            get;
        }

        IRepository<tbBillOther> tbBillOther
        {
            get;
        }

        IRepository<ProductionInStorage> ProductionInStorage
        {
            get;
        }

        IRepository<SaleCheck> SaleCheck
        {
            get;
        }
    }
}
