<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmClientOperDetail.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.paraconf.wfmClientOperDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
�ͻ��˲���Ա
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
        <style type="text/css">
            #Table2
            {
                width:65%;
                border:0px;
                margin:0 auto;
                border-collapse:separate;
                border-spacing:1px;
            }          
            #Table2 td
            {
                padding:5px;
                text-align:center;
            }   
            #Table1
            {
                width:65%;
                border:0px;
                margin:0 auto;
                border-collapse:separate;
                border-spacing:10px;
            }           
            #Table1 td
            {
                padding:5px;
            }           
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">
						<asp:Label id="lbltitle" runat="server" Height="24px" Width="380px"></asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1">
				<TR>
					<TD style="WIDTH: 152px; FONT-SIZE: 10pt;text-align:right;">�ͻ��˲���Ա���</TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtOperID" runat="server" Height="24px" Width="135px" Font-Size="10pt"></asp:TextBox></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 150px; FONT-SIZE: 10pt;text-align:right;">�ͻ��˲���Ա����</TD>
					<TD>
						<asp:TextBox id="txtOperName" runat="server" Height="24px" Width="136px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 152px; FONT-SIZE: 10pt;text-align:right;">Ȩ��</TD>
					<TD style="WIDTH: 136px">
						<asp:DropDownList id="ddlLimit" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
					<TD style="WIDTH: 40px"></TD>
					<TD style="WIDTH: 150px; FONT-SIZE: 10pt;text-align:right;">�ŵ�</TD>
					<TD>
						<asp:DropDownList id="ddlDept" runat="server" Width="136px" Font-Size="10pt"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 152px;text-align:center;">
						<asp:Button id="btAdd" runat="server" Width="64px" Font-Size="10pt" Text="���"></asp:Button></TD>
					<TD style="WIDTH: 136px;text-align:center;">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD style="WIDTH: 40px">
						<asp:Button id="btnFreeze" runat="server" Width="64px" Font-Size="10pt" Text="����"></asp:Button></TD>
					<TD style="WIDTH: 150px;text-align:center;">
						<asp:Button id="btPwdBegin" runat="server" Width="82px" Font-Size="10pt" Text="�����ʼ��"></asp:Button></TD>
					<TD style=";text-align:center;">
						<asp:Button id="btcancel" runat="server" Width="63px" Font-Size="10pt" Text="����"></asp:Button></TD>
				</TR>
				<tr>
					<td colspan="5" style="COLOR: #cc0000; FONT-SIZE: 10pt">1�������ͻ��˲���Աʱ������Ĭ��Ϊ��000000</td>
				</tr>
				<tr>
					<td colspan="5" style="COLOR: #cc0000; FONT-SIZE: 10pt">2�������ʼ������ѿͻ��˲���Ա��¼�������ʼ��Ϊ��000000</td>
				</tr>
			</TABLE>
</asp:Content>