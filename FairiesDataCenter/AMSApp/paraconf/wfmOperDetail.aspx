<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmOperDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmOperDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
��վ����Ա��Ϣ
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px"></asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">��¼ID</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtLoginID" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right">����Ա����</TD>
					<TD>
						<asp:TextBox id="txtOperName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="����">�鿴Ȩ��</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList id="ddlLimit" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right">�ŵ�</TD>
					<TD>
						<asp:DropDownList id="ddlDept" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center">
						<asp:Button id="btAdd" runat="server" Font-Size="10pt" Width="64px" Text="���"></asp:Button></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<td style="WIDTH: 40px"></td>
					<TD style="WIDTH: 79px" align="center">
						<asp:Button id="btDel" runat="server" Width="64px" Font-Size="10pt" Text="ɾ��"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="btcancel" runat="server" Font-Size="10pt" Width="103px" Text="���ز���Ա����"></asp:Button></TD>
				</TR>
				<tr>
					<td colspan="5" style="FONT-SIZE: 10pt; COLOR: #cc0000">������վ����Աʱ������Ĭ��Ϊ��123456</td>
				</tr>
			</TABLE>
</asp:Content>