<%@ Page language="c#" Codebehind="wfmBillOfEnterStorageDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfEnterStorageDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfEnterStorageDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">进仓单细节</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 278px; HEIGHT: 6px" align="right" colspan="2"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="206px">输入要过滤的原材料名称：</asp:label></TD>
								<TD style="WIDTH: 152px; HEIGHT: 6px" align="right" colspan="2"><asp:textbox id="txtMaterialFilter" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 128px; HEIGHT: 6px" align="left">
									<asp:button id="btnQueryFilter" runat="server" Width="54px" Font-Size="10pt" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 202px; HEIGHT: 6px"></TD>
								<TD style="WIDTH: 144px; HEIGHT: 6px" align="right"></TD>
								<TD style="WIDTH: 421px; HEIGHT: 6px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt" Width="52px">供应商：</asp:label></TD>
								<TD style="WIDTH: 188px"><asp:dropdownlist id="ddlProvider" runat="server" Font-Size="10pt" AutoPostBack="true" Width="181px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 78px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">日期：</asp:label></TD>
								<TD style="WIDTH: 3px"><INPUT id=txtBegin style="WIDTH: 148px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=18 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD style="WIDTH: 128px" align="right"><FONT face="宋体">
										<asp:label id="Label14" runat="server" Font-Size="10pt" Width="88px">进仓类型：</asp:label></FONT></TD>
								<TD style="WIDTH: 202px">
									<asp:dropdownlist id="ddlEnterType" runat="server" Font-Size="10pt" Width="168px" AutoPostBack="false"></asp:dropdownlist></TD>
								<td style="WIDTH: 144px"></td>
								<TD style="WIDTH: 421px; HEIGHT: 6px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="64px">送货员：</asp:label></TD>
								<TD style="WIDTH: 188px"><asp:textbox id="txtDeliverMan" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 78px" align="right"><asp:label id="Label4" runat="server" Font-Size="10pt">验收：</asp:label></TD>
								<TD style="WIDTH: 3px"><asp:textbox id="txtValidateOper" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 128px" align="right"><asp:label id="Label9" runat="server" Font-Size="10pt" Width="48px">保安：</asp:label></TD>
								<TD style="WIDTH: 202px"><asp:textbox id="txtSafeOper" runat="server" Font-Size="10pt" Width="160px"></asp:textbox></TD>
								<td style="WIDTH: 144px"></td>
								<TD style="WIDTH: 421px; HEIGHT: 6px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right"><asp:label id="Label12" runat="server" Font-Size="10pt" Width="64px">仓管：</asp:label></TD>
								<TD style="WIDTH: 188px"><asp:textbox id="txtStorageOper" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 78px" align="right"><asp:label id="Label13" runat="server" Font-Size="10pt">打单：</asp:label></TD>
								<TD style="WIDTH: 3px"><asp:textbox id="txtBillOper" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 128px" align="right"><FONT face="宋体"><asp:button id="btnEnterNew" runat="server" Font-Size="10pt" Width="96px" Text="进仓单-确定" BorderColor="LemonChiffon"
											BackColor="Orange" ForeColor="ControlText"></asp:button></FONT></TD>
								<TD style="WIDTH: 202px" align="center"><FONT face="宋体">
										<asp:button id="btnCancel" runat="server" Width="54px" Font-Size="10pt" Text="返回"></asp:button></FONT></TD>
								<td style="WIDTH: 144px">
									<asp:button id="btnPrint" runat="server" Width="54px" Font-Size="10pt" Text="打印"></asp:button></td>
								<TD style="WIDTH: 421px; HEIGHT: 6px"></TD>
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
								<TD style="WIDTH: 149px" align="right"><asp:label id="Label15" runat="server" Font-Size="10pt">原材料名称：</asp:label></TD>
								<TD style="WIDTH: 168px"><asp:dropdownlist id="ddlProduct" runat="server" Font-Size="10pt" AutoPostBack="true" Width="160px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 143px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">进仓规格：</asp:label></TD>
								<TD style="WIDTH: 190px"><asp:textbox id="txtStandardUnit" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 115px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">规格数量：</asp:label></TD>
								<TD style="WIDTH: 267px">
									<asp:textbox id="txtStandardCount" runat="server" Width="148px" Font-Size="10pt"></asp:textbox>
									<asp:label id="lblUnit" runat="server" Width="56px" Font-Size="10pt"></asp:label></TD>
								<TD style="WIDTH: 62px" align="right"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 149px" align="right">
									<asp:label id="Label11" runat="server" Font-Size="10pt">供货价格：</asp:label></TD>
								<TD style="WIDTH: 168px">
									<asp:textbox id="txtPrice" runat="server" Width="148px" Font-Size="10pt"></asp:textbox></TD>
								<TD style="WIDTH: 143px" align="right"><FONT face="宋体"><asp:label id="Label7" runat="server" Font-Size="10pt">数量：</asp:label></FONT></TD>
								<TD style="WIDTH: 190px"><asp:textbox id="txtCount" runat="server" Font-Size="10pt" Width="149px"></asp:textbox></TD>
								<TD style="WIDTH: 115px" align="right"><asp:label id="Label8" runat="server" Font-Size="10pt">金额：</asp:label></TD>
								<TD style="WIDTH: 267px"><asp:textbox id="txtSum" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 62px" align="right">
									<asp:button id="btnAdd" runat="server" Width="72px" Font-Size="10pt" Text="添加产品"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="X-Small" BorderColor="Black"
							PagerStyle-HorizontalAlign="Right" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False"
							PageSize="20" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProviderCode" ReadOnly="True" HeaderText="供应商编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProviderName" ReadOnly="True" HeaderText="供应商名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="进仓规格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStandardCount" ReadOnly="True" HeaderText="规格数量"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="供货价格"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
