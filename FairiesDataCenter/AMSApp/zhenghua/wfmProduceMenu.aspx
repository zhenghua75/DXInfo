<%@ Page language="c#" Codebehind="wfmProduceMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.wfmProduceMenu" %>
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
			<TABLE id="tblProduceMenu" cellSpacing="1" cellPadding="1" width="146" align="left" border="0"
				runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="HEIGHT: 38px; COLOR: #330033; FONT-WEIGHT: bold" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trMaterial" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Formula/wfmMaterial.aspx'"
							href="javascript:">原料材料维护</A></TD>
				</TR>
				<TR id="trFormulaQuery" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Formula/wfmFormulaQuery.aspx'"
							href="javascript:">配方维护</A></TD>
				</TR>
				<TR id="trProductQuery" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Order/wfmProductQuery.aspx'"
							href="javascript:">下单-【产品查询】</A></TD>
				</TR>
				<TR id="trOrderDetail" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Order/wfmOrderDetail.aspx'"
							href="javascript:">下单-【订单细节】</A></TD>
				</TR>
				<TR id="trOrder" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Order/wfmOrder.aspx'"
							href="javascript:">下单 -【下单】</A></TD>
				</TR>
				<TR id="trOrderQuery" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Order/wfmOrderQuery.aspx'"
							href="javascript:">订单维护</A></TD>
				</TR>
				<TR id="trProducePlanQuery" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmProducePlanQuery.aspx'"
							href="javascript:">生产计划</A></TD>
				</TR>
				<TR id="trProducePlanQueryMake" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmProducePlanQueryMake.aspx'"
							href="javascript:">预估打单</A></TD>
				</TR>
				<TR id="trProducePlanQueryGoods" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmProducePlanQueryGoods.aspx'"
							href="javascript:">分货</A></TD>
				</TR>
				<TR id="trSalesRoomProduce" runat="server">
					<TD align="center" style="HEIGHT: 38px" background="images/anniu.jpg"><A style="COLOR: #ffffff; FONT-SIZE: 12pt; TEXT-DECORATION: none" onclick="parent.right.location='./Produce/wfmSalesRoomProduce.aspx'"
							href="javascript:">门市生产</A></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
