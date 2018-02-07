<%@ Page language="c#" Codebehind="wfmProvider.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProvider" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProvider</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">供应商档案</TD>
				</TR>
			</TABLE>
			<table border="1" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 69px"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="72px">供应商编码</asp:label></TD>
								<TD style="WIDTH: 127px"><asp:textbox id="txtProviderID" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 53px"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="72px">供应商名称</asp:label></TD>
								<TD style="WIDTH: 118px"><FONT face="宋体"><asp:textbox id="txtProviderName" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></FONT></TD>
								<td style="WIDTH: 258px"></td>
								<TD><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD><asp:button id="btnAdd" runat="server" Width="64px" Text="添加"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
