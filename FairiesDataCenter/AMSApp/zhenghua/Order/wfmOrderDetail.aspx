<%@ Page language="c#" Codebehind="wfmOrderDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrderDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOrderDetail</title>
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
					<td style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">
						<asp:Label id="Label1" runat="server">����ϸ��</asp:Label></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="ɾ��" HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
