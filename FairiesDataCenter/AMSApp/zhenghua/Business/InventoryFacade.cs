using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua;
using AMSApp.zhenghua.Entity;
using System.Collections;
using System.IO;

namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// InventoryFacade 的摘要说明。
	/// </summary>
	public class InventoryFacade
	{
		public InventoryFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region productclass
		public int AddProductClass(OperLog operLog,ProductClass pc)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					EntityMapping.Create(pc,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "产品分类："+pc.cnvcProductClassCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int UpdateProductClass(OperLog operLog,ProductClass pc)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProductClass oldpc = new ProductClass();
					oldpc.cnvcProductClassCode = pc.cnvcProductClassCode;

					oldpc = EntityMapping.Get(oldpc,trans) as ProductClass;
					oldpc.cnvcComments = pc.cnvcComments;
					oldpc.cnvcProductClassName = pc.cnvcProductClassName;
					oldpc.cnvcProductType = pc.cnvcProductType;
					EntityMapping.Update(pc,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "产品分类："+pc.cnvcProductClassCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int DeleteProductClass(OperLog operLog,ProductClass pc)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProductClass oldpc = new ProductClass();
					oldpc.cnvcProductClassCode = pc.cnvcProductClassCode;

					oldpc = EntityMapping.Get(oldpc,trans)  as ProductClass;
					EntityMapping.Delete(oldpc,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "产品分类："+ pc.cnvcProductClassCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		#endregion


		public int AddInventory(OperLog operLog,Entity.Inventory inv)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					//inv.cndSDate = dtSysTime;
					inv.cnvcCreatePerson = operLog.cnvcOperID;
					inv.cndModifyDate = dtSysTime;
					EntityMapping.Create(inv,trans);					
					SyncGoods(inv,trans);
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "存货编码："+inv.cnvcInvCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}
		private void SyncGoods(Entity.Inventory inv,SqlTransaction trans)
		{
			//string strsql = "SELECT * FROM tbProductClass where cnvcproductclasscode='"+inv.cnvcInvCCode+"'";
			//DataTable dtpc = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
			//			if(dtpc.Rows.Count>0)
			//			{
			//Entity.ProductClass pc = new ProductClass(dtpc);
			//				if(pc.cnvcProductType == "FINALPRODUCT")
			//				{

			string strsql = "select * from tbGoods where vcGoodsID='"+inv.cnvcInvCode+"'";
			DataTable dtGoods = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
			if(inv.cnbSale)
			{
				if(dtGoods.Rows.Count==0)
				{
					Entity.Goods gs = new Goods();
					gs.vcGoodsID = inv.cnvcInvCode;
					gs.vcGoodsName = inv.cnvcInvName;
					gs.vcSpell = Helper.GetChineseSpell(inv.cnvcInvName);
					gs.nPrice = Convert.ToDecimal(inv.cnfRetailPrice);
					gs.nRate = 0;
					gs.iIgValue = -1;
					gs.cNewFlag = "0";
					gs.vcComments = "存货档案添加同步";
					EntityMapping.Create(gs,trans);
				}
				else
				{
					Entity.Goods gs = new Goods(dtGoods);
					if(gs.vcGoodsName != inv.cnvcInvName)
					{
						gs.vcGoodsName = inv.cnvcInvName;
						gs.vcSpell = Helper.GetChineseSpell(inv.cnvcInvName);
					}
					if(inv.cnfRetailPrice > 0)
						gs.nPrice = Convert.ToDecimal(inv.cnfRetailPrice);
					gs.vcComments = "存货档案修改同步";
					EntityMapping.Update(gs,trans);
				}
			}
			else
			{
				if(dtGoods.Rows.Count>0)
				{
					Entity.Goods gs = new Goods(dtGoods);
					EntityMapping.Delete(gs,trans);
				}
			}
			//}
			//}
		}
		public int UpdateInventory(OperLog operLog,Entity.Inventory inv)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					//IFormatProvider ip = new System.ifor
					//strSysTime = DateTime.Parse(strSysTime).ToString("yyyy-MM-dd hh:mm:ss");
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					//dtSysTime = dtSysTime.ToString
					Entity.Inventory oldinv = new Entity.Inventory();
					oldinv.cnvcInvCode = inv.cnvcInvCode;

					oldinv = EntityMapping.Get(oldinv,trans) as Entity.Inventory;
					oldinv.cnbComsume = inv.cnbComsume;
					oldinv.cnbProductBill = inv.cnbProductBill;
					oldinv.cnbPurchase = inv.cnbPurchase;
					oldinv.cnbSale = inv.cnbSale;
					oldinv.cnbSelf = inv.cnbSelf;
					oldinv.cndEDate = inv.cndEDate;
					oldinv.cndSDate = inv.cndSDate;
					oldinv.cndModifyDate = dtSysTime;
					oldinv.cnfRetailPrice = inv.cnfRetailPrice;
					oldinv.cniInvCCost = inv.cniInvCCost;
					oldinv.cniInvNCost = inv.cniInvNCost;
					oldinv.cniLowSum = inv.cniLowSum;
					oldinv.cniSafeNum = inv.cniSafeNum;
					oldinv.cnvcColor = inv.cnvcColor;
					oldinv.cnvcComUnitCode = inv.cnvcComUnitCode;
					oldinv.cnvcFeel = inv.cnvcFeel;
					oldinv.cnvcGroupCode = inv.cnvcGroupCode;
					oldinv.cnvcInvCCode = inv.cnvcInvCCode;
					oldinv.cnvcInvCode = inv.cnvcInvCode;
					oldinv.cnvcInvName = inv.cnvcInvName;
					oldinv.cnvcInvStd = inv.cnvcInvStd;
					oldinv.cnvcModifyPerson = operLog.cnvcOperID;
					oldinv.cnvcOrganise = inv.cnvcOrganise;
					oldinv.cnvcProduceUnitCode = inv.cnvcProduceUnitCode;
					oldinv.cnvcPUComUnitCode = inv.cnvcPUComUnitCode;
					oldinv.cnvcSAComUnitCode = inv.cnvcSAComUnitCode;
					oldinv.cnvcShopUnitCode = inv.cnvcShopUnitCode;
					oldinv.cnvcSTComUnitCode = inv.cnvcSTComUnitCode;
					oldinv.cnvcTaste = inv.cnvcTaste;
					oldinv.cnvcValueType = inv.cnvcValueType;

					EntityMapping.Update(oldinv,trans);

					SyncGoods(inv,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "存货编码："+inv.cnvcInvCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int DeleteInventory(OperLog operLog,Entity.Inventory inv)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Inventory oldinv = new Entity.Inventory();
					oldinv.cnvcInvCode = inv.cnvcInvCode;

					oldinv = EntityMapping.Get(oldinv,trans)  as Entity.Inventory;
					EntityMapping.Delete(oldinv,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "存货编码："+inv.cnvcInvCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}


		
		public int UpdateBOM(OperLog operLog,string strinvcode,DataTable dtbom)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					//string strinvcode = "";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,"delete from tbbillofmaterials where cnvcpartinvcode='"+strinvcode+"'");
					foreach(DataRow drbom in dtbom.Rows)
					{
						Entity.BillOfMaterials bom = new BillOfMaterials(drbom);						
						EntityMapping.Create(bom,trans);						
					}
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "存货编码："+strinvcode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

	}
}
