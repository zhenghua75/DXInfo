<%@ Page language="c#" Codebehind="wfmSalesRoomProduce.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmSalesRoomProduce" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProducePlanQuery</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../scripts/calendar.js"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">��������</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">������λ��</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">��ʼ���ڣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceBeginDate" runat="server" ReadOnly="True" onfocus="calendar()" CssClass="textbox"></asp:TextBox></td>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">�������ڣ�</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceEndDate" runat="server" ReadOnly="True" onfocus="calendar()" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="6" align="center">
						<asp:Button id="btnQuery" runat="server" Text="��ѯ" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button"></asp:Button></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:datagrid id="Datagrid2" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							AllowPaging="True" AutoGenerateColumns="False" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="��ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="��������" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderStateComments" HeaderText="״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderOperIDComments" HeaderText="����Ա"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="����" Target="_self" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmSelfProduce.aspx?OperFlag=Produce&amp;OrderSerialNo={0}"
									HeaderText="����"></asp:HyperLinkColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderState" HeaderText="����״̬"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
