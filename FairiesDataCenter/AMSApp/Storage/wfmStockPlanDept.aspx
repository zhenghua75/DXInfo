<%@ Page language="c#" Codebehind="wfmStockPlanDept.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmStockPlanDept" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStockPlanDept</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">门店原材料预估</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 130px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt">门店：</asp:label></TD>
								<TD style="WIDTH: 221px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" AutoPostBack="false" Width="184px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 195px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt">产品：</asp:label></TD>
								<TD style="WIDTH: 188px">
									<asp:dropdownlist id="ddlProduct" runat="server" Font-Size="10pt" AutoPostBack="true" Width="184px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 83px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt">规格单位：</asp:label></TD>
								<TD style="WIDTH: 91px">
									<asp:textbox id="txtUnit" runat="server" Font-Size="10pt" Width="72px" ReadOnly="True"></asp:textbox></TD>
								<TD style="WIDTH: 78px" align="right"><FONT face="宋体">
										<asp:button id="btnQuery" runat="server" Font-Size="10pt" Width="61px" Text="查询"></asp:button></FONT></TD>
								<TD style="WIDTH: 62px" align="right">
									<asp:button id="Button1" runat="server" Font-Size="10pt" Width="61px" Text="取消"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 130px" align="right"><FONT face="宋体">
										<asp:label id="Label5" runat="server" Font-Size="10pt">月份：</asp:label><FONT face="宋体"></FONT></FONT></TD>
								<TD style="WIDTH: 221px"><FONT face="宋体"><FONT face="宋体">
											<asp:DropDownList id="ddlMonth" runat="server" Width="183px"></asp:DropDownList></FONT></FONT></TD>
								<TD style="WIDTH: 195px" align="right">
									<asp:label id="Label8" runat="server" Font-Size="10pt">预估总用量：</asp:label></TD>
								<TD style="WIDTH: 188px">
									<asp:textbox id="txtCount" runat="server" Font-Size="10pt" Width="184px"></asp:textbox></TD>
								<TD style="WIDTH: 82px" align="right"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 91px" align="center"></TD>
								<TD style="WIDTH: 78px" align="center"></TD>
								<TD style="WIDTH: 62px" align="right">
									<asp:button id="btnNewPlan" runat="server" Font-Size="10pt" Width="80px" Text="确定计划" BorderColor="LemonChiffon"
										BackColor="Orange"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<tr>
					<td align="right">
						<asp:button id="btnAdd" runat="server" Font-Size="10pt" Width="80px" Text="添加单品"></asp:button></td>
				</tr>
				<TR>
					<TD align="center">
						<asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
							Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue"
							Font-Name="Verdana" CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right"
							AllowPaging="True" PageSize="20">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPlanCount" ReadOnly="True" HeaderText="预估总用量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="规格单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMonth" ReadOnly="True" HeaderText="使用月份"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcPlanDeptID" ReadOnly="True" HeaderText="预估门店ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcPlanDeptName" ReadOnly="True" HeaderText="预估门店"></asp:BoundColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
