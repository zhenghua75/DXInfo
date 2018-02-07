<%@ Page language="c#" Codebehind="wfmProducePlan.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Produce.wfmProducePlan" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProducePlan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#feeff8">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server" CssClass="title">生产计划</asp:Label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label3" runat="server" CssClass="lable">生产单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">生产日期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtProduceDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:TextBox></td>
								<td>
									<asp:CheckBox id="chkSelf" runat="server" Text="是否自生产"></asp:CheckBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table align="center">
							<tr>
								<td colspan="4" align="center">
									<asp:Label id="Label6" runat="server" Font-Bold="True">关联订单发货日期</asp:Label></td>
							</tr>
							<tr>
								<td><FONT face="宋体">
										<asp:Label id="Label4" runat="server" CssClass="lable">开始日期：</asp:Label></FONT></td>
								<td><FONT face="宋体">
										<asp:TextBox id="txtShipBeginDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
											CssClass="Wdate"></asp:TextBox></FONT></td>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">结束日期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtShipEndDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="4" align="center">
									<asp:Button id="btnOK" runat="server" Text="确定" CssClass="button"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="取消" CssClass="nodispaly"></asp:Button>
									<asp:Button id="btnReturn" runat="server" Text="返回" CssClass="button"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
