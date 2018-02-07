namespace DXInfo.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using DXInfo.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.ComponentModel.DataAnnotations.Schema;
    using DXInfo.Data.Configuration;
        
    public class FairiesMemberManageDbContext : DbContext
    {        
        public DbSet<AdjustLocatorVouch> AdjustLocatorVouch { get; set; }
        public DbSet<AdjustLocatorVouchs> AdjustLocatorVouchs { get; set; }
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_AuthorizationRules> aspnet_AuthorizationRules { get; set; }
        public DbSet<aspnet_CustomProfile> aspnet_CustomProfile { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_Sitemaps> aspnet_Sitemaps { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<aspnet_UsersInRoles> aspnet_UsersInRoles { get; set; }
        public DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public DbSet<BillDonateInvLists> BillDonateInvLists { get; set; }
        public DbSet<BillInvLists> BillInvLists { get; set; }
        public DbSet<BillOfMaterials> BillOfMaterials { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BusType> BusType { get; set; }
        public DbSet<CardDonateInventory> CardDonateInventory { get; set; }
        public DbSet<CardLevels> CardLevels { get; set; }
        public DbSet<CardPoints> CardPoints { get; set; }
        public DbSet<Cards> Cards { get; set; }
        public DbSet<CardsLog> CardsLog { get; set; }
        public DbSet<CardTypes> CardTypes { get; set; }
        public DbSet<CategoryDepts> CategoryDepts { get; set; }
        public DbSet<CheckDifferences> CheckDifferences { get; set; }
        public DbSet<CheckVouch> CheckVouch { get; set; }
        public DbSet<CheckVouchs> CheckVouchs { get; set; }
        public DbSet<Consume> Consume { get; set; }        
        public DbSet<ConsumeInvPrice> ConsumeInvPrice { get; set; }
        public DbSet<OrderInvPrice> OrderInvPrice { get; set; }
        public DbSet<ConsumeDonateInv> ConsumeDonateInv { get; set; }
        public DbSet<ConsumeList> ConsumeList { get; set; }
        public DbSet<ConsumeListRds> ConsumeListRds { get; set; }
        public DbSet<ConsumePackages> ConsumePackages { get; set; }
        public DbSet<ConsumePoints> ConsumePoints { get; set; }
        public DbSet<ConsumeTastes> ConsumeTastes { get; set; }
        public DbSet<CurrentInvLocator> CurrentInvLocator { get; set; }
        public DbSet<CurrentStock> CurrentStock { get; set; }
        public DbSet<Depts> Depts { get; set; }
        public DbSet<Desks> Desks { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<ekey> ekey { get; set; }
        public DbSet<EnumType> EnumType { get; set; }
        public DbSet<EnumTypeDescription> EnumTypeDescription { get; set; }
        public DbSet<InvDepts> InvDepts { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InvPrice> InvPrice { get; set; }
        public DbSet<InventoryCategory> InventoryCategory { get; set; }
        public DbSet<InventoryDeptPrice> InventoryDeptPrice { get; set; }
        public DbSet<InvLocator> InvLocator { get; set; }
        public DbSet<IPads> IPads { get; set; }
        public DbSet<KitchenBill> KitchenBill { get; set; }
        public DbSet<KitchenMenuDesk> KitchenMenuDesk { get; set; }
        public DbSet<KitchenMiss> KitchenMiss { get; set; }
        public DbSet<Lines> Lines { get; set; }
        public DbSet<Locator> Locator { get; set; }
        public DbSet<MeasurementUnitGroup> MeasurementUnitGroup { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<MembersLog> MembersLog { get; set; }
        public DbSet<MenuStatus> MenuStatus { get; set; }
        public DbSet<MixVouch> MixVouch { get; set; }
        public DbSet<MixVouchs> MixVouchs { get; set; }
        public DbSet<MonthBalance> MonthBalance { get; set; }
        public DbSet<NameCode> NameCode { get; set; }
        public DbSet<OrderBookDeskes> OrderBookDeskes { get; set; }
        public DbSet<OrderBookDeskesHis> OrderBookDeskesHis { get; set; }
        public DbSet<OrderBooks> OrderBooks { get; set; }
        public DbSet<OrderBooksHis> OrderBooksHis { get; set; }
        public DbSet<OrderDeskes> OrderDeskes { get; set; }
        public DbSet<OrderDeskesHis> OrderDeskesHis { get; set; }
        public DbSet<OrderDishes> OrderDishes { get; set; }
        public DbSet<OrderDishesHis> OrderDishesHis { get; set; }
        public DbSet<OrderHurry> OrderHurry { get; set; }
        public DbSet<OrderIpads> OrderIpads { get; set; }
        public DbSet<OrderMenus> OrderMenus { get; set; }
        public DbSet<OrderMenusHis> OrderMenusHis { get; set; }
        public DbSet<OrderPackages> OrderPackages { get; set; }
        public DbSet<OrderPackagesHis> OrderPackagesHis { get; set; }
        public DbSet<OrderSequences> OrderSequences { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<PayTypes> PayTypes { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<PlayLists> PlayLists { get; set; }
        public DbSet<PTType> PTType { get; set; }
        public DbSet<RdRecord> RdRecord { get; set; }
        public DbSet<RdRecords> RdRecords { get; set; }
        public DbSet<RdType> RdType { get; set; }
        public DbSet<RechargeDonations> RechargeDonations { get; set; }
        public DbSet<Recharges> Recharges { get; set; }
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<ReceiptHis> ReceiptHis { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<schema_info> schema_info { get; set; }
        public DbSet<scope_config> scope_config { get; set; }
        public DbSet<scope_info> scope_info { get; set; }
        public DbSet<ScrapVouch> ScrapVouch { get; set; }
        public DbSet<ScrapVouchs> ScrapVouchs { get; set; }
        public DbSet<STType> STType { get; set; }
        public DbSet<Tastes> Tastes { get; set; }
        public DbSet<Transports> Transports { get; set; }
        public DbSet<TransportsLog> TransportsLog { get; set; }
        public DbSet<TransVouch> TransVouch { get; set; }
        public DbSet<TransVouchs> TransVouchs { get; set; }
        public DbSet<UnitOfMeasures> UnitOfMeasures { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VouchAuthority> VouchAuthority { get; set; }
        public DbSet<VouchCodeRule> VouchCodeRule { get; set; }
        public DbSet<VouchCodeSn> VouchCodeSn { get; set; }
        public DbSet<VouchType> VouchType { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<WarehouseDept> WarehouseDept { get; set; }
        public DbSet<WarehouseInventory> WarehouseInventory { get; set; }
        public DbSet<WRCardLevels> WRCardLevels { get; set; }
        //AMSApp
        public DbSet<tbLogin> tbLogin { get; set; }
        public DbSet<tbCommCode> tbCommCode { get; set; }
        public DbSet<tbFunc> tbFunc { get; set; }
        public DbSet<tbOperFunc> tbOperFunc { get; set; }
        public DbSet<tbGoods> tbGoods { get; set; }
        public DbSet<tbOper> tbOper { get; set; }
        public DbSet<tbNotice> tbNotice { get; set; }
        public DbSet<tbAssociator> tbAssociator { get; set; }
        public DbSet<tbAssociatorLog> tbAssociatorLog { get; set; }
        public DbSet<tbAssociatorSync> tbAssociatorSync { get; set; }
        public DbSet<vwBill> vwBill { get; set; }
        public DbSet<vwConsItem> vwConsItem { get; set; }
        public DbSet<vwFillFee> vwFillFee { get; set; }
        public DbSet<vwBusiLog> vwBusiLog { get; set; }
        public DbSet<tbBusiIncomeReport> tbBusiIncomeReport { get; set; }
        public DbSet<tbConsItemOther> tbConsItemOther { get; set; }
        public DbSet<tbBillOther> tbBillOther { get; set; }
        public DbSet<ProductionInStorage> ProductionInStorage { get; set; }
        public DbSet<SaleCheck> SaleCheck { get; set; }
        public FairiesMemberManageDbContext() : 
                base(nameOrConnectionString:"FairiesMemberManage")
        {
            Database.SetInitializer<FairiesMemberManageDbContext>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();            
            modelBuilder.Configurations.Add(new AdjustLocatorVouchConfiguration());
            modelBuilder.Configurations.Add(new AdjustLocatorVouchsConfiguration());
            modelBuilder.Configurations.Add(new aspnet_ApplicationsConfiguration());
            modelBuilder.Configurations.Add(new aspnet_AuthorizationRulesConfiguration());
            modelBuilder.Configurations.Add(new aspnet_CustomProfileConfiguration());
            modelBuilder.Configurations.Add(new aspnet_MembershipConfiguration());
            modelBuilder.Configurations.Add(new aspnet_PathsConfiguration());
            modelBuilder.Configurations.Add(new aspnet_PersonalizationAllUsersConfiguration());
            modelBuilder.Configurations.Add(new aspnet_PersonalizationPerUserConfiguration());
            modelBuilder.Configurations.Add(new aspnet_ProfileConfiguration());
            modelBuilder.Configurations.Add(new aspnet_RolesConfiguration());
            modelBuilder.Configurations.Add(new aspnet_SchemaVersionsConfiguration());
            modelBuilder.Configurations.Add(new aspnet_SitemapsConfiguration());
            modelBuilder.Configurations.Add(new aspnet_UsersConfiguration());
            modelBuilder.Configurations.Add(new aspnet_UsersInRolesConfiguration());
            modelBuilder.Configurations.Add(new aspnet_WebEvent_EventsConfiguration());
            modelBuilder.Configurations.Add(new BillDonateInvListsConfiguration());
            modelBuilder.Configurations.Add(new BillInvListsConfiguration());
            modelBuilder.Configurations.Add(new BillOfMaterialsConfiguration());
            modelBuilder.Configurations.Add(new BillsConfiguration());
            modelBuilder.Configurations.Add(new BooksConfiguration());
            modelBuilder.Configurations.Add(new BusTypeConfiguration());
            modelBuilder.Configurations.Add(new CardDonateInventoryConfiguration());
            modelBuilder.Configurations.Add(new CardLevelsConfiguration());
            modelBuilder.Configurations.Add(new CardPointsConfiguration());
            modelBuilder.Configurations.Add(new CardsConfiguration());
            modelBuilder.Configurations.Add(new CardsLogConfiguration());
            modelBuilder.Configurations.Add(new CardTypesConfiguration());
            modelBuilder.Configurations.Add(new CategoryDeptsConfiguration());
            modelBuilder.Configurations.Add(new CheckDifferencesConfiguration());
            modelBuilder.Configurations.Add(new CheckVouchConfiguration());
            modelBuilder.Configurations.Add(new CheckVouchsConfiguration());
            modelBuilder.Configurations.Add(new ConsumeConfiguration());
            modelBuilder.Configurations.Add(new ConsumeInvPriceConfiguration());
            modelBuilder.Configurations.Add(new OrderInvPriceConfiguration());
            modelBuilder.Configurations.Add(new ConsumeDonateInvConfiguration());
            modelBuilder.Configurations.Add(new ConsumeListConfiguration());
            modelBuilder.Configurations.Add(new ConsumeListRdsConfiguration());
            modelBuilder.Configurations.Add(new ConsumePackagesConfiguration());
            modelBuilder.Configurations.Add(new ConsumePointsConfiguration());
            modelBuilder.Configurations.Add(new ConsumeTastesConfiguration());
            modelBuilder.Configurations.Add(new CurrentInvLocatorConfiguration());
            modelBuilder.Configurations.Add(new CurrentStockConfiguration());
            modelBuilder.Configurations.Add(new DeptsConfiguration());
            modelBuilder.Configurations.Add(new DesksConfiguration());
            modelBuilder.Configurations.Add(new DriversConfiguration());
            modelBuilder.Configurations.Add(new ekeyConfiguration());
            modelBuilder.Configurations.Add(new EnumTypeConfiguration());
            modelBuilder.Configurations.Add(new EnumTypeDescriptionConfiguration());
            modelBuilder.Configurations.Add(new InvDeptsConfiguration());
            modelBuilder.Configurations.Add(new InventoryConfiguration());
            modelBuilder.Configurations.Add(new InvPriceConfiguration());
            modelBuilder.Configurations.Add(new InventoryCategoryConfiguration());
            modelBuilder.Configurations.Add(new InventoryDeptPriceConfiguration());
            modelBuilder.Configurations.Add(new InvLocatorConfiguration());
            modelBuilder.Configurations.Add(new IPadsConfiguration());
            modelBuilder.Configurations.Add(new KitchenBillConfiguration());
            modelBuilder.Configurations.Add(new KitchenMenuDeskConfiguration());
            modelBuilder.Configurations.Add(new KitchenMissConfiguration());
            modelBuilder.Configurations.Add(new LinesConfiguration());
            modelBuilder.Configurations.Add(new LocatorConfiguration());
            modelBuilder.Configurations.Add(new MeasurementUnitGroupConfiguration());
            modelBuilder.Configurations.Add(new MembersConfiguration());
            modelBuilder.Configurations.Add(new MembersLogConfiguration());
            modelBuilder.Configurations.Add(new MenuStatusConfiguration());
            modelBuilder.Configurations.Add(new MixVouchConfiguration());
            modelBuilder.Configurations.Add(new MixVouchsConfiguration());
            modelBuilder.Configurations.Add(new MonthBalanceConfiguration());
            modelBuilder.Configurations.Add(new NameCodeConfiguration());
            modelBuilder.Configurations.Add(new OrderBookDeskesConfiguration());
            modelBuilder.Configurations.Add(new OrderBookDeskesHisConfiguration());
            modelBuilder.Configurations.Add(new OrderBooksConfiguration());
            modelBuilder.Configurations.Add(new OrderBooksHisConfiguration());
            modelBuilder.Configurations.Add(new OrderDeskesConfiguration());
            modelBuilder.Configurations.Add(new OrderDeskesHisConfiguration());
            modelBuilder.Configurations.Add(new OrderDishesConfiguration());
            modelBuilder.Configurations.Add(new OrderDishesHisConfiguration());
            modelBuilder.Configurations.Add(new OrderHurryConfiguration());
            modelBuilder.Configurations.Add(new OrderIpadsConfiguration());
            modelBuilder.Configurations.Add(new OrderMenusConfiguration());
            modelBuilder.Configurations.Add(new OrderMenusHisConfiguration());
            modelBuilder.Configurations.Add(new OrderPackagesConfiguration());
            modelBuilder.Configurations.Add(new OrderPackagesHisConfiguration());
            modelBuilder.Configurations.Add(new OrderSequencesConfiguration());
            modelBuilder.Configurations.Add(new OrganizationsConfiguration());
            modelBuilder.Configurations.Add(new PackagesConfiguration());
            modelBuilder.Configurations.Add(new PayTypesConfiguration());
            modelBuilder.Configurations.Add(new PeriodConfiguration());
            modelBuilder.Configurations.Add(new PlayListsConfiguration());
            modelBuilder.Configurations.Add(new PTTypeConfiguration());
            modelBuilder.Configurations.Add(new RdRecordConfiguration());
            modelBuilder.Configurations.Add(new RdRecordsConfiguration());
            modelBuilder.Configurations.Add(new RdTypeConfiguration());
            modelBuilder.Configurations.Add(new RechargeDonationsConfiguration());
            modelBuilder.Configurations.Add(new RechargesConfiguration());
            modelBuilder.Configurations.Add(new RoomsConfiguration());
            modelBuilder.Configurations.Add(new ReceiptsConfiguration());
            modelBuilder.Configurations.Add(new ReceiptHisConfiguration());
            modelBuilder.Configurations.Add(new schema_infoConfiguration());
            modelBuilder.Configurations.Add(new scope_configConfiguration());
            modelBuilder.Configurations.Add(new scope_infoConfiguration());
            modelBuilder.Configurations.Add(new ScrapVouchConfiguration());
            modelBuilder.Configurations.Add(new ScrapVouchsConfiguration());
            modelBuilder.Configurations.Add(new STTypeConfiguration());
            modelBuilder.Configurations.Add(new TastesConfiguration());
            modelBuilder.Configurations.Add(new TransportsConfiguration());
            modelBuilder.Configurations.Add(new TransportsLogConfiguration());
            modelBuilder.Configurations.Add(new TransVouchConfiguration());
            modelBuilder.Configurations.Add(new TransVouchsConfiguration());
            modelBuilder.Configurations.Add(new UnitOfMeasuresConfiguration());
            modelBuilder.Configurations.Add(new VehiclesConfiguration());
            modelBuilder.Configurations.Add(new VendorConfiguration());
            modelBuilder.Configurations.Add(new VouchAuthorityConfiguration());
            modelBuilder.Configurations.Add(new VouchCodeRuleConfiguration());
            modelBuilder.Configurations.Add(new VouchCodeSnConfiguration());
            modelBuilder.Configurations.Add(new VouchTypeConfiguration());
            modelBuilder.Configurations.Add(new WarehouseConfiguration());
            modelBuilder.Configurations.Add(new WarehouseDeptConfiguration());
            modelBuilder.Configurations.Add(new WarehouseInventoryConfiguration());
            modelBuilder.Configurations.Add(new WRCardLevelsConfiguration());

            modelBuilder.Configurations.Add(new tbLoginConfiguration());
            modelBuilder.Configurations.Add(new tbCommCodeConfiguration());
            modelBuilder.Configurations.Add(new tbFuncConfiguration());
            modelBuilder.Configurations.Add(new tbOperFuncConfiguration());
            modelBuilder.Configurations.Add(new tbGoodsConfiguration());
            modelBuilder.Configurations.Add(new tbOperConfiguration());
            modelBuilder.Configurations.Add(new tbNoticeConfiguration());
            modelBuilder.Configurations.Add(new tbAssociatorConfiguration());
            modelBuilder.Configurations.Add(new tbAssociatorLogConfiguration());
            modelBuilder.Configurations.Add(new tbAssociatorSyncConfiguration());
            modelBuilder.Configurations.Add(new vwBillConfiguration());
            modelBuilder.Configurations.Add(new vwConsItemConfiguration());
            modelBuilder.Configurations.Add(new vwFillFeeConfiguration());
            modelBuilder.Configurations.Add(new vwBusiLogConfiguration());
            modelBuilder.Configurations.Add(new tbBusiIncomeReportConfiguration());
            modelBuilder.Configurations.Add(new tbConsItemOtherConfiguration());
            modelBuilder.Configurations.Add(new tbBillOtherConfiguration());
            modelBuilder.Configurations.Add(new ProductionInStorageConfiguration());
            modelBuilder.Configurations.Add(new SaleCheckConfiguration());
        }
    }
}
