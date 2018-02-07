<%@ Page language="c#" Codebehind="wfmComputationUnit.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.ComputationUnit.wfmComputationUnit" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmComputationUnit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../scripts/datetime.js"></script>
		<script language="javascript" src="../scripts/calendar.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<script type="text/javascript">
			function OpenGroupWin(flag,groupcode,groupname)
			{
				//var para = "bbbb";
				var retvalue = window.showModalDialog("wfmAddComputationGroup.aspx?flag="+flag+"&groupcode="+groupcode+"&groupname="+groupname,window,"center:yes;dialogWidth:400px;dialogHeight:400px;") ;
				if(retvalue == true)
				{
					// ˢ�µ�ǰ���� 
					//window.location.href =window.location.href; 
					
					//window.location.href.reload();
					//���°󶨼�����λ��
					__doPostBack('btnRefreshGroup','');
				}
			}
			function OpenUnitWin(flag,groupcode,groupname,comunitcode,comunitname,mainunit,changerate)
			{
				//var para = "bbbb";
				var retvalue = window.showModalDialog("wfmAddComputationUnit.aspx?flag="+flag+"&groupcode="+groupcode+"&groupname="+groupname+"&comunitcode="+comunitcode+"&comunitname="+comunitname+"&mainunit="+mainunit+"&changerate="+changerate,window,"center:yes;dialogWidth:400px;dialogHeight:400px;") ;
				if(retvalue == true)
				{
					// ˢ�µ�ǰ���� 
					//window.location.href =window.location.href; 
					//window.location.href.reload();
					__doPostBack('btnRefreshUnit','');
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">������λ����</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td><asp:Label ID="lblComputationGroup" Runat="server" CssClass="label">������λ�飺</asp:Label>
					<asp:DropDownList ID="ddlComputationGroup" Runat="server"></asp:DropDownList>
					<asp:Label ID="lblComnUnitCode" Runat="server"  CssClass="label">������λ����</asp:Label>
					<asp:TextBox ID="txtComUnitCode" Runat="server" CssClass="textbox"></asp:TextBox>
					<asp:Label ID="lblComUnitName" Runat="server" CssClass="label">������λ����</asp:Label>
					<asp:TextBox ID="txtComUnitName" Runat="server" CssClass="textbox"></asp:TextBox>
					<asp:Button ID="btnQuery" Runat="server" CssClass="button" Text="��ѯ"></asp:Button>
					</td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td vAlign="top">
						<table>
							<tr vAlign="top">
								<td><asp:label id="Label1" runat="server">������λ���б�</asp:label></td>
							</tr>
							<tr vAlign="top">
								<td>
									<asp:datagrid id="DataGrid1" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
											BorderStyle="None" BorderColor="#CC9966" AutoGenerateColumns="False" AllowPaging="True">
											<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
											<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cnvcGroupcode" HeaderText="������λ�����"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcGroupName" HeaderText="������λ������"></asp:BoundColumn>
												<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
							<tr>
								<td><INPUT onclick="OpenGroupWin('add','','');" value="��Ӽ�����λ��" type="button">
								<asp:button id="Button4" runat="server" Text="��Ӽ�����λ"></asp:button>
								</td>
							</tr>
							<tr>
								<td><asp:button id="Button3" runat="server" Text="�޸ļ�����λ��"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button1" runat="server" Text="ɾ��������λ��" CssClass="nodispaly"></asp:button>
								<asp:Button ID="btnRefreshGroup" Runat="server" Text="ˢ�¼�����λ��" CssClass="nodispaly"></asp:Button>
								</td>
							</tr>
						</table>
					</td>
					<td vAlign="top">
						<table>
							<tr vAlign="top">
								<td><asp:label id="Label2" runat="server">������λ�б�</asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:datagrid id="DataGrid2" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
											BorderStyle="None" BorderColor="#CC9966" AutoGenerateColumns="False" AllowPaging="True">
											<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
											<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cnvcComUnitCode" HeaderText="������λ����"></asp:BoundColumn>
												<asp:BoundColumn DataField="cnvcComUnitName" HeaderText="������λ����"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="�Ƿ���������λ">
													<ItemTemplate>
														<asp:CheckBox id="CheckBox1" runat="server" Enabled=False Checked=<%# DataBinder.Eval(Container, "DataItem.cnbMainUnit") %>></asp:CheckBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnbMainUnit") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="cniChangRate" HeaderText="������"></asp:BoundColumn>
												<asp:ButtonColumn Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
										</asp:datagrid>
								</td>
							</tr>
							
							<tr>
								<td><asp:button id="Button5" runat="server" Text="�޸ļ�����λ"></asp:button></td>
							</tr>
							<tr>
								<td><asp:button id="Button2" runat="server" Text="ɾ��������λ" CssClass="nodispaly"></asp:button>
									<asp:button id="btnRefreshUnit" runat="server" Text="ˢ�¼�����λ" CssClass="nodispaly"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
