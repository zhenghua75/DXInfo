<%@ Page language="c#" Codebehind="wfmAddComputationUnit.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.ComputationUnit.wfmAddComputationUnit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������λ��</title>
		<base target="_self">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../scripts/datetime.js"></script>
		<script language="javascript" src="../scripts/calendar.js"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmAddComputationUnit";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onkeydown="if (event.keyCode==116){reload.click()}">
		<a id="reload" href="wfmAddComputationUnit.aspx" style="DISPLAY:none">reload...</a>
		<form id="Form1" method="post" runat="server" target="wfmAddComputationUnit">
			<table width="100%" height="100%">
				<tr>
					<td align="center" valign="middle">
						<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">������λ����</asp:label></td>
							</tr>
						</table>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable">������λ����룺</asp:Label></td>
								<td>
									<asp:TextBox id="TextBox2" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">������λ�����ƣ�</asp:Label></td>
								<td>
									<asp:TextBox id="TextBox1" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label3" runat="server" CssClass="lable">������λ���룺</asp:Label></td>
								<td>
									<asp:TextBox id="TextBox3" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable">������λ���ƣ�</asp:Label></td>
								<td>
									<asp:TextBox id="TextBox4" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">�Ƿ���������λ��</asp:Label></td>
								<td>
									<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label6" runat="server" CssClass="lable">�����ʣ�</asp:Label></td>
								<td>
									<asp:TextBox id="TextBox6" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr align="center">
								<td colspan="2">
									<asp:Button id="Button1" runat="server" Text="ȷ��" CssClass="button"></asp:Button>
									<INPUT type="button" value="ȡ��" class="button" onclick="window.returnValue='cccc';window.close()"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
