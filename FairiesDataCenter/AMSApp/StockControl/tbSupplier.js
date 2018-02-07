

var url;
$(function () {
    $('#dlg').dialog({
        title: '供应商',
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
        //fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "供应商",
        url: 'Services/tbSupplier.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询供应商',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });

    $('#search-cndCreateDate').datebox({
    });
    $('#search-cndCreateDate1').datebox({
    });
});

function newtbSupplier() {
    $('#dlg').dialog('open').dialog('setTitle', '添加供应商');
    moveDialog('dlg');
    $('#fm').form('clear');
    $("#cnvcCode").removeAttr("readonly");
    url = "Services/tbSupplier.ashx?Method=new";
}
function edittbSupplier() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑供应商');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $("#cnvcCode").attr("readonly", "readonly");
        url = "Services/tbSupplier.ashx?Method=update&cnvcCode=" + row.cnvcCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbSupplier() {
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

function removetbSupplier() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbSupplier.ashx', { 'Method': 'remove', 'cnvcCode': row.cnvcCode }, function (json) {
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


function searchtbSupplier() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询供应商');
    moveDialog('search-dlg');
}
function highSearchtbSupplier() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            'cnvcCode': $('#search-cnvcCode').val(),
            'cnvcName': $('#search-cnvcName').val(),
            'cnvcAddress': $('#search-cnvcAddress').val(),
            'cnvcPostCode': $('#search-cnvcPostCode').val(),
            'cnvcPhone': $('#search-cnvcPhone').val(),
            'cnvcFax': $('#search-cnvcFax').val(),
            'cnvcEmail': $('#search-cnvcEmail').val(),
            'cnvcLinkName': $('#search-cnvcLinkName').val(),
            'cnvcCreateOper': $('#search-cnvcCreateOper').val(),
            'cndCreateDate': $('#search-cndCreateDate').datebox("getValue"),
            "cndCreateDate1": $("#search-cndCreateDate1").datebox("getValue"),
            'cnbInvalid': $('#search-cnbInvalid').attr("checked") ? 'on' : ''
        }
    });
}

function tbSupplierExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbSupplier.ashx?Method=excel',
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
