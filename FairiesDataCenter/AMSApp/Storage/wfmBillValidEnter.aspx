<%@ Page language="c#" Codebehind="wfmBillValidEnter.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillValidEnter" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillValidEnter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">�����������</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px" align="right"><asp:label id="Label1" runat="server" Width="57px" Font-Size="10pt">�����ŵ�</asp:label></TD>
								<TD style="WIDTH: 124px"><asp:dropdownlist id="ddlValidDept" runat="server" Width="176px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 97px" align="right"><FONT face="����"><asp:label id="Label2" runat="server" Width="57px" Font-Size="10pt">�������</asp:label></FONT></TD>
								<TD style="WIDTH: 207px"><FONT face="����"><asp:textbox id="txtAssignID" runat="server" Width="128px"></asp:textbox></FONT></TD>
								<TD style="WIDTH: 148px"></TD>
								<TD style="WIDTH: 229px"><asp:button id="btnQuery" runat="server" Width="64px" Text="��ѯ"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<td style="WIDTH: 133px" align="right">
									<asp:label id="Label4" runat="server" Width="96px" Font-Size="10pt">��ǰ������ţ�</asp:label></td>
								<td style="WIDTH: 110px">
									<asp:label id="lblAssignID" runat="server" Width="95px" Font-Size="10pt"></asp:label></td>
								<TD style="WIDTH: 130px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">�ջ��ˣ�</asp:label></TD>
								<TD style="WIDTH: 71px"><asp:textbox id="txtReceiveOper" runat="server" Width="108px" Font-Size="10pt" ReadOnly="True"></asp:textbox></TD>
								<TD style="WIDTH: 113px" align="right"><asp:label id="Label3" runat="server" Width="71px" Font-Size="10pt">�ջ�ʱ�䣺</asp:label></TD>
								<TD style="WIDTH: 43px"><asp:label id="lblReceiveDate" runat="server" Width="191px" Font-Size="10pt"></asp:label></TD>
								<TD style="WIDTH: 179px" align="center"><asp:button id="btnValidEnter" runat="server" Width="80px" Font-Size="10pt" Text="�������"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="X-Small" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" PageSize="20" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnAssignSerialNo" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderSerialNo" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipDeptID" ReadOnly="True" HeaderText="������λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcShipOperID" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndShipDate" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductType" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOrderCount" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="ʵ������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnTravelCount" HeaderText="���������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnValidOkCount" HeaderText="���պϸ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnValidNoCount" HeaderText="���ղ��ϸ�"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="����" CancelText="ȡ��" EditText="����"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
