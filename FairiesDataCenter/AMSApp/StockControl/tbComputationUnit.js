

var url;
$(function () {
    $('#dlg').dialog({
        title: '计量单位',
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
        title: "计量单位",
        url: 'Services/tbComputationUnit.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询计量单位',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });

    $('#cc').combogrid({
        panelWidth: 650,
        idField: 'cnvcGroupCode',
        textField: 'cnvcGroupName',
        url: 'Services/tbComputationGroup.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcGroupCode', title: '计量单位组编码', width: 100 },
        { field: 'cnvcGroupName', title: '计量单位组名称', width: 100 }
    ]]
    });

    $('#search-cnvcGroupCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcGroupCode',
        textField: 'cnvcGroupName',
        url: 'Services/tbComputationGroup.ashx?Method=query',
        //required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcGroupCode', title: '计量单位组编码', width: 100 },
        { field: 'cnvcGroupName', title: '计量单位组名称', width: 100 }
    ]]
    });  
});

function newtbComputationUnit() {
    $('#dlg').dialog('open').dialog('setTitle', '添加计量单位');
    moveDialog('dlg');
    $('#fm').form('clear');
    $("#cnvcComunitCode").removeAttr("readonly");
    url = "Services/tbComputationUnit.ashx?Method=new";
}
function edittbComputationUnit() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑计量单位');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $('#cc').combogrid('setValue', row.cnvcGroupCode);
        $('#cc').combogrid('setText', row.cnvcGroupCodeComments);
        $("#cnvcComunitCode").attr("readonly", "readonly");
        url = "Services/tbComputationUnit.ashx?Method=update&cnvcComunitCode=" + row.cnvcComunitCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbComputationUnit() {
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

function removetbComputationUnit() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbComputationUnit.ashx', { 'Method': 'remove', 'cnvcComunitCode': row.cnvcComunitCode }, function (json) {
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


function searchtbComputationUnit() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询计量单位');
    moveDialog('search-dlg');
}
function highSearchtbComputationUnit() {
    $('#search-dlg').dialog('close');
    //alert($('#search-cnbMainUnit').attr("checked"));
    //alert($('#search-cnbMainUnit').val());
    $('#dg').datagrid({
        queryParams: {
            'cnvcComunitCode': $('#search-cnvcComunitCode').val(),
            'cnvcComUnitName': $('#search-cnvcComUnitName').val(),
            'cnvcGroupCode': $('#search-cnvcGroupCode').combogrid('getValue'),
            'cnbMainUnit': $('#search-cnbMainUnit').attr("checked")?'on':'',
            'cniChangRate': $('#search-cniChangRate').val()
        }
    });
}

function tbComputationUnitExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbComputationUnit.ashx?Method=excel',
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
