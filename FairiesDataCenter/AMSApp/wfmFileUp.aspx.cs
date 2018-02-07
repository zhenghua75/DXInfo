using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CommCenter;
using BusiComm;

namespace AMSApp
{
	/// <summary>
	/// wfmFileUp 的摘要说明。
	/// </summary>
	public class wfmFileUp : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnReadFile;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnWriteFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileUpLoad;
		protected System.Web.UI.WebControls.Label lblFileName;
		protected System.Web.UI.WebControls.TextBox txtXlsType;
		protected System.Web.UI.WebControls.TextBox txtSheet;
		protected System.Web.UI.WebControls.Label lblFileUp;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.TextBox txtError;

		EmpBusi emb;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				if(Request["XlsType"] == null)
				{
					Popup("无效链接");
					return;
				}
				this.txtXlsType.Text = Request["XlsType"].ToString();
				switch(this.txtXlsType.Text)
				{
					case "EmpSch":
						txtSheet.Text = "员工日排班表$";
						lblFileUp.Text = "员工日排班表文件导入";
						break;
				}
				this.txtError.Text = "0";				
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
			this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.ItemDataBound+=new DataGridItemEventHandler(DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReadFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(txtSheet.Text == "")
					throw new Exception("无法识别的文件");
				DataSet ds = ReadFile(txtSheet.Text);
				//校验数据
				CheckFile(ds);
				Session["UpLoadFile"] = ds.Tables[0];
				this.DataGrid1.DataSource = ds;
				this.DataGrid1.DataBind();
				this.lblFileName.Text += "<br>错误数据数量："+txtError.Text;
				if(Convert.ToInt32(txtError.Text)>0)
					this.lblFileName.Text += "<br>请查看\"校验错误\"列";
			}
			catch(Exception ex)
			{
				txtError.Text = "1";
				Popup(ex.Message);
			}
		}

		private DataSet ReadFile(string strSheet)
		{
				
			string str = fileUpLoad.PostedFile.FileName;
			int i = str.LastIndexOf("\\");
			string filename=str.Substring(i+1);
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			string strFileName = ls1.strOperName+"_"+DateTime.Now.ToString("yyyyMMdd")+"_"+System.Guid.NewGuid().ToString();
			string strFileUpPath = System.Configuration.ConfigurationManager.AppSettings["FileUppath"];
			fileUpLoad.PostedFile.SaveAs(@strFileUpPath + strFileName);

			lblFileName.Text = "文件名为：" + filename;
			string strConn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source =" + @strFileUpPath + strFileName + ";Extended Properties=Excel 8.0";
			OleDbConnection conn = new OleDbConnection(strConn);
			conn.Open();
			string Sql = "select * from ["+strSheet+"]";
			OleDbDataAdapter cmd = new OleDbDataAdapter(Sql, conn);
			DataSet ds = new DataSet();
			cmd.Fill(ds);
			conn.Close();
			System.IO.File.Delete(@strFileUpPath + strFileName);
			return ds;
		}

		private DataSet CheckFile(DataSet ds)
		{
			DataSet dsOut = null;
			switch(this.txtXlsType.Text)
			{
				case "EmpSch":
					dsOut = CheckEmpSch(ds);
					break;
			}
			return dsOut;
		}

		private DataSet CheckEmpSch(DataSet ds)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			emb=new EmpBusi(strcons);
			DataTable dtEmp = emb.GetEmployInfo();
			Hashtable htEmpCardID = new Hashtable();
			foreach(DataRow dr in dtEmp.Rows)
			{
				CMSMStruct.EmployeeStruct emp1=new CommCenter.CMSMStruct.EmployeeStruct();
				emp1.strCardID=dr["vcCardID"].ToString();
				emp1.strEmpName=dr["vcEmpName"].ToString();
				emp1.strOfficer=dr["vcOfficer"].ToString();
				emp1.strDeptID=dr["vcDeptName"].ToString();
				htEmpCardID.Add(emp1.strCardID,emp1);
			}
			
			DataTable dtDeptManager = emb.GetDeptManagerList();
			Hashtable htManager = new Hashtable();
			string strManaDeptID="";
			string strManager="";
			if(dtDeptManager!=null&&dtDeptManager.Rows.Count>0)
			{
				strManaDeptID=dtDeptManager.Rows[0]["vcDeptID"].ToString();
			}
			for(int i=0;i<dtDeptManager.Rows.Count;i++)
			{
				if(strManaDeptID==dtDeptManager.Rows[i]["vcDeptID"].ToString())
				{
					strManager+=dtDeptManager.Rows[i]["vcEmpName"].ToString()+",";
				}
				else
				{
					if(strManager!="")
					{
						strManager=strManager.Substring(0,strManager.Length-1);
					}
					htManager.Add(strManaDeptID,strManager);
					strManaDeptID=dtDeptManager.Rows[i]["vcDeptID"].ToString();
					strManager=dtDeptManager.Rows[i]["vcEmpName"].ToString()+",";
				}
				if(i==dtDeptManager.Rows.Count-1)
				{
					if(strManager!="")
					{
						strManager=strManager.Substring(0,strManager.Length-1);
					}
					htManager.Add(strManaDeptID,strManager);
				}
			}

			int iError = 0;
			DataTable dt = ds.Tables[0];
			dt.Columns.Add("校验错误");
			dt.Columns.Add("vcSchID");
			dt.Columns.Add("vcDeptName");
			dt.Columns.Add("vcManager");
			dt.Columns.Add("vcCardID");
			dt.Columns.Add("vcEmpName");
			dt.Columns.Add("vcEmpOF");
			dt.Columns.Add("vcClass");
			dt.Columns.Add("dtCheckIn");
			dt.Columns.Add("dtCheckOut");
			foreach(DataRow dr in dt.Rows)
			{
				string strSchID = dr["排班日期"].ToString().Trim();
				if(strSchID.Length!=8)
				{
					dr["校验错误"] += strSchID+"：排班日期应为8位日期；";
					iError += 1;
				}
				else
				{
					DateTime dtsch=new DateTime(int.Parse(strSchID.Substring(0,4)),int.Parse(strSchID.Substring(4,2)),int.Parse(strSchID.Substring(6,2)));
					if(dtsch.CompareTo(DateTime.Today)!=0)
					{
						dr["校验错误"] += strSchID+"：排班日期必须是当天日期；";
						iError += 1;
					}
					else
					{
						dr["vcSchID"] = strSchID;
					}
				}

				string strDeptName = dr["门店"].ToString().Trim();
				string strDeptOldID="";
				bool isdeptexist=false;
				DataTable dtDept=(DataTable)Application["AllMD"];
				foreach(DataRow dr1 in dtDept.Rows)
				{
					if(dr1["vcCommName"].ToString()==strDeptName)
					{
						isdeptexist=true;
						strDeptOldID=dr1["vcCommCode"].ToString();
						break;
					}
				}
				if(!isdeptexist)
				{
					dr["校验错误"] += strDeptName+"：门店不存在；";
					iError += 1;
				}
				else
				{
					dr["vcDeptName"] = strDeptName;
				}

				if(dr["vcDeptName"].ToString()!=""&&strDeptOldID!="")
				{
					if(htManager.ContainsKey(strDeptOldID))
					{
						dr["vcManager"]=htManager[strDeptOldID].ToString();
					}
				}

				string strCardID = dr["员工卡号"].ToString();
				string strEmpName = dr["员工姓名"].ToString();
				string strOfficer = dr["职务"].ToString();
				if(!htEmpCardID.ContainsKey(strCardID))
				{
					dr["校验错误"] += strCardID+"：卡号不正确，员工不存在；";
					iError += 1;
				}
				else
				{
					dr["vcCardID"] = strCardID;
					CMSMStruct.EmployeeStruct emptmp2=(CMSMStruct.EmployeeStruct)htEmpCardID[strCardID];
					if(emptmp2.strEmpName!=strEmpName)
					{
						dr["校验错误"] += strEmpName+"：员工姓名不正确；";
						iError += 1;
					}
					else
					{
						dr["vcEmpName"] = strEmpName;
					}
					if(emptmp2.strOfficer!=strOfficer)
					{
						dr["校验错误"] += strOfficer+"：职务不正确；";
						iError += 1;
					}
					else
					{
						dr["vcEmpOF"] = strOfficer;
					}
					if(emptmp2.strDeptID!=strDeptName)
					{
						dr["校验错误"] += strDeptName+"：员工所属部门不正确；";
						iError += 1;
					}
				}
				
				string strClass =dr["班次"].ToString();
				if(emb.IsEmpSchExist(strSchID,strDeptName,strCardID,strClass))
				{
					dr["校验错误"] += "系统中已有该员工的本条排班信息；";
					iError += 1;
				}
				else
				{
					dr["vcClass"] = strClass;
				}

				string InHour= dr["上班-小时"].ToString().Trim();
				string InMin= dr["上班-分钟"].ToString().Trim();
				string OutHour= dr["下班-小时"].ToString().Trim();
				string OutMin= dr["下班-分钟"].ToString().Trim();
				string nextoutflag=dr["次日下班"].ToString().Trim();
				if(!(this.JudgeIsNum(InHour)&&this.JudgeIsNum(InMin)&&this.JudgeIsNum(OutHour)&&this.JudgeIsNum(OutMin)))
				{
					dr["校验错误"] += "上下班的小时数或分钟数中有非数字字符；";
					iError += 1;
				}
				else
				{
					if(!(this.JudgeIs24Hour(InHour)&&this.JudgeIs60Minute(InMin)&&this.JudgeIs24Hour(OutHour)&&this.JudgeIs60Minute(OutMin)))
					{
						dr["校验错误"] += "上下班的小时数或分钟数有误；";
						iError += 1;
					}
					else
					{
						if(dr["vcSchID"].ToString()!="")
						{
							DateTime dtIn=new DateTime(int.Parse(dr["vcSchID"].ToString().Substring(0,4)),int.Parse(dr["vcSchID"].ToString().Substring(4,2)),int.Parse(dr["vcSchID"].ToString().Substring(6,2)),int.Parse(InHour),int.Parse(InMin),0);
							DateTime dtOut=DateTime.Now;
							if(nextoutflag=="是")
							{
								dtOut=new DateTime(dtIn.AddDays(1).Year,dtIn.AddDays(1).Month,dtIn.AddDays(1).Day,int.Parse(OutHour),int.Parse(OutMin),0);
							}
							else
							{
								dtOut=new DateTime(int.Parse(dr["vcSchID"].ToString().Substring(0,4)),int.Parse(dr["vcSchID"].ToString().Substring(4,2)),int.Parse(dr["vcSchID"].ToString().Substring(6,2)),int.Parse(OutHour),int.Parse(OutMin),0);
							}
							if(dtIn.CompareTo(dtOut)>0)
							{
								dr["校验错误"] += "上班时间不能大于下班时间；";
								iError += 1;
							}
							TimeSpan tsIn=new TimeSpan(dtIn.Ticks);
							TimeSpan tsOut=new TimeSpan(dtOut.Ticks);
							TimeSpan ts=tsIn.Subtract(tsOut).Duration();
							double diffdate=Math.Round(double.Parse(ts.Hours.ToString())+double.Parse(ts.Minutes.ToString())/60,2);
							if(diffdate<8.5)
							{
								dr["校验错误"] += "每班次的上班时间应大于8小时30分钟；";
								iError += 1;
							}

							dr["dtCheckIn"]=dtIn;
							dr["dtCheckOut"]=dtOut;
						}
					}
				}
			}
			this.txtError.Text = iError.ToString();
			return ds;
		}

		private void AddEmpSch(DataTable dt)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			emb=new EmpBusi(strcons);
			emb.SchedEmpDailyBatch(dt);
		}

		private void btnWriteFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Session["UpLoadFile"] == null)
				{
					throw new Exception("请首先读取文件");
				}
				DataTable dt = (DataTable)Session["UpLoadFile"];
				if(txtError.Text != "")
				{
					int iError = Convert.ToInt32(txtError.Text);
					if(iError > 0)
						throw new Exception("请首先规范数据");
				}
				switch(this.txtXlsType.Text)
				{
					case "EmpSch":
						AddEmpSch(dt);
						Popup("员工日排班表导入成功");
						break;
				}
				Session["UpLoadFile"] = null;
				this.lblFileName.Text = "";
				this.txtError.Text = "0";
				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
				
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			switch(this.txtXlsType.Text)
			{
				case "EmpSch":
					this.Response.Redirect("Employ/wfmWorkDailyEvery.aspx");
					break;
			}
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(this.txtXlsType.Text=="CustInfo")
			{
				for(int i=31;i<61;i++)
				{
					e.Item.Cells[i].Visible=false;
				}
			}
		}
	}
}
