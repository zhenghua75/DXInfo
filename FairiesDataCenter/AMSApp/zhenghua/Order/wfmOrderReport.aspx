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
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,1)" value="��ӡ" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,6)" value="ֱ�Ӵ�ӡ" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(8,1)" value="ҳ������" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(7,1)" value="��ӡԤ��" type="button">
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="����"></asp:button>
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
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="�µ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcinvname" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderComments" HeaderText="����Ҫ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipAddress" HeaderText="�ͻ���ַ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndArrivedDate" HeaderText="Ҫ�󵽻�ʱ��" DataFormatString="{0:MM��dd��hh}"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td width="25%">
						<asp:Label id="Label6" runat="server">������������</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label4" runat="server">���ӣ�</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label5" runat="server">�ֿ⣺</asp:Label></td>
					<td width="25%">
						<asp:Label id="Label2" runat="server">�Ƶ���</asp:Label>
						<asp:Label id="lblOper" runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
