<%@ Page language="c#" Codebehind="wfmAdjustProduct.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Lj.wfmAdjustProduct" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmAdjustProduct</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="My97DatePicker/WdatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">������Ʒ����</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server">�������ţ�</asp:label></TD>
								<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
								<TD align="center"><asp:label id="Label2" runat="server">��ʼ���ڣ�</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtBeginDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server" Width="100px"></asp:textbox></td>
								<td>
									<asp:Label id="Label1" runat="server">�������ڣ�</asp:Label></td>
								<td>
									<asp:TextBox id="txtEndDate" class="Wdate" runat="server" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										Width="100px"></asp:TextBox></td>
								<td>
									<asp:Button id="btnQuery" runat="server" Text="������Ʒ��ѯ"></asp:Button></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center"><ASP:DATAGRID id="MyDataGrid" runat="server" AllowPaging="True" PagerStyle-Mode="NumericPages"
							PagerStyle-HorizontalAlign="Right" BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana"
							Font-Size="X-Small" HeaderStyle-BackColor="SteelBlue" AlternatingItemStyle-BackColor="#660033" Width="100%"
							Font-Names="Verdana" AutoGenerateColumns="False" PageSize="9">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnnSerialNo" ReadOnly="True" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCode" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="��Ʒ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="ʵ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="������"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="������">
									<ItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnAddCount") %>' ID="Textbox1" NAME="Textbox1" Width="40px">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="������">
									<ItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnReduceCount") %>' ID="Textbox2" NAME="Textbox2" Width="40px">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cndCreateDate" ReadOnly="True" HeaderText="��������"></asp:BoundColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="����" CommandName="AdjustAdd" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="����" CommandName="AdjustReduce" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="ɾ��" CommandName="Delete" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Font-Size="X-Small" HorizontalAlign="Right" Wrap="False" Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
