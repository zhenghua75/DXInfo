<%@ Page language="c#" MasterPageFile="~/AMSApp/AMSApp.Master" Codebehind="wfmGoodsDetail.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.paraconf.wfmGoodsDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" href="../css/window.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="5" width="60%" align="center">
				<TR>
					<TD align="center" style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold">
						<asp:Label id="lbltitle" runat="server" Height="32px" Width="382px">Label</asp:Label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" align="center" cellSpacing="10" cellPadding="5" width="647" border="0"
				style="WIDTH: 647px; HEIGHT: 288px">
				<TR>
					<TD style="WIDTH: 90px; FONT-SIZE: 10pt"><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;</FONT>
						<asp:Label id="Label1" runat="server" style="Z-INDEX: 0">Label</asp:Label></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtGoodsID" runat="server" Font-Size="10pt" Width="135px" Height="24px" Visible="False"></asp:TextBox>
						<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList></TD>
					<td style="WIDTH: 40px"><FONT face="宋体"></FONT></td>
					<TD style="WIDTH: 61px;FONT-SIZE: 10pt" align="right">商品名称</TD>
					<TD>
						<asp:TextBox id="txtGoodsName" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px;FONT-SIZE: 10pt" align="right"><FONT face="宋体">拼音简写</FONT></TD>
					<TD style="WIDTH: 136px">
						<asp:TextBox id="txtSpell" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
					<td style="WIDTH: 40px"><FONT face="宋体"></FONT></td>
					<TD style="WIDTH: 61px;FONT-SIZE: 10pt" align="right">单价</TD>
					<TD>
						<asp:TextBox id="txtPrice" runat="server" Font-Size="10pt" Width="136px" Height="24px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px;HEIGHT: 11px;FONT-SIZE: 10pt" align="right">积分兑换分值</TD>
					<TD style="WIDTH: 136px; HEIGHT: 11px">
						<asp:TextBox id="txtigvalue" runat="server" Font-Size="10pt" Width="136px" Height="24px">-1</asp:TextBox></TD>
					<td style="WIDTH: 40px; HEIGHT: 11px"></td>
					<TD style="WIDTH: 61px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px;HEIGHT: 46px;FONT-SIZE: 10pt" align="right">是否打折</TD>
					<TD colspan="4" style="HEIGHT: 46px">
						<P>
							<FONT face="宋体">
								<asp:CheckBox style="Z-INDEX: 0" id="CBDiscount" runat="server"></asp:CheckBox>
							</FONT><FONT face="宋体"></FONT>
						</P>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 90px" align="center">
						<asp:Button id="btAdd" runat="server" Font-Size="10pt" Width="64px" Text="添加" onclick="btAdd_Click"></asp:Button></TD>
					<TD style="WIDTH: 136px" align="center">
						<asp:Button id="btMod" runat="server" Width="64px" Font-Size="10pt" Text="保存" onclick="btMod_Click"></asp:Button></TD>
					<td style="WIDTH: 40px"></td>
					<TD style="WIDTH: 61px" align="center">
						<asp:Button id="btDel" runat="server" Width="64px" Font-Size="10pt" Text="删除" onclick="btDel_Click"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="btcancel" runat="server" Font-Size="10pt" Width="89px" Text="返回商品管理" onclick="btcancel_Click"></asp:Button></TD>
				</TR>
				<tr>
					<td colspan="5" align="center">
						<asp:Label id="lblPromt" runat="server" Width="512px" ForeColor="Red"></asp:Label></td>
				</tr>
			</TABLE>
</asp:Content>
