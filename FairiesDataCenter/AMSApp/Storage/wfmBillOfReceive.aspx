<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" Codebehind="wfmBillOfReceive.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfReceive" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfReceive</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">ԭ��������</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 8px" align="right">
									<asp:label id="Label1" runat="server" Width="57px" Font-Size="10pt">���ϵ�λ</asp:label></TD>
								<TD style="WIDTH: 124px; HEIGHT: 8px">
									<asp:DropDownList id="ddlReceiveDept" runat="server" Width="176px"></asp:DropDownList></TD>
								<TD style="WIDTH: 97px; HEIGHT: 8px" align="right">
									<asp:label id="Label5" runat="server" Width="55px" Font-Size="10pt">��ʼʱ��</asp:label></TD>
								<TD style="WIDTH: 175px; HEIGHT: 8px"><FONT face="����"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin></FONT></TD>
								<TD style="WIDTH: 99px; HEIGHT: 8px">
									<asp:button id="btnQuery" runat="server" Width="64px" Text="��ѯ"></asp:button></TD>
								<TD style="WIDTH: 157px; HEIGHT: 8px">
									<asp:button id="btnRelativeMakeBill" runat="server" Width="112px" Text="�����������ϵ�"></asp:button></TD>
								<TD style="WIDTH: 101px; HEIGHT: 8px"></TD>
								<TD style="HEIGHT: 8px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt" Width="57px">����״̬</asp:label></TD>
								<TD style="WIDTH: 124px"><FONT face="����">
										<asp:DropDownList id="ddlOrderState" runat="server" Width="176px"></asp:DropDownList></FONT></TD>
								<TD style="WIDTH: 97px" align="right">
									<asp:label id="Label3" runat="server" Width="55px" Font-Size="10pt">����ʱ��</asp:label></TD>
								<TD style="WIDTH: 175px"><FONT face="����"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd></FONT></TD>
								<TD style="WIDTH: 99px"></TD>
								<TD style="WIDTH: 157px">
									<asp:button id="btnAddNonMake" runat="server" Width="112px" Text="�ŵ����ϵ�"></asp:button></TD>
								<TD style="WIDTH: 101px"><FONT face="����">
										<asp:button id="btnSend" runat="server" Width="56px" Text="����"></asp:button></FONT></TD>
								<TD style="HEIGHT: 8px"><FONT face="����">
										<asp:button id="btnPrint" runat="server" Width="80px" Text="��ӡ������"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Verdana" Width="100%"
							AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana"
							CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right" PageSize="20" AllowPaging="True"
							PagerStyle-Mode="NumericPages">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnReceiveSerialNo" ReadOnly="True" HeaderText="������ˮ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveDeptID" ReadOnly="True" HeaderText="���ϵ�λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcGroup" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndReceiveDate" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcClass" ReadOnly="True" HeaderText="���"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcMaterialInchargeOperID" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStorageInchargeOperID" ReadOnly="True" HeaderText="�ֿ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSendOperID" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveType" ReadOnly="True" HeaderText="���ϵ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSendSerialNo" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcBillState" ReadOnly="True" HeaderText="����״̬"></asp:BoundColumn>
								<asp:HyperLinkColumn Text="���ϵ�ά��" DataNavigateUrlField="cnnReceiveSerialNo" DataNavigateUrlFormatString="wfmBillOfReceiveDetail.aspx?id={0}"
									HeaderText="�鿴"></asp:HyperLinkColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
