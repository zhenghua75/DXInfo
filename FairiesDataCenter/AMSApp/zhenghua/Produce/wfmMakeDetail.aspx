<%@ Page language="c#" Codebehind="wfmMakeDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmMakeDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmMakeDetail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<style media="print">.Noprint { DISPLAY: none }
	.PageNext { PAGE-BREAK-AFTER: always }
	</style>
</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table class="NOPRINT" align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">领料单</asp:label></td>
				</tr>
			</table>
			<table class="NOPRINT" align="center">
				<tr>
					<td><asp:button id="btnExcel" runat="server" CssClass="button" Text="生成EXCEL"></asp:button>
					<td>
      <OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 
      width=0 height=0 VIEWASTEXT>
	
	</OBJECT>
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,1)" value="打印" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(6,6)" value="直接打印" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(8,1)" value="页面设置" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(7,1)" value="打印预览" type="button">
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button></td>
					</td></tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="left">
					<asp:Label ID="lblProduceSerialNo" Runat="server"></asp:Label>
					</td>
					<td align="left"><asp:TextBox ID="txtProduceSerialNo" Runat="server" CssClass="nodispaly"></asp:TextBox>
					<asp:Label ID="lblTitle" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							ShowFooter="True" AutoGenerateColumns="False">
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
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="Datagrid2" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							ShowFooter="True" AutoGenerateColumns="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMakeCount" HeaderText="生产数量"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
