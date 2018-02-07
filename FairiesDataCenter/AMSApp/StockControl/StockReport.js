$(function () {
    $('#dg').datagrid({
        width: 800,
        height: 350,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        //pagination: true,
        title: "库存统计表",
        url: 'Services/StockReport.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });

    $('#search-cnvcWhCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcWhCode',
        textField: 'cnvcWhName',
        url: 'Services/tbWarehouse.ashx?Method=query&IsInvalid=false',
        pagination: true,
        mode: 'remote',
        columns: [[
        { field: 'cnvcWhCode', title: '仓库编码', width: 100 },
        { field: 'cnvcWhName', title: '仓库名称', width: 100 },
        { field: 'cnvcDepIDComments', title: '部门', width: 100 },
        { field: 'cnvcWhAddress', title: '地址', width: 100 },
        { field: 'cnvcWhPhone', title: '电话', width: 100 },
        { field: 'cnvcWhPersonComments', title: '负责人', width: 100 },
        { field: 'cnvcWhComments', title: '描述', width: 100 },
        { field: 'cnbInvalidComments', title: '是否失效', width: 100 }
    ]]
    });
    $('#search-cnvcDeptId').combogrid({
        panelWidth: 650,
        idField: 'cnvcDeptID',
        textField: 'cnvcDeptName',
        url: 'Services/tbDept.ashx?Method=all',
        columns: [[
        { field: 'cnvcDeptID', title: '部门编码', width: 100 },
        { field: 'cnvcDeptName', title: '部门名称', width: 100 },
        { field: 'cnvcDeptTypeComments', title: '部门类型', width: 120 }
    ]]
    });
    $('#search-cnvcInvCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcInvCode',
        textField: 'cnvcInvName',
        url: 'Services/tbInventory.ashx?Method=query',
        //required: true,
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        striped: true,
        mode: 'remote',
        columns: [[
            { field: 'cnvcInvCCodeComments', width: 100, title: '存货类别' },
            { field: 'cnvcInvCode', width: 100, title: '存货编码' },
			{ field: 'cnvcInvName', width: 100, title: '存货名称' },
			{ field: 'cnbProductBillComments', width: 100, title: '允许生产订单' },
			{ field: 'cnbSaleComments', width: 100, title: '是否销售' },
			{ field: 'cnbPurchaseComments', width: 100, title: '是否外购' },
			{ field: 'cnbSelfComments', width: 100, title: '是否自制' },
			{ field: 'cnbComsumeComments', width: 100, title: '是否生产耗用' },
			{ field: 'cniInvCCost', width: 100, title: '参考成本（元）' },
			{ field: 'cniInvNCost', width: 100, title: '最新成本（元）' },
			{ field: 'cniSafeNum', width: 100, title: '安全库存量' },
			{ field: 'cniLowSum', width: 100, title: '最低库存量' },
			{ field: 'cnnExpire', width: 100, title: '过期限制（天）' },
			{ field: 'cnnDue', width: 100, title: '到期提示（天）' },
			{ field: 'cndSDate', width: 100, title: '启用日期' },
			{ field: 'cndEDate', width: 100, title: '停用日期' },
            { field: 'cnvcCreatePersonComments', width: 100, title: '建档人' },
            { field: 'cnvcModifyPersonComments', width: 100, title: '变更人' },
			{ field: 'cndModifyDate', width: 100, title: '变更日期' },
            { field: 'cnvcValueTypeComments', width: 100, title: '计价方式' },
			{ field: 'cnvcInvStd', width: 100, title: '规格型号' },
            { field: 'cnvcGroupCodeComments', width: 100, title: '计量单位组' },
            { field: 'cnvcComUnitCodeComments', width: 100, title: '主计量单位' },
            { field: 'cnvcSAComUnitCodeComments', width: 100, title: '销售计量单位' },
            { field: 'cnvcPUComUnitCodeComments', width: 100, title: '采购计量单位' },
            { field: 'cnvcSTComUnitCodeComments', width: 100, title: '库存计量单位' },
            { field: 'cnvcProduceUnitCodeComments', width: 100, title: '生产计量单位' },
			{ field: 'cnfRetailPrice', width: 100, title: '零售价格' },
            { field: 'cnvcShopUnitCodeComments', width: 100, title: '零售计量单位' },
			{ field: 'cnvcFeel', width: 100, title: '口感' },
			{ field: 'cnvcOrganise', width: 100, title: '组织' },
			{ field: 'cnvcColor', width: 100, title: '内馅' },
			{ field: 'cnvcTaste', width: 100, title: '表面装饰' }
    ]]
    });

    $('#search-cnnYear').combobox(
    {
        url: 'Services/tbMonthlyBalance.ashx?Method=cnnYear',
        valueField: 'id',
        textField: 'text',
        //required: true,
        multiple: false
    }
    );
    $('#search-cnnMonth').combobox(
    {
        url: 'Services/tbMonthlyBalance.ashx?Method=cnnMonth',
        valueField: 'id',
        textField: 'text',
        //required: true,
        multiple: false
    }
    );
});

function searchStockReport() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询');
    moveDialog('search-dlg');
}
function highSearchStockReport() {
    $('#search-dlg').dialog('close');
    moveDialog('search-dlg');
    $('#dg').datagrid({
        queryParams: {
            "cnvcWhCode": $("#search-cnvcWhCode").val(),
            "cnvcDeptId": $("#search-cnvcDeptId").val(),
            "cnvcInvCode": $("#search-cnvcInvCode").val(),
            "cnnYear": $("#search-cnnYear").combobox("getValue"),
            "cnnMonth": $("#search-cnnMonth").combobox("getValue")
        }
    });
}

function StockReportExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/StockReport.ashx?Method=excel',
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (result) {
            var json = eval('(' + result + ')');
            if (json.msg == "loginOvertime") {
                window.top.location.href = '../default.aspx';
            }
        }
    });
}