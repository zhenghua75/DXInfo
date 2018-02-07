<%@ Page language="c#" Codebehind="wfmStorageMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmStorageMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmStorageMenu</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">* { BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px }
	BODY { FONT-SIZE: 12px; FONT-FAMILY: arial, 宋体, serif }
	#nav { WIDTH: 140px; LINE-HEIGHT: 24px; LIST-STYLE-TYPE: none; TEXT-ALIGN: left }
	#nav A { DISPLAY: block; PADDING-LEFT: 5px; WIDTH: 140px }
	#nav LI { BACKGROUND: #ccc; FLOAT: left; BORDER-BOTTOM: #fff 1px solid }
	#nav LI A:hover { BACKGROUND: #cc0000 }
	#nav A:link { COLOR: #666; TEXT-DECORATION: none }
	#nav A:visited { COLOR: #666; TEXT-DECORATION: none }
	#nav A:hover { COLOR: #fff; TEXT-DECORATION: none }
	#nav LI UL { LIST-STYLE-TYPE: none; TEXT-ALIGN: left }
	#nav LI UL LI { PADDING-LEFT: 5px; BACKGROUND: #ebebeb; WIDTH: 140px }
	#nav LI UL A { PADDING-LEFT: 5px; WIDTH: 140px }
	#nav LI UL A:link { COLOR: #666; TEXT-DECORATION: none }
	#nav LI UL A:visited { COLOR: #666; TEXT-DECORATION: none }
	#nav LI UL A:hover { FONT-WEIGHT: normal; BACKGROUND: #cc0000; COLOR: #f3f3f3; TEXT-DECORATION: none }
	#nav LI:hover UL { LEFT: auto }
	#nav LI.sfhover UL { LEFT: auto }
	#content { CLEAR: left }
	#nav UL.collapsed { DISPLAY: none }
	#PARENT { PADDING-LEFT: 5px; WIDTH: 140px }
	</style>
</HEAD>
	<body leftMargin="0" background="image/coolwp2.jpg" MS_POSITIONING="GridLayout">
		<P>
			<TABLE id="tblStorageMenu" cellSpacing="1" cellPadding="1" width="136" border="0" align="left"
				runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trPoStock" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmPoStock.aspx'"
							href="javascript:">采购计划</A></TD>
				</TR>
				<TR id="trPoStockEnter" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbStock.aspx'"
							href="javascript:">采购入库</A></TD>
				</TR>
				<TR id="trPoStockReturn" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmPoStockReturn.aspx'"
							href="javascript:">采购退货</A></TD>
				</TR>
				<TR id="trCostAccount" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmCostAccount.aspx'"
							href="javascript:">成本核算</A></TD>
				</TR>
				<TR id="trSaleDailyCheck" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmSaleDailyCheck.aspx'"
							href="javascript:">库存盘点</A></TD>
				</TR>
				<TR id="trSaleDailyClear" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmSaleDailyClear.aspx'"
							href="javascript:">售完成品清零盘点</A></TD>
				</TR>				
				<TR id="trStorageAlarm" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmStorageAlarm.aspx'"
							href="javascript:">库存报警</A></TD>
				</TR>
				<TR id="trInventoryMove" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmInventoryMove.aspx'"
							href="javascript:">调拨</A></TD>
				</TR>
				<TR id="trDeptStorageEnter" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmDeptStorageEnter.aspx'"
							href="javascript:">分货入库</A></TD>
				</TR>
				<TR id="trDeptInventoryLost" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmDeptInventoryLost.aspx'"
							href="javascript:">过期报损</A></TD>
				</TR>
				<TR id="trInventotyReStop" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmInventotyReStop.aspx'"
							href="javascript:">库存解冻</A></TD>
				</TR>				
			</TABLE>
		</P>
	</body>
</HTML>
