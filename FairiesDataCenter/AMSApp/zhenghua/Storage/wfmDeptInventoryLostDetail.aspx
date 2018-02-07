<%@ Page language="c#" Codebehind="wfmDeptInventoryLostDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmDeptInventoryLostDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptInventoryLostDetail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../../js/calendar.js"></SCRIPT>
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
			var xMsg = new xWin("1",480,200,pos.x,pos.y+document.getElementById("btnSelected").offsetHeight);
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
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="700" align="center">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center"><asp:label id="lbltitle" runat="server" Width="347px" Height="24px">过期损耗上报</asp:label></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" border="0" cellSpacing="10" cellPadding="5" width="700" align="center">
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">部门</TD>
						<TD style="WIDTH: 136px"><asp:dropdownlist style="Z-INDEX: 0" id="ddlDeptID" runat="server" Width="136px" Font-Size="10pt"></asp:dropdownlist></TD>
						<TD style="WIDTH: 101px"><asp:textbox style="Z-INDEX: 0" id="txtSerialNo" runat="server" Width="49px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">仓库</TD>
						<TD><asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="136px" Font-Size="10pt"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">存货编码</TD>
						<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtInvCode" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 101px"><INPUT style="Z-INDEX: 0; WIDTH: 62px; HEIGHT: 24px" id="btnSelected" onclick="ShowHide('1',null);return false;"
								value="选择存货" type="button" name="btnSelected"></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">存货名称</TD>
						<TD style="WIDTH: 40px"><asp:textbox style="Z-INDEX: 0" id="txtInvName" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">计量单位</TD>
						<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtUnit" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 101px"><asp:textbox style="Z-INDEX: 0" id="txtUnitCode" runat="server" Width="52px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">损耗数量</TD>
						<TD style="WIDTH: 40px"><asp:textbox style="Z-INDEX: 0" id="txtLostCount" runat="server" Width="135px" Height="24px"
								Font-Size="10pt"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">生产日期</TD>
						<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtMdate" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 101px"></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">过期日期</TD>
						<TD style="WIDTH: 40px"><asp:textbox style="Z-INDEX: 0" id="txtExpDate" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">调增量</TD>
						<TD style="WIDTH: 136px"><asp:textbox style="Z-INDEX: 0" id="txtAddCount" runat="server" Width="135px" Height="24px" Font-Size="10pt"></asp:textbox></TD>
						<TD style="WIDTH: 101px"></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right">调减量</TD>
						<TD style="WIDTH: 40px"><asp:textbox style="Z-INDEX: 0" id="txtReduceCount" runat="server" Width="135px" Height="24px"
								Font-Size="10pt"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">损耗日期</TD>
						<TD style="WIDTH: 136px"><INPUT 
      style="Z-INDEX: 0; WIDTH: 136px; HEIGHT: 22px" id=txtLostDate 
      onfocus=HS_setDate(this) value="<%=strLostDate%>" readOnly size=17 
      name=txtLostDate></TD>
						<TD style="WIDTH: 101px"></TD>
						<TD style="WIDTH: 107px; FONT-SIZE: 10pt" align="right"></TD>
						<TD style="WIDTH: 40px"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px; FONT-SIZE: 10pt" align="right">备注</TD>
						<TD colSpan="4"><asp:textbox id="txtComments" runat="server" Width="464px" Height="64px" Font-Size="10pt" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px" align="center"></TD>
						<TD style="WIDTH: 136px" align="center"><asp:button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="添加"></asp:button></TD>
						<TD style="WIDTH: 101px"><asp:button style="Z-INDEX: 0" id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存"></asp:button></TD>
						<TD style="WIDTH: 107px" align="center"><asp:button style="Z-INDEX: 0" id="btConfirm" runat="server" Width="64px" Font-Size="10pt" Text="确认损耗"></asp:button></TD>
						<TD style="WIDTH: 136px" align="center"><asp:button id="btReturn" runat="server" Width="64px" Font-Size="10pt" Text="返回"></asp:button></TD>
					</TR>
				</TABLE>
				<DIV id="xMsg11" onmousedown="getFocus(this)">
					<DIV id="xMsg21" onmouseup="stopDrag(this)" onmousemove="drag(this)" onmousedown="startDrag(this)"><SPAN id="xMsgs11">查询损耗存货</SPAN>
						<SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="min(this)">0</SPAN> <SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="ShowHide(1,null)">r</SPAN>
					</DIV>
					<DIV id="xMsg31">
						<TABLE id="Table6" align="center">
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label11" runat="server">存货编码：</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryInvCode" runat="server"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label1" runat="server">存货名称：</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryInvName" runat="server"></asp:textbox></TD>
								<TD><asp:button style="Z-INDEX: 0" id="Button1" runat="server" Text="查询"></asp:button></TD>
							</TR>
						</TABLE>
						<TABLE id="Table7" width="100%" align="center">
							<TR>
								<TD><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
										BorderColor="#3366CC" BorderWidth="1px" CellPadding="4" BackColor="White" BorderStyle="None">
										<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnvcInvCode" HeaderText="存货编码"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcInvName" HeaderText="存货名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcGroupCode" HeaderText="计量单位组"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cnvcSTComUnitCode" HeaderText="计量单位编码"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcComUnitName" HeaderText="计量单位"></asp:BoundColumn>
											<asp:BoundColumn DataField="cndMdate" HeaderText="生产日期"></asp:BoundColumn>
											<asp:BoundColumn DataField="cndExpDate" HeaderText="过期日期"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnQuantity" HeaderText="结存数量"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnStopQuantity" HeaderText="冻结数量"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnAvaQuantity" HeaderText="可用数量"></asp:BoundColumn>
											<asp:ButtonColumn Text="添加" CommandName="Select"></asp:ButtonColumn>
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
