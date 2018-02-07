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
//				this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//				return;
//			}
			if(!this.IsPostBack)
			{
				this.ddlDiscount.Items.Add("全部");
				this.ddlDiscount.Items.Add("是");
				this.ddlDiscount.Items.Add("否");
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
			if (this.ddlDiscount.SelectedValue=="全部")
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
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					dtout.TableName="商品资料清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
					}
					dtexcel.Columns.Remove("操作");
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
				this.SetErrorMsgPageBydir("查询错误，请重试！");
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
//				this.SetErrorMsgPageBydir("生成商品数据出错，请重试！");
//			}
//			else
//			{
//				this.SetSuccMsgPageBydir("生成商品数据成功！","");
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

		#region 中心到分店商品数据生成，只有在需要的时候才手动生成
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
//			//创建数据
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
//				clog.WriteLine("写数据生成日志失败，但文件已生成！");
//			}
//
//			return true;
//		}
		#endregion

		#region 创建商品信息
//		private bool CreateDataLocalPara(string strdownfiles)
//		{	
//			#region 加载商品资料
//			ArrayList alDownGoods=new ArrayList();
//			alDownGoods=m1.DownGoodsData();
//			if(alDownGoods==null)
//			{
//				clog.WriteLine("加载商品资料错误");
//				return false;
//			}
//			#endregion
//
//			StreamWriter swFile = new StreamWriter(strdownfiles+".tmp",true);
//			StructToString sts=new StructToString();
//
//			#region 写商品资料
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
//			#region 加密
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
