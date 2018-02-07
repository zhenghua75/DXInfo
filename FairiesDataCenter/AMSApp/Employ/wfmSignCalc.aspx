<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSignCalc.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmSignCalc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
考勤计算
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">考勤计算</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 156px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtBegin onfocus=HS_setDate(this) readOnly type=text size=11 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="96px" Text="查询计算情况" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 171px" align="right"></TD>
								<TD style="WIDTH: 156px"></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtEnd onfocus=HS_setDate(this) readOnly type=text size=11 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD><FONT face="宋体">
										<asp:button id="btCalc" runat="server" Font-Size="10pt" Width="80px" Text="开始计算" onclick="btCalc_Click"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" height="100" border="0">
				<TR>
					<TD align="center">
						<asp:Label id="lblCalcResult" runat="server" Width="826px" Height="71px" Font-Size="12pt" ForeColor="SlateBlue"></asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="left">
						<asp:Label id="lblQueryTitle" runat="server" Width="600px" Height="16px" Font-Size="12pt" ForeColor="SlateBlue"></asp:Label></TD>
					<TD align="right"><FONT face="宋体">注：蓝色--已计算；红色--未计算</FONT></TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" height="200" border="0">
				<TR>
					<TD align="center">
						<asp:Label id="lbl1" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">1</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl2" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">2</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl3" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">3</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl4" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">4</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl5" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">5</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl6" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">6</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl7" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">7</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl8" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">8</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl9" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">9</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl10" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">10</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Label id="lbl11" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">11</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl12" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">12</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl13" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">13</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl14" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">14</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl15" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">15</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl16" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">16</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl17" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">17</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl18" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">18</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl19" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">19</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl20" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">20</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Label id="lbl21" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">21</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl22" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">22</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl23" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">23</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl24" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">24</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl25" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">25</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl26" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">26</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl27" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">27</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl28" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">28</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl29" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">29</asp:Label></TD>
					<TD align="center">
						<asp:Label id="lbl30" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">30</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Label id="lbl31" runat="server" Font-Size="12pt" ForeColor="Red" Font-Underline="True">31</asp:Label></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
					<TD align="center"><FONT face="宋体"></FONT></TD>
				</TR>
			</TABLE>
</asp:Content>