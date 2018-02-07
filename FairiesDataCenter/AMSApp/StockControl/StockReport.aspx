<%@ Page Title="" Language="C#" MasterPageFile="~/AMSApp/StockControl/StockControl.Master"
    AutoEventWireup="true" CodeBehind="StockReport.aspx.cs" Inherits="AMSApp.StockControl.StockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="StockReport.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnvcInvCode" width="100">
                    存货编码
                </th>
                <th field="cnvcInvName" width="100">
                    存货名称
                </th>
                <th field="cnvcSTComUnitName" width="100">
                    单位
                </th>
                <th field="cnnInitQuantity" width="100" formatter="toFixed2">
                    期初数量
                </th>
                <th field="cnnInQuantity" width="100" formatter="toFixed2">
                    本期入库数量
                </th>
                <th field="cnnOutQuantity" width="100" formatter="toFixed2">
                    本期出库数量
                </th>
                <th field="cnnEndQuantity" width="100" formatter="toFixed2">
                    结余数量
                </th>
                <th field="cnnSTPrice" width="100" formatter="toFixed2">
                    单位成本
                </th>
                <th field="cnnEndAmount" width="100" formatter="toFixed2">
                    库存成本
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchStockReport();return false;">
            搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="StockReportExportToExcel();return false;">
                导出</a>
    </div>
    <div id="search-dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            查询</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>
                仓库
            </label>
            <input id="search-cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
                部门
            </label>
            <input id="search-cnvcDeptId" name="cnvcDeptId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
                存货
            </label>
            <input id="search-cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
                年
            </label>
            <input id="search-cnnYear" name="cnnYear" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
                月
            </label>
            <input id="search-cnnMonth" name="cnnMonth" class="easyui-validatebox" />
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchStockReport();return false;">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
