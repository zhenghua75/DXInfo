<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmBusiIncome.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmBusiIncome" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ҵ����ͳ�Ʊ���
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">ҵ����ͳ�Ʊ���</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">�ŵ�</asp:label></TD>
								<TD style="WIDTH: 156px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">��ʼ����</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtBegin onfocus=HS_setDate(this) readonly value="<%=strBeginDate%>" name=txtBegin style="WIDTH: 152px; HEIGHT: 22px"></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="��ѯ" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 171px" align="right"></TD>
								<TD style="WIDTH: 156px"></TD>
								<TD style="WIDTH: 81px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">��������</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtEnd onfocus=HS_setDate(this) readonly value="<%=strEndDate%>" name=txtEnd style="WIDTH: 152px; HEIGHT: 22px"></TD>
								<TD><FONT face="����">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="����"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD colspan="5" style="COLOR: #cc0000; FONT-SIZE: 10pt">ע��ԭ״̬�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ���л�Ա��ֹ����ʼ���ڵĻ��ֺ�����ܺ͡�<br>
									&nbsp;&nbsp;&nbsp; ��״̬�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ���л�Ա��ֹ���������ڵĻ��ֺ�����ܺ͡�<br>
									&nbsp;&nbsp;&nbsp; �����Ա�еĿ��û��ֺͽ�����ѯ�ŵ��޹أ�����ʾֵΪ��ѯ���ڷ�Χ�ڵ������Ա��ֹ���������ڵĻ��ֺ�����ܺ͡�
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
</asp:Content>