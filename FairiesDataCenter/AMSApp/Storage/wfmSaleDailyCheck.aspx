<%@ Page language="c#" Codebehind="wfmSaleDailyCheck.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmSaleDailyCheck" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleDailyCheck</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">库存盘点</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="1">
							<TR>
								<TD style="WIDTH: 247px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt" Width="48px" Height="14px">门店：</asp:label></TD>
								<TD style="WIDTH: 176px"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 138px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Size="10pt">仓库：</asp:label></TD>
								<TD style="WIDTH: 178px">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Font-Size="10pt" AutoPostBack="true"
										Width="160px"></asp:dropdownlist></TD>
								<td style="WIDTH: 62px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">日期：</asp:label></td>
								<td style="WIDTH: 104px" align="left"><asp:label id="lblCheckDate" runat="server" Font-Size="10pt" Width="138px" style="Z-INDEX: 0"
										Height="14px"></asp:label></td>
								<TD style="WIDTH: 102px"><asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" style="Z-INDEX: 0"></asp:button></TD>
								<td style="WIDTH: 90px"></td>
							</TR>
							<TR>
								<TD style="WIDTH: 247px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">产品类型：</asp:label></TD>
								<TD style="WIDTH: 176px"><asp:dropdownlist id="ddlProductType" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 138px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">产品类别：</asp:label></TD>
								<TD style="WIDTH: 178px"><asp:dropdownlist id="ddlProductClass" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
								<td style="WIDTH: 62px"></td>
								<td style="WIDTH: 104px"></td>
								<TD style="WIDTH: 102px"><FONT face="宋体">
										<asp:button id="btnreset" runat="server" Font-Size="10pt" Width="56px" Text="重选" style="Z-INDEX: 0"></asp:button></FONT></TD>
								<td style="WIDTH: 90px"></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">天气</asp:label></TD>
								<TD style="WIDTH: 171px"><asp:textbox id="Textbox1" runat="server" Font-Size="10pt" Width="160px"></asp:textbox></TD>
								<TD style="WIDTH: 77px" align="right"><asp:label id="Label11" runat="server" Font-Size="10pt">盘点人</asp:label></TD>
								<TD style="WIDTH: 185px"><asp:textbox id="Textbox2" runat="server" Font-Size="10pt"></asp:textbox></TD>
								<TD style="WIDTH: 62px" align="right"><asp:label id="Label13" runat="server" Font-Size="10pt">管理组</asp:label></TD>
								<td style="WIDTH: 197px"><asp:textbox id="Textbox3" runat="server" Font-Size="10pt"></asp:textbox></td>
								<TD style="WIDTH: 131px" align="left"><asp:button id="btnEdit" runat="server" Font-Size="10pt" Width="72px" Text="编辑"></asp:button></TD>
								<TD style="WIDTH: 150px" align="left"><asp:button id="btnCheckOk" runat="server" Font-Size="10pt" Width="64px" Text="盘点确认"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Verdana" Width="100%"
							AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnProductPrice" ReadOnly="True" HeaderText="产品单价"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOriginalStorage" ReadOnly="True" HeaderText="期初库存"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="进仓量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReceiveCount" ReadOnly="True" HeaderText="领用量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMoveOutCount" ReadOnly="True" HeaderText="调拨出量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMoveInCount" ReadOnly="True" HeaderText="调拨入量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" ReadOnly="True" HeaderText="损耗量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnFreeCount" ReadOnly="True" HeaderText="剩余量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnUseCount" ReadOnly="True" HeaderText="使用量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSellCount" ReadOnly="True" HeaderText="售卖量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSystemCount" ReadOnly="True" HeaderText="系统库存"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnRealCount" ReadOnly="True" HeaderText="实际库存"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="实际库存">
									<ItemTemplate>
										<asp:textbox id="Textbox5" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnDifferentCount" ReadOnly="True" HeaderText="差异量"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnnDifferentSum" ReadOnly="True" HeaderText="差异金额"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
