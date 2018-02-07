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
					<td><asp:label id="lblTitle" runat="server" CssClass="title">生产计划关联订单</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
			<tr><td>
			<table align="center">
				<tr>
					<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
					<td colSpan="4"><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
					<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">生产日期：</asp:label></td>
					<td><asp:textbox id="txtProduceDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
							<td>
<asp:CheckBox id=chkSelf runat="server" Text="是否自生产"></asp:CheckBox></td>
				</tr>
			</table></td></tr>
			<tr><td>
			<table align="center">
				<tr>
					<td colSpan="4" align="center"><asp:label id="Label6" runat="server" Font-Bold="True">关联订单发货日期</asp:label></td>
				</tr>
				<tr>
					<td><FONT face="宋体"><asp:label id="Label4" runat="server" CssClass="lable">开始日期：</asp:label></FONT></td>
					<td><FONT face="宋体"><asp:textbox id="txtShipBeginDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
								runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></FONT></td>
					<td><asp:label id="Label5" runat="server" CssClass="lable">结束日期：</asp:label></td>
					<td><asp:textbox id="txtShipEndDate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							runat="server" CssClass="Wdate" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnLinkOrder" runat="server" CssClass="button" Text="关联订单"></asp:button>
					<asp:button id="btnQueryOrder" runat="server" CssClass="button" Text="订单清单"></asp:button>
					<asp:button id="btnQueryProduct" runat="server" CssClass="button" Text="产品清单">
					</asp:button><asp:button id="btnModify" runat="server" CssClass="button" Text="修改"></asp:button>
					<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="取消"></asp:button><asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button></td>
				</tr>
			</table></td></tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" PageSize="20" BorderColor="Black"
							BorderWidth="1px" Caption="订单清单" AutoGenerateColumns="False" AllowPaging="True">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" HeaderText="订单序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderDeptIDComments" HeaderText="订单部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOrderTypeComments" HeaderText="订单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" HeaderText="发货日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" HeaderText="操作时间"></asp:BoundColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid2" runat="server" CssClass="datagrid" PageSize="20" BorderColor="Black"
							BorderWidth="1px" Caption="产品清单" AutoGenerateColumns="False" AllowPaging="True">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="产品代码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="订单数量"></asp:BoundColumn>								
								<asp:BoundColumn DataField="cnvcStComUnitName" ReadOnly="True" HeaderText="库存计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnwhcount" ReadOnly="True" HeaderText="库存数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProduceCount" HeaderText="计划生产数量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
