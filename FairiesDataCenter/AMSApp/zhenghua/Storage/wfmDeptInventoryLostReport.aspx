<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmDeptInventoryLostReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmDeptInventoryLostReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptInventoryLostReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">中心损耗统计</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 73px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="56px" Font-Size="10pt" Height="14px">损耗类型</asp:label></TD>
								<TD style="WIDTH: 102px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlLostType" runat="server" Width="96px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 58px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="35px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 167px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlDept" runat="server" Width="152px" Height="22px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 83px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="65px" Font-Size="10pt" Height="14px">开始时间</asp:label></TD>
								<TD style="WIDTH: 150px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></TD>
								<TD style="WIDTH: 107px" align="left">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<td></td>
								<td></td>
							</TR>
							<TR>
								<TD style="WIDTH: 73px" align="right"></TD>
								<TD style="WIDTH: 102px" align="left"></TD>
								<TD style="WIDTH: 58px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="29px" Font-Size="10pt" Height="14px">仓库</asp:label></TD>
								<TD style="WIDTH: 167px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlWhCode" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 83px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="56px" Font-Size="10pt" Height="14px">结束时间</asp:label></TD>
								<TD style="WIDTH: 150px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></TD>
								<TD style="WIDTH: 107px" align="left">
									<asp:Button style="Z-INDEX: 0" id="btExcel" runat="server" Height="23px" Width="48px" Text="导出"></asp:Button></TD>
								<td></td>
								<td style="WIDTH: 99px"></td>
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
