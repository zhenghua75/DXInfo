<%@ Page language="c#" Codebehind="wfmDeptPriority.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmDeptPriority" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmDeptPriority</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">部门分货优先级调整</asp:Label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CssClass="datagrid" BorderColor="Black"
							BorderWidth="1px" Width="600">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="cnvcDeptID" ReadOnly="True" HeaderText="部门ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcDeptName" ReadOnly="True" HeaderText="部门名称"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcDeptType" ReadOnly="True" HeaderText="部门类型"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcParentDeptID" ReadOnly="True" HeaderText="上级部门"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="cnvcComments" ReadOnly="True" HeaderText="描述"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnnPriority" HeaderText="优先级"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="修改" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
							</Columns>
							<PagerStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_page" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:Button id="btnReturn" runat="server" Text="返回" CssClass="button"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
