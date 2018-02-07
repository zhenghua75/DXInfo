<%@ Page language="c#" Codebehind="wfmWarehouseOut.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmWarehouseOut" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProducePlanQuery</title>
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
						<asp:Label id="Label1" runat="server" CssClass="title">生产材料领用</asp:Label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1"><tr><td>
			<table align="center" width="100%">
				<tr>
					<td>
						<asp:Label id="Label5" runat="server" CssClass="lable">生产单位：</asp:Label></td>
					<td>
						<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
					<td>
						<asp:Label id="Label2" runat="server" CssClass="lable">开始日期：</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceBeginDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate"></asp:TextBox></td>
					<td>
						<asp:Label id="Label3" runat="server" CssClass="lable">结束日期：</asp:Label></td>
					<td>
						<asp:TextBox id="txtProduceEndDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
							CssClass="Wdate"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="6" align="center">
						<asp:Button id="btnQuery" runat="server" Text="查询" CssClass="button"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Text="取消" CssClass="button"></asp:Button></td>
				</tr>
			</table></td></tr></table>
			<table align="center" width="100%">
				<tr>
					<td align="center">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceDeptID" HeaderText="单位ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptIDComments" HeaderText="生产单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndProduceDate" HeaderText="生产日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipBeginDate" HeaderText="发货开始日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipEndDate" HeaderText="发货结束日期" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceStateComments" ReadOnly="True" HeaderText="状态"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcProduceState" HeaderText="状态编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" ReadOnly="True" HeaderText="操作员"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="操作时间"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="领用" DataNavigateUrlField="cnnProduceSerialNo" DataNavigateUrlFormatString="wfmWarehouseCollar.aspx?ProduceSerialNo={0}"
									HeaderText="领用"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
