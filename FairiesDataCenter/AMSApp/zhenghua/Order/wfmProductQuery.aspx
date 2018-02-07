<%@ Page language="c#" Codebehind="wfmProductQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Order.wfmProductQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>产品查询</title>
		<base target="_self">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">		
		<script>window.name="wfmProductQuery";</script>
  </HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmProductQuery">
			<table align="center" cellSpacing="1" cellPadding="1" border="1" width="800">
				<tr>
					<td align="center" colSpan="5"><asp:label id="Label3" runat="server" CssClass="title">产品查询</asp:label></td>
				</tr>
				<tr>
					<td><asp:label id="Label1" runat="server" CssClass="lable">产品编码：</asp:label></td>
					<td><asp:textbox id="txtProductCode" runat="server" CssClass="textbox"></asp:textbox></td>
					<td><asp:label id="Label2" runat="server" CssClass="lable">产品名称：</asp:label></td>
					<td><asp:textbox id="txtProductName" runat="server" CssClass="textbox"></asp:textbox>
					<asp:button id="btnQuery" runat="server" CssClass="button" Text="查询产品"></asp:button>
					<asp:button id="btnBatchAddList" runat="server" Text="批量放入清单"></asp:button>
					</td>
					<td></td>
				</tr>
				<tr>
					<td><asp:label id="Label5" runat="server" CssClass="lable">修改百分比：</asp:label></td>
					<td><asp:textbox id="txtPercent" runat="server" CssClass="textbox"></asp:textbox><asp:label id="Label6" runat="server">%</asp:label>
					<asp:button id="btnPercent" runat="server" CssClass="button" Text="按比率修改"></asp:button>
					</td>
					<td><asp:label id="Label4" runat="server" CssClass="lable">相同数量：</asp:label></td>
					<td><asp:textbox id="txtCount" runat="server" CssClass="textbox"></asp:textbox>
					<asp:button id="btnAddList" runat="server" CssClass="button" Text="按相同数量放入清单" Width="124px"></asp:button>
					</td>
					
				</tr>
				<tr>
					<td colSpan="5"><asp:checkbox id="chkSame" runat="server" AutoPostBack="True" Text="是否使用同期数据"></asp:checkbox>
					<asp:button id="btnOrderDetail" runat="server" CssClass="button" Text="产品清单"></asp:button>
					<asp:button id="btnCancel" runat="server" CssClass="nodispaly" Text="取消"></asp:button>
						<asp:Button id="Button1" runat="server" CssClass="button" Text="确定"></asp:Button></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td><asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" PageSize="17" BorderColor="Black"
							BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcinvcode" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvccomunitname" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" HeaderText="价格"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="数量">
									<ItemTemplate>
										<asp:TextBox id="TextBox1" runat="server" Width="78px"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="放入清单">
									<ItemTemplate>
										<asp:Button id="Button3" Text="放入清单" runat="server" CommandName="putin"></asp:Button>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<table width="100%" align="center">
				<tr vAlign="top" align="center">
					<td>
						<asp:DataGrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datagrid"
							BorderWidth="1px" BorderColor="Black" PageSize="26">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcinvcode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcinvname" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvccomunitname" ReadOnly="True" HeaderText="生产计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfretailprice" ReadOnly="True" HeaderText="价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnsum" ReadOnly="True" HeaderText="合计"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="删除" HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
