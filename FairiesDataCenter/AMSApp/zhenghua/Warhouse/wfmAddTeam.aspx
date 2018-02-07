<%@ Page language="c#" Codebehind="wfmAddTeam.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmAddTeam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>生产组</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmAddTeam";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmAddTeam">
			<table width="100%" height="100%">
				<tr>
					<td align="center" valign="middle">
						<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">生产组</asp:label></td>
							</tr>
						</table>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable" Visible="False">生产组编码：</asp:Label></td>
								<td>
									<asp:TextBox id="txtTeamID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">生产组名称</asp:Label></td>
								<td>
									<asp:TextBox id="txtTeamName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<asp:Button id="Button1" runat="server" Text="确定" CssClass="button"></asp:Button>
									<asp:Button id="Button2" runat="server" Text="取消" CssClass="button"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
