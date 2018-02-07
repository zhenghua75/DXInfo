<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" CodeBehind="wfmSysParaSet.aspx.cs"
    AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmSysParaSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统参数管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/window.css">
    <script language="javascript" src="../js/calendar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                系统参数管理
            </td>
        </tr>
    </table>
    <table id="Table2" height="500" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td width="50%">
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0" height="100%">
                    <tr>
                        <td colspan="3" style="color: #3366cc; font-size: 10pt">
                            新商品推荐设置
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 184px; font-size: 10pt">
                            现有商品
                        </td>
                        <td style="width: 49px">
                        </td>
                        <td style="font-size: 10pt">
                            推荐新品
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 184px">
                            <font face="宋体">
										<asp:ListBox id="lbtcurrent" runat="server" Height="444px" Width="176px" Font-Size="10pt"></asp:ListBox></font>
                        </td>
                        <td style="width: 49px">
                            <table id="Table5" cellspacing="1" cellpadding="1" width="83" border="0" style="width: 83px;
                                height: 225px" align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btAdd" runat="server" Text="添加>>" OnClick="btAdd_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btDel" runat="server" Text="<<移除" OnClick="btDel_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btNewGoods" runat="server" Text="保存" Width="61px" OnClick="btNewGoods_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="lbtNew" runat="server" Height="446px" Width="176px" Font-Size="10pt">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="100%">
                <table id="Table4" cellspacing="1" cellpadding="1" width="100%" height="100%" border="0">
                    <tr>
                        <td height="20%">
                            <table id="Table6" cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td colspan="5" style="color: #3366cc; font-size: 10pt">
                                        消费积分设置：
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 91px; font-size: 10pt" align="right">
                                        消费
                                    </td>
                                    <td style="width: 85px" align="center">
                                        <asp:TextBox ID="txtFee" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="width: 69px; font-size: 10pt" align="center">
                                        元，赠送
                                    </td>
                                    <td style="width: 67px" align="center">
                                        <asp:TextBox ID="txtIg" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        积分
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 344px; color: #cc0000; font-size: 10pt" colspan="4">
                                        注意：该参数如果不设置，将视为消费无积分
                                    </td>
                                    <td>
                                        <asp:Button ID="btIg" runat="server" Width="48px" Text="设置" OnClick="btIg_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="30%">
                            <table id="Table7" cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td colspan="3" style="color: #3366cc; font-size: 10pt">
                                        充值赠款金额设置：
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        100-200赠款比例
                                    </td>
                                    <td style="width: 96px">
                                        <asp:TextBox ID="txtPromRate1" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        200-300赠款比例
                                    </td>
                                    <td style="width: 96px">
                                        <asp:TextBox ID="txtPromRate2" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        300-400赠款比例
                                    </td>
                                    <td style="width: 96px">
                                        <asp:TextBox ID="txtPromRate3" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        400-500赠款比例
                                    </td>
                                    <td style="width: 96px">
                                        <asp:TextBox ID="txtPromRate4" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        500以上赠款比例
                                    </td>
                                    <td style="width: 96px">
                                        <asp:TextBox ID="txtPromRate5" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 295px; color: #cc0000; font-size: 10pt">
                                        注意：该参数如果不设置，将视为充值无赠款
                                    </td>
                                    <td>
                                        <asp:Button ID="btProm" runat="server" Width="48px" Text="设置" OnClick="btProm_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="10%">
                            <table id="Table8" cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td colspan="3" style="color: #3366cc; font-size: 10pt">
                                        会员卡类型设置： <a href="wfmAssTypeSet.aspx?id=" target="_self">
                                            <img src="../image/favorite.png" border="0"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="40%">
                            <table id="Table9" cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td colspan="3" style="color: #3366cc; font-size: 10pt">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Label">积分清零日期</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" onfocus="HS_setDate(this)"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="设置" OnClick="Button1_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="Table10" cellspacing="1" cellpadding="1" width="100%" border="0">
                                <tr>
                                    <td colspan="3" style="color: #3366cc; font-size: 10pt">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="公司名称"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="Button2" runat="server" Text="设置" onclick="Button2_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="图片"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" Text="设置" onclick="Button3_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
