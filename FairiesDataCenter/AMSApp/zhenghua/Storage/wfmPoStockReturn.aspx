<%@ Page language="c#" Codebehind="wfmPoStockReturn.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmPoStockReturn" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPoStockReturn</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">采购退货</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Font-Size="10pt" Width="84px">采购退货单号</asp:label></TD>
								<TD style="WIDTH: 162px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtPoEnterID" runat="server" Font-Size="10pt" Width="144px"></asp:textbox></TD>
								<TD style="WIDTH: 82px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Size="10pt" Width="40px">部门</asp:label></TD>
								<TD style="WIDTH: 161px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Font-Size="10pt" Width="144px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Font-Size="10pt" Width="80px">退货开始时间</asp:label></TD>
								<TD style="WIDTH: 132px"><INPUT style="WIDTH: 96px; HEIGHT: 22px" 
            id=txtBegin onfocus=HS_setDate(this) value="<%=strBeginDate%>" 
            readOnly size=10 name=txtBegin></TD>
								<TD style="WIDTH: 100px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="40px">仓库</asp:label></TD>
								<TD style="WIDTH: 162px; COLOR: blue; FONT-SIZE: 10pt" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 82px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="69px" Font-Size="10pt">单据状态</asp:label></TD>
								<TD style="WIDTH: 161px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlState" runat="server" Width="144px" Font-Size="10pt" Height="22px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label7" runat="server" Font-Size="10pt" Width="80px" Height="5px">退货结束时间</asp:label></TD>
								<TD style="WIDTH: 132px"><INPUT style="WIDTH: 96px; HEIGHT: 22px" 
            id=txtEnd onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=10 name=txtEnd></TD>
								<TD style="WIDTH: 100px">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD>
									<asp:button style="Z-INDEX: 0" id="btnAdd" runat="server" Width="64px" Text="新退货单"></asp:button></TD>
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
