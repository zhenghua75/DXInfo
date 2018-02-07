<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSignQuerySLink.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmSignQuerySLink" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">门店考勤明细情况</TD>
					</TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 171px" align="right">
										<asp:label id="Label5" runat="server" Font-Size="10pt" style="Z-INDEX: 0">开始时间</asp:label></TD>
									<TD style="WIDTH: 156px">
										<asp:TextBox style="Z-INDEX: 0" id="txtBegin" runat="server" Font-Size="10pt" ReadOnly="True"></asp:TextBox></TD>
									<TD style="WIDTH: 81px" align="right">
										<asp:label id="Label4" runat="server" Font-Size="10pt" style="Z-INDEX: 0">结束时间</asp:label></TD>
									<TD style="WIDTH: 259px">
										<asp:TextBox style="Z-INDEX: 0" id="txtEnd" runat="server" Font-Size="10pt" ReadOnly="True"></asp:TextBox></TD>
									<TD style="WIDTH: 101px">
										<asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="56px" Text="导出" style="Z-INDEX: 0"></asp:button></TD>
									<TD><INPUT type="button" style="CURSOR:hand" value="返  回" onClick="javascript:window.history.back();"></TD>
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