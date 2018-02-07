<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmNotice.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmNotice" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
系统通知管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">系统通知管理</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 96px" align="right">
										<asp:label id="Label2" runat="server" Width="69px" Font-Size="10pt">发往部门</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:dropdownlist id="ddlDept" runat="server" Width="168px" Font-Size="10pt" AutoPostBack="True"></asp:dropdownlist></TD>
									<TD style="WIDTH: 545px" align="right">
										<asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
									<TD style="WIDTH: 671px"><INPUT id=txtBegin style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strBeginDate%>" name=txtBegin></TD>
									<TD style="WIDTH: 486px"></TD>
									<TD style="WIDTH: 324px"></TD>
									<TD style="WIDTH: 94px"></TD>
									<TD style="WIDTH: 360px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 96px" align="right">
										<asp:label id="Label1" runat="server" Width="88px" Font-Size="10pt">通知内容</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtContent" runat="server" Width="160px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 545px" align="right">
										<asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></TD>
									<TD style="WIDTH: 671px"><INPUT id=txtEnd style="FONT-SIZE: 10pt; WIDTH: 128px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=16 value="<%=strEndDate%>" name=txtEnd></TD>
									<TD style="WIDTH: 486px">
										<asp:button id="btQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
									<TD style="WIDTH: 324px">
										<asp:button id="btAdd" runat="server" Width="64px" Text="添加"></asp:button></TD>
									<TD style="WIDTH: 94px">
										<asp:button id="btnExcel" runat="server" Width="64px" Text="导出"></asp:button></TD>
									<TD style="WIDTH: 360px"></TD>
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
