<%@ Page language="c#" Codebehind="wfmError.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.wfmError" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>错误页面</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td valign="middle" align="center">
						<table>
							<tr>
								<td width="30"><IMG alt="" src="Images/Error.gif" align="absMiddle"></td>
								<td><asp:Label id="lblError" runat="server">抱歉，出错啦，请与系统管理员联系！</asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
