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
					<TD style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" align="center" bgColor="#ebf0ec">û��Ȩ��</TD>
				</TR>
				<TR id="trProductListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductListReport.aspx'"
							href="javascript:">��Ʒ�嵥</A></TD>
				</TR>
				<TR id="trProductSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductSumReport.aspx'"
							href="javascript:">��Ʒ����</A></TD>
				</TR>
				<TR id="trProductLostListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductLostListReport.aspx'"
							href="javascript:">������ϸ</A></TD>
				</TR>
				<TR id="trProductLostSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProductLostSumReport.aspx'"
							href="javascript:">�������</A></TD>
				</TR>
				<%--
				<TR id="trSalesList" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesList.aspx'"
							href="javascript:">��������嵥</A></TD>
				</TR>
				<TR id="trSalesSum" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesSum.aspx'"
							href="javascript:">�������ͳ�Ʊ���</A></TD>
				</TR>
				
				<TR id="trSalesCheckListReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesCheckListReport.aspx'"
							href="javascript:">�����̵��嵥</A></TD>
				</TR>
				<TR id="trSalesCheckSumReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesCheckSumReport.aspx'"
							href="javascript:">�����̵�ͳ�Ʊ���</A></TD>
				</TR>
				
				<TR id="trSalesChart" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmSalesChart.aspx'"
							href="javascript:">��������ͼ</A></TD>
				</TR>--%>
				<TR id="trProduceLogReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProduceLogReport.aspx'"
							href="javascript:">������־</A></TD>
				</TR>
				<TR id="trProduceLostSalesReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/Report/wfmProduceLostSalesReport.aspx'"
							href="javascript:">��������</A></TD>
				</TR>
				<TR id="trProductMaterialUseReorpt" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Storage/wfmProductMaterialUseReorpt.aspx'"
							href="javascript:">ԭ������������</A></TD>
				</TR>			
				<TR id="trCostReport" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmCostReport.aspx'"
							href="javascript:">�����ɱ����㱨��</A></TD>
				</TR>			
			</TABLE>
		</form>
	</body>
</HTML>
