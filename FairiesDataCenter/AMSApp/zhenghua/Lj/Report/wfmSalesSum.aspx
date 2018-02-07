<%@ Register TagPrefix="uc2" TagName="ucPageView" Src="../../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmSalesSum.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Lj.Report.wfmSalesSum" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSalesSum</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../My97DatePicker/WdatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">销售入库汇总报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server">销售部门：</asp:label></TD>
								<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
								<TD align="center"><asp:label id="Label2" runat="server">开始日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtBeginDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server"></asp:textbox></td>
								<TD align="center"><asp:label id="Label1" runat="server">结束日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtEndDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server"></asp:textbox></td>
								<td>
									<asp:Button id="Button2" runat="server" Text="查询"></asp:Button></td>
								<td>
									<asp:Button id="Button1" runat="server" Text="导出"></asp:Button></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center">
					</td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc2:ucPageView id="UcPageView1" runat="server"></uc2:ucPageView></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
