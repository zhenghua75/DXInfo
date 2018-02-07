<%@ Page language="c#" Codebehind="wfmSelfProduce.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmSelfProduce" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSelfProduce</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">门市自生产</asp:label></td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="lable">订单序号：</asp:label></td>
					<td><asp:textbox id="txtOrderSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">生产日期：</asp:label></td>
					<td><asp:textbox id="txtShipDate" onfocus="calendar()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="6" align="center"><asp:button id="btnProductList" runat="server" CssClass="button" Text="产品清单"></asp:button><asp:button id="btnProduce" runat="server" CssClass="button" Text="生产"></asp:button><asp:button style="Z-INDEX: 0" id="btnStorage" runat="server" CssClass="button" Text="库存产品"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" AutoGenerateColumns="False" BorderColor="Black"
							BorderWidth="1px" ShowFooter="True" PageSize="20" AllowPaging="True" Caption="自生产订单产品清单">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="订单量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="总计"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperID" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="操作时间"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td>
						<asp:DataGrid id="DataGrid2" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20" Caption="库存产品清单">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="数量"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
