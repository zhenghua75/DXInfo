<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmMaterialModReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialModReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialModReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">原材料入出库修正清单</TD>
				</TR>
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0" style="FONT-SIZE: 10pt">
							<TR>
								<TD style="WIDTH: 150px" align="right">
									<asp:label id="Label3" runat="server" Width="102px" Font-Size="10pt">入出库类型</asp:label></TD>
								<TD style="WIDTH: 146px">
									<asp:DropDownList id="ddlEnterOutType" runat="server" Width="112px"></asp:DropDownList></TD>
								<TD style="WIDTH: 141px" align="right"></TD>
								<TD style="WIDTH: 191px" align="left"></TD>
								<TD style="WIDTH: 58px"></TD>
								<TD style="WIDTH: 106px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" align="right">
									<asp:label id="Label1" runat="server" Width="102px" Font-Size="10pt">操作开始时间</asp:label></TD>
								<TD style="WIDTH: 146px"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD style="WIDTH: 141px" align="right">
									<asp:label id="Label4" runat="server" Width="90px" Font-Size="10pt">操作结束时间</asp:label></TD>
								<TD style="WIDTH: 191px" align="left"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD style="WIDTH: 58px"></TD>
								<TD style="WIDTH: 106px">
									<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="64px" Text="导出"></asp:button></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt" align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
