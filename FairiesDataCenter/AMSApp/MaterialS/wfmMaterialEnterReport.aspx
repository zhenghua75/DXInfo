<%@ Page language="c#" Codebehind="wfmMaterialEnterReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialEnterReport" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialEnterReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">原材料入库报表</TD>
				</TR>
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0" style="FONT-SIZE: 10pt">
							<TR>
								<TD style="WIDTH: 150px" align="right">
									<asp:label id="Label3" runat="server" Width="56px" Font-Size="10pt">统计类型</asp:label></TD>
								<TD style="WIDTH: 127px">
									<asp:DropDownList id="ddlQueryType" runat="server" Width="191px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right">
									<asp:label id="Label2" runat="server" Width="32px" Font-Size="10pt">月份</asp:label></TD>
								<td style="WIDTH: 257px">
									<asp:DropDownList id="ddlMonth" runat="server" Width="120px" AutoPostBack="False"></asp:DropDownList></td>
								<TD><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right">
									<asp:label id="Label1" runat="server" Width="55px" Font-Size="10pt">供应商</asp:label></TD>
								<TD style="WIDTH: 127px">
									<asp:DropDownList id="ddlProviderName" runat="server" Width="191px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right"></TD>
								<td style="WIDTH: 257px"></td>
								<TD style="WIDTH: 122px">
									<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="64px" Text="导出"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right"><asp:label id="Label5" runat="server" Width="72px" Font-Size="10pt">原材料类型</asp:label></TD>
								<TD style="WIDTH: 127px">
									<asp:DropDownList id="ddlMaterialType" runat="server" Width="192px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right">
									<asp:label id="Label6" runat="server" Width="72px" Font-Size="10pt">原材料名称</asp:label></TD>
								<TD style="WIDTH: 257px">
									<asp:TextBox id="txtMaterialName" runat="server" Width="184px"></asp:TextBox></TD>
								<TD style="WIDTH: 122px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="110%" border="0">
				<TR>
					<TD align="left" width="206" style="WIDTH: 206px" nowrap><FONT style="FONT-SIZE: 10pt; COLOR: #000099" face="宋体">查询记录数：</FONT>
						<asp:TextBox id="txtRecordCount" runat="server" Width="112px" ReadOnly="True"></asp:TextBox></TD>
					<TD align="left" width="188" style="WIDTH: 188px" nowrap><FONT style="FONT-SIZE: 10pt; COLOR: #000099" face="宋体">数量合计：</FONT>
						<asp:TextBox id="txtToltalCount" runat="server" Width="112px" ReadOnly="True"></asp:TextBox></TD>
					<TD align="left" width="188" style="WIDTH: 188px" nowrap><FONT style="FONT-SIZE: 10pt; COLOR: #000099" face="宋体">金额合计：</FONT>
						<asp:TextBox id="txtTotalFee" runat="server" Width="112px" ReadOnly="True"></asp:TextBox></TD>
					<TD width="50%"></TD>
				</TR>
				<TR>
					<TD colspan="4" align="center" style="FONT-SIZE: 10pt">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
