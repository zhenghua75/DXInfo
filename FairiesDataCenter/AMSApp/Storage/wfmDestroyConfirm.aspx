<%@ Page language="c#" Codebehind="wfmDestroyConfirm.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmDestroyConfirm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDestroyConfirm</title>
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
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">损耗确认</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"><asp:label id="Label1" runat="server" Width="57px" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 124px"><asp:dropdownlist id="ddlLoseDept" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 97px" align="right"><FONT face="宋体"><asp:label id="Label7" runat="server" Width="57px" Font-Size="10pt">损耗类型</asp:label></FONT></TD>
								<TD style="WIDTH: 207px"><FONT face="宋体"><asp:dropdownlist id="ddlLoseType" runat="server" Width="176px"></asp:dropdownlist></FONT></TD>
								<TD style="WIDTH: 148px"><asp:label id="Label5" runat="server" Width="55px" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 148px"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin 
            ></TD>
								<TD style="WIDTH: 229px"><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right"><asp:label id="Label2" runat="server" Width="57px" Font-Size="10pt">确认标志</asp:label></TD>
								<TD style="WIDTH: 124px"><asp:dropdownlist id="ddlDestroyFlag" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 97px" align="right"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 207px"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 148px"><asp:label id="Label6" runat="server" Width="55px" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 148px"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd 
            ></TD>
								<TD style="WIDTH: 229px"></TD>
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
								<TD style="WIDTH: 110px"></TD>
								<TD style="WIDTH: 130px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">确认人：</asp:label></TD>
								<TD style="WIDTH: 71px"><asp:textbox id="txtConfirmOper" runat="server" Width="155px" Font-Size="10pt" ReadOnly="True"></asp:textbox></TD>
								<TD style="WIDTH: 113px" align="right"><asp:label id="Label3" runat="server" Width="71px" Font-Size="10pt">确认时间：</asp:label></TD>
								<TD style="WIDTH: 213px"><asp:label id="lblConfirmDate" runat="server" Width="208px" Font-Size="10pt"></asp:label></TD>
								<TD style="WIDTH: 179px" align="center"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td style="FONT-SIZE: 10pt; WIDTH: 696px; COLOR: #0000cc">本确认功能只支持单页操作，即最多只支持一页(20条)损耗记录的确认。</td>
					<td style="WIDTH: 201px" align="center"><asp:button id="btnAllSelect" runat="server" Width="104px" Text="单页全选"></asp:button></td>
					<td align="center"><asp:button id="btnBatchConfirm" runat="server" Width="80px" Text="确认提交"></asp:button></td>
					<td width="150"></td>
				</tr>
				<TR>
					<TD align="center" colspan="4"><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="X-Small" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue" AlternatingItemStyle-BackColor="#660033"
							Font-Names="Verdana" AutoGenerateColumns="False" PageSize="20" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:CheckBox id="chkLose" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cnnLoseSerialNo" ReadOnly="True" HeaderText="损耗流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDeptID" ReadOnly="True" HeaderText="门店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseDate" ReadOnly="True" HeaderText="损耗时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" ReadOnly="True" HeaderText="损耗数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcLoseComments" ReadOnly="True" HeaderText="损耗原因"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWeather" ReadOnly="True" HeaderText="天气"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcLoseType" ReadOnly="True" HeaderText="损耗类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDestroyFlag" ReadOnly="True" HeaderText="确认标志"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperID" ReadOnly="True" HeaderText="报损人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseOperDate" ReadOnly="True" HeaderText="报损操作时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDestroyOperID" ReadOnly="True" HeaderText="确认人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndDestroyDate" ReadOnly="True" HeaderText="销毁确认时间"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
