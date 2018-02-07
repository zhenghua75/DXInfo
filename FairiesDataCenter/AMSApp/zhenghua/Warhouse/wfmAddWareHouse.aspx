<%@ Page language="c#" Codebehind="wfmAddWareHouse.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Warhouse.wfmAddWareHouse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>仓库档案</title>
		<base target="_self">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmAddWareHouse";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">		
		<form id="Form1" method="post" runat="server" target="wfmAddWareHouse">
			<table width="100%" height="100%">
				<tr>
					<td align="center" valign="middle">
						<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">仓库档案</asp:label></td>
							</tr>
						</table>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable">仓库编码：</asp:Label></td>
								<td>
									<asp:TextBox id="txtWhCode" runat="server" CssClass="textbox"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">仓库名称：</asp:Label></td>
								<td>
									<asp:TextBox id="txtWhName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label3" runat="server" CssClass="lable">所属部门：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlDepCode" runat="server" AutoPostBack="True"></asp:DropDownList></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable">仓库地址：</asp:Label></td>
								<td>
									<asp:TextBox id="txtWhAddress" runat="server" CssClass="textbox" TextMode="MultiLine" Width="135px"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">电话 ：</asp:Label></td>
								<td>
									<asp:TextBox id="txtWhPhone" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label6" runat="server" CssClass="lable">负责人：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWhPerson" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label7" runat="server" CssClass="lable">计价方式：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWhValueStyle" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label8" runat="server" CssClass="lable">是否冻结：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkFreeze" runat="server"></asp:CheckBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label9" runat="server" CssClass="lable">盘点周期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtFrequency" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label10" runat="server" CssClass="lable">盘点周期单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlFrequency" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label12" runat="server" CssClass="lable">仓库属性：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlWHProperty" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label13" runat="server" CssClass="lable">是否门店：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkShop" runat="server"></asp:CheckBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label14" runat="server" CssClass="lable">备注：</asp:Label></td>
								<td>
									<asp:TextBox id="txtWhMemo" runat="server" CssClass="textbox" TextMode="MultiLine" Width="136px"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<asp:Button id="Button1" runat="server" Text="确定" CssClass="button"></asp:Button>
									<asp:Button id="Button2" runat="server" Text="取消" CssClass="button"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
