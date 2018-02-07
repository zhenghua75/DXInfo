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
��			Response.Write(CommPrint(dsprint,strType));
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
					dtDetail.Columns["cnvcProductCode"].ColumnName="��Ʒ����";
					dtDetail.Columns["cnvcProductName"].ColumnName="��Ʒ����";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="���ֹ��";
					dtDetail.Columns["cnnPrice"].ColumnName="�����۸�";
					dtDetail.Columns["cnnCount"].ColumnName="����";
					dtDetail.Columns["cnnSum"].ColumnName="���";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>��Ӧ�̣�</td><td colspan='2'>"+strProviderName+"</td><td colspan='3'>�������ڣ�"+dtLog.Rows[0]["cndEnterDate"].ToString()+"</td></tr>";

					for(int i=0;i<myCol;i++)
					{     
						colHeaders +="<td>"+ dtDetail.Columns[i].ColumnName.ToString()+"</td>";
					}
					colHeaders += "</tr>";
					sb.Append(colHeaders);

					double dSumFee=0;
					for(int i=0;i<myRow;i++)
					{
						dSumFee+=double.Parse(dtDetail.Rows[i]["���"].ToString());
						sb.Append("<tr>");
						for(int j=0;j<myCol;j++)
						{
							sb.Append("<td>");
							sb.Append(dtDetail.Rows[i][j].ToString().Trim());
							sb.Append("</td>");
						}
						sb.Append("</tr>");   
					}

					sb.Append("<tr valign='center' height='20'><td colspan='6'>���ι������ϼƣ�"+dSumFee.ToString()+"Ԫ</td></tr>");
					sb.Append("<tr valign='center' height='20'><td >�ͻ�Ա��"+dtLog.Rows[0]["cnvcDeliverMan"].ToString()+"</td><td>���գ�"+dtLog.Rows[0]["cnvcValidateOperID"].ToString()+"</td><td>������"+dtLog.Rows[0]["cnvcSafeOperID"].ToString()+"</td><td>�ֹܣ�"+dtLog.Rows[0]["cnvcStorageOperID"].ToString()+"</td><td colspan='2'>�򵥣�"+dtLog.Rows[0]["cnvcBillOperID"].ToString()+"</td></tr>");

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='��ӡ'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='����'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "MoveBill":
					dtLog=ds.Tables["MoveLog"];
					dtDetail=ds.Tables["MoveDetail"];
					dtDetail.Columns["cnvcProductCode"].ColumnName="��Ʒ����";
					dtDetail.Columns["cnvcProductName"].ColumnName="��Ʒ����";
					dtDetail.Columns["cnvcUnit"].ColumnName="��λ";
					dtDetail.Columns["cnnMoveCount"].ColumnName="ԭ����";
					dtDetail.Columns["cnnLoseCount"].ColumnName="�����";
					dtDetail.Columns["cnnRealMoveCount"].ColumnName="ʵ������";
					dtDetail.Columns["cnvcComments"].ColumnName="ԭ��";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>�����꣺</td><td>"+dtLog.Rows[0]["cnvcOutDeptID"].ToString()+"</td><td>����꣺</td><td colspan='2'>"+dtLog.Rows[0]["cnvcInDeptID"].ToString()+"</td><td colspan='2'>��ţ�"+dtLog.Rows[0]["cnnMoveSerialNo"].ToString()+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>�����ˣ�</td><td>"+dtLog.Rows[0]["cnvcOutOperID"].ToString()+"</td><td>�����ˣ�</td><td colspan='2'>"+dtLog.Rows[0]["cnvcInOperID"].ToString()+"</td><td colspan='2'>.</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>���ڣ�</td><td colspan='4'>"+dtLog.Rows[0]["cndOperDate"].ToString()+"</td><td colspan='2'>.</td></tr>";

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
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='��ӡ'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='����'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "SpecReceiveBill":
					dtLog=ds.Tables["ReceiveLog"];
					dtDetail=ds.Tables["ReceiveDetail"];
					dtDetail.Columns.Remove("cnnStandardCount");
					dtDetail.Columns["cnvcProductCode"].ColumnName="���ϱ���";
					dtDetail.Columns["cnvcProductName"].ColumnName="��������";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="���";
					dtDetail.Columns["cnvcUnit"].ColumnName="��λ";
					dtDetail.Columns["cnnReceiveCount"].ColumnName="Ӧ����";
					dtDetail.Columns["cnnOutCount"].ColumnName="������";
					dtDetail.Columns["cnnClassStorage"].ColumnName="�ϰ���";
					dtDetail.Columns["cnnLoseCount"].ColumnName="�����";
					dtDetail.Columns["cnnCount"].ColumnName="ʵ��������";
					dtDetail.Columns["cnvcReceiveOperID"].ColumnName="������ǩ��";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					colHeaders+="<tr valign='center' height='20'><td>���ϵ�λ��</td><td colspan='2'>"+dtLog.Rows[0]["cnvcReceiveDeptID"].ToString()+"</td><td>�����飺</td><td>"+dtLog.Rows[0]["cnvcGroup"].ToString()+"</td><td colspan='3'>�������ڣ�"+dtLog.Rows[0]["cndReceiveDate"].ToString()+"</td><td colspan='2'>��ţ�"+dtLog.Rows[0]["cnnReceiveSerialNo"].ToString()+"</td></tr>";

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

					sb.Append("<tr valign='center' height='20'><td colspan='3'>�������ܺˣ�"+dtLog.Rows[0]["cnvcMaterialInchargeOperID"].ToString()+"</td><td colspan='3'>�ֿ�������"+dtLog.Rows[0]["cnvcStorageInchargeOperID"].ToString()+"</td><td colspan='4'>�����ˣ�"+dtLog.Rows[0]["cnvcSendOperID"].ToString()+"</td></tr>");

					sb.Append("<tr class='noprint'><td align='center' colspan='"+myCol.ToString()+"'>");
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='��ӡ'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='����'/>");
					sb.Append("</td></tr>");
					sb.Append("</table>"); 
					break;
				case "ReceiveSendOutBill":
					dtLog=ds.Tables["dtSendLog"];
					dtDetail=ds.Tables["dtSendDetail"];
					dtDetail.Columns["cnvcProductCode"].ColumnName="���ϱ���";
					dtDetail.Columns["cnvcProductName"].ColumnName="��������";
					dtDetail.Columns["cnvcStandardUnit"].ColumnName="���λ";
					dtDetail.Columns["cnnReceiveCount"].ColumnName="��Ӧ����";
					dtDetail.Columns["cnvcQC002"].ColumnName="��ǵ�";
					dtDetail.Columns["cnvcCF001"].ColumnName="�����";
					dtDetail.Columns["cnvcCF006"].ColumnName="�Ƹ���";
					dtDetail.Columns["cnvcBS004"].ColumnName="���Ƶ�";
					dtDetail.Columns["cnvcJG003"].ColumnName="����";
					dtDetail.Columns["cnvcXS007"].ColumnName="������";
					dtDetail.Columns["cnvcSH005"].ColumnName="�Ϻ�ɳ��";
					dtDetail.Columns["cnvcFYZX1"].ColumnName="��������";
					dtDetail.Columns["cnvcCY009"].ColumnName="����Ӣ����";
					dtDetail.Columns["cnvcJM010"].ColumnName="������";
					dtDetail.Columns["cnvcXM011"].ColumnName="С���ŵ�";
					dtDetail.Columns["cnnOutCount"].ColumnName="�ܷ�����";

					myRow=dtDetail.Rows.Count; 
					myCol=dtDetail.Columns.Count;

					colHeaders="<table border='1' cellSpacing='0' cellPadding='0' style='FONT-SIZE: 10pt;' align='center' width='600'>";

					colHeaders+="<tr><td colspan='"+myCol+"' align='center' style='FONT-SIZE: 14pt;' height='30' valign='center'>"+ds.DataSetName+"</td></tr>";
					if(dtLog.Rows[0]["cnnSendSerialNo"].ToString()!="")
					{
						colHeaders+="<tr valign='center' height='20'><td colspan='3' align='right'>�������ţ�</td><td colspan='13' align='left'>"+dtLog.Rows[0]["cnnSendSerialNo"].ToString()+"</td></tr>";
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
					sb.Append("<input type='button' id='cmdPRINT' onclick='printReport()' value='��ӡ'/>");
					sb.Append("<input  type='button' onclick='javascript:window.history.back();' value='����'/>");
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
