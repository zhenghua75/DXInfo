<%@ Page language="c#" Codebehind="wfmframeset.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmframeset" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<HTML>
	<HEAD>
		<TITLE>面包港湾网络中心</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript">
			function MM_redirectorPage(url){
				window.location.href = url;
				return;
			}

			function MM_reloadPage(init) {  //reloads the window if Nav4 resized
				if (init==true) with (navigator) 
				{
					if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
						document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; 
						}
				}
				else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) 
					location.reload();
			}
			
			MM_reloadPage(true);

			function check(checkmsg,url) {
				if (confirm(checkmsg)) {
					MM_redirectorPage(url);
				}
			}
		</SCRIPT>
	</HEAD>
	<frameset frameSpacing="0" border="1" frameBorder="0" rows="114,63%">
		<frame src="wfmMainTop.aspx" name="top" scrolling="no">
		<frameset id="frame" frameSpacing="0" border="1" cols="136,85%" frameBorder="0">
			<frame noResize marginHeight="0" src="wfmParaMenu.aspx" name="left" marginWidth="0" scrolling="no">
			<frame src="wfmWelcome.aspx" name="right">
		</frameset>
	</frameset>
</HTML>