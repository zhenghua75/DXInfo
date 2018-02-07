var url;
$(function () {
    $('#cc').combobox(
                {
                    url: 'Services/tbProductType.ashx',
                    valueField: 'id',
                    textField: 'text',
                    required: true,
                    multiple: false
                }
                );

    $('#dlg').dialog({
        title: '存货分类',
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
        title: "存货分类",
        url: 'Services/tbProductClass.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询存货分类',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });
    $('#search-cc').combobox(
                {
                    url: 'Services/tbProductType.ashx',
                    valueField: 'id',
                    textField: 'text'
                }
                );
});

function newProductClass() {
    $('#dlg').dialog('open').dialog('setTitle', '添加存货分类');
    moveDialog('dlg');
    $('#fm').form('clear');
    $("#cnvcProductClassCode").removeAttr("readonly");
    url = "Services/tbProductClass.ashx?Method=new";
}
function editProductClass() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑存货分类');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $("#cnvcProductClassCode").attr("readonly", "readonly");
        url = "Services/tbProductClass.ashx?Method=update&cnvcProductClassCode=" + row.cnvcProductClassCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function saveProductClass() {
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

function removeProductClass() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbProductClass.ashx', { 'Method': 'remove', 'cnvcProductClassCode': row.cnvcProductClassCode }, function (json) {
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


function searchProductClass() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询存货分类');
    moveDialog('search-dlg');
}
function highSearchProductClass() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
    queryParams:{
        "cnvcProductType": $("#search-cc").combobox("getValue"),
        "cnvcProductClassCode":$("#search-code").val(),
        "cnvcProductClassName":$("#search-name").val(),
        "cnnDays":$("#search-days").val(),
        "cnvcComments":$("#search-comments").val()
        }});
}

function productClassExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbProductClass.ashx?Method=excel',
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