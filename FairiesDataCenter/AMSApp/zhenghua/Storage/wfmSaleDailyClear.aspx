<%@ Page language="c#" Codebehind="wfmSaleDailyClear.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Storage.wfmSaleDailyClear" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleDailyClear</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">已售完成品一次性清零盘点</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" border="1" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD>
						<TABLE style="WIDTH: 1059px; HEIGHT: 48px" id="Table3" border="0" cellSpacing="1" cellPadding="1"
							width="1059">
							<TR>
								<TD style="WIDTH: 83px" align="right"><asp:label id="Label6" runat="server" Height="14px" Width="48px" Font-Size="10pt">门店：</asp:label></TD>
								<TD style="WIDTH: 176px"><asp:dropdownlist id="ddlDept" runat="server" Width="160px" Font-Size="10pt" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 85px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt">仓库：</asp:label></TD>
								<TD style="WIDTH: 178px"><asp:dropdownlist id="ddlWhouse" runat="server" Width="160px" Font-Size="10pt"></asp:dropdownlist></TD>
								<TD style="WIDTH: 98px" align="right"></TD>
								<TD style="WIDTH: 74px"><asp:button id="btQuery" runat="server" Width="56px" Font-Size="10pt" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 178px"></TD>
								<TD style="WIDTH: 90px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 83px" align="right"><asp:label id="Label1" runat="server" Font-Size="10pt">盘点日期</asp:label></TD>
								<TD style="WIDTH: 176px" align="center"><asp:label id="lblCheckDate" runat="server" Height="15px" Width="136px" Font-Size="10pt"></asp:label></TD>
								<TD style="WIDTH: 85px" align="right"></TD>
								<TD style="WIDTH: 178px">
									<asp:TextBox id="txtCheckNo" runat="server" Width="104px"></asp:TextBox></TD>
								<TD style="WIDTH: 98px" align="right"></TD>
								<TD style="WIDTH: 137px" align="left"><asp:button id="btnCheckOk" runat="server" Width="66px" Font-Size="10pt" Text="盘点确认"></asp:button></TD>
								<TD style="WIDTH: 74px"></TD>
								<TD style="WIDTH: 178px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
				<TR>
					<TD align="center"><asp:datagrid id="Datagrid2" runat="server" Width="100%" Font-Size="X-Small" ForeColor="Blue"
							AllowPaging="True" Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue"
							Font-Name="Verdana" PagerStyle-HorizontalAlign="Right" CellPadding="3" BorderWidth="1px" BorderColor="Black"
							AutoGenerateColumns="False">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcDeptID" ReadOnly="True" HeaderText="部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWhCode" ReadOnly="True" HeaderText="仓库"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvCode" ReadOnly="True" HeaderText="存货编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcInvName" ReadOnly="True" HeaderText="存货名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcSTComunitCode" ReadOnly="True" HeaderText="计量单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSysCount" ReadOnly="True" HeaderText="系统存量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCheckCount" ReadOnly="True" HeaderText="盘点存量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndMdate" ReadOnly="True" HeaderText="生产日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndExpDate" ReadOnly="True" HeaderText="过期日期"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
