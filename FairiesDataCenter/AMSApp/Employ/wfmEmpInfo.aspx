<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmEmpInfo.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmEmpInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
员工基本信息维护
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table3" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">员工基本信息维护</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 184px" align="right">
										<asp:label id="Label1" runat="server" Width="60px" Font-Size="10pt">员工卡号</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtCardID" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 132px" align="right">
										<asp:label id="Label2" runat="server" Width="34px" Font-Size="10pt">状态</asp:label></TD>
									<TD style="WIDTH: 118px">
										<asp:dropdownlist id="ddlstate" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 127px"></TD>
									<TD style="WIDTH: 105px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 184px" align="right">
										<asp:label id="Label3" runat="server" Width="54px" Font-Size="10pt">员工姓名</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtEmpName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 132px" align="right">
										<asp:label id="Label4" runat="server" Width="34px" Font-Size="10pt">门店</asp:label></TD>
									<TD style="WIDTH: 118px">
										<asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px" Height="24px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 127px"></TD>
									<TD style="WIDTH: 105px">
										<asp:button id="btQuery" runat="server" Width="64px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
									<TD>
										<asp:button id="btnExcel" runat="server" Width="64px" Text="导出" 
                                            onclick="btnExcel_Click"></asp:button></TD>
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