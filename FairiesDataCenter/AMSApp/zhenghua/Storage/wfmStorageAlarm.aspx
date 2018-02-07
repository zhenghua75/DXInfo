<%@ Page language="c#" Codebehind="wfmStorageAlarm.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmStorageAlarm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageAlarm</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">库存报警</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 84px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Font-Size="10pt" Width="60px" Height="2px">部门</asp:label></TD>
								<TD style="WIDTH: 178px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Font-Size="10pt" Width="160px" Height="22px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 82px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Size="10pt" Width="60px" Height="2px">仓库</asp:label></TD>
								<TD style="WIDTH: 161px" align="left">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Font-Size="10pt" Width="160px"
										Height="22px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="60px" Height="2px">存货类型</asp:label></TD>
								<TD style="WIDTH: 199px">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlProductType" runat="server" Font-Size="10pt" Width="160px"
										Height="22px"></asp:dropdownlist></TD>
								<TD>
									<asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcDept" ReadOnly="True" HeaderText="部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWhName" ReadOnly="True" HeaderText="仓库"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvType" ReadOnly="True" HeaderText="存货类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="存货编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="存货名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroupCode" ReadOnly="True" HeaderText="计量单位组"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComUnitCode" ReadOnly="True" HeaderText="主计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cniSafeNum" ReadOnly="True" HeaderText="安全库存量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cniLowSum" ReadOnly="True" HeaderText="最低库存"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCurCount" ReadOnly="True" HeaderText="当前库存量"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="报警">
									<ItemTemplate>
										<asp:Image style="Z-INDEX: 0" id="imgAlarm" runat="server"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
