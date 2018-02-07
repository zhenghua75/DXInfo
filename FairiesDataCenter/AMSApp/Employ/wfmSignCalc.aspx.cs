using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusiComm;
using CommCenter;

namespace AMSApp.Employ
{
	/// <summary>
	/// Summary description for wfmSignCalc.
	/// </summary>
	public partial class wfmSignCalc : wfmBase
	{

		protected string strEndDate;
		protected string strBeginDate;
		BusiComm.EmpBusi empb;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001"&&ls1.strLimit!="CL002")
//				{
//					this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
					if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btCalc_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			string[] SchDatelistBegin=strBeginDate.Split('-');
			if(SchDatelistBegin.Length!=3)
			{
				this.SetErrorMsgPageBydir("ʱ���ʽ����ȷ��");
				return;
			}
			string strSchIDBegin=SchDatelistBegin[0];
			if(int.Parse(SchDatelistBegin[1])<10)
			{
				strSchIDBegin+="0" + SchDatelistBegin[1];
			}
			else
			{
				strSchIDBegin+=SchDatelistBegin[1];
			}
			if(int.Parse(SchDatelistBegin[2])<10)
			{
				strSchIDBegin+="0" + SchDatelistBegin[2];
			}
			else
			{
				strSchIDBegin+=SchDatelistBegin[2];
			}

			string[] SchDatelistEnd=strEndDate.Split('-');
			if(SchDatelistEnd.Length!=3)
			{
				this.SetErrorMsgPageBydir("ʱ���ʽ����ȷ��");
				return;
			}
			string strSchIDEnd=SchDatelistEnd[0];
			if(int.Parse(SchDatelistEnd[1])<10)
			{
				strSchIDEnd+="0" + SchDatelistEnd[1];
			}
			else
			{
				strSchIDEnd+=SchDatelistEnd[1];
			}
			if(int.Parse(SchDatelistEnd[2])<10)
			{
				strSchIDEnd+="0" + SchDatelistEnd[2];
			}
			else
			{
				strSchIDEnd+=SchDatelistEnd[2];
			}

			if(int.Parse(strSchIDBegin)>int.Parse(strSchIDEnd))
			{
				this.SetErrorMsgPageBydir("��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
				return;
			}

			string strDeptID=ddlDept.SelectedValue;
			string strDeptName=ddlDept.SelectedItem.Text;
			
			Hashtable htpara=new Hashtable();
			htpara.Add("strDeptID",strDeptID);
			htpara.Add("strDeptName",strDeptName);
			htpara.Add("strSchIDBegin",strSchIDBegin);
			htpara.Add("strSchIDEnd",strSchIDEnd);

			try
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				empb=new BusiComm.EmpBusi(strcons);
				string strerr="";
				if(empb.SignCalc(htpara,out strerr))
				{
					this.lblCalcResult.Text=strDeptName+" ���㿼�ڳɹ�������뿼�ڲ�ѯͳ���н��в�ѯ��";
					this.QueryRefresh(strDeptName,strSchIDBegin.Substring(0,6));
					return;
				}
				else
				{
					this.lblCalcResult.Text=strDeptName+" ���㿼��ʧ�ܣ�"+strerr;
					this.QueryRefresh(strDeptName,strSchIDBegin.Substring(0,6));
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("���㿼�ڳ���<br>"+er.ToString());
				return;
			}
		}

		protected void btQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			if(strBeginDate==""||strBeginDate==null)
			{
				this.SetErrorMsgPageBydir("��ʼʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			string[] SchDatelistBegin=strBeginDate.Split('-');
			if(SchDatelistBegin.Length!=3)
			{
				this.SetErrorMsgPageBydir("ʱ���ʽ����ȷ��");
				return;
			}
			string strSchIDBegin=SchDatelistBegin[0];
			if(int.Parse(SchDatelistBegin[1])<10)
			{
				strSchIDBegin+="0" + SchDatelistBegin[1];
			}
			else
			{
				strSchIDBegin+=SchDatelistBegin[1];
			}

			this.QueryRefresh(ddlDept.SelectedItem.Text,strSchIDBegin);
		}

		private void QueryRefresh(string strDept,string strDate)
		{
			int daycount=DateTime.DaysInMonth(int.Parse(strDate.Substring(0,4)),int.Parse(strDate.Substring(4,2)));
			for(int i=1;i<=31;i++)
			{
				switch(i)
				{
					#region ��ʼ������������ɫΪ��ɫ
					case 1:
						lbl1.ForeColor=Color.Red;
						break;
					case 2:
						lbl2.ForeColor=Color.Red;
						break;
					case 3:
						lbl3.ForeColor=Color.Red;
						break;
					case 4:
						lbl4.ForeColor=Color.Red;
						break;
					case 5:
						lbl5.ForeColor=Color.Red;
						break;
					case 6:
						lbl6.ForeColor=Color.Red;
						break;
					case 7:
						lbl7.ForeColor=Color.Red;
						break;
					case 8:
						lbl8.ForeColor=Color.Red;
						break;
					case 9:
						lbl9.ForeColor=Color.Red;
						break;
					case 10:
						lbl10.ForeColor=Color.Red;
						break;
					case 11:
						lbl11.ForeColor=Color.Red;
						break;
					case 12:
						lbl12.ForeColor=Color.Red;
						break;
					case 13:
						lbl13.ForeColor=Color.Red;
						break;
					case 14:
						lbl14.ForeColor=Color.Red;
						break;
					case 15:
						lbl15.ForeColor=Color.Red;
						break;
					case 16:
						lbl16.ForeColor=Color.Red;
						break;
					case 17:
						lbl17.ForeColor=Color.Red;
						break;
					case 18:
						lbl18.ForeColor=Color.Red;
						break;
					case 19:
						lbl19.ForeColor=Color.Red;
						break;
					case 20:
						lbl20.ForeColor=Color.Red;
						break;
					case 21:
						lbl21.ForeColor=Color.Red;
						break;
					case 22:
						lbl22.ForeColor=Color.Red;
						break;
					case 23:
						lbl23.ForeColor=Color.Red;
						break;
					case 24:
						lbl24.ForeColor=Color.Red;
						break;
					case 25:
						lbl25.ForeColor=Color.Red;
						break;
					case 26:
						lbl26.ForeColor=Color.Red;
						break;
					case 27:
						lbl27.ForeColor=Color.Red;
						break;
					case 28:
						lbl28.ForeColor=Color.Red;
						break;
					case 29:
						lbl29.ForeColor=Color.Red;
						break;
					case 30:
						lbl30.ForeColor=Color.Red;
						break;
					case 31:
						lbl31.ForeColor=Color.Red;
						break;
						#endregion
				}							
			}

			for(int j=31;j>daycount;j--)
			{
				switch(j)
				{
					case 31:
						lbl31.Visible=false;
						break;
					case 30:
						lbl30.Visible=false;
						break;
					case 29:
						lbl29.Visible=false;
						break;
				}
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			empb=new BusiComm.EmpBusi(strcons);
			ArrayList alDay=empb.GetSignCalcResultQuery(strDept,strDate);
			this.lblQueryTitle.Text=strDept+" "+strDate.Substring(0,4)+"��"+strDate.Substring(4,2)+"�µĿ��ڼ���������£�";
			if(alDay.Count>0)
			{
				for(int i=0;i<alDay.Count;i++)
				{
					switch(int.Parse(alDay[i].ToString()))
					{
						#region ����������������ɫ
						case 1:
							lbl1.ForeColor=Color.Blue;
							break;
						case 2:
							lbl2.ForeColor=Color.Blue;
							break;
						case 3:
							lbl3.ForeColor=Color.Blue;
							break;
						case 4:
							lbl4.ForeColor=Color.Blue;
							break;
						case 5:
							lbl5.ForeColor=Color.Blue;
							break;
						case 6:
							lbl6.ForeColor=Color.Blue;
							break;
						case 7:
							lbl7.ForeColor=Color.Blue;
							break;
						case 8:
							lbl8.ForeColor=Color.Blue;
							break;
						case 9:
							lbl9.ForeColor=Color.Blue;
							break;
						case 10:
							lbl10.ForeColor=Color.Blue;
							break;
						case 11:
							lbl11.ForeColor=Color.Blue;
							break;
						case 12:
							lbl12.ForeColor=Color.Blue;
							break;
						case 13:
							lbl13.ForeColor=Color.Blue;
							break;
						case 14:
							lbl14.ForeColor=Color.Blue;
							break;
						case 15:
							lbl15.ForeColor=Color.Blue;
							break;
						case 16:
							lbl16.ForeColor=Color.Blue;
							break;
						case 17:
							lbl17.ForeColor=Color.Blue;
							break;
						case 18:
							lbl18.ForeColor=Color.Blue;
							break;
						case 19:
							lbl19.ForeColor=Color.Blue;
							break;
						case 20:
							lbl20.ForeColor=Color.Blue;
							break;
						case 21:
							lbl21.ForeColor=Color.Blue;
							break;
						case 22:
							lbl22.ForeColor=Color.Blue;
							break;
						case 23:
							lbl23.ForeColor=Color.Blue;
							break;
						case 24:
							lbl24.ForeColor=Color.Blue;
							break;
						case 25:
							lbl25.ForeColor=Color.Blue;
							break;
						case 26:
							lbl26.ForeColor=Color.Blue;
							break;
						case 27:
							lbl27.ForeColor=Color.Blue;
							break;
						case 28:
							lbl28.ForeColor=Color.Blue;
							break;
						case 29:
							lbl29.ForeColor=Color.Blue;
							break;
						case 30:
							lbl30.ForeColor=Color.Blue;
							break;
						case 31:
							lbl31.ForeColor=Color.Blue;
							break;
							#endregion
					}							
				}
			}
		}
	}
}
