<%@ Page language="c#" Codebehind="wfmSaleLoseQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmSaleLoseQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleLoseQuery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">���۱���</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<td style="WIDTH: 50px"></td>
								<TD style="WIDTH: 147px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">�ŵ꣺</asp:label></TD>
								<TD style="WIDTH: 121px"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="176px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 120px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt" Width="80px">��ʼ���ڣ�</asp:label></TD>
								<TD style="WIDTH: 126px"><INPUT id=txtBegin 
            style="WIDTH: 160px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=21 value="<%=strBeginDate%>" name=txtBegin 
            ></TD>
								<TD style="WIDTH: 103px"><FONT face="����"><asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="��ѯ"></asp:button></FONT></TD>
								<TD style="WIDTH: 114px"><asp:button id="btnAdd" runat="server" Font-Size="10pt" Width="56px" Text="���"></asp:button></TD>
								<TD style="WIDTH: 150px"></TD>
							</TR>
							<TR>
								<td style="WIDTH: 50px"></td>
								<TD style="WIDTH: 147px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="68px">������ͣ�</asp:label></TD>
								<TD style="WIDTH: 121px"><asp:dropdownlist id="ddlLoseType" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 120px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt" Width="81px">�������ڣ�</asp:label></TD>
								<TD style="WIDTH: 126px"><INPUT id=txtEnd 
            style="WIDTH: 160px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=21 value="<%=strEndDate%>" name=txtEnd 
            ></TD>
								<td style="WIDTH: 103px"></td>
								<TD style="WIDTH: 114px"></TD>
								<TD style="WIDTH: 150px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" AllowPaging="True"
							PageSize="20">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="cnnLoseSerialNo" ReadOnly="True" HeaderText="�����ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseDate" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWeather" ReadOnly="True" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcLoseComments" ReadOnly="True" HeaderText="���ԭ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperID" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseOperDate" ReadOnly="True" HeaderText="�������ʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDestroyFlag" ReadOnly="True" HeaderText="ȷ�ϱ�־"></asp:BoundColumn>
								<asp:ButtonColumn Text="ɾ��" HeaderText="����" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
