<%@ Page language="c#" Codebehind="wfmDividGoods.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmDividGoods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDividGoods</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function getwh()
			{
				var wh = document.getElementById("ddlWarehouse");
				var currSelectText = wh.options[wh.selectedIndex].text;
				return "�Ƿ��"+currSelectText+"�ֻ�";
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">�ֻ�</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
								<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
								<td><asp:textbox id="txtProduceDate" CssClass="textbox" onfocus="calendar()" runat="server" ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lblWarehouse" runat="server">�ֿ�</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWarehouse" runat="server"></asp:DropDownList></td>
								<td></td>
								<td>
									<asp:CheckBox id="CheckBox1" runat="server" Text="�Ƿ��ѷֻ�����" Enabled="False"></asp:CheckBox></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="1000" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td colspan="6" align="center">
									<asp:Label id="Label8" runat="server" CssClass="lable">��ѯ����</asp:Label></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label9" runat="server" CssClass="lable">�ֻ���ˮ��</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlAssignSerialNo" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable">��������</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlOrderDept" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">��Ʒ����</asp:Label></td>
								<td>
									<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label6" runat="server" CssClass="lable">��Ʒ����</asp:Label></td>
								<td>
									<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td colSpan="6" align="center" style="HEIGHT: 26px">
									<asp:button id="btnDivideGoods" runat="server" CssClass="button" Text="���ɷֻ�����"></asp:button>
									<asp:button id="btnClearDivideGoods" runat="server" CssClass="button" Text="����ֻ�����"></asp:button>
									<asp:button id="btnQueryGoods" runat="server" Text="�ֻ�ƾ��" CssClass="button" Width="84px"></asp:button>
									<asp:Button id="btnExcel" runat="server" Text="�ֻ�ƾ������EXCEL"></asp:Button>
									<asp:button id="btnReturn" runat="server" Text="����" CssClass="button" Width="91px"></asp:button>
									<asp:TextBox id="txtProduceState" runat="server" Visible="False" Width="285px"></asp:TextBox>
									<asp:Button id="btnQuery" runat="server" CssClass="button" Text="�ֻ��嵥" Width="130px"></asp:Button>
									<asp:Button id="Button1" runat="server" CssClass="button" Text="�ֻ�����"></asp:Button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" CssClass="datagrid" BorderWidth="1px" BorderColor="Black">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid2" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderWidth="1px"
							BorderColor="Black" AllowPaging="True" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="�ֻ���ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptIDComments" ReadOnly="True" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptIDComments" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfRetailPrice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAssignCount" HeaderText="�ֻ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�ֻ����ݵ���" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderType" ReadOnly="True" HeaderText="��������ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
