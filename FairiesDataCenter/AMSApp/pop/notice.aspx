<%@ Page CodeBehind="notice.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="AMSApp.Pop.notice" %>

<HTML>
  <HEAD>
		<title>重要通知</title>
		<meta http-equiv="Content-Language" content="zh-cn">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
  </HEAD>
	<body bgcolor="#99ccff">
		<div align="center">
			<table border="0" width="354" height="163" cellspacing="0" cellpadding="0">
				<tr>
					<td height="17" width="354">
						<img border="0" src="top.jpg" width="354" height="17"></td>
				</tr>
				<tr>
					<td width="354" background="center.jpg" valign="top">
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
											<img border="0" width="104" height="46" src="logo.jpg"></p>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td height="15" width="354">
						<img border="0" src="buttom.jpg" width="354" height="15"></td>
				</tr>
				<tr>
					<td align="center"><a href="javascript:window.close();">关闭</HREF></a></td>
				<tr>
				</tr>
			</table>
		</div>
	</body>
</HTML>