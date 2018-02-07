<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmStorageCheckLog.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.Report.wfmStorageCheckLog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageCheckLog</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
	</HEAD>
	<body bgColor=#feeff8 onload="<%=strExcelPath%>" 
MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">盘点日志查询</TD>
				</TR>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" style="FONT-SIZE: 10pt" cellSpacing="0" cellPadding="1" width="100%"
							border="0">
							<TR>
								<TD style="WIDTH: 150px; HEIGHT: 29px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="56px">开始时间</asp:label></TD>
								<TD style="WIDTH: 127px; HEIGHT: 29px"><INPUT 
            id=txtBegin style="WIDTH: 160px; HEIGHT: 22px" 
            onfocus=HS_setDate(this) readOnly type=text size=21 
            value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD style="WIDTH: 53px; HEIGHT: 29px"></TD>
								<TD style="WIDTH: 96px; HEIGHT: 29px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="56px">结束时间</asp:label></TD>
								<td style="WIDTH: 193px; HEIGHT: 29px"><INPUT 
            id=txtEnd style="WIDTH: 160px; HEIGHT: 22px" 
            onfocus=HS_setDate(this) readOnly type=text size=21 
            value="<%=strEndDate%>" name=txtEnd></td>
								<TD style="HEIGHT: 29px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="HEIGHT: 29px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="72px">部门</asp:label></TD>
								<TD style="WIDTH: 127px"><FONT face="宋体"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></FONT></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right"><FONT face="宋体"></FONT></TD>
								<td style="WIDTH: 193px"></td>
								<TD><asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="64px" Text="导出"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt" Width="72px">产品类型</asp:label></TD>
								<TD style="WIDTH: 127px"><asp:dropdownlist id="ddlProductType" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt" Width="72px">产品类别</asp:label></TD>
								<TD style="WIDTH: 193px"><asp:dropdownlist id="ddlProductClass" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt" align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
