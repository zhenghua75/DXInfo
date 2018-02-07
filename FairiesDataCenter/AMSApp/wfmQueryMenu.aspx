<%@ Page language="c#" Codebehind="wfmQueryMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmQueryMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmQueryMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblQueryMenu" cellSpacing="1" cellPadding="1" width="136" border="0" align="left"
			runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trAssInfo" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmAssInfo.aspx'"
						href="javascript:">会员资料查询</A></TD>
			</TR>
			<TR id="trConsItem" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmConsItem.aspx'"
						href="javascript:">消费统计查询</A></TD>
			</TR>
			<TR valign="middle" id="trFillQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmFillQuery.aspx'"
						href="javascript:">会员充值查询</A></TD>
			</TR>
			<TR valign="middle" id="trConsKindQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmConsKindQuery.aspx'"
						href="javascript:">消费分类统计</A></TD>
			</TR>
			<TR valign="middle" id="trBusiLogQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmBusiLogQuery.aspx'"
						href="javascript:">操作员日志</A></TD>
			</TR>
			<TR valign="middle" id="trTopQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmTopQuery.aspx'"
						href="javascript:">销售排名榜</A></TD>
			</TR>
			<TR valign="middle" id="trBusiIncome" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmBusiIncome.aspx'"
						href="javascript:">业务量统计</A></TD>
			</TR>
			<TR valign="middle" id="trSalesChart" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Lj/Report/wfmSalesChart.aspx'"
						href="javascript:">销售曲线图</A></TD>
			</TR>
			<TR valign="middle" id="trDailyCashQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmDailyCashQuery.aspx'"
						href="javascript:">当日收银</A></TD>
			</TR>
			<TR valign="middle" id="trSpecConsQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='BusiQuery/wfmSpecConsQuery.aspx'"
						href="javascript:">特殊消费统计</A></TD>
			</TR>		
			<TR valign="middle" id="trProductClassQuery" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/produce/wfmProductClassQuery.aspx'"
						href="javascript:">大类类别统计</A></TD>
			</TR>	
			<TR valign="middle" id="trSellReport" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/produce/wfmSellReport.aspx'"
						href="javascript:">销售分析报表</A></TD>
			</TR>		
		</TABLE>
	</body>
</HTML>
