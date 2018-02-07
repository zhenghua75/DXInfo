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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmProductMaterialUseReorpt 的摘要说明。
	/// </summary>
	public class wfmProductMaterialUseReorpt : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label1;

		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected string strBeginDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				this.FillDropDownList("NewDept",this.ddlDept,"","全部");
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
				strEndDate =  Request.Form["txtEnd"].ToString();
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
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string strsql="";
			if(this.ddlDept.SelectedValue!="全部")
			{
				strsql="select t.cnvcProduceDeptID as 部门,t.cnvcInvCode as 存货编码,t.cnvcInvName as 存货名称,t.cnvcProduceUnitCode as 单位,cnnMakeCount as 应用量,";
				strsql+="cnnCollarCount as 实际使用量,isnull(g.lost,0) as 损耗量 from (select c.cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode,";
				strsql+="sum(cnnMakeCount) as cnnMakeCount,sum(cnnCollarCount) as cnnCollarCount from tbMakeLog a,tbMakeDetail b,tbProduceCheckLog c,tbInventory d";
				strsql+=" where a.cnnMakeSerialNo=b.cnnMakeSerialNo and a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59' and c.cnvcProduceDeptID='"+this.ddlDept.SelectedValue+"'"; 
				strsql+=" and a.cnnProduceSerialNo=c.cnnProduceSerialNo and b.cnvcInvCode=d.cnvcInvCode and b.cnvcInvCode=d.cnvcInvCode";
				strsql+=" group by cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode) t left join (select a.cnvcInvCode,";
				strsql+="sum((a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount)*c.cniChangRate/d.cniChangRate) as lost from tbLostSerial a,tbInventory b,tbComputationUnit c,";
				strsql+="tbComputationUnit d where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
				strsql+=" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcComunitCode=c.cnvcComunitCode and b.cnvcProduceUnitCode=d.cnvcComunitCode group by a.cnvcInvCode) g";
				strsql+=" on t.cnvcInvCode=g.cnvcInvCode order by t.cnvcProduceDeptID,t.cnvcInvCode,t.cnvcInvName";
			}
			else
			{
				strsql="select t.cnvcProduceDeptID as 部门,t.cnvcInvCode as 存货编码,t.cnvcInvName as 存货名称,t.cnvcProduceUnitCode as 单位,cnnMakeCount as 应用量,";
				strsql+="cnnCollarCount as 实际使用量,isnull(g.lost,0) as 损耗量 from (select c.cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode,";
				strsql+="sum(cnnMakeCount) as cnnMakeCount,sum(cnnCollarCount) as cnnCollarCount from tbMakeLog a,tbMakeDetail b,tbProduceCheckLog c,tbInventory d";
				strsql+=" where a.cnnMakeSerialNo=b.cnnMakeSerialNo and a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'"; 
				strsql+=" and a.cnnProduceSerialNo=c.cnnProduceSerialNo and b.cnvcInvCode=d.cnvcInvCode and b.cnvcInvCode=d.cnvcInvCode";
				strsql+=" group by cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode) t left join (select a.cnvcInvCode,";
				strsql+="sum((a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount)*c.cniChangRate/d.cniChangRate) as lost from tbLostSerial a,tbInventory b,tbComputationUnit c,";
				strsql+="tbComputationUnit d where cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
				strsql+=" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcComunitCode=c.cnvcComunitCode and b.cnvcProduceUnitCode=d.cnvcComunitCode group by a.cnvcInvCode) g";
				strsql+=" on t.cnvcInvCode=g.cnvcInvCode order by t.cnvcProduceDeptID,t.cnvcInvCode,t.cnvcInvName";
			}
				
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"单位","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="原材料生产使用表";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
