<%@ Page language="c#" Codebehind="wfmAddProducer.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmAddProducer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����Ա</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmAddProducer";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmAddProducer">
			<table width="100%" height="100%">
				<tr>
					<td align="center" valign="middle">
						<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">����Ա</asp:label></td>
							</tr>
						</table>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable" Visible="False">����Ա���룺</asp:Label></td>
								<td>
									<asp:TextBox id="txtProducerID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">����Ա������</asp:Label></td>
								<td>
									<asp:TextBox id="txtProducerName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<asp:Button id="Button1" runat="server" Text="ȷ��" CssClass="button"></asp:Button>
									<asp:Button id="Button2" runat="server" Text="ȡ��" CssClass="button"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
