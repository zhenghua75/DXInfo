<%@ Page language="c#" Codebehind="wfmEmpMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmEmpMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmEmpMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblEmpMenu" cellSpacing="1" cellPadding="1" width="136" border="0" align="left"
			runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trEmpInfo" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmEmpInfo.aspx'"
						href="javascript:">员工信息维护</A></TD>
			</TR>
			<TR id="trWorkDailyEvery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmWorkDailyEvery.aspx'"
						href="javascript:">排班操作</A></TD>
			</TR>
			<TR id="trShcQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmShcQuery.aspx'"
						href="javascript:">查询排班表</A></TD>
			</TR>
			<TR id="trSignCalc" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmSignCalc.aspx'"
						href="javascript:">考勤计算</A></TD>
			</TR>
			<TR id="trEmpUnitSign" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmEmpUnitSign.aspx'"
						href="javascript:">个人考勤查询</A></TD>
			</TR>
			<TR id="trSignQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Employ/wfmSignQuery.aspx'"
						href="javascript:">门店考勤统计</A></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
