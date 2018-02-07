<%@ Page language="c#" Codebehind="wfmPoStockAdd.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPoStockAdd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPoStockAdd</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">采购计划内容</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="0" cellSpacing="10" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">采购计划号</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtPoID" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 112px"></TD>
					<TD style="WIDTH: 103px; FONT-SIZE: 10pt" align="right">供应商</TD>
					<TD>
						<asp:DropDownList style="Z-INDEX: 0" id="ddlProvider" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">采购周期</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtPlanCycle" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 112px; COLOR: blue; FONT-SIZE: 10pt">格式如“201003”</TD>
					<TD style="WIDTH: 103px; FONT-SIZE: 10pt" align="right"></TD>
					<TD style="WIDTH: 90px" align="center"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; HEIGHT: 39px; FONT-SIZE: 10pt" align="right">到货地址</TD>
					<TD colSpan="4" style="HEIGHT: 39px">
						<asp:TextBox id="txtAddress" runat="server" Height="27px" Width="464px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">备注</TD>
					<TD colSpan="4">
						<asp:TextBox id="txtComments" runat="server" Height="64px" Width="464px" Font-Size="10pt" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加" style="Z-INDEX: 0"></asp:Button></TD>
					<TD style="WIDTH: 112px" align="center">
						<asp:Button style="Z-INDEX: 0" id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:Button></TD>
					<TD style="WIDTH: 103px" align="center">
						<asp:Button style="Z-INDEX: 0" id="btChState" runat="server" Width="70px" Font-Size="10pt"></asp:Button></TD>
					<TD align="center">
						<INPUT type="button" style="CURSOR:hand" value="返 回" onClick="javascript:window.history.go(-1);"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
