<%@ Page language="c#" Codebehind="wfmMainTop.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmMainTop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMainTop</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">		
			var Condition = false;
			function window_onunload(url)
			{
			  if(Condition==false)
			  {
			     //window.open(url+'?xclose=yes');
			     //onunload="window_onunload('Exit.aspx')" 清除退出事件处理
			  }			  
			}
			function rd()
			{
				if(confirm("您是否确认退出系统？"))
				{
					window.parent.parent.location.href="default.aspx";
				}
			}
			function openwin(surl)
			{
				//window.showModalDialog(surl,"_new","dialogHeight:360px;dialogWidth:360px;status:no");
				//var win  = 
				window.open(surl,"_new","height=380,width=410,toolbar=no,status=no,left=250,top=10,scrollbars=no,resizable=no");
				//win.focus();
			}	
		</script>
	</HEAD>
	<body onload="<%=strPopStr%>" leftMargin=0 topMargin=0 bgColor=#ccccff ms_positioning="GridLayout">
		<TABLE style="HEIGHT: 114px" id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
			<TR>
				<TD style="BACKGROUND-IMAGE: url(image/banner.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 78px"
					height="78" vAlign="bottom" width="100%" align="right"><asp:label id="Label1" runat="server" ForeColor="RoyalBlue" Font-Size="12pt" Width="180px"
						Height="31px"></asp:label></TD>
			</TR>
			<TR>
				<TD style="BACKGROUND-IMAGE: url(image/banner11.jpg); BACKGROUND-REPEAT: no-repeat"
					bgColor="#ccccff" height="34" borderColor="#ccccff" width="100%" borderColorDark="#99ccff"
					noWrap>
					<TABLE style="Z-INDEX: 0; WIDTH: 945px; HEIGHT: 25px" border="0" width="945" height="25">
						<TR>
							<td style="WIDTH: 142px" vAlign="bottom"><asp:label id="lbloper" runat="server" ForeColor="RoyalBlue" Font-Size="11pt" Width="144px"
									Height="23px"></asp:label></td>
							<td vAlign="bottom">&nbsp;<A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.left.location='wfmParaMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">基本信息</A>&nbsp;&nbsp;&nbsp; <A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.left.location='wfmQueryMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">销售报表</A>&nbsp;&nbsp;&nbsp;
                                    <A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.left.location='wfmStorageMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">库存管理</A>&nbsp;&nbsp;&nbsp;
                                    <A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.left.location='zhenghua/wfmProduceMenu.aspx';parent.right.location='wfmWelcome.aspx';"
									href="javascript:">库存报表</A>&nbsp;&nbsp;&nbsp;
                                    <A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" href="javascript:rd();">注销退出</A>
							</td>
						</TR>
					</TABLE>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
