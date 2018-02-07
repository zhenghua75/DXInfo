<%@ Page language="c#" Codebehind="wfmWarehouseCollar.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmWarehouseCollar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMakeLog</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../scripts/calendar.js"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function getwh()
			{
				var wh = document.getElementById("ddlWarehouse");
				var currSelectText = wh.options[wh.selectedIndex].text;
				return "是否从"+currSelectText+"领用";
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">生产材料领用</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1"><tr><td>
			<table align="center" width="100%">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label CssClass="lable" id="Label2" runat="server">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="calendar()" runat="server" ReadOnly="True" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label4" runat="server">仓库</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlWarehouse" runat="server"></asp:DropDownList></td>
					<td></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="6" align="center" style="HEIGHT: 26px">
						<asp:button id="btnQueryMake" runat="server" Text="领用明细" CssClass="button" Width="91px"></asp:button>
						<asp:Button id="Button2" runat="server" Text="领用"></asp:Button>
						<asp:button style="Z-INDEX: 0" id="btnReturn" runat="server" Text="返回"></asp:button>
						<asp:TextBox id="txtMakeSerialNo" runat="server" Visible="False"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan="6" align="center"></td>
				</tr>
			</table></td></tr></table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:datagrid style="Z-INDEX: 0" id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black"
							BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="True">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvStd" HeaderText="产品规格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCollarCount" HeaderText="领用数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSTComUnitName" HeaderText="出库单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStCount" HeaderText="出库数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnwhcount" HeaderText="库存可用量"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="是否领用">
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbCollar") %>></asp:CheckBox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbCollar") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
