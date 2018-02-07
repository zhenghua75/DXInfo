<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmGoodsChart.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.Storage.Report.wfmGoodsChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
单产品销售量日走势
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="3"
				cellPadding="3" width="95%" align="center" border="0">
				<TR>
					<TD align="center" style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">单产品销售量日走势</TD>
				</TR>
			</TABLE>
			<TABLE class="table_content_group" id="Table2" 
				cellspacing="0" cellpadding="0" width="95%" border="0">
				<TR>
					<TD noWrap style="WIDTH: 124px; HEIGHT: 30px" align="right">月份</TD>
					<TD style="WIDTH: 214px; HEIGHT: 30px"><asp:dropdownlist id="ddlAcctMonth" runat="server" Width="112px"></asp:dropdownlist></TD>
					<TD noWrap style="WIDTH: 110px; HEIGHT: 30px" align="right">商品类型</TD>
					<TD style="WIDTH: 154px; HEIGHT: 30px"><asp:dropdownlist id="ddlGoodType" runat="server" Width="152px" AutoPostBack="True" onselectedindexchanged="ddlGoodType_SelectedIndexChanged"></asp:dropdownlist></TD>
					<TD style="WIDTH: 139px; HEIGHT: 30px" noWrap align="right"></TD>
					<TD style="WIDTH: 266px; HEIGHT: 30px" width="266"></TD>
					<TD style="WIDTH: 300px; HEIGHT: 30px" width="300"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 124px" noWrap align="right">商品名称</TD>
					<TD style="WIDTH: 214px; HEIGHT: 29px">
						<asp:TextBox style="Z-INDEX: 0" id="txtGoodName" runat="server"></asp:TextBox><INPUT style="Z-INDEX: 0" id="btnserch" name="btnserch" value="筛选" type="button" runat="server" onserverclick="btnserch_ServerClick"></TD>
					<TD noWrap style="WIDTH: 110px" align="right">商品</TD>
					<TD style="WIDTH: 154px; HEIGHT: 29px"><asp:dropdownlist id="ddlGoods" runat="server" Width="152px"></asp:dropdownlist></TD>
					<TD style="WIDTH: 139px; HEIGHT: 29px" noWrap align="right"></TD>
					<TD style="WIDTH: 266px; HEIGHT: 29px" width="266"><INPUT style="Z-INDEX: 0" id="btnOk" name="btnOk" value="查  询" type="button" runat="server" onserverclick="btnOk_ServerClick"></TD>
					<TD style="WIDTH: 300px; HEIGHT: 30px" width="300"></TD>
				</TR>
			</TABLE>
            <table>
                <tr>
                    <td><asp:image id="Image1" runat="server"></asp:image></td>
                    <td runat="server" id="info"></td>
                </tr>
            </table>
			</form>
</asp:Content>
