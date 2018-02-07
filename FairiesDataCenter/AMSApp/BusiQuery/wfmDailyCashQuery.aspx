<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmDailyCashQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmDailyCashQuery" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
操作员当日收银统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">操作员当日收银统计</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD>
						<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 129px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt" style="Z-INDEX: 0">门店</asp:label></TD>
								<TD style="WIDTH: 202px">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Font-Size="10pt" Width="184px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">开始时间</asp:label></TD>
								<TD style="WIDTH: 156px" align="left"><INPUT id=txtBegin onfocus=HS_setDate(this) 
            value="<%=strBeginDate%>" readOnly size=18 name=txtBegin style="Z-INDEX: 0; WIDTH: 144px; HEIGHT: 21px"></TD>
								<TD style="WIDTH: 81px"></TD>
								<TD style="WIDTH: 33px"></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right"><FONT face="宋体">
										<asp:label id="Label7" runat="server" Font-Size="10pt" style="Z-INDEX: 0">操作员</asp:label></FONT></TD>
								<TD style="WIDTH: 202px">
									<asp:dropdownlist id="ddlOper" runat="server" Font-Size="10pt" Width="184px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt" style="Z-INDEX: 0">结束时间</asp:label></TD>
								<TD style="WIDTH: 156px"><INPUT id=txtEnd onfocus=HS_setDate(this) 
            value="<%=strEndDate%>" readOnly size=18 name=txtEnd style="Z-INDEX: 0; WIDTH: 144px; HEIGHT: 21px"></TD>
								<TD style="WIDTH: 81px" align="right"></TD>
								<TD style="WIDTH: 33px"></TD>
								<TD><FONT face="宋体"></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right"></TD>
								<TD style="WIDTH: 202px"></TD>
								<TD style="WIDTH: 171px" align="right"></TD>
								<TD style="WIDTH: 156px"></TD>
								<TD style="WIDTH: 81px" align="right"></TD>
								<TD style="WIDTH: 33px"></TD>
								<TD></TD>
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
			<TABLE id="Table5" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center">
						<asp:label id="lblSum" runat="server" Font-Size="12pt" Width="90%" ForeColor="#C00000"></asp:label></TD>
				</TR>
			</TABLE>
</asp:Content>