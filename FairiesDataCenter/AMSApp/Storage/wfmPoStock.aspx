<%@ Page language="c#" Codebehind="wfmPoStock.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPoStock" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPoStock</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">采购计划</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 104px" align="right"><asp:label style="Z-INDEX: 0" id="Label2" runat="server" Font-Size="10pt" Width="72px">采购计划号</asp:label></TD>
								<TD style="WIDTH: 197px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtPoID" runat="server" Font-Size="10pt" Width="144px"></asp:textbox></TD>
								<TD style="WIDTH: 67px" align="right"><asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="40px">供应商</asp:label></TD>
								<TD style="WIDTH: 179px" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlProvider" runat="server" Font-Size="10pt" Width="160px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 85px" align="right"><asp:label style="Z-INDEX: 0" id="Label4" runat="server" Font-Size="10pt" Width="69px">订单状态</asp:label></TD>
								<TD style="WIDTH: 179px" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlPoState" runat="server" Font-Size="10pt" Width="96px"
										Height="22px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 104px" align="right"><asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Size="10pt" Width="65px" Height="15px">采购周期</asp:label></TD>
								<TD style="WIDTH: 197px; COLOR: blue; FONT-SIZE: 10pt" align="left"><asp:textbox style="Z-INDEX: 0" id="txtCycle" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>格式如“201003”</TD>
								<TD style="WIDTH: 67px" align="right"></TD>
								<TD style="WIDTH: 179px" align="left"></TD>
								<TD style="WIDTH: 85px" align="right"></TD>
								<TD style="WIDTH: 179px" align="left"></TD>
								<TD style="WIDTH: 98px"><asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD><asp:button style="Z-INDEX: 0" id="btnAdd" runat="server" Width="64px" Text="新计划"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
