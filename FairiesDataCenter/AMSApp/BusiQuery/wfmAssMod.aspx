<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmAssMod.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmAssMod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
��Ա�����޸�
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">��Ա�����޸�</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="0" cellSpacing="10" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">��Ա����</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtCardID" runat="server" Height="24px" Width="135px" Font-Size="10pt" ReadOnly="True"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right"></TD>
					<TD>
						<asp:TextBox id="txtAssID" runat="server" Height="24px" Width="136px" Font-Size="10pt" Visible="False"
							ReadOnly="True"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="����">��Ա����</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtAssName" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right">ƴ����д</TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtSpell" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="����">���֤��</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtAssNbr" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right">��Ա����</TD>
					<TD>
						<asp:DropDownList ID="ddlAssType" runat="server" Height="20px" 
                            style="Z-INDEX: 0" Width="136px">
                        </asp:DropDownList>
                    </TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="����">��ϵ�绰</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtLinkPhone" runat="server" Height="24px" Width="135px"
							Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="WIDTH: 79px; FONT-SIZE: 10pt" align="right">E-Mail</TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtEmail" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="����">��ϵ��ַ</FONT></TD>
					<TD colspan="4">
						<asp:TextBox style="Z-INDEX: 0" id="txtLinkAddress" runat="server" Height="56px" Width="450px"
							Font-Size="10pt" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">��&nbsp; ע</TD>
					<TD colspan="4">
						<asp:TextBox style="Z-INDEX: 0" id="txtComments" runat="server" Height="56px" Width="450px" Font-Size="10pt"
							TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 79px" align="center"><INPUT type="button" style="CURSOR:hand" value="��  ��" onClick="javascript:window.history.back();"></TD>
					<TD align="center"></TD>
				</TR>
			</TABLE>
</asp:Content>