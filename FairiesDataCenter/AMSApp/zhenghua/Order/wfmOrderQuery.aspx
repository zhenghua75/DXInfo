<%@ Page language="c#" Codebehind="wfmOrderQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmOrderQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmOrderQuery</title>
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
						<asp:Label id="Label13" runat="server" CssClass="title">生产订单</asp:Label></td>
				</tr>
			</table>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="900" border="1">
				<TR vAlign="top">
					<TD align="center">
						<table align="center" width="100%">
							<tr>
								<td>
									<asp:Label id="Label10" runat="server" CssClass="lable">下单门市：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlSalesRoom" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label11" runat="server" CssClass="lable">生产单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label12" runat="server" CssClass="lable">订单类型：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlOrderType" runat="server" AutoPostBack="True"></asp:DropDownList></td>
								<td><asp:label id="Label4" runat="server" CssClass="lable">订单状态：</asp:label></td>
								<td><asp:dropdownlist id="ddlOrderState" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td><asp:label id="Label9" runat="server" CssClass="lable" Visible="False">订单流水：</asp:label></td>
								<td><asp:textbox id="txtOrderSerialNo" runat="server" CssClass="textbox" Visible="False"></asp:textbox></td>
								<td><asp:label id="Label1" runat="server" CssClass="lable" Visible="False">订单操作员：</asp:label></td>
								<td><asp:dropdownlist id="ddlOrderOper" runat="server" Visible="False"></asp:dropdownlist></td>
								
							</tr>
							<tr>
							
								<td><asp:label id="Label2" runat="server" CssClass="lable">订单日期：</asp:label></td>
								<td><asp:textbox id="txtOrderDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate"></asp:textbox></td><td>------</td><td>
									<asp:textbox id="txtOrderDateEnd" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										runat="server" CssClass="Wdate"></asp:textbox>
								</td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">发货日期：</asp:label></td>
								<td><asp:textbox id="txtShipDate" runat="server" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:textbox></td><td>-----</td><td>
									<asp:textbox id="txtShipDateEnd" runat="server" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:textbox>
								</td>
							</tr>
						</table></TD></TR>
						<tr><td>						
						<table runat="server" id="tblCustom" align="center">
							<tr>
								<td><asp:label id="Label5" runat="server" CssClass="lable">客户姓名/单位：</asp:label></td>
								<td><asp:textbox id="txtCustomName" runat="server"  CssClass="textbox" Width="300"></asp:textbox></td>
								<td><asp:label id="Label7" runat="server" CssClass="lable">联系电话：</asp:label></td>
								<td><asp:textbox id="txtLinkPhone" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="lable">送货地址：</asp:label></td>
								<td><asp:textbox id="txtShipAddress" runat="server"  CssClass="textbox" Width="300"></asp:textbox></td>
								
								<td><asp:label id="Label8" runat="server" CssClass="lable">要求到货时间：</asp:label></td>
								<td><asp:textbox id="txtArrivedDate" runat="server" ReadOnly="True" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',isShowClear:true,readOnly:true,skin:'blue'})"></asp:textbox></td>
							</tr>
						</table></td></tr><tr><td>
						<table align="center">
							<tr>
								<td align="center"><asp:button id="btnQuery" runat="server" Text="查询" CssClass="button"></asp:button>
									<asp:button id="Button2" runat="server" Text="取消" CssClass="button"></asp:button>
									<asp:button id="Button1" runat="server" Text="添加生产订单" CssClass="button" Width="91px"></asp:button>
									<asp:Button id="btnPrint" runat="server" Text="明细打印" CssClass="button"></asp:Button>
									<asp:Button id="btnSumPrint" runat="server" Text="汇总打印"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<table align="center" width="100%">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="10">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="门市"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="发货日期" DataFormatString="{0:D}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderStateComments" HeaderText="状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOrderDate" HeaderText="订单时间"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="细节" Target="_self" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmOrderQueryDetail.aspx?OperFlag=Detail&amp;OrderSerialNo={0}" 
 HeaderText="细节"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn Text="编辑" DataNavigateUrlField="cnnOrderSerialNo" DataNavigateUrlFormatString="wfmProOrder.aspx?OperFlag=Edit&amp;OrderSerialNo={0}" 
 HeaderText="编辑"></asp:HyperLinkColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcOrderState" HeaderText="订单状态"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
