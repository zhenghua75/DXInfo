<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmStockPlan.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmStockPlan" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStockPlan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">ԭ���ϲɹ��ƻ�</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 268px; HEIGHT: 26px" align="right">
									<asp:label id="Label5" runat="server" Width="37px" Font-Size="10pt">�·�</asp:label></TD>
								<TD style="WIDTH: 158px; HEIGHT: 26px"><FONT face="����">
										<asp:DropDownList id="ddlMonth" runat="server" Width="176px"></asp:DropDownList></FONT></TD>
								<TD style="WIDTH: 72px; HEIGHT: 26px"></TD>
								<TD style="WIDTH: 139px; HEIGHT: 26px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="��ѯ"></asp:button></TD>
								<TD style="HEIGHT: 26px">
									<asp:button id="btnAdd" runat="server" Width="112px" Text="��Ӳ��żƻ�"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 268px" align="right"></TD>
								<TD style="WIDTH: 158px"><FONT face="����"></FONT></TD>
								<TD style="WIDTH: 72px"></TD>
								<TD style="WIDTH: 139px"></TD>
								<TD></TD>
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
		</FORM>
	</body>
</HTML>
