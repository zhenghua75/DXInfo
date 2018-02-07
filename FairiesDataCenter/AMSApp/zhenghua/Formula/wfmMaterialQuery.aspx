<%@ Page language="c#" Codebehind="wfmMaterialQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Formula.wfmMaterialQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialQuery</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">
						<asp:Label id="Label5" runat="server">���ԭ��</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server">��Ʒ���ͣ�</asp:label></td>
					<td><asp:dropdownlist id="ddlProductType" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server">��Ʒ���</asp:label></td>
					<td><asp:dropdownlist id="ddlProductClass" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server">��Ʒ���룺</asp:label></td>
					<td><asp:textbox id="txtProductCode" runat="server"></asp:textbox></td>
					<td><asp:label id="Label4" runat="server">��Ʒ���ƣ�</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnQuery" runat="server" Text="��ѯ" Width="60px"></asp:button><asp:button id="btnCancel" runat="server" Text="ȡ��" Width="60px"></asp:button><asp:button id="btnReturn" runat="server" Text="����" Width="60px"></asp:button></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966"
							BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="4" AllowPaging="True"
							PageSize="20">
							<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
							<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Button id="Button1" runat="server" Text="���" CommandName="Add"></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnvcProductType" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductClass" HeaderText="��Ʒ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProductTypeCode" ReadOnly="True" HeaderText="��Ʒ���ͱ���"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
