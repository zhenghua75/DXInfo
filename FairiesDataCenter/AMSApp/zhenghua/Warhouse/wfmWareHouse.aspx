<%@ Page language="c#" Codebehind="wfmWareHouse.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmWareHouse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>仓库档案</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="title">仓库档案</asp:label></td>
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
									<asp:ButtonColumn Text="选择" CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="cnvcWhCode" HeaderText="仓库编码"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhName" HeaderText="仓库名称"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcDepCode" HeaderText="所属部门"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhAddress" HeaderText="仓库地址"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhPhone" HeaderText="电话"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhPerson" HeaderText="负责人"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhValueStyle" HeaderText="计价方式"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcWhMemo" HeaderText="备注"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="是否冻结">
										<ItemTemplate>
											<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbFreeze") %>></asp:CheckBox>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbFreeze") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cnnFrequency" HeaderText="盘点周期"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcFrequency" HeaderText="盘点周期单位"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndLastDate" HeaderText="上次盘点日期"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnWHProperty" HeaderText="仓库属性"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="是否门店">
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
					<td><asp:button id="Button1" runat="server" Text="添加仓库档案"></asp:button></td>
				</tr>
				<tr>
					<td><asp:button id="Button2" runat="server" Text="修改仓库档案"></asp:button></td>
				</tr>
				<tr>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
