<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmWorkDailyEvery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmWorkDailyEvery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Ա���չ������żƻ�
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">Ա���չ������żƻ�</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" align="center" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 70px" align="right">
									<asp:label id="Label1" runat="server" Width="60px" Font-Size="10pt">�ŵ�</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:dropdownlist id="ddlDept" runat="server" Width="144px" Font-Size="10pt" Height="24px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 143px" align="right">
									<asp:label id="Label4" runat="server" Width="82px" Font-Size="10pt">Ҫ�Ű������</asp:label></TD>
								<TD style="WIDTH: 227px" align="left"><INPUT id=txtSchDate onfocus=HS_setDate(this) readOnly type=text size=17 value="<%=strSchDate%>" name=txtSchDate></TD>
								<TD style="WIDTH: 103px"><FONT face="����">
										<asp:button id="btquery" runat="server" Width="65px" Text="��ѯ" onclick="btquery_Click"></asp:button></FONT></TD>
								<TD style="WIDTH: 109px">
									<asp:button id="btnadd" runat="server" Width="65px" Text="���" onclick="btnadd_Click"></asp:button></TD>
								<TD style="WIDTH: 137px">
									<asp:button id="btnExport" runat="server" Width="65px" Text="����" onclick="btnExport_Click"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
</asp:Content>