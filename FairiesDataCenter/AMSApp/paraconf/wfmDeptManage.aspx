<%@ Page Language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" CodeBehind="wfmDeptManage.aspx.cs"
    AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmDeptManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    部门参数管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/window.css">
    <style type="text/css">
        #Table3
        {
            width: 95%;
            border: 0px;
            border-collapse: separate;
            border-spacing: 1px;
        }
        #Table3 td
        {
            padding: 5px;
            text-align: center;
        }
        #Table5
        {
            width: 95%;
            border: 1px;
            margin: 0 auto;
            border-collapse: separate;
            border-spacing: 0px;
        }
        #Table5 td
        {
            padding: 0px;
        }
        #Table2
        {
            width: 100%;
            border: 1px;
            margin: 0 auto;
            border-collapse: separate;
            border-spacing: 0px;
        }
        #Table2 td
        {
            padding: 1px;
        }
        #Table4
        {
            width: 95%;
            border: 0px;
            border-collapse: separate;
            border-spacing: 1px;
        }
        #Table4 td
        {
            padding: 1px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="Table3">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold">
                部门参数管理
            </td>
        </tr>
    </table>
    <table id="Table5">
        <tr>
            <td>
                <table id="Table2">
                    <tr>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="Label1" runat="server" Font-Size="10pt" Width="120px">客户端部门名称</asp:Label>
                        </td>
                        <td style="width: 127px">
                            <asp:TextBox ID="txtClientName" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>
                        <td style="width: 186px; text-align: right;">
                            <asp:Label ID="Label2" runat="server" Font-Size="10pt" Width="104px">客户端部门编号</asp:Label>
                        </td>
                        <td style="width: 142px">
                            <asp:TextBox ID="txtClientID" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>
                        <td style="width: 105px">
                        <asp:Label ID="Label3" runat="server" Font-Size="10pt" Width="104px">小票打印电话</asp:Label>
                        </td>
                        <td style="width: 145px">
                        <asp:TextBox ID="txtComments" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Width="64px" Text="查询" 
                                onclick="btnQuery_Click"></asp:Button>
                        </td>
                    <%--</tr>
                    <tr>--%>
                        <%--<td style="width: 160px; text-align: right;">
                            <asp:Label ID="Label3" runat="server" Font-Size="10pt" Width="152px">生产库存中的部门名称</asp:Label>
                        </td>
                        <td style="width: 127px">
                            <asp:TextBox ID="txtNewName" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>
                        <td style="width: 186px; text-align: right;">
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt" Width="149px">生产库存中的部门编号</asp:Label>
                        </td>
                        <td style="width: 142px">
                            <asp:TextBox ID="txtNewID" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>
                        <td style="width: 105px; text-align: right;">
                            <asp:Label ID="Label5" runat="server" Font-Size="10pt" Width="74px">排序序号</asp:Label>
                        </td>
                        <td style="width: 145px">
                            <asp:TextBox ID="txtSortNum" runat="server" Font-Size="10pt" Width="152px" Height="24px"></asp:TextBox>
                        </td>--%>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Width="64px" Text="添加" 
                                onclick="btnAdd_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table4">
        <tr>
            <td>
                <asp:DataGrid ID="DataGrid1" runat="server" Font-Size="X-Small" Width="100%" PagerStyle-HorizontalAlign="Right"
                    BorderColor="Black" BorderWidth="1px" CellPadding="3" Font-Name="Verdana" HeaderStyle-BackColor="SteelBlue"
                    AlternatingItemStyle-BackColor="#660033" Font-Names="Verdana" AutoGenerateColumns="False">
                    <FooterStyle Wrap="False"></FooterStyle>
                    <SelectedItemStyle Wrap="False"></SelectedItemStyle>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028">
                    </HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="vcCommName" HeaderText="客户端部门名称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="vcCommCode" ReadOnly="True" HeaderText="客户端部门编号"></asp:BoundColumn>
                        <asp:BoundColumn DataField="vcComments" HeaderText="小票打印电话"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="cnvcDeptName" HeaderText="生产库存中的部门名称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="cnvcDeptID" ReadOnly="True" HeaderText="生产库存中的部门编号">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="cnnPriority" HeaderText="排序序号"></asp:BoundColumn>--%>
                        <asp:EditCommandColumn ButtonType="PushButton" UpdateText="更新" HeaderText="操作" CancelText="取消"
                            EditText="修改"></asp:EditCommandColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
