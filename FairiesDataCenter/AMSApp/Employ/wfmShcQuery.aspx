<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmShcQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmShcQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Ա��ÿ���Ű��
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">Ա��ÿ���Ű��</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" align="center" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 224px" align="right"></TD>
								<TD style="WIDTH: 174px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt" Width="75px">�Ű�����</asp:label></TD>
								<TD style="WIDTH: 264px"><FONT face="����"><INPUT id=txtSchDate style="WIDTH: 136px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=17 value="<%=strSchDate%>" name=txtSchDate></FONT></TD>
								<TD style="WIDTH: 158px">
									<asp:button id="btquery" runat="server" Width="65px" Text="��ѯ" onclick="btquery_Click"></asp:button></TD>
								<TD style="WIDTH: 137px"></TD>
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