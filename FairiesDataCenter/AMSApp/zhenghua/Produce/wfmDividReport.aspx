<%@ Page language="c#" Codebehind="wfmDividReport.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmDividReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>wfmDividReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<style media="print">.Noprint { DISPLAY: none }
	.PageNext { PAGE-BREAK-AFTER: always }
	</style>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" align="center" class="Noprint">
				<tr>
					<td align="center">
      <OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 
      width=0 height=0 VIEWASTEXT>
	</OBJECT>
						<input class="button" value="��ӡ" type="button" runat="server" id="btnPrint"> <input class="button" value="ֱ�Ӵ�ӡ" type="button" runat="server" id="btnDPrint">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(8,1)" value="ҳ������" type="button">
						<input class="button" onclick="document.all.WebBrowser.ExecWB(7,1)" value="��ӡԤ��" type="button">
						<asp:button id="btnReturn" runat="server" CssClass="button" Text="����"></asp:button>
						<asp:TextBox id="txtProduceSerialNo" runat="server" Visible="False"></asp:TextBox>
						<asp:TextBox id="txtOrderSerialNo" runat="server" Visible="False"></asp:TextBox>
						<asp:TextBox id="txtAssignSerialNo" runat="server" Visible="False"></asp:TextBox></td></TD></tr>
			</table>
			<table align="center" width="100%">
				<tr>
					<td colspan="3" align="center">
						<asp:Label id="Label8" runat="server" CssClass="title">������巢����</asp:Label></td>
				</tr>
				<tr>
					<td width="33%"><asp:label id="Label1" runat="server">���ڣ�</asp:label><asp:label id="lblShipDate" runat="server"></asp:label></td>
					<td><asp:label id="Label9" runat="server">�ֻ���ˮ��</asp:label><asp:label id="lblAssignSerialNo" runat="server"></asp:label></td>
					<td width="33%"><asp:label id="Label3" runat="server">�ŵ꣺</asp:label><asp:label id="lblOrderDept" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="center" colspan="3">
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black"
							BorderWidth="1px">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cnvcInvName" HeaderText="Ʒ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnfRetailPrice" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnAssignCount" HeaderText="ʵ������"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnSum" HeaderText="�ܶ�"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="���"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="��ע"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td width="33%">
						<asp:Label id="Label2" runat="server">�Ƶ���</asp:Label>
						<asp:Label id="lblOper" runat="server"></asp:Label></td>
					<td width="33%">
						<asp:Label id="Label4" runat="server">�����ˣ�</asp:Label></td>
					<td width="33%">
						<asp:Label id="Label5" runat="server">���䣺</asp:Label></td>
				</tr>
				<tr>
					<td width="33%">
						<asp:Label id="Label6" runat="server">�ŵ�ǩ���ˣ�</asp:Label></td>
					<td></td>
					<td width="33%">
						<asp:Label id="Label7" runat="server">���ڣ�</asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
