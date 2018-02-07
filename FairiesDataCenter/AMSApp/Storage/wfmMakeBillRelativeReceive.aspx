<%@ Page language="c#" Codebehind="wfmMakeBillRelativeReceive.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmMakeBillRelativeReceive" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMakeBillRelativeReceive</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">关联原材料领用制令单</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 796px; HEIGHT: 8px" align="left">
									<asp:Label id="Label1" runat="server" Font-Size="10pt" Width="656px" ForeColor="Blue">查询尚未关联的原材料领用制令单，再确定关联，将原材料制令单关联为原材料领用单。</asp:Label></TD>
								<TD style="WIDTH: 128px; HEIGHT: 8px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<td></td>
							</TR>
							<TR>
								<TD style="WIDTH: 796px" align="right"></TD>
								<TD style="WIDTH: 128px"><asp:button id="btnRelativeMakeToReceive" runat="server" Width="72px" Text="确定关联"></asp:button></TD>
								<td>
									<asp:button id="btnReturn" runat="server" Width="64px" Text="返回"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Verdana" Width="100%"
							AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right" PageSize="20" AllowPaging="True"
							PagerStyle-Mode="NumericPages">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnProduceSerialNo" ReadOnly="True" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProduceDeptID" ReadOnly="True" HeaderText="生产部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroup" ReadOnly="True" HeaderText="生产组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndProduceDate" ReadOnly="True" HeaderText="领料日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCode" ReadOnly="True" HeaderText="原材料编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="原材料名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="orgCount" ReadOnly="True" HeaderText="原制令数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="addCount" ReadOnly="True" HeaderText="加单数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="reduceCount" ReadOnly="True" HeaderText="减单数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="realCount" ReadOnly="True" HeaderText="最终应领量"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
