<%@ Page language="c#" Codebehind="wfmParaMenu.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.wfmParaMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmParaMenu</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
		<TABLE id="tblParaMenu" cellSpacing="1" cellPadding="1" width="136" border="0" align="left"
			runat="server">
			<TR id="trnoprom" runat="server">
				<TD align="center" style="HEIGHT: 31px; COLOR: #330033; FONT-WEIGHT: bold" bgcolor="#ebf0ec">没有权限</TD>
			</TR>
			<TR id="trParaRefresh" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt;TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmParaRefresh.aspx'"
						href="javascript:">参数刷新</A></TD>
			</TR>
			<TR id="trGoods" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt;TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmGoods.aspx'"
						href="javascript:">商品管理</A></TD>
			</TR>
			<TR id="trLoginOper" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmLoginOper.aspx'"
						href="javascript:">网站操作员管理</A></TD>
			</TR>
			<TR id="trLoginPwd" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmLoginPwd.aspx'"
						href="javascript:">操作员密码修改</A></TD>
			</TR>
			<TR id="trNotice" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmNotice.aspx'"
						href="javascript:">系统通知</A></TD>
			</TR>
			<TR id="trSysParaSet" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmSysParaSet.aspx'"
						href="javascript:">系统参数设定</A></TD>
			</TR>
			<TR id="trDeptManage" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmDeptManage.aspx'"
						href="javascript:">部门参数管理</A></TD>
			</TR>
            <TR id="trComputationGroup" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbComputationGroup.aspx'"
						href="javascript:">计量单位组</A></TD>
			</TR>
			<TR id="trComputationUnit" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbComputationUnit.aspx'"
						href="javascript:">计量单位</A></TD>
			</TR>
            <TR id="trProductClass" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbProductClass.aspx'"
						href="javascript:">存货分类</A></TD>
			</TR>
			<TR id="trInventory" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbInventory.aspx'"
						href="javascript:">存货档案</A></TD>
			</TR>
			<TR id="trSupplier" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbSupplier.aspx'"
						href="javascript:">供应商</A></TD>
			</TR>
			<TR id="trProviderGoods" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='Storage/wfmProviderGoods.aspx'"
						href="javascript:">供应商存货对照</A></TD>
			</TR>
			<TR id="trWarehouse" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='StockControl/tbWarehouse.aspx'"
						href="javascript:">仓库档案</A></TD>
			</TR>
			<TR id="trBillOfMaterials" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='zhenghua/Formula/tbBillOfMaterials.aspx'"
						href="javascript:">配方维护</A></TD>
			</TR>
			<TR id="trDeptOperManage" runat="server">
				<TD align="center" style="HEIGHT: 31px" background="image/anniu.png"><A style="COLOR: #ffffff; FONT-SIZE: 11pt; TEXT-DECORATION: none" onclick="parent.right.location='paraconf/wfmDeptOperManage.aspx'"
						href="javascript:">客户端操作员</A></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
