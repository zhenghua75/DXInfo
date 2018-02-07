<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmCostAccount.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmCostAccount" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmCostAccount</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">成本核算</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="60px" Height="2px">查询类型</asp:label></TD>
								<TD style="WIDTH: 178px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlQueryType" runat="server" Font-Size="10pt" Width="160px"
										Height="22px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 82px" align="right"></TD>
								<TD style="WIDTH: 161px" align="left"></TD>
								<TD style="WIDTH: 98px" align="right"></TD>
								<TD style="WIDTH: 94px">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 141px">
									<asp:button style="Z-INDEX: 0" id="btnInvAccount" runat="server" Width="112px" Text="原材料成本核算"></asp:button></TD>
								<TD>
									<asp:button style="Z-INDEX: 0" id="btProductAccount" runat="server" Width="96px" Text="成品成本核算"></asp:button></TD>
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
