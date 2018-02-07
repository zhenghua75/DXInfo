<%@ Register TagPrefix="uc1" TagName="ucPageView" Src="../ucPageView.ascx" %>

<%@ Page Language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" CodeBehind="wfmSysParaSet.aspx.cs"
    AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmSysParaSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ϵͳ��������
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/window.css">
    <script language="javascript" src="../js/calendar.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="5" width="95%" border="0">
        <tr>
            <td style="color: #330033; font-size: 15pt; font-weight: bold" align="center">
                ϵͳ��������
            </td>
        </tr>
    </table>
    <table id="Table2" height="500" cellspacing="1" cellpadding="1" width="95%" border="0">
        <tr>
            <td width="50%">
                <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0" height="100%">
                    <tr>
                        <td colspan="3" style="color: #3366cc; font-size: 10pt">
                            ����Ʒ�Ƽ�����
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 184px; font-size: 10pt">
                            ������Ʒ
                        </td>
                        <td style="width: 49px">
                        </td>
                        <td style="font-size: 10pt">
                            �Ƽ���Ʒ
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 184px">
                            <font face="����">
										<asp:ListBox id="lbtcurrent" runat="server" Height="444px" Width="176px" Font-Size="10pt"></asp:ListBox></font>
                        </td>
                        <td style="width: 49px">
                            <table id="Table5" cellspacing="1" cellpadding="1" width="83" border="0" style="width: 83px;
                                height: 225px" align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btAdd" runat="server" Text="���>>" OnClick="btAdd_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btDel" runat="server" Text="<<�Ƴ�" OnClick="btDel_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btNewGoods" runat="server" Text="����" Width="61px" OnClick="btNewGoods_Click">
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
                                        ���ѻ������ã�
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 91px; font-size: 10pt" align="right">
                                        ����
                                    </td>
                                    <td style="width: 85px" align="center">
                                        <asp:TextBox ID="txtFee" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="width: 69px; font-size: 10pt" align="center">
                                        Ԫ������
                                    </td>
                                    <td style="width: 67px" align="center">
                                        <asp:TextBox ID="txtIg" runat="server" Width="94px" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                    <td style="font-size: 10pt">
                                        ����
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 344px; color: #cc0000; font-size: 10pt" colspan="4">
                                        ע�⣺�ò�����������ã�����Ϊ�����޻���
                                    </td>
                                    <td>
                                        <asp:Button ID="btIg" runat="server" Width="48px" Text="����" OnClick="btIg_Click">
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
                                        ��ֵ���������ã�
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 194px; font-size: 10pt" align="right">
                                        100-200�������
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
                                        200-300�������
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
                                        300-400�������
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
                                        400-500�������
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
                                        500�����������
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
                                        ע�⣺�ò�����������ã�����Ϊ��ֵ������
                                    </td>
                                    <td>
                                        <asp:Button ID="btProm" runat="server" Width="48px" Text="����" OnClick="btProm_Click">
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
                                        ��Ա���������ã� <a href="wfmAssTypeSet.aspx?id=" target="_self">
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
                                                    <asp:Label ID="Label1" runat="server" Text="Label">������������</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" onfocus="HS_setDate(this)"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="����" OnClick="Button1_Click" />
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
                                                    <asp:Label ID="Label2" runat="server" Text="��˾����"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="Button2" runat="server" Text="����" onclick="Button2_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="ͼƬ"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" Text="����" onclick="Button3_Click" /></td>
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
