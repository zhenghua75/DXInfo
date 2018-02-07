<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmEmpUnitSign.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmEmpUnitSign" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
个人考勤查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">个人考勤查询</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">员工卡号</asp:label></TD>
								<TD style="WIDTH: 156px">
									<asp:TextBox id="txtCardID" runat="server" Font-Size="10pt" MaxLength="4"></asp:TextBox></TD>
								<TD style="WIDTH: 113px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">查询月份</asp:label></TD>
								<TD style="WIDTH: 243px">
									<asp:TextBox id="txtMonth" runat="server" Font-Size="10pt" Width="136px"></asp:TextBox><FONT face="宋体"><FONT color="#ff6600" size="2">&nbsp;如：200801</FONT></FONT></TD>
								<TD>
									<asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="查询" onclick="btQuery_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 171px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt">员工姓名</asp:label></TD>
								<TD style="WIDTH: 156px">
									<asp:TextBox id="txtEmpName" runat="server" Font-Size="10pt"></asp:TextBox></TD>
								<TD style="WIDTH: 113px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt">查询类型</asp:label></TD>
								<TD style="WIDTH: 243px"><FONT face="宋体">
										<asp:DropDownList id="ddlType" runat="server" Font-Size="10pt" Width="168px"></asp:DropDownList></FONT></TD>
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