using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace AMSApp
	{
	/// <summary>
	///		Summary description for wbcPageView.
	/// </summary>
	public abstract class ucPageView : System.Web.UI.UserControl
	{
		public System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.Label lbPageLabel;
		protected System.Web.UI.WebControls.LinkButton btnGo;
		protected System.Web.UI.WebControls.LinkButton btnLast;
		protected System.Web.UI.WebControls.LinkButton btnNext;
		protected System.Web.UI.WebControls.LinkButton btnPrev;
		protected System.Web.UI.WebControls.LinkButton btnFirst;

		private System.Data.DataView iCollection;

		public int iRecordCount = 0;
		public System.Web.UI.HtmlControls.HtmlTableRow FootBar;

		public System.Data.DataView MyDataSource
		{
			set{
				this.iCollection = value;
				Session["page_view"] = value;
			}
			get{
				return this.iCollection;
			}
		}
		
			
		protected void PagerButtonClick(Object sender, EventArgs e) 
		{
			//used by external paging UI
			String arg = ((LinkButton)sender).CommandArgument;
			SetDataGridCurrentPageIndex(MyDataGrid,arg);
			BindGrid();
		}

		protected void MyDataGrid_Page(Object sender, DataGridPageChangedEventArgs e) 
		{
			//used by built-in pager.  CurrentPageIndex already set
			MyDataGrid.CurrentPageIndex = e.NewPageIndex;			
			BindGrid();
		}				

		//分页索引
		protected void SetDataGridCurrentPageIndex(DataGrid myDataGrid,string strArg)
		{
			switch(strArg)
			{
				case ("next"):
					if (myDataGrid.CurrentPageIndex < (myDataGrid.PageCount - 1))
						myDataGrid.CurrentPageIndex ++;
					break;
				case ("prev"):
					if (myDataGrid.CurrentPageIndex > 0)
						myDataGrid.CurrentPageIndex --;
					break;
				case ("last"):
					myDataGrid.CurrentPageIndex = (myDataGrid.PageCount - 1);
					break;
				case ("jump"):
					int iTempIndex = Convert.ToInt16(Request["page_number"])-1;//PageNumber.Value)-1;
					if(iTempIndex > myDataGrid.PageCount-1)
						iTempIndex = myDataGrid.PageCount-1;
					if(iTempIndex < 0)
						iTempIndex = 0;
					myDataGrid.CurrentPageIndex = iTempIndex;
					break;
				default:
					//page number
					myDataGrid.CurrentPageIndex = Convert.ToInt32(strArg);
					break;
			}			
		}	
		   
		//数据源绑定
		public void BindGrid() 
		{
			if(this.iCollection!=null)
			{
				MyDataGrid.DataSource = this.iCollection;		
				MyDataGrid.CurrentPageIndex = 0;
				this.iRecordCount = this.iCollection.Count;
			}
			else
			{
				MyDataGrid.DataSource = (DataView)Session["page_view"];				
				this.iRecordCount = ((DataView)Session["page_view"]).Count;
			}
			if(this.iRecordCount>0)
			{
				this.FootBar.Visible = true;
			}
			else
			{
				this.FootBar.Visible = false;
			}			
			MyDataGrid.DataBind();			
			ShowPageLabel(lbPageLabel);	
		}

		//根据页码绑定数据
		public void BindGridPage(int iPageIndex) 
		{
			if(this.iCollection!=null)
			{
				MyDataGrid.DataSource = this.iCollection;		
				MyDataGrid.CurrentPageIndex = iPageIndex;
				this.iRecordCount = this.iCollection.Count;
			}
			else
			{
				MyDataGrid.DataSource = (DataView)Session["page_view"];				
				this.iRecordCount = ((DataView)Session["page_view"]).Count;
			}
			if(this.iRecordCount>0)
			{
				this.FootBar.Visible = true;
			}
			else
			{
				this.FootBar.Visible = false;
			}			
			MyDataGrid.DataBind();			
			ShowPageLabel(lbPageLabel);			
		}

		public void DebindGrid()
		{
			MyDataGrid.DataSource = null;
			Session.Remove("page_view");
			this.FootBar.Visible = false;
			MyDataGrid.DataBind();
		}

		//提取
		public void ShowPageLabel(Label myLable) 
		{
			myLable.Text = "第 " + (MyDataGrid.CurrentPageIndex+1) +" 页/共 " + MyDataGrid.PageCount+" 页，共"+this.iRecordCount+"条记录";
		}

		private void MyDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{			
			this.FootBar.Visible = false;
			if(Session["page_view"]!=null)
			{
				if(((DataView)Session["page_view"]).Count>0)
				{
					this.FootBar.Visible = true;
				}

			}
			if(MyDataGrid.DataSource!=null)
			{
				if(((DataView)MyDataGrid.DataSource).Count>0)
				{
					this.FootBar.Visible = true;
				}
			}

		}
	
	}
}
