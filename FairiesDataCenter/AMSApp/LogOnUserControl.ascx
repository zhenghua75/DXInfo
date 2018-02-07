<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogOnUserControl.ascx.cs"
    Inherits="AMSApp.LogOnUserControl" %>
<%
    if (Request.IsAuthenticated)
    {
        DXInfo.Profile.CustomProfile profile = ProfileCommon.GetUserProfile();
%>
<input id="hidUN" type="hidden" value="<%:profile.UserName %>" />
欢迎使用 <strong>
    <%: profile.FullName%></strong>(<%:profile.DeptName %>)! [<a href="/Account/LogOff.aspx">注销</a>
]
<%
    }
    else
    {
%>
[ <a href="/Account/LogOn.aspx">登录</a> ]
<%
    }
%>
