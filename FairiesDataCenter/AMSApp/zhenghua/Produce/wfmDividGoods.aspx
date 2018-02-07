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
				return "是否从"+currSelectText+"分货";
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">分货</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
								<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label2" runat="server" CssClass="lable">生产日期：</asp:label></td>
								<td><asp:textbox id="txtProduceDate" CssClass="textbox" onfocus="calendar()" runat="server" ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lblWarehouse" runat="server">仓库</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWarehouse" runat="server"></asp:DropDownList></td>
								<td></td>
								<td>
									<asp:CheckBox id="CheckBox1" runat="server" Text="是否已分货出库" Enabled="False"></asp:CheckBox></td>
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
									<asp:Label id="Label8" runat="server" CssClass="lable">查询条件</asp:Label></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label9" runat="server" CssClass="lable">分货流水：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlAssignSerialNo" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable">订单部门</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlOrderDept" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">产品编码</asp:Label></td>
								<td>
									<asp:TextBox id="txtProductCode" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label6" runat="server" CssClass="lable">产品名称</asp:Label></td>
								<td>
									<asp:TextBox id="txtProductName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td colSpan="6" align="center" style="HEIGHT: 26px">
									<asp:button id="btnDivideGoods" runat="server" CssClass="button" Text="生成分货数据"></asp:button>
									<asp:button id="btnClearDivideGoods" runat="server" CssClass="button" Text="清除分货数据"></asp:button>
									<asp:button id="btnQueryGoods" runat="server" Text="分货凭条" CssClass="button" Width="84px"></asp:button>
									<asp:Button id="btnExcel" runat="server" Text="分货凭条导出EXCEL"></asp:Button>
									<asp:button id="btnReturn" runat="server" Text="返回" CssClass="button" Width="91px"></asp:button>
									<asp:TextBox id="txtProduceState" runat="server" Visible="False" Width="285px"></asp:TextBox>
									<asp:Button id="btnQuery" runat="server" CssClass="button" Text="分货清单" Width="130px"></asp:Button>
									<asp:Button id="Button1" runat="server" CssClass="button" Text="分货出库"></asp:Button>
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
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="分货流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptIDComments" ReadOnly="True" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="发货时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="订单流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptIDComments" ReadOnly="True" HeaderText="订单部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" ReadOnly="True" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfRetailPrice" ReadOnly="True" HeaderText="价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="订单量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAssignCount" HeaderText="分货量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="合计"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="分货数据调整" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderType" ReadOnly="True" HeaderText="订单类型ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
