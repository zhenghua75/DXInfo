<%@ Page language="c#" Codebehind="wfmWareHouse.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmWareHouse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ֿ⵵��</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<script type="text/javascript">
			function OpenWarehouseWin(flag,whcode)
			{
				window.showModalDialog("wfmAddWarehouse.aspx?flag="+flag+"&whcode="+whcode,window,"center:yes;dialogWidth:400px;dialogHeight:600px;") ;
				
					window.location.href ="wfmWarehouse.aspx";
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�ֿ⵵��</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderWidth="1px" BorderColor="Black"
								AutoGenerateColumns="False" AllowPaging="True" Width="1200">
								<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
								<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
								<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
								<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
								<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="cnvcWhCode" HeaderText="�ֿ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhName" HeaderText="�ֿ�����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcDepCode" HeaderText="��������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhAddress" HeaderText="�ֿ��ַ"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhPhone" HeaderText="�绰"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhPerson" HeaderText="������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhValueStyle" HeaderText="�Ƽ۷�ʽ"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhMemo" HeaderText="��ע"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="�Ƿ񶳽�">
										<ItemTemplate>
											<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbFreeze") %>></asp:CheckBox>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbFreeze") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cnnFrequency" HeaderText="�̵�����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcFrequency" HeaderText="�̵����ڵ�λ"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndLastDate" HeaderText="�ϴ��̵�����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnWHProperty" HeaderText="�ֿ�����"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="�Ƿ��ŵ�">
										<ItemTemplate>
											<asp:CheckBox id="CheckBox2" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbShop") %>></asp:CheckBox>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbShop") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td><asp:button id="Button1" runat="server" Text="��Ӳֿ⵵��"></asp:button></td>
				</tr>
				<tr>
					<td><asp:button id="Button2" runat="server" Text="�޸Ĳֿ⵵��"></asp:button></td>
				</tr>
				<tr>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
