<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSignQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmSignQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
门店考勤统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">门店考勤统计</TD>
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
										<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 171px" align="right">
										<asp:label id="Label1" runat="server" Font-Size="10pt">查询类别</asp:label></TD>
									<TD style="WIDTH: 156px">
										<asp:dropdownlist id="ddlType" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 81px" align="right">
										<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
									<TD style="WIDTH: 259px"><INPUT id=txtEnd onfocus=HS_setDate(this) readOnly type=text size=11 value="<%=strEndDate%>" name=txtEnd></TD>
									<TD>
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<TR>
						<TD align="center">
							<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
					</TR>
				</TABLE>
</asp:Content>