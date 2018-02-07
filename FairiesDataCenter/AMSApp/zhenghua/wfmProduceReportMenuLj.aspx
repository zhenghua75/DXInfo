<%@ Page language="c#" Codebehind="wfmProduceReportMenuLj.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.wfmProduceReportMenuLj" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProduceMenu</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="images/coolwp2.jpg">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblProduceMenu" cellSpacing="1" cellPadding="1" width="136" align="left" border="0"
				runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trProductListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductListReport.aspx'"
							href="javascript:">产品清单</A></TD>
				</TR>
				<TR id="trProductSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductSumReport.aspx'"
							href="javascript:">产品汇总</A></TD>
				</TR>
				<TR id="trProductLostListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductLostListReport.aspx'"
							href="javascript:">报损明细</A></TD>
				</TR>
				<TR id="trProductLostSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductLostSumReport.aspx'"
							href="javascript:">报损汇总</A></TD>
				</TR>
				<%--
				<TR id="trSalesList" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesList.aspx'"
							href="javascript:">销售入库清单</A></TD>
				</TR>
				<TR id="trSalesSum" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesSum.aspx'"
							href="javascript:">销售入库统计报表</A></TD>
				</TR>
				
				<TR id="trSalesCheckListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesCheckListReport.aspx'"
							href="javascript:">销售盘点清单</A></TD>
				</TR>
				<TR id="trSalesCheckSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesCheckSumReport.aspx'"
							href="javascript:">销售盘点统计报表</A></TD>
				</TR>
				
				<TR id="trSalesChart" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesChart.aspx'"
							href="javascript:">销售曲线图</A></TD>
				</TR>--%>
				<TR id="trProduceLogReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProduceLogReport.aspx'"
							href="javascript:">操作日志</A></TD>
				</TR>
				<TR id="trProduceLostSalesReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProduceLostSalesReport.aspx'"
							href="javascript:">评估报表</A></TD>
				</TR>
				<TR id="trProductMaterialUseReorpt" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Storage/wfmProductMaterialUseReorpt.aspx'"
							href="javascript:">原材料生产评估</A></TD>
				</TR>			
				<TR id="trCostReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmCostReport.aspx'"
							href="javascript:">生产成本核算报表</A></TD>
				</TR>			
			</TABLE>
		</form>
	</body>
</HTML>
