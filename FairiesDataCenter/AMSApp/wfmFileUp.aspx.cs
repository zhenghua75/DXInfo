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
	/// wfmFileUp ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				if(Request["XlsType"] == null)
				{
					Popup("��Ч����");
					return;
				}
				this.txtXlsType.Text = Request["XlsType"].ToString();
				switch(this.txtXlsType.Text)
				{
					case "EmpSch":
						txtSheet.Text = "Ա�����Ű��$";
						lblFileUp.Text = "Ա�����Ű���ļ�����";
						break;
				}
				this.txtError.Text = "0";				
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
					throw new Exception("�޷�ʶ����ļ�");
				DataSet ds = ReadFile(txtSheet.Text);
				//У������
				CheckFile(ds);
				Session["UpLoadFile"] = ds.Tables[0];
				this.DataGrid1.DataSource = ds;
				this.DataGrid1.DataBind();
				this.lblFileName.Text += "<br>��������������"+txtError.Text;
				if(Convert.ToInt32(txtError.Text)>0)
					this.lblFileName.Text += "<br>��鿴\"У�����\"��";
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

			lblFileName.Text = "�ļ���Ϊ��" + filename;
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
			dt.Columns.Add("У�����");
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
				string strSchID = dr["�Ű�����"].ToString().Trim();
				if(strSchID.Length!=8)
				{
					dr["У�����"] += strSchID+"���Ű�����ӦΪ8λ���ڣ�";
					iError += 1;
				}
				else
				{
					DateTime dtsch=new DateTime(int.Parse(strSchID.Substring(0,4)),int.Parse(strSchID.Substring(4,2)),int.Parse(strSchID.Substring(6,2)));
					if(dtsch.CompareTo(DateTime.Today)!=0)
					{
						dr["У�����"] += strSchID+"���Ű����ڱ����ǵ������ڣ�";
						iError += 1;
					}
					else
					{
						dr["vcSchID"] = strSchID;
					}
				}

				string strDeptName = dr["�ŵ�"].ToString().Trim();
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
					dr["У�����"] += strDeptName+"���ŵ겻���ڣ�";
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

				string strCardID = dr["Ա������"].ToString();
				string strEmpName = dr["Ա������"].ToString();
				string strOfficer = dr["ְ��"].ToString();
				if(!htEmpCardID.ContainsKey(strCardID))
				{
					dr["У�����"] += strCardID+"�����Ų���ȷ��Ա�������ڣ�";
					iError += 1;
				}
				else
				{
					dr["vcCardID"] = strCardID;
					CMSMStruct.EmployeeStruct emptmp2=(CMSMStruct.EmployeeStruct)htEmpCardID[strCardID];
					if(emptmp2.strEmpName!=strEmpName)
					{
						dr["У�����"] += strEmpName+"��Ա����������ȷ��";
						iError += 1;
					}
					else
					{
						dr["vcEmpName"] = strEmpName;
					}
					if(emptmp2.strOfficer!=strOfficer)
					{
						dr["У�����"] += strOfficer+"��ְ����ȷ��";
						iError += 1;
					}
					else
					{
						dr["vcEmpOF"] = strOfficer;
					}
					if(emptmp2.strDeptID!=strDeptName)
					{
						dr["У�����"] += strDeptName+"��Ա���������Ų���ȷ��";
						iError += 1;
					}
				}
				
				string strClass =dr["���"].ToString();
				if(emb.IsEmpSchExist(strSchID,strDeptName,strCardID,strClass))
				{
					dr["У�����"] += "ϵͳ�����и�Ա���ı����Ű���Ϣ��";
					iError += 1;
				}
				else
				{
					dr["vcClass"] = strClass;
				}

				string InHour= dr["�ϰ�-Сʱ"].ToString().Trim();
				string InMin= dr["�ϰ�-����"].ToString().Trim();
				string OutHour= dr["�°�-Сʱ"].ToString().Trim();
				string OutMin= dr["�°�-����"].ToString().Trim();
				string nextoutflag=dr["�����°�"].ToString().Trim();
				if(!(this.JudgeIsNum(InHour)&&this.JudgeIsNum(InMin)&&this.JudgeIsNum(OutHour)&&this.JudgeIsNum(OutMin)))
				{
					dr["У�����"] += "���°��Сʱ������������з������ַ���";
					iError += 1;
				}
				else
				{
					if(!(this.JudgeIs24Hour(InHour)&&this.JudgeIs60Minute(InMin)&&this.JudgeIs24Hour(OutHour)&&this.JudgeIs60Minute(OutMin)))
					{
						dr["У�����"] += "���°��Сʱ�������������";
						iError += 1;
					}
					else
					{
						if(dr["vcSchID"].ToString()!="")
						{
							DateTime dtIn=new DateTime(int.Parse(dr["vcSchID"].ToString().Substring(0,4)),int.Parse(dr["vcSchID"].ToString().Substring(4,2)),int.Parse(dr["vcSchID"].ToString().Substring(6,2)),int.Parse(InHour),int.Parse(InMin),0);
							DateTime dtOut=DateTime.Now;
							if(nextoutflag=="��")
							{
								dtOut=new DateTime(dtIn.AddDays(1).Year,dtIn.AddDays(1).Month,dtIn.AddDays(1).Day,int.Parse(OutHour),int.Parse(OutMin),0);
							}
							else
							{
								dtOut=new DateTime(int.Parse(dr["vcSchID"].ToString().Substring(0,4)),int.Parse(dr["vcSchID"].ToString().Substring(4,2)),int.Parse(dr["vcSchID"].ToString().Substring(6,2)),int.Parse(OutHour),int.Parse(OutMin),0);
							}
							if(dtIn.CompareTo(dtOut)>0)
							{
								dr["У�����"] += "�ϰ�ʱ�䲻�ܴ����°�ʱ�䣻";
								iError += 1;
							}
							TimeSpan tsIn=new TimeSpan(dtIn.Ticks);
							TimeSpan tsOut=new TimeSpan(dtOut.Ticks);
							TimeSpan ts=tsIn.Subtract(tsOut).Duration();
							double diffdate=Math.Round(double.Parse(ts.Hours.ToString())+double.Parse(ts.Minutes.ToString())/60,2);
							if(diffdate<8.5)
							{
								dr["У�����"] += "ÿ��ε��ϰ�ʱ��Ӧ����8Сʱ30���ӣ�";
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
					throw new Exception("�����ȶ�ȡ�ļ�");
				}
				DataTable dt = (DataTable)Session["UpLoadFile"];
				if(txtError.Text != "")
				{
					int iError = Convert.ToInt32(txtError.Text);
					if(iError > 0)
						throw new Exception("�����ȹ淶����");
				}
				switch(this.txtXlsType.Text)
				{
					case "EmpSch":
						AddEmpSch(dt);
						Popup("Ա�����Ű����ɹ�");
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
