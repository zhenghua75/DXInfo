<%@ Page language="c#" Codebehind="wfmBillOfReceiveDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmBillOfReceiveDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmBillOfReceiveDetail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#feeff8">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">领料单细节</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 279px" align="right" colspan="2">
									<asp:label id="Label3" runat="server" Font-Size="10pt" Width="225px">输入要过滤的原材料名称：</asp:label></TD>
								<TD style="WIDTH: 276px" align="left" colspan="2">
									<asp:textbox id="txtMaterialFilter" runat="server" Font-Size="10pt" Width="280px"></asp:textbox></TD>
								<TD style="WIDTH: 101px" align="left">
									<asp:button id="btnQueryFilter" runat="server" Font-Size="10pt" Width="54px" Text="查询"></asp:button></TD>
								<TD style="WIDTH: 183px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right">
									<asp:label id="Label17" runat="server" Font-Size="10pt">单据类型：</asp:label></TD>
								<TD style="WIDTH: 189px">
									<asp:dropdownlist id="ddlBillType" runat="server" Font-Size="10pt" Width="181px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 105px" align="right">
									<asp:label id="Label6" runat="server" Font-Size="10pt">领料单位：</asp:label></TD>
								<TD style="WIDTH: 168px">
									<asp:dropdownlist id="ddlReceiveDeptID" runat="server" Font-Size="10pt" Width="181px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 101px" align="right">
									<asp:label id="Label5" runat="server" Font-Size="10pt">领料日期：</asp:label></TD>
								<TD style="WIDTH: 183px"><INPUT id=txtBegin 
            style="WIDTH: 148px; HEIGHT: 22px" onfocus=HS_setDate(this) readOnly type=text size=18 value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD>
									<asp:button id="btnReceiveNew" runat="server" Font-Size="10pt" Width="88px" Text="领料单-预领" BorderColor="LemonChiffon"
										BackColor="Orange"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right">
									<asp:label id="Label4" runat="server" Font-Size="10pt">生产组：</asp:label></TD>
								<TD style="WIDTH: 189px">
									<asp:dropdownlist id="ddlGroup" runat="server" Font-Size="10pt" Width="181px" AutoPostBack="false"></asp:dropdownlist></TD>
								<TD style="WIDTH: 105px" align="right"></TD>
								<TD style="WIDTH: 168px"></TD>
								<TD style="WIDTH: 101px" align="right">
									<asp:label id="Label9" runat="server" Font-Size="10pt">班次：</asp:label></TD>
								<TD style="WIDTH: 183px">
									<asp:textbox id="txtClass" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD>
									<asp:button id="btnPrint" runat="server" Font-Size="10pt" Width="56px" Text="打 印"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 87px" align="right">
									<asp:label id="Label13" runat="server" Font-Size="10pt">仓库主管：</asp:label></TD>
								<TD style="WIDTH: 189px">
									<asp:textbox id="txtStorageInchargeOperID" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 105px" align="right">
									<asp:label id="Label14" runat="server" Font-Size="10pt">发料人：</asp:label></TD>
								<TD style="WIDTH: 168px">
									<asp:textbox id="txtSendOperID" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 101px" align="right">
									<asp:label id="Label12" runat="server" Font-Size="10pt" Width="72px">物料主管：</asp:label></TD>
								<TD style="WIDTH: 183px" align="left">
									<asp:textbox id="txtMaterialInchargeOperID" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD><FONT face="宋体">
										<asp:button id="btnCancel" runat="server" Font-Size="10pt" Width="56px" Text="返 回"></asp:button></FONT></TD>
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
								<TD style="WIDTH: 183px" align="right">
									<asp:label id="Label18" runat="server" Font-Size="10pt" Width="80px">原材料类别：</asp:label></TD>
								<TD style="WIDTH: 339px">
									<asp:dropdownlist id="ddlMaterialType" runat="server" Font-Size="10pt" Width="189px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 154px" align="right">
									<asp:label id="Label1" runat="server" Font-Size="10pt" Width="80px">原材料名称：</asp:label></TD>
								<TD style="WIDTH: 347px">
									<asp:dropdownlist id="ddlProduct" runat="server" Font-Size="10pt" Width="189px" AutoPostBack="true"></asp:dropdownlist></TD>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 250px">
									<asp:textbox id="txtBillState" runat="server" Font-Size="10pt" Width="40px" Visible="False" ReadOnly="True"></asp:textbox>
									<asp:textbox id="txtReceiveID" runat="server" Font-Size="10pt" Width="40px" Visible="False" ReadOnly="True"></asp:textbox></TD>
								<TD style="WIDTH: 62px" align="right"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 183px" align="right">
									<asp:label id="Label2" runat="server" Font-Size="10pt">规格单位：</asp:label></TD>
								<TD style="WIDTH: 339px">
									<asp:textbox id="txtStandardUnit" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 154px" align="right">
									<asp:label id="Label16" runat="server" Font-Size="10pt">规格数量：</asp:label></TD>
								<TD style="WIDTH: 347px">
									<asp:textbox id="txtStandardCount" runat="server" Font-Size="10pt" Width="104px"></asp:textbox>
									<asp:label id="lblUnit" runat="server" Font-Size="10pt" Width="56px"></asp:label></TD>
								<TD style="WIDTH: 160px" align="right"></TD>
								<TD style="WIDTH: 250px"></TD>
								<TD style="WIDTH: 62px" align="right"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 183px" align="right">
									<asp:label id="Label11" runat="server" Font-Size="10pt">应领量：</asp:label></TD>
								<TD style="WIDTH: 339px">
									<asp:textbox id="txtReceiveCount" runat="server" Font-Size="10pt" Width="144px"></asp:textbox></TD>
								<TD style="WIDTH: 154px" align="right">
									<asp:label id="Label7" runat="server" Font-Size="10pt">上班库存：</asp:label></TD>
								<TD style="WIDTH: 347px">
									<asp:textbox id="txtClassStorage" runat="server" Font-Size="10pt" Width="149px"></asp:textbox></TD>
								<TD style="WIDTH: 160px" align="right">
									<asp:label id="Label15" runat="server" Font-Size="10pt">领料人：</asp:label></TD>
								<TD style="WIDTH: 250px">
									<asp:textbox id="txtReceiveOperID" runat="server" Font-Size="10pt" Width="148px"></asp:textbox></TD>
								<TD style="WIDTH: 62px" align="right">
									<asp:button id="btnAdd" runat="server" Font-Size="10pt" Width="72px" Text="添加产品"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" PagerStyle-HorizontalAlign="Right"
							BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
							AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False"
							AllowPaging="True" PageSize="20">
							<FooterStyle Wrap="False"></FooterStyle>
							<SelectedItemStyle Wrap="False"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
							<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcProductCode" ReadOnly="True" HeaderText="物料编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcProductName" ReadOnly="True" HeaderText="物料名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcStandardUnit" ReadOnly="True" HeaderText="规格单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnStandardCount" ReadOnly="True" HeaderText="规格数量"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcUnit" ReadOnly="True" HeaderText="库存单位"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnClassStorage" ReadOnly="True" HeaderText="上班库存"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcReceiveOperID" ReadOnly="True" HeaderText="领料人"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnReceiveCount" ReadOnly="True" HeaderText="应领量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnOutCount" HeaderText="发货量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnLoseCount" HeaderText="损耗量"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnCount" HeaderText="实际领用量"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑数量"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="删除" HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
