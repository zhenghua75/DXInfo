<%@ Page language="c#" Codebehind="wfmProvGInfo.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProvGInfo" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProvGInfo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JScript">
			<!--
			//可以打包为js文件;
			var x0=0,y0=0,x1=0,y1=0;
			var offx=6,offy=6;
			var moveable=false;
			var hover='orange',normal='#336699';//color;
			var index=10000;//z-index;
			//开始拖动;
			function startDrag(obj)
			{
			if(event.button==1)
			{
			//锁定标题栏;
			obj.setCapture();
			//定义对象;
			var win = obj.parentNode;
			var sha = win.nextSibling;
			//记录鼠标和层位置;
			x0 = event.clientX;
			y0 = event.clientY;
			x1 = parseInt(win.style.left);
			y1 = parseInt(win.style.top);
			//记录颜色;
			normal = obj.style.backgroundColor;
			//改变风格;
			obj.style.backgroundColor = hover;
			win.style.borderColor = hover;
			obj.nextSibling.style.color = hover;
			sha.style.left = x1 + offx;
			sha.style.top  = y1 + offy;
			moveable = true;
			}
			}
			//拖动;
			function drag(obj)
			{
			if(moveable)
			{
			var win = obj.parentNode;
			var sha = win.nextSibling;
			win.style.left = x1 + event.clientX - x0;
			win.style.top  = y1 + event.clientY - y0;
			sha.style.left = parseInt(win.style.left) + offx;
			sha.style.top  = parseInt(win.style.top) + offy;
			}
			}
			//停止拖动;
			function stopDrag(obj)
			{
			if(moveable)
			{
			var win = obj.parentNode;
			var sha = win.nextSibling;
			var msg = obj.nextSibling;
			win.style.borderColor     = normal;
			obj.style.backgroundColor = normal;
			msg.style.color           = normal;
			sha.style.left = obj.parentNode.style.left;
			sha.style.top  = obj.parentNode.style.top;
			obj.releaseCapture();
			moveable = false;
			}
			}
			//获得焦点;
			function getFocus(obj)
			{
			if(obj.style.zIndex!=index)
			{
			index = index + 2;
			var idx = index;
			obj.style.zIndex=idx;
			obj.nextSibling.style.zIndex=idx-1;
			}
			}
			//最小化;
			function min(obj)
			{
			var win = obj.parentNode.parentNode;
			var sha = win.nextSibling;
			var tit = obj.parentNode;
			var msg = tit.nextSibling;
			var flg = msg.style.display=="none";
			if(flg)
			{
			win.style.height  = parseInt(msg.style.height) + parseInt(tit.style.height) + 2*2;
			sha.style.height  = win.style.height;
			msg.style.display = "block";
			obj.innerHTML = "0";
			}
			else
			{
			win.style.height  = parseInt(tit.style.height) + 2*2;
			sha.style.height  = win.style.height;
			obj.innerHTML = "2";
			msg.style.display = "none";
			}
			}
			//创建一个对象;
			function xWin(id,w,h,l,t)
			{
			index = index+2;
			this.id      = id;
			this.width   = w;
			this.height  = h;
			this.left    = l;
			this.top     = t;
			this.zIndex  = index;
			this.obj     = null;
			}

			//显示隐藏窗口
			function ShowHide(id,dis){
			var bdisplay = (dis==null)?((document.getElementById("xMsg1"+id).style.display=="")?"none":""):dis
			document.getElementById("xMsg1"+id).style.display = bdisplay;
			document.getElementById("xMsg4"+id).style.display = bdisplay;
			}

			-->
		</script>
		<script language="JScript">
			<!--
			function CPos(x, y)
			{
				this.x = x;
				this.y = y;
			}
			//获取控件的位置
			function GetObjPos(ATarget)
			{
				var target = ATarget;
				var pos = new CPos(target.offsetLeft, target.offsetTop);
			    
				var target = target.offsetParent;
				while (target)
				{
					pos.x += target.offsetLeft;
					pos.y += target.offsetTop;
			        
					target = target.offsetParent
				}
			    
				return pos;
			}

			function initialize()
			{

			var pos=GetObjPos(document.getElementById("btnSelected"));
			var xMsg = new xWin("1",400,200,pos.x,pos.y+document.getElementById("btnSelected").offsetHeight);
			document.getElementById("xMsg1"+xMsg.id).style.zIndex=xMsg.zIndex;
			document.getElementById("xMsg1"+xMsg.id).style.width=xMsg.width;
			document.getElementById("xMsg1"+xMsg.id).style.height=xMsg.height;
			document.getElementById("xMsg1"+xMsg.id).style.left=xMsg.left;
			document.getElementById("xMsg1"+xMsg.id).style.top=xMsg.top;
			document.getElementById("xMsg1"+xMsg.id).style.position="absolute";
			  
			document.getElementById("xMsg1"+xMsg.id).style.backgroundColor=normal;
			document.getElementById("xMsg1"+xMsg.id).style.color=normal;
			document.getElementById("xMsg1"+xMsg.id).style.fontSize="10pt";
			document.getElementById("xMsg1"+xMsg.id).style.fontFamily="Tahoma";
			document.getElementById("xMsg1"+xMsg.id).style.cursor="default";
			document.getElementById("xMsg1"+xMsg.id).style.border="2px solid " + normal;

			document.getElementById("xMsg2"+xMsg.id).style.backgroundColor=normal;
			document.getElementById("xMsg2"+xMsg.id).style.width=xMsg.width-2*2;
			document.getElementById("xMsg2"+xMsg.id).style.height=20;
			document.getElementById("xMsg2"+xMsg.id).style.color="white";

			document.getElementById("xMsgs1"+xMsg.id).style.width =xMsg.width-2*12-20;
			document.getElementById("xMsgs1"+xMsg.id).style.paddingLeft="3px";
			 
			document.getElementById("xMsg3"+xMsg.id).style.width="100%";
			document.getElementById("xMsg3"+xMsg.id).style.height=xMsg.height-20-4;
			document.getElementById("xMsg3"+xMsg.id).style.backgroundColor="white";
			document.getElementById("xMsg3"+xMsg.id).style.lineHeight="8px";
			document.getElementById("xMsg3"+xMsg.id).style.padding="3px";
			    
			document.getElementById("xMsg4"+xMsg.id).style.zIndex=xMsg.zIndex-1;
			document.getElementById("xMsg4"+xMsg.id).style.width=xMsg.width;
			document.getElementById("xMsg4"+xMsg.id).style.height=xMsg.height;
			document.getElementById("xMsg4"+xMsg.id).style.left=xMsg.left;
			document.getElementById("xMsg4"+xMsg.id).style.top=xMsg.top;
			document.getElementById("xMsg4"+xMsg.id).style.position="absolute";

			document.getElementById("xMsg4"+xMsg.id).style.backgroundColor="black";
			document.getElementById("xMsg4"+xMsg.id).style.filter="alpha(opacity=40)";

			//ShowHide("1","none");//隐藏窗口1
			}
			window.onload = initialize;
			-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">供应商存货信息</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
								<TR>
									<TD style="WIDTH: 98px; HEIGHT: 27px" align="right"><asp:label id="Label1" runat="server" Width="72px" Font-Size="10pt">供应商编码</asp:label></TD>
									<TD style="WIDTH: 124px; HEIGHT: 27px"><asp:textbox id="txtProviderID" runat="server" Width="112px" Font-Size="10pt" Enabled="False"
											ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 68px; HEIGHT: 27px" align="right"></TD>
									<TD style="WIDTH: 98px; HEIGHT: 27px" align="right"><FONT face="宋体"><asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="72px" Font-Size="10pt">供应商名称</asp:label></FONT></TD>
									<TD style="WIDTH: 168px; HEIGHT: 27px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtProviderName" runat="server" Width="157px" Font-Size="10pt"
											Enabled="False" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 93px; HEIGHT: 27px"></TD>
									<TD style="WIDTH: 147px; HEIGHT: 27px"></TD>
									<TD style="HEIGHT: 27px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 98px" align="right"><asp:label id="Label3" runat="server" Width="93px" Font-Size="10pt">供应货品编码</asp:label></TD>
									<TD style="WIDTH: 124px"><asp:textbox id="txtGoodsCode" runat="server" Width="112px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 68px" align="left"><INPUT style="Z-INDEX: 0; WIDTH: 57px; HEIGHT: 24px" id="btnSelected" onclick="ShowHide('1',null);return false;"
											value="选择" type="button" name="btnSelected"></TD>
									<TD style="WIDTH: 98px" align="right"><FONT face="宋体"><asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="88px" Font-Size="10pt">供应货品名称</asp:label></FONT></TD>
									<TD style="WIDTH: 168px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtGoodsName" runat="server" Width="157px" Font-Size="10pt"
											Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 93px" align="right">
										<asp:button style="Z-INDEX: 0" id="btnAdd" runat="server" Width="64px" Text="添加"></asp:button></TD>
									<TD style="WIDTH: 147px; HEIGHT: 27px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 98px" align="right"><asp:label id="Label6" runat="server" Width="85px" Font-Size="10pt">采购计量单位</asp:label></TD>
									<TD style="WIDTH: 124px">
										<asp:textbox style="Z-INDEX: 0" id="txtStockUnit" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
									<TD style="WIDTH: 68px" align="left"></TD>
									<TD style="WIDTH: 98px" align="right"><FONT face="宋体"><asp:label style="Z-INDEX: 0" id="Label7" runat="server" Width="63px" Font-Size="10pt">供货单价</asp:label></FONT></TD>
									<TD style="WIDTH: 168px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtStockPrice" runat="server" Width="156px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 93px" align="right"></TD>
									<TD style="WIDTH: 147px; HEIGHT: 27px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 98px" align="right">
										<asp:label style="Z-INDEX: 0" id="Label8" runat="server" Font-Size="10pt" Width="35px" Height="14px">厂商</asp:label></TD>
									<TD style="WIDTH: 189px" colspan="2">
										<asp:textbox style="Z-INDEX: 0" id="txtProducer" runat="server" Font-Size="10pt" Width="193px"
											Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 98px" align="right"><FONT face="宋体">
											<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Font-Size="10pt" Width="60px" Height="14px">有效状态</asp:label></FONT></TD>
									<TD style="WIDTH: 168px" align="left">
										<asp:DropDownList style="Z-INDEX: 0" id="ddlInvalidFlag" runat="server" Font-Size="10pt" Width="112px"
											Height="22px"></asp:DropDownList></TD>
									<TD style="WIDTH: 93px" align="right">
										<asp:button style="Z-INDEX: 0" id="btnSave" runat="server" Width="64px" Text="保存"></asp:button></TD>
									<TD style="WIDTH: 147px; HEIGHT: 27px"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
					<TR>
						<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
					</TR>
				</TABLE>
				<DIV id="xMsg11" onmousedown="getFocus(this)">
					<DIV id="xMsg21" onmouseup="stopDrag(this)" onmousemove="drag(this)" onmousedown="startDrag(this)"><SPAN id="xMsgs11">查询存货档案</SPAN>
						<SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="min(this)">0</SPAN> <SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="ShowHide(1,null)">r</SPAN>
					</DIV>
					<DIV id="xMsg31">
						<TABLE align="center">
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label10" runat="server">货品编号：</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryGoodsCode" runat="server"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 8pt">
									<asp:Label id="Label11" runat="server">货品名称：</asp:Label></TD>
								<TD style="FONT-SIZE: 8pt">
									<asp:TextBox id="txtQueryGoodsName" runat="server"></asp:TextBox></TD>
								<TD><asp:button id="Button1" runat="server" Text="查询"></asp:button></TD>
							</TR>
						</TABLE>
						<TABLE width="100%" align="center">
							<TR>
								<TD><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
										BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="4">
										<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnvcGoodsCode" HeaderText="货品编号"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcGoodsName" HeaderText="货品名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcStockUnit" HeaderText="采购单位"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnStockPrice" HeaderText="供货单价"></asp:BoundColumn>
											<asp:ButtonColumn Text="选择" CommandName="Select"></asp:ButtonColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</DIV>
				</DIV>
				<DIV id="xMsg41"></DIV>
			</FORM>
		</DIV>
	</body>
</HTML>
