<%@ Page language="c#" Codebehind="wfmProduceMenuLj.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.wfmProduceMenuLj" %>
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
				<TR id="trOrderQuery" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Order/wfmOrderQuery.aspx'" href="javascript:">生产订单</A></TD>
				</TR>
				<TR id="trProducePlanQuery" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmProducePlanQuery.aspx'" href="javascript:">生产计划</A></TD>
				</TR>
				<TR id="trProducePlanQueryMake" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmProducePlanQueryMake.aspx'" href="javascript:">生产预估</A></TD>
				</TR>
				<TR id="trWarehouseOut" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmWarehouseOut.aspx'" href="javascript:">生产材料领用</A></TD>
				</TR>
				<TR id="trProduceCheck" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmProduceCheck.aspx'" href="javascript:">生产盘点</A></TD>
				</TR>
				<TR id="trProduceCheckWh" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmProduceCheckWh.aspx'" href="javascript:">生产入库</A></TD>
				</TR>
				<TR id="trProducePlanQueryGoods" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Produce/wfmProducePlanQueryGoods.aspx'" href="javascript:">分货</A></TD>
				</TR>
				<TR id="trAddProductLost" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Lj/wfmAddProductLost.aspx'" href="javascript:">生产产品报损</A></TD>
				</TR>
				<TR id="trAdjustProductLost" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none"
							onclick="parent.right.location='./Lj/wfmAdjustProductLost.aspx'" href="javascript:">生产产品报损调整</A></TD>
				</TR>
				<%--
				<TR id="trAddProduct" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmAddProduct.aspx'"
							href="javascript:">生产产品入库</A></TD>
				</TR>
				<TR id="trAdjustProduct" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmAdjustProduct.aspx'"
							href="javascript:">生产产品调整</A></TD>
				</TR>
				<TR id="trSalesRoomProduce" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmSalesRoomProduce.aspx'"
							href="javascript:">门市生产</A></TD>
				</TR>
				<TR id="trAddSales" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmAddSales.aspx'"
							href="javascript:">销售入库</A></TD>
				</TR>
				<TR id="trAdjustSales" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmAdjustSales.aspx'"
							href="javascript:">销售入库调整</A></TD>
				</TR>
				
				<TR id="trCheck" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmCheck.aspx'"
							href="javascript:">销售盘点入库</A></TD>
				</TR>
				<TR id="trAdjustCheck" runat="server">
					<TD align="center" style="HEIGHT: 31px" background="images/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='./Lj/wfmAdjustCheck.aspx'"
							href="javascript:">销售盘点入库调整</A></TD>
				</TR>
				--%>
			</TABLE>
		</form>
	</body>
</HTML>
