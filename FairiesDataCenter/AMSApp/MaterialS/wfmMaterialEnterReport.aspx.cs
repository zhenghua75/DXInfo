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
using CommCenter;
using BusiComm;

namespace AMSApp.MaterialS
{
	/// <summary>
	/// wfmMaterialEnterReport 的摘要说明。
	/// </summary>
	public class wfmMaterialEnterReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlQueryType;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtMaterialName;
		protected System.Web.UI.WebControls.DropDownList ddlProviderName;
	
		protected ucPageView UcPageView1;
		protected System.Web.UI.WebControls.TextBox txtRecordCount;
		protected System.Web.UI.WebControls.TextBox txtToltalCount;
		protected System.Web.UI.WebControls.TextBox txtTotalFee;
		MaterialSBusi msb1=null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.ddlQueryType.Items.Add(new ListItem("原材料入库明细","0"));
					this.ddlQueryType.Items.Add(new ListItem("入库供应商月报表","1"));
					this.ddlQueryType.Items.Add(new ListItem("入库供应商汇总月报表","2"));

					string strvalue="";
					string strYear=DateTime.Now.Year.ToString();
					for(int i=0;i<12;i++)
					{
						if(i!=0&&DateTime.Now.AddMonths(-i).Month==12)
						{
							strYear=DateTime.Now.AddYears(-1).Year.ToString();
						}
						if(DateTime.Now.AddMonths(-i).Month<10)
						{
							strvalue=strYear+"0"+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						else
						{
							strvalue=strYear+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						this.ddlMonth.Items.Add(new ListItem(strvalue,strvalue));
					}

					this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='MaterialType'","全部");

					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");

					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					msb1=new MaterialSBusi(strcons);
					DataTable dtProviderList=msb1.GetProviderList();
					this.FillDropDownList(dtProviderList,this.ddlProviderName,"全部");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			string strQueryType=this.ddlQueryType.SelectedValue;
			if(strQueryType=="1")
			{
				if(this.ddlProviderName.SelectedValue=="全部")
				{
					this.Popup("请指定要统计的供应商！");
					return;
				}
			}
			string strProviderName=this.ddlProviderName.SelectedValue;
			if(strProviderName=="全部")
			{
				strProviderName="";
			}
			string strMaterialType=this.ddlMaterialType.SelectedValue;
			if(strMaterialType=="全部")
			{
				strMaterialType="";
			}
			Hashtable htpara=new Hashtable();
			htpara.Add("strMonth",this.ddlMonth.SelectedValue);
			htpara.Add("strProviderName",strProviderName);
			htpara.Add("strMaterialType",strMaterialType);
			htpara.Add("strMaterialName",this.txtMaterialName.Text.Trim());

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);
			try
			{
				DataSet dsout=msb1.GetMaterialsEnterReport(strQueryType,htpara);
				DataTable dtout=dsout.Tables["QueryResult"];
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					txtRecordCount.Text=dsout.Tables["SumState"].Rows[0]["RecordCount"].ToString();
					txtToltalCount.Text=dsout.Tables["SumState"].Rows[0]["TotalCount"].ToString();
					txtTotalFee.Text=dsout.Tables["SumState"].Rows[0]["TotalFee"].ToString();
					dtout.TableName="原材料资料清单";
					if(strQueryType=="0")
					{
						this.TableConvert(dtout,"原材料类型","tbNameCodeToStorage","vcCommSign='MaterialType'");
						this.TableConvert(dtout,"操作类型","tbNameCodeToStorage","vcCommSign='MaEnterOutType'");
						this.TableConvert(dtout,"部门","NewDept");
					}
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					//					for(int i=0;i<dtexcel.Rows.Count;i++)
					//					{
					//						dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
					//					}
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
	}
}
