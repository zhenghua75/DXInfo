<%@ Register TagPrefix="uc2" TagName="ucPageView" Src="../../ucPageView.ascx" %>
<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSellReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmSellReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
销售分析报表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="5" width="95%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033" align="center">销售分析报表</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<tr vAlign="top">
					<td align="center">
						<table cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server">部门：</asp:label></TD>
								<td><asp:dropdownlist id="ddlDept" runat="server"></asp:dropdownlist></td>
								<td><asp:Label id="Label4" runat="server">指定大类</asp:Label></td>
								<td><asp:DropDownList id="ddlProductClass" runat="server"></asp:DropDownList></td>
								<TD align="center"><asp:label id="Label2" runat="server">开始日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtBeginDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server"></asp:textbox></td>
								<TD align="center"><asp:label id="Label1" runat="server">结束日期：</asp:label></TD>
								<td><asp:textbox class="Wdate" id="txtEndDate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})"
										runat="server"></asp:textbox></td>
								<td>
									<asp:Button id="Button2" runat="server" Text="查询"></asp:Button></td>
								<td>
									<asp:Button id="Button1" runat="server" Text="导出"></asp:Button></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center">
					</td>
				</tr>
			</TABLE>
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="95%" border="0">
				<TR>
					<TD align="center">
						<uc2:ucPageView id="UcPageView1" runat="server"></uc2:ucPageView></TD>
				</TR>
			</TABLE>
</asp:Content>