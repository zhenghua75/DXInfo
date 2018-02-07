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
						<asp:Label id="Label11" runat="server" CssClass="title">添加原料材料</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="lable">原料编码</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">原料名称</asp:Label></td>
					<td>
						<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">计量单位</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlLeastUnit" runat="server"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">计量价格</asp:Label></td>
					<td>
						<asp:TextBox id="txtPrice" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label9" runat="server" CssClass="lable">产品类型</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProductType" runat="server" AutoPostBack="True"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label12" runat="server" CssClass="lable">产品类别</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProductClass" runat="server" AutoPostBack="True"></asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Label id="Label10" runat="server" CssClass="title">出仓单位转换</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">换算关系</asp:Label></td>
					<td>
						<asp:TextBox id="txtConversion" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label6" runat="server" CssClass="lable">出仓单位</asp:Label></td>
					<td>
						<asp:TextBox id="txtUnit" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label7" runat="server" CssClass="lable">规格单位</asp:Label></td>
					<td>
						<asp:TextBox id="txtStandardUnit" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label8" runat="server" CssClass="lable">规格数量</asp:Label></td>
					<td>
						<asp:TextBox id="txtStandardCount" runat="server" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Button id="btnAdd" runat="server" Text="确定" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="取消" CssClass="button"></asp:Button>
						<asp:Button id="btnReturn" runat="server" Text="返回" CssClass="button"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
