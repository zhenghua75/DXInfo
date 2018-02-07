<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmFalse.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmFalse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
错误
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<LINK href="../css/window.css" type="text/css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<table style="WIDTH: 426px; HEIGHT: 500px" height="500" align="center">
				<tr>
					<td>
						<TABLE class="table_title_group" cellSpacing="1" cellPadding="3" width="100%" align="center"
							border="0">
							<TR>
								<TD align="left"><FONT face="宋体" size="Medium">错误提示：</FONT></TD>
							</TR>
						</TABLE>
						<TABLE class="table_content_group" style="HEIGHT: 53px" cellSpacing="0" cellPadding="3"
							width="100%" align="center" border="1">
							<TR>
								<TD align="left" width="10%"><FONT face="宋体">&nbsp;
										<asp:Image id="Image1" runat="server" ImageUrl="image/fail.gif"></asp:Image></FONT></TD>
								<TD align="center" width="90%"><asp:label id="lbMessage" runat="server" ForeColor="Red" Font-Size="Medium"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE style="HEIGHT: 110px" cellSpacing="1" cellPadding="3" width="100%" align="center"
							border="0">
							<TR>
								<TD align="center">
									<INPUT type="button" style="CURSOR:hand" value="返  回" onClick="javascript:window.location='/'"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
</asp:Content>