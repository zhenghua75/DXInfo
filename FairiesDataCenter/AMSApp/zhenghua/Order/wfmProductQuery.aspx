<%@ Page language="c#" Codebehind="wfmProductQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmProductQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>��Ʒ��ѯ</title>
		<base target="_self">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">		
		<script>window.name="wfmProductQuery";</script>
  </HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmProductQuery">
			<table align="center" cellSpacing="1" cellPadding="1" border="1" width="800">
				<tr>
					<td align="center" colSpan="5"><asp:label id="Label3" runat="server" CssClass="title">��Ʒ��ѯ</asp:label></td>
				</tr>
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="lable">��Ʒ���룺</asp:label></td>
					<td><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">��Ʒ���ƣ�</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox>
					<asp:button id="btnQuery" runat="server" CssClass="button" Text="��ѯ��Ʒ"></asp:button>
					<asp:button id="btnBatchAddList" runat="server" Text="���������嵥"></asp:button>
					</td>
					<td></td>
				</tr>
				<tr>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�޸İٷֱȣ�</asp:label></td>
					<td><asp:textbox id="txtPercent" runat="server" CssClass="textbox"></asp:textbox><asp:label id="Label6" runat="server">%</asp:label>
					<asp:button id="btnPercent" runat="server" CssClass="button" Text="�������޸�"></asp:button>
					</td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">��ͬ������</asp:label></td>
					<td><asp:textbox id="txtCount" runat="server" CssClass="textbox"></asp:textbox>
					<asp:button id="btnAddList" runat="server" CssClass="button" Text="����ͬ���������嵥" Width="124px"></asp:button>
					</td>
					
				</tr>
				<tr>
					<td colSpan="5"><asp:checkbox id="chkSame" runat="server" AutoPostBack="True" Text="�Ƿ�ʹ��ͬ������"></asp:checkbox>
					<asp:button id="btnOrderDetail" runat="server" CssClass="button" Text="��Ʒ�嵥"></asp:button>
					<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="ȡ��"></asp:button>
						<asp:Button id="Button1" runat="server" CssClass="button" Text="ȷ��"></asp:Button></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" PageSize="17" BorderColor="Black"
							BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcinvcode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvccomunitname" HeaderText="����������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" HeaderText="�۸�"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="����">
									<ItemTemplate>
										<asp:TextBox id="TextBox1" runat="server" Width="78px"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�����嵥">
									<ItemTemplate>
										<asp:Button id="Button3" Text="�����嵥" runat="server" CommandName="putin"></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td>
						<asp:DataGrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="26">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcinvcode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcinvname" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvccomunitname" ReadOnly="True" HeaderText="����������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnsum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
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
