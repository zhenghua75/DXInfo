<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmConsKindQuery.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.BusiQuery.wfmConsKindQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
消费分类统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../js/calendar.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">消费分类统计</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 129px" align="right"><asp:label id="Label5" runat="server" Font-Size="10pt">开始时间</asp:label></TD>
								<TD style="WIDTH: 151px"><INPUT id=txtBegin 
            onfocus=HS_setDate(this) readOnly type=text size=11 
            value="<%=strBeginDate%>" name=txtBegin></TD>
								<TD style="WIDTH: 171px" align="right"><asp:checkbox id="chbAssType" runat="server" Font-Size="10pt" Checked="True" Text="会员类型"></asp:checkbox></TD>
								<TD style="WIDTH: 156px"><asp:dropdownlist id="ddlAssType" runat="server" Font-Size="10pt" Height="24px" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right"></TD>
								<TD style="WIDTH: 197px"></TD>
								<TD><asp:button id="btQuery" runat="server" Font-Size="10pt" Text="查询" Width="56px"></asp:button></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right"><FONT face="宋体"><asp:label id="Label4" runat="server" Font-Size="10pt">结束时间</asp:label></FONT></TD>
								<TD style="WIDTH: 151px"><INPUT id=txtEnd 
            onfocus=HS_setDate(this) readOnly type=text size=11 
            value="<%=strEndDate%>" name=txtEnd></TD>
								<TD style="WIDTH: 171px" align="right"><FONT face="宋体"><asp:checkbox id="chbGoodsType" runat="server" Font-Size="10pt" Checked="True" Text="商品类型"></asp:checkbox></FONT></TD>
								<TD style="WIDTH: 156px"><asp:dropdownlist id="ddlGoodsType" runat="server" Font-Size="10pt" Height="24px" Width="144px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 81px" align="right">
									<asp:Label id="Label1" runat="server" Font-Size="10pt">产品名称</asp:Label></TD>
								<TD style="WIDTH: 197px" align="left">
									<asp:TextBox id="txtGoodsName" runat="server" Font-Size="10pt" Width="176px"></asp:TextBox></TD>
								<TD><FONT face="宋体"><asp:button id="btnExcel" runat="server" Font-Size="10pt" Text="导出" Width="56px"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right"></TD>
								<TD style="WIDTH: 151px"></TD>
								<TD style="WIDTH: 171px" align="right"><asp:checkbox id="chbDept" runat="server" Font-Size="10pt" Checked="True" Text="门店"></asp:checkbox></TD>
								<TD style="WIDTH: 156px"><FONT face="宋体"><asp:dropdownlist id="ddlDept" runat="server" Font-Size="10pt" Width="144px"></asp:dropdownlist></FONT></TD>
								<TD style="WIDTH: 81px" align="right"></TD>
								<TD style="WIDTH: 197px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<tr>
					<TD align="center" width="35%">
						<asp:label id="lblSumCount" runat="server" Font-Size="12pt" Width="258px" ForeColor="Red">总数量：0</asp:label></TD>
					<TD align="center" width="35%">
						<asp:label id="lblSumFee" runat="server" Font-Size="12pt" Width="283px" ForeColor="Red">总金额：0元</asp:label></TD>
					<TD width="30%"></TD>
				</tr>
				<TR>
					<TD align="center" colspan="3"><uc1:ucpageview id="UcPageView1" runat="server" Visible="true"></uc1:ucpageview></TD>
				</TR>
			</TABLE>
</asp:Content>