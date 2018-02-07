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
using System.IO;
using cc;

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmNoticeDetail.
	/// </summary>
	public class wfmNoticeDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtContent;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Button btcancel;
		protected System.Web.UI.WebControls.Label Label2;
	
		BusiComm.Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			if (!IsPostBack )
			{
				if(Request.QueryString.HasKeys())
				{
					string strid=Request.QueryString["id"].ToString();
					if(strid!=null&&strid!="")
					{
						Hashtable htapp=(Hashtable)Application["appconf"];
						string strcons=(string)htapp["cons"];
						m1=new Manager(strcons);

						string strActive=m1.getNoticeActiveFlag(strid);
						if(strActive!="0")
						{
							this.SetErrorMsgPageBydir("����֪ͨ�Ѿ����͹�������ϵͳ֪ͨ������������²�ѯˢ�£�");
							return;
						}
						else
						{
							DateTime dtCreateDate=DateTime.Now;
							string strYDate=dtCreateDate.Year.ToString();
							string strMDate=dtCreateDate.Month.ToString();
							string strDDate=dtCreateDate.Day.ToString();
							if(strMDate.Length<2)
							{
								strMDate="0"+strMDate;
							}
							if(strDDate.Length<2)
							{
								strDDate="0"+strDDate;
							}
							string strCreateDate=strYDate+strMDate+strDDate;
							int todaycount=int.Parse(m1.getNotiSerial(strCreateDate));
							if(!CenterToDeptNotice(todaycount,dtCreateDate,strCreateDate,strid))
							{
								this.SetErrorMsgPageBydir("����ϵͳ֪ͨ���������ԣ�");
								return;
							}
							else
							{
								m1.UpdateNotice(strid);
								this.SetSuccMsgPageBydir("����ϵͳ֪ͨ�ɹ���","paraconf/wfmNotice.aspx");
								return;
							}
						}
					}
				}

				this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","ȫ���ŵ�");
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
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region ����ϵͳ֪ͨ
		private bool CenterToDeptNotice(int lastserial,DateTime dtCreateDate,string strCreateDate,string strid)
		{
			int maxFileID=lastserial+1;
			string filePath=@"E:\AMSDataSoft\CenterToDept\";
			string downFileName="";
			if(maxFileID<10)
			{
				downFileName="CEN00" + strCreateDate + "0" + maxFileID.ToString() +".noti";
			}
			else
			{
				downFileName="CEN00" + strCreateDate + maxFileID.ToString() +".noti";
			}

			//��������
			if(!CreateNotice(filePath+downFileName,strid))
			{
				return false;
			}

			FileInfo file_size=new FileInfo(filePath+downFileName);
			long fsize=file_size.Length;

			DateTime dtFinDate=DateTime.Now;
			string strsqlset="'" + downFileName.Trim() + "'," + fsize.ToString() + ",1,'" + dtCreateDate.ToShortDateString() + " " + dtCreateDate.ToLongTimeString() + "','" + dtFinDate.ToShortDateString() + " " + dtFinDate.ToLongTimeString() + "','NOTI'";
			if(!m1.writeDataLog(strsqlset))
			{
				clog.WriteLine("д����������־ʧ�ܣ����ļ������ɣ�");
			}

			return true;
		}
		#endregion

		#region ����ϵͳ֪ͨ
		private bool CreateNotice(string strdownfiles,string strid)
		{	
			#region ����ϵͳ֪ͨ
			ArrayList alDownNoti=new ArrayList();
			alDownNoti=m1.DownNotice(strid);
			if(alDownNoti==null)
			{
				clog.WriteLine("����ϵͳ֪ͨ����");
				return false;
			}
			#endregion

			StreamWriter swFile = new StreamWriter(strdownfiles+".tmp",true);
			StructToString sts=new StructToString();

			#region дϵͳ֪ͨ
			CMSMStruct.NoticeStruct notitmp=null;
			swFile.WriteLine("NOTITOL=" + alDownNoti.Count.ToString());
			for(int i=0;i<alDownNoti.Count;i++)
			{
				notitmp=alDownNoti[i] as CMSMStruct.NoticeStruct;
				swFile.WriteLine(sts.ToNoticeString(notitmp));
			}
			swFile.WriteLine("END");
			#endregion

			swFile.Close();

			#region ����
			DESEncryptor dese=new DESEncryptor();
			dese.InputFilePath=strdownfiles+".tmp";
			dese.OutFilePath=strdownfiles;
			dese.EncryptKey="cmsmyykx";
			dese.FileDesEncrypt();
			if(dese.NoteMessage!=null)
			{
				clog.WriteLine(dese.NoteMessage);
				return false;
			}
			dese=null;
			#endregion

			if(System.IO.File.Exists(strdownfiles+".tmp"))
				System.IO.File.Delete(strdownfiles+".tmp");

			return true;
		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			string strDeptFlag=ddlDept.SelectedValue;
			if(strDeptFlag=="ȫ���ŵ�")
			{
				strDeptFlag="all";
			}
			string strContent=txtContent.Text.Trim();
			strContent=strContent.Replace("\r\n","");
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			if(!m1.InsertNotice(strDeptFlag,strContent))
			{
				this.SetErrorMsgPageBydir("���ϵͳ֪ͨ����");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("���ϵͳ֪ͨ�ɹ���","");
				return;
			}
		}

		private void btcancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmNotice.aspx");
		}
	}
}
