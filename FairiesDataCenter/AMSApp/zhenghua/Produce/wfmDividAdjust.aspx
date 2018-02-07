<%@ Page language="c#" Codebehind="wfmDividAdjust.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmDividAdjust" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDividModify</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label6" runat="server" CssClass="title">�ֻ������ѯ</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">������ţ�</asp:label></td>
								<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
								<td><asp:textbox id="txtProduceDate" onfocus="calendar()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
								<td>
									<asp:CheckBox id="chkSelf" runat="server" Text="������"></asp:CheckBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label5" runat="server">�ֻ���ˮ��</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlAssignSerialNo" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable" style="Z-INDEX: 0">�������ţ�</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlOrderDept" runat="server" style="Z-INDEX: 0"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable" style="Z-INDEX: 0">�������ͣ�</asp:Label></td>
								<td colspan="2">
									<asp:DropDownList id="ddlOrderType" runat="server" style="Z-INDEX: 0"></asp:DropDownList></td>
							</tr>
							<tr>
								<td colSpan="6" align="center"><asp:button id="btnQuery" runat="server" CssClass="button" Text="��ѯ"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="����"></asp:button><asp:textbox id="txtProduceState" runat="server" Visible="False"></asp:textbox></td>
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
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="�ֻ���ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCustomName" HeaderText="�ͻ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="�ͻ�����" Target="_self" DataNavigateUrlField="cnvcLink" DataNavigateUrlFormatString="{0}"
									HeaderText="�ͻ�����"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
