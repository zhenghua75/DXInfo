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
						<asp:Label id="Label1" runat="server" CssClass="title">�����ƻ�</asp:Label></td>
				</tr>
			</table>
			<table align="center" cellSpacing="1" cellPadding="1" width="800" border="1">
				<tr>
					<td>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label3" runat="server" CssClass="lable">������λ��</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlProduceDept" runat="server"></asp:DropDownList></td>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">�������ڣ�</asp:Label></td>
								<td>
									<asp:TextBox id="txtProduceDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:TextBox></td>
								<td>
									<asp:CheckBox id="chkSelf" runat="server" Text="�Ƿ�������"></asp:CheckBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table align="center">
							<tr>
								<td colspan="4" align="center">
									<asp:Label id="Label6" runat="server" Font-Bold="True">����������������</asp:Label></td>
							</tr>
							<tr>
								<td><FONT face="����">
										<asp:Label id="Label4" runat="server" CssClass="lable">��ʼ���ڣ�</asp:Label></FONT></td>
								<td><FONT face="����">
										<asp:TextBox id="txtShipBeginDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
											CssClass="Wdate"></asp:TextBox></FONT></td>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">�������ڣ�</asp:Label></td>
								<td>
									<asp:TextBox id="txtShipEndDate" runat="server" ReadOnly="True" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"
										CssClass="Wdate"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="4" align="center">
									<asp:Button id="btnOK" runat="server" Text="ȷ��" CssClass="button"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="ȡ��" CssClass="nodispaly"></asp:Button>
									<asp:Button id="btnReturn" runat="server" Text="����" CssClass="button"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
