<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmLoginPwd.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmLoginPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
����Ա�����޸�
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="60%" align="center" border="0"
				height="40">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">����Ա�����޸�</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="561" align="center" border="1"
				style="WIDTH: 561px; HEIGHT: 168px">
				<TR>
					<TD width="40%" align="center" style="FONT-SIZE: 10pt">��¼ID</TD>
					<TD width="60%">
						<asp:TextBox id="txtLoginID" runat="server" Height="24px" Width="184px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="40%" align="center" style="FONT-SIZE: 10pt">������</TD>
					<TD width="60%">
						<asp:TextBox id="txtNewPwd" runat="server" Height="24px" Width="184px" Font-Size="10pt" TextMode="Password"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="40%" align="center" style="FONT-SIZE: 10pt">������ȷ��</TD>
					<TD width="60%" align="left">
						<asp:TextBox id="txtNewPwdConf" runat="server" Height="24px" Width="184px" Font-Size="10pt" TextMode="Password"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="40%" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD width="60%" align="center">
						<asp:Button id="btClose" runat="server" Width="64px" Font-Size="10pt" Text="�ر�"></asp:Button></TD>
				</TR>
			</TABLE>
</asp:Content>