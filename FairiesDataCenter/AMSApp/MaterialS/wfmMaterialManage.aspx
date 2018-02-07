<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmMaterialManage.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialManage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor=#feeff8 onload="<%=strExcelPath%>" 
MS_POSITIONING="GridLayout">
		<FONT face="宋体">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">原材料配置管理</TD>
					</TR>
				</TABLE>
				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 42px" align="right"><asp:label id="Label4" runat="server" Font-Size="10pt" Width="80px">原材料批次</asp:label></TD>
									<TD style="WIDTH: 127px"><asp:textbox id="txtBatchNo" runat="server" Font-Size="10pt" Width="120px" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 42px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="80px">原材料编号</asp:label></TD>
									<TD style="WIDTH: 119px"><asp:textbox id="txtMaterialCode" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 53px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="79px">原材料名称</asp:label></TD>
									<TD style="WIDTH: 118px"><asp:textbox id="txtMaterialName" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 53px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="79px">原材料类型</asp:label></TD>
									<TD style="WIDTH: 118px"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="151px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 86px"><asp:button id="Button1" runat="server" Width="64px" Text="查询"></asp:button></TD>
									<TD><asp:button id="Button2" runat="server" Width="64px" Text="添加"></asp:button></TD>
									<TD><FONT face="宋体"><asp:button id="btnExcel" runat="server" Width="64px" Text="导出"></asp:button></FONT></TD>
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
		</FONT>
	</body>
</HTML>
