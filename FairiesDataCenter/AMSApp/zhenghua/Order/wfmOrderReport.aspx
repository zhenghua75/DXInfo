<%@ Page language="c#" Codebehind="wfmOrderReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrderReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDividReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<style media="print">.Noprint { DISPLAY: none }
	.PageNext { PAGE-BREAK-AFTER: always }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" align="center" class="Noprint">
				<tr>
					<td align="center">
						<OBJECT id="WebBrowser" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"
							VIEWASTEXT>
							
						</OBJECT>
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,1)" value="打印" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,6)" value="直接打印" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(8,1)" value="页面设置" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(7,1)" value="打印预览" type="button">
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button>
						<asp:TextBox id="txtProduceSerialNo" runat="server" Visible="False"></asp:TextBox></td>
					</TD></tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td colspan="4" align="center">
						<asp:label id="lblDate" runat="server" CssClass="title"></asp:label></td>
				</tr>
				<tr>
					<td align="center" colspan="4">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black"
							BorderWidth="1px">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="订单序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="下单门市"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcinvname" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="产品数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderComments" HeaderText="制作要求"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipAddress" HeaderText="送货地址"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndArrivedDate" HeaderText="要求到货时间" DataFormatString="{0:MM月dd日hh}"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td width="25%">
						<asp:Label id="Label6" runat="server">经理（厂长）：</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label4" runat="server">车队：</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label5" runat="server">仓库：</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label2" runat="server">制单：</asp:Label>
						<asp:Label id="lblOper" runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
