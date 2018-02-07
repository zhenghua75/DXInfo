<%@ Page language="c#" Codebehind="wfmWelcome.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmWelcome" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmWelcome</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<asp:Label id="lblwel" style="Z-INDEX: 101; LEFT: 352px; POSITION: absolute; TOP: 192px" runat="server"
					Width="384px" Height="120px" Font-Size="XX-Large" ForeColor="DodgerBlue">Label</asp:Label></FONT>
			<div align="center" runat="server" id="divt">
				<table border="0" width="354" height="163" cellspacing="0" cellpadding="0">
					<tr>
						<td height="17" width="354">
							<img border="0" src="pop/top.jpg" width="354" height="17"></td>
					</tr>
					<tr>
						<td width="354" background="pop/center.jpg" valign="top">
							<div align="center">
								<table border="0" width="82%" style="FONT-SIZE: 14px; COLOR: #ffffff; LINE-HEIGHT: 150%; FONT-FAMILY: 宋体"
									height="304">
									<tr>
										<td height="23">
											<font color="#ffff00" size="4" face="黑体">通知：</font></td>
									</tr>
									<tr>
										<td height="158">
											<P>&nbsp;&nbsp;&nbsp;&nbsp;<%=strComments%><br>
												&nbsp;&nbsp;&nbsp; 谢谢合作！</P>
											<P>面包港湾网络中心<%=strReleaseDate%>&nbsp;&nbsp;&nbsp;&nbsp;</P>
										</td>
									</tr>
									<tr>
										<td>
											<p align="right">
												<img border="0" width="104" height="46" src="pop/logo.jpg"></p>
										</td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
					<tr>
						<td height="15" width="354">
							<img border="0" src="pop/buttom.jpg" width="354" height="15"></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
