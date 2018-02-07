<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmReceiveMaterialReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.Report.wfmReceiveMaterialReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmReceiveMaterialReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">原材料领用报表</TD>
				</TR>
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0" style="FONT-SIZE: 10pt">
							<TR>
								<TD style="WIDTH: 150px; HEIGHT: 29px" align="right">
									<asp:label id="Label3" runat="server" Width="56px" Font-Size="10pt">统计类型</asp:label></TD>
								<TD style="WIDTH: 127px; HEIGHT: 29px">
									<asp:DropDownList id="ddlQueryType" runat="server" Width="191px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px; HEIGHT: 29px"></TD>
								<TD style="WIDTH: 96px; HEIGHT: 29px" align="right">
									<asp:label id="Label2" runat="server" Width="32px" Font-Size="10pt">月份</asp:label></TD>
								<td style="WIDTH: 193px; HEIGHT: 29px">
									<asp:DropDownList id="ddlMonth" runat="server" Width="104px" AutoPostBack="False"></asp:DropDownList></td>
								<TD style="HEIGHT: 29px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="HEIGHT: 29px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right">
									<asp:label id="Label1" runat="server" Width="55px" Font-Size="10pt">领料单位</asp:label></TD>
								<TD style="WIDTH: 127px">
									<asp:DropDownList id="ddlDeptID" runat="server" Width="191px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right"><FONT face="宋体">
										<asp:label id="Label4" runat="server" Font-Size="10pt" Width="55px">生产组</asp:label></FONT></TD>
								<td style="WIDTH: 193px">
									<asp:DropDownList id="ddlGroup" runat="server" Width="191px" AutoPostBack="False"></asp:DropDownList></td>
								<TD>
									<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="64px" Text="导出"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right"><asp:label id="Label5" runat="server" Width="72px" Font-Size="10pt">原材料类型</asp:label></TD>
								<TD style="WIDTH: 127px">
									<asp:DropDownList id="ddlMaterialType" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 53px"></TD>
								<TD style="WIDTH: 96px" align="right">
									<asp:label id="Label6" runat="server" Width="72px" Font-Size="10pt">原材料名称</asp:label></TD>
								<TD style="WIDTH: 193px">
									<asp:DropDownList id="ddlMaterilName" runat="server" Width="192px" AutoPostBack="False"></asp:DropDownList></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="110%" border="0">
				<TR>
					<TD align="center" style="FONT-SIZE: 10pt">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
