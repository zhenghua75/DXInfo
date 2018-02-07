<%@ Page language="c#" Codebehind="wfmFormula.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Formula.wfmFormula" %>
<%@ Register TagPrefix="cc1" Namespace="AMSApp.zhenghua.ImageDisplay" Assembly="AMSApp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmFormula</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function OpenInvWin(flag)
			{
				window.showModalDialog("../Inventory/wfmInventoryQuery.aspx?flag="+flag,window,"center:yes;dialogWidth:1000px;dialogHeight:600px;") ;
				
				window.location.href ="wfmFormula.aspx";		
			}						
		</script>
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table borderColor="black" cellSpacing="0" cellPadding="0" align="center" border="0">
				<TBODY>
					<tr>
						<td>
							<table width="100%">
								<tr>
									<td align="center" colSpan="4"><asp:label id="lblFormula" runat="server" CssClass="title">�༭�䷽</asp:label></td>
								</tr>
								<tr>
									<td align="center" colSpan="4"><asp:label id="Label4" runat="server">ĸ��</asp:label></td>
								</tr>
								<tr>
									<td style="HEIGHT: 24px"><asp:label id="Label1" runat="server" CssClass="lable">��Ʒ���</asp:label></td>
									<td style="HEIGHT: 24px"><asp:dropdownlist id="ddlProductClass" runat="server" Enabled="False" AutoPostBack="True"></asp:dropdownlist></td>
									<td style="HEIGHT: 24px"><asp:label id="Label3" runat="server" CssClass="lable">��Ʒ���룺</asp:label></td>
									<td style="HEIGHT: 24px"><asp:textbox id="txtProductCode" runat="server" CssClass="textbox" Enabled="False"></asp:textbox><asp:button id="Button1" runat="server" Text="��" CssClass="nodispaly"></asp:button></td>
								</tr>
								<tr>
									<td><asp:label id="Label7" runat="server" CssClass="lable">������λ�飺</asp:label></td>
									<td><asp:dropdownlist id="ddlGroupCode" runat="server" Enabled="False" AutoPostBack="True"></asp:dropdownlist></td>
									<td><asp:label id="Label2" runat="server" CssClass="lable">��Ʒ���ƣ�</asp:label></td>
									<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox" Enabled="False"></asp:textbox></td>
								</tr>
								<tr>
									<td><asp:label id="Label12" runat="server" CssClass="lable">����������λ��</asp:label></td>
									<td><asp:dropdownlist id="ddlUnit" runat="server" Enabled="False"></asp:dropdownlist></td>
									<td><asp:label id="Label5" runat="server" CssClass="lable">��������</asp:label></td>
									<td><asp:textbox id="txtBaseQtyD" runat="server" CssClass="textbox"></asp:textbox></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td vAlign="top" colSpan="2">
							<table width="100%">
								<tr>
									<td vAlign="top" width="50%">
										<hr>
										<table borderColor="black" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td><asp:label id="Label6" runat="server">�Ӽ�</asp:label><asp:button id="Button2" runat="server" Text="����"></asp:button></td>
											</tr>
											<tr>
												<td><asp:datagrid id="dgBOM" runat="server" CssClass="datagrid" AutoGenerateColumns="False" ShowFooter="True"
														PageSize="5" BorderWidth="1px" BorderColor="Black">
														<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
														<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
														<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
														<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
														<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="�������">
																<ItemTemplate>
																	<asp:Label id=Label13 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcInvName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="����������λ">
																<ItemTemplate>
																	<asp:Label id=Label14 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcProduceUnitCodeName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="����">
																<ItemTemplate>
																	<asp:Label id=Label15 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnbaseQtyN") %>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id=TextBox1 runat="server" Width="66px" Text='<%# DataBinder.Eval(Container, "DataItem.cnnbaseQtyN") %>'>
																	</asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
															<asp:TemplateColumn HeaderText="����">
																<ItemTemplate>
																	<asp:LinkButton id="LinkButton2" runat="server" CommandName="Delete" Text="ɾ��" CausesValidation="false"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" DataField="cnvcInvCode" ReadOnly="True" HeaderText="���ϱ���"></asp:BoundColumn>
														</Columns>
													</asp:datagrid></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td align="center" colSpan="2"><asp:button id="btnAdd" runat="server" CssClass="button" Text="ȷ��"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="����"></asp:button></td>
								</tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
