<%@ Page language="c#" Codebehind="Exit.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Exit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>退出系统</title>	
		<base target="_self">
		<script>window.name="Exit";</script>
	</HEAD>
	<body>
		<form name='Form1' action='/Account/LogOff.aspx' target="Exit">
			<FONT face="宋体"></FONT>
		</form>
		<SCRIPT>
			//window.opener=null
			//window.open("","_self")
			//window.close(); 
			document.Form1.target="_top";
			document.Form1.submit();
		</SCRIPT>
	</body>
</HTML>
