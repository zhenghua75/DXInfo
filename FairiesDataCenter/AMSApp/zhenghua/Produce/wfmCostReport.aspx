<%@ Page language="c#" Codebehind="wfmCostReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmCostReport" %>
<%@ Register TagPrefix="uc2" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmCostReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">生产成本核算报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">

								<TR>
									<TD><asp:label id="Label3" runat="server" Font-Size="10pt" >部门：</asp:label></TD>
									<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
									<TD>
									<asp:label id="Label4" runat="server" Font-Size="10pt" >存货类型：</asp:label>
					</td>
					<TD>
						<asp:DropDownList id="ddlProType" runat="server"  AutoPostBack="True"></asp:DropDownList></TD>
					<TD>
						<asp:label id="Label5" runat="server"  Font-Size="10pt" >存货类别：</asp:label></TD>
					<TD>
						<asp:DropDownList id="ddlProClass" runat="server"></asp:DropDownList></TD>
					<TD align="center"><asp:label id="Label2" runat="server" Font-Size="10pt" >开始日期：</asp:label></TD>
					<td><asp:textbox class="Wdate" id="txtBeginDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
							runat="server"></asp:textbox></td>
					<TD align="center"><asp:label id="Label1" runat="server" Font-Size="10pt" >结束日期：</asp:label></TD>
					<td><asp:textbox class="Wdate" id="txtEndDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
							runat="server"></asp:textbox></td>
					<td>
						<asp:Button id="Button2" runat="server" Text="查询"></asp:Button></td>
					<td>
						<asp:Button id="Button1" runat="server" Text="导出"></asp:Button></td>
				</tr>
			</TABLE>
			</TD></TR>
			<tr vAlign="top">
				<td align="center">
				</td>
			</tr>
			</TBODY></TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc2:ucPageView id="UcPageView1" runat="server"></uc2:ucPageView></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
