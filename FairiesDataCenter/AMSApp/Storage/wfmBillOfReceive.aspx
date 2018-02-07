<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmBillOfReceive.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfReceive" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfReceive</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">原材料领用</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 8px" align="right">
									<asp:label id="Label1" runat="server" Width="57px" Font-Size="10pt">领料单位</asp:label></TD>
								<TD style="WIDTH: 124px; HEIGHT: 8px">
									<asp:DropDownList id="ddlReceiveDept" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px; HEIGHT: 8px" align="right">
									<asp:label id="Label5" runat="server" Width="55px" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 175px; HEIGHT: 8px"><FONT face="宋体"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin></FONT></TD>
								<TD style="WIDTH: 99px; HEIGHT: 8px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 157px; HEIGHT: 8px">
									<asp:button id="btnRelativeMakeBill" runat="server" Width="112px" Text="关联制令领料单"></asp:button></TD>
								<TD style="WIDTH: 101px; HEIGHT: 8px"></TD>
								<TD style="HEIGHT: 8px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt" Width="57px">工单状态</asp:label></TD>
								<TD style="WIDTH: 124px"><FONT face="宋体">
										<asp:DropDownList id="ddlOrderState" runat="server" Width="176px"></asp:DropDownList></FONT></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label3" runat="server" Width="55px" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 175px"><FONT face="宋体"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd></FONT></TD>
								<TD style="WIDTH: 99px"></TD>
								<TD style="WIDTH: 157px">
									<asp:button id="btnAddNonMake" runat="server" Width="112px" Text="门店领料单"></asp:button></TD>
								<TD style="WIDTH: 101px"><FONT face="宋体">
										<asp:button id="btnSend" runat="server" Width="56px" Text="发货"></asp:button></FONT></TD>
								<TD style="HEIGHT: 8px"><FONT face="宋体">
										<asp:button id="btnPrint" runat="server" Width="80px" Text="打印发货单"></asp:button></FONT></TD>
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
								<asp:BoundColumn DataField="cnvcBillState" ReadOnly="True" HeaderText="工单状态"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="领料单维护" DataNavigateUrlField="cnnReceiveSerialNo" DataNavigateUrlFormatString="wfmBillOfReceiveDetail.aspx?id={0}"
									HeaderText="查看"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
