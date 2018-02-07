<%@ Page language="c#" Codebehind="wfmProOrder.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmProOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmProOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../scripts/datetime.js"></script>
		<script language="javascript" src="../scripts/calendar.js"></script>
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
			function OpenQueryWin()
			{
				var retvalue = window.showModalDialog("wfmProductQuery.aspx",window,"center:yes;dialogWidth:800px;dialogHeight:600px;") ;
				if(retvalue == true)
				{
					// ˢ�µ�ǰ���� 
					var flag = document.getElementById("hidflag");
					flag.value="reload";
					window.location.href ="wfmProOrder.aspx";
					window.location.href.reload();
				}
			}
		</script>
</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">��������</asp:label></td>
							</tr>
						</table>
			<table  cellSpacing="1" cellPadding="1" width="800" border="1" align=center>
				
				<tr>
					<td >
						<table id="tblOrder" align="center" runat="server">
							<tr>
								<td><asp:label id="Label2" runat="server" CssClass="lable">�µ����У�</asp:label></td>
								<td><asp:dropdownlist id="ddlSalesRoom" runat="server" CssClass="textbox"></asp:dropdownlist></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">������λ��</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label4" runat="server" CssClass="lable">�������ͣ�</asp:label></td>
								<td><asp:dropdownlist id="ddlOrderType" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
								
								
							</tr>
							<tr>
								<td><asp:label id="Label10" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
								<td><asp:textbox id="txtOrderDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
										
								<td><asp:label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:label></td>
								<td><asp:textbox id="txtShipDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
								
									<td colspan="2"><asp:button id="Button1" runat="server" Text="��ѯ��Ʒ"></asp:Button></td>
							</tr>
						</table>
						<asp:textbox id="txtOrderSerialNo" runat="server" Visible="False"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>
						<table id="tblCustom" align="center" runat="server">
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="lable">�ͻ�����/��λ��</asp:label></td>
								<td><asp:textbox id="txtCustomName" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">�ͻ���ַ��</asp:label></td>
								<td><asp:textbox id="txtShipAddress" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr><tr>
								<td><asp:label id="Label1" runat="server" CssClass="lable">����Ҫ��</asp:label></td>
								<td><asp:textbox id="txtOrderComments" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label8" runat="server" CssClass="lable">��ϵ�绰��</asp:label></td>
								<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label9" runat="server" CssClass="lable">Ҫ�󵽻�ʱ�䣺</asp:label></td>
								<td colSpan="3"><asp:textbox id="txtArrivedDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr>
					<td>
						<table id="tblDetial" width="100%" align="center" runat="server">
							<tr>
								<td align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" BorderColor="Black" PageSize="20">
										<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
										<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
										<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
										<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnvcinvcode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcinvName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvccomunitname" ReadOnly="True" HeaderText="����������λ"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnfretailprice" ReadOnly="True" HeaderText="�۸�"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnOrderCount" HeaderText="����"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="�ϼ�"></asp:BoundColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
											<asp:ButtonColumn Text="ɾ��" HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="tblOper" align="center" runat="server">
							<tr>
								<td><asp:button id="btnOK" runat="server" CssClass="button" Text="ȷ��"></asp:button>
								<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="ȡ��"></asp:button><asp:button id="Button2" runat="server" CssClass="button" Text="����"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
