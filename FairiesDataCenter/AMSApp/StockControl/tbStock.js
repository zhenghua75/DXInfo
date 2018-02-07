//$.messager.progress({
//    title: 'Your Title',
//    msg: 'Processing, Please Wait'
//});
var data = { total: 0, rows: [] };
var rows = [];
var isLocal = false;
var stockType = '0';
var dgTitle = '库存';
var mainUrl = 'Services/tbStock.ashx?Method=query';
//$(document).ready(function(){})，可以简写为$(function(){})
$(function () {
    $('#toolbar-monthlyBalance-buttons').hide();
    stockType = $.getUrlVar('StockType');
    if (stockType && stockType != '5')
        mainUrl += '&cnnOperType=' + stockType;
    var iStockType = parseInt(stockType);
    switch (iStockType) {
        case 0:
            dgTitle = '期初库存';
            break;
        case 1:
            dgTitle = '采购入库';
            break;
        case 2:
            dgTitle = '完工入库';
            break;
        case 3:
            dgTitle = '销售出库';
            break;
        case 4:
            dgTitle = '盘点';
            break;
        case 5:
            dgTitle = '月结';
            break;
        case 6:
            dgTitle = '领料单';
            break;
        case 7:
            dgTitle = '调拨';
            break;
    };
    $('#dlg-main').dialog({
        title: '库存主表',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '保存', iconCls: 'icon-ok', handler: function () { savetbStockMain(); return false; } },
                 { text: '取消', iconCls: 'icon-cancel', handler: function () { javascript: $('#dlg-main').dialog('close'); return false; } }
            ],
        onOpen: function () {
            $('#cnvcWhCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcWhCode',
                textField: 'cnvcWhName',
                url: 'Services/tbWarehouse.ashx?Method=query&IsInvalid=false',
                required: true,
                pagination: true,
                mode: 'remote',
                onSelect: function (index, row) {
                    $('#cnvcDeptId').combogrid('setValue', row.cnvcDepID);
                    $('#cnvcDeptId').combogrid('setText', row.cnvcDepIDComments);
                },
                columns: [[
                    { field: 'cnvcWhCode', title: '仓库编码', width: 100 },
                    { field: 'cnvcWhName', title: '仓库名称', width: 100 },
                    { field: 'cnvcDepID', title: '部门编码', width: 100 },
                    { field: 'cnvcDepIDComments', title: '部门名称', width: 100 },
                    { field: 'cnvcWhAddress', title: '地址', width: 100 },
                    { field: 'cnvcWhPhone', title: '电话', width: 100 },
                    { field: 'cnvcWhPersonComments', title: '负责人', width: 100 },
                    { field: 'cnvcWhComments', title: '描述', width: 100 },
                    { field: 'cnbInvalidComments', title: '是否失效', width: 100 }
                ]]
            });
            $('#cnvcDeptId').combogrid({
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
            $('#incnvcWhCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcWhCode',
                textField: 'cnvcWhName',
                url: 'Services/tbWarehouse.ashx?Method=query&IsInvalid=false',
                required: true,
                pagination: true,
                mode: 'remote',
                onSelect: function (index, row) {
                    $('#incnvcDeptId').combogrid('setValue', row.cnvcDepID);
                    $('#incnvcDeptId').combogrid('setText', row.cnvcDepIDComments);
                },
                columns: [[
                    { field: 'cnvcWhCode', title: '仓库编码', width: 100 },
                    { field: 'cnvcWhName', title: '仓库名称', width: 100 },
                    { field: 'cnvcDepID', title: '部门编码', width: 100 },
                    { field: 'cnvcDepIDComments', title: '部门名称', width: 100 },
                    { field: 'cnvcWhAddress', title: '地址', width: 100 },
                    { field: 'cnvcWhPhone', title: '电话', width: 100 },
                    { field: 'cnvcWhPersonComments', title: '负责人', width: 100 },
                    { field: 'cnvcWhComments', title: '描述', width: 100 },
                    { field: 'cnbInvalidComments', title: '是否失效', width: 100 }
                ]]
            });
            $('#incnvcDeptId').combogrid({
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
            $('#cnvcSupplierCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcCode',
                textField: 'cnvcName',
                url: 'Services/tbSupplier.ashx?Method=query&IsInvalid=false',
                required: true,
                pagination: true,
                mode: 'remote',
                columns: [[
                    { field: 'cnvcCode', title: '供应商编码', width: 100 },
                    { field: 'cnvcName', title: '供应商名称', width: 100 },
                    { field: 'cnvcAddress', title: '地址', width: 100 },
                    { field: 'cnvcPostCode', title: '邮编', width: 100 },
                    { field: 'cnvcPhone', title: '电话', width: 100 },
                    { field: 'cnvcFax', title: '传真', width: 100 },
                    { field: 'cnvcEmail', title: '电子邮件', width: 100 },
                    { field: 'cnvcLinkName', title: '联系人', width: 100 },
                    { field: 'cnvcCreateOper', title: '创建人', width: 100 },
                    { field: 'cndCreateDate', title: '创建时间', width: 100 },
                    { field: 'cnbInvalidComments', title: '是否失效', width: 100 }
                ]]
            });
            $('#dg-detail').datagrid({
                //url: 'Services/tbStock.ashx?Method=query&cnnMainId=1',
                width: 800,
                height: 300,
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                title: "库存子表",
                toolbar: [
                    { id: 'toolbar-detail-new', text: '添加', iconCls: 'icon-add', handler: function () { newtbStockDetail(); return false; } },
                    { id: 'toolbar-detail-modify', text: '修改', iconCls: 'icon-edit', handler: function () { edittbStockDetail(); return false; } },
                    { id: 'toolbar-detail-remove', text: '删除', iconCls: 'icon-remove', handler: function () { removetbStockMain(); return false; } }
                ],
                columns: [[
                { field: 'cnnDetailId', title: '子表流水', width: 100 },
                { field: 'cnvcInvCode', title: '存货编码', width: 100 },
                { field: 'cnvcInvCodeComments', title: '存货名称', width: 100 },
                { field: 'cnvcComUnitCode', title: '计量单位编码', width: 100 },
                { field: 'cnvcComUnitCodeComments', title: '计量单位名称', width: 100 },
                { field: 'cnnQuantity', title: '数量', width: 100, formatter: formatNegative4 },
                //                { field: 'cnvcMainComUnitCode', title: '主计量单位编码', width: 100, hidden: true },
                //                { field: 'cnnMainQuantity', title: '主计量单位数量', width: 100, hidden: true },
                {field: 'cnnPrice', title: '单价', width: 100, formatter: toFixed2 },
                { field: 'cnnAmount', title: '金额', width: 100, formatter: formatNegative4 }
            ]]
            });
            switch (iStockType) {
                case 0:
                    //dgTitle = '期初库存';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });
                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 1:
                    //dgTitle = '采购入库';
                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 2:
                    //dgTitle = '完工入库';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });

                    $('#divcnnPrice').hide();
                    $('#cnnPrice').validatebox({ required: false });
                    $('#divcnnAmount').hide();
                    $('#cnnAmount').validatebox({ required: false });

                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 3:
                    //dgTitle = '销售出库';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });

                    $('#divcnnPrice').hide();
                    $('#cnnPrice').validatebox({ required: false });
                    $('#divcnnAmount').hide();
                    $('#cnnAmount').validatebox({ required: false });

                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 4:
                    //dgTitle = '盘点';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });
                    $('#divcnnPrice').hide();
                    $('#cnnPrice').validatebox({ required: false });
                    $('#divcnnAmount').hide();
                    $('#cnnAmount').validatebox({ required: false });

                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 5:
                    //dgTitle = '月结';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });

                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 6:
                    //dgTitle = '领料单';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });
                    $('#divcnnPrice').hide();
                    $('#cnnPrice').validatebox({ required: false });
                    $('#divcnnAmount').hide();
                    $('#cnnAmount').validatebox({ required: false });

                    $('#OutWh').hide();
                    $('#InWh').hide();
                    $('#divInWh').hide();
                    $('#divInDept').hide();
                    $('#incnvcWhCode').combogrid({ required: false });
                    $('#incnvcDeptId').combogrid({ required: false });
                    break;
                case 7:
                    //dgTitle = '调拨';
                    $('#divcnvcSupplierCode').hide();
                    $('#cnvcSupplierCode').combogrid({ required: false });
                    break;
            }
        }
    });
    $('#dlg-detail').dialog({
        title: '库存子表',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '保存', iconCls: 'icon-ok', handler: function () { savetbStockDetail(); return false; } },
                 { text: '取消', iconCls: 'icon-cancel', handler: function () { javascript: $('#dlg-detail').dialog('close'); return false; } }
            ],
        onOpen: function () {
            $('#cnvcInvCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcInvCode',
                textField: 'cnvcInvName',
                url: 'Services/tbInventory.ashx?Method=query',
                required: true,
                //fitColumns: true,
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                striped: true,
                mode: 'remote',
                onSelect: function (index, row) {
                    switch (iStockType) {
                        case 0:
                            //dgTitle = '期初库存';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);
                            break;
                        case 1:
                            //dgTitle = '采购入库';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcPUComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcPUComUnitCodeComments);
                            break;
                        case 2:
                            //dgTitle = '完工入库';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcProduceUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcProduceUnitCodeComments);
                            //                    if (row.cnvcProductType != 'SEMIPRODUCT')
                            //                        $('#cnnPrice').val(row.cnfRetailPrice);
                            //                    else {
                            //                        $('#divcnnPrice').hide();
                            //                        $('#cnnPrice').validatebox({ required: false });
                            //                        $('#divcnnAmount').hide();
                            //                        $('#cnnAmount').validatebox({ required: false });
                            //                    }
                            break;
                        case 3:
                            //dgTitle = '销售出库';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSAComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSAComUnitCodeComments);
                            $('#cnnPrice').val(row.cnfRetailPrice);
                            break;
                        case 4:
                            //dgTitle = '盘点';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);
                            break;
                        case 5:
                            //dgTitle = '月结';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);
                            break;
                        case 6:
                            //dgTitle = '领料单';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);
                            break;
                        case 7:
                            //dgTitle = '调拨';
                            $('#cnvcComUnitCode').combogrid('setValue', row.cnvcSTComUnitCode);
                            $('#cnvcComUnitCode').combogrid('setText', row.cnvcSTComUnitCodeComments);
                            break;
                    }
                },
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
                    { field: 'cnvcPUComUnitCode', width: 100, title: '采购计量单位编码' },
                    { field: 'cnvcPUComUnitCodeComments', width: 100, title: '采购计量单位名称' },
                    { field: 'cnvcSTComUnitCodeComments', width: 100, title: '库存计量单位' },
                    { field: 'cnvcProduceUnitCodeComments', width: 100, title: '生产计量单位' },
			        { field: 'cnfRetailPrice', width: 100, title: '零售价格' },
                    { field: 'cnvcShopUnitCodeComments', width: 100, title: '零售计量单位' },
			        { field: 'cnvcFeel', width: 100, title: '口感' },
			        { field: 'cnvcOrganise', width: 100, title: '组织' },
			        { field: 'cnvcColor', width: 100, title: '内馅' },
			        { field: 'cnvcTaste', width: 100, title: '表面装饰' },
                    { field: 'cnvcProductType', width: 100, title: '产品组编码' },
			        { field: 'cnvcProductTypeComments', width: 100, title: '产品组名称' }
            ]]
            });
            $('#cnvcComUnitCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcComunitCode',
                textField: 'cnvcComUnitName',
                url: 'Services/tbComputationUnit.ashx?Method=query',
                required: true,
                pagination: true,
                striped: true,
                mode: 'remote',
                columns: [[
                { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
                { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
                { field: 'cnvcGroupCodeComments', title: '计量单位组', width: 100 },
                { field: 'cnbMainUnitComments', title: '是否主计量单位', width: 100 },
                { field: 'cniChangRate', title: '换算率', width: 100 }
            ]]
            });
        }
    });
    $('#dg-main').datagrid({
        width: 1000,
        height: 350,
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        title: dgTitle,
        url: mainUrl,
        toolbar: [
            { id: 'toolbar-main-new', text: '添加', iconCls: 'icon-add', handler: function () { newtbStockMain(); return false; } },
            { id: 'toolbar-main-modify', text: '修改', iconCls: 'icon-edit', handler: function () { edittbStockMain(); return false; } },
            { id: 'toolbar-main-check', text: '审核', iconCls: 'icon-edit', handler: function () { checktbStockMain(); return false; } },
            { id: 'toolbar-main-uncheck', text: '弃审', iconCls: 'icon-edit', handler: function () { unchecktbStockMain(); return false; } },
            { id: 'toolbar-main-remove', text: '删除', iconCls: 'icon-remove', handler: function () { removetbStockMain(); return false; } },
            { id: 'toolbar-main-monthlyBalance', text: '月结列表', iconCls: 'icon-list', handler: function () { monthlyBalanceList(); return false; } },
            { id: 'toolbar-main-search', text: '搜索', iconCls: 'icon-search', handler: function () { searchtbStock(); return false; } },
            { id: 'toolbar-main-save', text: '导出', iconCls: 'icon-save', handler: function () { tbStockExportToExcel(); return false; } },
            { id: 'toolbar-main-print', text: '打印', iconCls: 'icon-print', handler: function () { printtbStockMain(); return false; } }
        ],
        columns: [[
            { field: 'cnnMainId', title: '主表流水', width: 100 },
            { field: 'cnvcSupplierCode', title: '供应商编码', width: 100 },
            { field: 'cnvcSupplierCodeComments', title: '供应商名称', width: 100 },
            { field: 'cnvcWhCode', title: '仓库编码', width: 100 },
            { field: 'cnvcWhCodeComments', title: '仓库名称', width: 100 },
            { field: 'cnvcDeptId', title: '部门编码', width: 100 },
            { field: 'cnvcDeptIdComments', title: '部门名称', width: 100 },
            { field: 'cnnOperType', title: '出入库类型编码', width: 100, hidden: true },
            { field: 'cnnOperTypeComments', title: '出入库类型名称', width: 100, hidden: true },
            { field: 'cndCreateDate', title: '创建时间', width: 100 },
            { field: 'cnvcCreaterId', title: '创建人编码', width: 100 },
            { field: 'cnvcCreaterName', title: '创建人姓名', width: 100 },
            { field: 'cndCheckDate', title: '审核时间', width: 100 },
            { field: 'cnvcCheckerId', title: '审核人编码', width: 100 },
            { field: 'cnvcCheckerName', title: '审核人姓名', width: 100 },
            { field: 'cndBusinessDate', title: '业务日期', width: 100, formatter: formatDate },
            { field: 'cnnYear', title: '年', width: 100, hidden: true },
            { field: 'cnnMonth', title: '月', width: 100, hidden: true },
            { field: 'cnnStatus', title: '状态', width: 100, hidden: true },
            { field: 'cnnStatusComments', title: '状态', width: 100 },
            { field: 'cnvcComments', title: '描述', width: 100 },
            { field: 'cnnDetailId', title: '子表流水', width: 100 },
            { field: 'cnvcInvCode', title: '存货编码', width: 100 },
            { field: 'cnvcInvCodeComments', title: '存货名称', width: 100 },
            { field: 'cnvcComUnitCode', title: '计量单位编码', width: 100 },
            { field: 'cnvcComUnitCodeComments', title: '计量单位名称', width: 100 },
            { field: 'cnnQuantity', title: '数量', width: 100, formatter: formatNegative4 },
            { field: 'cnvcMainComUnitCode', title: '主计量单位编码', width: 100, hidden: true },
            { field: 'cnnMainQuantity', title: '主计量单位数量', width: 100, hidden: true },
            { field: 'cnnPrice', title: '单价', width: 100, formatter: toFixed2 },
            { field: 'cnnAmount', title: '金额', width: 100, formatter: formatNegative4 }
        ]]
    });

    switch (iStockType) {
        case 0:
            dgTitle = '期初库存';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
        case 1:
            dgTitle = '采购入库';
            $('#toolbar-main-monthlyBalance').hide();
            break;
        case 2:
            dgTitle = '完工入库';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
        case 3:
            dgTitle = '销售出库';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
        case 4:
            dgTitle = '盘点';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
        case 5:
            dgTitle = '月结';
            $('#toolbar-main-new').hide();
            $('#toolbar-main-modify').hide();
            $('#toolbar-main-check').hide();
            $('#toolbar-main-uncheck').hide();
            $('#toolbar-main-remove').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            $('#dg-main').datagrid('showColumn', 'cnnOperTypeComments');
            break;
        case 6:
            dgTitle = '领料单';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
        case 7:
            dgTitle = '调拨';
            $('#toolbar-main-monthlyBalance').hide();
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCode');
            $('#dg-main').datagrid('hideColumn', 'cnvcSupplierCodeComments');
            break;
    }

    $('#search-dlg').dialog({
        title: '查询库存',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '查询', iconCls: 'icon-search', handler: function () { highSearchtbStock(); return false; } },
                 { text: '取消', iconCls: 'icon-cancel', handler: function () { javascript: $('#search-dlg').dialog('close'); return false; } }
            ],
        onOpen: function () {
            $('#search-cnvcSupplierCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcCode',
                textField: 'cnvcName',
                url: 'Services/tbSupplier.ashx?Method=query&IsInvalid=false',
                pagination: true,
                mode: 'remote',
                columns: [[
                    { field: 'cnvcCode', title: '供应商编码', width: 100 },
                    { field: 'cnvcName', title: '供应商名称', width: 100 },
                    { field: 'cnvcAddress', title: '地址', width: 100 },
                    { field: 'cnvcPostCode', title: '邮编', width: 100 },
                    { field: 'cnvcPhone', title: '电话', width: 100 },
                    { field: 'cnvcFax', title: '传真', width: 100 },
                    { field: 'cnvcEmail', title: '电子邮件', width: 100 },
                    { field: 'cnvcLinkName', title: '联系人', width: 100 },
                    { field: 'cnvcCreateOper', title: '创建人', width: 100 },
                    { field: 'cndCreateDate', title: '创建时间', width: 100 },
                    { field: 'cnbInvalidComments', title: '是否失效', width: 100 }
                ]]
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
            $('#search-cnvcComUnitCode').combogrid({
                panelWidth: 650,
                idField: 'cnvcComunitCode',
                textField: 'cnvcComUnitName',
                url: 'Services/tbComputationUnit.ashx?Method=query',
                pagination: true,
                striped: true,
                mode: 'remote',
                columns: [[
                    { field: 'cnvcComunitCode', title: '计量单位编码', width: 100 },
                    { field: 'cnvcComUnitName', title: '计量单位名称', width: 100 },
                    { field: 'cnvcGruopCodeComments', title: '计量单位组', width: 100 },
                    { field: 'cnbMainUnit', title: '是否主计量单位', width: 100 },
                    { field: 'cniChangRate', title: '换算率', width: 100 }
                ]]
            });
            $('#search-cnnStatus').combogrid({
                panelWidth: 650,
                idField: 'cnvcCode',
                textField: 'cnvcName',
                url: 'Services/tbNameCode.ashx?Method=StockStatus',
                //required: true,
                columns: [[
                { field: 'cnvcCode', title: '编码', width: 100 },
                { field: 'cnvcName', title: '名称', width: 100 },
                { field: 'cnvcComments', title: '描述', width: 100 }
            ]]
            });
        }
    });

    $('#cndBusinessDate').datebox({ required: true
    });
    $('#search-cndBusinessDate').datebox({
    });
    $('#search-cndCreateDate').datetimebox({
    });
    $('#search-cndCheckDate').datetimebox({
    });
    $('#search-cndBusinessDate2').datebox({
    });
    $('#search-cndCreateDate2').datetimebox({
    });
    $('#search-cndCheckDate2').datetimebox({
    });



    $('#dlg-monthlyBalance').dialog({
        title: '月结清单',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '关闭', iconCls: 'icon-cancel', handler: function () { javascript: $('#dlg-monthlyBalance').dialog('close'); return false; } }
            ],
        onOpen: function () {
            $('#dg-monthlyBalance').datagrid({
                width: 900,
                height: 350,
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                title: '月结清单',
                url: 'Services/tbMonthlyBalance.ashx?Method=query',
                toolbar: [
                    { id: 'toolbar-monthlyBalance-new', text: '添加', iconCls: 'icon-add', handler: function () { newtbMonthlyBalance(); return false; } },
                    { id: 'toolbar-monthlyBalance-modify', text: '月结', iconCls: 'icon-edit', handler: function () { balance(); return false; } },
                    { id: 'toolbar-monthlyBalance-check', text: '取消月结', iconCls: 'icon-edit', handler: function () { cancelBalance(); return false; } }
                ],
                columns: [[
                    { field: 'cnnYear', title: '年', width: 50 },
                    { field: 'cnnMonth', title: '月', width: 20 },
                    { field: 'cnbIsBalance', title: '是否月结', width: 80, formatter: formatCheckBox },
                    { field: 'cnvcCreater', title: '创建人编码', width: 100 },
                    { field: 'cnvcCreaterName', title: '创建人姓名', width: 100 },
                    { field: 'cndCreateDate', title: '创建时间', width: 140 },
                    { field: 'cnvcBalancer', title: '月结人编码', width: 100 },
                    { field: 'cnvcBalancerName', title: '月结人姓名', width: 100 },
                    { field: 'cndBalanceDate', title: '月结时间', width: 140 },
                    { field: 'cnvcModifier', title: '修改人编码', width: 100 },
                    { field: 'cnvcModifierName', title: '修改人姓名', width: 100 },
                    { field: 'cndModifyDate', title: '修改时间', width: 140 }
                ]]
            });
        }
    });

    $('#cnnQuantity').change(function () {
        var quantity = $('#cnnQuantity').val();
        if (quantity.length == 0) quantity = "0";
        var price = $('#cnnPrice').val();
        if (price.length == 0) price = "0";
        $('#cnnAmount').val(parseFloat(quantity) * parseFloat(price));
    }).change();
    $('#cnnPrice').change(function () {
        var quantity = $('#cnnQuantity').val();
        if (quantity.length == 0) quantity = "0";
        var price = $('#cnnPrice').val();
        if (price.length == 0) price = "0";
        $('#cnnAmount').val(parseFloat(quantity) * parseFloat(price));
    }).change();
    $('#dlg-monthlyBalance-oper').dialog({
        title: '月结',
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '保存', iconCls: 'icon-ok', handler: function () { savetbMonthlyBalance(); return false; } },
                 { text: '取消', iconCls: 'icon-cancel', handler: function () { javascript: $('#dlg-monthlyBalance-oper').dialog('close'); return false; } }
            ],
        onOpen: function () {
            $('#cnnYear').combobox(
            {
                url: 'Services/tbMonthlyBalance.ashx?Method=cnnYear',
                valueField: 'id',
                textField: 'text',
                required: true,
                multiple: false
            }
            );
            $('#cnnMonth').combobox(
            {
                url: 'Services/tbMonthlyBalance.ashx?Method=cnnMonth',
                valueField: 'id',
                textField: 'text',
                required: true,
                multiple: false
            }
            );
        }
    });



    $('#dlg-print').dialog({
        title: dgTitle,
        modal: true,
        collapsible: true,
        resizable: true,
        closed: true,
        shadow: false,
        buttons: [{ text: '打印', iconCls: 'icon-print', handler: function () { print(); return false; } },
                 { text: '取消', iconCls: 'icon-cancel', handler: function () { javascript: $('#dlg-print').dialog('close'); return false; } }
                 ]
    });

    $('#dg-print').datagrid({
        //url: 'Services/tbStock.ashx?Method=query&cnnMainId=1',
        rownumbers: true,
        columns: [[
                    { field: 'cnvcInvCode', title: '存货编码', width: 100 },
                    { field: 'cnvcInvCodeComments', title: '存货名称', width: 100 },
                    { field: 'cnvcComUnitCodeComments', title: '单位', width: 100 },
                    { field: 'cnnQuantity', title: '数量', width: 100, formatter: formatNegative2 },
                    { field: 'cnnPrice', title: '单价', width: 100, formatter: toFixed2 },
                    { field: 'cnnAmount', title: '金额', width: 140, formatter: formatNegative2 }
                ]]
    });
    //$.messager.progress('close');
    
});

function newtbStockMain() {
//    $.messager.progress({
//        title: '添加',
//        msg: '请等待！'
//    });
    $('#dlg-main').dialog('open').dialog('setTitle', '添加库存主表');
    moveDialog('dlg-main');
    $('#fm-main').form('clear');
    $('#fm-main').attr('action', 'Services/tbStock.ashx?Method=newMain');
    isLocal = true;
    data = { total: 0, rows: [] };
    rows = [];
    $('#dg-detail').datagrid({
        url: ''
    });
    $('#dg-detail').datagrid('loadData', data);
    //$.messager.progress('close');
    if (stockType == "7") {
        $('#OutWh').show();
        $('#InWh').show();
        $('#divInWh').show();
        $('#divInDept').show();
        $('#incnvcWhCode').combogrid({ required: true });
        $('#incnvcDeptId').combogrid({ required: true });
    }
}
function newtbStockDetail() {
    $('#dlg-detail').dialog('open').dialog('setTitle', '添加库存子表');
    moveDialog('dlg-detail');
    $('#fm-detail').form('clear');
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        //isLocal = false;
        $('#cnnMainId-detail').val(row.cnnMainId);
        $('#fm-detail').attr('action', 'Services/tbStock.ashx?Method=newDetail');
    }
    else {
        //isLocal = true;
    }
}
function edittbStockMain() {
    isLocal = false;
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        $('#dlg-main').dialog('open').dialog('setTitle', '编辑库存主表');
        moveDialog('dlg-main');
        $('#fm-main').form('load', row);
        $('#cnvcSupplierCode').combogrid('setValue', row.cnvcSupplierCode);
        $('#cnvcSupplierCode').combogrid('setText', row.cnvcSupplierCodeComments);
        $('#cnvcWhCode').combogrid('setValue', row.cnvcWhCode);
        $('#cnvcWhCode').combogrid('setText', row.cnvcWhCodeComments);
        $('#cnvcDeptId').combogrid('setValue', row.cnvcDeptId);
        $('#cnvcDeptId').combogrid('setText', row.cnvcDeptIdComments);

        $('#OutWh').hide();
        $('#InWh').hide();
        $('#divInWh').hide();
        $('#divInDept').hide();
        $('#incnvcWhCode').combogrid({ required: false });
        $('#incnvcDeptId').combogrid({ required: false });

        $('#fm-main').attr('action', 'Services/tbStock.ashx?Method=updateMain&cnnMainId=' + row.cnnMainId);
        //$('#dg-detail').datagrid('load', { 'cnnMainId': row.cnnMainId });
        $('#dg-detail').datagrid({
            url: 'Services/tbStock.ashx?Method=query&cnnMainId=' + row.cnnMainId
        });
        //$('#dg-detail').datagrid('reload');
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function checktbStockMain() {
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        $.post('Services/tbStock.ashx', { 'method': 'checkMain', 'cnnMainId': row.cnnMainId },
            function (result) {
                var json = eval('(' + result + ')');
                if (json.success==true) {
                    $('#dg-main').datagrid('reload');    // reload the user data  
                }
                else {
                    $.messager.show({
                        title: '错误信息',
                        msg: json.msg
                    });
                }
            });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function unchecktbStockMain() {
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        $.post('Services/tbStock.ashx', { 'method': 'uncheckMain', 'cnnMainId': row.cnnMainId },
            function (result) {
                var json = eval('(' + result + ')');
                if (json.success == true) {
                    $('#dg-main').datagrid('reload');    // reload the user data  
                }
                else {
                    $.messager.show({
                        title: '错误信息',
                        msg: json.msg
                    });
                }
            });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function edittbStockDetail() {
    var row = $('#dg-detail').datagrid('getSelected');
    if (row) {
        $('#dlg-detail').dialog('open').dialog('setTitle', '编辑库存子表');
        moveDialog('dlg-detail');
        $('#fm-detail').form('load', row);
        //alert(row.cnvcInvCode + '=' + row.cnvcInvCodeComments);
        $('#cnvcInvCode').combogrid('setValue', row.cnvcInvCode);
        $('#cnvcInvCode').combogrid('setText', row.cnvcInvCodeComments);
        $('#cnvcComUnitCode').combogrid('setValue', row.cnvcComUnitCode);
        $('#cnvcComUnitCode').combogrid('setText', row.cnvcComUnitCodeComments);
        //alert($('#cnvcInvCode').combogrid('getValue')+'='+$('#cnvcInvCode').combogrid('getText'));
        $('#fm-detail').attr('action','Services/tbStock.ashx?Method=updateDetail&cnnDetailId=' + row.cnnDetailId);        
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行编辑'
        });
    }
}
function savetbStockMain() {
    if (isLocal) {
        var url = $('#fm-main').attr('action');        
        var para="&AddDetail=true&DetailCount="+rows.length;
        $.each(rows, function (index, value) {
            $.each(value, function (key, value1) {
                para += "&"+key + "[" + index + "]=" + value1;
            });
        });
        url += para;
        $('#fm-main').attr('action', url); 
    }
    $('#fm-main').form('submit', {
        onSubmit: function () {
            $('#cnnOperType').val(stockType);
            return $(this).form('validate');
        },
        success: function (result) {
            var json = eval('(' + result + ')');
            if (json.success) {
                $('#dlg-main').dialog('close');      // close the dialog  
                $('#dg-main').datagrid('reload');    // reload the user data  
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
function savetbStockDetail() {
    if (isLocal) {
        if ($('#fm-detail').form('validate')) {
            var invCode = $('#cnvcInvCode').combogrid('getValue');
            var invName = $('#cnvcInvCode').combogrid('getText');
            var unitCode = $('#cnvcComUnitCode').combogrid('getValue');
            var unitName = $('#cnvcComUnitCode').combogrid('getText');
            var quantity = $('#cnnQuantity').val();
            var price = $('#cnnPrice').val();
            var amount = $('#cnnAmount').val();
            var row = { cnvcInvCode: invCode, cnvcInvCodeComments: invName, cnvcComUnitCode: unitCode, cnvcComUnitCodeComments: unitName,
                cnnQuantity: quantity, cnnPrice: price, cnnAmount: amount
            };
            rows.push(row);
            data = { total: rows.length, rows: rows }
            $('#dlg-detail').dialog('close');
            $('#dg-detail').datagrid('loadData', data);
        }
    }
    else {
        $('#fm-detail').form('submit', {
            onSubmit: function () {
                return $(this).form('validate');
            },
            success: function (result) {
                var json = eval('(' + result + ')');
                if (json.success) {
                    $('#dlg-detail').dialog('close');      // close the dialog  
                    $('#dg-detail').datagrid('reload');    // reload the user data  
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
}
function removetbStockMain() {
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbStock.ashx', { 'Method': 'removeMain', 'cnnMainId': row.cnnMainId }, function (result) {
                    var json = eval('(' + result + ')');
                    if (json.success == true) {
                        $('#dg-main').datagrid('reload');
                    } else {
                        $.messager.show({
                            title: '错误信息',
                            msg: json.msg
                        });
                    }
                });
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
function removetbStockDetail() {
    var row = $('#dg-detail').datagrid('getSelected');
    if (row) {
        $.messager.confirm('提示', '是否确定删除?', function (r) {
            if (r) {
                $.post('Services/tbStock.ashx', { 'Method': 'removeDetail', 'cnnDetailId': row.cnnDetailId }, function (result) {                    
                    var json = eval('(' + result + ')');
                    if (json.success == true) {
                        $('#dg-detail').datagrid('reload');
                    } else {
                        $.messager.show({
                            title: '错误信息',
                            msg: json.msg
                        });
                    }
                });
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


function searchtbStock() {
    $('#search-dlg').dialog('open').dialog('setTitle', '查询库存主表');
    moveDialog('search-dlg');
}
function highSearchtbStock() {
    $('#search-dlg').dialog('close');
    //var invCode = $('#cnvcInvCode').combogrid('getValue');
    var sStockType = stockType;
    if (sStockType = '5') sStockType = '';
    $('#dg-main').datagrid({
        queryParams: {
            'cnnMainId': $('#search-cnnMainId').val(),
            'cnvcSupplierCode': $('#search-cnvcSupplierCode').combogrid('getValue'),
            'cnvcWhCode': $('#search-cnvcWhCode').combogrid('getValue'),
            'cnvcDeptId': $('#search-cnvcDeptId').combogrid('getValue'),
            'cnnOperType': sStockType,
            'cndCreateDate': $('#search-cndCreateDate').datebox('getValue'),
            'cndCreateDate2': $('#search-cndCreateDate2').datebox('getValue'),
            'cnvcCreaterId': $('#search-cnvcCreaterId').val(),
            'cnvcCreaterName': $('#search-cnvcCreaterName').val(),
            'cndCheckDate': $('#search-cndCheckDate').datebox('getValue'),
            'cndCheckDate2': $('#search-cndCheckDate2').datebox('getValue'),
            'cnvcCheckerId': $('#search-cnvcCheckerId').val(),
            'cnvcCheckerName': $('#search-cnvcCheckerName').val(),
            'cndBusinessDate': $('#search-cndBusinessDate').datebox('getValue'),
            'cndBusinessDate2': $('#search-cndBusinessDate2').datebox('getValue'),
            //'cnnYear': $('#search-cnnYear').val(),
            //'cnnMonth': $('#search-cnnMonth').val(),
            'cnnStatus': $('#search-cnnStatus').combogrid('getValue'),
            'cnvcComments': $('#search-cnvcComments').val(),
            'cnnDetailId': $('#search-cnnDetailId').val(),
            //'cnnMainId': $('#search-cnnMainId').val(),
            'cnvcInvCode': $('#search-cnvcInvCode').combogrid('getValue'),
            'cnvcComUnitCode': $('#search-cnvcComUnitCode').combogrid('getValue'),
            'cnnQuantity': $('#search-cnnQuantity').val(),
            //'cnvcMainComUnitCode': $('#search-cnvcMainComUnitCode').val(),
            //'cnnMainQuantity': $('#search-cnnMainQuantity').val(),
            'cnnPrice': $('#search-cnnPrice').val(),
            'cnnAmount': $('#search-cnnAmount').val()
        }
    });
}

function tbStockExportToExcel() {
    $('#export-excel').form('submit', {
        url: 'Services/tbStock.ashx?Method=excel&cnnOperType=' + stockType,
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

function monthlyBalanceList() {
    $('#dlg-monthlyBalance').dialog('open');
    moveDialog('dlg-monthlyBalance');
}
function newtbMonthlyBalance() {
    $('#dlg-monthlyBalance-oper').dialog('open').dialog('setTitle', '添加月结');
    moveDialog('dlg-monthlyBalance-oper');
    $('#fm-monthlyBalance-oper').form('clear');
    $('#fm-monthlyBalance-oper').attr('action', 'Services/tbMonthlyBalance.ashx?Method=newMonthlyBalance');
}
function savetbMonthlyBalance() {
    $('#fm-monthlyBalance-oper').form('submit', {
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (result) {
            var json = eval('(' + result + ')');
            if (json.success) {
                $('#dlg-monthlyBalance-oper').dialog('close');      // close the dialog  
                $('#dg-monthlyBalance').datagrid('reload');    // reload the user data  
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

function balance() {
    //alert('balance');
    var row = $('#dg-monthlyBalance').datagrid('getSelected');
    //alert(row.cnnYear);
    if (row) {
        $.post('Services/tbMonthlyBalance.ashx', { 'Method': 'balance', 'cnnYear': row.cnnYear,'cnnMonth':row.cnnMonth }, function (result) {
            var json = eval('(' + result + ')');
            if (json.success == true) {
                $('#dg-monthlyBalance').datagrid('reload');
                $('#dg-main').datagrid('reload');                
            } else {
                $.messager.show({
                    title: '错误信息',
                    msg: json.msg
                });
            }
        });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行月结'
        });
    }
}
function cancelBalance() {
    var row = $('#dg-monthlyBalance').datagrid('getSelected');
    if (row) {
        $.post('Services/tbMonthlyBalance.ashx', { 'Method': 'cancelBalance', 'cnnYear': row.cnnYear, 'cnnMonth': row.cnnMonth }, function (result) {
            var json = eval('(' + result + ')');
            if (json.success == true) {
                $('#dg-monthlyBalance').datagrid('reload');
                $('#dg-main').datagrid('reload');
            } else {
                $.messager.show({
                    title: '错误信息',
                    msg: json.msg
                });
            }
        });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行取消月结'
        });
    }
}


function printtbStockMain() {
    var row = $('#dg-main').datagrid('getSelected');
    if (row) {
        $('#dlg-print').dialog('open').dialog('setTitle', '打印' + dgTitle);
        moveDialog('dlg-print');
        //$('#fm-print').form('load', row);
        $('#print-cnnMainId').text(row.cnnMainId);
        $('#print-cnvcSupplierCodeComments').text(row.cnvcSupplierCodeComments);
        $('#print-cnvcWhCodeComments').text(row.cnvcWhCodeComments);
        $('#print-cnvcDeptIdComments').text(row.cnvcDeptIdComments);
        $('#print-cndBusinessDate').text(row.cndBusinessDate);
        $('#print-cnvcComments').text(row.cnvcComments);
        //$('#dg-print').datagrid('load', { 'cnnMainId': row.cnnMainId });
        $('#dg-print').datagrid({
            url: 'Services/tbStock.ashx?Method=query&cnnMainId=' + row.cnnMainId
        });
    }
    else {
        $.messager.show({
            title: '错误信息',
            msg: '请选择一行数据进行打印'
        });
    }
}
function print() {
    $("#divprint").printElement({ printMode: 'popup', pageTitle: dgTitle, overrideElementCSS: [
		'easyui.css', { href: 'scripts/jquery-easyui/themes/cupertino/easyui.css', media: 'all' },
        'label.css', { href: 'css/label.css', media: 'all' },
        'icon.css', { href: 'scripts/jquery-easyui/themes/icon.css', media: 'all'}],
        styleToAdd: 'padding:10px;margin:10px;color:#FFFFFF !important;',
        iframeElementOptions:
            {
                styleToAdd: 'position:absolute;width:0px;height:0px;bottom:0px;'
            }
    });
//    $("#divprint").printArea({ mode: "popup", popClose: false });
}