<%@ Page language="c#" Codebehind="wfmStorageReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmStorageReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageReport</title>
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
			<TABLE id="tblStorageReportMenu" cellSpacing="1" cellPadding="1" width="136" border="0"
				align="left" runat="server">
				<TR id="trnoprom" runat="server">
					<TD style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" align="center" bgColor="#ebf0ec">没有权限</TD>
				</TR>
				<TR id="trWHStorageRepot" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmWHStorageRepot.aspx'"
							href="javascript:">仓库库存查询</A></TD>
				</TR>
				<TR id="trPoStockReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmPoStockReport.aspx'"
							href="javascript:">采购报表</A></TD>
				</TR>
				<TR id="trPoStockReturnReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmPoStockReturnReport.aspx'"
							href="javascript:">采购退货报表</A></TD>
				</TR>
				<TR id="trInventoryMoveReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmInventoryMoveReport.aspx'"
							href="javascript:">调拨报表</A></TD>
				</TR>
				<TR id="trDeptStorageEnterReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmDeptStorageEnterReport.aspx'"
							href="javascript:">分货入库报表</A></TD>
				</TR>
				<TR id="trDeptInventoryLostReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmDeptInventoryLostReport.aspx'"
							href="javascript:">中心损耗统计</A></TD>
				</TR>
				<TR id="trSaleDailyCheckReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmSaleDailyCheckReport.aspx'"
							href="javascript:">库存盘点查询</A></TD>
				</TR>
				<TR id="trStorageBalanceReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmStorageBalanceReport.aspx'"
							href="javascript:">库存平衡关系表</A></TD>
				</TR>
				<TR id="trStorageHisReport" runat="server">
					<TD style="HEIGHT: 31px" align="center" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Storage/wfmStorageHisReport.aspx'"
							href="javascript:">历史库存</A></TD>
				</TR>				
			</TABLE>
		</P>
	</body>
</HTML>
