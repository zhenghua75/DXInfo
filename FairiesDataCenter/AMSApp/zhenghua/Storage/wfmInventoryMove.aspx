<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmInventoryMove.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmInventoryMove" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmInventoryMove</title>
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
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">部门仓库调拨</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label2" runat="server" Width="65px" Font-Size="10pt" Height="14px">调拨部门</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:DropDownList id="ddlDept" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label5" runat="server" Width="55px" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 150px"><FONT face="宋体"><INPUT 
            style="WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></FONT></TD>
								<TD style="WIDTH: 55px"></TD>
								<TD style="WIDTH: 97px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD>
									<asp:button id="btnAdd" runat="server" Width="80px" Text="新调拨单"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="65px" Font-Size="10pt" Height="14px">调拨单号</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:textbox style="Z-INDEX: 0" id="txtCode" runat="server" Width="176px" Font-Size="10pt"></asp:textbox></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label3" runat="server" Width="55px" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 150px"><FONT face="宋体"><INPUT 
            style="WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></FONT></TD>
								<TD style="WIDTH: 55px"></TD>
								<TD style="WIDTH: 97px"></TD>
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
