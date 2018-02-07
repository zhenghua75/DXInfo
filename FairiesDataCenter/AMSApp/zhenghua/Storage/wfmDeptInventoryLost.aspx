<%@ Page language="c#" Codebehind="wfmDeptInventoryLost.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmDeptInventoryLost" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptInventoryLost</title>
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
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">过期报损</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 28px" align="right"></TD>
								<TD style="WIDTH: 65px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="41px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 195px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlDept" runat="server" Width="176px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 90px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="55px" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 156px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></TD>
								<TD style="WIDTH: 111px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD>
									<asp:button id="btnAdd" runat="server" Width="80px" Text="新增损耗"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 28px"></TD>
								<TD style="WIDTH: 65px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="40px" Font-Size="10pt" Height="14px">仓库</asp:label></TD>
								<TD style="WIDTH: 195px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 90px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="55px" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 156px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></TD>
								<TD style="WIDTH: 111px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="100%">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
