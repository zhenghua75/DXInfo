<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmBillOfEnterStorage.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfEnterStorage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfEnterStorage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">原材料进仓</TD>
				</TR>
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"><asp:label id="Label1" runat="server" Width="40px" Font-Size="10pt">供应商</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:DropDownList id="ddlProvider" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt" Width="55px">开始时间</asp:label></TD>
								<TD style="WIDTH: 168px"><FONT face="宋体"><INPUT id=txtBegin onfocus=HS_setDate(this) 
            readOnly type=text size=13 value="<%=strBeginDate%>" 
            name=txtBegin style="WIDTH: 112px; HEIGHT: 22px"></FONT></TD>
								<td style="WIDTH: 23px"></td>
								<TD style="WIDTH: 78px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD><asp:button id="btnAdd" runat="server" Width="80px" Text="新进仓单"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 124px"></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label3" runat="server" Font-Size="10pt" Width="55px">结束时间</asp:label></TD>
								<TD style="WIDTH: 168px"><FONT face="宋体"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly 
            type=text size=13 value="<%=strEndDate%>" name=txtEnd></FONT></TD>
								<td style="WIDTH: 23px"></td>
								<TD style="WIDTH: 78px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
