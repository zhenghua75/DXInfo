

var url;
$(function () {
    $('#dlg').dialog({
        title: '仓库',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#dlg-buttons'
    });
    $('#dg').datagrid({
        width: 800,
        height: 350,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "仓库",
        url: 'Services/tbWarehouse.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询仓库',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });

    $('#cnvcDepID').combogrid({
        panelWidth: 650,
        idField: 'cnvcDeptID',
        textField: 'cnvcDeptName',
        url: 'Services/tbDept.ashx?Method=all',
        required: true,
        columns: [[
        { field: 'cnvcDeptID', title: '部门编码', width: 100 },
        { field: 'cnvcDeptName', title: '部门名称', width: 100 },
        { field: 'cnvcDeptTypeComments', title: '部门类型', width: 120 }
    ]]
    });

    $('#cnvcWhPerson').combogrid({
        panelWidth: 650,
        idField: 'vcLoginID',
        textField: 'vcOperName',
        url: 'Services/tbLogin.ashx?Method=query',
        //required: true,
        pagination: true,
        columns: [[
        { field: 'vcLoginID', title: '操作员ID', width: 100 },
        { field: 'vcOperName', title: '操作员姓名', width: 100 },
        { field: 'vcDeptIDComments', title: '门店', width: 100 }
    ]]
    });
    $('#search-cnvcDepID').combogrid({
        panelWidth: 650,
        idField: 'cnvcDeptID',
        textField: 'cnvcDeptName',
        url: 'Services/tbDept.ashx?Method=all',
        //required: true,
        columns: [[
        { field: 'cnvcDeptID', title: '部门编码', width: 100 },
        { field: 'cnvcDeptName', title: '部门名称', width: 100 },
        { field: 'cnvcDeptTypeComments', title: '部门类型', width: 120 }
    ]]
    });

    $('#search-cnvcWhPerson').combogrid({
        panelWidth: 650,
        idField: 'vcLoginID',
        textField: 'vcOperName',
        url: 'Services/tbLogin.ashx?Method=query',
        //required: true,
        columns: [[
        { field: 'vcLoginID', title: '操作员ID', width: 100 },
        { field: 'vcOperName', title: '操作员姓名', width: 100 },
        { field: 'vcDeptIDComments', title: '门店', width: 100 }
    ]]
    });
});

function newtbWarehouse() {
    $('#dlg').dialog('open').dialog('setTitle', '添加仓库');
    moveDialog('dlg');
    $('#fm').form('clear');
    url = "Services/tbWarehouse.ashx?Method=new";
}
function edittbWarehouse() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑仓库');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $('#cnvcDepID').combogrid('setValue', row.cnvcDepID);
        $('#cnvcDepID').combogrid('setText', row.cnvcDepIDComments);

        $('#cnvcWhPerson').combogrid('setValue', row.cnvcWhPerson);
        $('#cnvcWhPerson').combogrid('setText', row.cnvcWhPersonComments);

        url = "Services/tbWarehouse.ashx?Method=update&cnvcWhCode=" + row.cnvcWhCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbWarehouse() {
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

function removetbWarehouse() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbWarehouse.ashx', { 'Method': 'remove', 'cnvcWhCode': row.cnvcWhCode }, function (json) {
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


function searchtbWarehouse() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询仓库');
    moveDialog('search-dlg');
}
function highSearchtbWarehouse() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            'cnvcWhCode': $('#search-cnvcWhCode').val(),
            'cnvcWhName': $('#search-cnvcWhName').val(),
            'cnvcDepID': $('#search-cnvcDepID').val(),
            'cnvcWhAddress': $('#search-cnvcWhAddress').val(),
            'cnvcWhPhone': $('#search-cnvcWhPhone').val(),
            'cnvcWhPerson': $('#search-cnvcWhPerson').val(),
            'cnvcWhComments': $('#search-cnvcWhComments').val(),
            'cnbInvalid': $('#search-cnbInvalid').attr("checked") ? 'on' : ''
        }
    });
}

function tbWarehouseExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbWarehouse.ashx?Method=excel',
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
