<%@ Page language="c#" Codebehind="wfmWDividReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmWDividReport" %>
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
						<OBJECT id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" width="0" height="0"
							VIEWASTEXT>
						</OBJECT>
						<input class="button" value="打印" type="button" runat="server" id="btnPrint"> <input class="button" value="直接打印" type="button" runat="server" id="btnDPrint">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(8,1)" value="页面设置" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(7,1)" value="打印预览" type="button">
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button>
						<asp:TextBox id="txtProduceSerialNo" runat="server" Visible="False"></asp:TextBox>
						<asp:TextBox style="Z-INDEX: 0" id="txtAssignSerialNo" runat="server" Visible="False"></asp:TextBox>
						<asp:TextBox style="Z-INDEX: 0" id="txtOrderSerialNo" runat="server" Visible="False"></asp:TextBox></td>
					</TD></tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td colspan="3" align="center">
						<asp:Label id="lblTitle" runat="server" CssClass="title"></asp:Label>
					</td>
				</tr>
				<tr>
					<td width="33%"><asp:label id="Label1" runat="server">客户单位/姓名:</asp:label><asp:label id="lblCustomName" runat="server"></asp:label></td>
					<td colspan="2"><asp:label id="Label3" runat="server">送货地址:</asp:label><asp:label id="lblShipAddress" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="Label8" runat="server">联系电话：</asp:Label>
						<asp:Label id="lblLinkPhone" runat="server"></asp:Label></td>
					<td colspan="2">
						<asp:Label id="Label10" runat="server">客人要求到货时间:</asp:Label>
						<asp:Label id="lblArrivedDate" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td align="center" colspan="3">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black"
							BorderWidth="1px">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="品项"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" HeaderText="单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAssignCount" HeaderText="实发数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="总额"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="损耗"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="备注"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td width="33%">
						<asp:Label id="Label9" runat="server">发货数量合计：</asp:Label>
						<asp:Label id="lblCount" runat="server"></asp:Label></td>
					<td width="33%"></td>
					<td width="33%">
						<asp:Label id="Label12" runat="server">发货金额合计：</asp:Label>
						<asp:Label id="lblSum" runat="server">Label</asp:Label></td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:Label id="Label11" runat="server">注:本单一式三联,白联为存根联,黄联为对账联(客户留存),红联为财务联.</asp:Label></td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:Label id="Label13" runat="server">发货时间：</asp:Label>
						<asp:Label id="lblShipDate" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:Label id="Label14" runat="server">实际到货时间：</asp:Label></td>
				</tr>
				<tr>
					<td width="33%">
						<asp:Label id="Label2" runat="server">客户签收：</asp:Label></td>
					<td width="33%">
						<asp:Label id="Label4" runat="server">运输：</asp:Label></td>
					<td width="33%">
						<asp:Label id="Label5" runat="server">成品仓：</asp:Label></td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:Label id="Label6" runat="server">客户意见反馈：</asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
