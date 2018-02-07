

var url;
$(function () {
    $('#dlg').dialog({
        title: '库存子表',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        buttons: '#dlg-buttons'
    });
    $('#dg').datagrid({
        width: 800,
        height: 350,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "库存子表",
        url: 'Services/tbStockDetail.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询库存子表',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        buttons: '#search-dlg-buttons'
    });
});

function newtbStockDetail() {
    $('#dlg').dialog('open').dialog('setTitle', '添加库存子表');
    $('#fm').form('clear');
    url = "Services/tbStockDetail.ashx?Method=new";
}
function edittbStockDetail() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑库存子表');
        $('#fm').form('load', row);
        url = "Services/tbStockDetail.ashx?Method=update&cnnId=" + row.cnnId;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbStockDetail() {
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
                $.messager.show({
                    title: '错误信息',
                    msg: json.msg
                });
            }
        }
    });
}

function removetbStockDetail() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbStockDetail.ashx', { 'Method': 'remove', 'cnnId': row.cnnId }, function (json) {
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


function searchtbStockDetail() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询库存子表');
}
function highSearchtbStockDetail() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            'cnnId': $('#search-cnnId').val(),
            'cnnMainId': $('#search-cnnMainId').val(),
            'cnvcInvCode': $('#search-cnvcInvCode').val(),
            'cnvcComUnitCode': $('#search-cnvcComUnitCode').val(),
            'cnnQuantity': $('#search-cnnQuantity').val(),
            'cnvcMainComUnitCode': $('#search-cnvcMainComUnitCode').val(),
            'cnnMainQuantity': $('#search-cnnMainQuantity').val(),
            'cnnPrice': $('#search-cnnPrice').val(),
            'cnnAmount': $('#search-cnnAmount').val()
        }
    });
}

function tbStockDetailExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbStockDetail.ashx?Method=excel',
        onSubmit: function () {
            return $(this).form('validate');
        }
    });
}
