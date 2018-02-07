

var url;
$(function () {
    $('#dlg').dialog({
        title: '配方',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#dlg-buttons'
    });
    $('#dg').datagrid({
        width: 1000,
        height: 350,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "配方",
        url: 'Services/tbBillOfMaterials.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询配方',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });

    $('#cnvcPartInvCode').combogrid({
        panelWidth: 650,
        idField: 'Code',
        textField: 'Name',
        url: 'Services/Inventory.ashx?Method=query',
        required: true,
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        striped: true,
        mode:'remote',
        columns: [[
            
            { field: 'Code', width: 100, title: '存货编码' },
			{ field: 'Name', width: 100, title: '存货名称' },
            { field: 'CategoryName', width: 100, title: '存货类别' },
			{ field: 'Specs', width: 100, title: '规格型号' },
        { field: 'MeasurementUnitGroupName', width: 100, title: '计量单位组' },
			{ field: 'MainUnitName', width: 100, title: '主计量单位' },
			{ field: 'StockUnitName', width: 100, title: '库存单位' },
			{ field: 'HighStock', width: 100, title: '最高库存量' },
            { field: 'LowStock', width: 100, title: '最低库存量' },
            { field: 'SecurityStock', width: 100, title: '安全库存量' },
			{field: 'CheckCycleName', width: 100, title: '盘点周期单位' },
			{ field: 'SomeDay', width: 100, title: '每天/周/月第（）天' },
			{ field: 'ShelfLife', width: 100, title: '保质期' },
			{ field: 'ShelfLifeTypeName', width: 100, title: '保质期单位' },
			{ field: 'EarlyWarningDay', width: 100, title: '预警天数' },
            { field: 'Comment', width: 100, title: '描述' },
            { field: 'IsInvalid', width: 100, title: '是否失效' }
    ]]
    });
    $('#cnvcComponentInvCode').combogrid({
        panelWidth: 650,
        idField: 'Code',
        textField: 'Name',
        url: 'Services/Inventory.ashx?Method=query',
        required: true,
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        striped: true,
        mode: 'remote',
        columns: [[
            { field: 'Code', width: 100, title: '存货编码' },
			{ field: 'Name', width: 100, title: '存货名称' },
            { field: 'CategoryName', width: 100, title: '存货类别' },
			{ field: 'Specs', width: 100, title: '规格型号' },
        { field: 'MeasurementUnitGroupName', width: 100, title: '计量单位组' },
			{ field: 'MainUnitName', width: 100, title: '主计量单位' },
			{ field: 'StockUnitName', width: 100, title: '库存单位' },
			{ field: 'HighStock', width: 100, title: '最高库存量' },
            { field: 'LowStock', width: 100, title: '最低库存量' },
            { field: 'SecurityStock', width: 100, title: '安全库存量' },
			{ field: 'CheckCycleName', width: 100, title: '盘点周期单位' },
			{ field: 'SomeDay', width: 100, title: '每天/周/月第（）天' },
			{ field: 'ShelfLife', width: 100, title: '保质期' },
			{ field: 'ShelfLifeTypeName', width: 100, title: '保质期单位' },
			{ field: 'EarlyWarningDay', width: 100, title: '预警天数' },
            { field: 'Comment', width: 100, title: '描述' },
            { field: 'IsInvalid', width: 100, title: '是否失效' }
    ]]
    });
    $('#search-cnvcPartInvCode').combogrid({
        panelWidth: 650,
        idField: 'Code',
        textField: 'Name',
        url: 'Services/Inventory.ashx?Method=query',
        //required: true,
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        mode: 'remote',
        columns: [[
                        { field: 'Code', width: 100, title: '存货编码' },
			{ field: 'Name', width: 100, title: '存货名称' },
            { field: 'CategoryName', width: 100, title: '存货类别' },
			{ field: 'Specs', width: 100, title: '规格型号' },
        { field: 'MeasurementUnitGroupName', width: 100, title: '计量单位组' },
			{ field: 'MainUnitName', width: 100, title: '主计量单位' },
			{ field: 'StockUnitName', width: 100, title: '库存单位' },
			{ field: 'HighStock', width: 100, title: '最高库存量' },
            { field: 'LowStock', width: 100, title: '最低库存量' },
            { field: 'SecurityStock', width: 100, title: '安全库存量' },
			{ field: 'CheckCycleName', width: 100, title: '盘点周期单位' },
			{ field: 'SomeDay', width: 100, title: '每天/周/月第（）天' },
			{ field: 'ShelfLife', width: 100, title: '保质期' },
			{ field: 'ShelfLifeTypeName', width: 100, title: '保质期单位' },
			{ field: 'EarlyWarningDay', width: 100, title: '预警天数' },
            { field: 'Comment', width: 100, title: '描述' },
            { field: 'IsInvalid', width: 100, title: '是否失效' }
    ]]
    });

    $('#search-cnvcComponentInvCode').combogrid({
        panelWidth: 650,
        idField: 'Code',
        textField: 'Name',
        url: 'Services/Inventory.ashx?Method=query',
        //required: true,
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        striped: true,
        mode: 'remote',
        columns: [[
                        { field: 'Code', width: 100, title: '存货编码' },
			{ field: 'Name', width: 100, title: '存货名称' },
            { field: 'CategoryName', width: 100, title: '存货类别' },
			{ field: 'Specs', width: 100, title: '规格型号' },
        { field: 'MeasurementUnitGroupName', width: 100, title: '计量单位组' },
			{ field: 'MainUnitName', width: 100, title: '主计量单位' },
			{ field: 'StockUnitName', width: 100, title: '库存单位' },
			{ field: 'HighStock', width: 100, title: '最高库存量' },
            { field: 'LowStock', width: 100, title: '最低库存量' },
            { field: 'SecurityStock', width: 100, title: '安全库存量' },
			{ field: 'CheckCycleName', width: 100, title: '盘点周期单位' },
			{ field: 'SomeDay', width: 100, title: '每天/周/月第（）天' },
			{ field: 'ShelfLife', width: 100, title: '保质期' },
			{ field: 'ShelfLifeTypeName', width: 100, title: '保质期单位' },
			{ field: 'EarlyWarningDay', width: 100, title: '预警天数' },
            { field: 'Comment', width: 100, title: '描述' },
            { field: 'IsInvalid', width: 100, title: '是否失效' }
    ]]
    });
});

function newtbBillOfMaterials() {
    $('#dlg').dialog('open').dialog('setTitle', '添加配方');
    moveDialog('dlg');
    $('#fm').form('clear');
    url = "Services/tbBillOfMaterials.ashx?Method=new";
}
function edittbBillOfMaterials() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑配方');
        moveDialog('dlg');
        $('#cnvcPartInvCode').combogrid('setValue', row.cnvcPartInvCode);
        $('#cnvcPartInvCode').combogrid('setText', row.cnvcPartInvName);
        $('#cnvcComponentInvCode').combogrid('setValue', row.cnvcComponentInvCode);
        $('#cnvcComponentInvCode').combogrid('setText', row.cnvcComponentInvName);
        $('#fm').form('load', row);
        url = "Services/tbBillOfMaterials.ashx?Method=update&cnvcPartInvCode="+row.cnvcPartInvCode+"&cnvcComponentInvCode="+row.cnvcComponentInvCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbBillOfMaterials() {
    $('#fm').form('submit', {
        url: url,
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (result) {
            var json = eval('(' + result + ')');
            if (json.success) {
                $('#dlg').dialog('close');      // close the dialog  
                $('#dg').datagrid('reload');    // reload the user data  
            } else {
                if (json.msg == "loginOvertime") {
                    window.top.location.href = '../default.aspx';
                }
                else {
                    $.messager.show({
                        title: '错误信息',
                        msg: json.msg
                    });
                }
            }
        }
    });
}

function removetbBillOfMaterials() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbBillOfMaterials.ashx', { 'Method': 'remove', 'cnvcPartInvCode': row.cnvcPartInvCode, 'cnvcComponentInvCode': row.cnvcComponentInvCode}, function (json) {
                    if (json.success == true) {
                        $('#dg').datagrid('reload');
                    } else {
                        $.messager.show({
                            title: '错误信息',
                            msg: json.msg
                        });
                    }
                }, 'json');
            }
        });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行删除'
        });
    }
}


function searchtbBillOfMaterials() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询配方');
    moveDialog('search-dlg');
}
function highSearchtbBillOfMaterials() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            'cnvcPartInvCode': $('#search-cnvcPartInvCode').combogrid("getValue"),
            'cnvcComponentInvCode': $('#search-cnvcComponentInvCode').combogrid("getValue"),
'cnnBaseQtyN':$('#search-cnnBaseQtyN').val(),
'cnnBaseQtyD':$('#search-cnnBaseQtyD').val()}
    });
}

function tbBillOfMaterialsExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbBillOfMaterials.ashx?Method=excel',
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
