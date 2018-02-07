<%@ Page language="c#" Codebehind="wfmDeptStorageEnterAdd.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmDeptStorageEnterAdd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptStorageEnterAdd</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">
						<asp:Label id="lbltitle" runat="server" Width="347px" Height="24px">分货入库单内容</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" border="0" cellSpacing="10" cellPadding="5" width="700" align="center">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">分货入库单号</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtEnterCode" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px">
						<asp:TextBox style="Z-INDEX: 0" id="txtRdID" runat="server" Width="37px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">部门</TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList style="Z-INDEX: 0" id="ddlDeptID" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">仓库</TD>
					<TD style="WIDTH: 40px">
						<asp:DropDownList style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">库管员</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtWHPerson" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">入库日期</TD>
					<TD style="WIDTH: 40px"><INPUT style="WIDTH: 136px; HEIGHT: 22px" 
      id=txtArvDate onfocus=HS_setDate(this) value="<%=strArriveDate%>" readOnly size=17 name=txtArvDate></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; HEIGHT: 39px; FONT-SIZE: 10pt" align="right">发货地址</TD>
					<TD style="HEIGHT: 39px" colSpan="4">
						<asp:TextBox id="txtShipAddress" runat="server" Width="464px" Height="27px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; HEIGHT: 39px; FONT-SIZE: 10pt" align="right">到货地址</TD>
					<TD style="HEIGHT: 39px" colSpan="4">
						<asp:TextBox id="txtArvAddress" runat="server" Width="464px" Height="27px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">备注</TD>
					<TD colSpan="4">
						<asp:TextBox id="txtComments" runat="server" Width="464px" Height="64px" Font-Size="10pt" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加"></asp:Button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 107px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:Button></TD>
					<TD align="center">
						<INPUT type="button" style="CURSOR:hand" value="返 回" onClick="javascript:window.history.go(-1);"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
