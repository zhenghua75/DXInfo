<%@ Page language="c#" Codebehind="wfmPrintReceiveSend.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmPrintReceiveSend" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmPrintReceiveSend</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="100%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">���Ϸ�������ѯ��ӡ</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 182px" align="right">
									<asp:label id="Label1" runat="server" Width="70px" Font-Size="10pt">��������</asp:label></TD>
								<TD style="WIDTH: 105px" align="right"><FONT face="����">
										<asp:TextBox id="txtSendSerial" runat="server" Font-Size="10pt"></asp:TextBox></FONT></TD>
								<TD style="WIDTH: 214px"></TD>
								<TD style="WIDTH: 107px" align="left">
									<asp:button id="btnQuery" runat="server" Text="��ѯ" Width="56px" Font-Size="10pt"></asp:button></TD>
								<TD style="WIDTH: 115px" align="left">
									<asp:button id="btnPrint" runat="server" Text="��ӡ" Width="56px" Font-Size="10pt"></asp:button></TD>
								<TD><asp:button id="btnCancel" runat="server" Font-Size="10pt" Width="56px" Text="�� ��"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 182px"></TD>
								<TD style="WIDTH: 105px" align="right"><FONT face="����"></FONT></TD>
								<TD style="WIDTH: 214px"></TD>
								<TD style="WIDTH: 107px" align="right"></TD>
								<TD style="WIDTH: 115px"></TD>
								<TD><FONT face="����"></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0" width="100%">
				<TR>
					<TD align="left"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
							Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right" PageSize="20"
							AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="���ϱ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="���λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReceiveCount" ReadOnly="True" HeaderText="��Ӧ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcQC002" ReadOnly="True" HeaderText="��ǵ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCF001" ReadOnly="True" HeaderText="�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCF006" ReadOnly="True" HeaderText="�Ƹ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcBS004" ReadOnly="True" HeaderText="���Ƶ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcJG003" ReadOnly="True" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcXS007" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSH005" ReadOnly="True" HeaderText="�Ϻ�ɳ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcFYZX1" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCY009" ReadOnly="True" HeaderText="����Ӣ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcJM010" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcXM011" ReadOnly="True" HeaderText="С���ŵ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOutCount" ReadOnly="True" HeaderText="�ܷ�����"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
