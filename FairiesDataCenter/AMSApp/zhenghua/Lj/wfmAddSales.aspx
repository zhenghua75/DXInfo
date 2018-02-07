<%@ Page language="c#" Codebehind="wfmAddSales.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Lj.wfmAddSales" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmAddSales</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="My97DatePicker/WdatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">销售入库</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR vAlign="top">
					<TD align="center">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<td><asp:label id="Label4" runat="server">拼音简码：</asp:label></td>
								<td><asp:textbox id="TextBox2" runat="server" AutoPostBack="True"></asp:textbox></td>
								<TD><asp:label id="Label1" runat="server">产品列表：</asp:label></TD>
								<td><asp:dropdownlist id="ddlGoods" runat="server"></asp:dropdownlist></td>
								<td>
									<asp:Label id="Label5" runat="server">数量</asp:Label></td>
								<td>
									<asp:TextBox id="txtCount" runat="server"></asp:TextBox></td>
								<td><asp:button id="Button1" runat="server" Text="添加产品"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr vAlign="top">
					<td align="center"><ASP:DATAGRID id="MyDataGrid" runat="server" AllowPaging="True" PagerStyle-Mode="NumericPages"
							PagerStyle-HorizontalAlign="Right" BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana"
							Font-Size="X-Small" HeaderStyle-BackColor="SteelBlue" AlternatingItemStyle-BackColor="#660033" Width="100%"
							Font-Names="Verdana" AutoGenerateColumns="False">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="产品数量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle Visible="False" Font-Size="X-Small" HorizontalAlign="Right" Wrap="False" Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></td>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server">销售部门：</asp:label></TD>
								<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
								<TD align="center"><asp:label id="Label2" runat="server">销售日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="TextBox1" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server"></asp:textbox></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<asp:Button id="Button2" runat="server" Text="销售入库"></asp:Button>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
