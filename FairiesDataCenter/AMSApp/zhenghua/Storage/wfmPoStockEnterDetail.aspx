<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmPoStockEnterDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmPoStockEnterDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmPoStockEnterDetail</title>
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
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">采购入库单明细</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
								<TR>
									<TD style="WIDTH: 104px" align="right"><asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="85px" Font-Size="10pt">采购入库单号</asp:label></TD>
									<TD style="WIDTH: 166px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtCode" runat="server" Width="160px" Font-Size="10pt"></asp:textbox></TD>
									<td style="WIDTH: 86px" align="right">
										<asp:label style="Z-INDEX: 0" id="Label5" runat="server" Font-Size="10pt" Width="58px">部门</asp:label></td>
									<TD style="WIDTH: 139px" align="left">
										<asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Font-Size="10pt" Width="129px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 174px" align="left"></TD>
									<TD style="WIDTH: 121px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtRdID" runat="server" Width="62px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 136px"></TD>
									<td>
<asp:textbox style="Z-INDEX: 0" id=txtgopage runat="server" Font-Size="10pt" Width="62px" Visible="False"></asp:textbox></td>
								</TR>
								<TR>
									<TD style="WIDTH: 104px; HEIGHT: 17px" align="right"><asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="58px" Font-Size="10pt">仓库</asp:label></TD>
									<TD style="WIDTH: 166px; HEIGHT: 17px; COLOR: blue; FONT-SIZE: 10pt" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="129px" Font-Size="10pt"></asp:dropdownlist></TD>
									<td style="WIDTH: 86px; HEIGHT: 17px" align="right"></td>
									<TD style="WIDTH: 139px; HEIGHT: 17px" align="left"></TD>
									<TD style="WIDTH: 85px; HEIGHT: 17px" align="right"><INPUT style="Z-INDEX: 0; WIDTH: 104px; HEIGHT: 24px" id="btnSelected" onclick="ShowHide('1',null);return false;"
											value="关联采购订单" type="button" name="btnSelected"></TD>
									<TD style="WIDTH: 121px; HEIGHT: 17px" align="left"><asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="79px" Height="23px" Text="查询明细"></asp:button></TD>
									<TD style="WIDTH: 136px; HEIGHT: 17px"><asp:button style="Z-INDEX: 0" id="btEnterComf" runat="server" Width="78px" Height="24px" Text="确定入库"></asp:button></TD>
									<TD style="HEIGHT: 17px"><INPUT type="button" style="CURSOR:hand" value="返 回" onClick="javascript:window.history.go(<%=gopage%>);"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1">
					<TR>
						<TD align="center"><asp:datagrid style="Z-INDEX: 0" id="Datagrid2" runat="server" Font-Size="X-Small" AutoGenerateColumns="False"
								BorderColor="Black" BorderWidth="1px" CellPadding="3" PagerStyle-HorizontalAlign="Right" Font-Name="Verdana"
								HeaderStyle-BackColor="SteelBlue" AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AllowPaging="True"
								ForeColor="Blue" Caption="采购入库子单明细表" CaptionAlign="Left">
								<FooterStyle Wrap="False"></FooterStyle>
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
								<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="cnnAutoID" ReadOnly="True" HeaderText="子表标识"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnnRdID" ReadOnly="True" HeaderText="入库主表标识"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcPOID" ReadOnly="True" HeaderText="对应订单号"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcProviderID" ReadOnly="True" HeaderText="供应商"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="存货编码"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="存货名称"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnPrice" HeaderText="单价"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcGroupCode" ReadOnly="True" HeaderText="计量单位组"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcComunitCode" ReadOnly="True" HeaderText="采购单位"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnPoCount" ReadOnly="True" HeaderText="订单数量"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnQuantity" HeaderText="入库数量"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="入库数量">
										<ItemTemplate>
											<asp:textbox id="txtQuantity" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cnnCost" ReadOnly="True" HeaderText="金额"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnExtraCost" HeaderText="其它金额"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="其它金额">
										<ItemTemplate>
											<asp:textbox id="txtExtraCost" runat="server" Font-Size="10pt" Width="80px"></asp:textbox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cndMdate" HeaderText="生产日期"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="生产日期">
										<ItemTemplate>
											<INPUT style="Z-INDEX: 0; WIDTH: 80px; HEIGHT: 22px" runat=server id=txtMDate onfocus=HS_setDate(this) value="<%=strMDate%>" readOnly size=17 name=txtMDate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cndExpDate" HeaderText="过期日期"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="过期日期">
										<ItemTemplate>
											<INPUT style="Z-INDEX: 0; WIDTH: 80px; HEIGHT: 22px" runat=server id=txtExpDate onfocus=HS_setDate(this) value="<%=strExpDate%>" readOnly size=17 name=txtExpDate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="cnvcCommens" HeaderText="备注"></asp:BoundColumn>
									<asp:EditCommandColumn ButtonType="PushButton" UpdateText="更新" HeaderText="修改" CancelText="取消" EditText="修改"></asp:EditCommandColumn>
									<asp:ButtonColumn Text="入库" ButtonType="PushButton" HeaderText="确定入库" CommandName="SelectEnter"></asp:ButtonColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcFlag" ReadOnly="True" HeaderText="入库标志"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" border="0" cellSpacing="1" cellPadding="1" width="95%">
					<TR>
						<TD align="center"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
					</TR>
				</TABLE>
				<DIV id="xMsg11" onmousedown="getFocus(this)">
					<DIV id="xMsg21" onmouseup="stopDrag(this)" onmousemove="drag(this)" onmousedown="startDrag(this)"><SPAN id="xMsgs11">查询采购订单</SPAN>
						<SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="min(this)">0</SPAN> <SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="ShowHide(1,null)">r</SPAN>
					</DIV>
					<DIV id="xMsg31">
						<TABLE align="center">
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label10" runat="server">供应商编码：</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:dropdownlist style="Z-INDEX: 0" id="ddlProviderQuery" runat="server" Width="154px" Font-Size="10pt"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label11" runat="server">采购订单号：</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryPoID" runat="server"></asp:textbox></TD>
								<TD><asp:button id="Button1" runat="server" Text="查询"></asp:button></TD>
							</TR>
						</TABLE>
						<TABLE width="100%" align="center">
							<TR>
								<TD><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
										BorderColor="#3366CC" BorderWidth="1px" CellPadding="4" BackColor="White" BorderStyle="None">
										<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnvcPoID" HeaderText="采购订单号"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcPrvdCode" HeaderText="供应商"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcComments" HeaderText="备注"></asp:BoundColumn>
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
