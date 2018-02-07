<%@ Page language="c#" Codebehind="wfmProduceOrder.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmProduceOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmProduceOrder</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="title">�����ƻ���������</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
			<tr><td>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
					<td colSpan="4"><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
							<td>
<asp:CheckBox id=chkSelf runat="server" Text="�Ƿ�������"></asp:CheckBox></td>
				</tr>
			</table></td></tr>
			<tr><td>
			<table align="center">
				<tr>
					<td colSpan="4" align="center"><asp:label id="Label6" runat="server" Font-Bold="True">����������������</asp:label></td>
				</tr>
				<tr>
					<td><FONT face="����"><asp:label id="Label4" runat="server" CssClass="lable">��ʼ���ڣ�</asp:label></FONT></td>
					<td><FONT face="����"><asp:textbox id="txtShipBeginDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
								runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></FONT></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td><asp:textbox id="txtShipEndDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnLinkOrder" runat="server" CssClass="button" Text="��������"></asp:button>
					<asp:button id="btnQueryOrder" runat="server" CssClass="button" Text="�����嵥"></asp:button>
					<asp:button id="btnQueryProduct" runat="server" CssClass="button" Text="��Ʒ�嵥">
					</asp:button><asp:button id="btnModify" runat="server" CssClass="button" Text="�޸�"></asp:button>
					<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="ȡ��"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="����"></asp:button></td>
				</tr>
			</table></td></tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" PageSize="20" BorderColor="Black"
							BorderWidth="1px" Caption="�����嵥" AutoGenerateColumns="False" AllowPaging="True">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="��������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="����ʱ��"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid2" runat="server" CssClass="datagrid" PageSize="20" BorderColor="Black"
							BorderWidth="1px" Caption="��Ʒ�嵥" AutoGenerateColumns="False" AllowPaging="True">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="����������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>								
								<asp:BoundColumn DataField="cnvcStComUnitName" ReadOnly="True" HeaderText="��������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnwhcount" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProduceCount" HeaderText="�ƻ���������"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
