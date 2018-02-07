<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSpecConsQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmSpecConsQuery" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
特殊消费统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">特殊消费统计</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD>
						<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 93px; HEIGHT: 25px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt">特殊类型</asp:label></TD>
								<TD style="WIDTH: 151px; HEIGHT: 25px">
									<asp:dropdownlist id="ddlConsType" runat="server" Font-Size="10pt" Height="24px" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px; HEIGHT: 25px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">开始时间</asp:label></TD>
								<TD style="WIDTH: 125px; HEIGHT: 25px"><INPUT id=txtBegin onfocus=HS_setDate(this) 
            value="<%=strBeginDate%>" readOnly size=12 name=txtBegin style="Z-INDEX: 0; WIDTH: 104px; HEIGHT: 21px"></TD>
								<TD style="WIDTH: 69px; HEIGHT: 25px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt" style="Z-INDEX: 0">门店</asp:label></TD>
								<TD style="WIDTH: 200px; HEIGHT: 25px">
									<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="168px" AutoPostBack="True" style="Z-INDEX: 0"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 25px">
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 93px" align="right"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 151px"></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt" style="Z-INDEX: 0">结束时间</asp:label></TD>
								<TD style="WIDTH: 125px"><INPUT id=txtEnd onfocus=HS_setDate(this) 
            value="<%=strEndDate%>" readOnly size=12 name=txtEnd style="Z-INDEX: 0; WIDTH: 104px; HEIGHT: 21px"></TD>
								<TD style="WIDTH: 69px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt" style="Z-INDEX: 0">操作员</asp:label></TD>
								<TD style="WIDTH: 200px">
									<asp:dropdownlist id="ddlOper" runat="server" Font-Size="10pt" Width="168px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
								<TD><FONT face="宋体">
										<asp:button style="Z-INDEX: 0" id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></FONT></TD>
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