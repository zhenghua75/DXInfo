<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmGoods.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmGoods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
��Ʒ����
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">��Ʒ����</TD>
				</TR>
				<!--
				<TR>
					<TD align="left" style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #3300ff; TEXT-DECORATION: underline">ע����Ʒ�������ɹ����벻ҪƵ��ʹ�ã�������ɲ������ң��뾡���ܰ�������ӻ��޸ĵ���Ʒ��Ϣ<Font style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #ff0000; TEXT-DECORATION: underline">ȫ���޸���Ϻ�</Font>����һ�ν�����Ʒ���������Ա���ȷ���·ֵ���Ʒ��</TD>
				</TR>
				-->
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 42px"><asp:label id="Label1" runat="server" Width="40px" Font-Size="10pt">��ƷID</asp:label></TD>
								<TD style="WIDTH: 127px"><asp:textbox id="txtGoodsID" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
								<TD style="WIDTH: 53px"><asp:label id="Label2" runat="server" Width="56px" Font-Size="10pt">��Ʒ����</asp:label></TD>
								<TD style="WIDTH: 118px"><FONT face="����"><asp:textbox id="txtGoodsName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></FONT></TD>
								<td style="WIDTH: 258px"><FONT face="����">�Ƿ����
										<asp:dropdownlist style="Z-INDEX: 0" id="ddlDiscount" runat="server" Font-Size="10pt" Width="144px"
											AutoPostBack="True"></asp:dropdownlist></FONT></td>
								<TD><asp:button id="Button1" runat="server" Width="64px" Text="��ѯ" onclick="Button1_Click"></asp:button></TD>
								<TD><asp:button id="Button2" runat="server" Width="64px" Text="���" onclick="Button2_Click"></asp:button></TD>
								<TD><FONT face="����">
										<asp:button id="btnExcel" runat="server" Width="64px" Text="����"></asp:button></FONT></TD>
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
</asp:Content>
