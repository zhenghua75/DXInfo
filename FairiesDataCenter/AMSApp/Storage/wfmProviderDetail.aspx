<%@ Page language="c#" Codebehind="wfmProviderDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProviderDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProviderDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">Label</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">供应商编码</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtProviderCode" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right"></TD>
					<TD>
						<asp:Button id="btnFind" runat="server" Width="72px" Font-Size="10pt" Text="自动查找" style="Z-INDEX: 0"></asp:Button></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right"><FONT face="宋体" style="Z-INDEX: 0">供应商名称</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtProviderName" runat="server" Width="136px" Height="24px"
							Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"><FONT face="宋体"></FONT></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right"><FONT style="Z-INDEX: 0" face="宋体">供应商简称</FONT></TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtProviderAbbName" runat="server" Width="135px" Height="24px"
							Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">信用等级</TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList style="Z-INDEX: 0" id="ddlCredit" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right">资质证明</TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtQualification" runat="server" Width="136px" Height="24px"
							Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">联系人</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtLinkName" runat="server" Width="136px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right">有效标志</TD>
					<TD>
						<asp:DropDownList style="Z-INDEX: 0" id="ddlActiveFlag" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">电话</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox style="Z-INDEX: 0" id="txtLinkPhone" runat="server" Width="136px" Height="24px"
							Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right">传真</TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtFax" runat="server" Width="136px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">Email地址</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtEmail" runat="server" Height="24px" Width="136px" Font-Size="10pt" style="Z-INDEX: 0"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px; FONT-SIZE: 10pt" align="right">邮政编码</TD>
					<TD>
						<asp:TextBox style="Z-INDEX: 0" id="txtPostCode" runat="server" Width="136px" Height="24px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">地址</TD>
					<TD colSpan="4">
						<asp:TextBox id="txtAddress" runat="server" Width="440px" Height="64px" Font-Size="10pt" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加"></asp:Button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 93px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="btcancel" runat="server" Width="59px" Font-Size="10pt" Text="返回"></asp:Button></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
