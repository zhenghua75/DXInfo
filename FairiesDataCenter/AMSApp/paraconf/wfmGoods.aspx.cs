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
using System.Collections.Specialized;
using System.IO;
using cc;

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmGoods.
	/// </summary>
	public partial class wfmGoods : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;

		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			
//			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//			if(ls1.strLimit=="CL003"||ls1.strLimit=="CL002")
//			{
//				this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
//				return;
//			}
			if(!this.IsPostBack)
			{
				this.ddlDiscount.Items.Add("ȫ��");
				this.ddlDiscount.Items.Add("��");
				this.ddlDiscount.Items.Add("��");
//				this.Button3.Enabled=false;
				Session.Remove("QUERY");
				Session.Remove("toExcel");
				Session.Remove("page_view");
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			string strGoodsID=txtGoodsID.Text.Trim();
			string strGoodsName=txtGoodsName.Text.Trim();
			string strComments="";
			if (this.ddlDiscount.SelectedValue=="ȫ��")
			{
				strComments="";
			}
			else
			{
				strComments=this.ddlDiscount.SelectedValue;
			}

			try
			{
				DataTable dtout=m1.GetGoods(strGoodsID,strGoodsName,strComments);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					dtout.TableName="��Ʒ�����嵥";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
					}
					dtexcel.Columns.Remove("����");
					Session["toExcel"]=dtexcel;

					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						btnExcel.Enabled=true;	
					}
				}
				UcPageView1.MyDataGrid.PageSize = 30;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmGoodsDetail.aspx");
		}

//		private void Button3_Click(object sender, System.EventArgs e)
//		{
//			DateTime dtCreateDate=DateTime.Now;
//			string strYDate=dtCreateDate.Year.ToString();
//			string strMDate=dtCreateDate.Month.ToString();
//			string strDDate=dtCreateDate.Day.ToString();
//			if(strMDate.Length<2)
//			{
//				strMDate="0"+strMDate;
//			}
//			if(strDDate.Length<2)
//			{
//				strDDate="0"+strDDate;
//			}
//			string strCreateDate=strYDate+strMDate+strDDate;
//			Hashtable htapp=(Hashtable)Application["appconf"];
//			string strcons=(string)htapp["cons"];
//			m1=new Manager(strcons);
//			int todaycount=int.Parse(m1.getGoodsSerial(strCreateDate));
//			if(!CenterToDeptPara(todaycount,dtCreateDate,strCreateDate))
//			{
//				this.SetErrorMsgPageBydir("������Ʒ���ݳ��������ԣ�");
//			}
//			else
//			{
//				this.SetSuccMsgPageBydir("������Ʒ���ݳɹ���","");
//			}
//		}

//		private void chkCreate_CheckedChanged(object sender, System.EventArgs e)
//		{
//			if(chkCreate.Checked)
//			{
//				Button3.Enabled=true;
//			}
//			else
//			{
//				Button3.Enabled=false;
//			}
//		}

		#region ���ĵ��ֵ���Ʒ�������ɣ�ֻ������Ҫ��ʱ����ֶ�����
//		private bool CenterToDeptPara(int lastserial,DateTime dtCreateDate,string strCreateDate)
//		{
//			int maxFileID=lastserial+1;
//			string filePath=@"E:\AMSDataSoft\CenterToDept\";
//			string downFileName="";
//			if(maxFileID<10)
//			{
//				downFileName="CEN00" + strCreateDate + "0" + maxFileID.ToString() +".goods";
//			}
//			else
//			{
//				downFileName="CEN00" + strCreateDate + maxFileID.ToString() +".goods";
//			}
//
//			//��������
//			if(!CreateDataLocalPara(filePath+downFileName))
//			{
//				return false;
//			}
//
//			FileInfo file_size=new FileInfo(filePath+downFileName);
//			long fsize=file_size.Length;
//
//			DateTime dtFinDate=DateTime.Now;
//			string strsqlset="'" + downFileName.Trim() + "'," + fsize.ToString() + ",1,'" + dtCreateDate.ToShortDateString() + " " + dtCreateDate.ToLongTimeString() + "','" + dtFinDate.ToShortDateString() + " " + dtFinDate.ToLongTimeString() + "','GOODS'";
//			if(!m1.writeDataLog(strsqlset))
//			{
//				clog.WriteLine("д����������־ʧ�ܣ����ļ������ɣ�");
//			}
//
//			return true;
//		}
		#endregion

		#region ������Ʒ��Ϣ
//		private bool CreateDataLocalPara(string strdownfiles)
//		{	
//			#region ������Ʒ����
//			ArrayList alDownGoods=new ArrayList();
//			alDownGoods=m1.DownGoodsData();
//			if(alDownGoods==null)
//			{
//				clog.WriteLine("������Ʒ���ϴ���");
//				return false;
//			}
//			#endregion
//
//			StreamWriter swFile = new StreamWriter(strdownfiles+".tmp",true);
//			StructToString sts=new StructToString();
//
//			#region д��Ʒ����
//			CMSMStruct.GoodsStruct goodstmp=null;
//			swFile.WriteLine("GOODTOL=" + alDownGoods.Count.ToString());
//			for(int i=0;i<alDownGoods.Count;i++)
//			{
//				goodstmp=alDownGoods[i] as CMSMStruct.GoodsStruct;
//				swFile.WriteLine(sts.ToGoodsString(goodstmp));
//			}
//			swFile.WriteLine("END");
//			#endregion
//
//			swFile.Close();
//
//			#region ����
//			DESEncryptor dese=new DESEncryptor();
//			dese.InputFilePath=strdownfiles+".tmp";
//			dese.OutFilePath=strdownfiles;
//			dese.EncryptKey="cmsmyykx";
//			dese.FileDesEncrypt();
//			if(dese.NoteMessage!=null)
//			{
//				clog.WriteLine(dese.NoteMessage);
//				return false;
//			}
//			dese=null;
//			#endregion
//
//			if(System.IO.File.Exists(strdownfiles+".tmp"))
//				System.IO.File.Delete(strdownfiles+".tmp");
//
//			return true;
//		}
		#endregion

	}
}
