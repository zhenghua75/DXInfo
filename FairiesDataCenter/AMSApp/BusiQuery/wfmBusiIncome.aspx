<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmBusiIncome.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmBusiIncome" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
业务量统计报表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">业务量统计报表</TD>
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
									<asp:label id="Label5" runat="server" Font-Size="10pt">开始日期</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtBegin onfocus=HS_setDate(this) readonly value="<%=strBeginDate%>" name=txtBegin style="WIDTH: 152px; HEIGHT: 22px"></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 171px" align="right"></TD>
								<TD style="WIDTH: 156px"></TD>
								<TD style="WIDTH: 81px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">结束日期</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT id=txtEnd onfocus=HS_setDate(this) readonly value="<%=strEndDate%>" name=txtEnd style="WIDTH: 152px; HEIGHT: 22px"></TD>
								<TD><FONT face="宋体">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD colspan="5" style="COLOR: #cc0000; FONT-SIZE: 10pt">注：原状态中的可用积分和金额与查询门店无关，所显示值为所有会员截止到开始日期的积分和余额总和。<br>
									&nbsp;&nbsp;&nbsp; 现状态中的可用积分和金额与查询门店无关，所显示值为所有会员截止到结束日期的积分和余额总和。<br>
									&nbsp;&nbsp;&nbsp; 新入会员中的可用积分和金额与查询门店无关，所显示值为查询日期范围内的新入会员截止到结束日期的积分和余额总和。
								</TD>
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