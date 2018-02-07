<%@ Page language="c#" Codebehind="wfmDividModify.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmDividModify" %>
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
					<td><asp:label id="Label6" runat="server" CssClass="title">生产盘点</asp:label></td>
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
								<td><asp:label id="Label2" runat="server" CssClass="lable">生产日期：</asp:label></td>
								<td><asp:textbox id="txtProduceDate" onfocus="calendar()" runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="6" align="center">
									<asp:CheckBox id="CheckBox1" runat="server" Text="是否已盘点" Enabled="False"></asp:CheckBox><asp:button id="btnQuery" runat="server" CssClass="button" Text="计划查询" Visible="False"></asp:button>
									<asp:button id="btnCheck" runat="server" CssClass="button" Text="盘点"></asp:button>
									<asp:Button id="btnAdjustCheck" runat="server" Text="调整盘点" Visible="False"></asp:Button>
									<asp:Button id="btnCheckQuery" runat="server" Text="盘点清单"></asp:Button>
									<asp:button id="btnReturn" runat="server" CssClass="button" Text="返回"></asp:button>
									<asp:textbox id="txtProduceState" runat="server" Visible="False"></asp:textbox>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<td align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderColor="Black" BorderWidth="1px"
							AllowPaging="false" AutoGenerateColumns="False" PageSize="20">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="生产流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitName" ReadOnly="True" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="订单量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProduceCount" ReadOnly="True" HeaderText="生产量"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="盘点量">
									<ItemTemplate>
										<asp:TextBox ID="txtAdjustCheckCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCheckCount") %>'>
										</asp:TextBox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnCheckCount") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="生产组">
									<ItemTemplate>
										<asp:DropDownList id="ddlTeamText" runat="server"></asp:DropDownList>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="ddlTeam" runat="server"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="生产员">
									<ItemTemplate>
										<asp:DropDownList id="ddlProducerText" runat="server"></asp:DropDownList>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="ddlProducer" runat="server"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
