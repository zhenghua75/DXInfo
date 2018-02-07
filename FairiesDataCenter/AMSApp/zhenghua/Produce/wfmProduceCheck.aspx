<%@ Page language="c#" Codebehind="wfmProduceCheck.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmProduceCheck" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProducePlanQuery</title>
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
					<td><asp:label id="Label1" runat="server" CssClass="title">�����̵�</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td><asp:label id="Label5" runat="server" CssClass="lable">������λ��</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label2" runat="server" CssClass="lable">��ʼ���ڣ�</asp:label></td>
								<td><asp:textbox id="txtProduceBeginDate" runat="server" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
								<td><asp:textbox id="txtProduceEndDate" runat="server" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="6" align="center"><asp:button id="btnQuery" runat="server" CssClass="button" Text="��ѯ"></asp:button><asp:button id="btnCancel" runat="server" CssClass="button" Text="ȡ��"></asp:button>
									<asp:Button id="Button1" runat="server" CssClass="button" Text="������"></asp:Button>
									<asp:Button id="Button2" runat="server" CssClass="button" Text="����Ա"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							AllowPaging="True" AutoGenerateColumns="False" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceDeptID" HeaderText="��λID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndProduceDate" HeaderText="��������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipBeginDate" HeaderText="������ʼ����" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipEndDate" HeaderText="������������" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceStateComments" ReadOnly="True" HeaderText="״̬"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceState" HeaderText="״̬����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" ReadOnly="True" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="�����̵�" Target="_self" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmDividModify.aspx?ProduceSerialNo={0}"
									HeaderText="�����̵�"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
