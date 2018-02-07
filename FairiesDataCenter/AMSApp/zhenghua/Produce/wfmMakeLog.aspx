<%@ Page language="c#" Codebehind="wfmMakeLog.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmMakeLog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMakeLog</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../scripts/calendar.js"></script>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="title">生产预估</asp:label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center" width="100%">
							<tr>
								<td><asp:label id="Label7" runat="server" CssClass="lable">生产序号：</asp:label></td>
								<td><asp:textbox id="txtProduceSerialNo" runat="server" CssClass="textbox"></asp:textbox></td>
								<td><asp:label id="Label3" runat="server" CssClass="lable">生产单位：</asp:label></td>
								<td><asp:dropdownlist id="ddlProduceDept" runat="server"></asp:dropdownlist></td>
								<td>
									<asp:Label id="Label4" runat="server">仓库：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWarehouse" runat="server"></asp:DropDownList></td>
								<td><asp:label CssClass="lable" id="Label2" runat="server">生产日期：</asp:label></td>
								<td><asp:textbox id="txtProduceDate" onfocus="calendar()" runat="server" ReadOnly="True" CssClass="textbox"></asp:textbox></td>
								<td>
									<asp:CheckBox id="chkSelf" runat="server" Text="是否自生产"></asp:CheckBox></td>
							</tr>
							<tr>
								<td colSpan="9" align="center" style="HEIGHT: 26px">
									<asp:Button id="btnClear" runat="server" Text="清除预估数据"></asp:Button><asp:button id="btnMakeLog" runat="server" Text="预估" CssClass="button"></asp:button>
									<asp:button id="btnQueryMake" runat="server" Text="预估明细" CssClass="button" Width="91px"></asp:button>
									<asp:Button id="Button1" runat="server" Text="预估调整"></asp:Button>
									<asp:button id="btnReturn" runat="server" Text="返回"></asp:button>
									<asp:Button id="Button2" runat="server" Text="领料单"></asp:Button>
								</td>
							</tr>
							<tr>
								<td colspan="6" align="center"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
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
								<asp:BoundColumn DataField="cnnMakeSerialNo" ReadOnly="True" HeaderText="预估流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperIDComments" ReadOnly="True" HeaderText="预估人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="预估日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductTypeName" ReadOnly="True" HeaderText="产品组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductClass" ReadOnly="True" HeaderText="存货分类"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="存货编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="存货名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMakeCount" ReadOnly="True" HeaderText="计划数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStComUnitName" ReadOnly="True" HeaderText="库存计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnWhCount" ReadOnly="True" HeaderText="库存数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStCount" HeaderText="库存领用量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAdjustCount" HeaderText="生产调整数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="生产数量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
