<%@ Page language="c#" Codebehind="wfmWHStorageRepot.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmWHStorageRepot" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmWHStorageRepot</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">仓库库存统计</TD>
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
									<asp:CheckBox style="Z-INDEX: 0" id="chbSum" runat="server" Font-Size="10pt" Text="汇总"></asp:CheckBox></TD>
								<TD style="WIDTH: 171px; COLOR: red; FONT-SIZE: 10pt">不区分生产日期和过期日期</TD>
								<TD style="WIDTH: 71px">
									<asp:CheckBox style="Z-INDEX: 0" id="chkExp" runat="server" Font-Size="10pt" Text="过期"></asp:CheckBox></TD>
								<td></td>
							</TR>
							<TR>
								<TD style="WIDTH: 78px" align="right">
									<asp:label id="Label4" runat="server" Width="55px" Font-Size="10pt" Height="14px">存货类型</asp:label></TD>
								<TD style="WIDTH: 174px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlProType" runat="server" Width="152px" Height="22px" AutoPostBack="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 71px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="59px" Font-Size="10pt" Height="14px">存货类别</asp:label></TD>
								<TD style="WIDTH: 171px" align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlProClass" runat="server" Width="152px" Height="22px"></asp:DropDownList></TD>
								<TD style="WIDTH: 66px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Width="56px" Font-Size="10pt" Height="15px">存货名称</asp:label></TD>
								<TD style="WIDTH: 171px" align="left">
									<asp:TextBox style="Z-INDEX: 0" id="txtInvName" runat="server" Font-Size="10pt"></asp:TextBox></TD>
								<TD style="WIDTH: 71px">
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="56px" Text="查询" Height="23px"></asp:button></TD>
								<td>
									<asp:Button style="Z-INDEX: 0" id="btExcel" runat="server" Height="23px" Width="48px" Text="导出"></asp:Button></td>
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
