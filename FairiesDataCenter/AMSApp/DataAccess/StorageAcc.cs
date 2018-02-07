using System;
using System.Data;
using System.Data.SqlClient;
using CommCenter;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// Summary description for StorageAcc.
	/// </summary>
	public class StorageAcc
	{
		SqlConnection con;
		AMSLog clog=new AMSLog();
		public StorageAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetCheckData(Hashtable htpara)
		{
			DataTable dttmp=new DataTable();
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string strSerial=System.Guid.NewGuid().ToString();
					string sql="";
					switch(htpara["strPType"].ToString())
					{
						//原料
						case "Raw":
							#region 生成原料库存盘点数据
							//创建初始数据到临时表，所有数量均为0
							sql="insert into tbSellDayCheckDetailTmp ";
							sql+="select '"+strSerial+"',b.cnvcMaterialCode,b.cnvcMaterialName,b.cnnPrice*b.cnnConversion,b.cnvcStandardUnit as cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbStorage a,tbMaterial b where a.cnvcProductCode=b.cnvcMaterialCode and b.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductClass='"+htpara["strPClass"].ToString()+"' and a.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							sql+=" union select '"+strSerial+"',cnvcMaterialCode,cnvcMaterialName,cnnPrice*cnnConversion,cnvcStandardUnit as cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbMaterial where cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and cnvcMaterialCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新期初库存，取最近一次盘点的库存
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnOriginalStorage=cast(b.cnnCount/d.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbStorageLog b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c,tbMaterial d ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductCode=c.cnvcProductCode and b.cnnSerialNo=c.maxSerial and d.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductCode=d.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								//更新进仓量，如果是生产中心，进仓量只取采购量
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfEnterStorageLog a,tbBillOfEnterStorageDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnEnterSerialNo=b.cnnEnterSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndEnterDate,112)>c.cndOperDate and cnvcEnterType='采购入库' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								//更新进仓量，如果是其它门店，进仓量只取领用入库量
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新生产中心领用出的量，
							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								sql="update tbSellDayCheckDetailTmp set cnnReceiveCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新调拨出量，原材料不能调拨，只能领用

							//更新调拨入量，原材料不能调拨，只能领用

							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								//更新损耗量，生产中心只有生产损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where a.cnvcProductCode=b.cnvcProductCode and convert(char(8),cndLoseDate,112)>b.cndOperDate and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType='CA04' group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								//更新损耗量，门店损耗包括：生产损耗，运输损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount  from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where a.cnvcProductCode=b.cnvcProductCode and convert(char(8),cndLoseDate,112)>b.cndOperDate and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType in('DA02','DA04') group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新损耗量单位为规格单位
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnLoseCount=cast(cnnLoseCount/b.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbMaterial b where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);							

							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								sql="";
								//更新售卖量，原材料不能售卖

								//更新剩余量，生产中心需要剩余量回入
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfEnterStorageLog a,tbBillOfEnterStorageDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnEnterSerialNo=b.cnnEnterSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndEnterDate,112)>c.cndOperDate and cnvcEnterType='剩余回入' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								sql="";
								//更新使用量，生产中心使用量为：生产中心领用出库-生产中心剩余回入
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=b.cnnCount-cnnFreeCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								sql="";
								//更新售卖量，原材料不能售卖

								//更新使用量，门店使用量只有自生产中的原材料出
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cast(a.cnnCount/c.cnnStatdardCount as numeric(10,2))) as cnnCount from tbStorageLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b,tbMaterial c where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),max(a.cndOperDate),112)>b.cndOperDate and cnvcOperType='DA03' and c.cnvcProductType='"+htpara["strPType"].ToString()+"' and a.cnvcProductCode=c.cnvcMaterialCode group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								//更新使用量单位为规格单位
								sql="";
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=cast(cnnUseCount/b.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbMaterial b where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcMaterialCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);	

								sql="";
								//更新剩余量
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=(cnnOriginalStorage+cnnOrderCount+cnnMoveInCount-cnnMoveOutCount-cnnLoseCount-cnnUseCount-cnnSellCount)";
								sql+=" where cnvcCheckID='"+strSerial+"'";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新系统库存，取系统当前库存
							//更新实际库存量，理论上与系统库存相等
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnSystemCount=cast(b.cnnCount/c.cnnStatdardCount as numeric(10,2)),cnnRealCount=cast(b.cnnCount/c.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbStorage b,tbMaterial c";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and c.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductCode=c.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							break;
							#endregion
						//材料
						case "Pack":
							#region 生成材料库存盘点数据
							//创建初始数据到临时表，所有数量均为0
							sql="insert into tbSellDayCheckDetailTmp ";
							sql+="select '"+strSerial+"',b.cnvcMaterialCode,b.cnvcMaterialName,b.cnnPrice*b.cnnConversion,b.cnvcStandardUnit as cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbStorage a,tbMaterial b where a.cnvcProductCode=b.cnvcMaterialCode and b.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductClass='"+htpara["strPClass"].ToString()+"' and a.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							sql+=" union select '"+strSerial+"',cnvcMaterialCode,cnvcMaterialName,cnnPrice*cnnConversion,cnvcStandardUnit as cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbMaterial where cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and cnvcMaterialCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新期初库存，取前一天的最终库存，即前一天的盘点库存
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnOriginalStorage=cast(b.cnnCount/d.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbStorageLog b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c,tbMaterial d ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductCode=c.cnvcProductCode and b.cnnSerialNo=c.maxSerial and d.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductCode=d.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								//更新进仓量，如果是生产中心，进仓量取采购量,，工具类材料需要归还入库
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfEnterStorageLog a,tbBillOfEnterStorageDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnEnterSerialNo=b.cnnEnterSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndEnterDate,112)>c.cndOperDate and cnvcEnterType in('采购入库','工具类材料归还') group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								//更新进仓量，如果是其它门店，进仓量只取领用入库量
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新生产中心领用出的量，
							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								sql="update tbSellDayCheckDetailTmp set cnnReceiveCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新调拨出量，原材料不能调拨，只能领用

							//更新调拨入量，原材料不能调拨，只能领用

							sql="";
							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								//更新损耗量，生产中心只有生产损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where a.cnvcProductCode=b.cnvcProductCode and convert(char(8),cndLoseDate,112)>b.cndOperDate and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType='CA04' group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								//更新损耗量，门店损耗包括：生产损耗，运输损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount  from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where a.cnvcProductCode=b.cnvcProductCode and convert(char(8),cndLoseDate,112)>b.cndOperDate and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType in('DA02','DA04') group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新损耗量单位为规格单位
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnLoseCount=cast(cnnLoseCount/b.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbMaterial b where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);	

							if(htpara["strDeptID"].ToString()=="FYZX1")
							{
								sql="";
								//更新售卖量，原材料不能售卖

								//更新剩余量，生产中心需要剩余量回入
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfEnterStorageLog a,tbBillOfEnterStorageDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnEnterSerialNo=b.cnnEnterSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndEnterDate,112)>c.cndOperDate and cnvcEnterType='剩余回入' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								sql="";
								//更新使用量，生产中心使用量为：生产中心领用出库-生产中心剩余回入
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=b.cnnCount-cnnFreeCount from tbSellDayCheckDetailTmp a,";
								sql+="(select b.cnvcProductCode,sum(cnnCount) as cnnCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndReceiveDate,112)>c.cndOperDate and cnvcReceiveDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}
							else
							{
								sql="";
								//更新售卖量，原材料不能售卖

								//更新使用量，门店使用量只有自生产中的原材料出
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cast(a.cnnCount/c.cnnStatdardCount as numeric(10,2))) as cnnCount from tbStorageLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b,tbMaterial c where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndOperDate,112)>b.cndOperDate and cnvcOperType='DA03' and c.cnvcProductType='"+htpara["strPType"].ToString()+"' and a.cnvcProductCode=c.cnvcMaterialCode group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								//更新使用量单位为规格单位
								sql="";
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=cast(cnnUseCount/b.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbMaterial b where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcMaterialCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);	

								sql="";
								//更新剩余量
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=(cnnOriginalStorage+cnnOrderCount+cnnMoveInCount-cnnMoveOutCount-cnnLoseCount-cnnUseCount-cnnSellCount)";
								sql+=" where cnvcCheckID='"+strSerial+"'";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新系统库存，取系统当前库存
							//更新实际库存量，理论上与系统库存相等
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnSystemCount=cast(b.cnnCount/c.cnnStatdardCount as numeric(10,2)),cnnRealCount=cast(b.cnnCount/c.cnnStatdardCount as numeric(10,2)) from tbSellDayCheckDetailTmp a,tbStorage b,tbMaterial c";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and c.cnvcProductType='"+htpara["strPType"].ToString()+"' and b.cnvcProductCode=c.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							break;
							#endregion
						//半成品
						case "SEMIPRODUCT":
							#region 生成半成品库存盘点数据
							//创建初始数据到临时表，所有数量均为0
							sql="insert into tbSellDayCheckDetailTmp ";
							sql+="select '"+strSerial+"',b.cnvcProductCode,b.cnvcProductName,0,b.cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbStorage a,tbFormula b where a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and a.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							sql+=" union select '"+strSerial+"',cnvcProductCode,cnvcProductName,0,cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbFormula where cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新期初库存，取前一天的最终库存，即前一天的盘点库存
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnOriginalStorage=b.cnnCount from tbSellDayCheckDetailTmp a,tbStorageLog b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductCode=c.cnvcProductCode and b.cnnSerialNo=c.maxSerial";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								//更新进仓量，如果是其它门店，进仓量只取订单验收入库
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnCount) as cnnCount from tbStorageLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='DB01' and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndOperDate,112)>b.cndOperDate group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新调拨出量
							sql="update tbSellDayCheckDetailTmp set cnnMoveOutCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
							sql+="(select b.cnvcProductCode,sum(cnnRealMoveCount) as cnnCount from tbMoveLog a,tbMoveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnMoveSerialNo=b.cnnMoveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndMoveDate,112)>c.cndOperDate and a.cnvcOutDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新调拨入量
							sql="update tbSellDayCheckDetailTmp set cnnMoveInCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
							sql+="(select b.cnvcProductCode,sum(cnnRealMoveCount) as cnnCount from tbMoveLog a,tbMoveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnMoveSerialNo=b.cnnMoveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndMoveDate,112)>c.cndOperDate and a.cnvcInDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								//更新损耗量，门店损耗包括：生产损耗，运输损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount  from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType in('DB02','DB03') and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndLoseDate,112)>b.cndOperDate group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								sql="";
								//更新售卖量，半成品不可售卖

								//更新使用量，门店使用量只有自生产中的半成品出
								sql="update tbSellDayCheckDetailTmp set cnnUseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnCount) as cnnCount from tbStorageLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='DB04' and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndOperDate,112)>b.cndOperDate group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								sql="";
								//更新剩余量，
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=(cnnOriginalStorage+cnnOrderCount+cnnMoveInCount-cnnMoveOutCount-cnnLoseCount-cnnUseCount-cnnSellCount)";
								sql+=" where cnvcCheckID='"+strSerial+"'";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新系统库存，取系统当前库存
							//更新实际库存量，理论上与系统库存相等
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnSystemCount=b.cnnCount,cnnRealCount=b.cnnCount from tbSellDayCheckDetailTmp a,tbStorage b";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							break;
							#endregion
						//成品
						case "FINALPRODUCT":
							#region 生成成品库存盘点数据
							//创建初始数据到临时表，所有数量均为0
							sql="insert into tbSellDayCheckDetailTmp ";
							sql+="select '"+strSerial+"',b.cnvcProductCode,b.cnvcProductName,0,b.cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbStorage a,tbFormula b where a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and a.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							sql+=" union select '"+strSerial+"',cnvcProductCode,cnvcProductName,0,cnvcUnit,0,0,0,0,0,0,0,0,0,0,0,0,0 from tbFormula where cnvcProductType='"+htpara["strPType"].ToString()+"' and cnvcProductClass='"+htpara["strPClass"].ToString()+"' and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新商品单价
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnProductPrice=b.nPrice from tbSellDayCheckDetailTmp a,tbGoods b";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.vcGoodsID";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新期初库存，取前一天的最终库存，即前一天的盘点库存
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnOriginalStorage=b.cnnCount from tbSellDayCheckDetailTmp a,tbStorageLog b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductCode=c.cnvcProductCode and b.cnnSerialNo=c.maxSerial";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								//更新进仓量，如果是其它门店，进仓量为订单验收入库，成品入
								sql="update tbSellDayCheckDetailTmp set cnnOrderCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnCount) as cnnCount from tbStorageLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType in('DC01','DC03') and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndOperDate,112)>b.cndOperDate group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新调拨出量
							sql="update tbSellDayCheckDetailTmp set cnnMoveOutCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
							sql+="(select b.cnvcProductCode,sum(cnnRealMoveCount) as cnnCount from tbMoveLog a,tbMoveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnMoveSerialNo=b.cnnMoveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndMoveDate,112)>c.cndOperDate and a.cnvcOutDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							//更新调拨入量
							sql="update tbSellDayCheckDetailTmp set cnnMoveInCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
							sql+="(select b.cnvcProductCode,sum(cnnRealMoveCount) as cnnCount from tbMoveLog a,tbMoveDetail b,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) c where a.cnnMoveSerialNo=b.cnnMoveSerialNo and b.cnvcProductCode=c.cnvcProductCode and convert(char(8),cndMoveDate,112)>c.cndOperDate and a.cnvcInDeptID='"+htpara["strDeptID"].ToString()+"' group by b.cnvcProductCode) b ";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

							sql="";
							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								//更新损耗量，门店损耗包括：运输损耗，销售中损耗,销售剩余损耗
								sql="update tbSellDayCheckDetailTmp set cnnLoseCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select a.cnvcProductCode,sum(cnnLoseCount) as cnnCount  from tbSellLoseLog a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcLoseType in('DC02','DC05','DC08') and a.cnvcProductCode=b.cnvcProductCode and convert(char(8),a.cndLoseDate,112)>b.cndOperDate group by a.cnvcProductCode) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							if(htpara["strDeptID"].ToString()!="FYZX1")
							{
								sql="";
								//更新售卖量
								sql="update tbSellDayCheckDetailTmp set cnnSellCount=b.cnnCount from tbSellDayCheckDetailTmp a,";
								sql+="(select vcGoodsID,sum(iCount) as cnnCount from tbConsItem a,(select cnvcProductCode,max(cnnSerialNo) as maxSerial,convert(char(8),max(cndOperDate),112) as cndOperDate from tbStorageLog where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcOperType='SC99' group by cnvcProductCode) b where vcDeptID in('"+htpara["strOldDept"].ToString()+"') and convert(char(8),dtConsDate,112)>b.cndOperDate and a.vcGoodsID=b.cnvcProductCode group by a.vcGoodsID) b ";
								sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.vcGoodsID";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

								//更新使用量，门店使用量无，门店只有售卖

								sql="";
								//更新剩余量，
								sql="update tbSellDayCheckDetailTmp set cnnFreeCount=(cnnOriginalStorage+cnnOrderCount+cnnMoveInCount-cnnMoveOutCount-cnnLoseCount-cnnUseCount-cnnSellCount)";
								sql+=" where cnvcCheckID='"+strSerial+"'";
								SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							}

							//更新系统库存，取系统当前库存
							//更新实际库存量，理论上与系统库存相等
							sql="";
							sql="update tbSellDayCheckDetailTmp set cnnSystemCount=b.cnnCount,cnnRealCount=b.cnnCount from tbSellDayCheckDetailTmp a,tbStorage b";
							sql+=" where a.cnvcCheckID='"+strSerial+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"'";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
							break;
							#endregion
					}

					//更新差异量
					sql="";
					sql="update tbSellDayCheckDetailTmp set cnnDifferentCount=cnnSystemCount-cnnRealCount where cnvcCheckID='"+strSerial+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

					//取出临时表中生成的库存明细
					sql="";
					sql="select * from tbSellDayCheckDetailTmp where cnvcCheckID='"+strSerial+"' order by cnvcProductCode";
					dttmp=SqlHelper.ExecuteDataTable(con,tran,CommandType.Text,sql);
					dttmp.Columns.Remove("cnvcCheckID");

					//删除临时表中当前数据
					sql="";
					sql="delete from tbSellDayCheckDetailTmp where cnvcCheckID='"+strSerial+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);
					
					tran.Commit();
					return dttmp;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return null;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int DayCheckFinal(Hashtable htpara,DataTable dtIn)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbSellDayCheckLog values('"+htpara["strDeptID"].ToString()+"','"+htpara["strDate"].ToString()+"','"+htpara["strWeather"].ToString()+"','"+htpara["strCheckOper"].ToString()+"','"+htpara["strManager"].ToString()+"','"+htpara["strOperName"].ToString()+"','"+htpara["strOperDate"].ToString()+"');SELECT scope_identity()";
					SqlDataReader dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					dr.Read();
					string strSerial=dr[0].ToString();
					dr.Close();
					if(strSerial=="")
					{
						tran.Rollback();
						return 0;
					}
					else
					{
						string sql3="";
						string strProductName="";
						foreach(DataRow drtmp in dtIn.Rows)
						{
							strProductName="";
							strProductName=drtmp["cnvcProductName"].ToString().Replace("'","''");
							sql3+="insert into tbSellDayCheckDetail values("+strSerial+",'"+drtmp["cnvcProductCode"].ToString()+"','"+strProductName+"',"+drtmp["cnnProductPrice"].ToString()+",'"+drtmp["cnvcUnit"].ToString()+"',"+drtmp["cnnOriginalStorage"].ToString()+","+drtmp["cnnOrderCount"].ToString()+","+drtmp["cnnMoveOutCount"].ToString()+","+drtmp["cnnMoveInCount"].ToString()+","+drtmp["cnnLoseCount"].ToString()+","+drtmp["cnnFreeCount"].ToString()+","+drtmp["cnnUseCount"].ToString()+","+drtmp["cnnSellCount"].ToString()+","+drtmp["cnnSystemCount"].ToString()+","+drtmp["cnnRealCount"].ToString()+","+drtmp["cnnDifferentCount"].ToString()+","+drtmp["cnnDifferentSum"].ToString()+","+drtmp["cnnReceiveCount"].ToString()+");";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						string sql4="";
						string sql5="";
						string sql6="";
						if(htpara["strProductType"].ToString()=="Raw"||htpara["strProductType"].ToString()=="Pack")
						{
							sql4="update tbStorage set cnnCount=cast(a.cnnRealCount*c.cnnStatdardCount as numeric(12,2)),cnvcUnit=c.cnvcUnit from tbSellDayCheckDetail a,tbStorage b,tbMaterial c where a.cnnCheckSerialNo="+strSerial+" and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and c.cnvcProductType='"+htpara["strProductType"].ToString()+"' and b.cnvcProductCode=c.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

							sql5="insert into tbStorage select '"+htpara["strDeptID"].ToString()+"',a.cnvcProductCode,a.cnvcProductName,cnvcUnit=b.cnvcUnit,cast(a.cnnRealCount*b.cnnStatdardCount as numeric(12,2)),0,0 from tbSellDayCheckDetail a,tbMaterial b where cnnCheckSerialNo="+strSerial+"  and a.cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"') and b.cnvcProductType='"+htpara["strProductType"].ToString()+"' and a.cnvcProductCode=b.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);

							sql6="insert into tbStorageLog select '"+htpara["strDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,b.cnvcUnit,cast(a.cnnRealCount*b.cnnStatdardCount as numeric(12,2)),0,0,'SC99','"+htpara["strOperName"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbSellDayCheckDetail a,tbMaterial b where cnnCheckSerialNo="+strSerial+" and b.cnvcProductType='"+htpara["strProductType"].ToString()+"' and a.cnvcProductCode=b.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);
						}
						else if(htpara["strProductType"].ToString()=="SEMIPRODUCT"||htpara["strProductType"].ToString()=="FINALPRODUCT")
						{
							sql4="update tbStorage set cnnCount=a.cnnRealCount from tbSellDayCheckDetail a,tbStorage b where a.cnnCheckSerialNo="+strSerial+" and b.cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

							sql5="insert into tbStorage select '"+htpara["strDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnRealCount,0,0 from tbSellDayCheckDetail where cnnCheckSerialNo="+strSerial+"  and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);

							sql6="insert into tbStorageLog select '"+htpara["strDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnRealCount,0,0,'SC99','"+htpara["strOperName"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbSellDayCheckDetail where cnnCheckSerialNo="+strSerial+"";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);
						}

						tran.Commit();
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetLoseDaySale(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="select cnnLoseSerialNo,cnvcProductName,convert(char(10),cndLoseDate,120) as cndLoseDate,cnvcWeather,cnnLoseCount,cnvcUnit,cnvcLoseComments,cnvcOperID,cndLoseOperDate,(case cnvcDestroyFlag when '0' then '未确认' when '1' then '已确认' else cnvcDestroyFlag end) as cnvcDestroyFlag from tbSellLoseLog where cnvcDeptID='"+htPara["strDeptID"].ToString()+"' and cndLoseDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and cnvcLoseType='"+htPara["strLoseType"].ToString()+"'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetProductInfoByPClass(string strPClass)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="select cnvcProductCode as cnvcCode,cnvcProductName as cnvcName,cnvcUnit from tbFormula where cnvcProductType='FINALPRODUCT' and cnvcProductClass='"+strPClass+"'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetMaterialInfoByProvider(string strProvider,string strFilter)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="select distinct cnvcProductCode as cnvcCode,cnvcProductName as cnvcName from tbProvider where cnvcProviderCode='"+strProvider+"' and cnvcProductName like '%"+strFilter+"%'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataSet GetMaterialProviderByFilter(string strFilter)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select distinct cnvcProviderCode as cnvcCode,cnvcProviderName as cnvcName from tbProvider where cnvcProductName like '%"+ strFilter +"%'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="provider";
				dsout.Tables.Add(dtout);

				if(dtout.Rows.Count>0)
				{
					string sql1="select distinct cnvcProductCode as cnvcCode,cnvcProductName as cnvcName from tbProvider where cnvcProviderCode='"+dtout.Rows[0]["cnvcCode"].ToString()+"' and cnvcProductName like '%"+ strFilter +"%'"; 
					dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
					dtout.TableName="material";
					dsout.Tables.Add(dtout);
				}
				else
				{
					dtout=new DataTable();
					dtout.TableName="material";
					dtout.Columns.Add("cnvcCode");
					dtout.Columns.Add("cnvcName");
					dsout.Tables.Add(dtout);
				}
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int NewSaleLoseAdd(DataTable dtLoseList)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					foreach(DataRow drtmp in dtLoseList.Rows)
					{
						sql1+="insert into tbSellLoseLog values('"+drtmp["cnvcDeptID"].ToString()+"','"+drtmp["cndLoseDate"].ToString()+"','"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"',"+drtmp["cnnLoseCount"].ToString()+",'"+drtmp["cnvcUnit"].ToString()+"','"+drtmp["cnvcLoseComments"].ToString()+"','"+drtmp["cnvcWeather"].ToString()+"','"+drtmp["cnvcLoseType"].ToString()+"','0','"+drtmp["cnvcOperID"].ToString()+"','"+drtmp["cndLoseOperDate"].ToString()+"','',null);";
					}

					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int SaleLoseDelete(string strSerialNo)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="delete from tbSellLoseLog where cnnLoseSerialNo="+strSerialNo;
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetProvider(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				string sql="";
				if(htPara["strProviderID"].ToString()!="")
				{
					strCondition=" cnvcPrvdCode='"+htPara["strProviderID"].ToString()+"' ";
				}
				
				if(htPara["strProviderName"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" cnvcPrvdName like '%"+htPara["strProviderName"].ToString()+"%' ";
					}
					else
					{
						strCondition+=" and cnvcPrvdName like '%"+htPara["strProviderName"].ToString()+"%' ";
					}
				}
				if(strCondition=="")
				{
					sql="select * from tbProviderNew order by cnvcPrvdName";
				}
				else
				{
					sql="select * from tbProviderNew where "+strCondition+" order by cnvcPrvdName";
				}

				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetProviderDetailOne(string strProviderCode)
		{
			DataTable dtProvider=new DataTable();
			try
			{
				string sql1="select * from tbProviderNew where cnvcPrvdCode='" + strProviderCode + "'";
				dtProvider=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtProvider;
		}

		public int NewProviderAdd(CMSMStruct.ProviderStruct ps1)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select isnull(max(cast(substring(cnvcPrvdCode,2,5) as int)),0) from tbProviderNew";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					int maxid=int.Parse(drr[0].ToString());
					drr.Close();

					maxid++;
					if(maxid<10)
						ps1.strPrvdCode="P"+"0000"+maxid.ToString();
					else if(maxid>=10&&maxid<100)
						ps1.strPrvdCode="P"+"000"+maxid.ToString();
					else if(maxid>=100&&maxid<1000)
						ps1.strPrvdCode="P"+"00"+maxid.ToString();
					else if(maxid>=1000&&maxid<10000)
						ps1.strPrvdCode="P"+"0"+maxid.ToString();
					else if(maxid>=10000&&maxid<100000)
						ps1.strPrvdCode="P"+maxid.ToString();
					else
						return 0;

					string sql2="insert into tbProviderNew values('"+ps1.strPrvdCode+"','"+ps1.strPrvdName+"','"+ps1.strPrvdAbbName+"','','"+ps1.strAddress+"','"+ps1.strPostCode+"','"+ps1.strPrvdPhone+"','"+ps1.strPrvdFax+"','"+ps1.strPrvdEmail+"','"+ps1.strPrvdLinkName+"',null,0,'"+ps1.strPrvdCredit+"','"+ps1.strPrvdQualification+"','"+ps1.strPrvdCreater+"',getdate(),'"+ps1.strActiveFlag+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int IsExistProviderProduct(string strProviderCode,string strProductCode)
		{
			int existcount=0;
			try
			{
				string sql1="select count(*) from tbProvider where cnvcProviderCode='" + strProviderCode + "' and cnvcProductCode='" +strProductCode+ "'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				existcount=int.Parse(drr[0].ToString());
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return existcount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return existcount;
		}

		public string IsExistProvider(string strProviderName)
		{
			string strProvidercount="";
			try
			{
				string sql1="select count(*) from tbProviderNew where cnvcPrvdName='" + strProviderName + "'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strProvidercount=drr[0].ToString();
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return "";
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strProvidercount;
		}

		public int ModProviderInfo(CMSMStruct.ProviderStruct psnew,CMSMStruct.ProviderStruct psold)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string strCondition="";
					if(psnew.strPrvdName!=psold.strPrvdName)
					{
						strCondition="cnvcPrvdName='"+psnew.strPrvdName+"'";
					}
					if(psnew.strPrvdAbbName!=psold.strPrvdAbbName)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdAbbName="+psnew.strPrvdAbbName;
						}
						else
						{
							strCondition+=",cnvcPrvdAbbName="+psnew.strPrvdAbbName;
						}
					}
					if(psnew.strAddress!=psold.strAddress)
					{
						if(strCondition=="")
						{
							strCondition="cnvcAddress='"+psnew.strAddress+"'";
						}
						else
						{
							strCondition+=",cnvcAddress='"+psnew.strAddress+"'";
						}
					}
					if(psnew.strPostCode!=psold.strPostCode)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPostCode='"+psnew.strPostCode+"'";
						}
						else
						{
							strCondition+=",cnvcPostCode='"+psnew.strPostCode+"'";
						}
					}
					if(psnew.strPrvdPhone!=psold.strPrvdPhone)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdPhone='"+psnew.strPrvdPhone+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdPhone='"+psnew.strPrvdPhone+"'";
						}
					}
					if(psnew.strPrvdFax!=psold.strPrvdFax)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdFax='"+psnew.strPrvdFax+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdFax='"+psnew.strPrvdFax+"'";
						}
					}
					if(psnew.strPrvdEmail!=psold.strPrvdEmail)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdEmail='"+psnew.strPrvdEmail+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdEmail='"+psnew.strPrvdEmail+"'";
						}
					}
					if(psnew.strPrvdLinkName!=psold.strPrvdLinkName)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdLinkName='"+psnew.strPrvdLinkName+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdLinkName='"+psnew.strPrvdLinkName+"'";
						}
					}
					if(psnew.strPrvdCredit!=psold.strPrvdCredit)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdCredit='"+psnew.strPrvdCredit+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdCredit='"+psnew.strPrvdCredit+"'";
						}
					}
					if(psnew.strPrvdQualification!=psold.strPrvdQualification)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPrvdQualification='"+psnew.strPrvdQualification+"'";
						}
						else
						{
							strCondition+=",cnvcPrvdQualification='"+psnew.strPrvdQualification+"'";
						}
					}
					if(psnew.strActiveFlag!=psold.strActiveFlag)
					{
						if(strCondition=="")
						{
							strCondition="cnvcActiveFlag='"+psnew.strActiveFlag+"'";
						}
						else
						{
							strCondition+=",cnvcActiveFlag='"+psnew.strActiveFlag+"'";
						}
					}
					if(strCondition!="")
					{
						string sql1="update tbProviderNew set "+strCondition+" where cnvcPrvdCode='"+psnew.strPrvdCode+"'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetBillOfEnterStorage(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="";
				if(htPara["strProviderID"].ToString()=="全部")
				{
					sql="select a.cnnEnterSerialNo,a.cnvcProviderCode,b.cnvcProviderName,a.cndEnterDate,a.cnvcDeliverMan,a.cnvcValidateOperID,a.cnvcSafeOperID,a.cnvcStorageOperID,a.cnvcBillOperID,a.cnvcEnterType from tbBillOfEnterStorageLog a,(select distinct cnvcProviderCode,cnvcProviderName from tbProvider) b where a.cndEnterDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnvcProviderCode=b.cnvcProviderCode";
				}
				else
				{
					sql="select a.cnnEnterSerialNo,a.cnvcProviderCode,b.cnvcProviderName,a.cndEnterDate,a.cnvcDeliverMan,a.cnvcValidateOperID,a.cnvcSafeOperID,a.cnvcStorageOperID,a.cnvcBillOperID,a.cnvcEnterType from tbBillOfEnterStorageLog a,(select distinct cnvcProviderCode,cnvcProviderName from tbProvider) b where a.cnvcProviderCode='"+htPara["strProviderID"].ToString()+"' and a.cndEnterDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnvcProviderCode=b.cnvcProviderCode";
				}
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataSet GetBillOfEnterStorageOneLog(string strSerial)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select * from tbBillOfEnterStorageLog where cnnEnterSerialNo="+strSerial;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="EnterLog";
				dsout.Tables.Add(dtout);

				dtout=new DataTable();
				sql="select a.cnvcProviderCode,b.cnvcProviderName,cnvcProductCode,cnvcProductName,cnvcStandardUnit,cnnStandardCount,cnvcUnit,cnnPrice,cnnCount,cnnSum from tbBillOfEnterStorageDetail a,(select distinct cnvcProviderCode,cnvcProviderName from tbProvider) b where cnnEnterSerialNo="+strSerial+" and a.cnvcProviderCode=b.cnvcProviderCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="EnterDetail";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int NewBillOfEnterStorageAdd(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbBillOfEnterStorageLog values('"+htpara["strProviderCode"].ToString()+"','"+htpara["strDate"].ToString()+"','"+htpara["strDeliverMan"].ToString()+"','"+htpara["strValidateOperID"].ToString()+"','"+htpara["strSafeOperID"].ToString()+"','"+htpara["strStorageOperID"].ToString()+"','"+htpara["strBillOperID"].ToString()+"','"+htpara["strEnterType"].ToString()+"');SELECT scope_identity()";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strserial=drr[0].ToString();
					drr.Close();
					
					if(strserial=="")
					{
						tran.Rollback();
						return 0;
					}
					else
					{
						string sql3="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							sql3+="insert into tbBillOfEnterStorageDetail values("+strserial+",'"+drtmp["cnvcProviderCode"].ToString()+"','"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"','"+drtmp["cnvcStandardUnit"].ToString()+"',"+drtmp["cnnStandardCount"].ToString()+",'"+drtmp["cnvcUnit"].ToString()+"',"+drtmp["cnnPrice"].ToString()+","+drtmp["cnnCount"].ToString()+","+drtmp["cnnSum"].ToString()+");";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						string sql4="update tbStorage set cnnCount=a.cnnCount+(b.cnnCount*b.cnnStandardCount) from tbStorage a,tbBillOfEnterStorageDetail b where a.cnvcStorageDeptID='FYZX1' and a.cnvcProductCode=b.cnvcProductCode and b.cnnEnterSerialNo="+strserial;
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						string sql5="insert into tbStorage select 'FYZX1',cnvcProductCode,cnvcProductName,cnvcUnit,cnnCount*cnnStandardCount,0,0 from tbBillOfEnterStorageDetail where cnnEnterSerialNo="+strserial+" and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='FYZX1')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);

						string sql6="insert into tbStorageLog select 'FYZX1',cnvcProductCode,cnvcProductName,cnvcUnit,cnnCount*cnnStandardCount,0,0,'"+htpara["strStorageOperType"].ToString()+"','"+htpara["strOperID"].ToString()+"','"+htpara["strSysTime"].ToString()+"' from tbBillOfEnterStorageDetail where cnnEnterSerialNo="+strserial;
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);

						tran.Commit();
						return 1;
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetProductMoveLog(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strOutDept"].ToString()!="全部")
				{
					strCondition="cnvcOutDeptID='"+htPara["strOutDept"].ToString()+"'";
				}
				if(htPara["strInDept"].ToString()!="全部")
				{
					if(strCondition=="")
					{
						strCondition="cnvcInDeptID='"+htPara["strInDept"].ToString()+"'";
					}
					else
					{
						strCondition+=" and cnvcInDeptID='"+htPara["strInDept"].ToString()+"'";
					}
				}

				string sql="";
				if(strCondition=="")
				{
					sql="select * from tbMoveLog where cndMoveDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' order by cnnMoveSerialNo";
				}
				else
				{
					sql="select * from tbMoveLog where "+strCondition+" and cndMoveDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' order by cnnMoveSerialNo";
				}
				
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataSet GetMoveOneLog(string strSerial)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select cnnMoveSerialNo,cnvcOutDeptID,cnvcOutOperID,cnvcInDeptID,cnvcInOperID,convert(char(10),cndMoveDate,120) as cndMoveDate,cnvcOperID,convert(char(19),cndOperDate,120) as cndOperDate,cnvcBillState from tbMoveLog where cnnMoveSerialNo="+strSerial;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="MoveLog";
				dsout.Tables.Add(dtout);

				dtout=new DataTable();
				sql="select cnvcProductCode,cnvcProductName,cnvcUnit,cnnMoveCount,0 as cnnLoseCount,cnnRealMoveCount,cnvcComments from tbMoveDetail where cnnMoveSerialNo="+strSerial;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="MoveDetail";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int NewProductMoveAdd(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbMoveLog values('"+htpara["strOutDeptID"].ToString()+"','"+htpara["strOutOperID"].ToString()+"','"+htpara["strInDeptID"].ToString()+"','"+htpara["strInOperID"].ToString()+"','"+htpara["strMoveDate"].ToString()+"','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','0');SELECT scope_identity()";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strserial=drr[0].ToString();
					drr.Close();
					
					if(strserial=="")
					{
						tran.Rollback();
						return 0;
					}
					else
					{
						string sql2="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							sql2+="insert into tbMoveDetail values("+strserial+",'"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"','"+drtmp["cnvcUnit"].ToString()+"',"+drtmp["cnnMoveCount"].ToString()+","+drtmp["cnnRealMoveCount"].ToString()+",'"+drtmp["cnvcComments"].ToString()+"');";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

						string sql3="update tbStorage set cnnCount=a.cnnCount-b.cnnMoveCount from tbStorage a,tbMoveDetail b where a.cnvcStorageDeptID='"+htpara["strOutDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnnMoveSerialNo="+strserial;
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						string sql4="insert into tbStorageLog select '"+htpara["strOutDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,-cnnMoveCount,0,0,'DC06','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbMoveDetail where cnnMoveSerialNo="+strserial;
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						tran.Commit();
						return 1;
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int UpdateProductMoveValidEnter(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
						string sql1="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							sql1+="update tbMoveDetail set cnnRealMoveCount="+drtmp["cnnRealMoveCount"].ToString()+" where cnnMoveSerialNo="+htpara["MoveID"].ToString()+" and cnvcProductCode='"+drtmp["cnvcProductCode"].ToString()+"' and cnvcProductName='"+drtmp["cnvcProductName"].ToString()+"';"; 
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

						string sql2="update tbMoveLog set cnvcBillState='1' where cnnMoveSerialNo="+htpara["MoveID"].ToString();
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

						string sql3="update tbStorage set cnnCount=a.cnnCount+b.cnnMoveCount from tbStorage a,tbMoveDetail b where a.cnvcStorageDeptID='"+htpara["strInDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnnMoveSerialNo="+htpara["MoveID"].ToString();
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						string sql4="insert into tbStorage select '"+htpara["strInDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnRealMoveCount,0,0 from tbMoveDetail where cnnMoveSerialNo="+htpara["MoveID"].ToString()+" and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strInDeptID"].ToString()+"')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						string sql5="insert into tbStorageLog select '"+htpara["strInDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnMoveCount,0,0,'DC07','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbMoveDetail where cnnMoveSerialNo="+htpara["MoveID"].ToString();
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);

						string sql6="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							if(Math.Round(double.Parse(drtmp["cnnLoseCount"].ToString()),2)>0)
							{
								sql6+="insert into tbSellLoseLog values("+htpara["MoveID"].ToString()+",'"+htpara["strInDeptID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"',"+drtmp["cnnLoseCount"].ToString()+",'"+drtmp["cnvcUnit"].ToString()+"','调拨损耗','','运输损耗','0','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','',null);";
							}
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);

						tran.Commit();
						return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataSet GetStockPlanQuery(string strMonth)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select cnvcProductCode,cnvcProductName,cnvcUnit,cnnPlanCount,'0' as RealCount,'0' as SafeCount,convert(char(10),cndStartDate1,120) as cndStartDate1,cnnCount1,cnnSumFee1,convert(char(10),cndStartDate2,120) as cndStartDate2,cnnCount2,cnnSumFee2,";
				sql+="convert(char(10),cndStartDate3,120) as cndStartDate3,cnnCount3,cnnSumFee3,convert(char(10),cndStartDate4,120) as cndStartDate4,cnnCount4,cnnSumFee4,cnnSumFee from tbStockPlan where cnvcMonth='"+strMonth+"' order by cnvcProductCode,cnvcProductName";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="PlanSum";
				dsout.Tables.Add(dtout);

				sql="select cnvcProductCode,cnvcProductName,cnvcUnit,sum(cnnCount) as cnnCount,max(cnnSafeCount) as cnnSafeCount from tbStorage group by cnvcProductCode,cnvcProductName,cnvcUnit order by cnvcProductCode,cnvcProductName,cnvcUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="CurStorageSum";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public DataTable GetPlanDeptDetail(Hashtable htpara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql1="select cnvcProductCode,cnvcProductName,cnnPlanCount,cnvcUnit as cnvcStandardUnit,cnvcMonth,cnvcPlanDeptID,cnvcPlanDeptID as cnvcPlanDeptName,'1' as DelFlag from tbStockPlanDetail where cnvcPlanDeptID='" + htpara["strDeptID"].ToString() + "' and cnvcMonth='" +htpara["strMonth"].ToString()+ "'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int NewStockPlanAdd(DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					string strMonth=dtDetail.Rows[0]["cnvcMonth"].ToString();
					foreach(DataRow drtmp in dtDetail.Rows)
					{
						if(drtmp["DelFlag"].ToString()=="0")
						{
							sql1+="insert into tbStockPlanDetail values('"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"',"+drtmp["cnnPlanCount"].ToString()+",'"+drtmp["cnvcStandardUnit"].ToString()+"','"+drtmp["cnvcMonth"].ToString()+"','"+drtmp["cnvcPlanDeptID"].ToString()+"');";
						}
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="insert into tbStockPlan (cnvcProductCode,cnvcProductName,cnnPlanCount,cnvcUnit,cnvcMonth) select cnvcProductCode,cnvcProductName,sum(cnnPlanCount),cnvcUnit,cnvcMonth from tbStockPlanDetail where cnvcMonth='"+strMonth+"' and cnvcProductCode not in(select distinct cnvcProductCode from tbStockPlan where cnvcMonth='"+strMonth+"') group by cnvcProductCode,cnvcProductName,cnvcUnit,cnvcMonth";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="update tbStockPlan set cnnPlanCount=b.sumcount from tbStockPlan a,(select cnvcProductCode,cnvcProductName,cnvcUnit,sum(cnnPlanCount) as sumcount from tbStockPlanDetail where cnvcMonth='"+strMonth+"' group by cnvcProductCode,cnvcProductName,cnvcUnit) b where a.cnvcMonth='"+strMonth+"' and a.cnvcProductCode=b.cnvcProductCode and a.cnvcProductName=b.cnvcProductName and a.cnvcUnit=b.cnvcUnit";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetBillOfReceive(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="";
				string strCondition="";
				if(htPara["strDeptID"].ToString()!="全部")
				{
					strCondition=" and cnvcReceiveDeptID='"+htPara["strDeptID"].ToString()+"' ";
				}
//				if(htPara["strGroup"].ToString()!="全部")
//				{
//					
//					strCondition+=" and cnvcGroup='"+htPara["strGroup"].ToString()+"' ";
//				}
				if(htPara["strOrderState"].ToString()!="全部")
				{
					strCondition=" and cnvcBillState='"+htPara["strOrderState"].ToString()+"' ";
				}

				sql="select * from tbBillOfReceiveLog where (cndReceiveDate is null or cndReceiveDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59') "+strCondition;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataSet GetBillOfReceiveOneLog(string strSerial)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select * from tbBillOfReceiveLog where cnnReceiveSerialNo="+strSerial;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="ReceiveLog";
				dsout.Tables.Add(dtout);

				dtout=new DataTable();
				sql="select cnvcProductCode,cnvcProductName,cnvcStandardUnit,cnnStandardCount,cnvcUnit,cnnClassStorage,cnvcReceiveOperID,cnnReceiveCount,cnnOutCount,cast(0 as numeric(10,2)) as cnnLoseCount,cnnCount from tbBillOfReceiveDetail where cnnReceiveSerialNo="+strSerial;
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="ReceiveDetail";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int NewBillOfReceiveAdd(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbBillOfReceiveLog values('"+htpara["strReceiveDeptID"].ToString()+"','"+htpara["strGroup"].ToString()+"','"+htpara["strReceiveDate"].ToString()+"','"+htpara["strClass"].ToString()+"','"+htpara["strMaterialInchargeOperID"].ToString()+"','"+htpara["strStorageInchargeOperID"].ToString()+"','"+htpara["strSendOperID"].ToString()+"','"+htpara["strBillType"].ToString()+"','0',null,null);SELECT scope_identity()";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strserial=drr[0].ToString();
					drr.Close();
					
					if(strserial=="")
					{
						tran.Rollback();
						return 0;
					}
					else
					{
						string sql3="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							sql3+="insert into tbBillOfReceiveDetail values("+strserial+",'"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"','"+drtmp["cnvcUnit"].ToString()+"','"+drtmp["cnvcStandardUnit"].ToString()+"',"+drtmp["cnnStandardCount"].ToString()+","+drtmp["cnnReceiveCount"].ToString()+","+drtmp["cnnClassStorage"].ToString()+","+drtmp["cnnOutCount"].ToString()+","+drtmp["cnnCount"].ToString()+",'"+drtmp["cnvcReceiveOperID"].ToString()+"');";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						tran.Commit();
						return 1;
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int UpdateBillOfReceiveOut(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
						string sql1="";
						foreach(DataRow drtmp in dtDetail.Rows)
						{
							sql1+="update tbBillOfReceiveDetail set cnnOutCount="+drtmp["cnnOutCount"].ToString()+" where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString()+" and cnvcProductCode='"+drtmp["cnvcProductCode"].ToString()+"' and cnvcProductName='"+drtmp["cnvcProductName"].ToString()+"';";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

//						string sql2="update tbBillOfReceiveLog set cnvcBillState='1' where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
//						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

//						string sql3="update tbStorage set cnnCount=a.cnnCount-(b.cnnOutCount*b.cnnStandardCount) from tbStorage a,tbBillOfReceiveDetail b where a.cnvcStorageDeptID='FYZX1' and a.cnvcProductCode=b.cnvcProductCode and b.cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
//						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);
//					
//						string sql4="insert into tbStorageLog select 'FYZX1',cnvcProductCode,cnvcProductName,cnvcUnit,-(cnnOutCount*cnnStandardCount),0,0,'CA02','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbBillOfReceiveDetail where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
//						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);
						
						tran.Commit();
						return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int UpdateBillOfReceiveValidEnter(Hashtable htpara,DataTable dtDetail)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					foreach(DataRow drtmp in dtDetail.Rows)
					{
						sql1+="update tbBillOfReceiveDetail set cnnCount="+drtmp["cnnCount"].ToString()+" where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString()+" and cnvcProductCode='"+drtmp["cnvcProductCode"].ToString()+"' and cnvcProductName='"+drtmp["cnvcProductName"].ToString()+"';";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbBillOfReceiveLog set cnvcBillState='2' where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					if(htpara["strReceiveDeptID"].ToString()!="FYZX1")
					{
						string sql3="update tbStorage set cnnCount=a.cnnCount+(b.cnnCount*b.cnnStandardCount) from tbStorage a,tbBillOfReceiveDetail b where a.cnvcStorageDeptID='"+htpara["strReceiveDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						string sql4="insert into tbStorage select '"+htpara["strReceiveDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnCount*cnnStandardCount,0,0 from tbBillOfReceiveDetail where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString()+" and cnvcProductCode not in(select distinct cnvcProductCode from tbStorage where cnvcStorageDeptID='"+htpara["strReceiveDeptID"].ToString()+"')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						string sql5="insert into tbStorageLog select '"+htpara["strReceiveDeptID"].ToString()+"',cnvcProductCode,cnvcProductName,cnvcUnit,cnnCount*cnnStandardCount,0,0,'DA01','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbBillOfReceiveDetail where cnnReceiveSerialNo="+htpara["ReceiveID"].ToString();
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);
					}

					string sql6="";
					foreach(DataRow drtmp in dtDetail.Rows)
					{
						if(Math.Round(double.Parse(drtmp["cnnLoseCount"].ToString()),2)>0)
						{
							string losecount=(Math.Round(double.Parse(drtmp["cnnLoseCount"].ToString())*double.Parse(drtmp["cnnStandardCount"].ToString()),2)).ToString();
							sql6+="insert into tbSellLoseLog values("+htpara["ReceiveID"].ToString()+",'"+htpara["strReceiveDeptID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','"+drtmp["cnvcProductCode"].ToString()+"','"+drtmp["cnvcProductName"].ToString()+"',"+losecount+",'"+drtmp["cnvcUnit"].ToString()+"','领用损耗','','运输损耗','0','"+htpara["strOperID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','',null);";
						}
					}
					if(sql6!="")
					{
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);
					}

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetDailySaleChart(string strMonth,string strDept="全部")
		{
			DataTable dtout=new DataTable();
            try
            {
                string sql = "select vcDeptID,convert(char(8),dtConsDate,112) as SaleDay,round(sum(nFee)/10000,2) as SaleFee from tbConsItemOther where convert(char(6),dtConsDate,112)='"
                        + strMonth
                        + "'";
                if (strDept != "全部")
                {
                    sql += " and vcDeptID='" + strDept + "'";
                }
                sql += " group by vcDeptID,convert(char(8),dtConsDate,112) order by vcDeptID,convert(char(8),dtConsDate,112)";
                dtout = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable QueryBillEnterReport(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				string sql="";
				switch(htPara["strQueryType"].ToString())
				{
					case "MoreProviderEnter":
						if(htPara["strProviderID"].ToString()!="")
						{
							strCondition=" and a.cnvcProviderCode='"+htPara["strProviderID"].ToString()+"' ";
						}
						if(htPara["strProviderName"].ToString()!="")
						{
							strCondition+=" and c.cnvcProviderName like '%"+htPara["strProviderName"].ToString()+"%' ";
						}
						if(htPara["strMaterialType"].ToString()!="全部")
						{
							strCondition+=" and d.cnvcProductType = '"+htPara["strMaterialType"].ToString()+"' ";
						}
						if(htPara["strMaterialID"].ToString()!="全部")
						{
							strCondition+=" and a.cnvcProductCode = '"+htPara["strMaterialID"].ToString()+"' ";
						}
							
						sql="select c.cnvcProviderName,convert(char(8),b.cndEnterDate,112) as EnterDay,sum(cnnCount) as EnterCount from tbBillOfEnterStorageDetail a,tbBillOfEnterStorageLog b,tbProvider c,tbMaterial d ";
						sql+=" where b.cnvcEnterType='采购入库' and a.cnnEnterSerialNo=b.cnnEnterSerialNo and a.cnvcProviderCode=c.cnvcProviderCode and a.cnvcProductCode=c.cnvcProductCode and a.cnvcProductCode=d.cnvcMaterialCode and convert(char(6),b.cndEnterDate,112)='"+htPara["strMonth"].ToString()+"' ";
						sql+=strCondition +" group by c.cnvcProviderName,convert(char(8),b.cndEnterDate,112) order by c.cnvcProviderName,convert(char(8),b.cndEnterDate,112)";
						break;
					case "OneProviderEnter":
						if(htPara["strProviderID"].ToString()!="")
						{
							strCondition=" and a.cnvcProviderCode='"+htPara["strProviderID"].ToString()+"' ";
						}
						if(htPara["strProviderName"].ToString()!="")
						{
							strCondition+=" and c.cnvcProviderName like '%"+htPara["strProviderName"].ToString()+"%' ";
						}
						if(htPara["strMaterialType"].ToString()!="全部")
						{
							strCondition+=" and d.cnvcProductType = '"+htPara["strMaterialType"].ToString()+"' ";
						}
						if(htPara["strMaterialID"].ToString()!="全部")
						{
							strCondition+=" and a.cnvcProductCode = '"+htPara["strMaterialID"].ToString()+"' ";
						}
							
						sql="select a.cnvcProductCode,a.cnvcProductName,a.cnvcStandardUnit,a.cnvcUnit,convert(char(8),b.cndEnterDate,112) as EnterDay,sum(cnnCount) as EnterCount from tbBillOfEnterStorageDetail a,tbBillOfEnterStorageLog b,tbProvider c,tbMaterial d ";
						sql+=" where b.cnvcEnterType='采购入库' and a.cnnEnterSerialNo=b.cnnEnterSerialNo and a.cnvcProviderCode=c.cnvcProviderCode and a.cnvcProductCode=c.cnvcProductCode and a.cnvcProductCode=d.cnvcMaterialCode and convert(char(6),b.cndEnterDate,112)='"+htPara["strMonth"].ToString()+"' ";
						sql+=strCondition +" group by a.cnvcProductCode,a.cnvcProductName,a.cnvcStandardUnit,a.cnvcUnit,convert(char(8),b.cndEnterDate,112) order by a.cnvcProductCode,a.cnvcProductName,a.cnvcStandardUnit,a.cnvcUnit,convert(char(8),b.cndEnterDate,112)";
						break;
				}

				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetAssignToValidEnter(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="select a.cnnAssignSerialNo,a.cnnOrderSerialNo,a.cnvcShipDeptID,a.cnvcShipOperID,a.cndShipDate,b.cnvcProductCode,b.cnvcProductName,c.cnvcProductType,b.cnvcUnit,b.cnnOrderCount,b.cnnCount,'' as cnnTravelCount,'' as cnnValidOkCount,'' as cnnValidNoCount from tbAssignLog a,tbAssignDetail b,tbFormula c where a.cnnAssignSerialNo=b.cnnAssignSerialNo and a.cnnOrderSerialNo=b.cnnOrderSerialNo and a.cnvcReceiveDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnnAssignSerialNo="+htPara["strAssignID"].ToString()+" and a.cnnOrderSerialNo in(select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='2') and b.cnvcProductCode=c.cnvcProductCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int AssignToValidEnterFinal(Hashtable htpara,DataTable dtIn)
		{
			con.Open();
			SqlDataReader dr=null;
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					string sql2="";
					string sql4="";
					string sql5="";
					for(int i=0;i<dtIn.Rows.Count;i++)
					{
						sql1="";
						sql2="";
						sql4="";
						sql5="";

						sql1="select count(*) from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcProductCode='"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"'";
						dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
						dr.Read();
						int ExistCount=int.Parse(dr[0].ToString());
						dr.Close();

						if(ExistCount==0)
						{
							sql2="insert into tbStorage values('"+htpara["strDeptID"].ToString()+"','"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"','"+dtIn.Rows[i]["cnvcProductName"].ToString()+"','"+dtIn.Rows[i]["cnvcUnit"].ToString()+"',"+dtIn.Rows[i]["cnnValidOkCount"].ToString()+",0,0)";
						}
						else
						{
							sql2="update tbStorage set cnnCount=cnnCount+"+dtIn.Rows[i]["cnnValidOkCount"].ToString()+" where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcProductCode='"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"'";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

						if(dtIn.Rows[i]["cnvcProductType"].ToString()=="FINALPRODUCT")
						{
							sql4="insert into tbStorageLog values('"+htpara["strDeptID"].ToString()+"','"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"','"+dtIn.Rows[i]["cnvcProductName"].ToString()+"','"+dtIn.Rows[i]["cnvcUnit"].ToString()+"',"+dtIn.Rows[i]["cnnValidOkCount"].ToString()+",0,0,'DC01','"+htpara["strReceiveOper"].ToString()+"','"+htpara["strValidDate"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

							sql5="insert into tbSellLoseLog values('"+htpara["strDeptID"].ToString()+"','"+htpara["strValidDate"].ToString()+"','"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"','"+dtIn.Rows[i]["cnvcProductName"].ToString()+"',"+dtIn.Rows[i]["cnnTravelCount"].ToString()+",'"+dtIn.Rows[i]["cnvcUnit"].ToString()+"','','','DC02','0','"+htpara["strOperID"].ToString()+"','"+htpara["strValidDate"].ToString()+"','',null)";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);
						}
						else if(dtIn.Rows[i]["cnvcProductType"].ToString()=="SEMIPRODUCT")
						{
							sql4="insert into tbStorageLog values('"+htpara["strDeptID"].ToString()+"','"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"','"+dtIn.Rows[i]["cnvcProductName"].ToString()+"','"+dtIn.Rows[i]["cnvcUnit"].ToString()+"',"+dtIn.Rows[i]["cnnValidOkCount"].ToString()+",0,0,'DB01','"+htpara["strReceiveOper"].ToString()+"','"+htpara["strValidDate"].ToString()+"')";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

							sql5="insert into tbSellLoseLog values('"+htpara["strDeptID"].ToString()+"','"+htpara["strValidDate"].ToString()+"','"+dtIn.Rows[i]["cnvcProductCode"].ToString()+"','"+dtIn.Rows[i]["cnvcProductName"].ToString()+"',"+dtIn.Rows[i]["cnnTravelCount"].ToString()+",'"+dtIn.Rows[i]["cnvcUnit"].ToString()+"','','','DB03','0','"+htpara["strOperID"].ToString()+"','"+htpara["strValidDate"].ToString()+"','',null)";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);
						}
					}

					string sql6="update tbOrderBook set cnvcOrderState='3' where cnnOrderSerialNo in(select distinct cnnOrderSerialNo from tbAssignLog where cnnAssignSerialNo="+htpara["strAssignID"].ToString()+")";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);

					string sql7="update tbAssignLog set cnvcReceiveOperID='"+htpara["strReceiveOper"].ToString()+"',cndReceiveDate='"+htpara["strValidDate"].ToString()+"' where cnnAssignSerialNo="+htpara["strAssignID"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql7);
					
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetLoseInfo(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strLoseDept"].ToString()!="全部")
				{
					strCondition=" and cnvcDeptID='"+htPara["strLoseDept"].ToString()+"' ";
				}
				if(htPara["strLoseType"].ToString()!="全部")
				{
					strCondition=" and cnvcLoseType='"+htPara["strLoseType"].ToString()+"' ";
				}
				string sql="select [cnnLoseSerialNo], [cnvcDeptID], [cndLoseDate], [cnvcProductCode], [cnvcProductName], [cnnLoseCount], [cnvcLoseComments], [cnvcWeather], [cnvcLoseType], (case [cnvcDestroyFlag] when '0' then '未确认' when '1' then '已确认' else [cnvcDestroyFlag] end) as [cnvcDestroyFlag], [cnvcOperID], [cndLoseOperDate], [cnvcDestroyOperID], [cndDestroyDate] ";
				sql+=" from tbSellLoseLog where cndLoseDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and cnvcDestroyFlag='"+htPara["strConfirmFlag"].ToString()+"'"+strCondition+" order by cnvcDeptID,cnvcLoseType,cnnLoseSerialNo";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int UpdateLoseConfirm(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbSellLoseLog set cnvcDestroyFlag='1',cnvcDestroyOperID='"+htpara["strConfirmOper"].ToString()+"',cndDestroyDate='"+htpara["strConfirmDate"].ToString()+"' where cnnLoseSerialNo in("+htpara["strLoseSerial"].ToString()+")";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public string GetGoodsUnit(string strProductCode)
		{
			string strUnit="";
			try
			{
				string sql="select cnvcUnit from tbFormula where cnvcProductCode='"+strProductCode+"'";
				SqlDataReader dr=SqlHelper.ExecuteReader(con,CommandType.Text,sql);
				dr.Read();
				strUnit=dr[0].ToString();
				dr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return strUnit;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strUnit;
		}

		public DataTable QueryBillReceiveReport(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				string sql="";
				switch(htPara["strQueryType"].ToString())
				{
					case "MoreMaterialReceive":
						if(htPara["strDeptID"].ToString()!="全部")
						{
							strCondition=" and a.cnvcReceiveDeptID='"+htPara["strDeptID"].ToString()+"' ";
						}
						if(htPara["strGroup"].ToString()!="全部")
						{
							strCondition+=" and a.cnvcGroup='"+htPara["strGroup"].ToString()+"' ";
						}
						if(htPara["strMaterialType"].ToString()!="全部")
						{
							strCondition+=" and c.cnvcProductType = '"+htPara["strMaterialType"].ToString()+"' ";
						}
						if(htPara["strMaterialID"].ToString()!="全部")
						{
							strCondition+=" and b.cnvcProductCode = '"+htPara["strMaterialID"].ToString()+"' ";
						}
							
						sql="select b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit,b.cnvcUnit,convert(char(8),cndReceiveDate,112) as ReceiveDay,sum(cnnCount) as ReceiveCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,tbMaterial c";
						sql+=" where a.cnvcBillState='2' and a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcMaterialCode and convert(char(6),cndReceiveDate,112)='"+htPara["strMonth"].ToString()+"' ";
						sql+=strCondition +" group by b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit,b.cnvcUnit,convert(char(8),cndReceiveDate,112) order by b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit,b.cnvcUnit,convert(char(8),cndReceiveDate,112)";
						break;
					case "OneMaterialReceive":
						if(htPara["strDeptID"].ToString()!="全部")
						{
							strCondition=" and a.cnvcReceiveDeptID='"+htPara["strDeptID"].ToString()+"' ";
						}
						if(htPara["strGroup"].ToString()!="全部")
						{
							strCondition+=" and a.cnvcGroup='"+htPara["strGroup"].ToString()+"' ";
						}
						if(htPara["strMaterialType"].ToString()!="全部")
						{
							strCondition+=" and c.cnvcProductType = '"+htPara["strMaterialType"].ToString()+"' ";
						}
						if(htPara["strMaterialID"].ToString()!="全部")
						{
							strCondition+=" and b.cnvcProductCode = '"+htPara["strMaterialID"].ToString()+"' ";
						}
							
						sql="select a.cnvcReceiveDeptID,a.cnvcGroup,convert(char(8),cndReceiveDate,112) as ReceiveDay,sum(cnnCount) as ReceiveCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b,tbMaterial c";
						sql+=" where a.cnvcBillState='2' and a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and b.cnvcProductCode=c.cnvcMaterialCode and convert(char(6),cndReceiveDate,112)='"+htPara["strMonth"].ToString()+"' ";
						sql+=strCondition +" group by a.cnvcReceiveDeptID,a.cnvcGroup,convert(char(8),cndReceiveDate,112) order by a.cnvcReceiveDeptID,a.cnvcGroup,convert(char(8),cndReceiveDate,112)";
						break;
				}

				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int UpdateSotckPlanBatch(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string strBatch=htpara["strBatch"].ToString();
					string sql="update tbStockPlan set cndStartDate"+strBatch+"='"+htpara["strStartDate"].ToString()+"',cnnCount"+strBatch+"="+htpara["strCount"].ToString()+",cnnSumFee"+strBatch+"="+htpara["strSumFee"].ToString()+" where cnvcProductCode='"+htpara["strProductCode"].ToString()+"' and cnvcProductName='"+htpara["strProductName"].ToString()+"' and cnvcUnit='"+htpara["strUnit"].ToString()+"' and cnvcMonth='"+htpara["strMonth"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetStorageCheckLog(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strDeptID"].ToString()!="全部")
				{
					strCondition=" and a.cnvcCheckDeptID='"+htPara["strDeptID"].ToString()+"'";
				}
				if(htPara["strProductType"].ToString()!="全部")
				{
					strCondition+=" and c.cnvcProductType='"+htPara["strProductType"].ToString()+"'";
				}
				if(htPara["strProductClass"].ToString()!="全部")
				{
					strCondition+=" and c.cnvcProductClass='"+htPara["strProductClass"].ToString()+"'";
				}
				string sql="";
				if(htPara["strProductType"].ToString()=="FINALPRODUCT"||htPara["strProductType"].ToString()=="SEMIPRODUCT")
				{
					sql="select a.cnnCheckSerialNo,cndOperDate,cnvcWeather,cnvcCheckOperID,cnvcManageOperID,b.[cnvcProductCode], b.[cnvcProductName], b.[cnnProductPrice], [cnnOriginalStorage], [cnnOrderCount], [cnnMoveOutCount], [cnnMoveInCount], [cnnLoseCount], [cnnFreeCount], [cnnUseCount], [cnnSellCount], [cnnSystemCount], [cnnRealCount], [cnnDifferentCount]";
					sql+=" from tbSellDayCheckLog a,tbSellDayCheckDetail b,tbFormula c where a.cndOperDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnnCheckSerialNo=b.cnnCheckSerialNo and b.cnvcProductCode=c.cnvcProductCode "+strCondition;
				}
				else if(htPara["strProductType"].ToString()=="Raw"||htPara["strProductType"].ToString()=="Pack")
				{
					sql="select a.cnnCheckSerialNo,cndOperDate,cnvcWeather,cnvcCheckOperID,cnvcManageOperID,b.[cnvcProductCode], b.[cnvcProductName], b.[cnnProductPrice], [cnnOriginalStorage], [cnnOrderCount], [cnnMoveOutCount], [cnnMoveInCount], [cnnLoseCount], [cnnFreeCount], [cnnUseCount], [cnnSellCount], [cnnSystemCount], [cnnRealCount], [cnnDifferentCount]";
					sql+=" from tbSellDayCheckLog a,tbSellDayCheckDetail b,tbMaterial c where a.cndOperDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnnCheckSerialNo=b.cnnCheckSerialNo and b.cnvcProductCode=c.cnvcMaterialCode "+strCondition;
				}
				else
				{
					sql="select a.cnnCheckSerialNo,cndOperDate,cnvcWeather,cnvcCheckOperID,cnvcManageOperID,b.[cnvcProductCode], b.[cnvcProductName], b.[cnnProductPrice], [cnnOriginalStorage], [cnnOrderCount], [cnnMoveOutCount], [cnnMoveInCount], [cnnLoseCount], [cnnFreeCount], [cnnUseCount], [cnnSellCount], [cnnSystemCount], [cnnRealCount], [cnnDifferentCount]";
					sql+=" from tbSellDayCheckLog a,tbSellDayCheckDetail b,tbFormula c where a.cndOperDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnnCheckSerialNo=b.cnnCheckSerialNo and b.cnvcProductCode=c.cnvcProductCode "+strCondition;
					sql+=" union ";
					sql+="select a.cnnCheckSerialNo,cndOperDate,cnvcWeather,cnvcCheckOperID,cnvcManageOperID,b.[cnvcProductCode], b.[cnvcProductName], b.[cnnProductPrice], [cnnOriginalStorage], [cnnOrderCount], [cnnMoveOutCount], [cnnMoveInCount], [cnnLoseCount], [cnnFreeCount], [cnnUseCount], [cnnSellCount], [cnnSystemCount], [cnnRealCount], [cnnDifferentCount]";
					sql+=" from tbSellDayCheckLog a,tbSellDayCheckDetail b,tbMaterial c where a.cndOperDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59' and a.cnnCheckSerialNo=b.cnnCheckSerialNo and b.cnvcProductCode=c.cnvcMaterialCode "+strCondition;
				}
				
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public double QueryCurrentProductStorage(string strStorageDept,string strProductCode)
		{
			double dpstorage=0;
			try
			{
				string sql="select cnnCount from tbStorage where cnvcStorageDeptID='"+strStorageDept+"' and cnvcProductCode='"+strProductCode+"'";
				SqlDataReader dr=SqlHelper.ExecuteReader(con,CommandType.Text,sql);
				dr.Read();
				dpstorage=Math.Round(double.Parse(dr[0].ToString()),2);
				dr.Close();

			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dpstorage;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dpstorage;
		}

		public DataTable GetCurSafeStorage(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="";
				string strClassCondition="";
				if(htPara["strProductClass"].ToString()!="全部")
				{
					strClassCondition=" and b.cnvcProductClass='"+htPara["strProductClass"].ToString()+"'";
				}
				switch(htPara["strQueryType"].ToString())
				{
					case "全部":
						if(htPara["strProductType"].ToString()=="FINALPRODUCT"||htPara["strProductType"].ToString()=="SEMIPRODUCT")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.* from tbStorage a,tbFormula b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' "+strClassCondition+" order by a.cnvcProductCode";
						}
						else if(htPara["strProductType"].ToString()=="Pack"||htPara["strProductType"].ToString()=="Raw")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.cnvcStorageDeptID,a.cnvcProductCode,a.cnvcProductName,b.cnvcStandardUnit as cnvcUnit,cast(a.cnnCount/b.cnnStatdardCount as numeric(10,2)) as cnnCount,cast(a.cnnSafeCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeCount,cast(a.cnnSafeUpCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeUpCount from tbStorage a,tbMaterial b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcMaterialCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' "+strClassCondition+" order by a.cnvcProductCode";
						}
						break;
					case "0":
						if(htPara["strProductType"].ToString()=="FINALPRODUCT"||htPara["strProductType"].ToString()=="SEMIPRODUCT")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.* from tbStorage a,tbFormula b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' and cnnCount>cnnSafeCount and cnnCount<cnnSafeUpCount "+strClassCondition+" order by a.cnvcProductCode";
						}
						else if(htPara["strProductType"].ToString()=="Pack"||htPara["strProductType"].ToString()=="Raw")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.cnvcStorageDeptID,a.cnvcProductCode,a.cnvcProductName,b.cnvcStandardUnit as cnvcUnit,cast(a.cnnCount/b.cnnStatdardCount as numeric(10,2)) as cnnCount,cast(a.cnnSafeCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeCount,cast(a.cnnSafeUpCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeUpCount from tbStorage a,tbMaterial b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcMaterialCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' and cnnCount>cnnSafeCount and cnnCount<cnnSafeUpCount "+strClassCondition+" order by a.cnvcProductCode";
						}
						break;
					case "1":
						if(htPara["strProductType"].ToString()=="FINALPRODUCT"||htPara["strProductType"].ToString()=="SEMIPRODUCT")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.* from tbStorage a,tbFormula b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcProductCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' and (cnnCount<=cnnSafeCount or cnnCount>=cnnSafeUpCount) "+strClassCondition+" order by a.cnvcProductCode";
						}
						else if(htPara["strProductType"].ToString()=="Pack"||htPara["strProductType"].ToString()=="Raw")
						{
							sql="select a.cnvcStorageDeptID as cnvcDeptID,a.cnvcStorageDeptID,a.cnvcProductCode,a.cnvcProductName,b.cnvcStandardUnit as cnvcUnit,cast(a.cnnCount/b.cnnStatdardCount as numeric(10,2)) as cnnCount,cast(a.cnnSafeCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeCount,cast(a.cnnSafeUpCount/b.cnnStatdardCount as numeric(10,2)) as cnnSafeUpCount from tbStorage a,tbMaterial b where a.cnvcStorageDeptID='"+htPara["strDeptID"].ToString()+"' and a.cnvcProductCode=b.cnvcMaterialCode and b.cnvcProductType='"+htPara["strProductType"].ToString()+"' and (cnnCount<=cnnSafeCount or cnnCount>=cnnSafeUpCount) "+strClassCondition+" order by a.cnvcProductCode";
						}
						break;
				}
				if(sql!="")
				{
					dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				}
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int UpdateProductSafeCount(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbStorage set cnnSafeCount="+htpara["SafeLowCount"].ToString()+",cnnSafeUpCount="+htpara["SafeUpCount"].ToString()+" where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcProductCode='"+htpara["strProductCode"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="insert into tbStorageLog select cnvcStorageDeptID,cnvcProductCode,cnvcProductName,cnvcUnit,cnnCount,cnnSafeCount,cnnSafeUpCount,'SC98','"+htpara["strOperName"].ToString()+"','"+htpara["strOperDate"].ToString()+"' from tbStorage where cnvcStorageDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcProductCode='"+htpara["strProductCode"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataSet GetMakeBillNoRelative()
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select a.cnnProduceSerialNo,a.cnvcProduceDeptID,b.cnnMakeSerialNo,b.cnvcGroup,b.cnvcMakeName,b.cnvcMakeType,c.cnvcCode,cnvcName,cnvcUnit,cnnCount from tbProduceLog a,tbMakeLog b,tbMakeDetail c ";
				sql+=" where a.cnvcProduceState='2' and a.cnnProduceSerialNo=b.cnnProduceSerialNo and b.cnnMakeSerialNo=c.cnnMakeSerialNo and b.cnvcGroup in('BZ','PL') and b.cnnProduceSerialNo not in(select distinct cnnProduceSerialNo from tbBillOfReceiveLog where cnnProduceSerialNo is not null) ";
				sql+=" order by a.cnnProduceSerialNo,b.cnvcMakeType";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="dtMakeDetail";
				dsout.Tables.Add(dtout);

				dtout=new DataTable();
				string sql1="select distinct a.cnnProduceSerialNo,a.cnvcProduceDeptID,a.cndProduceDate,b.cnvcGroup,c.cnvcCode,cnvcName,cnvcUnit,cast(0 as numeric(10,4)) as orgCount,cast(0 as numeric(10,4)) as addCount,cast(0 as numeric(10,4)) as reduceCount,cast(0 as numeric(10,4)) as realCount from tbProduceLog a,tbMakeLog b,tbMakeDetail c ";
				sql1+=" where a.cnvcProduceState='2' and a.cnnProduceSerialNo=b.cnnProduceSerialNo and b.cnnMakeSerialNo=c.cnnMakeSerialNo and b.cnvcGroup in('BZ','PL') and b.cnnProduceSerialNo not in(select distinct cnnProduceSerialNo from tbBillOfReceiveLog where cnnProduceSerialNo is not null) ";
				sql1+=" order by a.cnnProduceSerialNo,a.cnvcProduceDeptID,a.cndProduceDate,b.cnvcGroup,c.cnvcCode,cnvcName,cnvcUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dtout.TableName="dtResult";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int RelativeMakeToReceive(DataTable dtIn,DataTable dtName)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					string sql2="";
					string sql3="";
					string sql4="";
					string strcount="";
					string strSerial="";
					string strProduceSerialNo="";
					string strProduceDeptID="";
					string strGroup="";
					SqlDataReader dr=null;
					for(int i=0;i<dtName.Rows.Count;i++)
					{
						sql1="";
						sql2="";
						sql3="";
						sql4="";
						strProduceSerialNo=dtName.Rows[i]["cnnProduceSerialNo"].ToString();
						strProduceDeptID=dtName.Rows[i]["cnvcProduceDeptID"].ToString();
						strGroup=dtName.Rows[i]["cnvcGroup"].ToString();
						sql1="select count(*) from tbBillOfReceiveLog where cnnProduceSerialNo="+strProduceSerialNo+" and cnvcGroup='"+strGroup+"'";
						dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
						dr.Read();
						strcount=dr[0].ToString();
						dr.Close();

						if(strcount!="0")
						{
							throw new Exception("可能有其它操作员在并发地操作此关联功能！");
						}
						else
						{
							sql2="insert into tbBillOfReceiveLog values('"+strProduceDeptID+"','"+strGroup+"','"+dtName.Rows[i]["cndProduceDate"].ToString()+"','','','','','原材料制令领用单','0',null,"+strProduceSerialNo+");SELECT scope_identity()";
						}
						dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql2);
						dr.Read();
						strSerial=dr[0].ToString();
						dr.Close();

						if(strSerial=="")
						{
							throw new Exception("获取流水失败！");
						}
						else
						{
							for(int k=0;k<dtIn.Rows.Count;k++)
							{
								if(strProduceSerialNo==dtIn.Rows[k]["cnnProduceSerialNo"].ToString()&&strProduceDeptID==dtIn.Rows[k]["cnvcProduceDeptID"].ToString()&&strGroup==dtIn.Rows[k]["cnvcGroup"].ToString())
								{
									sql3+="insert into tbBillOfReceiveDetail values("+strSerial+",'"+dtIn.Rows[k]["cnvcCode"].ToString()+"','"+dtIn.Rows[k]["cnvcName"].ToString()+"','','"+dtIn.Rows[k]["cnvcUnit"].ToString()+"',0,"+dtIn.Rows[k]["realCount"].ToString()+",0,0,0,'');";
								}
							}
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

							sql4="update tbBillOfReceiveDetail set cnvcUnit=b.cnvcUnit,cnnStandardCount=b.cnnStatdardCount from tbBillOfReceiveDetail a,tbMaterial b where a.cnnReceiveSerialNo="+strSerial+" and a.cnvcProductCode=b.cnvcMaterialCode";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);
						}
					}
					
					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetBillOfReceiveNoSend()
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql="select * from tbBillOfReceiveLog where cnvcBillState='0' order by cnnReceiveSerialNo";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataSet GetBillOfReceiveTempDetail(string strSerialList)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit,sum(cnnReceiveCount) as cnnReceiveCount,cast(0 as numeric(10,4)) as cnvcQC002,cast(0 as numeric(10,4)) as cnvcCF001,cast(0 as numeric(10,4)) as cnvcCF006,cast(0 as numeric(10,4)) as cnvcBS004,";
				sql+="cast(0 as numeric(10,4)) as cnvcJG003,cast(0 as numeric(10,4)) as cnvcXS007,cast(0 as numeric(10,4)) as cnvcSH005,cast(0 as numeric(10,4)) as cnvcFYZX1,cast(0 as numeric(10,4)) as cnvcCY009,cast(0 as numeric(10,4)) as cnvcJM010,cast(0 as numeric(10,4)) as cnvcXM011,sum(cnnOutCount) as cnnOutCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and a.cnnReceiveSerialNo in("+strSerialList+") group by b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="dtResult";
				dsout.Tables.Add(dtout);

				string sql1="select b.cnvcProductCode,b.cnvcProductName,a.cnvcReceiveDeptID,b.cnvcStandardUnit,sum(b.cnnOutCount) as cnnOutCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and a.cnnReceiveSerialNo in("+strSerialList+") group by b.cnvcProductCode,b.cnvcProductName,a.cnvcReceiveDeptID,b.cnvcStandardUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dtout.TableName="dtDetail";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int UpdateBatchBillOfReceiveSend(string strSerialList,string strOperID,string strOperDate)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql="INSERT INTO tbSendOutSerialNo (vcFill) VALUES ('0');SELECT scope_identity()";
					SqlDataReader dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql);
					dr.Read();
					string strSendSerial=dr[0].ToString();
					dr.Close();

					string sql1="update tbBillOfReceiveLog set cnvcBillState='1',cnnSendSerialNo="+strSendSerial+" where cnnReceiveSerialNo in("+strSerialList+")";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbStorage set cnnCount=a.cnnCount-b.cnnOutCount from tbStorage a,(select cnvcProductCode,sum(cnnOutCount*cnnStandardCount) as cnnOutCount from tbBillOfReceiveDetail where cnnReceiveSerialNo in("+strSerialList+") group by cnvcProductCode) b where a.cnvcStorageDeptID='FYZX1' and a.cnvcProductCode=b.cnvcProductCode";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
				
					string sql3="insert into tbStorageLog select 'FYZX1',b.cnvcProductCode,b.cnvcProductName,b.cnvcUnit,-b.cnnOutCount,0,0,'CA02','"+strOperID+"','"+strOperDate+"' from (select cnvcProductCode,cnvcProductName,cnvcUnit,sum(cnnOutCount*cnnStandardCount) as cnnOutCount from tbBillOfReceiveDetail where cnnReceiveSerialNo in("+strSerialList+") group by cnvcProductCode,cnvcProductName,cnvcUnit) b";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);
						
					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataSet GetBillOfReceiveSendPrint(string strSendSerial)
		{
			DataSet dsout=new DataSet();
			DataTable dtout=new DataTable();
			try
			{
				string sql="select b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit,sum(cnnReceiveCount) as cnnReceiveCount,cast(0 as numeric(10,4)) as cnvcQC002,cast(0 as numeric(10,4)) as cnvcCF001,cast(0 as numeric(10,4)) as cnvcCF006,cast(0 as numeric(10,4)) as cnvcBS004,";
				sql+="cast(0 as numeric(10,4)) as cnvcJG003,cast(0 as numeric(10,4)) as cnvcXS007,cast(0 as numeric(10,4)) as cnvcSH005,cast(0 as numeric(10,4)) as cnvcFYZX1,cast(0 as numeric(10,4)) as cnvcCY009,cast(0 as numeric(10,4)) as cnvcJM010,cast(0 as numeric(10,4)) as cnvcXM011,sum(cnnOutCount) as cnnOutCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and a.cnnSendSerialNo="+strSendSerial+" group by b.cnvcProductCode,b.cnvcProductName,b.cnvcStandardUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="dtResult";
				dsout.Tables.Add(dtout);

				string sql1="select b.cnvcProductCode,b.cnvcProductName,a.cnvcReceiveDeptID,b.cnvcStandardUnit,sum(b.cnnOutCount) as cnnOutCount from tbBillOfReceiveLog a,tbBillOfReceiveDetail b where a.cnnReceiveSerialNo=b.cnnReceiveSerialNo and a.cnnSendSerialNo="+strSendSerial+" group by b.cnvcProductCode,b.cnvcProductName,a.cnvcReceiveDeptID,b.cnvcStandardUnit";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dtout.TableName="dtDetail";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public DataTable GetProviderStockFillTree(string strQueryType,Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strSql = null;
				string strCondition="";
				if(strQueryType == "prov")
				{
					if(htPara["strProvID"].ToString()!="")
					{
						strCondition=" cnvcPrvdCode like '%"+htPara["strProvID"].ToString()+"%' ";
					}
				
					if(htPara["strProvName"].ToString()!="")
					{
						if(strCondition=="")
						{
							strCondition=" cnvcPrvdName like '%"+htPara["strProvName"].ToString()+"%' ";
						}
						else
						{
							strCondition+=" and cnvcPrvdName like '%"+htPara["strProvName"].ToString()+"%' ";
						}
					}

					if(strCondition=="")
					{
						strSql = "select distinct cnvcPrvdName,cnvcPrvdCode from tbProviderNew order by cnvcPrvdName";
					}
					else
					{
						strSql = "select distinct cnvcPrvdName,cnvcPrvdCode from tbProviderNew where "+strCondition+" order by cnvcPrvdName";
					}
				}
				else
				{
					if(htPara["strGoodsID"].ToString()!="")
					{
						strCondition=" and cnvcInvCode like '%"+htPara["strGoodsID"].ToString()+"%' ";
					}
				
					if(htPara["strGoodsName"].ToString()!="")
					{
						strCondition+=" and cnvcInvName like '%"+htPara["strGoodsName"].ToString()+"%' ";
					}

					if(strCondition=="")
					{
						strSql = "select distinct cnvcInvName as  cnvcGoodsName,b.cnvcProductClassName from tbInventory a,tbProductClass b where a.cnvcInvCCode=b.cnvcProductClassCode order by cnvcGoodsName";
					}
					else
					{
						strSql = "select distinct cnvcInvName as cnvcGoodsName,b.cnvcProductClassName from tbInventory a,tbProductClass b where a.cnvcInvCCode=b.cnvcProductClassCode "+strCondition+" order by cnvcGoodsName";
					}
				}
				
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strSql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetProviderToGoods(string strProvID)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strsql = "select a.cnvcGoodsCode,b.cnvcInvName,a.cnnGoodsPrice,b.cnvcPUComUnitCode,a.cnvcProducer,(case a.cnvcInvalidFlag when '0' then '无效' else '有效' end) as cnvcInvalidFlag from tbProviderStock a,tbInventory b where a.cnvcPrvdCode='"+strProvID+"' and a.cnvcGoodsCode=b.cnvcInvCode order by cnvcGoodsName";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int NewProviderStockAdd(CMSMStruct.ProviderStockStruct pss)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbProviderStock values('"+pss.strPrvdCode+"','"+pss.strGoodsCode+"','"+pss.strGoodsName+"',"+pss.dGoodsPrice.ToString()+",'1',getdate(),'"+pss.strProducer+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetProvStockOne(string strProvID,string strGoodsID)
		{
			DataTable dtout=null;
			try
			{
				string sql1 = "select b.cnvcInvName,c.cnvcComUnitName,a.cnnGoodsPrice,a.cnvcInvalidFlag,a.cnvcProducer from tbProviderStock a,tbInventory b,tbComputationUnit c";
				sql1+=" where a.cnvcPrvdCode='"+strProvID+"' and a.cnvcGoodsCode='"+strGoodsID+"' and a.cnvcGoodsCode=b.cnvcInvCode and b.cnvcPUComUnitCode=c.cnvcComunitCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public int ModProvGoodsInfo(CMSMStruct.ProviderStockStruct pss)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbProviderStock set cnnGoodsPrice="+pss.dGoodsPrice.ToString()+",cnvcInvalidFlag='"+pss.strInvalidFlag+"',cnvcProducer='"+pss.strProducer+"' where cnvcPrvdCode='"+pss.strPrvdCode+"' and cnvcGoodsCode='"+pss.strGoodsCode+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int IsExistProvGoods(string strPrvdID,string strGoodsID)
		{
			int existcount=0;
			try
			{
				string sql1="select count(*) from tbProviderStock where cnvcPrvdCode='"+strPrvdID+"' and cnvcGoodsCode='" +strGoodsID+ "'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				existcount=int.Parse(drr[0].ToString());
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return existcount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return existcount;
		}

		public DataTable GetGoodsToProvider(string strGoodsName)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strsql = "select a.cnvcGoodsCode,a.cnvcGoodsName,b.cnvcPrvdCode,b.cnvcPrvdName,a.cnvcProducer from tbProviderStock a,tbProviderNew b where a.cnvcPrvdCode=b.cnvcPrvdCode and a.cnvcGoodsName='"+strGoodsName+"'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		#region 查询采购订单主表信息
		public DataTable GetPoStockMain(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				string sql="";
				if(htPara["strPoID"].ToString()!="")
				{
					strCondition=" cnvcPoID='"+htPara["strPoID"].ToString()+"'";
				}
				if(htPara["strProvider"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" cnvcPrvdCode= '"+htPara["strProvider"].ToString()+"' ";
					}
					else
					{
						strCondition+=" and cnvcPrvdCode = '"+htPara["strProvider"].ToString()+"' ";
					}
				}
				if(htPara["strCycle"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" cnvcPlanCycle like '%"+htPara["strCycle"].ToString()+"%' ";
					}
					else
					{
						strCondition+=" and cnvcPlanCycle like '%"+htPara["strCycle"].ToString()+"%' ";
					}
				}
				if(htPara["strPoState"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" cnvcPoState = '"+htPara["strPoState"].ToString()+"' ";
					}
					else
					{
						strCondition+=" and cnvcPoState = '"+htPara["strPoState"].ToString()+"' ";
					}
				}
				if(strCondition=="")
				{
					sql="select cnvcPoID,cnvcPrvdCode,cnvcPlanCycle,cnvcPoState,cnvcCreater,cndCreateDate,cnvcModer,cndModDate,cnvcCloser,cndCloseDate from tbPoStockMain order by cnvcPoID";
				}
				else
				{
					sql="select cnvcPoID,cnvcPrvdCode,cnvcPlanCycle,cnvcPoState,cnvcCreater,cndCreateDate,cnvcModer,cndModDate,cnvcCloser,cndCloseDate from tbPoStockMain where "+strCondition+" order by cnvcPoID";
				}

				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}
		#endregion

		#region 查询采购订单主表单条记录
		public DataTable GetPoStockMainOne(string strPoID)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql1="select * from tbPoStockMain where cnvcPoID='" + strPoID + "'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}
		#endregion

		#region 检查是否存在相同供应商和相同周期的订单
		public string IsExistPoProviderCycle(string strProvCode,string strCycle)
		{
			string strcount="";
			try
			{
				string sql1="select count(*) from tbPoStockMain where cnvcPrvdCode='" + strProvCode + "' and cnvcPlanCycle='"+strCycle+"' and cnvcPoState<>'2'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strcount=drr[0].ToString();
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return "1";
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strcount;
		}
		#endregion

		#region 新增采购订单主表记录
		public int NewPoSotckMainAdd(CMSMStruct.PoStockMainStruct psm1)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select isnull(max(cast(substring(cnvcPoID,10,4) as int)),0) from tbPoStockMain where cnvcPoID like '"+psm1.strPoID.Substring(0,9)+"%'";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					int maxid=int.Parse(drr[0].ToString());
					drr.Close();

					maxid++;
					if(maxid<10)
						psm1.strPoID=psm1.strPoID.Substring(0,9)+"000"+maxid.ToString();
					else if(maxid>=10&&maxid<100)
						psm1.strPoID=psm1.strPoID.Substring(0,9)+"00"+maxid.ToString();
					else if(maxid>=100&&maxid<1000)
						psm1.strPoID=psm1.strPoID.Substring(0,9)+"0"+maxid.ToString();
					else if(maxid>=1000&&maxid<9999)
						psm1.strPoID=psm1.strPoID.Substring(0,9)+maxid.ToString();
					else
						return 0;

					string sql2="insert into tbPoStockMain values('"+psm1.strPoID+"','"+psm1.strPrvdCode+"','"+psm1.strAddress+"','"+psm1.strComments+"','"+psm1.strPoState+"','"+psm1.strPlanCycle+"','"+psm1.strCreater+"','','','',getdate(),null,null,null)";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}
		#endregion

		#region 修改采购订单主表内容
		public int ModPoSotckMainInfo(CMSMStruct.PoStockMainStruct psmnew,CMSMStruct.PoStockMainStruct psmold)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string strCondition="";
					if(psmnew.strAddress!=psmold.strAddress)
					{
						strCondition="cnvcAddress='"+psmnew.strAddress+"'";
					}
					if(psmnew.strComments!=psmold.strComments)
					{
						if(strCondition=="")
						{
							strCondition="cnvcComments='"+psmnew.strComments+"'";
						}
						else
						{
							strCondition+=",cnvcComments='"+psmnew.strComments+"'";
						}
					}
					if(psmnew.strPlanCycle!=psmold.strPlanCycle)
					{
						if(strCondition=="")
						{
							strCondition="cnvcPlanCycle='"+psmnew.strPlanCycle+"'";
						}
						else
						{
							strCondition+=",cnvcPlanCycle='"+psmnew.strPlanCycle+"'";
						}
					}
					if(strCondition!="")
					{
						string sql1="update tbPoStockMain set "+strCondition+",cnvcModer='"+psmnew.strModer+"',cndModDate=getdate() where cnvcPoID='"+psmnew.strPoID+"'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}
		#endregion

		#region 快速检索供应商存货
		public DataTable GetGoodsBySelect(string strPrvdCode,string strGoodsCode,string strGoodsName)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(strGoodsCode!=""&&strGoodsCode!="*")
				{
					strCondition+=" and cnvcInvCode = '" + strGoodsCode + "'";
				}
				if(strGoodsName!=""&&strGoodsName!="*")
				{
					strCondition+= " and cnvcInvName like '%" + strGoodsName + "%'";
				}
				string sql1="select top 10 cnvcInvCode as cnvcGoodsCode,cnvcInvName as cnvcGoodsName,cnvcPUComUnitCode as cnvcStockUnit,0 as cnnStockPrice from tbInventory ";
				sql1+=" where cnvcInvCode not in(select cnvcGoodsCode from tbProviderStock where cnvcPrvdCode='"+strPrvdCode+"')";
				if(strCondition!="")
				{
					sql1+=strCondition + " order by cnvcInvCode";
				}
				else
				{
					sql1+=" order by cnvcGoodsCode";
				}
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetPoStockGoodsBySelect(string strPoID,string strPrvdID,string strGoodsCode,string strGoodsName)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(strGoodsCode!=""&&strGoodsCode!="*")
				{
					strCondition+=" and a.cnvcGoodsCode = '" + strGoodsCode + "'";
				}
				if(strGoodsName!=""&&strGoodsName!="*")
				{
					strCondition+= " and b.cnvcInvName like '%" + strGoodsName + "%'";
				}
				string sql1="select top 10 a.cnvcGoodsCode,b.cnvcInvName as cnvcGoodsName,b.cnvcPUComUnitCode as cnvcStockUnit,a.cnnGoodsPrice as cnnStockPrice,b.cnvcGroupCode";
				sql1+=" from tbProviderStock a,tbInventory b where a.cnvcInvalidFlag='1' and a.cnvcPrvdCode='"+strPrvdID+"' and a.cnvcGoodsCode=b.cnvcInvCode and a.cnvcGoodsCode not in";
				sql1+="(select cnvcGoodsCode from tbPoStockDetail where cnvcPoID='"+strPoID+"')";
				sql1+= strCondition + " order by cnvcGoodsCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}

		public DataTable GetStockGoodsOneInfo(string strPrvdCode,string strGoodsCode)
		{
			DataTable dtout=new DataTable();
			try
			{
				string sql1="select * from tbProviderStock where cnvcPrvdCode='"+strPrvdCode+"' and cnvcGoodsCode='" + strGoodsCode+"'";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}
		#endregion

		#region 查询采购订单明细和合计
		public DataSet GetPoStockDetailSum(string strPoID)
		{
			DataTable dtout=new DataTable();
			DataSet dsout=new DataSet();
			try
			{
				string sql="select a.[cnvcPoID], a.[cnvcDeptID],a.[cnvcDeptID] as cnvcDeptName, a.cnvcGoodsCode,b.cnvcInvName as cnvcGoodsName, a.[cnvcGroupCode],a.[cnvcGroupCode] as cnvcGroupName, a.[cnvcStockUnit],";
				sql+="a.[cnnStockPrice], a.[cnnStockCount], a.[cnnStockFee], convert(char(10),a.[cndArriveDate],120) as cndArriveDate, a.[cnvcRowState], a.[cnvcCreater], a.[cnvcModer], a.[cnvcChecker],";
				sql+="a.[cndCreateDate], a.[cndModDate], a.[cndCheckDate] from tbPoStockDetail a,tbInventory b where cnvcPoID='"+strPoID+"' and a.cnvcGoodsCode=b.cnvcInvCode order by a.cnvcGoodsCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="detail";
				dsout.Tables.Add(dtout);

				sql="select cnvcPoID,cnvcGoodsCode,cnvcGroupCode,cnvcStockUnit,cnnStockPrice,cnnStockCountSum,cnnStockFeeSum,cnnArriveCount,cnnArriveFee,cast(cnnFinallyRate*100 as varchar(20))+'%' as cnnFinallyRate from tbPoStockSum where cnvcPoID='"+strPoID+"' order by cnvcGoodsCode";
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
				dtout.TableName="sum";
				dsout.Tables.Add(dtout);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}
		#endregion

		#region 检查是否存在同一订单下相同部门和相同货品的子订单
		public string IsExistPoStockDetail(string strPoID,string strDeptID,string strGoodsCode)
		{
			string strcount="";
			try
			{
				string sql1="select count(*) from tbPoStockDetail where cnvcPoID='" + strPoID + "' and cnvcDeptID='"+strDeptID+"' and cnvcGoodsCode='"+strGoodsCode+"'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strcount=drr[0].ToString();
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return "1";
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strcount;
		}
		#endregion

		#region 采购子订单表操作记录
		public int NewPoSotckDetailAdd(CMSMStruct.PoStockDetailStruct psds1,string stroperid)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbPoStockSum where cnvcPoID='"+psds1.strPoID+"' and cnvcGoodsCode='"+psds1.strGoodsCode+"'";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					int iscount=int.Parse(drr[0].ToString());
					drr.Close();

					string sql5="select distinct cnnRdID from tbRdRecordDetail where cnvcPOID='"+psds1.strPoID+"' and cnnRdID in(select cnnRdID from tbRdRecord where cnvcRdCode='RD001' and cnvcRdFlag='0' and cnvcState='0')";
					DataTable dtrdid=SqlHelper.ExecuteDataTable(con,tran,CommandType.Text,sql5);

					string sql7="select distinct cnnRdID from tbRdRecordDetail where cnvcPOID='"+psds1.strPoID+"' and cnvcInvCode='"+psds1.strGoodsCode+"' and cnnRdID in(select cnnRdID from tbRdRecord where cnvcRdCode='RD001' and cnvcRdFlag='0' and cnvcState='0')";
					DataTable dtgoodsrdid=SqlHelper.ExecuteDataTable(con,tran,CommandType.Text,sql7);

					string sql2="insert into tbPoStockDetail values('"+psds1.strPoID+"','"+psds1.strDeptID+"','"+psds1.strGoodsCode+"','"+psds1.strGroupCode+"','"+psds1.strStockUnit+"',"+psds1.dStockPrice+","+psds1.dStockCount+","+psds1.dStockFee+",'"+psds1.dtArriveDate.ToShortDateString()+"','"+psds1.strRowState+"','"+psds1.strCreater+"','','',getdate(),null,null)";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="";
					if(iscount==0)
					{
						sql3="insert into tbPoStockSum values('"+psds1.strPoID+"','"+psds1.strGoodsCode+"','"+psds1.strGroupCode+"','"+psds1.strStockUnit+"',"+psds1.dStockPrice+","+psds1.dStockCount+","+psds1.dStockFee+",0,0,0)";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);
					}
					else
					{
						sql3="update tbPoStockSum set cnnStockCountSum=b.sumcount,cnnStockFeeSum=b.sumfee from tbPoStockSum a,(select cnvcPoID,cnvcGoodsCode,sum(cnnStockCount) as sumcount,sum(cnnStockFee) as sumfee";
						sql3+=" from tbPoStockDetail where cnvcPoID='"+psds1.strPoID+"' group by cnvcPoID,cnvcGoodsCode ) b where a.cnvcPoID='"+psds1.strPoID+"' and a.cnvcPoID=b.cnvcPoID and a.cnvcGoodsCode=b.cnvcGoodsCode";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);
					}

					if(dtrdid.Rows.Count>0)
					{
						if(dtgoodsrdid.Rows.Count==0)
						{
							string sql6="insert into tbRdRecordDetail select "+dtrdid.Rows[0]["cnnRdID"].ToString()+",'"+psds1.strPoID+"',0,b.cnvcPrvdCode,a.cnvcGoodsCode,a.cnnStockCountSum,a.cnnStockPrice,a.cnnStockFeeSum,0,a.cnvcGroupCode,a.cnvcStockUnit,'','0','',null,0,'0',null";
							sql6+=" from tbPoStockSum a,tbPoStockMain b where a.cnvcPoID=b.cnvcPoID and b.cnvcPoID='"+psds1.strPoID+"' and a.cnvcGoodsCode='"+psds1.strGoodsCode+"'";
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);
						}
						else
						{
							string sql6="update tbRdRecordDetail set cnnQuantity=a.cnnStockCountSum,cnnCost=a.cnnStockFeeSum from tbPoStockSum a,tbRdRecordDetail b";
							sql6+=" where a.cnvcPOID=b.cnvcPOID and a.cnvcPOID='"+psds1.strPoID+"' and b.cnvcInvCode='"+psds1.strGoodsCode+"' and a.cnvcGoodsCode=b.cnvcInvCode and b.cnnRdID="+dtrdid.Rows[0]["cnnRdID"].ToString();
							SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);
						}
					}

					string sql4="insert into tbOperLog values('采购订单明细添加','"+stroperid+"','"+psds1.strDeptID+"',getdate(),'采购订单明细添加"+psds1.strPoID+",产品"+psds1.strGoodsCode+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int PoSotckDetailMod(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbPoStockDetail set cnnStockCount="+htpara["strNewEnterCount"].ToString()+",cnnStockFee="+htpara["strNewfee"].ToString()+",cnvcModer='"+htpara["strOperName"].ToString()+"',cndModDate=getdate() where cnvcPoID='" + htpara["strPoId"].ToString()+"' and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsCode"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbPoStockSum set cnnStockCountSum=b.sumcount,cnnStockFeeSum=b.sumfee from tbPoStockSum a,(select cnvcPoID,cnvcGoodsCode,sum(cnnStockCount) as sumcount,sum(cnnStockFee) as sumfee";
					sql2+=" from tbPoStockDetail where cnvcPoID='"+htpara["strPoId"].ToString()+"' group by cnvcPoID,cnvcGoodsCode ) b where a.cnvcPoID='"+htpara["strPoId"].ToString()+"' and a.cnvcPoID=b.cnvcPoID and a.cnvcGoodsCode=b.cnvcGoodsCode and a.cnvcGoodsCode='"+htpara["strGoodsCode"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="update tbRdRecordDetail set cnnQuantity=a.cnnStockCountSum,cnnCost=a.cnnStockFeeSum from tbPoStockSum a,tbRdRecordDetail b";
					sql3+=" where a.cnvcPOID=b.cnvcPOID and a.cnvcPOID='"+htpara["strPoId"].ToString()+"' and b.cnvcInvCode='"+htpara["strGoodsCode"].ToString()+"' and a.cnvcGoodsCode=b.cnvcInvCode and b.cnnRdID in(select cnnRdID from tbRdRecord where cnvcRdCode='RD001' and cnvcRdFlag='0' and cnvcState='0')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					string sql4="insert into tbOperLog values('采购订单明细修改','"+htpara["strOperID"].ToString()+"','"+htpara["strDeptID"].ToString()+"',getdate(),'采购订单明细修改"+htpara["strPoId"].ToString()+",产品"+htpara["strGoodsCode"].ToString()+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int PoSotckDetailDelete(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select cnnStockCount from tbPoStockDetail where cnvcPoID='"+htpara["strPoID"].ToString()+"' and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsID"].ToString()+"'";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					decimal detailcount=Math.Round(decimal.Parse(drr[0].ToString()),2);
					drr.Close();

					string sql2="select cnnStockCountSum from tbPoStockSum where cnvcPoID='"+htpara["strPoID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsID"].ToString()+"'";
					drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql2);
					drr.Read();
					decimal sumcount=Math.Round(decimal.Parse(drr[0].ToString()),2);
					drr.Close();

					string sql3="delete from tbPoStockDetail where cnvcPoID='"+htpara["strPoID"].ToString()+"' and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsID"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					if(detailcount==sumcount)
					{
						string sql4="delete from tbPoStockSum where cnvcPoID='"+htpara["strPoID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsID"].ToString()+"'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						string sql5="delete from tbRdRecordDetail where cnvcPOID='"+htpara["strPoID"].ToString()+"' and cnvcInvCode='"+htpara["strGoodsID"].ToString()+"' and cnnRdID in(select cnnRdID from tbRdRecord where cnvcRdCode='RD001' and cnvcRdFlag='0' and cnvcState='0')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);
					}
					else
					{
						string sql4="update tbPoStockSum set cnnStockCountSum=b.sumcount,cnnStockFeeSum=b.sumfee from tbPoStockSum a,(select cnvcPoID,cnvcGoodsCode,sum(cnnStockCount) as sumcount,sum(cnnStockFee) as sumfee";
						sql4+=" from tbPoStockDetail where cnvcPoID='"+htpara["strPoID"].ToString()+"' group by cnvcPoID,cnvcGoodsCode ) b where a.cnvcPoID='"+htpara["strPoID"].ToString()+"' and a.cnvcPoID=b.cnvcPoID and a.cnvcGoodsCode=b.cnvcGoodsCode and a.cnvcGoodsCode='"+htpara["strGoodsID"].ToString()+"'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql4);

						string sql5="update tbRdRecordDetail set cnnQuantity=a.cnnStockCountSum,cnnCost=a.cnnStockFeeSum from tbPoStockSum a,tbRdRecordDetail b";
						sql5+=" where a.cnvcPOID=b.cnvcPOID and a.cnvcPOID='"+htpara["strPoID"].ToString()+"' and b.cnvcInvCode='"+htpara["strGoodsID"].ToString()+"' and a.cnvcGoodsCode=b.cnvcInvCode and b.cnnRdID in(select cnnRdID from tbRdRecord where cnvcRdCode='RD001' and cnvcRdFlag='0' and cnvcState='0')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql5);
					}

					string sql6="insert into tbOperLog values('采购订单明细删除','"+htpara["strOperID"].ToString()+"','"+htpara["strDeptID"].ToString()+"',getdate(),'采购订单明细删除"+htpara["strPoID"].ToString()+",产品"+htpara["strGoodsID"].ToString()+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql6);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int PoSotckDetailChecked(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbPoStockDetail set cnvcRowState='2',cnvcChecker='"+htpara["strOperName"].ToString()+"',cndCheckDate=getdate() where cnvcPoID='" + htpara["strPoId"].ToString()+"' and cnvcDeptID='"+htpara["strDeptID"].ToString()+"' and cnvcGoodsCode='"+htpara["strGoodsCode"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public string IsExistPoStockDetailStateUncheck(string strPoID)
		{
			string strcount="";
			try
			{
				string sql1="select count(*) from tbPoStockDetail where cnvcPoID='" + strPoID + "' and cnvcRowState<>'2'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strcount=drr[0].ToString();
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return "1";
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strcount;
		}

		public int PoSotckMainChecked(string strPoID,string strChecker)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbPoStockMain set cnvcPoState='2',cnvcChecker='"+strChecker+"',cndCheckDate=getdate() where cnvcPoID='" + strPoID+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public string IsExistPoStockDetailStock(string strPoID)
		{
			string strcount="";
			try
			{
				string sql1="select count(*) from tbPoStockDetail where cnvcPoID='" + strPoID + "'";
				SqlDataReader drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strcount=drr[0].ToString();
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return "1";
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strcount;
		}

		public int PoSotckMainExecing(string strPoID)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbPoStockMain set cnvcPoState='1' where cnvcPoID='" + strPoID+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbPoStockDetail set cnvcRowState='1' where cnvcPoID='" + strPoID+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public int PoSotckMainClose(string strPoID,string strCloser)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbPoStockMain set cnvcPoState='2',cnvcCloser='"+strCloser+"',cndCloseDate=getdate() where cnvcPoID='" + strPoID+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbPoStockDetail set cnvcRowState='2' where cnvcPoID='" + strPoID+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}
		#endregion

		#region 查询采购入库单主表信息
		public DataTable GetPoStockEnterMain(Hashtable htPara)
		{
			DataTable dtout=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strPoEnterID"].ToString()!="")
				{
					strCondition=" and cnvcCode='"+htPara["strPoEnterID"].ToString()+"' ";
				}
				if(htPara["strWHouse"].ToString()!="")
				{
					strCondition+=" and cnvcWhCode like '%"+htPara["strWHouse"].ToString()+"%' ";
				}
				if(htPara["strState"].ToString()!="")
				{
					strCondition+=" and cnvcState = '"+htPara["strState"].ToString()+"' ";
				}
				if(htPara["strPoID"].ToString()!="")
				{
					strCondition+=" and cnnRdID in(select distinct cnnRdID from tbRdRecordDetail where cnvcPoID='"+htPara["strPoID"].ToString()+"')";
				}

				string sql="select cnnRdID,cnvcCode,cnvcDepID,cnvcWhCode,cnvcMaker,cndARVDate,(case cnvcState when '0' then '未入库' else '已入库' end) as cnvcState from tbRdRecord where cnvcRdCode='RD001' and cndARVDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59'";
				if(strCondition=="")
				{
					sql+=" order by cnvcCode";
				}
				else
				{
					sql+=strCondition+" order by cnvcCode";
				}
				dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtout;
		}
		#endregion

        public DataTable GetGoodsChart(string strMonth, string strGoodsID)
        {
            DataTable dtout = new DataTable();
            try
            {
                string sql = "select vcGoodsID,convert(char(8),dtConsDate,112) as SaleDay,sum(iCount) as SaleCount from tbConsItemOther where convert(char(6),dtConsDate,112)='" + strMonth + "' and vcGoodsID='" + strGoodsID + "' group by vcGoodsID,convert(char(8),dtConsDate,112) order by vcGoodsID,convert(char(8),dtConsDate,112)";
                dtout = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtout;
        }

        public DataTable ChartSeachGoods(string strGoodType, string strGoodName)
        {
            DataTable dtout = new DataTable();
            try
            {
                string sql = "";
                if (strGoodType != "" && strGoodName != "")
                {
                    sql = "select vcGoodsID as cnvcCode,vcGoodsName as cnvcName from tbGoods where vcGoodsType='" + strGoodType + "' and vcGoodsName like '%" + strGoodName + "%' order by vcGoodsID";
                }
                else if (strGoodType != "" && strGoodName == "")
                {
                    sql = "select vcGoodsID as cnvcCode,vcGoodsName as cnvcName from tbGoods where vcGoodsType='" + strGoodType + "' order by vcGoodsID";
                }
                else if (strGoodType == "" && strGoodName != "")
                {
                    sql = "select vcGoodsID as cnvcCode,vcGoodsName as cnvcName from tbGoods where vcGoodsName like '%" + strGoodName + "%' order by vcGoodsID";
                }
                dtout = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtout;
        }

	}
}
