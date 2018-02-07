<%@ Page language="c#" Codebehind="wfmOrderQueryDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrderQueryDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmOrderQueryDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="lblTitle" runat="server" CssClass="title">����ϸ��</asp:Label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="95%" border="1" >
			<tr>
					<td >
						<table align="center">
				<tr>
					<td>
						<asp:Label id="Label10" runat="server" CssClass="lable">�µ����У�</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlSalesRoom" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label11" runat="server" CssClass="lable">������λ��</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label12" runat="server" CssClass="lable">�������ͣ�</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlOrderType" runat="server" AutoPostBack="True"></asp:DropDownList></td>
				</tr>
				<tr>
					<td><asp:label id="Label9" runat="server" CssClass="lable">������ˮ��</asp:label></td>
					<td><asp:textbox id="txtOrderSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
					<td>
						<asp:label id="Label4" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td>
						<asp:textbox id="txtShipDate" runat="server" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate"></asp:textbox></td>
					<td><asp:label id="lblOrderState" runat="server" CssClass="lable">����״̬��</asp:label></td>
					<td><asp:dropdownlist id="ddlOrderState" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>
						<asp:label id="lblOrderDate" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
					<td>
						<asp:textbox id="txtOrderDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate"></asp:textbox></td>
					<td>
						<asp:label id="lblOrderOper" runat="server" CssClass="lable">��������Ա��</asp:label></td>
					<td>
						<asp:dropdownlist id="ddlOrderOper" runat="server"></asp:dropdownlist></td>
					<td></td>
					<td></td>
				</tr>
			</table></td></tr>
			<tr>
					<td >
			<table runat="server" id="tblCustom" align="center">
				<tr>
					<td><asp:label id="Label6" runat="server" CssClass="lable">�ͻ�����/��λ��</asp:label></td>
					<td><asp:textbox id="txtCustomName" runat="server" Width="300" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label8" runat="server" CssClass="lable">��ϵ�绰��</asp:label></td>
					<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">�ͻ���ַ��</asp:label></td>
					<td><asp:textbox id="txtShipAddress" runat="server" Width="300" CssClass="textbox"></asp:textbox></td>
					
					<td><asp:label id="Label13" runat="server" CssClass="lable">Ҫ�󵽻�ʱ�䣺</asp:label></td>
					<td><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label5" runat="server" CssClass="lable">����Ҫ��</asp:label></td>
					<td><asp:textbox id="txtOrderComments" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
					<td></td>
					<td></td>
				</tr>
			</table></td></tr>
			<tr>
					<td >
			<table align="center">
				<tr>
					<td>
						<asp:Button id="btnModify" runat="server" Text="�޸�" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="button"></asp:Button>
						<asp:Button id="btnReturn" runat="server" Text="����" CssClass="button"></asp:Button></td>
				</tr>
			</table></td></tr></table>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:datagrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" Caption="����ϸ��"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcinvcode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcinvname" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvccomunitname" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" ReadOnly="True" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnorderCount" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="ɾ��" HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True"
							CssClass="datagrid" BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="��Ʒ����">
									<ItemTemplate>
										<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnvcProductCode") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Button id="Button2" runat="server" Text="���붩��" CommandName="Add"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="�ϼ�">
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnSum") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="Label2" runat="server"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
