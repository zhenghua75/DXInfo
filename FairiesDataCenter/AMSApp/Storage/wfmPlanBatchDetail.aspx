<%@ Page language="c#" Codebehind="wfmPlanBatchDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPlanBatchDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPlanBatchDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">�ɹ��ƻ�������ϸ</TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">��Ʒ����</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtProductCode" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">��Ʒ����</TD>
					<TD>
						<asp:TextBox id="txtProductName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="����">��λ</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"><FONT face="����"></FONT></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">�ɹ�����</TD>
					<TD>
						<asp:DropDownList id="ddlBatch" runat="server" Width="136px"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">����</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtCount" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">����</TD>
					<TD>
						<asp:TextBox id="txtSumFee" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">��������</TD>
					<TD style="WIDTH: 136px"><INPUT id=txtBegin 
      style="WIDTH: 136px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=17 value="<%=strBeginDate%>" name=txtBegin></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="����">�·�</FONT></TD>
					<TD>
						<asp:TextBox id="txtMonth" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center"></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 90px" align="center">
						<asp:Button id="btcancel" runat="server" Width="68px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD align="center"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
