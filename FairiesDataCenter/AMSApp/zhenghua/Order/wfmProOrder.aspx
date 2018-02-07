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
					// 刷新当前窗口 
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
								<td><asp:label id="lblTitle" runat="server" CssClass="title">生产订单</asp:label></td>
							</tr>
						</table>
			<table  cellSpacing="1" cellPadding="1" width="800" border="1" align=center>
				
				<tr>
					<td >
						<table id="tblOrder" align="center" runat="server">
							<tr>
								<td><asp:label id="Label2" runat="server" CssClass="lable">下单门市：</asp:label></td>
								<td><asp:dropdownlist id="ddlSalesRoom" runat="server" CssClass="textbox"></asp:dropdownlist></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td><asp:label id="Label4" runat="server" CssClass="lable">订单类型：</asp:label></td>
								<td><asp:dropdownlist id="ddlOrderType" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
								
								
							</tr>
							<tr>
								<td><asp:label id="Label10" runat="server" CssClass="lable">订单日期：</asp:label></td>
								<td><asp:textbox id="txtOrderDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
										
								<td><asp:label id="Label5" runat="server" CssClass="lable">发货日期：</asp:label></td>
								<td><asp:textbox id="txtShipDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
								
									<td colspan="2"><asp:button id="Button1" runat="server" Text="查询产品"></asp:Button></td>
							</tr>
						</table>
						<asp:textbox id="txtOrderSerialNo" runat="server" Visible="False"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>
						<table id="tblCustom" align="center" runat="server">
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="lable">客户姓名/单位：</asp:label></td>
								<td><asp:textbox id="txtCustomName" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">送货地址：</asp:label></td>
								<td><asp:textbox id="txtShipAddress" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr><tr>
								<td><asp:label id="Label1" runat="server" CssClass="lable">制作要求：</asp:label></td>
								<td><asp:textbox id="txtOrderComments" runat="server" CssClass="textbox" Width="300"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label8" runat="server" CssClass="lable">联系电话：</asp:label></td>
								<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label9" runat="server" CssClass="lable">要求到货时间：</asp:label></td>
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
											<asp:BoundColumn DataField="cnvcinvcode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcinvName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvccomunitname" ReadOnly="True" HeaderText="生产计量单位"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnfretailprice" ReadOnly="True" HeaderText="价格"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnOrderCount" HeaderText="数量"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="合计"></asp:BoundColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
											<asp:ButtonColumn Text="删除" HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
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
								<td><asp:button id="btnOK" runat="server" CssClass="button" Text="确定"></asp:button>
								<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="取消"></asp:button><asp:button id="Button2" runat="server" CssClass="button" Text="返回"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
