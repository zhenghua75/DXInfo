<%@ Page language="c#" Codebehind="wfmMaterialPara.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialPara" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialPara</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">
							<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px">Label</asp:Label></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">原材料批次</FONT></TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtBatchNo" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 40px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"><FONT face="宋体"></FONT></TD>
						<TD><FONT face="宋体"></FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">原材料名称</FONT></TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtMaterialName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 40px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"><FONT face="宋体">规格</FONT></TD>
						<TD><FONT face="宋体">
								<asp:TextBox id="txtStandardUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">单位</FONT></TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 40px"><FONT face="宋体"></FONT></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right">单价</TD>
						<TD>
							<asp:TextBox id="txtPrice" runat="server" Height="24px" Width="118px" Font-Size="10pt"></asp:TextBox><FONT face="宋体">元</FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">供应商</TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtProviderName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 40px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"><FONT face="宋体">原材料类型</FONT></TD>
						<TD>
							<asp:dropdownlist id="ddlMaterialType" runat="server" Width="136px" Font-Size="10pt" AutoPostBack="True"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px" align="center">
							<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加"></asp:Button></TD>
						<TD style="WIDTH: 136px" align="center">
							<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:Button></TD>
						<TD style="WIDTH: 40px"></TD>
						<TD style="WIDTH: 79px" align="center">
							<asp:Button id="btDel" runat="server" Width="64px" Font-Size="10pt" Text="作废"></asp:Button></TD>
						<TD align="center">
							<asp:Button id="btcancel" runat="server" Width="89px" Font-Size="10pt" Text="返回材料管理"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="5">
							<asp:Label id="lblPromt" runat="server" Width="512px" ForeColor="Red"></asp:Label></TD>
					</TR>
				</TABLE>
			</FORM>
		</DIV>
	</body>
</HTML>
