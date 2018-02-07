<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmStorageHisReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmStorageHisReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageHisReport</title>
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
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">历史库存</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 78px" align="right">
									<asp:label id="Label2" runat="server" Width="42px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 174px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlDept" runat="server" Width="152px" Height="22px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 71px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="49px" Font-Size="10pt" Height="14px">仓库</asp:label></TD>
								<TD style="WIDTH: 171px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 66px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="56px" Font-Size="10pt" Height="14px">开始时间</asp:label></TD>
								<TD style="WIDTH: 142px; COLOR: red; FONT-SIZE: 10pt"><INPUT style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></TD>
								<TD style="WIDTH: 71px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 78px" align="right">
									<asp:label id="Label4" runat="server" Width="55px" Font-Size="10pt" Height="14px">存货类型</asp:label></TD>
								<TD style="WIDTH: 174px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlProType" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 71px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Width="56px" Font-Size="10pt" Height="15px">存货名称</asp:label></TD>
								<TD style="WIDTH: 171px" align="left">
									<asp:TextBox style="Z-INDEX: 0" id="txtInvName" runat="server" Font-Size="10pt"></asp:TextBox></TD>
								<TD style="WIDTH: 66px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="56px" Font-Size="10pt" Height="14px">结束时间</asp:label></TD>
								<TD style="WIDTH: 142px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></TD>
								<TD style="WIDTH: 71px">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="56px" Height="23px" Text="查询"></asp:button></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="btExcel" runat="server" Width="48px" Height="23px" Text="导出"></asp:Button></TD>
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
