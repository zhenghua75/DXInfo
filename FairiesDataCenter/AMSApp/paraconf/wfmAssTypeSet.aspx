<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmAssTypeSet.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmAssTypeSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
会员类型管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<LINK rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
				<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="5" width="95%">
					<TR>
						<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">会员类型管理</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" border="1" cellSpacing="0" cellPadding="0" width="95%">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
								<TR>
									<TD style="WIDTH: 109px" align="right"><font style="FONT-FAMILY: 宋体; FONT-SIZE: 10pt">会员类型名称</font></TD>
									<TD style="WIDTH: 222px"><asp:textbox id="txtAssTypeName" runat="server" Height="24px" Font-Size="10pt" Width="176px"></asp:textbox></TD>
									<TD style="WIDTH: 138px" align="right"><font style="FONT-FAMILY: 宋体; FONT-SIZE: 10pt">会员类型编码</font></TD>
									<TD style="WIDTH: 177px"><asp:textbox id="txtAssTypeCode" runat="server" Height="24px" Font-Size="10pt" Width="104px"></asp:textbox></TD>
									<TD><asp:button id="btnQuery" runat="server" Width="64px" Text="查询" onclick="btnQuery_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 109px" align="right"><font style="FONT-FAMILY: 宋体; FONT-SIZE: 10pt">折扣</font></TD>
									<TD style="WIDTH: 222px"><asp:textbox id="txtAssTypeRate" runat="server" Height="24px" Font-Size="10pt" Width="104px"></asp:textbox></TD>
									<TD style="WIDTH: 138px" align="right"><font style="FONT-FAMILY: 宋体; FONT-SIZE: 10pt">是否屏蔽</font></TD>
									<TD style="WIDTH: 177px"><asp:dropdownlist id="ddlDisp" runat="server" Height="22px" Font-Size="10pt" Width="104px" AutoPostBack="false"></asp:dropdownlist></TD>
									<TD><asp:button id="btnAdd" runat="server" Width="64px" Text="添加" onclick="btnAdd_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD colspan="5" align="left"><font style="FONT-FAMILY: 宋体; COLOR: red; FONT-SIZE: 10pt; FONT-WEIGHT: bold">注：折扣为10以内的数字，如折扣为95折，则应填写：9.5；0为不打折。<br>
											&nbsp;&nbsp;&nbsp;是否屏蔽是指在新发卡和修改会员时的会员类型选项是否进行屏蔽。</font></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="1" width="95%">
					<TR>
						<TD align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" AutoGenerateColumns="False"
								Font-Names="Verdana" AlternatingItemStyle-BackColor="#660033" HeaderStyle-BackColor="SteelBlue" Font-Name="Verdana"
								CellPadding="3" BorderWidth="1px" BorderColor="Black" PagerStyle-HorizontalAlign="Right">
								<FooterStyle Wrap="False"></FooterStyle>
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
								<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="vcCommName" HeaderText="会员类型名称"></asp:BoundColumn>
									<asp:BoundColumn DataField="vcCommCode" ReadOnly="True" HeaderText="会员类型编码"></asp:BoundColumn>
									<asp:BoundColumn DataField="rate" HeaderText="折扣"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="是否屏蔽">
										<ItemTemplate>
											<asp:Label style="Z-INDEX: 0" id="lbldis" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.disp")%>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList style="Z-INDEX: 0" id="dgddlDisp" runat="server" Height="21px" Width="81px"></asp:DropDownList>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="PushButton" UpdateText="更新" HeaderText="操作" CancelText="取消" EditText="修改"></asp:EditCommandColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
</asp:Content>