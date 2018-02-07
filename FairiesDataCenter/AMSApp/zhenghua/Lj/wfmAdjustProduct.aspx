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
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">生产产品调整</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server">生产部门：</asp:label></TD>
								<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
								<TD align="center"><asp:label id="Label2" runat="server">开始日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtBeginDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server" Width="100px"></asp:textbox></td>
								<td>
									<asp:Label id="Label1" runat="server">结束日期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtEndDate" class="Wdate" runat="server" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										Width="100px"></asp:TextBox></td>
								<td>
									<asp:Button id="btnQuery" runat="server" Text="生产产品查询"></asp:Button></td>
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
								<asp:BoundColumn DataField="cnnSerialNo" ReadOnly="True" HeaderText="生产序号"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="实际量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" ReadOnly="True" HeaderText="生产量"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="调增量">
									<ItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnAddCount") %>' ID="Textbox1" NAME="Textbox1" Width="40px">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="调减量">
									<ItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cnnReduceCount") %>' ID="Textbox2" NAME="Textbox2" Width="40px">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="cndCreateDate" ReadOnly="True" HeaderText="生产日期"></asp:BoundColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="调增" CommandName="AdjustAdd" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="调减" CommandName="AdjustReduce" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton runat="server" Text="删除" CommandName="Delete" CausesValidation="false"></asp:LinkButton>
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
