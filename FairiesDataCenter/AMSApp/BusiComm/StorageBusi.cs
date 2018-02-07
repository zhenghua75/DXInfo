using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// Summary description for StorageBusi.
	/// </summary>
	public class StorageBusi
	{
		string strcon="";
		StorageAcc sac=null;
		public StorageBusi(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			sac=new StorageAcc(strcon);
		}

		public DataTable GetCheckData(Hashtable htpara)
		{
			DataTable dtout=sac.GetCheckData(htpara);
			return dtout;
		}

		public bool DayCheckFinal(Hashtable htpara,DataTable dtIn)
		{
			int recount=sac.DayCheckFinal(htpara,dtIn);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetLoseDaySale(Hashtable htpara)
		{
			DataTable dtout=sac.GetLoseDaySale(htpara);
			return dtout;
		}

		public DataTable GetProductInfoByPClass(string strPClass)
		{
			DataTable dtout=sac.GetProductInfoByPClass(strPClass);
			return dtout;
		}

		public DataTable GetMaterialInfoByProvider(string strProvider,string strFilter)
		{
			DataTable dtout=sac.GetMaterialInfoByProvider(strProvider,strFilter);
			return dtout;
		}

		public DataSet GetMaterialProviderByFilter(string strFilter)
		{
			DataSet dsout=sac.GetMaterialProviderByFilter(strFilter);
			return dsout;
		}

		public bool NewSaleLoseAdd(DataTable dtLoseList)
		{
			int recount=sac.NewSaleLoseAdd(dtLoseList);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool NewBillOfEnterStorageAdd(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.NewBillOfEnterStorageAdd(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool NewProductMoveAdd(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.NewProductMoveAdd(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateProductMoveValidEnter(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.UpdateProductMoveValidEnter(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool SaleLoseDelete(string strSerial)
		{
			int recount=sac.SaleLoseDelete(strSerial);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetProvider(Hashtable htpara)
		{
			DataTable dtout=sac.GetProvider(htpara);
			if(dtout!=null)
			{
				dtout.Columns["cnvcPrvdCode"].ColumnName="供应商编码";
				dtout.Columns["cnvcPrvdName"].ColumnName="供应商名称";
				dtout.Columns["cnvcPrvdAbbName"].ColumnName="供应商简称";
				dtout.Columns["cnvcPrvdClass"].ColumnName="供应商分类";
				dtout.Columns["cnvcAddress"].ColumnName="地址";
				dtout.Columns["cnvcPostCode"].ColumnName="邮政编码";
				dtout.Columns["cnvcPrvdPhone"].ColumnName="电话";
				dtout.Columns["cnvcPrvdFax"].ColumnName="传真";
				dtout.Columns["cnvcPrvdEmail"].ColumnName="Email地址";
				dtout.Columns["cnvcPrvdLinkName"].ColumnName="联系人";
				dtout.Columns["cndLastDate"].ColumnName="最后交易日期";
				dtout.Columns["cnnLastMoney"].ColumnName="最后交易金额";
				dtout.Columns["cnvcPrvdCredit"].ColumnName="信用等级";
				dtout.Columns["cnvcPrvdQualification"].ColumnName="资质证明";
				dtout.Columns["cnvcPrvdCreater"].ColumnName="建档人";
				dtout.Columns["cndPrvdCreateDate"].ColumnName="建档时间";
				dtout.Columns["cnvcActiveFlag"].ColumnName="有效标志";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmProviderDetail.aspx?PVID=" + dtout.Rows[i]["供应商编码"].ToString() +"'>编辑</a>";
				}
			}
			return dtout;
		}

		public CMSMStruct.ProviderStruct GetProviderDetailOne(string strProviderCode)
		{
			DataTable dtout=sac.GetProviderDetailOne(strProviderCode);
			CMSMStruct.ProviderStruct ps1=new CommCenter.CMSMStruct.ProviderStruct();
			if(dtout!=null)
			{
				ps1.strPrvdCode=dtout.Rows[0]["cnvcPrvdCode"].ToString();
				ps1.strPrvdName=dtout.Rows[0]["cnvcPrvdName"].ToString();
				ps1.strPrvdAbbName=dtout.Rows[0]["cnvcPrvdAbbName"].ToString();
				ps1.strPrvdClass=dtout.Rows[0]["cnvcPrvdClass"].ToString();
				ps1.strAddress=dtout.Rows[0]["cnvcAddress"].ToString();
				ps1.strPostCode=dtout.Rows[0]["cnvcPostCode"].ToString();
				ps1.strPrvdPhone=dtout.Rows[0]["cnvcPrvdPhone"].ToString();
				ps1.strPrvdFax=dtout.Rows[0]["cnvcPrvdFax"].ToString();
				ps1.strPrvdEmail=dtout.Rows[0]["cnvcPrvdEmail"].ToString();
				ps1.strPrvdLinkName=dtout.Rows[0]["cnvcPrvdLinkName"].ToString();
				ps1.strPrvdCredit=dtout.Rows[0]["cnvcPrvdCredit"].ToString();
				ps1.strPrvdQualification=dtout.Rows[0]["cnvcPrvdQualification"].ToString();
				ps1.strActiveFlag=dtout.Rows[0]["cnvcActiveFlag"].ToString();
			}
			return ps1;
		}

		public bool NewProviderAdd(CMSMStruct.ProviderStruct ps1)
		{
			int recount=sac.NewProviderAdd(ps1);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool IsExistProviderProduct(string strProviderCode,string strProductCode)
		{
			int recount=sac.IsExistProviderProduct(strProviderCode,strProductCode);
			if(recount>0)
			{
				return true;
			}

			return false;
		}

		public string IsExistProvider(string strProviderName)
		{
			return sac.IsExistProvider(strProviderName);
		}

		public bool ModProviderInfo(CMSMStruct.ProviderStruct psnew,CMSMStruct.ProviderStruct psold)
		{
			int recount=sac.ModProviderInfo(psnew,psold);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetBillOfEnterStorage(Hashtable htpara)
		{
			DataTable dtout=sac.GetBillOfEnterStorage(htpara);
			if(dtout!=null)
			{
				dtout.Columns["cnnEnterSerialNo"].ColumnName="进仓流水";
				dtout.Columns["cnvcProviderCode"].ColumnName="供应商编码";
				dtout.Columns["cnvcProviderName"].ColumnName="供应商名称";
				dtout.Columns["cndEnterDate"].ColumnName="进仓时间";
				dtout.Columns["cnvcDeliverMan"].ColumnName="送货员";
				dtout.Columns["cnvcValidateOperID"].ColumnName="验收";
				dtout.Columns["cnvcSafeOperID"].ColumnName="保安";
				dtout.Columns["cnvcStorageOperID"].ColumnName="仓管";
				dtout.Columns["cnvcBillOperID"].ColumnName="打单";
				dtout.Columns["cnvcEnterType"].ColumnName="进仓类型";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmBillOfEnterStorageDetail.aspx?ID=" + dtout.Rows[i]["进仓流水"].ToString()+"'>查看细节</a>";
				}
			}
			return dtout;
		}

		public DataSet GetBillOfEnterStorageOneLog(string strSerial)
		{
			DataSet dsout=sac.GetBillOfEnterStorageOneLog(strSerial);
			return dsout;
		}

		public DataTable GetProductMoveLog(Hashtable htpara)
		{
			DataTable dtout=sac.GetProductMoveLog(htpara);
//			if(dtout!=null)
//			{
//				dtout.Columns["cnnMoveSerialNo"].ColumnName="调拨流水";
//				dtout.Columns["cnvcOutDeptID"].ColumnName="调出店";
//				dtout.Columns["cnvcOutOperID"].ColumnName="调出人";
//				dtout.Columns["cnvcInDeptID"].ColumnName="调入店";
//				dtout.Columns["cnvcInOperID"].ColumnName="调入人";
//				dtout.Columns["cndMoveDate"].ColumnName="调拨日期";
//				dtout.Columns["cnvcOperID"].ColumnName="操作员";
//				dtout.Columns["cndOperDate"].ColumnName="操作时间";
//				dtout.Columns.Add("操作");
//				for(int i=0;i<dtout.Rows.Count;i++)
//				{
//					dtout.Rows[i]["操作"]="<a href='wfmProductMoveDetail.aspx?ID=" + dtout.Rows[i]["调拨流水"].ToString()+"'>查看细节</a>";
//				}
//			}
			return dtout;
		}

		public DataSet GetMoveOneLog(string strSerial)
		{
			DataSet dsout=sac.GetMoveOneLog(strSerial);
			return dsout;
		}

		public DataTable GetStockPlanQuery(string strMonth)
		{
			DataSet dsout=sac.GetStockPlanQuery(strMonth);
			DataTable dtplan=new DataTable();
			if(dsout!=null)
			{
				dtplan=dsout.Tables["PlanSum"];
				DataTable dtstorage=dsout.Tables["CurStorageSum"];
				foreach(DataRow drtmp1 in dtplan.Rows)
				{
					foreach(DataRow drtmp2 in dtstorage.Rows)
					{
						if(drtmp1["cnvcProductCode"].ToString()==drtmp2["cnvcProductCode"].ToString()&&drtmp1["cnvcProductName"].ToString()==drtmp2["cnvcProductName"].ToString()&&drtmp1["cnvcUnit"].ToString()==drtmp2["cnvcUnit"].ToString())
						{
							drtmp1["RealCount"]=drtmp2["cnnCount"].ToString();
							drtmp1["SafeCount"]=drtmp2["cnnSafeCount"].ToString();
							continue;
						}
					}
				}
			}
			dtplan.Columns["cnvcProductCode"].ColumnName="产品编码";
			dtplan.Columns["cnvcProductName"].ColumnName="产品名称";
			dtplan.Columns["cnvcUnit"].ColumnName="单位";
			dtplan.Columns["cnnPlanCount"].ColumnName="预估总用量";
			dtplan.Columns["RealCount"].ColumnName="实际库存量";
			dtplan.Columns["SafeCount"].ColumnName="安全库存量";
			dtplan.Columns["cndStartDate1"].ColumnName="第一批启动日期";
			dtplan.Columns["cnnCount1"].ColumnName="第一批数量";
			dtplan.Columns["cnnSumFee1"].ColumnName="第一批费用";
			dtplan.Columns["cndStartDate2"].ColumnName="第二批启动日期";
			dtplan.Columns["cnnCount2"].ColumnName="第二批数量";
			dtplan.Columns["cnnSumFee2"].ColumnName="第二批费用";
			dtplan.Columns["cndStartDate3"].ColumnName="第三批启动日期";
			dtplan.Columns["cnnCount3"].ColumnName="第三批数量";
			dtplan.Columns["cnnSumFee3"].ColumnName="第三批费用";
			dtplan.Columns["cndStartDate4"].ColumnName="第四批启动日期";
			dtplan.Columns["cnnCount4"].ColumnName="第四批数量";
			dtplan.Columns["cnnSumFee4"].ColumnName="第四批费用";
			dtplan.Columns["cnnSumFee"].ColumnName="总费用";
			dtplan.Columns.Add("操作");
			for(int i=0;i<dtplan.Rows.Count;i++)
			{
				dtplan.Rows[i]["操作"]="<a href='wfmPlanBatchDetail.aspx?PID=" + dtplan.Rows[i]["产品编码"].ToString() + "&PName="+dtplan.Rows[i]["产品名称"].ToString()+"&PUnit="+dtplan.Rows[i]["单位"].ToString()+"&month="+strMonth+"'>编辑</a>";
			}
			return dtplan;
		}

		public DataTable GetPlanDeptDetail(Hashtable htpara)
		{
			DataTable dtout=sac.GetPlanDeptDetail(htpara);
			return dtout;
		}

		public bool NewStockPlanAdd(DataTable dtDetail)
		{
			int recount=sac.NewStockPlanAdd(dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetBillOfReceive(Hashtable htpara)
		{
			DataTable dtout=sac.GetBillOfReceive(htpara);
//			if(dtout!=null)
//			{
//				dtout.Columns["cnnReceiveSerialNo"].ColumnName="领料流水";
//				dtout.Columns["cnnMakeSerialNo"].ColumnName="制令流水";
//				dtout.Columns["cnvcReceiveDeptID"].ColumnName="领料单位";
//				dtout.Columns["cnvcGroup"].ColumnName="生产组";
//				dtout.Columns["cndReceiveDate"].ColumnName="领料日期";
//				dtout.Columns["cnvcClass"].ColumnName="班次";
//				dtout.Columns["cnvcMaterialInchargeOperID"].ColumnName="物料主管";
//				dtout.Columns["cnvcStorageInchargeOperID"].ColumnName="仓库主管";
//				dtout.Columns["cnvcSendOperID"].ColumnName="发料人";
//				dtout.Columns["cnvcReceiveType"].ColumnName="领料单类型";
//				dtout.Columns["cnvcBillState"].ColumnName="工单状态";
//			}
			return dtout;
		}

		public DataSet GetBillOfReceiveOneLog(string strSerial)
		{
			DataSet dsout=sac.GetBillOfReceiveOneLog(strSerial);
			return dsout;
		}

		public bool NewBillOfReceiveAdd(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.NewBillOfReceiveAdd(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateBillOfReceiveOut(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.UpdateBillOfReceiveOut(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateBillOfReceiveValidEnter(Hashtable htpara,DataTable dtDetail)
		{
			int recount=sac.UpdateBillOfReceiveValidEnter(htpara,dtDetail);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetDailySaleChart(string strMonth,string strDept="全部")
		{
            DataTable dtout = sac.GetDailySaleChart(strMonth, strDept);
			return dtout;
		}

		public DataTable QueryBillEnterReport(Hashtable htpara)
		{
			DataTable dtout=sac.QueryBillEnterReport(htpara);
			string strYear=htpara["strMonth"].ToString().Substring(0,4);
			string strMonth=htpara["strMonth"].ToString().Substring(4,2);
			int monthday=DateTime.DaysInMonth(int.Parse(strYear),int.Parse(strMonth));
			DataTable dtReport=new DataTable();
			if(dtout!=null)
			{
				switch(htpara["strQueryType"].ToString())
				{
					case "MoreProviderEnter":
						for(int i=0;i<monthday+2;i++)
						{
							dtReport.Columns.Add("C"+i.ToString());
						}
						if(dtout.Rows.Count>0)
						{
							bool existreport=false;
							int existrow=0;
							int dataofday=0;
							double sumCount=0;
							double thisCount=0;
							for(int j=0;j<dtout.Rows.Count;j++)
							{
								existreport=false;
								existrow=0;
								dataofday=0;
								sumCount=0;
								thisCount=0;
								for(int w=0;w<dtReport.Rows.Count;w++)
								{
									if(dtReport.Rows[w]["C0"].ToString()==dtout.Rows[j]["cnvcProviderName"].ToString())
									{
										existreport=true;
										existrow=w;
										break;
									}
								}
								if(existreport)
								{
									dataofday=int.Parse(dtout.Rows[j]["EnterDay"].ToString().Substring(6,2));
									sumCount=double.Parse(dtReport.Rows[existrow]["C"+(monthday+1).ToString()].ToString());
									thisCount=double.Parse(dtout.Rows[j]["EnterCount"].ToString());
									dtReport.Rows[existrow]["C"+(dataofday).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									dtReport.Rows[existrow]["C"+(monthday+1).ToString()]=(Math.Round(sumCount+thisCount,2)).ToString();
								}
								else
								{
									DataRow drtmp=dtReport.NewRow();
									drtmp["C0"]=dtout.Rows[j]["cnvcProviderName"].ToString();
									for(int k=1;k<=monthday+1;k++)
									{
										drtmp["C"+k.ToString()]="0";
									}
									dataofday=int.Parse(dtout.Rows[j]["EnterDay"].ToString().Substring(6,2));
									drtmp["C"+(dataofday).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									drtmp["C"+(monthday+1).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									dtReport.Rows.Add(drtmp);
								}
							}
						}
						dtReport.Columns["C0"].ColumnName="供应商";
						for(int k=1;k<monthday+1;k++)
						{
							dtReport.Columns["C"+k.ToString()].ColumnName=k.ToString()+"日";
						}
						dtReport.Columns["C"+(monthday+1).ToString()].ColumnName="合计";
						break;
					case "OneProviderEnter":
						for(int i=0;i<monthday+5;i++)
						{
							dtReport.Columns.Add("C"+i.ToString());
						}
						if(dtout.Rows.Count>0)
						{
							bool existreport=false;
							int existrow=0;
							int dataofday=0;
							double sumCount=0;
							double thisCount=0;
							for(int j=0;j<dtout.Rows.Count;j++)
							{
								existreport=false;
								existrow=0;
								dataofday=0;
								sumCount=0;
								thisCount=0;
								for(int w=0;w<dtReport.Rows.Count;w++)
								{
									if(dtReport.Rows[w]["C0"].ToString()==dtout.Rows[j]["cnvcProductCode"].ToString()&&dtReport.Rows[w]["C1"].ToString()==dtout.Rows[j]["cnvcProductName"].ToString()&&dtReport.Rows[w]["C2"].ToString()==dtout.Rows[j]["cnvcStandardUnit"].ToString()&&dtReport.Rows[w]["C3"].ToString()==dtout.Rows[j]["cnvcUnit"].ToString())
									{
										existreport=true;
										existrow=w;
										break;
									}
								}
								if(existreport)
								{
									dataofday=int.Parse(dtout.Rows[j]["EnterDay"].ToString().Substring(6,2));
									sumCount=double.Parse(dtReport.Rows[existrow]["C"+(monthday+4).ToString()].ToString());
									thisCount=double.Parse(dtout.Rows[j]["EnterCount"].ToString());
									dtReport.Rows[existrow]["C"+(dataofday+3).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									dtReport.Rows[existrow]["C"+(monthday+4).ToString()]=(Math.Round(sumCount+thisCount,2)).ToString();
								}
								else
								{
									DataRow drtmp=dtReport.NewRow();
									drtmp["C0"]=dtout.Rows[j]["cnvcProductCode"].ToString();
									drtmp["C1"]=dtout.Rows[j]["cnvcProductName"].ToString();
									drtmp["C2"]=dtout.Rows[j]["cnvcStandardUnit"].ToString();
									drtmp["C3"]=dtout.Rows[j]["cnvcUnit"].ToString();
									for(int k=4;k<=monthday+4;k++)
									{
										drtmp["C"+k.ToString()]="0";
									}
									dataofday=int.Parse(dtout.Rows[j]["EnterDay"].ToString().Substring(6,2));
									drtmp["C"+(dataofday+3).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									drtmp["C"+(monthday+4).ToString()]=dtout.Rows[j]["EnterCount"].ToString();
									dtReport.Rows.Add(drtmp);
								}
							}
						}
						dtReport.Columns["C0"].ColumnName="原材料编码";
						dtReport.Columns["C1"].ColumnName="原材料名称";
						dtReport.Columns["C2"].ColumnName="规格";
						dtReport.Columns["C3"].ColumnName="单位";
						for(int k=4;k<monthday+4;k++)
						{
							dtReport.Columns["C"+k.ToString()].ColumnName=(k-3).ToString()+"日";
						}
						dtReport.Columns["C"+(monthday+4).ToString()].ColumnName="合计";
						break;
				}			
			}
			return dtReport;
		}

		public DataTable GetAssignToValidEnter(Hashtable htpara)
		{
			DataTable dtout=sac.GetAssignToValidEnter(htpara);
			return dtout;
		}

		public bool AssignToValidEnterFinal(Hashtable htpara,DataTable dtIn)
		{
			int recount=sac.AssignToValidEnterFinal(htpara,dtIn);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetLoseInfo(Hashtable htpara)
		{
			DataTable dtout=sac.GetLoseInfo(htpara);
			return dtout;
		}

		public bool UpdateLoseConfirm(Hashtable htpara)
		{
			int recount=sac.UpdateLoseConfirm(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public string GetGoodsUnit(string strProductCode)
		{
			string strUnit="";
			strUnit=sac.GetGoodsUnit(strProductCode);
			return strUnit;
		}

		public DataTable QueryBillReceiveReport(Hashtable htpara)
		{
			DataTable dtout=sac.QueryBillReceiveReport(htpara);
			string strYear=htpara["strMonth"].ToString().Substring(0,4);
			string strMonth=htpara["strMonth"].ToString().Substring(4,2);
			int monthday=DateTime.DaysInMonth(int.Parse(strYear),int.Parse(strMonth));
			DataTable dtReport=new DataTable();
			if(dtout!=null)
			{
				switch(htpara["strQueryType"].ToString())
				{
					case "MoreMaterialReceive":
						for(int i=0;i<monthday+5;i++)
						{
							dtReport.Columns.Add("C"+i.ToString());
						}
						if(dtout.Rows.Count>0)
						{
							bool existreport=false;
							int existrow=0;
							int dataofday=0;
							double sumCount=0;
							double thisCount=0;
							for(int j=0;j<dtout.Rows.Count;j++)
							{
								existreport=false;
								existrow=0;
								dataofday=0;
								sumCount=0;
								thisCount=0;
								for(int w=0;w<dtReport.Rows.Count;w++)
								{
									if(dtReport.Rows[w]["C0"].ToString()==dtout.Rows[j]["cnvcProductCode"].ToString()&&dtReport.Rows[w]["C1"].ToString()==dtout.Rows[j]["cnvcProductName"].ToString()&&dtReport.Rows[w]["C2"].ToString()==dtout.Rows[j]["cnvcStandardUnit"].ToString()&&dtReport.Rows[w]["C3"].ToString()==dtout.Rows[j]["cnvcUnit"].ToString())
									{
										existreport=true;
										existrow=w;
										break;
									}
								}
								if(existreport)
								{
									dataofday=int.Parse(dtout.Rows[j]["ReceiveDay"].ToString().Substring(6,2));
									sumCount=double.Parse(dtReport.Rows[existrow]["C"+(monthday+4).ToString()].ToString());
									thisCount=double.Parse(dtout.Rows[j]["ReceiveCount"].ToString());
									dtReport.Rows[existrow]["C"+(dataofday+3).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									dtReport.Rows[existrow]["C"+(monthday+4).ToString()]=(Math.Round(sumCount+thisCount,2)).ToString();
								}
								else
								{
									DataRow drtmp=dtReport.NewRow();
									drtmp["C0"]=dtout.Rows[j]["cnvcProductCode"].ToString();
									drtmp["C1"]=dtout.Rows[j]["cnvcProductName"].ToString();
									drtmp["C2"]=dtout.Rows[j]["cnvcStandardUnit"].ToString();
									drtmp["C3"]=dtout.Rows[j]["cnvcUnit"].ToString();
									for(int k=4;k<=monthday+4;k++)
									{
										drtmp["C"+k.ToString()]="0";
									}
									dataofday=int.Parse(dtout.Rows[j]["ReceiveDay"].ToString().Substring(6,2));
									drtmp["C"+(dataofday+3).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									drtmp["C"+(monthday+4).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									dtReport.Rows.Add(drtmp);
								}
							}
						}
						dtReport.Columns["C0"].ColumnName="原材料编码";
						dtReport.Columns["C1"].ColumnName="原材料名称";
						dtReport.Columns["C2"].ColumnName="规格";
						dtReport.Columns["C3"].ColumnName="单位";
						for(int k=4;k<monthday+4;k++)
						{
							dtReport.Columns["C"+k.ToString()].ColumnName=(k-3).ToString()+"日";
						}
						dtReport.Columns["C"+(monthday+4).ToString()].ColumnName="合计";
						break;
					case "OneMaterialReceive":
						for(int i=0;i<monthday+3;i++)
						{
							dtReport.Columns.Add("C"+i.ToString());
						}
						if(dtout.Rows.Count>0)
						{
							bool existreport=false;
							int existrow=0;
							int dataofday=0;
							double sumCount=0;
							double thisCount=0;
							for(int j=0;j<dtout.Rows.Count;j++)
							{
								existreport=false;
								existrow=0;
								dataofday=0;
								sumCount=0;
								thisCount=0;
								for(int w=0;w<dtReport.Rows.Count;w++)
								{
									if(dtReport.Rows[w]["C0"].ToString()==dtout.Rows[j]["cnvcReceiveDeptID"].ToString()&&dtReport.Rows[w]["C1"].ToString()==dtout.Rows[j]["cnvcGroup"].ToString())
									{
										existreport=true;
										existrow=w;
										break;
									}
								}
								if(existreport)
								{
									dataofday=int.Parse(dtout.Rows[j]["ReceiveDay"].ToString().Substring(6,2));
									sumCount=double.Parse(dtReport.Rows[existrow]["C"+(monthday+2).ToString()].ToString());
									thisCount=double.Parse(dtout.Rows[j]["ReceiveCount"].ToString());
									dtReport.Rows[existrow]["C"+(dataofday+1).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									dtReport.Rows[existrow]["C"+(monthday+2).ToString()]=(Math.Round(sumCount+thisCount,2)).ToString();
								}
								else
								{
									DataRow drtmp=dtReport.NewRow();
									drtmp["C0"]=dtout.Rows[j]["cnvcReceiveDeptID"].ToString();
									drtmp["C1"]=dtout.Rows[j]["cnvcGroup"].ToString();
									for(int k=2;k<=monthday+2;k++)
									{
										drtmp["C"+k.ToString()]="0";
									}
									dataofday=int.Parse(dtout.Rows[j]["ReceiveDay"].ToString().Substring(6,2));
									drtmp["C"+(dataofday+1).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									drtmp["C"+(monthday+2).ToString()]=dtout.Rows[j]["ReceiveCount"].ToString();
									dtReport.Rows.Add(drtmp);
								}
							}
						}
						dtReport.Columns["C0"].ColumnName="领料单位";
						dtReport.Columns["C1"].ColumnName="生产组";
						for(int k=2;k<monthday+2;k++)
						{
							dtReport.Columns["C"+k.ToString()].ColumnName=(k-1).ToString()+"日";
						}
						dtReport.Columns["C"+(monthday+2).ToString()].ColumnName="合计";
						break;
				}			
			}
			return dtReport;
		}

		public bool UpdateSotckPlanBatch(Hashtable htpara)
		{
			int recount=sac.UpdateSotckPlanBatch(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetStorageCheckLog(Hashtable htpara)
		{
			DataTable dtout=sac.GetStorageCheckLog(htpara);
			if(dtout!=null)
			{
				dtout.Columns["cnnCheckSerialNo"].ColumnName="盘点流水";
				dtout.Columns["cndOperDate"].ColumnName="操作时间";
				dtout.Columns["cnvcWeather"].ColumnName="天气";
				dtout.Columns["cnvcCheckOperID"].ColumnName="盘点人";
				dtout.Columns["cnvcManageOperID"].ColumnName="管理组";
				dtout.Columns["cnvcProductCode"].ColumnName="产品编码";
				dtout.Columns["cnvcProductName"].ColumnName="产品名称";
				dtout.Columns["cnnProductPrice"].ColumnName="单价";
				dtout.Columns["cnnOriginalStorage"].ColumnName="期初库存";
				dtout.Columns["cnnOrderCount"].ColumnName="进仓量";
				dtout.Columns["cnnMoveOutCount"].ColumnName="调拨出量";
				dtout.Columns["cnnMoveInCount"].ColumnName="调拨入量";
				dtout.Columns["cnnLoseCount"].ColumnName="损耗量";
				dtout.Columns["cnnFreeCount"].ColumnName="剩余量";
				dtout.Columns["cnnUseCount"].ColumnName="使用量";
				dtout.Columns["cnnSellCount"].ColumnName="售卖量";
				dtout.Columns["cnnSystemCount"].ColumnName="系统库存";
				dtout.Columns["cnnRealCount"].ColumnName="实际库存";
				dtout.Columns["cnnDifferentCount"].ColumnName="差异量";
			}
			return dtout;
		}

		public double QueryCurrentProductStorage(string strStorageDept,string strProductCode)
		{
			double PStorage=sac.QueryCurrentProductStorage(strStorageDept,strProductCode);
			return PStorage;
		}

		public DataTable GetCurSafeStorage(Hashtable htpara)
		{
			DataTable dtout=sac.GetCurSafeStorage(htpara);
			return dtout;
		}

		public bool UpdateProductSafeCount(Hashtable htpara)
		{
			int recount=sac.UpdateProductSafeCount(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetMakeBillNoRelative()
		{
			DataSet dsout=sac.GetMakeBillNoRelative();
			DataTable dtMakeDetail=dsout.Tables["dtMakeDetail"];
			DataTable dtResult=dsout.Tables["dtResult"];

			string strProSerial="";
			string strMakeType="";
			string strProCode="";
			for(int i=0;i<dtMakeDetail.Rows.Count;i++)
			{
				strProSerial=dtMakeDetail.Rows[i]["cnnProduceSerialNo"].ToString();
				strMakeType=dtMakeDetail.Rows[i]["cnvcMakeType"].ToString();
				strProCode=dtMakeDetail.Rows[i]["cnvcCode"].ToString();
				switch(strMakeType)
				{
					case "0":
						for(int k=0;k<dtResult.Rows.Count;k++)
						{
							if(strProSerial==dtResult.Rows[k]["cnnProduceSerialNo"].ToString()&&strProCode==dtResult.Rows[k]["cnvcCode"].ToString())
							{
								dtResult.Rows[k]["orgCount"]=dtMakeDetail.Rows[i]["cnnCount"].ToString();
								break;
							}
						}
						break;
					case "1":
						for(int k=0;k<dtResult.Rows.Count;k++)
						{
							if(strProSerial==dtResult.Rows[k]["cnnProduceSerialNo"].ToString()&&strProCode==dtResult.Rows[k]["cnvcCode"].ToString())
							{
								dtResult.Rows[k]["addCount"]=dtMakeDetail.Rows[i]["cnnCount"].ToString();
								break;
							}
						}
						break;
					case "2":
						for(int k=0;k<dtResult.Rows.Count;k++)
						{
							if(strProSerial==dtResult.Rows[k]["cnnProduceSerialNo"].ToString()&&strProCode==dtResult.Rows[k]["cnvcCode"].ToString())
							{
								dtResult.Rows[k]["reduceCount"]=dtMakeDetail.Rows[i]["cnnCount"].ToString();
								break;
							}
						}
						break;
				}
			}

			for(int q=0;q<dtResult.Rows.Count;q++)
			{
				double dorg=Math.Round(double.Parse(dtResult.Rows[q]["orgCount"].ToString()),4);
				double dadd=Math.Round(double.Parse(dtResult.Rows[q]["addCount"].ToString()),4);
				double dreduce=Math.Round(double.Parse(dtResult.Rows[q]["reduceCount"].ToString()),4);
				dtResult.Rows[q]["realCount"]=(Math.Round(dorg+dadd-dreduce,4)).ToString();
			}
			return dtResult;
		}

		public bool RelativeMakeToReceive(DataTable dtIn)
		{
			DataTable dtName=new DataTable();
			dtName.Columns.Add("cnnProduceSerialNo");
			dtName.Columns.Add("cnvcProduceDeptID");
			dtName.Columns.Add("cnvcGroup");
			dtName.Columns.Add("cndProduceDate");
			string strProSerial=dtIn.Rows[0]["cnnProduceSerialNo"].ToString();
			string strProduceDeptID=dtIn.Rows[0]["cnvcProduceDeptID"].ToString();
			string strGroup=dtIn.Rows[0]["cnvcGroup"].ToString();
			string strProduceDate=dtIn.Rows[0]["cndProduceDate"].ToString();
			DataRow drnew=dtName.NewRow();
			drnew["cnnProduceSerialNo"]=strProSerial;
			drnew["cnvcProduceDeptID"]=strProduceDeptID;
			drnew["cnvcGroup"]=strGroup;
			drnew["cndProduceDate"]=strProduceDate;
			dtName.Rows.Add(drnew);
			for(int i=1;i<dtIn.Rows.Count;i++)
			{
				if(strProSerial==dtIn.Rows[i]["cnnProduceSerialNo"].ToString()&&strProduceDeptID==dtIn.Rows[i]["cnvcProduceDeptID"].ToString()&&strGroup==dtIn.Rows[i]["cnvcGroup"].ToString())
				{
					continue;
				}
				else
				{
					strProSerial=dtIn.Rows[i]["cnnProduceSerialNo"].ToString();
					strProduceDeptID=dtIn.Rows[i]["cnvcProduceDeptID"].ToString();
					strGroup=dtIn.Rows[i]["cnvcGroup"].ToString();
					strProduceDate=dtIn.Rows[i]["cndProduceDate"].ToString();
					DataRow dr1=dtName.NewRow();
					dr1["cnnProduceSerialNo"]=strProSerial;
					dr1["cnvcProduceDeptID"]=strProduceDeptID;
					dr1["cnvcGroup"]=strGroup;
					dr1["cndProduceDate"]=strProduceDate;
					dtName.Rows.Add(dr1);
				}
			}

			int recount=sac.RelativeMakeToReceive(dtIn,dtName);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetBillOfReceiveNoSend()
		{
			DataTable dtout=sac.GetBillOfReceiveNoSend();
			return dtout;
		}

		public DataTable GetBillOfReceiveTempDetail(DataTable dtID)
		{
			DataTable dtout=new DataTable();
			string strSerialList="";
			if(dtID.Rows.Count>0)
			{
				for(int i=0;i<dtID.Rows.Count;i++)
				{
					strSerialList+=dtID.Rows[i]["cnnReceiveSerialNo"].ToString()+",";
				}
				strSerialList=strSerialList.Substring(0,strSerialList.Length-1);

				DataSet dsout=sac.GetBillOfReceiveTempDetail(strSerialList);
				DataTable dtResult=dsout.Tables["dtResult"];
				DataTable dtDetail=dsout.Tables["dtDetail"];

				string strProCode="";
				string strProName="";
				string strDept="";
				string strOutCount="";
				for(int k=0;k<dtDetail.Rows.Count;k++)
				{
					strProCode=dtDetail.Rows[k]["cnvcProductCode"].ToString();
					strProName=dtDetail.Rows[k]["cnvcProductName"].ToString();
					strDept=dtDetail.Rows[k]["cnvcReceiveDeptID"].ToString();
					strOutCount=dtDetail.Rows[k]["cnnOutCount"].ToString();

					for(int m=0;m<dtResult.Rows.Count;m++)
					{
						if(strProCode==dtResult.Rows[m]["cnvcProductCode"].ToString()&&strProName==dtResult.Rows[m]["cnvcProductName"].ToString())
						{
							dtResult.Rows[m]["cnvc"+strDept]=strOutCount;
						}
					}
				}

				dtout=dtResult.Copy();
			}
			return dtout;
		}

		public bool UpdateBatchBillOfReceiveSend(DataTable dtSendReceiveID,string strOperID,string strOperDate)
		{
			string strSerialList="";
			if(dtSendReceiveID.Rows.Count>0)
			{
				for(int i=0;i<dtSendReceiveID.Rows.Count;i++)
				{
					strSerialList+=dtSendReceiveID.Rows[i]["cnnReceiveSerialNo"].ToString()+",";
				}
				strSerialList=strSerialList.Substring(0,strSerialList.Length-1);
				int recount=sac.UpdateBatchBillOfReceiveSend(strSerialList,strOperID,strOperDate);
				if(recount<=0)
				{
					return false;
				}
				return true;
			}
			else
			{
				return false;
			}			
		}

		public DataTable GetBillOfReceiveSendPrint(string strSendSerial)
		{
			DataSet dsout=sac.GetBillOfReceiveSendPrint(strSendSerial);
			DataTable dtResult=dsout.Tables["dtResult"];
			DataTable dtDetail=dsout.Tables["dtDetail"];

			string strProCode="";
			string strProName="";
			string strDept="";
			string strOutCount="";
			for(int k=0;k<dtDetail.Rows.Count;k++)
			{
				strProCode=dtDetail.Rows[k]["cnvcProductCode"].ToString();
				strProName=dtDetail.Rows[k]["cnvcProductName"].ToString();
				strDept=dtDetail.Rows[k]["cnvcReceiveDeptID"].ToString();
				strOutCount=dtDetail.Rows[k]["cnnOutCount"].ToString();

				for(int m=0;m<dtResult.Rows.Count;m++)
				{
					if(strProCode==dtResult.Rows[m]["cnvcProductCode"].ToString()&&strProName==dtResult.Rows[m]["cnvcProductName"].ToString())
					{
						dtResult.Rows[m]["cnvc"+strDept]=strOutCount;
					}
				}
			}

			return dtResult;
		}

		public DataTable GetProviderStockFillTree(string strQueryType,Hashtable htpara)
		{
			DataTable dtout=sac.GetProviderStockFillTree(strQueryType,htpara);
			return dtout;
		}

		public DataTable GetProviderToGoods(string strProvID,string strProvName)
		{
			DataTable dtout=sac.GetProviderToGoods(strProvID);
			if(dtout!=null)
			{
				dtout.Columns["cnvcGoodsCode"].ColumnName="存货编码";
				dtout.Columns["cnvcInvName"].ColumnName="存货名称";
				dtout.Columns["cnnGoodsPrice"].ColumnName="供货单价";
				dtout.Columns["cnvcPUComUnitCode"].ColumnName="采购计量单位";
				dtout.Columns["cnvcProducer"].ColumnName="厂商";
				dtout.Columns["cnvcInvalidFlag"].ColumnName="有效状态";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmProvGInfo.aspx?pid=" + strProvID + "&pname="+strProvName+"&gid="+dtout.Rows[i]["存货编码"].ToString()+"'>编辑</a>";
				}
			}
			return dtout;
		}

		public bool NewProviderStockAdd(CMSMStruct.ProviderStockStruct pss)
		{
			int recount=sac.NewProviderStockAdd(pss);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public CMSMStruct.ProviderStockStruct GetProvStockOne(string strProvID,string strGoodsID)
		{
			CMSMStruct.ProviderStockStruct pss1=new CommCenter.CMSMStruct.ProviderStockStruct();
			DataTable dtout=sac.GetProvStockOne(strProvID,strGoodsID);
			if(dtout!=null)
			{
				pss1.strGoodsName=dtout.Rows[0]["cnvcInvName"].ToString();
				pss1.strGoodsUnit=dtout.Rows[0]["cnvcComUnitName"].ToString();
				pss1.dGoodsPrice=Math.Round(double.Parse(dtout.Rows[0]["cnnGoodsPrice"].ToString()),2);
				pss1.strInvalidFlag=dtout.Rows[0]["cnvcInvalidFlag"].ToString();
				pss1.strProducer=dtout.Rows[0]["cnvcProducer"].ToString();
			}
			else
			{
				pss1=null;
			}
			return pss1;
		}

		public bool ModProvGoodsInfo(CMSMStruct.ProviderStockStruct pss)
		{
			int recount=sac.ModProvGoodsInfo(pss);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool IsExistProvGoods(string strPrvdID,string strGoodsID)
		{
			int recount=sac.IsExistProvGoods(strPrvdID,strGoodsID);
			if(recount>0)
			{
				return true;
			}

			return false;
		}

		public DataTable GetGoodsToProvider(string strGoodsName)
		{
			DataTable dtout=sac.GetGoodsToProvider(strGoodsName);
			if(dtout!=null)
			{
				dtout.Columns["cnvcGoodsCode"].ColumnName="供应货品编码";
				dtout.Columns["cnvcGoodsName"].ColumnName="供应货品名称";
				dtout.Columns["cnvcPrvdCode"].ColumnName="供应商编码";
				dtout.Columns["cnvcPrvdName"].ColumnName="供应商名称";
				dtout.Columns["cnvcProducer"].ColumnName="厂商";
			}
			return dtout;
		}

		#region 查询采购订单主表信息
		public DataTable GetPoStockMain(Hashtable htpara)
		{
			DataTable dtout=sac.GetPoStockMain(htpara);
			if(dtout!=null)
			{
				dtout.Columns["cnvcPoID"].ColumnName="采购订单号";
				dtout.Columns["cnvcPrvdCode"].ColumnName="供应商编码";
				dtout.Columns["cnvcPoState"].ColumnName="订单状态";
				dtout.Columns["cnvcPlanCycle"].ColumnName="周期";
				dtout.Columns["cnvcCreater"].ColumnName="制单人";
				dtout.Columns["cndCreateDate"].ColumnName="制单时间";
				dtout.Columns["cnvcModer"].ColumnName="修改人";
				dtout.Columns["cndModDate"].ColumnName="修改时间";
				dtout.Columns["cnvcCloser"].ColumnName="完成人";
				dtout.Columns["cndCloseDate"].ColumnName="完成时间";
				dtout.Columns.Add("订单内容");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["订单内容"]="<a href='wfmPoStockAdd.aspx?POID=" + dtout.Rows[i]["采购订单号"].ToString() +"'>内容</a>";
				}
				dtout.Columns.Add("订单明细");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["订单明细"]="<a href='wfmPoStockDetail.aspx?POID=" + dtout.Rows[i]["采购订单号"].ToString() +"&Prvd="+dtout.Rows[i]["供应商编码"].ToString()+"&pos="+dtout.Rows[i]["订单状态"].ToString()+"'>明细</a>";
				}
			}
			return dtout;
		}
		#endregion

		#region 查询采购订单主表单条记录
		public CMSMStruct.PoStockMainStruct GetPoStockMainOne(string strPoID)
		{
			DataTable dtout=sac.GetPoStockMainOne(strPoID);
			CMSMStruct.PoStockMainStruct psm1=new CommCenter.CMSMStruct.PoStockMainStruct();
			if(dtout!=null)
			{
				psm1.strPoID=dtout.Rows[0]["cnvcPoID"].ToString();
				psm1.strPrvdCode=dtout.Rows[0]["cnvcPrvdCode"].ToString();
				psm1.strAddress=dtout.Rows[0]["cnvcAddress"].ToString();
				psm1.strComments=dtout.Rows[0]["cnvcComments"].ToString();
				psm1.strPoState=dtout.Rows[0]["cnvcPoState"].ToString();
				psm1.strPlanCycle=dtout.Rows[0]["cnvcPlanCycle"].ToString();
				psm1.strCreater=dtout.Rows[0]["cnvcCreater"].ToString();
				psm1.strModer=dtout.Rows[0]["cnvcModer"].ToString();
				psm1.strChecker=dtout.Rows[0]["cnvcChecker"].ToString();
				psm1.strCloser=dtout.Rows[0]["cnvcCloser"].ToString();
				if(dtout.Rows[0]["cndCreateDate"].ToString()!="")
					psm1.dtCreateDate=(DateTime)dtout.Rows[0]["cndCreateDate"];

				if(dtout.Rows[0]["cndModDate"].ToString()!="")
					psm1.dtModDate=(DateTime)dtout.Rows[0]["cndModDate"];

				if(dtout.Rows[0]["cndCheckDate"].ToString()!="")
					psm1.dtCheckDate=(DateTime)dtout.Rows[0]["cndCheckDate"];

				if(dtout.Rows[0]["cndCloseDate"].ToString()!="")
					psm1.dtCloseDate=(DateTime)dtout.Rows[0]["cndCloseDate"];
			}
			return psm1;
		}
		#endregion

		#region 检查是否存在相同供应商和相同周期的订单
		public bool IsExistPoProviderCycle(string strProvCode,string strCycle)
		{
			if(sac.IsExistPoProviderCycle(strProvCode,strCycle)=="0")
				return false;
			else
				return true;
		}
		#endregion

		#region 新增采购订单主表记录
		public bool NewPoSotckMainAdd(CMSMStruct.PoStockMainStruct psm1)
		{
			int recount=sac.NewPoSotckMainAdd(psm1);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		#endregion

		#region 修改采购订单主表内容
		public bool ModPoSotckMainInfo(CMSMStruct.PoStockMainStruct psmnew,CMSMStruct.PoStockMainStruct psmold)
		{
			int recount=sac.ModPoSotckMainInfo(psmnew,psmold);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		#endregion

		#region 快速检索供应商存货等信息
		public DataTable GetGoodsBySelect(string strPrvdCode,string strGoodsCode,string strGoodsName)
		{
			DataTable dtout=sac.GetGoodsBySelect(strPrvdCode,strGoodsCode,strGoodsName);
			return dtout;
		}

		public DataTable GetPoStockGoodsBySelect(string strPoID,string strPrvdID,string strGoodsCode,string strGoodsName)
		{
			DataTable dtout=sac.GetPoStockGoodsBySelect(strPoID,strPrvdID,strGoodsCode,strGoodsName);
			return dtout;
		}

		public CMSMStruct.ProviderStockStruct GetStockGoodsOneInfo(string strPrvdCode,string strGoodsCode)
		{
			DataTable dtout=sac.GetStockGoodsOneInfo(strPrvdCode,strGoodsCode);
			CMSMStruct.ProviderStockStruct pss1=new CommCenter.CMSMStruct.ProviderStockStruct();
			if(dtout!=null)
			{
				pss1.strPrvdCode=dtout.Rows[0]["cnvcPrvdCode"].ToString();
				pss1.strGoodsCode=dtout.Rows[0]["cnvcGoodsCode"].ToString();
				pss1.strGoodsName=dtout.Rows[0]["cnvcGoodsName"].ToString();
				pss1.dGoodsPrice=Math.Round(double.Parse(dtout.Rows[0]["cnnGoodsPrice"].ToString()),2);
				pss1.strGoodsUnit=dtout.Rows[0]["cnvcGoodsUnit"].ToString();
				pss1.strGoodsQuality=dtout.Rows[0]["cnvcGoodsQuality"].ToString();
			}
			else
			{
				pss1=null;
			}
			return pss1;
		}
		#endregion

		#region 查询采购订单明细和合计
		public DataSet GetPoStockDetailSum(string strPoID)
		{
			DataSet dsout=sac.GetPoStockDetailSum(strPoID);
			if(dsout!=null)
			{
				dsout.Tables["sum"].Columns["cnvcPoID"].ColumnName="采购订单号";
				dsout.Tables["sum"].Columns["cnvcGoodsCode"].ColumnName="供应货品编码";
				dsout.Tables["sum"].Columns["cnvcGroupCode"].ColumnName="计量单位组";
				dsout.Tables["sum"].Columns["cnvcStockUnit"].ColumnName="单位";
				dsout.Tables["sum"].Columns["cnnStockPrice"].ColumnName="单价";
				dsout.Tables["sum"].Columns["cnnStockCountSum"].ColumnName="累计订单数量";
				dsout.Tables["sum"].Columns["cnnStockFeeSum"].ColumnName="累计订单金额";
				dsout.Tables["sum"].Columns["cnnArriveCount"].ColumnName="累计到货数量";
				dsout.Tables["sum"].Columns["cnnArriveFee"].ColumnName="累计到货金额";
				dsout.Tables["sum"].Columns["cnnFinallyRate"].ColumnName="完成比例";
			}
			return dsout;
		}
		#endregion

		#region 检查是否存在同一订单下相同部门和相同货品的子订单
		public bool IsExistPoStockDetail(string strPoID,string strDeptID,string strGoodsCode)
		{
			if(sac.IsExistPoStockDetail(strPoID,strDeptID,strGoodsCode)=="0")
				return false;
			else
				return true;
		}
		#endregion

		#region 采购子订单表操作记录
		public bool NewPoSotckDetailAdd(CMSMStruct.PoStockDetailStruct psds,string stroperid)
		{
			int recount=sac.NewPoSotckDetailAdd(psds,stroperid);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool PoSotckDetailMod(Hashtable htpara)
		{
			int recount=sac.PoSotckDetailMod(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool PoSotckDetailDelete(Hashtable htpara)
		{
			int recount=sac.PoSotckDetailDelete(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool PoSotckDetailChecked(Hashtable htpara)
		{
			int recount=sac.PoSotckDetailChecked(htpara);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool IsExistPoStockDetailStateUncheck(string strPoID)
		{
			if(sac.IsExistPoStockDetailStateUncheck(strPoID)=="0")
				return false;
			else
				return true;
		}

		public bool PoSotckMainChecked(string strPoID,string strChecker)
		{
			int recount=sac.PoSotckMainChecked(strPoID,strChecker);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool IsExistPoStockDetailStock(string strPoID)
		{
			if(sac.IsExistPoStockDetailStock(strPoID)=="0")
				return false;
			else
				return true;
		}

		public bool PoSotckMainExecing(string strPoID)
		{
			int recount=sac.PoSotckMainExecing(strPoID);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool PoSotckMainClose(string strPoID,string strCloser)
		{
			int recount=sac.PoSotckMainClose(strPoID,strCloser);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}
		#endregion

		#region 查询采购入库单主表信息
		public DataTable GetPoStockEnterMain(Hashtable htpara)
		{
			DataTable dtout=sac.GetPoStockEnterMain(htpara);
			if(dtout!=null)
			{
				dtout.Columns["cnnRdID"].ColumnName="主表标识";
				dtout.Columns["cnvcCode"].ColumnName="采购入库单号";
				dtout.Columns["cnvcDepID"].ColumnName="部门";
				dtout.Columns["cnvcWhCode"].ColumnName="仓库";
				dtout.Columns["cnvcMaker"].ColumnName="制单人";
				dtout.Columns["cndARVDate"].ColumnName="到货日期";
				dtout.Columns["cnvcState"].ColumnName="状态";
				dtout.Columns.Add("单据内容");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["单据内容"]="<a href='../zhenghua/Storage/wfmPoStockEnterAdd.aspx?rdid=" + dtout.Rows[i]["主表标识"].ToString() +"'>内容</a>";
				}
				dtout.Columns.Add("单据明细");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["单据明细"]="<a href='../zhenghua/Storage/wfmPoStockEnterDetail.aspx?rdid=" + dtout.Rows[i]["主表标识"].ToString()+"&code="+dtout.Rows[i]["采购入库单号"].ToString()+"&dept="+dtout.Rows[i]["部门"].ToString()+"&whid="+dtout.Rows[i]["仓库"].ToString()+"'>明细</a>";
				}
			}
			return dtout;
		}
		#endregion

        public DataTable GetGoodsChart(string strMonth, string strGoodsID)
        {
            DataTable dtout = sac.GetGoodsChart(strMonth, strGoodsID);
            return dtout;
        }

        public DataTable ChartSeachGoods(string strGoodType, string strGoodName)
        {
            DataTable dtout = sac.ChartSeachGoods(strGoodType, strGoodName);
            return dtout;
        }
	}
}
