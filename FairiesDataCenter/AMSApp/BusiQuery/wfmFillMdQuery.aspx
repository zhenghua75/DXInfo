<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmFillMdQuery.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.BusiQuery.wfmFillMdQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
充值门店查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">充值门店查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 129px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt">会员类型</asp:label></TD>
								<TD style="WIDTH: 151px">
									<asp:dropdownlist id="ddlAssType" runat="server" Font-Size="10pt" Height="24px" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label8" runat="server" Font-Size="10pt">查询类型</asp:label></TD>
								<TD style="WIDTH: 156px" align="left">
									<asp:dropdownlist id="ddlFillType" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:Label id="Label9" runat="server" Font-Size="10pt">发卡门店</asp:Label></TD>
								<TD style="WIDTH: 259px">
									<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right"><FONT face="宋体">
										<asp:label id="Label2" runat="server" Font-Size="10pt">会员卡号</asp:label></FONT></TD>
								<TD style="WIDTH: 151px">
									<asp:textbox id="txtCardID" runat="server" Font-Size="10pt" Width="142px"></asp:textbox></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 156px"><INPUT id=txtBegin onfocus=HS_setDate(this) type=text readonly size=11 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 259px">
									<asp:dropdownlist id="ddlDept" runat="server" AutoPostBack="True" Font-Size="10pt" Width="144px" onselectedindexchanged="ddlDept_SelectedIndexChanged"></asp:dropdownlist></TD>
								<TD><FONT face="宋体">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right">
									<asp:label id="Label3" runat="server" Font-Size="10pt">会员姓名</asp:label></TD>
								<TD style="WIDTH: 151px">
									<asp:textbox id="txtAssName" runat="server" Font-Size="10pt" Width="142px"></asp:textbox></TD>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 156px"><INPUT id=txtEnd onfocus=HS_setDate(this) type=text readonly size=11 value="<%=strEndDate%>" name=txtEnd></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt">操作员</asp:label></TD>
								<TD style="WIDTH: 259px">
									<asp:dropdownlist id="ddlOper" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></TD>
								<TD></TD>
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
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblSum" runat="server" Font-Size="12pt" Width="90%" ForeColor="#C00000"></asp:label></TD>
				</TR>
			</TABLE>
</asp:Content>
