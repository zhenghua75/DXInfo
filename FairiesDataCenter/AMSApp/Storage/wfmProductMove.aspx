<%@ Page language="c#" Codebehind="wfmProductMove.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProductMove" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProductMove</title>
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
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">产品调拨</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label2" runat="server" Width="40px" Font-Size="10pt">调出店</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:DropDownList id="ddlOutDept" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label5" runat="server" Width="55px" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 168px"><FONT face="宋体"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin></FONT></TD>
								<TD style="WIDTH: 23px"></TD>
								<TD style="WIDTH: 78px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
								<TD>
									<asp:button id="btnAdd" runat="server" Width="80px" Text="新调拨单"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label1" runat="server" Width="40px" Font-Size="10pt">调入店</asp:label></TD>
								<TD style="WIDTH: 124px">
									<asp:DropDownList id="ddlInDept" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label3" runat="server" Width="55px" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 168px"><FONT face="宋体"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd></FONT></TD>
								<TD style="WIDTH: 23px"></TD>
								<TD style="WIDTH: 78px"></TD>
								<TD></TD>
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
								<asp:BoundColumn DataField="cnnMoveSerialNo" ReadOnly="True" HeaderText="调拨流水"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOutDeptID" ReadOnly="True" HeaderText="调出店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOutOperID" ReadOnly="True" HeaderText="调出人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInDeptID" ReadOnly="True" HeaderText="调入店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInOperID" ReadOnly="True" HeaderText="调入人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndMoveDate" ReadOnly="True" HeaderText="调拨日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcBillState" ReadOnly="True" HeaderText="工单状态"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="调拨单维护" DataNavigateUrlField="cnnMoveSerialNo" DataNavigateUrlFormatString="wfmProductMoveDetail.aspx?id={0}"
									HeaderText="查看"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
