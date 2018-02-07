<%@ Page language="c#" Codebehind="wfmMaterialOutMod.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.MaterialS.wfmMaterialOutMod" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmMaterialOutMod</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<DIV align="center" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
					<TR>
						<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">原材料出库修正</TD>
					</TR>
				</TABLE>
				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" border="1">
					<TR>
						<TD>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
								<TR>
									<TD align="right" style="WIDTH: 120px">
										<asp:label id="Label7" runat="server" Width="80px" Font-Size="10pt">部门</asp:label></TD>
									<TD style="WIDTH: 127px; HEIGHT: 11px">
										<asp:DropDownList id="ddlDept" runat="server" Width="136px"></asp:DropDownList></TD>
									<TD style="WIDTH: 102px; HEIGHT: 11px" align="right">
										<asp:label id="Label9" runat="server" Width="79px" Font-Size="10pt">原材料批次</asp:label></TD>
									<TD style="WIDTH: 125px; HEIGHT: 11px">
										<asp:textbox id="txtBatchNo" runat="server" Font-Size="10pt" Width="112px" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 101px; HEIGHT: 11px" align="right"><FONT face="宋体"></FONT></TD>
									<TD style="WIDTH: 118px; HEIGHT: 11px"></TD>
									<TD style="WIDTH: 86px; HEIGHT: 11px"><FONT face="宋体"></FONT></TD>
									<TD style="HEIGHT: 11px">
										<asp:button id="Button1" runat="server" Width="64px" Text="查询"></asp:button></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px" align="right">
										<asp:label id="Label1" runat="server" Width="80px" Font-Size="10pt">原材料编号</asp:label></TD>
									<TD style="WIDTH: 127px; HEIGHT: 11px">
										<asp:textbox id="txtMaterialCode" runat="server" Width="130px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 102px; HEIGHT: 11px" align="right">
										<asp:label id="Label2" runat="server" Width="79px" Font-Size="10pt">原材料名称</asp:label></TD>
									<TD style="WIDTH: 118px; HEIGHT: 11px">
										<asp:textbox id="txtMaterialName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 101px; HEIGHT: 11px" align="right">
										<asp:label id="Label3" runat="server" Width="79px" Font-Size="10pt">原材料类型</asp:label></TD>
									<TD style="WIDTH: 118px; HEIGHT: 11px">
										<asp:DropDownList id="ddlMaterialType" runat="server" Width="136px"></asp:DropDownList></TD>
									<TD style="WIDTH: 86px; HEIGHT: 11px"></TD>
									<TD style="HEIGHT: 11px"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px" align="right">
										<asp:label id="Label4" runat="server" Width="80px" Font-Size="10pt">出库流水</asp:label></TD>
									<TD style="WIDTH: 127px">
										<asp:textbox id="txtOutSerial" runat="server" Width="130px" Font-Size="10pt" Height="24px"></asp:textbox></TD>
									<TD style="WIDTH: 102px" align="right">
										<asp:label id="Label5" runat="server" Width="79px" Font-Size="10pt">出库开始时间</asp:label></TD>
									<TD style="WIDTH: 118px"><INPUT id=txtBegin 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strBeginDate%>" name=txtBegin></TD>
									<TD style="WIDTH: 101px" align="right">
										<asp:label id="Label6" runat="server" Width="79px" Font-Size="10pt">出库结束时间</asp:label></TD>
									<TD style="WIDTH: 118px"><INPUT id=txtEnd 
            style="WIDTH: 112px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=13 value="<%=strEndDate%>" name=txtEnd></TD>
									<TD style="WIDTH: 86px"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<TR>
						<TD align="center">
							<asp:datagrid id="DataGrid1" runat="server" Width="100%" Font-Size="X-Small" BorderColor="Black"
								PagerStyle-HorizontalAlign="Right" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
								AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False"
								PageSize="20" AllowPaging="True">
								<FooterStyle Wrap="False"></FooterStyle>
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
								<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="cnnSerialNo" ReadOnly="True" HeaderText="出库流水"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcBatchNo" ReadOnly="True" HeaderText="原材料批次"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnMaterialCode" ReadOnly="True" HeaderText="原材料编号"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcMaterialName" ReadOnly="True" HeaderText="原材料名称"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="规格"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcUnit" ReadOnly="True" HeaderText="单位"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnPrice" ReadOnly="True" HeaderText="单价"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcProviderName" ReadOnly="True" HeaderText="供应商"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcName" ReadOnly="True" HeaderText="原材料类型"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="cnnLastCount" ReadOnly="True" HeaderText="出库前数量"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnnOutCount" HeaderText="出库数量"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndOutDate" ReadOnly="True" HeaderText="出库时间"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcDeptName" ReadOnly="True" HeaderText="部门"></asp:BoundColumn>
									<asp:BoundColumn DataField="cndOperDate" ReadOnly="True" HeaderText="操作时间"></asp:BoundColumn>
									<asp:BoundColumn DataField="cnvcOperName" ReadOnly="True" HeaderText="操作员"></asp:BoundColumn>
									<asp:EditCommandColumn ButtonType="PushButton" UpdateText="更新" HeaderText="修正" CancelText="取消" EditText="修正"></asp:EditCommandColumn>
									<asp:ButtonColumn Text="删除" ButtonType="PushButton" HeaderText="撤消" CommandName="Delete"></asp:ButtonColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</FORM>
		</DIV>
	</body>
</HTML>
