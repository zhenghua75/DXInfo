<%@ Page language="c#" Codebehind="wfmSaleDailyChart.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.Report.wfmSaleDailyChart" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleDailyChart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 29px; POSITION: absolute; TOP: 9px" cellSpacing="3"
				cellPadding="3" width="95%" align="center" border="0">
				<TR>
					<TD align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033">各分店销售额日走势（万元）</TD>
				</TR>
			</TABLE>
			<TABLE class="table_content_group" id="Table2" style="FONT-SIZE: 10pt; Z-INDEX: 102; LEFT: 29px; POSITION: absolute; TOP: 46px"
				cellspacing="0" cellpadding="0" width="95%" border="1">
				<TR>
					<TD noWrap style="WIDTH: 110px" align="right">月份</TD>
					<TD style="HEIGHT: 29px"><asp:dropdownlist id="ddlAcctMonth" runat="server" Width="112px"></asp:dropdownlist></TD>
					<TD style="WIDTH: 143px; HEIGHT: 29px" noWrap align="right">Y轴标尺</TD>
					<TD style="HEIGHT: 29px" width="40%"><asp:dropdownlist id="ddlYAXis" runat="server" Width="88px">
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3" Selected="True">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5">5</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD style="HEIGHT: 29px" width="40%">
						<P></P>
						<P><INPUT id="btnOk" type="button" value="查  询" name="btnOk" runat="server"></P>
					</TD>
					<TD style="HEIGHT: 29px" width="10%"><FONT face="宋体"></FONT></TD>
				</TR>
			</TABLE>
			<asp:image id="Image1" style="Z-INDEX: 103; LEFT: 32px; POSITION: absolute; TOP: 87px" runat="server"></asp:image></form>
	</body>
</HTML>
