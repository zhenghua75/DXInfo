<%@ Page language="c#" Codebehind="Exit.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Exit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>退出系统</title>
		<SCRIPT language="javascript">
		<!--
			function exit(){
				document.Form1.submit();
			}
		//-->
		</SCRIPT>
	</HEAD>
	<body>
		<form name='Form1' action='default.aspx' target='_top'>
			<FONT face="宋体"></FONT>
		</form>
		<%
		if (Request.QueryString["xclose"]!="yes")
		{
		%>		
		<SCRIPT>
			exit();
		</SCRIPT>		
		<%
		}
		else
		{
		%>
		<SCRIPT>
			window.opener = null;
			window.close();
		</SCRIPT>
		<%
		}
		%>
	</body>
</HTML>
