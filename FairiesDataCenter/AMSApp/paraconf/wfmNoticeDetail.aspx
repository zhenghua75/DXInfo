<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmNoticeDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmNoticeDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ϵͳ֪ͨ
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">ϵͳ֪ͨ���</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 101px" align="right">
									<asp:label id="Label2" runat="server" Width="56px" Font-Size="10pt">�����ŵ�</asp:label></TD>
								<TD style="WIDTH: 538px">
									<asp:dropdownlist id="ddlDept" runat="server" Width="248px" Font-Size="10pt" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 101px; HEIGHT: 261px" align="right">
									<asp:label id="Label1" runat="server" Width="88px" Font-Size="10pt">֪ͨ����</asp:label></TD>
								<TD style="WIDTH: 127px; HEIGHT: 261px">
									<asp:textbox id="txtContent" runat="server" Width="745px" Font-Size="12pt" Height="208px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<td></td>
								<TD align="center">
									<asp:button id="btAdd" runat="server" Width="64px" Text="���"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btcancel" runat="server" Width="101px" Text="����֪ͨ����"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
</asp:Content>
