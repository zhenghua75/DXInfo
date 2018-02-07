<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmMain.aspx.cs" Inherits="AMSApp.wfmMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>面包派对网络中心</title>
    <link rel="stylesheet" type="text/css" href="StockControl/scripts/jquery-easyui/themes/Cupertino/easyui.css" />
    <link rel="stylesheet" type="text/css" href="StockControl/scripts/jquery-easyui/themes/icon.css" />
    <script type="text/javascript" src="StockControl/scripts/jquery-easyui/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="StockControl/scripts/jquery-easyui/jquery.easyui.min.js"></script>
    <script src="StockControl/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="StockControl/wfmMain.js" type="text/javascript"></script>

    <%--<script src="StockControl/scripts/jquery-easyui/plugins/jquery.validatebox.js" type="text/javascript"></script>--%>
    <%--<script src="StockControl/scripts/validator.js" type="text/javascript"></script>--%>    
    <%--<script src="StockControl/scripts/jquery-easyui/jquery.json-2.3.min.js" type="text/javascript"></script>--%>
    <%--<script src="StockControl/scripts/formatter.js" type="text/javascript"></script>--%>
<%--    <script src="StockControl/Scripts/DateExtend.js" type="text/javascript"></script>
    <script src="StockControl/Scripts/move.js" type="text/javascript"></script>
    <script src="StockControl/Scripts/JQueryExtend.js" type="text/javascript"></script>
    <script src="StockControl/Scripts/jquery.printElement.js" type="text/javascript"></script>
    <link href="StockControl/css/label.css" rel="stylesheet" type="text/css"/>
    <link href="StockControl/css/div.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="StockControl/tbStock.js" type="text/javascript"></script>--%>
</head>
<body class="easyui-layout">
    <%--<div id="cc"  style="width:2024px;height:768px;"> --%>
    <div region="north" style="height: 80px; overflow: hidden">
        <div style="margin: 0 0 0 0;">
            <img alt="面包派对网络管理中心" src="image/banner.jpg" style="height: 80px; width: 100%; margin: 0 0 0 0;" />
        </div>
        <div style="position: absolute; right: 0px; bottom: 0px; width: 100%; text-align: right;">
            <%--<label id="lblOperName">
            </label>--%>
            <a id="lblOperName" href="#" iconcls="icon-personal" class="easyui-linkbutton" plain="true" onclick="return false;"></a>
            | <a href="#" iconcls="icon-logout" class="easyui-linkbutton" plain="true" onclick="rd();return false;">
                注销</a>|<a href="StockControl/help.htm" iconcls="icon-help" class="easyui-linkbutton" plain="true" target="_blank">帮助</a>
        </div>
    </div>
    <div region="south" style="height: 55px;">
        <p style="text-align: center;">
            Copyright ©2012 - 2014 DXInfo. All Rights Reserved. Design by <a href="http://www.kmdx.cn">
                昆明道讯科技有限公司</a>.</p>
    </div>
    <div region="west" split="true" title="内容" style="width: 210px;">
        <div class="easyui-accordion" fit="true">
            <div title="基本信息">
                <ul>
                    <li runat="server" id="liParaRefresh" visible="false"><a href="paraconf/wfmParaRefresh.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        参数刷新</a></li>
                    <li runat="server" id="liGoods" visible="false"><a href="paraconf/wfmGoods.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        商品管理</a></li>
                    <li runat="server" id="liLoginOper" visible="false"><a href="paraconf/wfmLoginOper.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        网站操作员管理</a></li>
                    <li runat="server" id="liLoginPwd" visible="false"><a href="paraconf/wfmLoginPwd.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        操作员密码修改</a></li>
                    <li runat="server" id="liNotice" visible="false"><a href="paraconf/wfmNotice.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        系统通知</a></li>
                    <li runat="server" id="liSysParaSet" visible="false"><a href="paraconf/wfmSysParaSet.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        系统参数设定</a></li>
                    <li runat="server" id="liDeptManage" visible="false"><a href="paraconf/wfmDeptManage.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        部门参数管理</a></li>
                    <li runat="server" id="liComputationGroup" visible="false"><a href="StockControl/tbComputationGroup.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        计量单位组</a></li>
                    <li runat="server" id="liComputationUnit" visible="false"><a href="StockControl/tbComputationUnit.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        计量单位</a></li>
                    <li runat="server" id="liProductClass" visible="false"><a href="StockControl/tbProductClass.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        存货分类</a></li>
                    <li runat="server" id="liInventory" visible="false"><a href="StockControl/tbInventory.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        存货档案</a></li>
                    <li runat="server" id="liSupplier" visible="false"><a href="StockControl/tbSupplier.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        供应商</a></li>
                    <li runat="server" id="liWarehouse" visible="false"><a href="StockControl/tbWarehouse.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        仓库档案</a></li>
                    <li runat="server" id="liBillOfMaterials" visible="false"><a href="StockControl/tbBillOfMaterials.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        配方维护</a></li>
                    <li runat="server" id="liDeptOperManage" visible="false"><a href="paraconf/wfmDeptOperManage.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        客户端操作员</a></li>
                </ul>
            </div>
            <div title="销售报表">
                <ul>
                    <li runat="server" id="liAssInfo" visible="false"><a href="BusiQuery/wfmAssInfo.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        会员资料查询</a> </li>
                    <li runat="server" id="liConsItem" visible="false"><a href="BusiQuery/wfmConsItem.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        消费统计查询</a> </li>
                    <li runat="server" id="liFillQuery" visible="false"><a href="BusiQuery/wfmFillQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        会员充值查询</a> </li>
                    <li runat="server" id="liConsKindQuery" visible="false"><a href="BusiQuery/wfmConsKindQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        消费分类统计</a> </li>
                    <li runat="server" id="liBusiLogQuery" visible="false"><a href="BusiQuery/wfmBusiLogQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        操作员日志</a> </li>
                    <li runat="server" id="liTopQuery" visible="false"><a href="BusiQuery/wfmTopQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        销售排名榜</a> </li>
                    <li runat="server" id="liBusiIncome" visible="false"><a href="BusiQuery/wfmBusiIncome.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        业务量统计</a> </li>
                    <li runat="server" id="liSalesChart" visible="false"><a href="zhenghua/Lj/Report/wfmSalesChart.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        销售曲线图</a> </li>
                    <li runat="server" id="liDailyCashQuery" visible="false"><a href="BusiQuery/wfmDailyCashQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        当日收银</a> </li>
                    <li runat="server" id="liSpecConsQuery" visible="false"><a href="BusiQuery/wfmSpecConsQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        特殊消费统计</a> </li>
                    <li runat="server" id="liProductClassQuery" visible="false"><a href="zhenghua/produce/wfmProductClassQuery.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        大类类别统计</a> </li>
                    <li runat="server" id="liSellReport" visible="false"><a href="zhenghua/produce/wfmSellReport.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        销售分析报表</a> </li>
                </ul>
            </div>
            <div title="库存管理" selected="true">
                <ul>
                    <li runat="server" id="liStock_0" visible="false"><a href="StockControl/tbStock.aspx?StockType=0"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        期初库存</a> </li>
                    <li runat="server" id="liStock_1" visible="false"><a href="StockControl/tbStock.aspx?StockType=1"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        采购入库</a> </li>
                    <li runat="server" id="liStock_6" visible="false"><a href="StockControl/tbStock.aspx?StockType=6"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        领料单</a> </li>
                    <li runat="server" id="liStock_2" visible="false"><a href="StockControl/tbStock.aspx?StockType=2"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        完工入库</a> </li>
                    <li runat="server" id="liStock_3" visible="false"><a href="StockControl/tbStock.aspx?StockType=3"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        销售出库</a> </li>
                    <li runat="server" id="liStock_4" visible="false"><a href="StockControl/tbStock.aspx?StockType=4"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        盘点</a> </li>
                    <li runat="server" id="liStock_5" visible="false"><a href="StockControl/tbStock.aspx?StockType=5"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        月结</a> </li>
                    <li runat="server" id="liStock_7" visible="false"><a href="StockControl/tbStock.aspx?StockType=7"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        调拨</a> </li>
                    <li runat="server" id="liStockReport" visible="false"><a href="StockControl/StockReport.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        库存统计表</a> </li>
                        <li runat="server" id="li1" visible="true"><a href="http://localhost:1692/StockManage/PurchaseInStock.aspx"
                        iconcls="icon-gnome-main-menu" plain="true" class="easyui-linkbutton" onclick="AddTab(this);return false;">
                        test</a> </li>
                </ul>
            </div>
        </div>
    </div>
    <div region="center">
        <%--<div id="pp">--%>
        <div id="tt" class="easyui-tabs">
            <div title="欢迎" closable="true" href="wfmWelcome.aspx">
                <%--<iframe scrolling="auto" frameborder="0" src="wfmWelcome.aspx" style="width: 100%;
                    height: 100%;"></iframe>--%>
            </div>
        </div>
    </div>
</body>
</html>
