<%@ Page language="c#" Codebehind="wfmInventoryQuery.aspx.cs" AutoEventWireup="false"  Inherits="AMSApp.zhenghua.Inventory.wfmInventoryQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�������</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�������</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr align="center">
					<td colspan="2">
					<asp:Label Runat="server" ID="Label3">��Ʒ��</asp:Label><asp:DropDownList Runat="server" ID="ddlProductType" AutoPostBack="True"></asp:DropDownList>
					<asp:Label Runat="server" ID="Label4">��Ʒ���</asp:Label><asp:DropDownList Runat="server" ID="ddlProductClass"></asp:DropDownList>
					<asp:Label Runat="server" ID="lblInvCode">�������</asp:Label> <asp:TextBox Runat="server" ID="txtInvCode"></asp:TextBox>
					<asp:Label Runat="server" ID="lblInvName">�������</asp:Label> <asp:TextBox Runat="server" ID="txtInvName"></asp:TextBox>
					
					</td>
					
				</tr>
				<tr align="center">
					<td colspan="2" align="center">
						<asp:Button Runat="server" ID="btnQuery" Text="��ѯ" CssClass="button"></asp:Button>
						<asp:Button id="Button1" runat="server" CssClass="button" Text="ȷ��"></asp:Button>
						<asp:Button id="Button2" runat="server" CssClass="button" Text="ȡ��"></asp:Button></td>
				</tr>
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" Visible=False>�������</asp:Label><INPUT type="hidden" runat="server" id="hidflag"></td>
							</tr>
							<tr>
								<td>
									
										<asp:DataGrid Visible=False id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black" BorderWidth="1px">
<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>											<Columns>
												<asp:BoundColumn DataField="cnvcProductTypeName" HeaderText="��Ʒ��"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProductClassCode" HeaderText="��Ʒ������"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProductClassName" HeaderText="��Ʒ�������"></asp:BoundColumn>
												<asp:ButtonColumn Text="ѡ��" HeaderText="ѡ��" CommandName="Select"></asp:ButtonColumn>
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
									<asp:Label id="Label2" runat="server" Visible=False>�������</asp:Label></td>
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
												<asp:ButtonColumn Text="ѡ��" HeaderText="ѡ��" CommandName="Select"></asp:ButtonColumn>
												<asp:BoundColumn DataField="cnvcInvCode" HeaderText="�������"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvName" HeaderText="�������"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvCCode" HeaderText="������" Visible=False></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="������������" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbProductBill") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbProductBill") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�Ƿ�����" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox2" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbSale") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbSale") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�Ƿ��⹺" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox3" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbPurchase") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbPurchase") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�Ƿ�����" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox4" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbSelf") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbSelf") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�Ƿ���������" Visible=False>
													<ItemTemplate>
														<asp:CheckBox id="CheckBox5" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbComsume") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbComsume") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="cniInvCCost" HeaderText="�ο��ɱ�"></asp:BoundColumn>
												<asp:BoundColumn DataField="cniInvNCost" HeaderText="���³ɱ�"></asp:BoundColumn>
												<asp:BoundColumn DataField="cniSafeNum" HeaderText="��ȫ�����" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cniLowSum" HeaderText="��Ϳ��" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cndSDate" HeaderText="��������" Visible=False></asp:BoundColumn>
												<asp:BoundColumn HeaderText="ͣ������" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcCreatePerson" HeaderText="������" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcModifyPerson" HeaderText="�����" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cndModifyDate" HeaderText="�������" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcValueType" HeaderText="�Ƽ۷�ʽ" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcInvStd" HeaderText="����ͺ�"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcGroupCode" HeaderText="������λ�����" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcComUnitCode" HeaderText="��������λ����" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcSAComUnitCode" HeaderText="����Ĭ�ϼ�����λ" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcPUComUnitCode" HeaderText="�ɹ�Ĭ�ϼ�����λ" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcSTComUnitCode" HeaderText="���Ĭ�ϼ�����λ" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProduceUnitCode" HeaderText="����������λ����" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcProduceUnitCodeName" HeaderText="����������λ"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnfRetailPrice" HeaderText="���ۼ۸�" Visible=False></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcShopUnitCode" HeaderText="���ۼ�����λ" Visible=False></asp:BoundColumn>
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
