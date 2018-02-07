<%@ Page language="c#" Codebehind="wfmCheckInWh.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmCheckInWh" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDividModify</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<script type="text/javascript">
			function getwh()
			{
				var wh = document.getElementById("ddlWarehouse");
				var currSelectText = wh.options[wh.selectedIndex].text;
				return "是否是"+currSelectText+"入库";
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label6" runat="server" CssClass="title">生产入库</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1"><tr><td>
			<table align="center" width="100%">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="calendar()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblWarehouse" runat="server">仓库</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlWarehouse" runat="server"></asp:DropDownList></td>
					<td><asp:Label id="Label1" runat="server">过期天数调整量：</asp:Label></td>
					<td><asp:textbox id="txtDays" runat="server" CssClass="textbox"></asp:textbox></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="6" align="center"><asp:button id="btnQuery" runat="server" CssClass="button" Text="盘点明细" Visible="True"></asp:button>
						<asp:button id="btnCheck" runat="server" CssClass="button" Text="生产入库"></asp:button>
						<asp:Button id="btnCheckQuery" runat="server" Text="入库清单" Visible="False"></asp:Button>
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button>
						<asp:textbox id="txtProduceState" runat="server" Visible="False"></asp:textbox>
						<asp:TextBox id="txtMakeSerialNo" runat="server" Visible="False"></asp:TextBox>
					</td>
				</tr>
			</table></td></tr></table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							AllowPaging="True" AutoGenerateColumns="False" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								
								<asp:BoundColumn DataField="cnvcInvStd" HeaderText="产品规格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCheckCount" HeaderText="盘点数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSTComUnitName" HeaderText="入库单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnInCount" HeaderText="入库数量"></asp:BoundColumn>
																
								<asp:TemplateColumn HeaderText="是否入库">
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.cnbInWh")) %>></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>