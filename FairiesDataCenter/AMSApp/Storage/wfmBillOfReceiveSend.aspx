<%@ Page language="c#" Codebehind="wfmBillOfReceiveSend.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfReceiveSend" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfReceiveSend</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="100%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">领料单发货</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 182px"></TD>
								<TD style="WIDTH: 105px" align="right"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 164px"></TD>
								<TD style="WIDTH: 163px" align="right"></TD>
								<TD style="WIDTH: 129px"><asp:button id="btnQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询"></asp:button></TD>
								<TD><FONT face="宋体"><asp:button id="btnCancel" runat="server" Font-Size="10pt" Width="56px" Text="返 回"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td><font style="FONT-SIZE: 10pt; COLOR: #0033ff">未发货的领料单列表：---（先填写发货量，再添加至发货列表）---</font></td>
				</tr>
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
							Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right" AllowPaging="True"
							PagerStyle-Mode="NumericPages">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnReceiveSerialNo" ReadOnly="True" HeaderText="领料流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptID" ReadOnly="True" HeaderText="领料单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroup" ReadOnly="True" HeaderText="生产组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndReceiveDate" ReadOnly="True" HeaderText="领料日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcClass" ReadOnly="True" HeaderText="班次"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMaterialInchargeOperID" ReadOnly="True" HeaderText="物料主管"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStorageInchargeOperID" ReadOnly="True" HeaderText="仓库主管"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSendOperID" ReadOnly="True" HeaderText="发料人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveType" ReadOnly="True" HeaderText="领料单类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSendSerialNo" ReadOnly="True" HeaderText="出货单号"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="填写发货量" DataNavigateUrlField="cnnReceiveSerialNo" DataNavigateUrlFormatString="wfmBillOfReceiveDetail.aspx?id={0}"
									HeaderText="编辑"></asp:HyperLinkColumn>
								<asp:ButtonColumn Text="添加至发货列表" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td style="WIDTH: 1077px" align="right"></td>
					<td align="right"><asp:button id="btnSendOK" runat="server" Font-Size="10pt" Width="95px" Text="确认发货" BorderColor="LemonChiffon"
							BackColor="Orange"></asp:button></td>
				</tr>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
				<tr>
					<td><font style="FONT-SIZE: 10pt; COLOR: #0033ff">已添加到发货的领料单列表</font></td>
					<td style="WIDTH: 217px"><font style="FONT-SIZE: 10pt; COLOR: #0033ff">已添加到发货的领料明细列表</font></td>
					<td>
						<asp:button id="btnPrint" runat="server" Text="打印发货单" Width="86px" Font-Size="10pt"></asp:button></td>
				</tr>
				<TR>
					<TD align="left"><asp:datagrid id="DataGrid2" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
							Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnReceiveSerialNo" ReadOnly="True" HeaderText="领料流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveType" ReadOnly="True" HeaderText="领料单类型"></asp:BoundColumn>
								<asp:ButtonColumn Text="取消" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right"></PagerStyle>
						</asp:datagrid></TD>
					<TD align="left" colspan="2"><asp:datagrid id="DataGrid3" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
							Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="物料编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="物料名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="规格单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReceiveCount" ReadOnly="True" HeaderText="总应领量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcQC002" ReadOnly="True" HeaderText="倾城店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCF001" ReadOnly="True" HeaderText="翠湖店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCF006" ReadOnly="True" HeaderText="财富店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcBS004" ReadOnly="True" HeaderText="宝善店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcJG003" ReadOnly="True" HeaderText="金格店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcXS007" ReadOnly="True" HeaderText="新世界"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSH005" ReadOnly="True" HeaderText="上海沙龙"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcFYZX1" ReadOnly="True" HeaderText="生产中心"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCY009" ReadOnly="True" HeaderText="创意英国店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcJM010" ReadOnly="True" HeaderText="金马坊店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcXM011" ReadOnly="True" HeaderText="小西门店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOutCount" ReadOnly="True" HeaderText="总发货量"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
