<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AMSApp._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>面包派对网络中心</title>
    <link href="StockControl/css/style.css" rel="stylesheet" type="text/css" />

</head>
<body style="background-image: url(StockControl/images/loginframe.jpg);">
    <form id="form1" runat="server">
    <div id="divbig">
        <div id="divcontent">
            <div id="divloginpanal">
                <div id="divc2">
                    <div id="divc2_t1">
                        <span style="font-size: 12px; width: 100px;">用户名：</span><asp:TextBox ID="txtLoginID"
                            runat="server" Width="100px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ErrorMessage="请输入用户名！" ControlToValidate="txtLoginID">*</asp:RequiredFieldValidator>
                    </div>
                    <div id="divc2_t2">
                        <span style="font-family: '宋体', Simsun; font-size: 12px; width: 100px;">密&nbsp;&nbsp;码：</span><asp:TextBox
                            ID="txtPwd" runat="server" TextMode="Password" Width="100px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入密码！" ControlToValidate="txtPwd">*</asp:RequiredFieldValidator></div>
                    <div id="divc2_t3">
                        <span style="font-size: 12px; width: 100px;">验证码：</span><asp:TextBox ID="txtyan"
                            runat="server" Width="50px"></asp:TextBox>
                        <asp:Label ID="lblyanzheng" runat="server" BackColor="DeepSkyBlue" ForeColor="#004000"></asp:Label><asp:Label
                            ID="lblyan" runat="server" ForeColor="#FF3300"></asp:Label></div>
                    <div id="divc2_t4">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/AMSApp/StockControl/images/login.png"
                            OnClick="ImageButton1_Click" />&nbsp; <a href="#" onclick="javascript:window.close();">
                                <img src="StockControl/images/exit.png" alt="退出" border="0" /></a></div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
