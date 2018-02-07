<%@ Page language="c#" Codebehind="wfmMaterialReportMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmMaterialReportMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialReportMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblMaterialReportMenu" cellSpacing="1" cellPadding="1" width="146" border="0"
			align="left" runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trMaterialEnterReport" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialEnterReport.aspx'"
						href="javascript:">材料入库报表</A></TD>
			</TR>
			<TR id="trMaterialOutReport" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialOutReport.aspx'"
						href="javascript:">材料出库报表</A></TD>
			</TR>
			<TR id="trMaterialModReport" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 11pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialModReport.aspx'"
						href="javascript:">材料入出库修正清单</A></TD>
			</TR>
			<TR id="trMaterialStorageReport" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialStorageReport.aspx'"
						href="javascript:">材料库存报表</A></TD>
			</TR>
			<TR id="trMaterialSimpleAnalyse" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialSimpleAnalyse.aspx'"
						href="javascript:">材料分析报表</A></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
