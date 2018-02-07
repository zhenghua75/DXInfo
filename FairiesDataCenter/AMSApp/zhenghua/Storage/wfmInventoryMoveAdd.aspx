<%@ Page language="c#" Codebehind="wfmInventoryMoveAdd.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmInventoryMoveAdd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmInventoryMoveAdd</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center"><asp:label id="lbltitle" runat="server" Height="24px" Width="347px">调拨单内容</asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="0" cellSpacing="10" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">调拨单号</TD>
					<TD style="WIDTH: 136px"><asp:textbox id="txtMoveCode" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:textbox></TD>
					<TD style="WIDTH: 40px"><asp:textbox style="Z-INDEX: 0" id="txtRdID" runat="server" Height="24px" Width="37px" Font-Size="10pt"></asp:textbox></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">单据类型</TD>
					<TD><asp:dropdownlist style="Z-INDEX: 0" id="ddlRdCode" runat="server" Width="136px" Font-Size="10pt"></asp:dropdownlist></TD>
				</TR>
				<TR id="troutwh" runat="server">
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">调拨出部门</TD>
					<TD style="WIDTH: 136px"><asp:dropdownlist style="Z-INDEX: 0" id="ddlOutDeptID" runat="server" Width="136px" Font-Size="10pt"></asp:dropdownlist></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">调拨出仓库</TD>
					<TD style="WIDTH: 40px"><asp:dropdownlist style="Z-INDEX: 0" id="ddlOutWhouse" runat="server" Width="136px" Font-Size="10pt"
							AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR id="troutdate" runat="server">
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">库管员</TD>
					<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtOutWHPerson" runat="server" Height="24px" Width="135px"
							Font-Size="10pt"></asp:textbox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">出库日期</TD>
					<TD style="WIDTH: 40px"><INPUT style="WIDTH: 136px; HEIGHT: 22px" id=txtOutArvDate onfocus=HS_setDate(this) value="<%=strOutArriveDate%>" readOnly size=17 name=txtOutArvDate></TD>
				</TR>
				<TR id="trinwh" runat="server">
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">调拨入部门</TD>
					<TD style="WIDTH: 136px"><asp:dropdownlist style="Z-INDEX: 0" id="ddlInDeptID" runat="server" Width="136px" Font-Size="10pt"
							AutoPostBack="True"></asp:dropdownlist></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">调拨入仓库</TD>
					<TD style="WIDTH: 40px"><asp:dropdownlist style="Z-INDEX: 0" id="ddlInWhouse" runat="server" Width="136px" Font-Size="10pt"
							AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR id="trindate" runat="server">
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">库管员</TD>
					<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtInWHPerson" runat="server" Height="24px" Width="135px"
							Font-Size="10pt"></asp:textbox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">入库日期</TD>
					<TD style="WIDTH: 40px"><INPUT style="WIDTH: 136px; HEIGHT: 22px" id=txtInArvDate onfocus=HS_setDate(this) value="<%=strInArriveDate%>" readOnly size=17 name=txtInArvDate></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; HEIGHT: 39px; FONT-SIZE: 10pt" align="right">发货地址</TD>
					<TD style="HEIGHT: 39px" colSpan="4"><asp:textbox id="txtShipAddress" runat="server" Height="27px" Width="464px" Font-Size="10pt"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; HEIGHT: 39px; FONT-SIZE: 10pt" align="right">到货地址</TD>
					<TD style="HEIGHT: 39px" colSpan="4"><asp:textbox id="txtArvAddress" runat="server" Height="27px" Width="464px" Font-Size="10pt"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">备注</TD>
					<TD colSpan="4"><asp:textbox id="txtComments" runat="server" Height="64px" Width="464px" Font-Size="10pt" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center"><asp:button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加"></asp:button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px" align="center"><asp:button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:button></TD>
					<TD align="center"><INPUT type="button" style="CURSOR:hand" value="返 回" onClick="javascript:window.history.go(-1);"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
