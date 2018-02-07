<%@ Page language="c#" Codebehind="wfmAddMaterial.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Formula.wfmAddMaterial" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmAddMaterial</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td colspan="2" align="center">
						<asp:Label id="Label11" runat="server" CssClass="title">���ԭ�ϲ���</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="lable">ԭ�ϱ���</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">ԭ������</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">������λ</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlLeastUnit" runat="server"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">�����۸�</asp:Label></td>
					<td>
						<asp:TextBox id="txtPrice" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label9" runat="server" CssClass="lable">��Ʒ����</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProductType" runat="server" AutoPostBack="True"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label12" runat="server" CssClass="lable">��Ʒ���</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProductClass" runat="server" AutoPostBack="True"></asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Label id="Label10" runat="server" CssClass="title">���ֵ�λת��</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">�����ϵ</asp:Label></td>
					<td>
						<asp:TextBox id="txtConversion" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label6" runat="server" CssClass="lable">���ֵ�λ</asp:Label></td>
					<td>
						<asp:TextBox id="txtUnit" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label7" runat="server" CssClass="lable">���λ</asp:Label></td>
					<td>
						<asp:TextBox id="txtStandardUnit" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label8" runat="server" CssClass="lable">�������</asp:Label></td>
					<td>
						<asp:TextBox id="txtStandardCount" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Button id="btnAdd" runat="server" Text="ȷ��" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button"></asp:Button>
						<asp:Button id="btnReturn" runat="server" Text="����" CssClass="button"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
