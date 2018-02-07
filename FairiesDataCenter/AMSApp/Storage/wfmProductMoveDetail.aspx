<%@ Page language="c#" Codebehind="wfmProductMoveDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProductMoveDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProductMoveDetail</title>
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
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">调拨单细节</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 87px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">调出店：</asp:label></TD>
								<TD style="WIDTH: 136px"><asp:dropdownlist id="ddlOutDept" runat="server" Font-Size="10pt" Width="181px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 105px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">调入店：</asp:label></TD>
								<TD style="WIDTH: 14px"><asp:dropdownlist id="ddlInDept" runat="server" Font-Size="10pt" Width="181px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 127px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="72px">调拨日期：</asp:label></TD>
								<TD style="WIDTH: 124px"><INPUT id=txtBegin 
            style="WIDTH: 119px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=14 value="<%=strBeginDate%>" name=txtBegin 
            ></TD>
								<td style="WIDTH: 107px"><asp:textbox id="txtBillState" runat="server" Font-Size="10pt" Width="40px" Visible="False" ReadOnly="True"></asp:textbox><asp:textbox id="txtMoveID" runat="server" Font-Size="10pt" Width="40px" Visible="False" ReadOnly="True"></asp:textbox></td>
								<td></td>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right"><asp:label id="Label4" runat="server" Font-Size="10pt">调出人：</asp:label></TD>
								<TD style="WIDTH: 136px"><asp:textbox id="txtOutOperID" runat="server" Font-Size="10pt" Width="176px"></asp:textbox></TD>
								<TD style="WIDTH: 105px" align="right"><asp:label id="Label9" runat="server" Font-Size="10pt">调入人：</asp:label></TD>
								<TD style="WIDTH: 14px"><asp:textbox id="txtInOperID" runat="server" Font-Size="10pt" Width="176px"></asp:textbox></TD>
								<TD style="WIDTH: 127px" align="right"><asp:button id="btnMoveNew" runat="server" Font-Size="10pt" Width="88px" BackColor="Orange"
										BorderColor="LemonChiffon" Text="调拨单-发货"></asp:button></TD>
								<TD style="WIDTH: 124px" align="center"><asp:button id="btnPrint" runat="server" Font-Size="10pt" Width="54px" Text="打印"></asp:button></TD>
								<TD style="WIDTH: 107px" align="right"><FONT face="宋体">
										<asp:button id="btnCancel" runat="server" Font-Size="10pt" Width="54px" Text="返回"></asp:button></FONT></TD>
								<td></td>
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
								<td style="WIDTH: 241px" align="right" colspan="2">
									<asp:label id="Label10" runat="server" Font-Size="10pt" Width="158px">输入要过滤的产品名称：</asp:label></td>
								<TD style="WIDTH: 180px" align="right">
									<asp:textbox id="txtProductFilter" runat="server" Font-Size="10pt" Width="176px"></asp:textbox></TD>
								<TD style="WIDTH: 146px">
									<asp:button id="btnQueryFilter" runat="server" Font-Size="10pt" Width="54px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 77px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">产品：</asp:label></TD>
								<TD style="WIDTH: 58px"><asp:dropdownlist id="ddlProduct" runat="server" Font-Size="10pt" Width="195px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 158px" align="right"></TD>
								<TD style="WIDTH: 133px" align="left"></TD>
							</TR>
							<TR>
								<td style="WIDTH: 97px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">单位：</asp:label></td>
								<td style="WIDTH: 119px"><asp:textbox id="txtUnit" runat="server" Font-Size="10pt" Width="115px"></asp:textbox></td>
								<TD style="WIDTH: 180px" align="right"><asp:label id="Label7" runat="server" Font-Size="10pt">数量：</asp:label></TD>
								<TD style="WIDTH: 74px"><asp:textbox id="txtCount" runat="server" Font-Size="10pt" Width="96px"></asp:textbox></TD>
								<TD style="WIDTH: 133px" align="right"><asp:label id="Label8" runat="server" Font-Size="10pt">原因：</asp:label></TD>
								<TD style="WIDTH: 133px" align="right"><asp:textbox id="txtComments" runat="server" Font-Size="10pt" Width="266px" Height="48px" TextMode="MultiLine"></asp:textbox></TD>
								<TD style="WIDTH: 158px" align="center"><asp:button id="btnAdd" runat="server" Font-Size="10pt" Width="72px" Text="添加产品"></asp:button></TD>
								<TD style="WIDTH: 133px" align="right"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" BorderColor="Black"
							PagerStyle-HorizontalAlign="Right" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" AllowPaging="True"
							PageSize="20">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComments" ReadOnly="True" HeaderText="原因"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnMoveCount" ReadOnly="True" HeaderText="原数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" HeaderText="损耗量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnRealMoveCount" HeaderText="实际数量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑数量"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
