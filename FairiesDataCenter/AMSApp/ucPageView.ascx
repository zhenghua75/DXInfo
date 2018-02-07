<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ucPageView.ascx.cs" Inherits="AMSApp.ucPageView" %>

<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR vAlign="top">
		<TD colSpan="2"><ASP:DATAGRID id="MyDataGrid" Font-Names="Verdana" Width="100%" runat="server" AlternatingItemStyle-BackColor="#660033"
				HeaderStyle-BackColor="SteelBlue" Font-Size="X-Small" Font-Name="Verdana" CellPadding="3" BorderWidth="1px"
				BorderColor="Black" OnPageIndexChanged="MyDataGrid_Page" PagerStyle-HorizontalAlign="Right" PagerStyle-Mode="NumericPages"
				AllowPaging="True" ForeColor="Blue" CaptionAlign="Left">
				<FooterStyle Wrap="False"></FooterStyle>
				<SelectedItemStyle Wrap="False"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="#E6E6E6"></AlternatingItemStyle>
				<ItemStyle Wrap="False" ForeColor="Black" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Size="Small" Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#880028"></HeaderStyle>
				<PagerStyle Visible="False" Font-Size="X-Small" HorizontalAlign="Right" Wrap="False" Mode="NumericPages"></PagerStyle>
			</ASP:DATAGRID></TD>
	</TR>
	<TR id="FootBar" runat="server" name="FootBar">
		<TD><asp:label id="lbPageLabel" runat="server" Font-Size="10pt"></asp:label></TD>
		<TD align="right"><asp:linkbutton id="btnFirst" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="0" Text="首页"></asp:linkbutton>|
			<asp:linkbutton id="btnPrev" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="prev" Text="上页"></asp:linkbutton>|
			<asp:linkbutton id="btnNext" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="next" Text="下页"></asp:linkbutton>|
			<asp:linkbutton id="btnLast" onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt"
				ForeColor="navy" CommandArgument="last" Text="尾页"></asp:linkbutton>| <font size="2">
				跳到第</font><INPUT id=page_number size=1 value="<%=MyDataGrid.CurrentPageIndex+1%>" name=page_number><font size="2">页</font>
			<asp:linkbutton id="btnGo" onmouseover="javascript:if((!isInt(page_number.value))||page_number.value<=0){alert('跳转页码必须为正整数！');page_number.focus();return false;};"
				onclick="PagerButtonClick" runat="server" Font-Name="verdana" Font-size="8pt" ForeColor="navy"
				CommandArgument="jump" Text="GO">GO!</asp:linkbutton></FONT></TD>
	</TR>
</TABLE>
