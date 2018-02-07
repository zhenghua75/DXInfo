using System;
using System.Data;
using DataAccess;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// MaterialS ��ժҪ˵����
	/// </summary>
	public class MaterialSBusi
	{
		string strcon="";
		MaterialSAcc msa=null;

		public MaterialSBusi(string strcons)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			strcon=strcons;
			msa=new MaterialSAcc(strcons);
		}

		public CMSMStruct.MaterialSStruct GetMaterialInfo(string strBatch,string strMaterialID)
		{
			DataTable dtout=msa.GetMaterialInfo(strBatch,strMaterialID);
			CMSMStruct.MaterialSStruct ms1=new CommCenter.CMSMStruct.MaterialSStruct();
			if(dtout!=null)
			{
				ms1.strBatchNo=dtout.Rows[0]["cnvcBatchNo"].ToString();
				ms1.strMaterialCode=dtout.Rows[0]["cnnMaterialCode"].ToString();
				ms1.strMaterialName=dtout.Rows[0]["cnvcMaterialName"].ToString();
				ms1.strStandardUnit=dtout.Rows[0]["cnvcStandardUnit"].ToString();
				ms1.strUnit=dtout.Rows[0]["cnvcUnit"].ToString();
				ms1.dPrice=Math.Round(double.Parse(dtout.Rows[0]["cnnPrice"].ToString()),2);
				ms1.strProviderName=dtout.Rows[0]["cnvcProviderName"].ToString();
				ms1.strMaterialType=dtout.Rows[0]["cnvcMaterialType"].ToString();
				ms1.dAlarmCount=double.Parse(dtout.Rows[0]["cnnAlarmCount"].ToString());
				ms1.dCurCount=Math.Round(double.Parse(dtout.Rows[0]["cnnCurCount"].ToString()),2);
				ms1.strFlag=dtout.Rows[0]["cnvcFlag"].ToString();
			}
			else
			{
				ms1=null;
			}
			return ms1;
		}

		public bool ChkMaterialNameDup(string strBatch,string strMaterialName)
		{
			string strname=msa.getMaterialName(strBatch,strMaterialName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkNewMaterialNameDup(string strBatchNo,string strMaterialName,string strMaterialCode)
		{
			string strname=msa.getMaterialNamebyID(strBatchNo,strMaterialName,strMaterialCode);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertMaterial(CMSMStruct.MaterialSStruct mss)
		{
			int recount=msa.InsertMaterial(mss);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		
		public bool UpdateMaterial(CMSMStruct.MaterialSStruct mssnew,CMSMStruct.MaterialSStruct mssold)
		{
			string sqlset="";
			if(mssnew.strMaterialName!=mssold.strMaterialName)
			{
				sqlset+="cnvcMaterialName='" + mssnew.strMaterialName + "',";
			}
			if(mssnew.strStandardUnit!=mssold.strStandardUnit)
			{
				sqlset+="cnvcStandardUnit='" + mssnew.strStandardUnit + "',";
			}
			if(mssnew.strUnit!=mssold.strUnit)
			{
				sqlset+="cnvcUnit='" + mssnew.strUnit + "',";
			}
			if(mssnew.dPrice!=mssold.dPrice)
			{
				sqlset+="cnnPrice=" + mssnew.dPrice.ToString() + ",";
			}
			if(mssnew.strProviderName!=mssold.strProviderName)
			{
				sqlset+="cnvcProviderName='" + mssnew.strProviderName + "',";
			}
			if(mssnew.strMaterialType!=mssold.strMaterialType)
			{
				sqlset+="cnvcMaterialType='" + mssnew.strMaterialType + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=msa.UpdateMaterial(mssnew.strMaterialCode,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		public bool CancelMaterial(CMSMStruct.MaterialSStruct mssold)
		{
			int recount=msa.CancelMaterial(mssold);
			if(recount<=0)
			{
				return false;
			}
			
			return true;
		}

		public DataTable GetMaterials(string strMaterialCode,string strMaterialName,string strMaterialType,string strBatchNo)
		{
			DataTable dtout=msa.GetMaterials(strMaterialCode,strMaterialName,strMaterialType,strBatchNo);
			if(dtout!=null)
			{
				dtout.Columns["cnvcBatchNo"].ColumnName="ԭ��������";
				dtout.Columns["cnnMaterialCode"].ColumnName="ԭ���ϱ��";
				dtout.Columns["cnvcMaterialName"].ColumnName="ԭ��������";
				dtout.Columns["cnvcStandardUnit"].ColumnName="���";
				dtout.Columns["cnvcUnit"].ColumnName="��λ";
				dtout.Columns["cnnPrice"].ColumnName="����";
				dtout.Columns["cnvcProviderName"].ColumnName="��Ӧ��";
				dtout.Columns["cnvcName"].ColumnName="ԭ��������";
				dtout.Columns["cnvcFlag"].ColumnName="���ñ�־";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["���ñ�־"].ToString()=="����")
					{
						dtout.Rows[i]["����"]="<a href='wfmMaterialPara.aspx?batch="+dtout.Rows[i]["ԭ��������"].ToString() +"&&id=" + dtout.Rows[i]["ԭ���ϱ��"].ToString() + "'>�༭</a>";
					}
				}
			}
			return dtout;
		}

		public DataTable GetMaterialsBySelect(string strMaterialCode,string strMaterialName)
		{
			DataTable dtout=msa.GetMaterialsBySelect(strMaterialCode,strMaterialName);
			return dtout;
		}

		public bool InsertMaterialEnter(CMSMStruct.MaterialEnterStruct mes)
		{
			int recount=msa.InsertMaterialEnter(mes);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetMaterialEnterForMod(Hashtable htpara)
		{
			DataTable dtout=msa.GetMaterialEnterForMod(htpara);
			return dtout;
		}

		public bool MaterialEnterMod(Hashtable htpara)
		{
			int recount=msa.MaterialEnterMod(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool MaterialEnterModDetele(Hashtable htpara)
		{
			int recount=msa.MaterialEnterModDetele(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool InsertMaterialOut(CMSMStruct.MaterialOutStruct mes)
		{
			int recount=msa.InsertMaterialOut(mes);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetMaterialOutForMod(Hashtable htpara)
		{
			DataTable dtout=msa.GetMaterialOutForMod(htpara);
			return dtout;
		}

		public bool MaterialOutMod(Hashtable htpara)
		{
			int recount=msa.MaterialOutMod(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool MaterialOutModDetele(Hashtable htpara)
		{
			int recount=msa.MaterialOutModDetele(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetProviderList()
		{
			DataTable dtout=msa.GetProviderList();
			return dtout;
		}

		public DataSet GetMaterialsEnterReport(string strQueryType,Hashtable htpara)
		{
			DataTable dtout=msa.GetMaterialsEnterReport(strQueryType,htpara);
			DataTable dtReport=dtout.Copy();
			dtReport.TableName="QueryResult";
			DataTable dtSum=new DataTable("SumState");
			dtSum.Columns.Add("RecordCount");
			dtSum.Columns.Add("TotalCount");
			dtSum.Columns.Add("TotalFee");
			DataSet dsReport=new DataSet();
			double totalcount=0;
			double totalFee=0;
			switch(strQueryType)
			{
				case "0":
					dtReport.Columns["cnnSerialNo"].ColumnName="�����ˮ";
					dtReport.Columns["cnvcBatchNo"].ColumnName="ԭ��������";
					dtReport.Columns["cnnMaterialCode"].ColumnName="ԭ���ϱ��";
					dtReport.Columns["cnvcMaterialName"].ColumnName="ԭ��������";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					dtReport.Columns["cnvcProviderName"].ColumnName="��Ӧ��";
					dtReport.Columns["cnvcMaterialType"].ColumnName="ԭ��������";
					dtReport.Columns["cnnEnterCount"].ColumnName="�������";
					dtReport.Columns["cnvcOperType"].ColumnName="��������";
					dtReport.Columns["cndEnterDate"].ColumnName="�������";
					dtReport.Columns["cnvcDeptID"].ColumnName="����";
					dtReport.Columns["cndOperDate"].ColumnName="��������";
					dtReport.Columns["cnvcOperName"].ColumnName="����Ա";
					DataRow drnew0=dtSum.NewRow();
					drnew0["RecordCount"]=dtReport.Rows.Count;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["�������"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["����"].ToString()),2)*Math.Round(double.Parse(dr["�������"].ToString()),2);
					}
					drnew0["TotalCount"]=totalcount.ToString();
					drnew0["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew0);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
				case "1":
					dtReport.Columns.Remove("enterdate");
					dtReport.Columns.Remove("sumcount");
					for(int i=1;i<=31;i++)
					{
						dtReport.Columns.Add(i.ToString());
					}
					dtReport.Columns.Add("�����ϼ�");
					dtReport.Columns.Add("���ϼ�");
					for(int j=0;j<dtout.Rows.Count;j++)
					{
						dtReport.Rows[j]["�����ϼ�"]="0.00";
						dtReport.Rows[j]["���ϼ�"]="0.00";
						for(int k=1;k<=31;k++)
						{
							dtReport.Rows[j][k.ToString()]="0.00";
						}
					}
					foreach(DataRow dr in dtout.Rows)
					{
						foreach(DataRow drrep in dtReport.Rows)
						{
							if(drrep["cnvcBatchNo"].ToString()==dr["cnvcBatchNo"].ToString()&&drrep["cnnMaterialCode"].ToString()==dr["cnnMaterialCode"].ToString()&&drrep["cnvcMaterialName"].ToString()==dr["cnvcMaterialName"].ToString()&&drrep["cnvcStandardUnit"].ToString()==dr["cnvcStandardUnit"].ToString()&&drrep["cnvcUnit"].ToString()==dr["cnvcUnit"].ToString()&&drrep["cnnPrice"].ToString()==dr["cnnPrice"].ToString())
							{
								int enterday=int.Parse(dr["enterdate"].ToString().Substring(6,2));
								drrep[enterday.ToString()]=dr["sumcount"].ToString();
							}
						}
					}
					foreach(DataRow drrep in dtReport.Rows)
					{
						totalcount=0;
						totalFee=0;
						for(int m=1;m<=31;m++)
						{
							totalcount+=Math.Round(double.Parse(drrep[m.ToString()].ToString()),2);
						}
						totalFee=Math.Round(double.Parse(drrep["cnnPrice"].ToString()),2)*totalcount;
						drrep["�����ϼ�"]=totalcount.ToString();
						drrep["���ϼ�"]=totalFee.ToString();
					}
					dtReport.Columns["cnvcBatchNo"].ColumnName="����";
					dtReport.Columns["cnnMaterialCode"].ColumnName="���";
					dtReport.Columns["cnvcMaterialName"].ColumnName="����";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					totalcount=0;
					totalFee=0;
					DataRow drnew1=dtSum.NewRow();
					drnew1["RecordCount"]=dtReport.Rows.Count;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["�����ϼ�"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["���ϼ�"].ToString()),2);
					}
					drnew1["TotalCount"]=totalcount.ToString();
					drnew1["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew1);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
				case "2":
					dtReport.Columns.Remove("cnvcProviderName");
					dtReport.Columns.Remove("sumcount");
					Hashtable htProvider=new Hashtable();
					foreach(DataRow dr in dtout.Rows)
					{
						if(!htProvider.ContainsKey(dr["cnvcProviderName"].ToString()))
						{
							htProvider.Add(dr["cnvcProviderName"].ToString(),dr["cnvcProviderName"].ToString());
						}
					}
					foreach(System.Collections.DictionaryEntry de in htProvider)
					{
						dtReport.Columns.Add(de.Key.ToString());
						dtReport.Columns.Add(de.Key.ToString()+"$");
					}
					dtReport.Columns.Add("�����ϼ�");
					dtReport.Columns.Add("���ϼ�");
					foreach(DataRow drrep in dtReport.Rows)
					{
						drrep["�����ϼ�"]="0.00";
						drrep["���ϼ�"]="0.00";
						foreach(System.Collections.DictionaryEntry de in htProvider)
						{
							drrep[de.Key.ToString()]="0.00";
							drrep[de.Key.ToString()+"$"]="0.00";
						}
					}
					foreach(DataRow dr in dtout.Rows)
					{
						foreach(DataRow drrep in dtReport.Rows)
						{
							if(drrep["cnvcBatchNo"].ToString()==dr["cnvcBatchNo"].ToString()&&drrep["cnnMaterialCode"].ToString()==dr["cnnMaterialCode"].ToString()&&drrep["cnvcMaterialName"].ToString()==dr["cnvcMaterialName"].ToString()&&drrep["cnvcStandardUnit"].ToString()==dr["cnvcStandardUnit"].ToString()&&drrep["cnvcUnit"].ToString()==dr["cnvcUnit"].ToString()&&drrep["cnnPrice"].ToString()==dr["cnnPrice"].ToString())
							{
								drrep[dr["cnvcProviderName"].ToString()]=dr["sumcount"].ToString();
								drrep[dr["cnvcProviderName"].ToString()+"$"]=(Math.Round(double.Parse(dr["sumcount"].ToString()),2)*Math.Round(double.Parse(dr["cnnPrice"].ToString()),2)).ToString();
							}
						}
					}

					foreach(DataRow drrep in dtReport.Rows)
					{
						totalcount=0;
						totalFee=0;
						foreach(System.Collections.DictionaryEntry de in htProvider)
						{
							totalcount+=Math.Round(double.Parse(drrep[de.Key.ToString()].ToString()),2);
							totalFee+=Math.Round(double.Parse(drrep[de.Key.ToString()+"$"].ToString()),2);
						}

						drrep["�����ϼ�"]=totalcount.ToString();
						drrep["���ϼ�"]=totalFee.ToString();
					}
					dtReport.Columns["cnvcBatchNo"].ColumnName="����";
					dtReport.Columns["cnnMaterialCode"].ColumnName="���";
					dtReport.Columns["cnvcMaterialName"].ColumnName="����";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					DataRow drnew2=dtSum.NewRow();
					drnew2["RecordCount"]=dtReport.Rows.Count;
					totalcount=0;
					totalFee=0;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["�����ϼ�"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["���ϼ�"].ToString()),2);
					}
					drnew2["TotalCount"]=totalcount.ToString();
					drnew2["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew2);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
			}
			return dsReport;
		}

		public DataSet GetMaterialsOutReport(string strQueryType,Hashtable htpara)
		{
			DataTable dtout=msa.GetMaterialsOutReport(strQueryType,htpara);
			DataTable dtReport=dtout.Copy();
			dtReport.TableName="QueryResult";
			DataTable dtSum=new DataTable("SumState");
			dtSum.Columns.Add("RecordCount");
			dtSum.Columns.Add("TotalCount");
			dtSum.Columns.Add("TotalFee");
			DataSet dsReport=new DataSet();
			double totalcount=0;
			double totalFee=0;
			switch(strQueryType)
			{
				case "0":
					dtReport.Columns["cnnSerialNo"].ColumnName="������ˮ";
					dtReport.Columns["cnvcBatchNo"].ColumnName="ԭ��������";
					dtReport.Columns["cnnMaterialCode"].ColumnName="ԭ���ϱ��";
					dtReport.Columns["cnvcMaterialName"].ColumnName="ԭ��������";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					dtReport.Columns["cnvcProviderName"].ColumnName="��Ӧ��";
					dtReport.Columns["cnvcMaterialType"].ColumnName="ԭ��������";
					dtReport.Columns["cnnOutCount"].ColumnName="��������";
					dtReport.Columns["cnvcOperType"].ColumnName="��������";
					dtReport.Columns["cndOutDate"].ColumnName="��������";
					dtReport.Columns["cnvcDeptID"].ColumnName="����";
					dtReport.Columns["cndOperDate"].ColumnName="��������";
					dtReport.Columns["cnvcOperName"].ColumnName="����Ա";
					DataRow drnew0=dtSum.NewRow();
					drnew0["RecordCount"]=dtReport.Rows.Count;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["��������"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["����"].ToString()),2)*Math.Round(double.Parse(dr["��������"].ToString()),2);
					}
					drnew0["TotalCount"]=totalcount.ToString();
					drnew0["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew0);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
				case "1":
					dtReport.Columns.Remove("enterdate");
					dtReport.Columns.Remove("sumcount");
					for(int i=1;i<=31;i++)
					{
						dtReport.Columns.Add(i.ToString());
					}
					dtReport.Columns.Add("�����ϼ�");
					dtReport.Columns.Add("���ϼ�");
					for(int j=0;j<dtout.Rows.Count;j++)
					{
						dtReport.Rows[j]["�����ϼ�"]="0.00";
						dtReport.Rows[j]["���ϼ�"]="0.00";
						for(int k=1;k<=31;k++)
						{
							dtReport.Rows[j][k.ToString()]="0.00";
						}
					}
					foreach(DataRow dr in dtout.Rows)
					{
						foreach(DataRow drrep in dtReport.Rows)
						{
							if(drrep["cnvcBatchNo"].ToString()==dr["cnvcBatchNo"].ToString()&&drrep["cnnMaterialCode"].ToString()==dr["cnnMaterialCode"].ToString()&&drrep["cnvcMaterialName"].ToString()==dr["cnvcMaterialName"].ToString()&&drrep["cnvcStandardUnit"].ToString()==dr["cnvcStandardUnit"].ToString()&&drrep["cnvcUnit"].ToString()==dr["cnvcUnit"].ToString()&&drrep["cnnPrice"].ToString()==dr["cnnPrice"].ToString())
							{
								int enterday=int.Parse(dr["enterdate"].ToString().Substring(6,2));
								drrep[enterday.ToString()]=dr["sumcount"].ToString();
							}
						}
					}
					foreach(DataRow drrep in dtReport.Rows)
					{
						totalcount=0;
						totalFee=0;
						for(int m=1;m<=31;m++)
						{
							totalcount+=Math.Round(double.Parse(drrep[m.ToString()].ToString()),2);
						}
						totalFee=Math.Round(double.Parse(drrep["cnnPrice"].ToString()),2)*totalcount;
						drrep["�����ϼ�"]=totalcount.ToString();
						drrep["���ϼ�"]=totalFee.ToString();
					}
					dtReport.Columns["cnvcBatchNo"].ColumnName="����";
					dtReport.Columns["cnnMaterialCode"].ColumnName="���";
					dtReport.Columns["cnvcMaterialName"].ColumnName="����";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					totalcount=0;
					totalFee=0;
					DataRow drnew1=dtSum.NewRow();
					drnew1["RecordCount"]=dtReport.Rows.Count;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["�����ϼ�"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["���ϼ�"].ToString()),2);
					}
					drnew1["TotalCount"]=totalcount.ToString();
					drnew1["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew1);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
				case "2":
					dtReport.Columns["cnvcBatchNo"].ColumnName="����";
					dtReport.Columns["cnnMaterialCode"].ColumnName="���";
					dtReport.Columns["cnvcMaterialName"].ColumnName="����";
					dtReport.Columns["cnvcStandardUnit"].ColumnName="���";
					dtReport.Columns["cnvcUnit"].ColumnName="��λ";
					dtReport.Columns["cnnPrice"].ColumnName="����";
					dtReport.Columns["sumcount"].ColumnName="�����ϼ�";
					dtReport.Columns["sumfee"].ColumnName="���ϼ�";
					DataRow drnew2=dtSum.NewRow();
					drnew2["RecordCount"]=dtReport.Rows.Count;
					totalcount=0;
					totalFee=0;
					foreach(DataRow dr in dtReport.Rows)
					{
						totalcount+=Math.Round(double.Parse(dr["�����ϼ�"].ToString()),2);
						totalFee+=Math.Round(double.Parse(dr["���ϼ�"].ToString()),2);
					}
					drnew2["TotalCount"]=totalcount.ToString();
					drnew2["TotalFee"]=totalFee.ToString();
					dtSum.Rows.Add(drnew2);
					dsReport.Tables.Add(dtReport);
					dsReport.Tables.Add(dtSum);
					break;
			}
			return dsReport;
		}

		public DataTable GetMaterialsEnterOutModList(string strOperType,string strBegin,string strEnd)
		{
			DataTable dtout=msa.GetMaterialsEnterOutModList(strOperType,strBegin,strEnd);
			if(dtout!=null)
			{
				dtout.Columns["cnvcBatchNo"].ColumnName="ԭ��������";
				dtout.Columns["cnnMaterialCode"].ColumnName="ԭ���ϱ��";
				dtout.Columns["cnvcMaterialName"].ColumnName="ԭ��������";
				dtout.Columns["cnvcStandardUnit"].ColumnName="���";
				dtout.Columns["cnvcUnit"].ColumnName="��λ";
				dtout.Columns["cnnPrice"].ColumnName="����";
				dtout.Columns["cnvcProviderName"].ColumnName="��Ӧ��";
				dtout.Columns["cnvcMaterialType"].ColumnName="ԭ��������";
				dtout.Columns["cnvcOperType"].ColumnName="��������";
				dtout.Columns["cnnLinkSerialNo"].ColumnName="����������ˮ";
				dtout.Columns["cnvcDeptID"].ColumnName="����";
				dtout.Columns["cndOperDate"].ColumnName="����ʱ��";
				dtout.Columns["cnvcOperName"].ColumnName="����Ա";
				if(strOperType=="0")
				{
					dtout.Columns["cnnSerialNo"].ColumnName="�����ˮ";
					dtout.Columns["cnnEnterCount"].ColumnName="�������";
					dtout.Columns["cndEnterDate"].ColumnName="���ʱ��";
				}
				else
				{
					dtout.Columns["cnnSerialNo"].ColumnName="������ˮ";
					dtout.Columns["cnnOutCount"].ColumnName="��������";
					dtout.Columns["cndOutDate"].ColumnName="����ʱ��";
				}
			}
			return dtout;
		}

		public DataTable GetMaterialsStorageCurrent(string strMaterialType)
		{
			DataTable dtout=msa.GetMaterialsStorageCurrent(strMaterialType);
			if(dtout!=null)
			{
				dtout.Columns["cnvcBatchNo"].ColumnName="ԭ��������";
				dtout.Columns["cnnMaterialCode"].ColumnName="ԭ���ϱ��";
				dtout.Columns["cnvcMaterialName"].ColumnName="ԭ��������";
				dtout.Columns["cnvcStandardUnit"].ColumnName="���";
				dtout.Columns["cnvcUnit"].ColumnName="��λ";
				dtout.Columns["cnnPrice"].ColumnName="����";
				dtout.Columns["cnvcProviderName"].ColumnName="��Ӧ��";
				dtout.Columns["cnvcMaterialType"].ColumnName="ԭ��������";
				dtout.Columns["cnnCurCount"].ColumnName="��ǰ�������";
				dtout.Columns["cnnFee"].ColumnName="���";
			}
			return dtout;
		}

		public DataTable GetMaterialsSimpleAnalyse(string strMaterialType,string strMonth)
		{
			DateTime dtlast=new DateTime(int.Parse(strMonth.Substring(0,4)),int.Parse(strMonth.Substring(4,2)),1);
			dtlast=dtlast.AddMonths(-1);
			string strmon="";
			if(dtlast.Month<10)
			{
				strmon="0"+dtlast.Month.ToString();
			}
			else
			{
				strmon=dtlast.Month.ToString();
			}
			string strLastMonth=dtlast.Year.ToString()+strmon;
			DataSet dsout=msa.GetMaterialsSimpleAnalyse(strMaterialType,strMonth,strLastMonth);
			DataTable dtResult=dsout.Tables["AnalyseResult"];
			foreach(DataRow dr in dtResult.Rows)
			{
				foreach(DataRow drLast in dsout.Tables["LastState"].Rows)
				{
					if(dr["cnvcBatchNo"].ToString()==drLast["cnvcBatchNo"].ToString()&&dr["cnnMaterialCode"].ToString()==drLast["cnnMaterialCode"].ToString())
					{
						dr["LastCount"]=drLast["LastCount"].ToString();
						dr["LastFee"]=drLast["LastFee"].ToString();
					}
				}

				foreach(DataRow drEnter in dsout.Tables["EnterState"].Rows)
				{
					if(dr["cnvcBatchNo"].ToString()==drEnter["cnvcBatchNo"].ToString()&&dr["cnnMaterialCode"].ToString()==drEnter["cnnMaterialCode"].ToString())
					{
						dr["EnterCount"]=drEnter["EnterCount"].ToString();
						dr["EnterFee"]=drEnter["EnterFee"].ToString();
					}
				}

				foreach(DataRow drOut in dsout.Tables["OutState"].Rows)
				{
					if(dr["cnvcBatchNo"].ToString()==drOut["cnvcBatchNo"].ToString()&&dr["cnnMaterialCode"].ToString()==drOut["cnnMaterialCode"].ToString())
					{
						dr["OutCount"]=drOut["OutCount"].ToString();
						dr["OutFee"]=drOut["OutFee"].ToString();
					}
				}

				foreach(DataRow drCur in dsout.Tables["CurState"].Rows)
				{
					if(dr["cnvcBatchNo"].ToString()==drCur["cnvcBatchNo"].ToString()&&dr["cnnMaterialCode"].ToString()==drCur["cnnMaterialCode"].ToString())
					{
						dr["CurCount"]=drCur["CurCount"].ToString();
						dr["CurFee"]=drCur["CurFee"].ToString();
					}
				}
			}

			if(dtResult.Rows.Count==0)
			{
				DataRow drnew=dtResult.NewRow();
				for(int i=0;i<13;i++)
				{
					drnew[i]="0";
				}
				dtResult.Rows.Add(drnew);
			}

			return dtResult;
		}
	}
}
