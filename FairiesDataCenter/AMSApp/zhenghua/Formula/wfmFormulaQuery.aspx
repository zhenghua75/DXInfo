<%@ Page language="c#" Codebehind="wfmFormulaQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Formula.wfmFormulaQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmFormulaQuery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td align="center"><asp:label id="Label5" runat="server" CssClass="title">配方维护</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:label id="Label4" runat="server" CssClass="lable">产品组：</asp:label></td>
					<td><asp:dropdownlist id="ddlProductType" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td><asp:label id="Label1" runat="server" CssClass="lable">产品类别：</asp:label></td>
					<td><asp:dropdownlist id="ddlProductClass" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">产品编码：</asp:label></td>
					<td><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">产品名称：</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label6" runat="server" CssClass="lable">材料编码：</asp:label></td>
					<td><asp:textbox id="txtinvcode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label7" runat="server" CssClass="lable">材料名称：</asp:label></td>
					<td><asp:textbox id="txtinvname" runat="server" CssClass="textbox"></asp:textbox></td>
					<td colspan="4"></td>
				</tr>
				<tr>
					<td align="center" colSpan="8"><asp:button id="btnQuery" runat="server" CssClass="button" Text="查询"></asp:button><asp:button id="btnCancel" runat="server" CssClass="button" Text="取消"></asp:button><asp:button id="btnAdd" runat="server" CssClass="button" Text="编辑配方" Visible="False"></asp:button></td>
				</tr>
			</table>
			<table height="100%" width="100%">
				<tr>
					<td vAlign="top" align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" PageSize="20" BorderColor="Black"
							BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductTypeComments" HeaderText="产品类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductClassComments" HeaderText="产品类别"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCostSum" HeaderText="最新成本"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="零售价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcFeel" HeaderText="口感"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrganise" HeaderText="组织"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcColor" HeaderText="内馅"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcTaste" HeaderText="表面装饰"></asp:BoundColumn>
								<asp:BoundColumn DataField="havebom" HeaderText="有无配方"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="编辑" Target="_self" DataNavigateUrlField="cnvcProductCode" DataNavigateUrlFormatString="wfmFormula.aspx?flag=Edit&amp;ResetFlag=true&amp;invcode={0}"
									HeaderText="编辑"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
