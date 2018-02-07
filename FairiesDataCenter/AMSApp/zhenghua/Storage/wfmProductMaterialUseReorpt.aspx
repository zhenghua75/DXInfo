<%@ Page language="c#" Codebehind="wfmProductMaterialUseReorpt.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmProductMaterialUseReorpt" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProductMaterialUseReorpt</title>
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
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">原材料生产使用报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 73px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="35px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 102px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlDept" runat="server" Width="152px" Height="22px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 113px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="65px" Font-Size="10pt" Height="14px">开始时间</asp:label></TD>
								<TD style="WIDTH: 182px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></TD>
								<TD style="WIDTH: 83px" align="right"></TD>
								<TD style="WIDTH: 150px" align="left">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 107px" align="left"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 73px" align="right"></TD>
								<TD style="WIDTH: 102px" align="left"></TD>
								<TD style="WIDTH: 113px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="56px" Font-Size="10pt" Height="14px">结束时间</asp:label></TD>
								<TD style="WIDTH: 182px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></TD>
								<TD style="WIDTH: 83px" align="right"></TD>
								<TD style="WIDTH: 150px" align="left"></TD>
								<TD style="WIDTH: 107px" align="right"></TD>
								<TD></TD>
								<TD style="WIDTH: 99px"></TD>
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
