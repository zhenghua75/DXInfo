using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// Summary description for EmpBusi.
	/// </summary>
	public class EmpBusi
	{
		string strcon="";
		EmpAcc Ea=null;
		public EmpBusi(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			Ea=new EmpAcc(strcon);
		}

		public DataTable GetEmployees(Hashtable htpara)
		{
			DataTable dtout=Ea.GetEmployees(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcCardID"].ColumnName="员工卡号";
				dtout.Columns["vcEmpName"].ColumnName="员工姓名";
				dtout.Columns["vcSex"].ColumnName="员工性别";
				dtout.Columns["vcEmpNbr"].ColumnName="身份证号";
				dtout.Columns["dtInDate"].ColumnName="入职时间";
				dtout.Columns["vcDegree"].ColumnName="学历";
				dtout.Columns["vcLinkPhone"].ColumnName="联系电话";
				dtout.Columns["vcAddress"].ColumnName="联系地址";
				dtout.Columns["vcOfficer"].ColumnName="职务";
				dtout.Columns["vcDeptID"].ColumnName="当前所属门店";
				dtout.Columns["vcFlag"].ColumnName="在职情况";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns["dtOperDate"].ColumnName="操作时间";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["在职情况"].ToString()=="0")
					{
						dtout.Rows[i]["操作"]="<a href='wfmEmpDetail.aspx?id=" + dtout.Rows[i]["员工卡号"].ToString() + "'>调整员工</a>";
					}
					else
					{
						dtout.Rows[i]["操作"]="";
					}
				}
			}
			return dtout;
		}

		public bool ChkEmpCardIDDup(string strCardID)
		{
			string strid=Ea.getEmpCardID(strCardID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkEmpNameDup(string strGoodsName)
		{
			string strname=Ea.getEmpName(strGoodsName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertEmployee(CMSMStruct.EmployeeStruct es)
		{
			int recount=Ea.InsertEmployee(es);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool ModEmployee(CMSMStruct.EmployeeStruct esold,CMSMStruct.EmployeeStruct esnew)
		{
			int recount=Ea.ModEmployee(esold,esnew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public CMSMStruct.EmployeeStruct GetEmpInfo(string strCardid)
		{
			DataTable dtout=Ea.GetEmpInfo(strCardid);
			CMSMStruct.EmployeeStruct es1=new CommCenter.CMSMStruct.EmployeeStruct();
			if(dtout!=null)
			{
				es1.strCardID=dtout.Rows[0]["vcCardID"].ToString();
				es1.strEmpName=dtout.Rows[0]["vcEmpName"].ToString();
				es1.strSex=dtout.Rows[0]["vcSex"].ToString();
				es1.strEmpNbr=dtout.Rows[0]["vcEmpNbr"].ToString();
				es1.strInDate=dtout.Rows[0]["dtInDate"].ToString();
				es1.strDegree=dtout.Rows[0]["vcDegree"].ToString();
				es1.strLinkPhone=dtout.Rows[0]["vcLinkPhone"].ToString();
				es1.strAddress=dtout.Rows[0]["vcAddress"].ToString();
				es1.strOfficer=dtout.Rows[0]["vcOfficer"].ToString();
				es1.strDeptID=dtout.Rows[0]["vcDeptID"].ToString();
				es1.strFlag=dtout.Rows[0]["vcFlag"].ToString();
				es1.strComments=dtout.Rows[0]["vcComments"].ToString();
			}
			return es1;
		}

		public CMSMStruct.EmployeeStruct GetEmpInfoByName(string strEmpName)
		{
			DataTable dtout=Ea.GetEmpInfoByName(strEmpName);
			CMSMStruct.EmployeeStruct es1=new CommCenter.CMSMStruct.EmployeeStruct();
			if(dtout!=null)
			{
				es1.strCardID=dtout.Rows[0]["vcCardID"].ToString();
				es1.strEmpName=dtout.Rows[0]["vcEmpName"].ToString();
				es1.strOfficer=dtout.Rows[0]["vcCommName"].ToString();
			}
			return es1;
		}

		public DataSet GetEmpSchList(Hashtable htpara)
		{
			DataSet dsout=Ea.GetEmpSchList(htpara);
			return dsout;
		}

		public DataTable GetEmpManager(string strDeptID)
		{
			DataTable dtout=Ea.GetEmpManager(strDeptID);
			return dtout;
		}

		public DataTable GetAllEmpName(string strDeptID,string strSchID)
		{
			DataTable dtout=Ea.GetAllEmpName(strDeptID,strSchID);
			return dtout;
		}

		public bool SchedEmpDaily(Hashtable htpara,ArrayList alSched)
		{
			int recount=Ea.SchedEmpDaily(htpara,alSched);
			if(recount==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool SchedEmpDailyEvery(CMSMStruct.EmpSchLogStruct empsl)
		{
			int recount=Ea.SchedEmpDailyEvery(empsl);
			if(recount<1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool SchedEmpDailyEveryDel(string strShcID,string strEmpName,string strClass)
		{
			int recount=Ea.SchedEmpDailyEveryDel(strShcID,strEmpName,strClass);
			if(recount<1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool IsEmpSchAllDayWork(string strShcID,string strEmpName,string strClass)
		{
			int recount=Ea.IsEmpSchAllDayWork(strShcID,strEmpName,strClass);
			if(recount<1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public DataTable GetEmpSchLog(string strShcID)
		{
			DataTable dtout=Ea.GetEmpSchLog(strShcID);
			if(dtout!=null)
			{
				dtout.Columns["vcDeptName"].ColumnName="门店名称";
				dtout.Columns["SchDate"].ColumnName="排班日期";
				dtout.Columns["IsSch"].ColumnName="是否排班";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["是否排班"].ToString()=="1")
					{
						dtout.Rows[i]["是否排班"]="是";
						dtout.Rows[i]["操作"]="<a href='wfmDeptShcDetail.aspx?dept=" + dtout.Rows[i]["门店名称"].ToString() + "&date="+ dtout.Rows[i]["排班日期"].ToString() +"'>查看详情</a>";
					}
					else
					{
						dtout.Rows[i]["是否排班"]="否";
						dtout.Rows[i]["操作"]="";
					}
				}
			}
			return dtout;
		}

		public DataTable GetEveryEmpSchLog(string strShcID,string strDeptID,string strDeptName)
		{
			DataTable dtout=Ea.GetEveryEmpSchLog(strShcID,strDeptID,strDeptName);
			if(dtout!=null)
			{
				dtout.Columns["vcCardID"].ColumnName="员工卡号";
				dtout.Columns["vcEmpName"].ColumnName="员工姓名";
				dtout.Columns["vcEmpOF"].ColumnName="员工职务";
				dtout.Columns["vcClass"].ColumnName="班次";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["班次"].ToString()!="")
					{
						dtout.Rows[i]["操作"]="<a href='wfmEmpEverySchDel.aspx?date="+strShcID+"&emp=" + dtout.Rows[i]["员工姓名"].ToString() + "&class="+ dtout.Rows[i]["班次"].ToString() +"'>删除</a>";
					}
				}
			}
			return dtout;
		}

		public ArrayList GetDeptSchDetail(string strDeptName,string strShcID)
		{
			DataSet dsout=Ea.GetDeptSchDetail(strDeptName,strShcID);
			ArrayList alDeptSch=new ArrayList();
			if(dsout!=null)
			{
				DataTable dtSch=dsout.Tables["SchDetail"];
				DataTable dtStruct=dsout.Tables["Struct"];

				for(int i=0;i<dtStruct.Rows.Count;i++)
				{
					CMSMStruct.DeptSchStruct deptsch=new CommCenter.CMSMStruct.DeptSchStruct();
					deptsch.strEmpOF=dtStruct.Rows[i]["vcEmpOF"].ToString();
					deptsch.strClass=dtStruct.Rows[i]["vcClass"].ToString();
					deptsch.strCheckIn=dtStruct.Rows[i]["dtCheckIn"].ToString();
					deptsch.strCheckOut=dtStruct.Rows[i]["dtCheckOut"].ToString();
					alDeptSch.Add(deptsch);
				}
				
				foreach(CMSMStruct.DeptSchStruct deptstmp in alDeptSch)
				{
					for(int j=0;j<dtSch.Rows.Count;j++)
					{
						if(dtSch.Rows[j]["vcEmpOF"].ToString()==deptstmp.strEmpOF&&dtSch.Rows[j]["vcClass"].ToString()==deptstmp.strClass&&dtSch.Rows[j]["dtCheckIn"].ToString()==deptstmp.strCheckIn&&dtSch.Rows[j]["dtCheckOut"].ToString()==deptstmp.strCheckOut)
						{					
							if(deptstmp.strSIOTID==null||deptstmp.strSIOTID=="")
							{
								deptstmp.strSIOTID=dtSch.Rows[j]["vcSchID"].ToString();
								deptstmp.strDeptName=dtSch.Rows[j]["vcDeptName"].ToString();
								deptstmp.strManager=dtSch.Rows[j]["vcManager"].ToString();
//								deptstmp.strCheckIn=dtSch.Rows[j]["dtCheckIn"].ToString();
//								deptstmp.strCheckOut=dtSch.Rows[j]["dtCheckOut"].ToString();
							}
							if(deptstmp.strEmpNameGroup==null||deptstmp.strEmpNameGroup=="")
							{
								deptstmp.strEmpNameGroup+=dtSch.Rows[j]["vcEmpName"].ToString();
							}
							else
							{
								deptstmp.strEmpNameGroup+="，"+dtSch.Rows[j]["vcEmpName"].ToString();
							}
						}
					}
				}

				foreach(CMSMStruct.DeptSchStruct deptstmp in alDeptSch)
				{						
					if(deptstmp.strEmpNameGroup==null||deptstmp.strEmpNameGroup=="")
					{
						deptstmp.strEmpNameGroup="未安排";
					}
				}
			}
			return alDeptSch;
		}

		public ArrayList GetSignCalcResultQuery(string strDept,string strDate)
		{
			DataTable dtout=Ea.GetSignCalcResultQuery(strDept,strDate);
			ArrayList alDate=new ArrayList();
			if(dtout!=null)
			{
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					alDate.Add(dtout.Rows[i]["vcday"].ToString());
				}
			}
			return alDate;
		}

		public DataTable GetSignSumQuery(Hashtable htpara)
		{
			DataSet dsget=Ea.GetSignSumQuery(htpara);
			DataTable dtout=dsget.Tables["t1"];
			DataTable dtEmpList=dsget.Tables["t2"];
			
			if(dtEmpList!=null&&dtEmpList.Rows.Count>0)
			{
				string strsno="";
				string strEmpList="";
				for(int i=0;i<dtEmpList.Rows.Count;i++)
				{
					if(strsno=="")
					{
						strEmpList="<a href='wfmSignQuerySLink.aspx?begin="+htpara["strBegin"].ToString()+"&end="+htpara["strEnd"].ToString()+"&dept="+htpara["strDeptName"].ToString()+"&emp="+dtEmpList.Rows[i]["vcEmpName"].ToString()+"'>"+dtEmpList.Rows[i]["vcEmpName"].ToString()+"("+dtEmpList.Rows[i]["eCount"].ToString()+")</a> ";
						strsno=dtEmpList.Rows[i]["sno"].ToString();
					}
					else
					{
						if(strsno==dtEmpList.Rows[i]["sno"].ToString())
						{
							strEmpList+="<a href='wfmSignQuerySLink.aspx?begin="+htpara["strBegin"].ToString()+"&end="+htpara["strEnd"].ToString()+"&dept="+htpara["strDeptName"].ToString()+"&emp="+dtEmpList.Rows[i]["vcEmpName"].ToString()+"'>"+dtEmpList.Rows[i]["vcEmpName"].ToString()+"("+dtEmpList.Rows[i]["eCount"].ToString()+")</a> ";
						}
						else
						{
							for(int j=0;j<dtout.Rows.Count;j++)
							{
								if(strsno==dtout.Rows[j]["sno"].ToString())
								{
									dtout.Rows[j]["EmpList"]=strEmpList;
									break;
								}
							}
							strEmpList="<a href='wfmSignQuerySLink.aspx?begin="+htpara["strBegin"].ToString()+"&end="+htpara["strEnd"].ToString()+"&dept="+htpara["strDeptName"].ToString()+"&emp="+dtEmpList.Rows[i]["vcEmpName"].ToString()+"'>"+dtEmpList.Rows[i]["vcEmpName"].ToString()+"("+dtEmpList.Rows[i]["eCount"].ToString()+")</a> ";
							strsno=dtEmpList.Rows[i]["sno"].ToString();
						}
					}

					if(i==dtEmpList.Rows.Count-1)
					{
						for(int k=0;k<dtout.Rows.Count;k++)
						{
							if(strsno==dtout.Rows[k]["sno"].ToString())
							{
								dtout.Rows[k]["EmpList"]=strEmpList;
								break;
							}
						}
					}
				}
			}
			dtout.Columns["sno"].ColumnName="序号";
			dtout.Columns["type"].ColumnName="考勤类型";
			dtout.Columns["TolEmpCount"].ColumnName="总人数";
			dtout.Columns["TolCount"].ColumnName="总次数";
			dtout.Columns["EmpList"].ColumnName="名单(次)";

			return dtout;
		}

		public DataTable GetSignDetailQuery(Hashtable htpara)
		{
			DataTable dtout=Ea.GetSignDetailQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcDept"].ColumnName="门店名称";
				dtout.Columns["vcCardID"].ColumnName="员工卡号";
				dtout.Columns["vcEmpName"].ColumnName="员工姓名";
				dtout.Columns["vcSignDate"].ColumnName="考勤日期";
				dtout.Columns["vcClass"].ColumnName="班次";
				dtout.Columns["dtSignIn"].ColumnName="签到时间";
				dtout.Columns["dtSignOut"].ColumnName="签退时间";
				dtout.Columns["vcSignResult"].ColumnName="考勤结果";
				dtout.Columns["vcComments"].ColumnName="备注";
			}
			return dtout;
		}

		public DataTable GetSignUnitQuery(Hashtable htpara)
		{
			DataTable dtout=Ea.GetSignUnitQuery(htpara);
			if(dtout!=null)
			{
				if(htpara["strType"].ToString()=="0")
				{
					dtout.Columns["vcDept"].ColumnName="门店名称";
					dtout.Columns["vcCardID"].ColumnName="员工卡号";
					dtout.Columns["vcEmpName"].ColumnName="员工姓名";
					dtout.Columns["vcSignDate"].ColumnName="考勤日期";
					dtout.Columns["vcClass"].ColumnName="班次";
					dtout.Columns["vcOfficer"].ColumnName="职务";
					dtout.Columns["dtSignIn"].ColumnName="签到时间";
					dtout.Columns["dtSignOut"].ColumnName="签退时间";
					dtout.Columns["vcSignResult"].ColumnName="考勤结果";
					dtout.Columns["vcComments"].ColumnName="备注";
				}
				else
				{
					dtout.Columns["vcCommName"].ColumnName="门店名称";
					dtout.Columns["vcCardID"].ColumnName="员工卡号";
					dtout.Columns["vcEmpName"].ColumnName="员工姓名";
					dtout.Columns["dtSignDate"].ColumnName="刷卡时间";
					dtout.Columns["vcSignFlag"].ColumnName="考勤标志";
					dtout.Columns["vcComments"].ColumnName="备注";
				}
			}
			return dtout;
		}

		//考勤计算
		public bool SignCalc(Hashtable htPara,out string strerr)
		{
			strerr="";
			DataSet dsout=Ea.GetSignCalcPara(htPara);
			if(dsout!=null&&dsout.Tables.Count==2)
			{
				DataTable dtSignLog=dsout.Tables["SignLog"];
				DataTable dtSchLog=dsout.Tables["SchLog"];
				if(dtSignLog.Rows.Count==0)
				{
					strerr="在计算时间内没有任何考勤记录！";
					return false;
				}
				if(dtSchLog.Rows.Count==0)
				{
					strerr="在计算时间内没有任何排班记录！";
					return false;
				}
				Hashtable htDateSign=new Hashtable();
				Hashtable htCardtmp=new Hashtable();
				ArrayList alSignLog=new ArrayList();
				string strDateTmp=dtSignLog.Rows[0]["vcSignID"].ToString();
				string strCardTmp=dtSignLog.Rows[0]["vcCardID"].ToString();
				for(int i=0;i<dtSignLog.Rows.Count;i++)
				{
					CMSMStruct.SignAtomStruct satom=new CommCenter.CMSMStruct.SignAtomStruct();
					satom.strSignFlag=dtSignLog.Rows[i]["vcSignFlag"].ToString();
					satom.dtSignDate=DateTime.Parse(dtSignLog.Rows[i]["dtSignDate"].ToString());
					satom.strComments=dtSignLog.Rows[i]["vcComments"].ToString();

					if(strDateTmp!=dtSignLog.Rows[i]["vcSignID"].ToString())
					{
						htCardtmp.Add(strCardTmp,alSignLog);
						strCardTmp=dtSignLog.Rows[i]["vcCardID"].ToString();
						htDateSign.Add(strDateTmp,htCardtmp);
						htCardtmp=new Hashtable();
						strDateTmp=dtSignLog.Rows[i]["vcSignID"].ToString();
						strCardTmp=dtSignLog.Rows[i]["vcCardID"].ToString();
						alSignLog=new ArrayList();
					}

					if(strCardTmp!=dtSignLog.Rows[i]["vcCardID"].ToString())
					{
						htCardtmp.Add(strCardTmp,alSignLog);
						strCardTmp=dtSignLog.Rows[i]["vcCardID"].ToString();
						alSignLog=new ArrayList();
					}
					
					alSignLog.Add(satom);

					if(i==dtSignLog.Rows.Count-1)
					{
						htCardtmp.Add(strCardTmp,alSignLog);
						strCardTmp=dtSignLog.Rows[i]["vcCardID"].ToString();
						htDateSign.Add(strDateTmp,htCardtmp);
					}
				}

				ArrayList alSignListFin=new ArrayList();
				if(dtSchLog!=null&&dtSchLog.Rows.Count>0)
				{
					DateTime dtCheckIn;
					DateTime dtCheckOut;
					DateTime timeold;

					#region 循环每天每个员工的排班表
					for(int j=0;j<dtSchLog.Rows.Count;j++)
					{
						CMSMStruct.SignListStruct sls=new CommCenter.CMSMStruct.SignListStruct();
						sls.strSignDate=dtSchLog.Rows[j]["vcSchID"].ToString();
						sls.strCardID=dtSchLog.Rows[j]["vcCardID"].ToString();
						sls.strClass=dtSchLog.Rows[j]["vcClass"].ToString();
						sls.strEmpName=dtSchLog.Rows[j]["vcEmpName"].ToString();
						sls.strDept=htPara["strDeptName"].ToString();
						sls.strOfficer=dtSchLog.Rows[j]["vcEmpOF"].ToString();
						sls.strSignIn="";
						sls.strSignOut="";
						sls.strSignState="0000000000";
						sls.strSignResult="";
						sls.strComments="";

						#region 某天某员工排班表的所有打卡记录
						Hashtable htCardSignAll=(Hashtable)htDateSign[sls.strSignDate];
						if(htCardSignAll!=null)
						{
							ArrayList alCardSignLog=(ArrayList)htCardSignAll[sls.strCardID];
							bool SignInFlag=false;
							bool SignOutFlag=false;
							bool SignQingJiaFlag=false;

							if(alCardSignLog!=null)
							{
								dtCheckIn=DateTime.Parse(dtSchLog.Rows[j]["dtCheckIn"].ToString());
								dtCheckOut=DateTime.Parse(dtSchLog.Rows[j]["dtCheckOut"].ToString());

								#region 对某天某员工的所有打卡记录循环，确认出有效的一次打卡记录，因为同一事项会打多次卡
								foreach(CMSMStruct.SignAtomStruct sastmp in alCardSignLog)
								{
									switch(sastmp.strSignFlag)
									{
										case "1":
											#region 确认签到有效记录，打卡时间在上班时间前后1小时内为有效，上班时间前1小时内为不迟到，过了上班时间点为迟到
											if((sastmp.dtSignDate.CompareTo(dtCheckIn.AddHours(-2)))>=0&&(sastmp.dtSignDate.CompareTo(dtCheckIn.AddHours(1)))<=0)
											{
												if(sls.strSignIn=="")
												{
													sls.strSignIn=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
													if((sastmp.dtSignDate.CompareTo(dtCheckIn))>0)
													{
														sls.strSignState="1"+sls.strSignState.Substring(1,9);
													}
													else
													{
														sls.strSignState="0"+sls.strSignState.Substring(1,9);
													}
												}
												else
												{
													timeold=DateTime.Parse(sls.strSignIn);
													if((sastmp.dtSignDate.CompareTo(timeold))<0)
													{
														sls.strSignIn=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
														if((sastmp.dtSignDate.CompareTo(dtCheckIn))>0)
														{
															sls.strSignState="1"+sls.strSignState.Substring(1,9);
														}
														else
														{
															sls.strSignState="0"+sls.strSignState.Substring(1,9);
														}
													}
												}
												SignInFlag=true;
											}
											#endregion
											break;
										case "2":
											#region 确认签退有效记录，打卡时间只要在当天内都均有效，下班时间前为早退，后为不早退
											if(sastmp.dtSignDate.Day==dtCheckOut.Day)
											{
												if(sls.strSignOut=="")
												{
													sls.strSignOut=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
													if((sastmp.dtSignDate.CompareTo(dtCheckOut))<0)
													{
														sls.strSignState=sls.strSignState.Substring(0,1)+"1"+sls.strSignState.Substring(2,8);
													}
													else
													{
														sls.strSignState=sls.strSignState.Substring(0,1)+"0"+sls.strSignState.Substring(2,8);
													}
												}
												else
												{
													timeold=DateTime.Parse(sls.strSignOut);
													if((sastmp.dtSignDate.CompareTo(timeold))<0)
													{
														sls.strSignOut=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
														if((sastmp.dtSignDate.CompareTo(dtCheckOut))<0)
														{
															sls.strSignState=sls.strSignState.Substring(0,1)+"1"+sls.strSignState.Substring(2,8);
														}
														else
														{
															sls.strSignState=sls.strSignState.Substring(0,1)+"0"+sls.strSignState.Substring(2,8);
														}
													}
												}
												SignOutFlag=true;
											}
											#endregion
											break;
										case "3":
											#region 确认病假有效记录
											if(sls.strSignIn==""&&sls.strSignOut=="")
											{
												sls.strSignIn=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
												sls.strSignOut=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
												sls.strSignState=sls.strSignState.Substring(0,2)+"1"+sls.strSignState.Substring(3,7);
												sls.strComments=sastmp.strComments;
											}
											else if((sls.strSignIn!=""&&sls.strSignOut=="")||(sls.strSignIn==""&&sls.strSignOut!=""))
											{
												sls.strSignState=sls.strSignState.Substring(0,2)+"1"+sls.strSignState.Substring(3,7);
												sls.strComments+=sastmp.strComments;
											}
											SignQingJiaFlag=true;
											#endregion
											break;
										case "4":
											#region 确认事假有效记录
											if(sls.strSignIn==""&&sls.strSignOut=="")
											{
												sls.strSignIn=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
												sls.strSignOut=sastmp.dtSignDate.ToShortDateString()+" "+sastmp.dtSignDate.ToLongTimeString();
												sls.strSignState=sls.strSignState.Substring(0,3)+"1"+sls.strSignState.Substring(4,6);
												sls.strComments=sastmp.strComments;
											}
											else if((sls.strSignIn!=""&&sls.strSignOut=="")||(sls.strSignIn==""&&sls.strSignOut!=""))
											{
												sls.strSignState=sls.strSignState.Substring(0,3)+"1"+sls.strSignState.Substring(4,6);
												sls.strComments+=sastmp.strComments;
											}
											SignQingJiaFlag=true;
											#endregion
											break;
										default:
											break;
									}
								}
								#endregion
							}
						
							//无请假，无签到，有签退，算迟到处理
							if(!SignQingJiaFlag&&!SignInFlag&&SignOutFlag)
							{
								sls.strSignState="1"+sls.strSignState.Substring(1,9);
							}
							//无请假，有签到，无签退，算早退处理
							if(!SignQingJiaFlag&&SignInFlag&&!SignOutFlag)
							{
								sls.strSignState=sls.strSignState.Substring(0,1)+"1"+sls.strSignState.Substring(2,8);
							}
							//无请假，无签到，无签退，等待后续处理，按签到或签退时间有无，两都均无为缺勤，否则为正常出勤
							if(!SignQingJiaFlag&&!SignInFlag&&!SignOutFlag)
							{
								sls.strSignState="0000000000";
							}
						}
						#endregion

						#region 经过筛选确认，考勤状态为“0000000000”是无打卡记录或已经正常出勤，否则为非正常出勤
						if(int.Parse(sls.strSignState)>0)
						{
							string strflag="";
							for(int k=0;k<8;k++)
							{
								strflag=sls.strSignState.Substring(k,1);
								if(strflag=="1")
								{
									switch(k)
									{
										case 0:
											sls.strSignResult+="迟到,";
											break;
										case 1:
											sls.strSignResult+="早退,";
											break;
										case 2:
											sls.strSignResult+="病假,";
											break;
										case 3:
											sls.strSignResult+="事假,";
											break;
										case 4:
											sls.strSignResult+="";
											break;
										case 5:
											sls.strSignResult+="";
											break;
										case 6:
											sls.strSignResult+="";
											break;
										case 7:
											sls.strSignResult+="";
											break;
										case 8:
											sls.strSignResult+="";
											break;
									}
								}											
							}
							sls.strSignResult=sls.strSignResult.Substring(0,sls.strSignResult.Length-1);
						}
						else
						{
							if(sls.strSignIn==""&&sls.strSignOut=="")
							{
								sls.strSignState="0000000001";
								sls.strSignResult="缺勤";
							}
							else
							{
								sls.strSignResult="正常出勤";
							}
						}
						#endregion
						
						alSignListFin.Add(sls);
					}
					#endregion

					return Ea.InsertSignCalc(htPara,alSignListFin);
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public DataTable GetEmployInfo()
		{
			DataTable dtout=Ea.GetEmployInfo();
			return dtout;
		}

		public bool IsEmpSchExist(string strSchID,string strDeptName,string strCardID,string strClass)
		{
			if(Ea.IsEmpSchExist(strSchID,strDeptName,strCardID,strClass)==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public DataTable GetDeptManagerList()
		{
			DataTable dtout=Ea.GetDeptManagerList();
			return dtout;
		}

		public void SchedEmpDailyBatch(DataTable dt)
		{
			Ea.SchedEmpDailyBatch(dt);
		}
	}
}
