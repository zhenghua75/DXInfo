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
	/// Summary description for wfmDeptShcDetail.
	/// </summary>
	public partial class wfmDeptShcDetail : wfmBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string strDept=Request.QueryString["dept"].ToString();
			string strDate=Request.QueryString["date"].ToString();
			string[] SchDatelist=strDate.Split('-');
			if(SchDatelist.Length!=3)
			{
				this.SetErrorMsgPageBydir("ʱ���ʽ����ȷ��");
				return;
			}
			strDate=SchDatelist[0]+SchDatelist[1]+SchDatelist[2];
			string strDateFormat=SchDatelist[0]+"��"+SchDatelist[1]+"��"+SchDatelist[2]+"��";
			if(strDept==""||strDate=="")
			{
				this.SetErrorMsgPageBydir("��ѯ����");
				return;
			}
			else
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				EmpBusi ebu=new EmpBusi(strcons);
				ArrayList alDeptSchAll=ebu.GetDeptSchDetail(strDept,strDate);
				if(alDeptSchAll==null||alDeptSchAll.Count<=0)
				{
					this.SetErrorMsgPageBydir("��ѯ����");
					return;
				}
				else
				{
					Response.Write("<table align='center' border='0' width='90%' cellSpacing='10' cellPadding='10'><tr><td style='FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033' align='center'>�Ű���ϸ��</td></tr></table>");
					Response.Write("<table align='center' border='0' width='90%' cellSpacing='5' cellPadding='5'><tr><td>�ŵ꣺"+strDept+"</td><td>���ڣ�"+strDateFormat+"</td></tr></table>");
					Response.Write("<table align='center' border='1' width='90%' cellSpacing='1' cellPadding='1' bordercolor='#ffccff' style='FONT-SIZE: 10pt'><tr style='COLOR: #0033ff'><td>ְ������</td><td>���</td><td>��Ա�б�</td><td>�ϰ�ʱ��</td><td>�°�ʱ��</td></tr>");
					foreach(CMSMStruct.DeptSchStruct deptstmp in alDeptSchAll)
					{
						if(deptstmp.strEmpNameGroup=="δ����")
						{
							Response.Write("<tr><td>"+deptstmp.strEmpOF+"</td><td>"+deptstmp.strClass+"</td><td style='COLOR: #cc0000'>"+deptstmp.strEmpNameGroup+"</td><td>"+deptstmp.strCheckIn+"</td><td>"+deptstmp.strCheckOut+"</td><td></tr>");
						}
						else
						{
							Response.Write("<tr><td>"+deptstmp.strEmpOF+"</td><td>"+deptstmp.strClass+"</td><td>"+deptstmp.strEmpNameGroup+"</td><td>"+deptstmp.strCheckIn+"</td><td>"+deptstmp.strCheckOut+"</td><td></tr>");
						}
					}
					Response.Write("</table>");
				}
				
				return;
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
	}
}
