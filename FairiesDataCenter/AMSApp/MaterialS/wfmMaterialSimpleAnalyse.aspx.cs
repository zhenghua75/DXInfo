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

namespace AMSApp.MaterialS
{
	/// <summary>
	/// wfmMaterialSimpleAnalyse 的摘要说明。
	/// </summary>
	public class wfmMaterialSimpleAnalyse : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnExcel;
	
		MaterialSBusi msb1=null;
		private bool upPager=true;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='MaterialType'");
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
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}

				if(this.DataGrid1.DataSource!=null)
				{
					if(((DataView)this.DataGrid1.DataSource).Count>0)
					{
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
			this.DataGrid1.ItemCreated+=new DataGridItemEventHandler(DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);
			try
			{
				DataTable dtout=msb1.GetMaterialsSimpleAnalyse(this.ddlMaterialType.SelectedValue,this.ddlMonth.SelectedValue);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					dtout.TableName="原材料分析报表";
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
				upPager=true;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void DataGrid1_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Pager)
			{
				if(upPager)
				{
					TableCell cell1=(TableCell)e.Item.Controls[0];
					cell1.Controls.Clear();
					cell1.BackColor=Color.SkyBlue;
					cell1.ForeColor=Color.Black;
					cell1.ColumnSpan=6;
					cell1.HorizontalAlign=HorizontalAlign.Center;
					cell1.Controls.Add(new LiteralControl("项目"));
					TableCell cell2=new TableCell();
					cell2.BackColor=Color.Gray;
					cell2.ForeColor=Color.Black;
					cell2.ColumnSpan=2;
					cell2.HorizontalAlign=HorizontalAlign.Center;
					cell2.Controls.Add(new LiteralControl("上月结存"));
					e.Item.Controls.Add(cell2);
					TableCell cell3=new TableCell();
					cell3.BackColor=Color.Gray;
					cell3.ForeColor=Color.Black;
					cell3.ColumnSpan=2;
					cell3.HorizontalAlign=HorizontalAlign.Center;
					cell3.Controls.Add(new LiteralControl("本月结存"));
					e.Item.Controls.Add(cell3);
					TableCell cell4=new TableCell();
					cell4.BackColor=Color.Gray;
					cell4.ForeColor=Color.Black;
					cell4.ColumnSpan=2;
					cell4.HorizontalAlign=HorizontalAlign.Center;
					cell4.Controls.Add(new LiteralControl("本月发出"));
					e.Item.Controls.Add(cell4);
					TableCell cell5=new TableCell();
					cell5.BackColor=Color.Gray;
					cell5.ForeColor=Color.Black;
					cell5.ColumnSpan=2;
					cell5.HorizontalAlign=HorizontalAlign.Center;
					cell5.Controls.Add(new LiteralControl("本月结存"));
					e.Item.Controls.Add(cell5);
					upPager=false;
				}
			}
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			upPager=true;
			this.DataGrid1.CurrentPageIndex=e.NewPageIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}
	}
}
