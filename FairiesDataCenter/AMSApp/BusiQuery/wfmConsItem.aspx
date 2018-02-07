<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmConsItem.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmConsItem" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
消费统计查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">消费统计查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr>
					<td>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 83px; HEIGHT: 18px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt">会员类型</asp:label></TD>
								<TD style="WIDTH: 141px; HEIGHT: 18px"><asp:dropdownlist id="ddlAssType" runat="server" Font-Size="10pt" Height="24px" Width="112px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 96px; HEIGHT: 18px" align="right">
									<asp:label id="Label9" runat="server" Font-Size="10pt">消费流水</asp:label></TD>
								<TD style="WIDTH: 136px; HEIGHT: 18px"><asp:textbox id="txtSerial" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 81px; HEIGHT: 18px" align="right">
									<asp:label id="Label8" runat="server" Font-Size="10pt">有效状态</asp:label></TD>
								<TD style="WIDTH: 163px; HEIGHT: 18px">
									<asp:dropdownlist id="ddlConsFlag" runat="server" Font-Size="10pt" Width="128px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 96px; HEIGHT: 18px" align="right">
									<asp:label id="Label10" runat="server" Font-Size="10pt">付费类型</asp:label></TD>
								<TD style="HEIGHT: 18px">
									<asp:dropdownlist id="ddlBillType" runat="server" Font-Size="10pt" Width="120px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 83px" align="right"><FONT face="宋体"><asp:label id="Label2" runat="server" Font-Size="10pt">会员卡号</asp:label></FONT></TD>
								<TD style="WIDTH: 141px"><asp:textbox id="txtCardID" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 96px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 136px"><input id="txtBegin" name="txtBegin" value="<%=strBeginDate%>" onfocus="HS_setDate(this)" type="text" readonly size="13" style="WIDTH: 112px; HEIGHT: 22px"></TD>
								<TD style="WIDTH: 81px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">门店</asp:label></TD>
								<TD style="WIDTH: 163px"><asp:dropdownlist id="ddlDept" runat="server" AutoPostBack="True" Font-Size="10pt" Width="128px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 96px"></TD>
								<TD><FONT face="宋体"><asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 83px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt">会员姓名</asp:label></TD>
								<TD style="WIDTH: 141px">
									<asp:textbox id="txtAssName" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 96px" align="right"><asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
								<TD style="WIDTH: 136px"><input id="txtEnd" name="txtEnd" value="<%=strEndDate%>" onfocus="HS_setDate(this)" type="text" readonly size="13" style="WIDTH: 112px; HEIGHT: 22px"></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt">操作员</asp:label></TD>
								<TD style="WIDTH: 163px">
									<asp:dropdownlist id="ddlOper" runat="server" Font-Size="10pt" Width="128px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 96px"></TD>
								<TD>
									<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblSum" runat="server" Font-Size="12pt" Width="90%" ForeColor="#C00000"></asp:label></TD>
				</TR>
			</TABLE>
</asp:Content>