<%@ Page Language="c#" CodeBehind="default2.aspx.cs" AutoEventWireup="true" Inherits="AMSApp._default2"
    EnableSessionState="True" Debug="False" EnableViewState="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>面包派对网络中心</title>
    <link href="StockControl/css/style.css" rel="stylesheet" type="text/css" />
    <script src="StockControl/Scripts/DD_belatedPNG_0.0.8a-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        DD_belatedPNG.fix('#divloginpanal');
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="divbig">
        <div id="divcontent">
            <div id="divloginpanal">
                <div id="divc2">
                    <div id="divc2_t1">
                        <span style="font-size: 12px;">用户名：</span><asp:TextBox ID="txtLoginID" runat="server"
                            Width="100px"></asp:TextBox><asp:Label ID="lblusename" runat="server" ForeColor="#FF3300"></asp:Label></div>
                    <div id="divc2_t2">
                        <span style="font-size: 12px">&nbsp&nbsp&nbsp密码：</span><asp:TextBox ID="txtPwd" runat="server"
                            TextMode="Password" Width="100px"></asp:TextBox><asp:Label ID="lblpass" runat="server"
                                ForeColor="#FF3300"></asp:Label></div>
                    <%--<div id="divc2_t3">
                        <span style="font-size: 12px">验证码：</span><asp:TextBox ID="txtyan" runat="server"
                            Width="50px"></asp:TextBox>
                        <asp:Label ID="lblyanzheng" runat="server" BackColor="DeepSkyBlue" ForeColor="#004000"></asp:Label><asp:Label
                            ID="lblyan" runat="server" ForeColor="#FF3300"></asp:Label></div>--%>
                    <div id="divc2_t4">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/StockControl/images/login.png"
                            OnClick="ImageButton1_Click" />&nbsp; <a href="#" onclick="javascript:window.close();">
                                <img src="StockControl/images/exit.png" alt="" /></a></div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
