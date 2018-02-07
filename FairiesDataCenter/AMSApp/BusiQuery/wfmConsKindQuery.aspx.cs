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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// Summary description for wfmConsKindQuery.
	/// </summary>
	public class wfmConsKindQuery : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.DropDownList ddlAssType;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.CheckBox chbAssType;
		protected System.Web.UI.WebControls.CheckBox chbGoodsType;
		protected System.Web.UI.WebControls.DropDownList ddlGoodsType;
		protected System.Web.UI.WebControls.CheckBox chbDept;


		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtGoodsName;
		protected System.Web.UI.WebControls.Label lblSumCount;
		protected System.Web.UI.WebControls.Label lblSumFee;
		BusiComm.BusiQuery busiq;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlAssType, "vcCommSign ='AT'","全部");
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					this.FillDropDownList("tbCommCode", ddlGoodsType, "vcCommSign ='GT'","全部");
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
				if(this.UcPageView1.MyDataGrid.DataSource!=null)
				{
					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
					{
						UcPageView1.FootBar.Visible = true;
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
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
			this.chbAssType.CheckedChanged += new System.EventHandler(this.chbAssType_CheckedChanged);
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.chbGoodsType.CheckedChanged += new System.EventHandler(this.chbGoodsType_CheckedChanged);
			this.chbDept.CheckedChanged += new System.EventHandler(this.chbDept_CheckedChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void chbAssType_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chbAssType.Checked)
			{
				ddlAssType.Enabled=true;
			}
			else
			{
				ddlAssType.Enabled=false;
			}
		}

		private void chbGoodsType_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chbGoodsType.Checked)
			{
				ddlGoodsType.Enabled=true;
			}
			else
			{
				ddlGoodsType.Enabled=false;
			}
		}

		private void chbDept_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chbDept.Checked)
			{
				ddlDept.Enabled=true;
			}
			else
			{
				ddlDept.Enabled=false;
			}
		}

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);

			Hashtable htPara=new Hashtable();
			string strAssType=ddlAssType.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strGoodsType=ddlGoodsType.SelectedValue;
			string strGoodsName=this.txtGoodsName.Text.Trim();
			if(strAssType=="全部")
			{
				strAssType="";
			}
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			if(strGoodsType=="全部")
			{
				strGoodsType="";
			}
			htPara.Add("strAssType",strAssType);
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strGoodsType",strGoodsType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strGoodsName",strGoodsName);
			
			try
			{
				string querytype="0";
				DataTable dtout=new DataTable();
				if(chbAssType.Checked&&chbGoodsType.Checked&&chbDept.Checked)
				{
					querytype="1";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						btnExcel.Enabled=false;
						return;
					}
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign ='MD'");
					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign ='AT'");
//					this.TableConvert(dtout,"商品类型","PClass","vcCommSign ='FINALPRODUCT'");
				}
				else if(chbAssType.Checked&&!chbGoodsType.Checked&&!chbDept.Checked)
				{
					querytype="2";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
				}
				else if(!chbAssType.Checked&&chbGoodsType.Checked&&!chbDept.Checked)
				{
					querytype="3";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
//					this.TableConvert(dtout,"商品类型","PClass","vcCommSign ='FINALPRODUCT'");
				}
				else if(!chbAssType.Checked&&!chbGoodsType.Checked&&chbDept.Checked)
				{
					querytype="4";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign ='MD'");
				}
				else if(chbAssType.Checked&&chbGoodsType.Checked&&!chbDept.Checked)
				{
					querytype="5";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						btnExcel.Enabled=false;
						return;
					}
					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign ='AT'");
//					this.TableConvert(dtout,"商品类型","PClass","vcCommSign ='FINALPRODUCT'");
				}
				else if(chbAssType.Checked&&!chbGoodsType.Checked&&chbDept.Checked)
				{
					querytype="6";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						btnExcel.Enabled=false;
						return;
					}
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign ='MD'");
					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign ='AT'");
				}
				else if(!chbAssType.Checked&&chbGoodsType.Checked&&chbDept.Checked)
				{
					querytype="7";
					dtout=busiq.GetConsKindQuery(htPara,querytype);
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						btnExcel.Enabled=false;
						return;
					}
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign ='MD'");
//					this.TableConvert(dtout,"商品类型","PClass","vcCommSign ='FINALPRODUCT'");
				}
				if(querytype=="0")
				{
					this.SetErrorMsgPageBydir("请至少选择一个分类项！");
					btnExcel.Enabled=false;
					return;
				}

				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					dtout.TableName="消费分类统计";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					Session["toExcel"]=dtexcel;
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
						this.lblSumCount.Text="总数量：0";
						this.lblSumFee.Text="总金额：0元";
					}
					else
					{
						double sumcount=0;
						double sumfee=0;
						for(int j=0;j<dtout.Rows.Count;j++)
						{
							sumcount+=Math.Round(double.Parse(dtout.Rows[j]["数量合计"].ToString()),2);
							sumfee+=Math.Round(double.Parse(dtout.Rows[j]["金额合计"].ToString()),2);
						}
						this.lblSumCount.Text="总数量："+sumcount.ToString();
						this.lblSumFee.Text="总金额："+sumfee.ToString()+"元";
						btnExcel.Enabled=true;	
					}
				}

				UcPageView1.MyDataGrid.PageSize = 20;
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
	}
}
