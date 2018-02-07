

var url;
$(function () {
    $('#dlg').dialog({
        title: '库存主表',
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
        title: "库存主表",
        url: 'Services/tbStockMain.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询库存主表',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        buttons: '#search-dlg-buttons'
    });
});

function newtbStockMain() {
    $('#dlg').dialog('open').dialog('setTitle', '添加库存主表');
    $('#fm').form('clear');
    url = "Services/tbStockMain.ashx?Method=new";
}
function edittbStockMain() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑库存主表');
        $('#fm').form('load', row);
        url = "Services/tbStockMain.ashx?Method=update&cnnId=" + row.cnnId;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbStockMain() {
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

function removetbStockMain() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbStockMain.ashx', { 'Method': 'remove', 'cnnId': row.cnnId }, function (json) {
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


function searchtbStockMain() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询库存主表');
}
function highSearchtbStockMain() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            'cnnId': $('#search-cnnId').val(),
            'cnvcSupplierCode': $('#search-cnvcSupplierCode').val(),
            'cnvcWhCode': $('#search-cnvcWhCode').val(),
            'cnvcDeptId': $('#search-cnvcDeptId').val(),
            'cnvcOperType': $('#search-cnvcOperType').val(),
            'cndCreateDate': $('#search-cndCreateDate').val(),
            'cnvcCreaterId': $('#search-cnvcCreaterId').val(),
            'cnvcCreaterName': $('#search-cnvcCreaterName').val(),
            'cndCheckDate': $('#search-cndCheckDate').val(),
            'cnvcCheckerId': $('#search-cnvcCheckerId').val(),
            'cnvcCheckerName': $('#search-cnvcCheckerName').val(),
            'cndBusinessDate': $('#search-cndBusinessDate').val(),
            'cnnYear': $('#search-cnnYear').val(),
            'cnnMonth': $('#search-cnnMonth').val(),
            'cnnStatus': $('#search-cnnStatus').val(),
            'cnvcComments': $('#search-cnvcComments').val()
        }
    });
}

function tbStockMainExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbStockMain.ashx?Method=excel',
        onSubmit: function () {
            return $(this).form('validate');
        }
    });
}
