<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DXInfo.WcfRestService.test.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery-1.8.1.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        function submit_word() {
            //var myurl = "/service1/LogOn";
            //var jsondata = '{ "userName": "admin", "passwd": "$186196!" }';
            //var httptype = "POST";

            //var myurl = "/service1/ReadImg/CB-海鲜披萨.jpg";
            //var jsondata = '{"imageFileName":"CB-海鲜披萨.jpg"}';            
            var httptype = "GET";
            var myurl = "/service1/GetOrderDeskInfo";
            var jsondata = '{ "userName": "admin", "passwd": "$186196!","deskNo":"05","quantity":2 }';
            //{"DeskId":"b2d27fd0-a60f-e111-8331-002264899614","DeskNo":"05","OrderDeskId":"bfbefd86-65d8-e211-9fbc-005056c00008","OrderDishId":"bebefd86-65d8-e211-9fbc-005056c00008","UserId":"27aeb029-dd72-4169-be64-44a2aeba8630"
            $.ajax({
                "contentType": "application/json", 
                "type": httptype,
                "url": myurl,
                "dataType": "json",
                //"data":jsondata,
                "success": function (data) {
                    alert(data);
                }
            });
        }    
    </script>
</head>
<body>
    <%--<form id="form1" runat="server">--%>
    <div>
    <input type="button" value="提交" onclick="submit_word()"/>
    <%--<img id="imgCode" src="/service1/ReadImg/CB-海鲜披萨.jpg" alt="test" />--%>
    </div>
    <%--</form>--%>
</body>
</html>
