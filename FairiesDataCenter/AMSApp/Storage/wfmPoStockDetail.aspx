<%@ Page language="c#" Codebehind="wfmPoStockDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPoStockDetail" %>
<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmPoStockDetail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
		<script language="JScript">
			<!--
			//���Դ��Ϊjs�ļ�;
			var x0=0,y0=0,x1=0,y1=0;
			var offx=6,offy=6;
			var moveable=false;
			var hover='orange',normal='#336699';//color;
			var index=10000;//z-index;
			//��ʼ�϶�;
			function startDrag(obj)
			{
			if(event.button==1)
			{
			//����������;
			obj.setCapture();
			//�������;
			var win = obj.parentNode;
			var sha = win.nextSibling;
			//��¼���Ͳ�λ��;
			x0 = event.clientX;
			y0 = event.clientY;
			x1 = parseInt(win.style.left);
			y1 = parseInt(win.style.top);
			//��¼��ɫ;
			normal = obj.style.backgroundColor;
			//�ı���;
			obj.style.backgroundColor = hover;
			win.style.borderColor = hover;
			obj.nextSibling.style.color = hover;
			sha.style.left = x1 + offx;
			sha.style.top  = y1 + offy;
			moveable = true;
			}
			}
			//�϶�;
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
			//ֹͣ�϶�;
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
			//��ý���;
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
			//��С��;
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
			//����һ������;
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

			//��ʾ���ش���
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
			//��ȡ�ؼ���λ��
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

			//ShowHide("1","none");//���ش���1
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
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">�ɹ��ƻ���ϸ</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" border="1" cellSpacing="0" cellPadding="0" width="95%">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
								<TR>
									<TD style="WIDTH: 83px" align="right"><asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="72px" Font-Size="10pt">�ɹ��ƻ���</asp:label></TD>
									<TD style="WIDTH: 166px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtPoID" runat="server" Width="160px" Font-Size="10pt"></asp:textbox></TD>
									<td style="WIDTH: 69px">
										<asp:textbox style="Z-INDEX: 0" id="txtState" runat="server" Font-Size="10pt" Width="62px" Visible="False"></asp:textbox></td>
									<TD style="WIDTH: 69px" align="right"><asp:label style="Z-INDEX: 0" id="Label5" runat="server" Width="40px" Font-Size="10pt">��λ</asp:label></TD>
									<TD style="WIDTH: 131px" align="left">
										<asp:dropdownlist style="Z-INDEX: 0" id="ddlStockUnit" runat="server" Font-Size="10pt" Width="113px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 85px" align="right"><asp:label style="Z-INDEX: 0" id="Label8" runat="server" Width="70px" Font-Size="10pt">������λ��</asp:label></TD>
									<TD style="WIDTH: 186px" align="left">
										<asp:dropdownlist style="Z-INDEX: 0" id="ddlUnitGroup" runat="server" Font-Size="10pt" Width="152px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 95px"><asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" Width="79px" Text="��ѯ��ϸ" Height="23px"></asp:button></TD>
									<td></td>
								</TR>
								<TR>
									<TD style="WIDTH: 83px; HEIGHT: 17px" align="right"><asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="58px" Font-Size="10pt">��Ӧ��</asp:label></TD>
									<TD style="WIDTH: 166px; HEIGHT: 17px; COLOR: blue; FONT-SIZE: 10pt" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlProvider" runat="server" Width="160px" Font-Size="10pt"></asp:dropdownlist></TD>
									<td style="WIDTH: 69px; HEIGHT: 17px"><asp:textbox style="Z-INDEX: 0" id="txtGoodsCode" runat="server" Width="62px" Font-Size="10pt"
											Visible="False"></asp:textbox></td>
									<TD style="WIDTH: 69px; HEIGHT: 17px" align="right"><asp:label style="Z-INDEX: 0" id="Label6" runat="server" Width="40px" Font-Size="10pt">����</asp:label></TD>
									<TD style="WIDTH: 131px; HEIGHT: 17px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtStockPrice" runat="server" Width="112px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 85px; HEIGHT: 17px" align="right"><asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="69px" Font-Size="10pt">�¶�����</asp:label></TD>
									<TD style="WIDTH: 186px; HEIGHT: 17px" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlDeptID" runat="server" Width="152px" Font-Size="10pt"
											Height="22px"></asp:dropdownlist></TD>
									<TD style="WIDTH: 95px; HEIGHT: 17px">
<asp:textbox style="Z-INDEX: 0" id=txtgopage runat="server" Font-Size="10pt" Width="62px" Visible="False"></asp:textbox></TD>
									<TD style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 83px" align="right"><asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="73px" Font-Size="10pt" Height="14px">��Ӧ��Ʒ</asp:label></TD>
									<TD style="WIDTH: 166px; COLOR: blue; FONT-SIZE: 10pt" align="left"><asp:textbox style="Z-INDEX: 0" id="txtGoodsName" runat="server" Width="159px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 69px"><INPUT style="Z-INDEX: 0; WIDTH: 57px; HEIGHT: 24px" id="btnSelected" onclick="ShowHide('1',null);return false;"
											value="ѡ��" type="button" name="btnSelected"></TD>
									<TD style="WIDTH: 69px" align="right"><asp:label style="Z-INDEX: 0" id="Label7" runat="server" Width="42px" Font-Size="10pt" Height="14px">����</asp:label></TD>
									<TD style="WIDTH: 131px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtStockCount" runat="server" Width="112px" Font-Size="10pt"></asp:textbox></TD>
									<TD style="WIDTH: 85px" align="right"><asp:label style="Z-INDEX: 0" id="Label9" runat="server" Width="82px" Font-Size="10pt" Height="2px">�赽������</asp:label></TD>
									<TD style="WIDTH: 186px" align="left"><INPUT 
            style="WIDTH: 112px; HEIGHT: 22px" id=txtArrive 
            onfocus=HS_setDate(this) value="<%=strArriveDate%>" readOnly size=13 
            name=txtArrive></TD>
									<TD style="WIDTH: 95px"><asp:button style="Z-INDEX: 0" id="btnAdd" runat="server" Width="80px" Text="����Ӷ���" Height="23px"></asp:button></TD>
									<td><INPUT type="button" style="CURSOR:hand" value="�� ��" onClick="javascript:window.history.go(<%=gopage%>);"></td>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1">
					<TR>
						<TD align="center"><asp:datagrid style="Z-INDEX: 0" id="Datagrid2" runat="server" Width="100%" Font-Size="X-Small"
								AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" CellPadding="3" PagerStyle-HorizontalAlign="Right"
								Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue" AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana"
								AllowPaging="True" ForeColor="Blue" Caption="�ɹ��Ӽƻ���ϸ��" CaptionAlign="Left">
								<FooterStyle Wrap="False"></FooterStyle>
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
								<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="cnvcPoID" ReadOnly="True" HeaderText="�ɹ��ƻ���"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcDeptID" ReadOnly="True" HeaderText="���ű���"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcDeptName" ReadOnly="True" HeaderText="�¶�����"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcGoodsCode" ReadOnly="True" HeaderText="�ɹ���Ʒ����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcGoodsName" ReadOnly="True" HeaderText="�ɹ���Ʒ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcGroupCode" ReadOnly="True" HeaderText="��λ�����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcGroupName" ReadOnly="True" HeaderText="������λ��"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcStockUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnStockPrice" ReadOnly="True" HeaderText="����"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnStockCount" HeaderText="��������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnStockFee" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndArriveDate" ReadOnly="True" HeaderText="�赽������"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcRowState" ReadOnly="True" HeaderText="״̬"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcCreater" ReadOnly="True" HeaderText="�Ƶ���"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndCreateDate" ReadOnly="True" HeaderText="�Ƶ�ʱ��"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcModer" ReadOnly="True" HeaderText="�޸���"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cndModDate" ReadOnly="True" HeaderText="�޸�ʱ��"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnvcChecker" ReadOnly="True" HeaderText="�����"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cndCheckDate" ReadOnly="True" HeaderText="���ʱ��"></asp:BoundColumn>
									<asp:EditCommandColumn ButtonType="PushButton" UpdateText="����" HeaderText="�޸�" CancelText="ȡ��" EditText="�޸�"></asp:EditCommandColumn>
									<asp:ButtonColumn Text="ɾ��" ButtonType="PushButton" HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
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
					<DIV id="xMsg21" onmouseup="stopDrag(this)" onmousemove="drag(this)" onmousedown="startDrag(this)"><SPAN id="xMsgs11">��ѯ��Ӧ�̴��</SPAN>
						<SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="min(this)">0</SPAN> <SPAN style="BORDER-RIGHT-WIDTH: 0px; WIDTH: 12px; FONT-FAMILY: webdings; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: white; BORDER-LEFT-WIDTH: 0px"
							onclick="ShowHide(1,null)">r</SPAN>
					</DIV>
					<DIV id="xMsg31">
						<TABLE align="center">
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label10" runat="server">��Ʒ��ţ�</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryGoodsCode" runat="server"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="FONT-SIZE: 8pt"><asp:label id="Label11" runat="server">��Ʒ���ƣ�</asp:label></TD>
								<TD style="FONT-SIZE: 8pt"><asp:textbox id="txtQueryGoodsName" runat="server"></asp:textbox></TD>
								<TD><asp:button id="Button1" runat="server" Text="��ѯ"></asp:button></TD>
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
											<asp:BoundColumn DataField="cnvcGoodsCode" HeaderText="��Ʒ���"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcGoodsName" HeaderText="��Ʒ����"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcGroupCode" HeaderText="������λ��"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnvcStockUnit" HeaderText="�ɹ���λ"></asp:BoundColumn>
											<asp:BoundColumn DataField="cnnStockPrice" HeaderText="��������"></asp:BoundColumn>
											<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
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
