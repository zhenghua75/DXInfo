<%@ Page language="c#" Codebehind="wfmOrderReduce.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrderReduce" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmOrderReduce</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">减单</asp:Label></td>
				</tr>
			</table>
			<table>
				<tr>
					<td>
						<asp:Label id="Label4" runat="server" CssClass="lable">订单流水：</asp:Label></td>
					<td>
						<asp:TextBox id="txtOrderSerialNo" runat="server" Enabled="False" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Label CssClass="lable" id="Label2" runat="server">减单类型：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlReduceType" runat="server"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">减单说明：</asp:Label></td>
					<td>
						<asp:TextBox id="txtReduceComments" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:TextBox></td>
				</tr>
			</table>
			<table>
				<tr>
					<td>
						<asp:DataGrid id="DataGrid2" runat="server" AutoGenerateColumns="False" AllowPaging="True" Caption="产品清单"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="合计"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table>
				<tr>
					<td>
						<asp:Button id="btnOK" runat="server" Text="确定" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="取消" CssClass="button"></asp:Button>
						<asp:Button id="btnReturn" runat="server" Text="返回" CssClass="button"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
