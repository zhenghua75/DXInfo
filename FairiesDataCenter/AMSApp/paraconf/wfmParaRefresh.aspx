<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmParaRefresh.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmParaRefresh" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
系统参数刷新
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
        <base target="_self" />
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table3" cellSpacing="10" cellPadding="20" width="931" border="0" style="WIDTH: 931px; HEIGHT: 320px">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033; HEIGHT: 52px" align="center">系统参数刷新</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="btrefresh" runat="server" Text="参数刷新" Width="92px" Height="35px" Font-Size="14pt" onclick="btrefresh_Click"></asp:Button></TD>
				</TR>
			</TABLE>
</asp:Content>