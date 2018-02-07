<%@ Page language="c#" Codebehind="wfmAddInventory.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.zhenghua.Inventory.wfmAddInventory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>存货档案</title>
		<base target="_self">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Lj/My97DatePicker/WdatePicker.js"></SCRIPT>
		<LINK href="../DataGrid.css" type="text/css" rel="stylesheet">
		<script>window.name="wfmAddInventory";</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" target="wfmAddInventory">
			<table width="100%" height="100%">
				<tr>
					<td align="center" valign="middle">
						<table align="center">
							<tr>
								<td><asp:label id="lblTitle" runat="server" CssClass="title">存货档案</asp:label></td>
							</tr>
						</table>
						<table align="center">
							<tr>
								<td>
									<asp:Label id="Label1" runat="server" CssClass="lable">存货编码：</asp:Label></td>
								<td>
									<asp:TextBox id="txtInvCode" runat="server" CssClass="textbox"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label2" runat="server" CssClass="lable">存货名称：</asp:Label></td>
								<td>
									<asp:TextBox id="txtInvName" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label3" runat="server" CssClass="lable">产品类别：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlInvCCode" runat="server" AutoPostBack="True"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label4" runat="server" CssClass="lable">允许生产订单：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkProductBill" runat="server"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label5" runat="server" CssClass="lable">是否销售：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkSale" runat="server"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label6" runat="server" CssClass="lable">是否外购：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkPurchase" runat="server"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label7" runat="server" CssClass="lable">是否自制：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkSelf" runat="server"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label8" runat="server" CssClass="lable">是否生产耗用：</asp:Label></td>
								<td>
									<asp:CheckBox id="chkComsume" runat="server"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label9" runat="server" CssClass="lable">参考成本（元）：</asp:Label></td>
								<td>
									<asp:TextBox id="txtInvCCost" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label10" runat="server" CssClass="lable">最新成本（元）：</asp:Label></td>
								<td>
									<asp:TextBox id="txtInvNCost" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label11" runat="server" CssClass="lable">安全库存量：</asp:Label></td>
								<td>
									<asp:TextBox id="txtSafeNum" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label12" runat="server" CssClass="lable">最低库存量：</asp:Label></td>
								<td>
									<asp:TextBox id="txtLowSum" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label style="Z-INDEX: 0" id="Label29" runat="server" CssClass="lable">过期限制（天）：</asp:Label></td>
								<td>
									<asp:TextBox style="Z-INDEX: 0" id="txtExpire" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label style="Z-INDEX: 0" id="Label30" runat="server" CssClass="lable">到期提示（天）：</asp:Label></td>
								<td>
									<asp:TextBox style="Z-INDEX: 0" id="txtDue" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label13" runat="server" CssClass="lable">启用日期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtSDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label14" runat="server" CssClass="lable">停用日期：</asp:Label></td>
								<td>
									<asp:TextBox id="txtEDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:true,readOnly:true,skin:'blue'})"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label15" runat="server" CssClass="lable">计价方式：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlValueType" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label16" runat="server" CssClass="lable">规格型号：</asp:Label></td>
								<td>
									<asp:TextBox id="txtInvStd" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td style="HEIGHT: 21px">
									<asp:Label id="Label17" runat="server" CssClass="lable">计量单位组：</asp:Label></td>
								<td style="HEIGHT: 21px">
									<asp:DropDownList id="ddlGroupCode" runat="server" AutoPostBack="True"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label18" runat="server" CssClass="lable">主计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlComUnitCode" runat="server" Enabled="False"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label19" runat="server" CssClass="lable">销售计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlSAComUnitCode" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label20" runat="server" CssClass="lable">采购计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlPUComUnitCode" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label21" runat="server" CssClass="lable">库存计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlSTComUnitCode" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label22" runat="server" CssClass="lable">生产计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlProduceUnitCode" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label23" runat="server" CssClass="lable">零售价格（元）：</asp:Label></td>
								<td>
									<asp:TextBox id="txtRetailPrice" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label24" runat="server" CssClass="lable">零售计量单位：</asp:Label></td>
								<td>
									<asp:DropDownList id="ddlShopUnitCode" runat="server"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label25" runat="server" CssClass="lable">口感：</asp:Label></td>
								<td>
									<asp:TextBox id="txtFeel" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label26" runat="server" CssClass="lable">组织：</asp:Label></td>
								<td>
									<asp:TextBox id="txtOrganise" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label27" runat="server" CssClass="lable">内馅：</asp:Label></td>
								<td>
									<asp:TextBox id="txtColor" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label28" runat="server" CssClass="lable">表面装饰：</asp:Label></td>
								<td>
									<asp:TextBox id="txtTaste" runat="server" CssClass="textbox"></asp:TextBox></td>
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
