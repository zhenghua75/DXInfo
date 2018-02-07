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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;
namespace AMSApp.StockControl
{
	/// <summary>
	/// wfmAddComputationGroup 的摘要说明。
	/// </summary>
	public class tbComputationGroup : wfmBase
	{
        //protected System.Web.UI.WebControls.Label Label1;
        //protected System.Web.UI.WebControls.TextBox TextBox1;
        //protected System.Web.UI.WebControls.Label Label2;
        //protected System.Web.UI.WebControls.TextBox TextBox2;
        //protected System.Web.UI.WebControls.Label lblTitle;
        //protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            //this.Response.Expires = -1;
            //this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
            //this.Response.CacheControl = "no-cache";

            //if(!this.IsPostBack)
            //{
            //    string strflag = this.Request.QueryString["flag"].ToString();
            //    if(strflag=="add")
            //    {
            //        this.lblTitle.Text = "添加计量单位组";
            //        this.TextBox2.Text = this.GetComputationGroupCode();
            //        //this.TextBox2.Enabled = false;
            //    }
            //    if(strflag=="modify")
            //    {
            //        this.lblTitle.Text = "修改计量单位组";
            //        string strgroupcode = this.Request.QueryString["groupcode"].ToString();
            //        string strgroupname = this.Request.QueryString["groupname"].ToString();
            //        this.TextBox2.Text = strgroupcode;
            //        this.TextBox1.Text = strgroupname;
            //        //this.TextBox2.Enabled = false;
            //    }
            //    this.TextBox2.Enabled = false;
			//}
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
			//this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        //private void Button1_Click(object sender, System.EventArgs e)
        //{
        //    //添加计量单位组
        //    if(this.JudgeIsNull(this.TextBox2.Text))
        //    {
        //        this.Popup("计量单位组编码获取失败，请关闭页面重新获取");
        //        return;
        //    }
        //    if(this.JudgeIsNull(this.TextBox1.Text))
        //    {
        //        this.Popup("请输入计量单位组名称");
        //        return;
        //    }
        //    if(this.lblTitle.Text == "添加计量单位组")
        //    {
        //        Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
        //        ol.cnvcOperType = "添加计量单位组";
        //        ol.cnvcOperID = this.oper.strLoginID;
        //        ol.cnvcDeptID = this.oper.strDeptID;

        //        Entity.ComputationGroup cg = new ComputationGroup();
        //        cg.cnvcGroupCode = this.TextBox2.Text;
        //        cg.cnvcGroupName = this.TextBox1.Text;
				
        //        Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
        //        int ret = bcu.AddComputationGroup(ol,cg);
        //        if(ret > 0 )
        //        {
        //            this.Popup("添加计量单位组成功！");
        //        }
        //        else
        //        {
        //            this.Popup("添加计量单位组失败！");
        //        }

        //        //this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
        //    }
        //    else if(this.lblTitle.Text == "修改计量单位组")
        //    {
        //        Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
        //        ol.cnvcOperType = "修改计量单位组";
        //        ol.cnvcOperID = this.oper.strLoginID;
        //        ol.cnvcDeptID = this.oper.strDeptID;

				

        //        Entity.ComputationGroup cg = new ComputationGroup();
        //        cg.cnvcGroupCode = this.TextBox2.Text;
        //        cg.cnvcGroupName = this.TextBox1.Text;

        //        Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
        //        int ret = bcu.UpdateComputationGroup(ol,cg);
        //        if(ret > 0 )
        //        {
        //            this.Popup("修改计量单位组成功！");
        //        }
        //        else
        //        {
        //            this.Popup("修改计量单位组失败！");
        //        }
				
        //    }
        //    this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
        //}
	
		
	}
}
