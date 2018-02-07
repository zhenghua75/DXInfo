<%@ Page language="c#" Codebehind="wfmTeam.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmTeam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>生产组</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../DataGrid.css">
		<script type="text/javascript">
			function OpenTeamWin(flag,teamid,teamname)
			{
				window.showModalDialog("wfmAddTeam.aspx?flag="+flag+"&teamid="+teamid+"&teamname="+teamname,window,"center:yes;dialogWidth:200px;dialogHeight:200px;") ;
				
					window.location.href ="wfmTeam.aspx";
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td><asp:label id="lblTitle" runat="server" CssClass="title">生产组</asp:label></td>
				</tr>
			</table>
			<table align="center">
				<tr>
					<td>
						<asp:datagrid id="DataGrid1" runat="server" CssClass="datagrid" BorderWidth="1px" BorderColor="Black"
							AutoGenerateColumns="False" AllowPaging="True" Width="800">
							<FooterStyle BorderWidth="1px" CssClass="dg_footer"></FooterStyle>
							<SelectedItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_selected"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="dg_alter"></AlternatingItemStyle>
							<ItemStyle BorderWidth="1px" BorderStyle="Solid" CssClass="dg_item"></ItemStyle>
							<HeaderStyle BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="dg_header"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn Text="选择" CommandName="Select"></asp:ButtonColumn>
								<asp:BoundColumn DataField="cnnTeamID" HeaderText="生产组编码"></asp:BoundColumn>
								<asp:BoundColumn DataField="cnvcTeamName" HeaderText="生产组名称"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td><asp:button id="Button1" runat="server" Text="添加生产组"></asp:button></td>
				</tr>
				<tr>
					<td><asp:button id="Button2" runat="server" Text="修改生产组"></asp:button></td>
				</tr>
				<tr>
					<td>
						<asp:Button id="Button3" runat="server" Width="96px" Text="返回"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
