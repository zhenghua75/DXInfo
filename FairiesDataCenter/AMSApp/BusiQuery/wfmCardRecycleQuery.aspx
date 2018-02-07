<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmCardRecycleQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmCardRecycleQuery" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
会员卡回收情况查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">会员卡回收情况查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD>
						<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 129px" align="right"><FONT face="宋体">
										<asp:label id="Label2" runat="server" Font-Size="10pt">会员卡号</asp:label></FONT></TD>
								<TD style="WIDTH: 134px">
									<asp:textbox id="txtCardID" runat="server" Font-Size="10pt" Width="142px"></asp:textbox></TD>
								<TD style="WIDTH: 120px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt" style="Z-INDEX: 0">门店</asp:label></TD>
								<TD style="WIDTH: 182px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px" AutoPostBack="True" style="Z-INDEX: 0"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">开始时间</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT style="Z-INDEX: 0; WIDTH: 136px; HEIGHT: 21px" 
            id=txtBegin onfocus=HS_setDate(this) value="<%=strBeginDate%>" 
            readOnly size=17 name=txtBegin></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right">
									<asp:label id="Label3" runat="server" Font-Size="10pt">会员姓名</asp:label></TD>
								<TD style="WIDTH: 134px">
									<asp:textbox id="txtAssName" runat="server" Font-Size="10pt" Width="142px"></asp:textbox></TD>
								<TD style="WIDTH: 120px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt">电话号码</asp:label></TD>
								<TD style="WIDTH: 182px">
									<asp:textbox style="Z-INDEX: 0" id="txtLinkPhone" runat="server" Font-Size="10pt" Width="142px"></asp:textbox></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt" style="Z-INDEX: 0">结束时间</asp:label></TD>
								<TD style="WIDTH: 259px"><INPUT style="Z-INDEX: 0; WIDTH: 136px; HEIGHT: 21px" 
            id=txtEnd onfocus=HS_setDate(this) value="<%=strEndDate%>" readOnly size=17 name=txtEnd></TD>
								<TD>
									<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center">
						<uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
</asp:Content>