<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmBalanceQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmBalanceQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
结算报表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<LINK rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">结算报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center">
						<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1" >
							<TR>
								<TD style="WIDTH: 256px"><asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label><INPUT 
            style="WIDTH: 160px; HEIGHT: 22px" id=txtBegin 
            onfocus=HS_setDate(this) value="<%=strBeginDate%>" readOnly size=21 
            name=txtBegin>
								</TD>
								<TD style="WIDTH: 540px">&nbsp;
									<asp:label style="Z-INDEX: 0" id="Label6" runat="server" Font-Size="10pt">门店</asp:label>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Font-Size="10pt" Width="144px" AutoPostBack="True"></asp:dropdownlist>
								</TD>
								<TD><FONT face="宋体">
										<asp:Label id="Label3" runat="server" Width="409px" ForeColor="Red">所填的天数，是以当前日期之前多少天的数据重新结算！</asp:Label></FONT></TD>
								<TD></TD>
							</TR>
							<tr>
								<td style="WIDTH: 256px">
									<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label><INPUT 
            style="Z-INDEX: 0; WIDTH: 160px; HEIGHT: 22px" id=txtEnd 
            onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=21 
            name=txtEnd></td>
								<td style="WIDTH: 540px"><asp:button style="Z-INDEX: 0" id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</FONT>
									<asp:button style="Z-INDEX: 0" id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出" onclick="btnExcel_Click"></asp:button></td>
								<td><asp:button style="Z-INDEX: 0" id="btnProcBalance" runat="server" Text="计算结算数据" ForeColor="Red"></asp:button><asp:label style="Z-INDEX: 0" id="Label1" runat="server"></asp:label><asp:textbox style="Z-INDEX: 0" id="txtDays" runat="server" Width="73px"></asp:textbox>
									<asp:Label id="Label2" runat="server" ForeColor="Red">天</asp:Label></td>
								<td><FONT face="宋体"></FONT></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center"><FONT face="宋体"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="4" BackColor="White"
								BorderWidth="1px" BorderStyle="None" BorderColor="#CC9966">
								<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
								<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="vcLocalDeptName" HeaderText="发卡门店"></asp:BoundColumn>
									<asp:BoundColumn DataField="vcRemoteDeptName" HeaderText="门店"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillFee_Pay" HeaderText="充值金额(支出)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillProm_Pay" HeaderText="充值赠送金额(支出)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFee_Pay" HeaderText="消费金额(支出)"></asp:BoundColumn>
									<asp:BoundColumn DataField="sumFee_Pay" HeaderText="小计(支出)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillFee_Income" HeaderText="充值金额(收入)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillProm_Income" HeaderText="充值赠送金额(收入)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFee_Income" HeaderText="消费金额(收入)"></asp:BoundColumn>
									<asp:BoundColumn DataField="sumFee_Income" HeaderText="小计(收入)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillFee_Dif" HeaderText="充值金额(差额)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFillProm_Dif" HeaderText="充值赠送金额(差额)"></asp:BoundColumn>
									<asp:BoundColumn DataField="nFee_Dif" HeaderText="消费金额(差额)"></asp:BoundColumn>
									<asp:BoundColumn DataField="sumFee_Dif" HeaderText="小计(差额)"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
							</asp:datagrid></FONT></TD>
				</TR>
			</TABLE>
</asp:Content>