<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmSalesChart.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Lj.Report.wfmSalesChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
���ֵ����۶������ƣ���Ԫ��
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../../../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table1" style="margin: 0 auto;width:95%;border:0;border-collapse:separate; border-spacing: 3px;">
				<TR>
					<TD style="text-align: center;FONT-WEIGHT: bold; FONT-SIZE: 15pt; COLOR: #330033;padding:3px">���ֵ����۶������ƣ���Ԫ��</TD>
				</TR>
			</TABLE>
			<TABLE class="table_content_group" id="Table2" 
            style="margin: 0 auto;FONT-SIZE: 10pt;width:800px;border:1;border-collapse:separate; border-spacing: 0px;">
				<TR>
                    <TD noWrap style="padding:0px;WIDTH: 110px" align="right">����</TD>
					<TD style="HEIGHT: 29px"><asp:dropdownlist id="ddlDept" runat="server" Width="112px"></asp:dropdownlist></TD>
					<TD noWrap style="padding:0px;WIDTH: 110px" align="right">�·�</TD>
					<TD style="HEIGHT: 29px"><asp:dropdownlist id="ddlAcctMonth" runat="server" Width="112px"></asp:dropdownlist></TD>
					<TD style="WIDTH: 143px; HEIGHT: 29px" noWrap align="right">Y����</TD>
					<TD style="HEIGHT: 29px" width="40%"><asp:dropdownlist id="ddlYAXis" runat="server" Width="88px">
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3" Selected="True">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5">5</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD style="HEIGHT: 29px" width="40%">
                    <INPUT id="btnOk" type="button" value="��  ѯ" name="btnOk" runat="server">
					</TD>
				</TR>
                </table>
                <table style="margin: 0 auto;">
                <tr>
                    <td colspan="4"><asp:image id="Image1" runat="server"></asp:image></td>
                    <td style="vertical-align: top;">
                        <TABLE runat="server" id="tbl" style='FONT-SIZE: 10pt;' cellSpacing='0' cellPadding='0' width='400' align='left' border='0'>
                        <tr style="height:34px"><td style='WIDTH: 200px;'>ע��<BR>X������<br>Y�����۶��Ԫ��</TD><TD></TD></tr>
                        </TABLE>
                    </td>
                </tr>
			</TABLE>
			
</asp:Content>