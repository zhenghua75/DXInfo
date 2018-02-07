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
				dtout.Columns["vcCardID"].ColumnName="Ա������";
				dtout.Columns["vcEmpName"].ColumnName="Ա������";
				dtout.Columns["vcSex"].ColumnName="Ա���Ա�";
				dtout.Columns["vcEmpNbr"].ColumnName="���֤��";
				dtout.Columns["dtInDate"].ColumnName="��ְʱ��";
				dtout.Columns["vcDegree"].ColumnName="ѧ��";
				dtout.Columns["vcLinkPhone"].ColumnName="��ϵ�绰";
				dtout.Columns["vcAddress"].ColumnName="��ϵ��ַ";
				dtout.Columns["vcOfficer"].ColumnName="ְ��";
				dtout.Columns["vcDeptID"].ColumnName="��ǰ�����ŵ�";
				dtout.Columns["vcFlag"].ColumnName="��ְ���";
				dtout.Columns["vcComments"].ColumnName="��ע";
				dtout.Columns["dtOperDate"].ColumnName="����ʱ��";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["��ְ���"].ToString()=="0")
					{
						dtout.Rows[i]["����"]="<a href='wfmEmpDetail.aspx?id=" + dtout.Rows[i]["Ա������"].ToString() + "'>����Ա��</a>";
					}
					else
					{
						dtout.Rows[i]["����"]="";
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
				dtout.Columns["vcDeptName"].ColumnName="�ŵ�����";
				dtout.Columns["SchDate"].ColumnName="�Ű�����";
				dtout.Columns["IsSch"].ColumnName="�Ƿ��Ű�";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["�Ƿ��Ű�"].ToString()=="1")
					{
						dtout.Rows[i]["�Ƿ��Ű�"]="��";
						dtout.Rows[i]["����"]="<a href='wfmDeptShcDetail.aspx?dept=" + dtout.Rows[i]["�ŵ�����"].ToString() + "&date="+ dtout.Rows[i]["�Ű�����"].ToString() +"'>�鿴����</a>";
					}
					else
					{
						dtout.Rows[i]["�Ƿ��Ű�"]="��";
						dtout.Rows[i]["����"]="";
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
				dtout.Columns["vcCardID"].ColumnName="Ա������";
				dtout.Columns["vcEmpName"].ColumnName="Ա������";
				dtout.Columns["vcEmpOF"].ColumnName="Ա��ְ��";
				dtout.Columns["vcClass"].ColumnName="���";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["���"].ToString()!="")
					{
						dtout.Rows[i]["����"]="<a href='wfmEmpEverySchDel.aspx?date="+strShcID+"&emp=" + dtout.Rows[i]["Ա������"].ToString() + "&class="+ dtout.Rows[i]["���"].ToString() +"'>ɾ��</a>";
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
								deptstmp.strEmpNameGroup+="��"+dtSch.Rows[j]["vcEmpName"].ToString();
							}
						}
					}
				}

				foreach(CMSMStruct.DeptSchStruct deptstmp in alDeptSch)
				{						
					if(deptstmp.strEmpNameGroup==null||deptstmp.strEmpNameGroup=="")
					{
						deptstmp.strEmpNameGroup="δ����";
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
			dtout.Columns["sno"].ColumnName="���";
			dtout.Columns["type"].ColumnName="��������";
			dtout.Columns["TolEmpCount"].ColumnName="������";
			dtout.Columns["TolCount"].ColumnName="�ܴ���";
			dtout.Columns["EmpList"].ColumnName="����(��)";

			return dtout;
		}

		public DataTable GetSignDetailQuery(Hashtable htpara)
		{
			DataTable dtout=Ea.GetSignDetailQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcDept"].ColumnName="�ŵ�����";
				dtout.Columns["vcCardID"].ColumnName="Ա������";
				dtout.Columns["vcEmpName"].ColumnName="Ա������";
				dtout.Columns["vcSignDate"].ColumnName="��������";
				dtout.Columns["vcClass"].ColumnName="���";
				dtout.Columns["dtSignIn"].ColumnName="ǩ��ʱ��";
				dtout.Columns["dtSignOut"].ColumnName="ǩ��ʱ��";
				dtout.Columns["vcSignResult"].ColumnName="���ڽ��";
				dtout.Columns["vcComments"].ColumnName="��ע";
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
					dtout.Columns["vcDept"].ColumnName="�ŵ�����";
					dtout.Columns["vcCardID"].ColumnName="Ա������";
					dtout.Columns["vcEmpName"].ColumnName="Ա������";
					dtout.Columns["vcSignDate"].ColumnName="��������";
					dtout.Columns["vcClass"].ColumnName="���";
					dtout.Columns["vcOfficer"].ColumnName="ְ��";
					dtout.Columns["dtSignIn"].ColumnName="ǩ��ʱ��";
					dtout.Columns["dtSignOut"].ColumnName="ǩ��ʱ��";
					dtout.Columns["vcSignResult"].ColumnName="���ڽ��";
					dtout.Columns["vcComments"].ColumnName="��ע";
				}
				else
				{
					dtout.Columns["vcCommName"].ColumnName="�ŵ�����";
					dtout.Columns["vcCardID"].ColumnName="Ա������";
					dtout.Columns["vcEmpName"].ColumnName="Ա������";
					dtout.Columns["dtSignDate"].ColumnName="ˢ��ʱ��";
					dtout.Columns["vcSignFlag"].ColumnName="���ڱ�־";
					dtout.Columns["vcComments"].ColumnName="��ע";
				}
			}
			return dtout;
		}

		//���ڼ���
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
					strerr="�ڼ���ʱ����û���κο��ڼ�¼��";
					return false;
				}
				if(dtSchLog.Rows.Count==0)
				{
					strerr="�ڼ���ʱ����û���κ��Ű��¼��";
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

					#region ѭ��ÿ��ÿ��Ա�����Ű��
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

						#region ĳ��ĳԱ���Ű������д򿨼�¼
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

								#region ��ĳ��ĳԱ�������д򿨼�¼ѭ����ȷ�ϳ���Ч��һ�δ򿨼�¼����Ϊͬһ�������ο�
								foreach(CMSMStruct.SignAtomStruct sastmp in alCardSignLog)
								{
									switch(sastmp.strSignFlag)
									{
										case "1":
											#region ȷ��ǩ����Ч��¼����ʱ�����ϰ�ʱ��ǰ��1Сʱ��Ϊ��Ч���ϰ�ʱ��ǰ1Сʱ��Ϊ���ٵ��������ϰ�ʱ���Ϊ�ٵ�
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
											#region ȷ��ǩ����Ч��¼����ʱ��ֻҪ�ڵ����ڶ�����Ч���°�ʱ��ǰΪ���ˣ���Ϊ������
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
											#region ȷ�ϲ�����Ч��¼
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
											#region ȷ���¼���Ч��¼
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
						
							//����٣���ǩ������ǩ�ˣ���ٵ�����
							if(!SignQingJiaFlag&&!SignInFlag&&SignOutFlag)
							{
								sls.strSignState="1"+sls.strSignState.Substring(1,9);
							}
							//����٣���ǩ������ǩ�ˣ������˴���
							if(!SignQingJiaFlag&&SignInFlag&&!SignOutFlag)
							{
								sls.strSignState=sls.strSignState.Substring(0,1)+"1"+sls.strSignState.Substring(2,8);
							}
							//����٣���ǩ������ǩ�ˣ��ȴ�����������ǩ����ǩ��ʱ�����ޣ���������Ϊȱ�ڣ�����Ϊ��������
							if(!SignQingJiaFlag&&!SignInFlag&&!SignOutFlag)
							{
								sls.strSignState="0000000000";
							}
						}
						#endregion

						#region ����ɸѡȷ�ϣ�����״̬Ϊ��0000000000�����޴򿨼�¼���Ѿ��������ڣ�����Ϊ����������
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
											sls.strSignResult+="�ٵ�,";
											break;
										case 1:
											sls.strSignResult+="����,";
											break;
										case 2:
											sls.strSignResult+="����,";
											break;
										case 3:
											sls.strSignResult+="�¼�,";
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
								sls.strSignResult="ȱ��";
							}
							else
							{
								sls.strSignResult="��������";
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
