<%@ Page language="c#" Codebehind="wfmMaterialMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmMaterialMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblMaterialMenu" cellSpacing="1" cellPadding="1" width="146" border="0" align="left"
			runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="FONT-WEIGHT: bold; COLOR: #330033; HEIGHT: 38px" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trMaterialPara" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialManage.aspx'"
						href="javascript:">配置材料参数</A></TD>
			</TR>
			<TR id="trMaterialEnter" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialEnter.aspx'"
						href="javascript:">材料入库</A></TD>
			</TR>
			<TR id="trMaterialEnterMod" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialEnterMod.aspx'"
						href="javascript:">材料入库修正</A></TD>
			</TR>
			<TR id="trMaterialOut" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialOut.aspx'"
						href="javascript:">材料出库</A></TD>
			</TR>
			<TR id="trMaterialOutMod" runat="server">
				<TD align="center" style="HEIGHT: 38px" background="image/anniu.jpg"><A style="FONT-SIZE: 12pt; COLOR: #ffffff; TEXT-DECORATION: none" onclick="parent.right.location='MaterialS/wfmMaterialOutMod.aspx'"
						href="javascript:">材料出库修正</A></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
