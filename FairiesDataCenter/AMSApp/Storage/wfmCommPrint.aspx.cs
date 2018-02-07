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
using System.Text;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmCommPrint.
	/// </summary>
	public class wfmCommPrint : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DataSet dsprint=(DataSet)Session["BillPrint"];
			string strType=Request.QueryString["type"];
　			Response.Write(CommPrint(dsprint,strType));
		}

		public string CommPrint(DataSet ds,string strType)
		{
			string colHeaders="";
			StringBuilder sb=new StringBuilder();
			DataTable dtLog=null;
			DataTable dtDetail=null;
			int myRow=0;
			int myCol=0;
			switch(strType)
			{
				case "EnterBill":
					dtLog=ds.Tables["EnterLog"];
					dtDetail=ds.Tables["EnterDetail"];
					string strProviderName=dtDetail.Rows[0]["cnvcProviderName"].ToString();
					dtDetail.Columns.Remove("cnvcProviderCode");
					dtDetail.Columns.Remove("cnvcProviderName");
					dtDetail.Columns.Remove("cnnStandardCount");
					dtDetail.Columns.Remove("cnvcUnit");
					dtDetail.Columns["cnvcProductCode"].ColumnName="产品编码";
					dtDetail.Columns["cnvcProductName"].ColumnName="产品名称";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="进仓规格";
					dtDetail.Columns["cnnPrice"].ColumnName="供货价格";
					dtDetail.Columns["cnnCount"].ColumnName="数量";
					dtDetail.Columns["cnnSum"].ColumnName="金额";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>供应商：</td><td colspan='2'>"+strProviderName+"</td><td colspan='3'>进仓日期："+dtLog.Rows[0]["cndEnterDate"].ToString()+"</td></tr>";

					for(int i=0;i<myCol;i++)
					{     
						colHeaders +="<td>"+ dtDetail.Columns[i].ColumnName.ToString()+"</td>";
					}
					colHeaders += "</tr>";
					sb.Append(colHeaders);

					double dSumFee=0;
					for(int i=0;i<myRow;i++)
					{
						dSumFee+=double.Parse(dtDetail.Rows[i]["金额"].ToString());
						sb.Append("<tr>");
						for(int j=0;j<myCol;j++)
						{
							sb.Append("<td>");
							sb.Append(dtDetail.Rows[i][j].ToString().Trim());
							sb.Append("</td>");
						}
						sb.Append("</tr>");   
					}

					sb.Append("<tr valign='center' height='20'><td colspan='6'>本次供货金额合计："+dSumFee.ToString()+"元</td></tr>");
					sb.Append("<tr valign='center' height='20'><td >送货员："+dtLog.Rows[0]["cnvcDeliverMan"].ToString()+"</td><td>验收："+dtLog.Rows[0]["cnvcValidateOperID"].ToString()+"</td><td>保安："+dtLog.Rows[0]["cnvcSafeOperID"].ToString()+"</td><td>仓管："+dtLog.Rows[0]["cnvcStorageOperID"].ToString()+"</td><td colspan='2'>打单："+dtLog.Rows[0]["cnvcBillOperID"].ToString()+"</td></tr>");

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='打印'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='返回'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "MoveBill":
					dtLog=ds.Tables["MoveLog"];
					dtDetail=ds.Tables["MoveDetail"];
					dtDetail.Columns["cnvcProductCode"].ColumnName="产品编码";
					dtDetail.Columns["cnvcProductName"].ColumnName="产品名称";
					dtDetail.Columns["cnvcUnit"].ColumnName="单位";
					dtDetail.Columns["cnnMoveCount"].ColumnName="原数量";
					dtDetail.Columns["cnnLoseCount"].ColumnName="损耗量";
					dtDetail.Columns["cnnRealMoveCount"].ColumnName="实际数量";
					dtDetail.Columns["cnvcComments"].ColumnName="原因";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>调出店：</td><td>"+dtLog.Rows[0]["cnvcOutDeptID"].ToString()+"</td><td>调入店：</td><td colspan='2'>"+dtLog.Rows[0]["cnvcInDeptID"].ToString()+"</td><td colspan='2'>序号："+dtLog.Rows[0]["cnnMoveSerialNo"].ToString()+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>调出人：</td><td>"+dtLog.Rows[0]["cnvcOutOperID"].ToString()+"</td><td>调入人：</td><td colspan='2'>"+dtLog.Rows[0]["cnvcInOperID"].ToString()+"</td><td colspan='2'>.</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>日期：</td><td colspan='4'>"+dtLog.Rows[0]["cndOperDate"].ToString()+"</td><td colspan='2'>.</td></tr>";

					for(int i=0;i<myCol;i++)
					{     
						colHeaders +="<td>"+ dtDetail.Columns[i].ColumnName.ToString()+"</td>";
					}
					colHeaders += "</tr>";
					sb.Append(colHeaders);

					for(int i=0;i<myRow;i++)
					{
						sb.Append("<tr>");
						for(int j=0;j<myCol;j++)
						{
							sb.Append("<td>");
							sb.Append(dtDetail.Rows[i][j].ToString().Trim());
							sb.Append("</td>");
						}
						sb.Append("</tr>");   
					}

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='打印'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='返回'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "SpecReceiveBill":
					dtLog=ds.Tables["ReceiveLog"];
					dtDetail=ds.Tables["ReceiveDetail"];
					dtDetail.Columns.Remove("cnnStandardCount");
					dtDetail.Columns["cnvcProductCode"].ColumnName="物料编码";
					dtDetail.Columns["cnvcProductName"].ColumnName="物料名称";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="规格";
					dtDetail.Columns["cnvcUnit"].ColumnName="单位";
					dtDetail.Columns["cnnReceiveCount"].ColumnName="应领量";
					dtDetail.Columns["cnnOutCount"].ColumnName="发货量";
					dtDetail.Columns["cnnClassStorage"].ColumnName="上班库存";
					dtDetail.Columns["cnnLoseCount"].ColumnName="损耗量";
					dtDetail.Columns["cnnCount"].ColumnName="实际领用量";
					dtDetail.Columns["cnvcReceiveOperID"].ColumnName="领料人签名";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>领料单位：</td><td colspan='2'>"+dtLog.Rows[0]["cnvcReceiveDeptID"].ToString()+"</td><td>生产组：</td><td>"+dtLog.Rows[0]["cnvcGroup"].ToString()+"</td><td colspan='3'>领料日期："+dtLog.Rows[0]["cndReceiveDate"].ToString()+"</td><td colspan='2'>序号："+dtLog.Rows[0]["cnnReceiveSerialNo"].ToString()+"</td></tr>";

					for(int i=0;i<myCol;i++)
					{     
						colHeaders +="<td>"+ dtDetail.Columns[i].ColumnName.ToString()+"</td>";
					}
					colHeaders += "</tr>";
					sb.Append(colHeaders);

					for(int i=0;i<myRow;i++)
					{
						sb.Append("<tr>");
						for(int j=0;j<myCol;j++)
						{
							sb.Append("<td>");
							sb.Append(dtDetail.Rows[i][j].ToString().Trim());
							sb.Append("</td>");
						}
						sb.Append("</tr>");   
					}

					sb.Append("<tr valign='center' height='20'><td colspan='3'>物料主管核："+dtLog.Rows[0]["cnvcMaterialInchargeOperID"].ToString()+"</td><td colspan='3'>仓库主管审："+dtLog.Rows[0]["cnvcStorageInchargeOperID"].ToString()+"</td><td colspan='4'>发料人："+dtLog.Rows[0]["cnvcSendOperID"].ToString()+"</td></tr>");

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='打印'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='返回'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "ReceiveSendOutBill":
					dtLog=ds.Tables["dtSendLog"];
					dtDetail=ds.Tables["dtSendDetail"];
					dtDetail.Columns["cnvcProductCode"].ColumnName="物料编码";
					dtDetail.Columns["cnvcProductName"].ColumnName="物料名称";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="规格单位";
					dtDetail.Columns["cnnReceiveCount"].ColumnName="总应领量";
					dtDetail.Columns["cnvcQC002"].ColumnName="倾城店";
					dtDetail.Columns["cnvcCF001"].ColumnName="翠湖店";
					dtDetail.Columns["cnvcCF006"].ColumnName="财富店";
					dtDetail.Columns["cnvcBS004"].ColumnName="宝善店";
					dtDetail.Columns["cnvcJG003"].ColumnName="金格店";
					dtDetail.Columns["cnvcXS007"].ColumnName="新世界";
					dtDetail.Columns["cnvcSH005"].ColumnName="上海沙龙";
					dtDetail.Columns["cnvcFYZX1"].ColumnName="生产中心";
					dtDetail.Columns["cnvcCY009"].ColumnName="创意英国店";
					dtDetail.Columns["cnvcJM010"].ColumnName="金马坊店";
					dtDetail.Columns["cnvcXM011"].ColumnName="小西门店";
					dtDetail.Columns["cnnOutCount"].ColumnName="总发货量";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					if(dtLog.Rows[0]["cnnSendSerialNo"].ToString()!="")
					{
						colHeaders+="<tr valign='center' height='20'><td colspan='3' align='right'>发货单号：</td><td colspan='13' align='left'>"+dtLog.Rows[0]["cnnSendSerialNo"].ToString()+"</td></tr>";
					}

					for(int i=0;i<myCol;i++)
					{     
						colHeaders +="<td>"+ dtDetail.Columns[i].ColumnName.ToString()+"</td>";
					}
					colHeaders += "</tr>";
					sb.Append(colHeaders);

					for(int i=0;i<myRow;i++)
					{
						sb.Append("<tr>");
						for(int j=0;j<myCol;j++)
						{
							sb.Append("<td>");
							sb.Append(dtDetail.Rows[i][j].ToString().Trim());
							sb.Append("</td>");
						}
						sb.Append("</tr>");   
					}

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='打印'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='返回'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
			}


			colHeaders=sb.ToString();               
			return(colHeaders);
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
