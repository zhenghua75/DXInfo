<%@ Page language="c#" Codebehind="wfmInventoryQuery.aspx.cs" AutoEventWireup="false"  Inherits="AMSApp.zhenghua.Inventory.wfmInventoryQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>存货档案</title>
		<base target="_self">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmInventoryQuery";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmInventoryQuery">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">存货档案</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr align="center">
					<td colspan="2">
					<asp:Label Runat="server" ID="Label3">产品组</asp:Label><asp:DropDownList Runat="server" ID="ddlProductType" AutoPostBack="True"></asp:DropDownList>
					<asp:Label Runat="server" ID="Label4">产品类别</asp:Label><asp:DropDownList Runat="server" ID="ddlProductClass"></asp:DropDownList>
					<asp:Label Runat="server" ID="lblInvCode">存货编码</asp:Label> <asp:TextBox Runat="server" ID="txtInvCode"></asp:TextBox>
					<asp:Label Runat="server" ID="lblInvName">存货名称</asp:Label> <asp:TextBox Runat="server" ID="txtInvName"></asp:TextBox>
					
					</td>
					
				</tr>
				<tr align="center">
					<td colspan="2" align="center">
						<asp:Button Runat="server" ID="btnQuery" Text="查询" CssClass="button"></asp:Button>
						<asp:Button id="Button1" runat="server" CssClass="button" Text="确定"></asp:Button>
						<asp:Button id="Button2" runat="server" CssClass="button" Text="取消"></asp:Button></td>
				</tr>
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" Visible=False>存货分类</asp:Label><INPUT type="hidden" runat="server" id="hidflag"></td>
							</tr>
							<tr>
								<td>
									
										<asp:DataGrid Visible=False id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black" BorderWidth="1px">
<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>											<Columns>
												<asp:BoundColumn DataField="cnvcProductTypeName" HeaderText="产品组"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProductClassCode" HeaderText="产品类别编码"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProductClassName" HeaderText="产品类别名称"></asp:BoundColumn>
												<asp:ButtonColumn Text="选择" HeaderText="选择" CommandName="Select"></asp:ButtonColumn>
											</Columns>
										</asp:DataGrid>
									
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" Visible=False>存货档案</asp:Label></td>
							</tr>
							<tr align="center">
								<td align="center">
									
										<asp:datagrid id="DataGrid2" runat="server" CssClass="datagrid" BorderWidth="1px" BorderColor="Black"
											AutoGenerateColumns="False">
											<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
											<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
											<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
											<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
											<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
											<Columns>
												<asp:ButtonColumn Text="选择" HeaderText="选择" CommandName="Select"></asp:ButtonColumn>
												<asp:BoundColumn DataField="cnvcInvCode" HeaderText="存货编码"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvName" HeaderText="存货名称"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvCCode" HeaderText="存货类别" Visible=False></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="允许生产订单" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbProductBill") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbProductBill") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="是否销售" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox2" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbSale") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbSale") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="是否外购" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox3" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbPurchase") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbPurchase") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="是否自制" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox4" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbSelf") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbSelf") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="是否生产耗用" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox5" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbComsume") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbComsume") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="cniInvCCost" HeaderText="参考成本"></asp:BoundColumn>
												<asp:BoundColumn DataField="cniInvNCost" HeaderText="最新成本"></asp:BoundColumn>
												<asp:BoundColumn DataField="cniSafeNum" HeaderText="安全库存量" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cniLowSum" HeaderText="最低库存" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cndSDate" HeaderText="启用日期" Visible=False></asp:BoundColumn>
												<asp:BoundColumn HeaderText="停用日期" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcCreatePerson" HeaderText="建档人" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcModifyPerson" HeaderText="变更人" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cndModifyDate" HeaderText="变更日期" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcValueType" HeaderText="计价方式" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvStd" HeaderText="规格型号"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcGroupCode" HeaderText="计量单位组编码" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcComUnitCode" HeaderText="主计量单位编码" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcSAComUnitCode" HeaderText="销售默认计量单位" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcPUComUnitCode" HeaderText="采购默认计量单位" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcSTComUnitCode" HeaderText="库存默认计量单位" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProduceUnitCode" HeaderText="生产计量单位编码" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProduceUnitCodeName" HeaderText="生产计量单位"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnfRetailPrice" HeaderText="零售价格" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcShopUnitCode" HeaderText="零售计量单位" Visible=False></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
		</form>
	</body>
</HTML>
