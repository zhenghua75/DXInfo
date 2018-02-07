var url;
$(function () {
    $('#cc').combogrid({
        panelWidth: 650,
        idField: 'cnvcProductClassCode',
        textField: 'cnvcProductClassName',
        url: 'Services/tbProductClass.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcProductTypeComments', title: '产品组', width: 100 },
        { field: 'cnvcProductClassCode', title: '类别编码', width: 100 },
        { field: 'cnvcProductClassName', title: '类别名称', width: 120 },
        { field: 'cnnDays', title: '过期天数', width: 100 },
        { field: 'cnvcComments', title: '描述', width: 200 }
    ]],
        onChange: function () {
            $.ajax({
                url: 'Services/tbInventory.ashx?Method=invcode&InvCCode=' + $(this).combogrid("getValue"),
                type: "GET",
                success: function (data) {
                    if (data) {
                        $("#cnvcInvCode").val(data);
                    }
                }
            });
        }
    });

    $('#dlg').dialog({
        title: '存货档案',
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
        striped: true,
        fitColumns: false,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: "存货档案",
        url: 'Services/tbInventory.ashx?Method=query',
        toolbar: '#toolbar'
    });

    $('#search-dlg').dialog({
        title: '查询存货档案',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: '#search-dlg-buttons'
    });
    $('#search-cc').combogrid({
        panelWidth: 650,
        idField: 'cnvcProductClassCode',
        textField: 'cnvcProductClassName',
        url: 'Services/tbProductClass.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcProductTypeComments', title: '产品组', width: 100 },
        { field: 'cnvcProductClassCode', title: '类别编码', width: 100 },
        { field: 'cnvcProductClassName', title: '类别名称', width: 120 },
        { field: 'cnnDays', title: '过期天数', width: 100 },
        { field: 'cnvcComments', title: '描述', width: 200 }
    ]]
    });

    $('#dd1').datebox({
    });
    $('#dd2').datebox({
    });
    $('#search-dd1').datebox({
    });
    $('#search-dd2').datebox({
    });
    $('#search-dd3').datebox({
    });
    $('#search-cndSDate1').datebox({
    });
    $('#search-cndEDate1').datebox({
    });
    $('#search-cndModifyDate1').datebox({
    });
    $('#cnvcValueType').combogrid({
        panelWidth: 650,
        idField: 'cnvcCode',
        textField: 'cnvcName',
        url: 'Services/tbNameCode.ashx?Method=ValueType',
        required: true,
        columns: [[
        { field: 'cnvcCode', title: '编码', width: 100 },
        { field: 'cnvcName', title: '名称', width: 100 },
        { field: 'cnvcComments', title: '描述', width: 100 }
    ]]
    });
    $('#cnvcGroupCode').combogrid({
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

    $('#cnvcComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });

    $('#cnvcSAComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#cnvcPUComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#cnvcSTComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#cnvcProduceUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#cnvcShopUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        required: true,
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    //搜索
    $('#search-cnvcValueType').combogrid({
        panelWidth: 650,
        idField: 'cnvcCode',
        textField: 'cnvcName',
        url: 'Services/tbNameCode.ashx?Method=ValueType',
        columns: [[
        { field: 'cnvcCode', title: '编码', width: 100 },
        { field: 'cnvcName', title: '名称', width: 100 },
        { field: 'cnvcComments', title: '描述', width: 100 }
    ]]
    });
    $('#search-cnvcGroupCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcGroupCode',
        textField: 'cnvcGroupName',
        url: 'Services/tbComputationGroup.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcGroupCode', title: '计量单位组编码', width: 100 },
        { field: 'cnvcGroupName', title: '计量单位组名称', width: 100 }
    ]]
    });

    $('#search-cnvcComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });

    $('#search-cnvcSAComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#search-cnvcPUComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#search-cnvcSTComUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#search-cnvcProduceUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });
    $('#search-cnvcShopUnitCode').combogrid({
        panelWidth: 650,
        idField: 'cnvcComunitCode',
        textField: 'cnvcComUnitName',
        url: 'Services/tbComputationUnit.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
        { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
        { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
        { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
        { field: 'cniChangRate', title: '换算率', width: 100 }
    ]]
    });

    $('#search-cnvcCreatePerson').combogrid({
        panelWidth: 650,
        idField: 'vcLoginID',
        textField: 'vcOperName',
        url: 'Services/tbLogin.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'vcLoginID', title: '操作员ID', width: 100 },
        { field: 'vcOperName', title: '操作员姓名', width: 100 },
        { field: 'vcDeptIDComments', title: '门店', width: 100 }
    ]]
    });
    $('#search-cnvcModifyPerson').combogrid({
        panelWidth: 650,
        idField: 'vcLoginID',
        textField: 'vcOperName',
        url: 'Services/tbLogin.ashx?Method=query',
        mode: 'remote',
        pagination: true,
        rownumbers: true,
        columns: [[
        { field: 'vcLoginID', title: '操作员ID', width: 100 },
        { field: 'vcOperName', title: '操作员姓名', width: 100 },
        { field: 'vcDeptIDComments', title: '门店', width: 100 }
    ]]
    });
});

function newInventory() {
    $('#dlg').dialog('open').dialog('setTitle', '添加存货');
    moveDialog('dlg');
    $('#fm').form('clear');
    $("#cnvcInvCode").removeAttr("readonly");
    $("#cc").combogrid("enable");
    url = "Services/tbInventory.ashx?Method=new";
}
function editInventory() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '编辑存货');
        moveDialog('dlg');
        $('#fm').form('load', row);
        $('#cc').combogrid('setValue', row.cnvcInvCCode);
        $('#cc').combogrid('setText', row.cnvcInvCCodeComments);

        $('#cnvcValueType').combogrid('setValue', row.cnvcValueType);
        $('#cnvcValueType').combogrid('setText', row.cnvcValueTypeComments);

        $('#cnvcGroupCode').combogrid('setValue', row.cnvcGroupCode);
        $('#cnvcGroupCode').combogrid('setText', row.cnvcGroupCodeComments);

        $('#cnvcComUnitCode').combogrid('setValue', row.cnvcComUnitCode);
        $('#cnvcComUnitCode').combogrid('setText', row.cnvcComUnitCodeComments);

        $('#cnvcSAComUnitCode').combogrid('setValue', row.cnvcSAComUnitCode);
        $('#cnvcSAComUnitCode').combogrid('setText', row.cnvcSAComUnitCodeComments);

        $('#cnvcPUComUnitCode').combogrid('setValue', row.cnvcPUComUnitCode);
        $('#cnvcPUComUnitCode').combogrid('setText', row.cnvcPUComUnitCodeComments);

        $('#cnvcSTComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
        $('#cnvcSTComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);

        $('#cnvcProduceUnitCode').combogrid('setValue', row.cnvcProduceUnitCode);
        $('#cnvcProduceUnitCode').combogrid('setText', row.cnvcProduceUnitCodeComments);

        $('#cnvcShopUnitCode').combogrid('setValue', row.cnvcShopUnitCode);
        $('#cnvcShopUnitCode').combogrid('setText', row.cnvcShopUnitCodeComments);
        $("#cnvcInvCode").attr("readonly", "readonly");
        $("#cc").combogrid("disable");
        url = "Services/tbInventory.ashx?Method=update&cnvcInvCode=" + row.cnvcInvCode;
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function saveInventory() {
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

function removeInventory() {
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbInventory.ashx', { 'Method': 'remove', 'cnvcInvCode': row.cnvcInvCode }, function (json) {
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


function searchInventory() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询存货');
    moveDialog('search-dlg');
}
function highSearchInventory() {
    $('#search-dlg').dialog('close');

    $('#dg').datagrid({
        queryParams: {
            "cnvcInvCCode": $("#search-cc").combogrid("getValue"),
            "cnvcInvCode": $("#search-cnvcInvCode").val(),
            "cnvcInvName": $("#search-cnvcInvName").val(),
            "cnbProductBill": $('#search-cnbProductBill').attr("checked") ? 'on' : '',
            "cnbSale": $('#search-cnbSale').attr("checked") ? 'on' : '',
            "cnbPurchase": $('#search-cnbPurchase').attr("checked") ? 'on' : '',
            "cnbSelf": $('#search-cnbSelf').attr("checked") ? 'on' : '',
            "cnbComsume": $('#search-cnbComsume').attr("checked") ? 'on' : '',
            "cniInvCCost": $("#search-cniInvCCost").val(),
            "cniInvNCost": $("#search-cniInvNCost").val(),
            "cniSafeNum": $("#search-cniSafeNum").val(),
            "cniLowSum": $("#search-cniLowSum").val(),
            "cnnExpire": $("#search-cnnExpire").val(),
            "cnnDue": $("#search-cnnDue").val(),
            "cndSDate": $("#search-dd1").datebox("getValue"),
            "cndSDate1": $("#search-cndSDate1").datebox("getValue"),
            "cndEDate": $("#search-dd2").datebox("getValue"),
            "cndEDate1": $("#search-cndEDate1").datebox("getValue"),
            "cnvcCreatePerson": $("#search-cnvcCreatePerson").combogrid("getValue"),
            "cnvcModifyPerson": $("#search-cnvcModifyPerson").combogrid("getValue"),
            "cndModifyDate": $("#search-dd3").datebox("getValue"),
            "cndModifyDate1": $("#search-cndModifyDate1").datebox("getValue"),
            "cnvcValueType": $("#search-cnvcValueType").combogrid("getValue"),
            "cnvcInvStd": $("#search-cnvcInvStd").val(),
            "cnvcGroupCode": $("#search-cnvcGroupCode").combogrid("getValue"),
            "cnvcComUnitCode": $("#search-cnvcComUnitCode").combogrid("getValue"),
            "cnvcSAComUnitCode": $("#search-cnvcSAComUnitCode").combogrid("getValue"),
            "cnvcPUComUnitCode": $("#search-cnvcPUComUnitCode").combogrid("getValue"),
            "cnvcSTComUnitCode": $("#search-cnvcSTComUnitCode").combogrid("getValue"),
            "cnvcProduceUnitCode": $("#search-cnvcProduceUnitCode").combogrid("getValue"),
            "cnfRetailPrice": $("#search-cnfRetailPrice").val(),
            "cnvcShopUnitCode": $("#search-cnvcShopUnitCode").combogrid("getValue"),
            "cnvcFeel": $("#search-cnvcFeel").val(),
            "cnvcOrganise": $("#search-cnvcOrganise").val(),
            "cnvcColor": $("#search-cnvcColor").val(),
            "cnvcTaste": $("#search-cnvcTaste").val()
        }
    });
}

function inventoryExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbInventory.ashx?Method=excel',
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