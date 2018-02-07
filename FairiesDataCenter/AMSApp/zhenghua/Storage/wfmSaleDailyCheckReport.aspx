<%@ Page language="c#" Codebehind="wfmSaleDailyCheckReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmSaleDailyCheckReport" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleDailyCheckReport</title>
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
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">库存盘点查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 72px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="35px" Font-Size="10pt" Height="14px">部门</asp:label></TD>
								<TD style="WIDTH: 143px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlDept" runat="server" Width="136px" Height="22px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 67px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="33px" Font-Size="10pt" Height="14px">仓库</asp:label></TD>
								<TD style="WIDTH: 136px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlWhCode" runat="server" Width="128px" Height="22px"></asp:DropDownList></TD>
								<td style="WIDTH: 66px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label7" runat="server" Width="56px" Font-Size="10pt" Height="14px">确认标志</asp:label></td>
								<td style="WIDTH: 98px">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlFlag" runat="server" Width="88px" Height="22px"></asp:DropDownList></td>
								<TD style="WIDTH: 70px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="65px" Font-Size="10pt" Height="14px">开始时间</asp:label></TD>
								<TD style="WIDTH: 155px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=13 
            name=txtBegin></TD>
								<TD>
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 72px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="59px" Font-Size="10pt" Height="14px">存货编码</asp:label></TD>
								<TD style="WIDTH: 143px" align="left">
									<asp:TextBox style="Z-INDEX: 0" id="txtInvCode" runat="server" Width="136px" Font-Size="10pt"
										Height="24px"></asp:TextBox></TD>
								<TD style="WIDTH: 67px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Width="59px" Font-Size="10pt" Height="14px">存货名称</asp:label></TD>
								<TD style="WIDTH: 136px" align="left">
									<asp:TextBox style="Z-INDEX: 0" id="txtInvName" runat="server" Width="128px" Font-Size="10pt"
										Height="24px"></asp:TextBox></TD>
								<td style="WIDTH: 66px"></td>
								<td style="WIDTH: 98px"></td>
								<TD style="WIDTH: 70px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="56px" Font-Size="10pt" Height="14px">结束时间</asp:label></TD>
								<TD style="WIDTH: 155px" align="left"><INPUT 
            style="Z-INDEX: 0; WIDTH: 112px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=13 
            name=txtEnd></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="btExcel" runat="server" Height="23px" Width="48px" Text="导出"></asp:Button></TD>
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
