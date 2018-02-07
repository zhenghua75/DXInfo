<%@ Page Language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" CodeBehind="wfmDeptOperManage.aspx.cs"
    AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmDeptOperManage" %>

<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    客户端操作员管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="Table3" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="font-weight: bold; font-size: 15pt; color: #330033" align="center">
                客户端操作员管理
            </td>
        </tr>
    </table>
    <table id="Table5" cellspacing="0" cellpadding="0" width="95%" border="1">
        <tr>
            <td>
                <table id="Table2" cellspacing="0" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td style="width: 42px" align="right">
                            <asp:Label ID="Label1" runat="server" Width="88px" Font-Size="10pt">操作员名称</asp:Label>
                        </td>
                        <td style="width: 127px">
                            <asp:TextBox ID="txtOperName" runat="server" Width="112px" Font-Size="10pt" Height="24px"></asp:TextBox>
                        </td>
                        <td style="width: 53px" align="right">
                            <asp:Label ID="Label2" runat="server" Width="56px" Font-Size="10pt">部门</asp:Label>
                        </td>
                        <td style="width: 118px">
                            <font face="宋体">
										<asp:dropdownlist id="ddlDept" runat="server" Width="168px" Font-Size="10pt" AutoPostBack="True"></asp:dropdownlist></font>
                        </td>
                        <td style="width: 142px">
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Width="64px" Text="查询"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Width="64px" Text="添加"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnExcel" runat="server" Width="64px" Text="导出"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td align="center">
                <uc1:ucPageView ID="UcPageView1" runat="server" Visible="true"></uc1:ucPageView>
            </td>
        </tr>
    </table>
</asp:Content>
