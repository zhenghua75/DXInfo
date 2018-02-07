<%@ Page language="c#" Codebehind="wfmMaterialSimpleAnalyse.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialSimpleAnalyse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialSimpleAnalyse</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">原材料分析报表</TD>
				</TR>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table2" style="FONT-SIZE: 10pt" cellSpacing="0" cellPadding="1" width="100%"
							border="0">
							<TR>
								<TD style="WIDTH: 150px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="102px">原材料类型</asp:label></TD>
								<TD style="WIDTH: 183px"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="160px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 107px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt" Width="32px">月份</asp:label></TD>
								<td style="WIDTH: 257px"><asp:dropdownlist id="ddlMonth" runat="server" Width="120px" AutoPostBack="False"></asp:dropdownlist></td>
								<TD style="WIDTH: 107px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 106px"><asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="64px" Text="导出"></asp:button></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" AllowPaging="True"
							PageSize="20" AutoGenerateColumns="False" Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue"
							Font-Name="Verdana" CellPadding="3" BorderWidth="1px" PagerStyle-HorizontalAlign="Right" BorderColor="Black">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcBatchNo" ReadOnly="True" HeaderText="原材料批次"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMaterialCode" ReadOnly="True" HeaderText="原材料编号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMaterialName" ReadOnly="True" HeaderText="原材料名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="规格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="LastCount" ReadOnly="True" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="LastFee" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
								<asp:BoundColumn DataField="EnterCount" ReadOnly="True" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="EnterFee" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
								<asp:BoundColumn DataField="OutCount" ReadOnly="True" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="OutFee" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
								<asp:BoundColumn DataField="CurCount" ReadOnly="True" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="CurFee" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages" Position="TopAndBottom" Font-Size="Small" Font-Bold="True"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
