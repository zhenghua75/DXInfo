<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmEmpEverySchAdd.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmEmpEverySchAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="70%" align="center" border="0">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">Ա���Ű����
							<asp:Label id="lblDeptID" runat="server" Visible="False"></asp:Label></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="70%" align="center" border="0"
					style=" HEIGHT: 188px">
					<TR>
						<TD style="WIDTH: 86px; FONT-SIZE: 10pt" align="right">Ա������</TD>
						<TD style="WIDTH: 170px">
							<asp:ListBox style="Z-INDEX: 0" id="ltbEmp" runat="server" Width="160px" Height="270px"></asp:ListBox></TD>
						<TD style="WIDTH: 92px; FONT-SIZE: 10pt" align="center"><P>
								<asp:Button style="Z-INDEX: 0" id="btSelect" runat="server" Text="ѡ��>>" onclick="btSelect_Click"></asp:Button></P>
							<P>
								<asp:Button style="Z-INDEX: 0" id="btUnselect" runat="server" Text="<<�Ƴ�" onclick="btUnselect_Click"></asp:Button></P>
						</TD>
						<TD>
							<asp:ListBox style="Z-INDEX: 0" id="ltbSelect" runat="server" Width="160px" Height="270px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 86px; FONT-SIZE: 10pt" align="right">�Ű�����</TD>
						<TD style="WIDTH: 170px">
							<asp:TextBox id="txtSchDate" runat="server" Height="24px" Width="136px" Font-Size="10pt" MaxLength="30"
								style="Z-INDEX: 0"></asp:TextBox></TD>
						<TD style="WIDTH: 92px; FONT-SIZE: 10pt" align="right"></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 86px; FONT-SIZE: 10pt" align="right"><FONT face="����">�ŵ�</FONT></TD>
						<TD style="WIDTH: 170px">
							<asp:TextBox id="txtDept" runat="server" Height="24px" Width="136px" Font-Size="10pt" MaxLength="18"></asp:TextBox></TD>
						<TD style="WIDTH: 92px; FONT-SIZE: 10pt" align="right">���</TD>
						<TD>
							<asp:dropdownlist id="ddlClass" runat="server" Height="24px" Width="144px" Font-Size="10pt"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 86px; FONT-SIZE: 10pt" align="right">�ϰ�ʱ��</TD>
						<TD style="WIDTH: 170px">
							<asp:DropDownList id="ddlInHour" runat="server" Width="56px" Font-Size="10pt"></asp:DropDownList>ʱ
							<asp:DropDownList id="ddlInMinute" runat="server" Width="56px" Font-Size="10pt"></asp:DropDownList>��</TD>
						<TD style="WIDTH: 92px; FONT-SIZE: 10pt" align="right">�°�ʱ��</TD>
						<TD>
							<asp:CheckBox style="Z-INDEX: 0" id="chcSecDay" runat="server" Font-Size="10pt" Text="����" Enabled="False"></asp:CheckBox>&nbsp;
							<asp:DropDownList id="ddlOutHour" runat="server" Width="56px" Font-Size="10pt"></asp:DropDownList>ʱ
							<asp:DropDownList id="ddlOutMinute" runat="server" Width="56px" Font-Size="10pt"></asp:DropDownList>��</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 271px" align="right" colSpan="2">
							<asp:Button id="btadd" runat="server" Width="64px" Font-Size="10pt" Text="���" onclick="btadd_Click"></asp:Button></TD>
						<TD align="left" colSpan="2">
							<asp:Button id="btcancel" runat="server" Width="56px" Font-Size="10pt" Text="����" onclick="btcancel_Click"></asp:Button></TD>
					</TR>
				</TABLE>
</asp:Content>