<%@ Page language="c#" Codebehind="wfmInventotyReStop.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmInventotyReStop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmInventotyReStop</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">���ⶳ</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 67px" align="right"><asp:label style="Z-INDEX: 0" id="Label2" runat="server" Width="41px" Font-Size="10pt" Height="14px">����</asp:label></TD>
								<TD style="WIDTH: 184px" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlDept" runat="server" Width="176px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD style="WIDTH: 79px" align="right"><asp:label style="Z-INDEX: 0" id="Label1" runat="server" Width="40px" Font-Size="10pt" Height="14px">�ֿ�</asp:label></TD>
								<TD style="WIDTH: 202px" align="left"><asp:dropdownlist style="Z-INDEX: 0" id="ddlWhouse" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 165px"><asp:button id="btnQuery" runat="server" Width="64px" Text="��ѯ"></asp:button></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 67px" align="right"><asp:label style="Z-INDEX: 0" id="Label3" runat="server" Width="54px" Font-Size="10pt" Height="14px">�������</asp:label></TD>
								<TD style="WIDTH: 184px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtInvCode" runat="server"></asp:textbox></TD>
								<TD style="WIDTH: 79px" align="right"><asp:label style="Z-INDEX: 0" id="Label4" runat="server" Width="54px" Font-Size="10pt" Height="14px">�������</asp:label></TD>
								<TD style="WIDTH: 202px" align="left"><asp:textbox style="Z-INDEX: 0" id="txtInvName" runat="server" Width="168px"></asp:textbox></TD>
								<TD style="WIDTH: 165px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center"><asp:datagrid style="Z-INDEX: 0" id="Datagrid2" runat="server" Width="100%" Font-Size="X-Small"
							CaptionAlign="Left" ForeColor="Blue" AllowPaging="True" Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033"
							HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana" PagerStyle-HorizontalAlign="Right" CellPadding="3"
							BorderWidth="1px" BorderColor="Black" AutoGenerateColumns="False">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="cnnAutoID" ReadOnly="True" HeaderText="�����ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWhCode" ReadOnly="True" HeaderText="�ֿ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcComunitName" ReadOnly="True" HeaderText="��浥λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndMdate" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndExpDate" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnQuantity" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStopQuantity" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAvaQuantity" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:ButtonColumn Text="�ⶳ" ButtonType="PushButton" HeaderText="����" CommandName="ReStop"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
