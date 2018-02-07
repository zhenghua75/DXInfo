<%@ Page language="c#" Codebehind="wfmOrder.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmOrder</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
  </HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�µ�</asp:label></td>
				</tr>
			</table>
			<table id="tblOrder" align="center" runat="server">
  <TBODY>
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="lable">�µ����У�</asp:label></td>
					<td><asp:dropdownlist id="ddlSalesRoom" runat="server" CssClass="textbox"></asp:dropdownlist></td>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">�������ͣ�</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderType" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td>
<asp:textbox id=txtShipDate onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})" runat="server" CssClass="Wdate></asp:textbox></td>&#13;&#10;&#9;&#9;&#9;&#9;</tr>&#13;&#10;&#9;&#9;&#9;</table>&#13;&#10;&#9;&#9;&#9;<table id=" align="center" tblCustom? ReadOnly="True">
				<tr>
					<td>
<asp:label id="Label6" runat="server" CssClass="lable">�ͻ�����/��λ��</asp:label></td>
					<td><asp:textbox id="txtCustomName" runat="server" CssClass="textbox"TextMode="MultiLine" Height="70px"></asp:textbox></td>
					<td><asp:label id="Label7" runat="server" CssClass="lable">�ͻ���ַ��</asp:label></td>
					<td><asp:textbox id="txtShipAddress" runat="server" Height="70px" TextMode="MultiLine" CssClass="textbox"></asp:textbox></td>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="lable">����Ҫ��</asp:Label></td>
					<td>
						<asp:TextBox id="txtOrderComments" runat="server" TextMode="MultiLine" Height="70px" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:label id="Label8" runat="server" CssClass="lable">��ϵ�绰��</asp:label></td>
					<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label9" runat="server" CssClass="lable">Ҫ�󵽻�ʱ�䣺</asp:label></td>
					<td colSpan="3"><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate></asp:textbox></td>
				</tr>
			</table>
			<table id="tblDetialcenter" runat="serverDataGrid1server��Ʒ�嵥TrueFalsedatagrid1px" BorderColor="Black" PageSize="20"" align="">
				<tr>
					<td><asp:datagrid id="" runat="" Caption="" AllowPaging="" AutoGenerateColumns=""
							CssClass="" BorderWidth=">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="�ϼ�"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table id="tblOper" align="center" runat="server">
				<tr>
					<td><asp:button id="btnOK" runat="server" CssClass="button" Text="ȷ��"></asp:button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
</asp:textbox></TR></TBODY></TABLE></FORM>
	</BODY></HTML>
