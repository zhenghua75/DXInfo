using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Resources;
using System.Reflection;

namespace DXInfo.Models
{
    public class SyncTableStruct
    {
        #region 库表结构数据同步
        public static void Update(SqlConnection conn)
        {            
            string[] scriptStructFileNames = {"NameCode.sql",
                                           "AdjustLocatorVouch.sql",
                                           "AdjustLocatorVouchs.sql",
                                       "aspnet_Applications.sql",
                                       "aspnet_AuthorizationRules.sql",
                                       "aspnet_CustomProfile.sql",
                                       "aspnet_Membership.sql",
                                       "aspnet_Paths.sql",
                                       "aspnet_PersonalizationAllUsers.sql",
                                       "aspnet_PersonalizationPerUser.sql",
                                       "aspnet_Profile.sql",
                                       "aspnet_Roles.sql",
                                       "aspnet_SchemaVersions.sql",
                                       "aspnet_Sitemaps.sql",
                                       "aspnet_Users.sql",
                                       "aspnet_UsersInRoles.sql",
                                       "aspnet_WebEvent_Events.sql",
                                       "BillDonateInvLists.sql",
                                       "BillInvLists.sql",
                                       "BillOfMaterials.sql",
                                       "Bills.sql",
                                       "Books.sql",
                                       "BusType.sql",
                                       "CardDonateInventory.sql",
                                       "CardLevels.sql",
                                       "CardPoints.sql",
                                       "Cards.sql",
                                       "CardsLog.sql",
                                       "CardTypes.sql",
                                       "CategoryDepts.sql",
                                       "CheckDifferences.sql",
                                       "CheckVouch.sql",
                                       "CheckVouchs.sql",
                                       "Consume.sql",
                                       "ConsumeDonateInv.sql",
                                       "ConsumeList.sql",
                                       "ConsumePackages.sql",
                                       "ConsumePoints.sql",
                                       "ConsumeTastes.sql",
                                       "CurrentInvLocator.sql",
                                       "CurrentStock.sql",
                                       "Depts.sql",
                                       "Desks.sql",
                                       "Drivers.sql",
                                       "ekey.sql",
                                       "EnumType.sql",
                                       "EnumTypeDescription.sql",
                                       "InvDepts.sql",
                                       "Inventory.sql",
                                       "InventoryCategory.sql",
                                       "InventoryDeptPrice.sql",
                                       "InvLocator.sql",
                                       "IPads.sql",
                                       "KitchenBill.sql",
                                       "KitchenMenuDesk.sql",
                                       "KitchenMiss.sql",
                                       "Lines.sql",
                                       "Locator.sql",
                                       "MeasurementUnitGroup.sql",
                                       "Members.sql",
                                       "MembersLog.sql",
                                       "MenuStatus.sql",
                                       "MixVouch.sql",
                                       "MixVouchs.sql",
                                       "MonthBalance.sql",
                                       "OrderBookDeskes.sql",
                                       "OrderBookDeskesHis.sql",
                                       "OrderBooks.sql",
                                       "OrderBooksHis.sql",
                                       "OrderDeskes.sql",
                                       "OrderDeskesHis.sql",
                                       "OrderDishes.sql",
                                       "OrderDishesHis.sql",
                                       "OrderHurry.sql",
                                       "OrderIpads.sql",
                                       "OrderMenus.sql",
                                       "OrderMenusHis.sql",
                                       "OrderPackages.sql",
                                       "OrderPackagesHis.sql",
                                       "OrderSequences.sql",
                                       "Organizations.sql",
                                       "Packages.sql",
                                       "PayTypes.sql",
                                       "Period.sql",
                                       "PlayLists.sql",
                                       "PTType.sql",
                                       "RdRecord.sql",
                                       "RdRecords.sql",
                                       "RdType.sql",
                                       "RechargeDonations.sql",
                                       "Recharges.sql",
                                       "Rooms.sql",
                                       "schema_info.sql",
                                       "scope_config.sql",
                                       "scope_info.sql",
                                       "ScrapVouch.sql",
                                       "ScrapVouchs.sql",
                                       "STType.sql",
                                       "Tastes.sql",
                                       "Transports.sql",
                                       "TransportsLog.sql",
                                       "TransVouch.sql",
                                       "TransVouchs.sql",
                                       "UnitOfMeasures.sql",
                                       "Vehicles.sql",
                                       "Vendor.sql",
                                       "VouchAuthority.sql",
                                       "VouchCodeRule.sql",
                                       "VouchCodeSn.sql",
                                       "VouchType.sql",
                                       "Warehouse.sql",
                                       "WarehouseDept.sql",
                                       "WarehouseInventory.sql",
                                       "WRCardLevels.sql",
                                             "insert_schema_info.sql",
                                             "aspnet_CheckSchemaVersion.sql",
                                             "aspnet_Membership_GetPasswordWithFormat.sql",
                                             "aspnet_Membership_GetUserByName.sql",
                                             //"CardLevels_BeginLetter.sql",
                                             "MembersLog_Birthday.sql",
                                             "MembersLog_Sex.sql",
                                             "aspnet_Roles_RemoveOtherIndex.sql",
                                             "aspnet_Users_RemoveOtherIndex.sql",
                                             "DropFK.sql",
                                             "vw_MenuDeskInfo.sql",
                                             "vw_UpdateInfoData.sql",
                                             "aspnet_Membership_UpdateUserInfo.sql",
                                             "Depts_DeptType.sql",
                                             "aspnet_Sitemaps_Sort.sql",                                             
                                             "InvPrice.sql",
                                             "ConsumeInvPrice.sql",
                                             "OrderInvPrice.sql",
                                             //"ConsumeList_IsValid_IsStock.sql",
                                             "RdRecords_SourceId.sql",
                                             "ConsumeListRds.sql",
                                             "CardTypes_IsVirtual.sql",
                                             "CardTypes_CardNoRule.sql",
                                             "Receipts.sql",
                                             "sp_DXInfo_GetDeptInventoryCategory.sql",
                                             "sp_DXInfo_GetDeptInventory.sql",
                                             "sp_DXInfo_GetDeptInventoryByCategory.sql",
                                             "sp_DXInfo_UpdateOrderMenuData.sql",
                                             "sp_DXInfo_UpdateOrderMenuDataComplete.sql",
                                             };
            UpdateDatabase(conn, scriptStructFileNames);
        }
        public static void UpdateServer(SqlConnection conn)
        {
            string[] scriptStructFileNames = {"Insert_NameCode.sql",
                                             "Insert_aspnet_SchemaVersions.sql",
                                             "aspnet_Membership_CreateUser.sql",
                                             "aspnet_Applications_CreateApplication.sql",
                                             "aspnet_Users_CreateUser.sql",
                                             "vw_aspnet_Users.sql",                                             
                                             //"Insert_aspnet_Sitemaps.sql",
                                             "aspnet_Membership_GetUserByUserId.sql",
                                             "aspnet_Membership_UpdateUser.sql",
                                             "aspnet_Roles_CreateRole.sql",
                                             "aspnet_Roles_DeleteRoleById.sql",
                                             "aspnet_Users_DeleteUser.sql",
                                             "vw_aspnet_MembershipUsers.sql",
                                             "vw_aspnet_UsersInRoles.sql",
                                             "vw_aspnet_Profiles.sql",
                                             "vw_aspnet_WebPartState_User.sql",
                                             "aspnet_Membership_UnlockUser.sql",
                                             "aspnet_Membership_GetPassword.sql",
                                             "aspnet_Membership_SetPassword.sql",
                                             "aspnet_UsersInRoles_IsUserInRole.sql",
                                             "aspnet_UsersInRoles_AddUsersToRoles.sql",
                                             "aspnet_UsersInRoles_RemoveUsersFromRoles.sql",
                                             "Insert_EnumTypeDescription.sql",
                                             "Insert_EnumType.sql",
                                             "Insert_VouchType.sql",
                                             "Insert_VouchCodeRule.sql",
                                             "Insert_RdType.sql",
                                             "Insert_BusType.sql",
                                             "Insert_PTType.sql",
                                             "Insert_STType.sql",
                                             "proc_balance_bydate.sql",
                                             "proc_Balance_Report.sql",
                                             "BalanceProc.sql",
                                             "tbLogin.sql",
                                             "tbCommCode.sql",
                                             "tbFunc.sql",
                                             "tbOperFunc.sql",
                                             "tbGoods.sql",
                                             "tbOper.sql",
                                             "tbNotice.sql",
                                             "tbAssociator.sql",
                                             "tbAssociatorSync.sql",
                                             "tbAssociatorLog.sql",                                             
                                             "tbConsItem.sql",
                                             "tbConsItemOther.sql",
                                             "tbConsItemHis.sql",
                                             "tbBill.sql",
                                             "tbBillOther.sql",
                                             "tbBillHis.sql",                                             
                                             "tbFillFee.sql",
                                             "tbFillFeeOther.sql",
                                             "tbFillFeeHis.sql",
                                             "tbBusiLog.sql",
                                             "tbBusiLogOther.sql",
                                             "tbBusiLogHis.sql",
                                             "vwConsItem.sql",
                                             "vwBill.sql",
                                             "vwFillFee.sql",
                                             "vwBusiLog.sql",
                                             "ProductionInStorage.sql",
                                             "SaleCheck.sql",
                                             "SyncAnchor.sql",
                                             "sp_DXInfo_GetCurrentInvLocatorOfCheck.sql"};
            UpdateDatabase(conn, scriptStructFileNames);
        }
        public static void UpdateSitemaps(SqlConnection conn)
        {
            string[] scriptStructFileNames = {                                           
                                             "Insert_aspnet_Sitemaps.sql",
                                             };
            UpdateDatabase(conn, scriptStructFileNames);
        }

//那友菜单        
//if not exists (select * from aspnet_Sitemaps where Code='000.008.001.008')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.008.001.008','QueryCurrentStock','查询库存','查询库存',NULL,NULL,NULL,'1306992444_report.png','000.008.001',1,1,1,109101106)
//if not exists (select * from aspnet_Sitemaps where Code='000.007.001.006')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.007.001.006','Receipt','添加订货单','添加订货单',NULL,'OrderAddViewModel',NULL,'1306992444_report.png','000.007.001',1,1,1,108101105)
//if not exists (select * from aspnet_Sitemaps where Code='000.007.001.007')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.007.001.007','ReceiptQuery','订货单查询修改','订货单查询修改',NULL,'OrderQueryViewModel',NULL,'1306992444_report.png','000.007.001',1,1,1,108101106)
//if not exists (select * from aspnet_Sitemaps where Code='000.007.001.008')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.007.001.008','Receipt','添加返修单','添加返修单',NULL,'ReworkAddViewModel',NULL,'1306992444_report.png','000.007.001',1,1,1,108101107)
//if not exists (select * from aspnet_Sitemaps where Code='000.007.001.009')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.007.001.009','ReceiptQuery','返修单查询修改','返修单查询修改',NULL,'ReworkQueryViewModel',NULL,'1306992444_report.png','000.007.001',1,1,1,108101108)
//if not exists (select * from aspnet_Sitemaps where Code='000.016.001.001')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.016.001.001',NULL,'订货单列表','订货单列表','BaseInfo','Receipt','ReceiptType=0',NULL,'000.016.001',1,1,0,103102106)
//if not exists (select * from aspnet_Sitemaps where Code='000.016.001.002')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.016.001.002',NULL,'返修单列表','返修单列表','BaseInfo','Receipt','ReceiptType=1',NULL,'000.016.001',1,1,0,103102107)
//if not exists (select * from aspnet_Sitemaps where Code='000.015.001.023')insert into aspnet_Sitemaps(Code,Name,Title,Description,Controller,Action,ParaId,Url,ParentCode,IsAuthorize,IsMenu,IsClient,Sort)values('000.015.001.023',NULL,'库存销售单位','库存销售单位','StockManage','Vendor','VendorType=1',NULL,'000.015.001',1,1,0,104101113)

        private static void UpdateDatabase(SqlConnection conn, string[] scriptStructFileNames)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> lScript = new List<string>();
            foreach (string scriptFileName in scriptStructFileNames)
            {
                string script = GetSql(assembly, scriptFileName);

                if (!string.IsNullOrEmpty(script))
                {
                    lScript.Add(script);
                }
            }
            if (lScript.Count > 0)
            {
                foreach (string script in lScript)
                {
                    using (SqlCommand cmd = new SqlCommand(script, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static string GetSql(Assembly assembly,string scriptFileName)
        {
            string script = string.Empty;
            Stream stream = assembly.GetManifestResourceStream("DXInfo.Models.SqlServerScript." + scriptFileName);
            StreamReader sr = new StreamReader(stream);
            if (sr != null)
            {
                script = sr.ReadToEnd();
                sr.Close();
                stream.Close();
            }
            return script;
        }
        #endregion
    }
}
