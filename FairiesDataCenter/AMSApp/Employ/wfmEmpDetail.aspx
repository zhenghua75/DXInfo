<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmEmpDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Employ.wfmEmpDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">Label</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">员工卡号</TD>
					<TD style="WIDTH: 170px">
						<asp:TextBox id="txtCardID" runat="server" Height="24px" Width="135px" Font-Size="10pt" MaxLength="4"></asp:TextBox></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 92px" align="right">员工姓名</TD>
					<TD>
						<asp:TextBox id="txtEmpName" runat="server" Height="24px" Width="136px" Font-Size="10pt" MaxLength="30"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right"><FONT face="宋体">身份证号</FONT></TD>
					<TD style="WIDTH: 170px">
						<asp:TextBox id="txtEmpNbr" runat="server" Height="24px" Width="136px" Font-Size="10pt" MaxLength="18"></asp:TextBox></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 92px" align="right">性别</TD>
					<TD>
						<asp:dropdownlist id="ddlSex" runat="server" Width="144px" Height="24px" Font-Size="10pt"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">联系电话</TD>
					<TD style="WIDTH: 170px">
						<asp:TextBox id="txtLinkPhone" runat="server" Height="24px" Width="136px" Font-Size="10pt" MaxLength="25"></asp:TextBox></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 92px" align="right">学历</TD>
					<TD>
						<asp:dropdownlist id="ddlDegree" runat="server" Width="144px" Height="24px" Font-Size="10pt"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">入职时间</TD>
					<TD style="WIDTH: 170px"><INPUT id="txtInDate" style="WIDTH: 136px; HEIGHT: 22px" readOnly type="text" size="17" value="<%=strInDate%>" name="txtInDate"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">当前所属门店</TD>
					<TD>
						<asp:dropdownlist id="ddlDept" runat="server" Width="144px" Height="24px" Font-Size="10pt"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">是否在职</TD>
					<TD style="WIDTH: 170px">
						<asp:dropdownlist id="ddlFlag" runat="server" Width="144px" Height="24px" Font-Size="10pt"></asp:dropdownlist></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">职务</TD>
					<TD>
						<asp:dropdownlist id="ddlOfficer" runat="server" Width="144px" Height="24px" Font-Size="10pt"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px" align="right">联系地址</TD>
					<TD style="WIDTH: 136px" colspan="3">
						<asp:TextBox id="txtAddress" runat="server" Height="88px" Width="447px" Font-Size="10pt" TextMode="MultiLine"
							MaxLength="256"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 86px; HEIGHT: 78px" align="right">备注</TD>
					<TD style="HEIGHT: 78px" colSpan="3">
						<asp:TextBox id="txtComments" runat="server" Height="88px" Width="447px" Font-Size="10pt" TextMode="MultiLine"
							MaxLength="256"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD colspan="2" align="right" style="WIDTH: 271px">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存" onclick="btMod_Click"></asp:Button></TD>
					<TD colspan="2" align="left">
						<asp:Button id="btcancel" runat="server" Width="89px" Font-Size="10pt" Text="返回员工管理" onclick="btcancel_Click"></asp:Button></TD>
				</TR>
			</TABLE>
</asp:Content>