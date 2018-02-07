<%@ Page language="c#" Codebehind="wfmMaterialOut.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialOut" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialOut</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
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

//-->
		</script>
		<script language='JScript'>
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
//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table2" cellSpacing="1" cellPadding="5" width="60%" align="center" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">原材料出库</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" cellSpacing="10" cellPadding="5" width="60%" align="center" border="0">
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">原材料批次</TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtBatchNo" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 74px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"></TD>
						<TD><FONT face="宋体"></FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">原材料名称</FONT></TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtMaterialName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 74px"><INPUT id="btnSelected" style="WIDTH: 57px; HEIGHT: 24px" onclick="ShowHide('1',null);return false;"
								type="button" value="选择" name="btnSelected"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"><FONT face="宋体">原材料编号</FONT></TD>
						<TD><FONT face="宋体">
								<asp:TextBox id="txtMaterialCode" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">规格</TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtStandardUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD style="WIDTH: 74px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right">单位</TD>
						<TD><FONT face="宋体">
								<asp:TextBox id="txtUnit" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></FONT></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"><FONT face="宋体">单价</FONT></TD>
						<TD style="WIDTH: 136px">
							<asp:TextBox id="txtPrice" runat="server" Height="24px" Width="107px" Font-Size="10pt"></asp:TextBox><FONT face="宋体">元</FONT></TD>
						<TD style="WIDTH: 74px"><FONT face="宋体"></FONT></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right">供应商</TD>
						<TD>
							<asp:TextBox id="txtProviderName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right">原材料类型</TD>
						<TD style="WIDTH: 136px">
							<asp:dropdownlist id="ddlMaterialType" runat="server" Width="136px" Font-Size="10pt" AutoPostBack="True"></asp:dropdownlist></TD>
						<TD style="WIDTH: 74px"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 79px" align="right"><FONT face="宋体">当前数量</FONT></TD>
						<TD>
							<asp:TextBox id="txtCurCount" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD colSpan="5"></TD>
					</TR>
					<TR>
						<TD style="FONT-SIZE: 10pt; WIDTH: 90px" align="right"></TD>
						<TD style="FONT-SIZE: 10pt; WIDTH: 136px" align="right">本次出库数量</TD>
						<TD style="WIDTH: 74px" colSpan="2">
							<asp:TextBox id="txtOutCount" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px" align="center"></TD>
						<TD style="WIDTH: 136px" align="center">
							<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="确定"></asp:Button></TD>
						<TD style="WIDTH: 40px"></TD>
						<TD style="WIDTH: 79px" align="center">
							<asp:Button id="btCancel" runat="server" Width="64px" Font-Size="10pt" Text="取消"></asp:Button></TD>
						<TD align="center"></TD>
					</TR>
				</TABLE>
				<DIV onmousedown="getFocus(this)" id="xMsg11">
					<DIV onmouseup="stopDrag(this)" onmousemove="drag(this)" onmousedown="startDrag(this)"
						id="xMsg21"><SPAN id="xMsgs11">查询原材料</SPAN> <SPAN style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; WIDTH: 12px; COLOR: white; FONT-FAMILY: webdings; BORDER-RIGHT-WIDTH: 0px"
							onclick="min(this)">0</SPAN> <SPAN style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; WIDTH: 12px; COLOR: white; FONT-FAMILY: webdings; BORDER-RIGHT-WIDTH: 0px"
							onclick="ShowHide(1,null)">r</SPAN>
					</DIV>
					<DIV id="xMsg31">
						<TABLE id="Table3" align="center">
							<TR>
								<TD style="FONT-SIZE: 8pt">
									<asp:Label id="Label7" runat="server">原材料编号：</asp:Label></TD>
								<TD style="FONT-SIZE: 8pt">
									<asp:TextBox id="txtQueryMaterialCode" runat="server"></asp:TextBox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 8pt">
									<asp:Label id="Label11" runat="server">原材料名称：</asp:Label></TD>
								<TD style="FONT-SIZE: 8pt">
									<asp:TextBox id="txtQueryMaterialName" runat="server"></asp:TextBox></TD>
								<TD>
									<asp:Button id="btnQuery" runat="server" Text="查询"></asp:Button></TD>
							</TR>
						</TABLE>
						<TABLE id="Table4" width="100%" align="center">
							<TR>
								<TD>
									<asp:DataGrid id="DataGrid1" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
										BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="4">
										<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cnvcBatchNo" HeaderText="原材料批次"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnMaterialCode" HeaderText="原材料编号"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcMaterialName" HeaderText="原材料名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcMaterialType" HeaderText="原材料类型"></asp:BoundColumn>
											<asp:ButtonColumn Text="选择" CommandName="Select"></asp:ButtonColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
									</asp:DataGrid></TD>
							</TR>
						</TABLE>
					</DIV>
				</DIV>
				<DIV id="xMsg41"></DIV>
			</FORM>
		</DIV>
		</FONT>
	</body>
</HTML>
