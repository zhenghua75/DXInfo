<%@ Page language="c#" Codebehind="wfmSaleLoseNew.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmSaleLoseNew" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmSaleLoseNew</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">销售报损</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 101px" align="right"><asp:label id="Label6" runat="server" Font-Size="10pt">门店：</asp:label></TD>
								<TD style="WIDTH: 164px"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="160px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 65px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">日期：</asp:label></TD>
								<TD style="WIDTH: 168px"><asp:label id="lblLoseDate" runat="server" Font-Size="10pt" Width="202px"></asp:label></TD>
								<td style="WIDTH: 70px" align="right"><asp:label id="Label3" runat="server" Font-Size="10pt" Width="70px">损耗类型：</asp:label></td>
								<td style="WIDTH: 1px"><FONT face="宋体"><asp:dropdownlist id="ddlLoseType" runat="server" Width="176px"></asp:dropdownlist></FONT></td>
								<TD style="WIDTH: 232px"><asp:button id="btnCancel" runat="server" Font-Size="10pt" Width="56px" Text="返回"></asp:button></TD>
								<TD style="WIDTH: 16px"></TD>
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
								<TD style="WIDTH: 92px" align="right"><asp:label id="Label8" runat="server" Font-Size="10pt">产品类型：</asp:label></TD>
								<TD style="WIDTH: 217px"><asp:dropdownlist id="ddlProductType" runat="server" Font-Size="10pt" Width="184px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 95px" align="right"><asp:label id="Label2" runat="server" Font-Size="10pt">产品类别：</asp:label></TD>
								<TD style="WIDTH: 180px"><asp:dropdownlist id="ddlProductClass" runat="server" Font-Size="10pt" Width="180px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 109px" align="right"><FONT face="宋体"><asp:label id="Label1" runat="server" Font-Size="10pt">产品名称：</asp:label></FONT></TD>
								<TD style="WIDTH: 237px"><asp:dropdownlist id="ddlProductName" runat="server" Font-Size="10pt" Width="189px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 127px" align="left"><asp:button id="btnLoseAdd" runat="server" Font-Size="10pt" Width="56px" Text="添加"></asp:button></TD>
								<td style="WIDTH: 109px"></td>
							</TR>
							<TR>
								<TD style="WIDTH: 92px" align="right"><asp:label id="Label10" runat="server" Font-Size="10pt">天气：</asp:label></TD>
								<TD style="WIDTH: 217px"><asp:textbox id="txtWeather" runat="server" Font-Size="10pt" Width="184px"></asp:textbox></TD>
								<TD style="WIDTH: 95px" align="right"><asp:label id="Label7" runat="server" Font-Size="10pt">数量：</asp:label></TD>
								<td style="WIDTH: 180px"><asp:textbox id="txtCount" runat="server" Font-Size="10pt" Width="108px"></asp:textbox></td>
								<td style="WIDTH: 109px" align="right"><asp:label id="Label4" runat="server" Font-Size="10pt" Width="50px">单位：</asp:label></td>
								<TD style="WIDTH: 237px"><asp:label id="lblUnit" runat="server" Font-Size="10pt" Width="120px"></asp:label></TD>
								<TD style="WIDTH: 127px" align="right"></TD>
								<td style="WIDTH: 109px"></td>
							</TR>
							<TR>
								<TD style="WIDTH: 92px" align="right"><asp:label id="Label11" runat="server" Font-Size="10pt">原因：</asp:label></TD>
								<TD style="WIDTH: 457px" colSpan="3"><asp:textbox id="txtComments" runat="server" Font-Size="10pt" Width="432px" Height="48px" TextMode="MultiLine"></asp:textbox></TD>
								<TD style="WIDTH: 109px"><FONT face="宋体"></FONT></TD>
								<TD style="WIDTH: 237px"></TD>
								<TD style="WIDTH: 127px" align="right"></TD>
								<td style="WIDTH: 109px"></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<tr>
					<td align="right"><asp:button id="btnAllOK" runat="server" Font-Size="10pt" Width="84px" Text="确认提交"></asp:button></td>
				</tr>
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
								<asp:BoundColumn Visible="False" DataField="cnvcDeptID" ReadOnly="True" HeaderText="门店ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDeptName" ReadOnly="True" HeaderText="门店"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="产品编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="产品名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseDate" ReadOnly="True" HeaderText="损耗日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcWeather" ReadOnly="True" HeaderText="天气"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" ReadOnly="True" HeaderText="损耗数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcLoseComments" ReadOnly="True" HeaderText="损耗原因"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcLoseType" ReadOnly="True" HeaderText="损耗类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcOperID" ReadOnly="True" HeaderText="报损人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cndLoseOperDate" ReadOnly="True" HeaderText="报损操作时间"></asp:BoundColumn>
								<asp:ButtonColumn Text="删除" HeaderText="操作" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
