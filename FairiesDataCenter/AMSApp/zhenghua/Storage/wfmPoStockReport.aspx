<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmPoStockReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmPoStockReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPoStockReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">采购报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 93px" align="right">
									<asp:label id="Label2" runat="server" Width="65px" Font-Size="10pt" Height="14px">采购周期</asp:label></TD>
								<TD style="WIDTH: 126px">
									<asp:textbox style="Z-INDEX: 0" id="txtCycle" runat="server" Width="120px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
								<TD style="WIDTH: 116px; COLOR: blue; FONT-SIZE: 10pt" align="left">
									格式如“201003”</TD>
								<TD style="WIDTH: 54px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="43px" Font-Size="10pt" Height="14px">供应商</asp:label></TD>
								<TD style="WIDTH: 177px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlProvider" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 89px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 79px">
									<asp:Button style="Z-INDEX: 0" id="btExcel" runat="server" Height="23px" Width="48px" Text="导出"></asp:Button></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
