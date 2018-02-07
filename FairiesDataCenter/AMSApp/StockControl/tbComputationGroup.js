var url;
$(function () {
    $('#dlg').dialog({
        title: '计量单位组',
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
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "计量单位组",
        url: 'Services/tbComputationGroup.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询计量单位组',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });
});

function newComputationGroup() {
    $('#dlg').dialog('open').dialog('setTitle', '添加计量单位组');
    moveDialog('dlg');
    $('#fm').form('clear');
    $("#cnvcGroupCode").removeAttr("readonly");
    url = "Services/tbComputationGroup.ashx?Method=new";
}
function editComputationGroup() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑计量单位组');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $("#cnvcGroupCode").attr("readonly", "readonly");
        url = "Services/tbComputationGroup.ashx?Method=update&cnvcGroupCode=" + row.cnvcGroupCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function saveComputationGroup() {
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

function removeComputationGroup() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbComputationGroup.ashx', { 'Method': 'remove', 'cnvcGroupCode': row.cnvcGroupCode }, function (json) {
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


function searchComputationGroup() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询计量单位组');
    moveDialog('search-dlg');
}
function highSearchComputationGroup() {
    $('#search-dlg').dialog('close');
    moveDialog('search-dlg');
    $('#dg').datagrid({
        queryParams: {
            "cnvcGroupCode": $("#search-code").val(),
            "cnvcGroupName": $("#search-name").val()
        }
    });
}

function ComputationGroupExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbComputationGroup.ashx?Method=excel',
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