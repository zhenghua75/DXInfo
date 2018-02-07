<%@ Page language="c#" Codebehind="wfmStorageSet.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmStorageSet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmStorageSet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">��ǰ���</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<td style="WIDTH: 89px"></td>
								<TD style="WIDTH: 101px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">�ŵ꣺</asp:label></TD>
								<TD style="WIDTH: 149px"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 109px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">��ѯ���</asp:label></TD>
								<TD style="WIDTH: 53px"><asp:dropdownlist id="ddlQueryType" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 114px"></TD>
								<TD style="WIDTH: 16px"></TD>
							</TR>
							<TR>
								<td style="WIDTH: 89px"></td>
								<TD style="WIDTH: 101px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">��Ʒ���ͣ�</asp:label></TD>
								<TD style="WIDTH: 149px"><asp:dropdownlist id="ddlProductType" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 109px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">��Ʒ���</asp:label></TD>
								<TD style="WIDTH: 53px"><asp:dropdownlist id="ddlProductClass" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 114px"><asp:button id="btQuery" runat="server" Font-Size="10pt" Width="56px" Text="��ѯ"></asp:button></TD>
								<TD style="WIDTH: 16px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False" PageSize="20" AllowPaging="True">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="cnvcDeptID" ReadOnly="True" HeaderText="�ŵ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStorageDeptID" ReadOnly="True" HeaderText="�ŵ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="��ǰ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSafeCount" HeaderText="��ȫ�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSafeUpCount" HeaderText="��ȫ�������"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="����" CancelText="ȡ��" EditText="�༭"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
