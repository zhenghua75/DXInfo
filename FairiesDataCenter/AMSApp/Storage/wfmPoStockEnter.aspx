<%@ Page language="c#" Codebehind="wfmPoStockEnter.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPoStockEnter" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPoStockEnter</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">采购入库</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Font-Size="10pt" Width="84px">采购入库单号</asp:label></TD>
								<TD style="WIDTH: 155px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtPoEnterID" runat="server" Font-Size="10pt" Width="144px"></asp:textbox></TD>
								<TD style="WIDTH: 69px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="48px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 157px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Width="144px" Font-Size="10pt" Height="22px"
										AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Font-Size="10pt" Width="80px">入库开始时间</asp:label></TD>
								<TD style="WIDTH: 109px"><INPUT id=txtBegin onfocus=HS_setDate(this) readOnly size=10 value="<%=strBeginDate%>" name=txtBegin style="WIDTH: 96px; HEIGHT: 22px"></TD>
								<TD style="WIDTH: 91px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="72px" Font-Size="10pt" Height="14px">采购计划号</asp:label></TD>
								<TD>
									<asp:textbox style="Z-INDEX: 0" id="txtPoID" runat="server" Width="104px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="40px">仓库</asp:label></TD>
								<TD style="WIDTH: 155px; COLOR: blue; FONT-SIZE: 10pt" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 69px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Font-Size="10pt" Width="69px">单据状态</asp:label></TD>
								<TD style="WIDTH: 157px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlState" runat="server" Font-Size="10pt" Width="144px" Height="22px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label7" runat="server" Font-Size="10pt" Width="80px" Height="5px">入库结束时间</asp:label></TD>
								<TD style="WIDTH: 109px"><INPUT id=txtEnd style="WIDTH: 96px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly size=10 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD style="WIDTH: 91px">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD>
									<asp:button style="Z-INDEX: 0" id="btnAdd" runat="server" Width="64px" Text="新入库单"></asp:button></TD>
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
