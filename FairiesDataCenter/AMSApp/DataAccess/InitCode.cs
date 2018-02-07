using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using CommCenter;

namespace DataAccess
{
	/// <summary>
	/// Summary description for InitCode.
	/// </summary>
	public class InitCode
	{
		public InitCode()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet LoadCodeTable(string strCon)
		{
			//初始化输出包
			DataSet		dsOut = new DataSet();

			//连接数据库
			using (SqlConnection conn=new SqlConnection(strCon))
			{
				//打开数据库连接
				conn.Open();
			
				try
				{
					string sql= "select * from tbCommCode where vcCommSign in('AS','AT','ES','GT','IGT','MD','OP','PT','DE','OF','EC','CLT','SFlag','PPS','LM') and vcCommCode<>'CEN00'";
					DataTable dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="tbCommCode";
					dsOut.Tables.Add(dt);

                    sql = "select * from tbCommCode where vcCommSign='AT' and vcCommCode not in ('AT999','ATMAS')";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "AssAT";
                    dsOut.Tables.Add(dt);

                    sql = "select * from tbCommCode where vcCommSign='AT' and substring(vcComments,1,1)='Y' and vcCommCode not in ('AT999','ATMAS') order by vcCommCode";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "AT1";
                    dsOut.Tables.Add(dt);

					sql= "select * from tbCommCode where vcCommSign='MD'";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="AllMD";
					dsOut.Tables.Add(dt);

					sql= "select vcMacAddress from tbMacAddress";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="MAC";
					dsOut.Tables.Add(dt);

					sql= "select * from tbOperFunc order by vcOperID,vcFuncName";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="OperFunc";
					dsOut.Tables.Add(dt);

					sql= "select a.vcCommName as iotName,substring(a.vcCommName,1,5) as Officer,b.vcCommName as vcClassName,b.vcCommCode as vcClassId,a.vcCommCode as InTime,a.vcCommSign as OutTime ";
					sql+=" from tbCommCode a,tbCommCode b where a.vcComments='IOTime' and b.vcCommSign='EC' and substring(a.vcCommName,6,1)=b.vcCommCode order by Officer,vcClassId";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="IOTime";
					dsOut.Tables.Add(dt);

					sql= "select vcGoodsID as vcCommCode,vcGoodsName as vcCommName from tbGoods";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="Goods";
					dsOut.Tables.Add(dt);

                    //sql= "select cnvcProductClassCode as vcCommCode,cnvcProductClassName as vcCommName,cnvcProductType as vcCommSign from tbProductClass order by cnvcProductType,cnvcProductClassCode";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="PClass";
                    //dsOut.Tables.Add(dt);

                    //sql= "select distinct cnvcMaterialCode as vcCommCode,cnvcMaterialName as vcCommName,cnvcProductType,cnvcUnit,cnvcProductClass,cnvcStandardUnit,cnnStatdardCount from tbMaterial order by cnvcMaterialName";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="AllMaterial";
                    //dsOut.Tables.Add(dt);

                    //sql= "select distinct cnvcPrvdCode as vcCommCode,cnvcPrvdName as vcCommName from tbProviderNew order by cnvcPrvdName";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="Provider";
                    //dsOut.Tables.Add(dt);

					sql= "select cnvcDeptName as vcCommName,cnvcDeptID as vcCommCode,cnvcDeptType as vcCommSign from tbDept where cnvcDeptID<>'CEN00' and cnvcDeptType in('SalesRoom','Factory')";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="NewDept";
					dsOut.Tables.Add(dt);

					sql= "select cnvcName as vcCommName,cnvcCode as vcCommCode,cnvcType as vcCommSign from tbNameCode";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="tbNameCodeToStorage";
					dsOut.Tables.Add(dt);

                    //sql= "select cnvcProductName as vcCommName,cnvcProductCode as vcCommCode,cnvcProductType,cnvcUnit,cnvcProductClass from tbFormula";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="tbFormula";
                    //dsOut.Tables.Add(dt);

					sql= "select distinct cnvcOldDeptID,cnvcNewDeptID from tbDeptMapInfo";
					dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
					dt.TableName="DeptMapInfo";
					dsOut.Tables.Add(dt);

                    sql = "select cnvcWhCode as vcCommCode,cnvcWhName as vcCommName,cnvcDepID as cnvcDepCode  from tbWarehouse";
                    dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
                    dt.TableName = "Warehouse";
                    dsOut.Tables.Add(dt);

                    //sql= "select cnvcGroupCode as vcCommCode,cnvcGroupName as vcCommName from tbComputationGroup";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="ComputationGroup";
                    //dsOut.Tables.Add(dt);

                    //sql= "select cnvcComunitCode as vcCommCode,cnvcComUnitName as vcCommName,cnvcGroupCode,cniChangRate from tbComputationUnit";
                    //dt = SqlHelper.ExecuteDataTable(conn,CommandType.Text,sql);
                    //dt.TableName="ComputationUnit";
                    //dsOut.Tables.Add(dt);
				}
				catch(Exception ex)
				{
					AMSLog clog=new AMSLog();
					clog.WriteLine(ex);
					dsOut = null;
				}
				finally			 
				{
					conn.Close();
				}
			}

			//返回数据
			return dsOut;
		}
	}
}
