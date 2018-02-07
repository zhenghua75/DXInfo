<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmTopQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmTopQuery" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
销售排名榜
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">销售排名榜</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 129px" align="right"><FONT face="宋体">
										<asp:label id="Label7" runat="server" Font-Size="10pt">统计类型</asp:label></FONT></TD>
								<TD style="WIDTH: 151px">
									<asp:dropdownlist id="ddlType" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 282px"><INPUT id=txtBegin onfocus=HS_setDate(this) type=text readonly size=11 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 151px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 282px"><INPUT id=txtEnd onfocus=HS_setDate(this) type=text readonly size=11 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD><FONT face="宋体">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></FONT></TD>
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