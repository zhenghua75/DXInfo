function resetForm() {
    $(prefix + "WhId").val("");//.trigger("liszt:updated");
    $(prefix + "OutWhId").val("");//.trigger("liszt:updated");
    $(prefix + "InWhId").val("");//.trigger("liszt:updated");
    $(prefix + "VenId").val("");//.trigger("liszt:updated");
    $(prefix + "DeptId").val("");//.trigger("liszt:updated");
    $(prefix + "OutDeptId").val("");//.trigger("liszt:updated");
    $(prefix + "InDeptId").val("");//.trigger("liszt:updated");
    $(prefix + "Salesman").val("");//.trigger("liszt:updated");
    $(prefix + "BusType").val("");

    $(prefix + "Code").val("");
    $(prefix + "RdDate").val("");
    $(prefix + "SVDate").val("");
    $(prefix + "TVDate").val("");
    $(prefix + "CVDate").val("");
    $(prefix + "ALVDate").val("");
    $(prefix + "MVDate").val("");
    $(prefix + "ARVCode").val("");
    $(prefix + "ARVDate").val("");
    $(prefix + "VerifyDate").val("");
    $(prefix + "Memo").val("");
    $(prefix + "Id").val("");
    $(prefix + "IsModify").val("false");
    $(prefix + "IsVerify").val("false");

    $(prefix + "MakeTime").val("");
}
function initForm() {
    //日期控件
    $(prefix + "RdDate").datepicker();
    $(prefix + "SVDate").datepicker();
    $(prefix + "TVDate").datepicker();
    $(prefix + "CVDate").datepicker();
    $(prefix + "ALVDate").datepicker();
    $(prefix + "MVDate").datepicker();
    $(prefix + "ARVDate").datepicker();
    //下拉框
    //$(prefix + "WhId").attr("data-placeholder", "选择仓库");
    //$(prefix + "OutWhId").attr("data-placeholder", "选择仓库");
    //$(prefix + "InWhId").attr("data-placeholder", "选择仓库");
    //$(prefix + "VenId").attr("data-placeholder", "选择供应商");
    //$(prefix + "DeptId").attr("data-placeholder", "选择部门");
    //$(prefix + "OutDeptId").attr("data-placeholder", "选择部门");
    //$(prefix + "InDeptId").attr("data-placeholder", "选择部门");
    //$(prefix + "Salesman").attr("data-placeholder", "选择业务员");
    //设置宽度
    $(prefix + "WhId").addClass("wide3");
    $(prefix + "OutWhId").addClass("wide3");
    $(prefix + "InWhId").addClass("wide3");
    $(prefix + "VenId").addClass("wide3");
    $(prefix + "DeptId").addClass("wide3");
    $(prefix + "OutDeptId").addClass("wide3");
    $(prefix + "InDeptId").addClass("wide3");
    $(prefix + "Salesman").addClass("wide3");


    //$(prefix + "WhId").chosen({ allow_single_deselect: true, no_results_text: "没有找到",search_contains: true  });
    //$(prefix + "OutWhId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "InWhId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "VenId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "DeptId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "OutDeptId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "InDeptId").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    //$(prefix + "Salesman").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });

    //禁用
//    if ($(prefix + "BusType").length > 0 && $(prefix + "BusType")[0].tagName.toLowerCase() == "select") {
//        $(prefix + "BusType").attr("data-placeholder", "选择入库类别");
//        $(prefix + "BusType").addClass("wide3");
//        $(prefix + "BusType").chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
//    }
    $(prefix + "Code").attr("readonly", "readonly");
    $(prefix + "VerifyDate").attr("readonly", "readonly");
}
function DisabledDiv() {
    $(prefix + "RdDate").attr("disabled", "disabled");
    $(prefix + "SVDate").attr("disabled", "disabled");
    $(prefix + "TVDate").attr("disabled", "disabled");
    $(prefix + "CVDate").attr("disabled", "disabled");
    $(prefix + "ALVDate").attr("disabled", "disabled");
    $(prefix + "MVDate").attr("disabled", "disabled");
    $(prefix + "WhId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "OutWhId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "InWhId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "ARVCode").attr("disabled", "disabled");
    $(prefix + "VenId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "DeptId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "OutDeptId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "InDeptId").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "Salesman").attr('disabled', true).trigger("liszt:updated");
    $(prefix + "ARVDate").attr("disabled", "disabled");
    $(prefix + "Memo").attr("disabled", "disabled");
    $(prefix + "BusType").attr('disabled', true).trigger("liszt:updated");
    //    $("#divForm").block({ message: null });
    //    $("#divGrid").block({ message: null }); 
}
function EnabledDiv() {
    $(prefix + "RdDate").removeAttr("disabled");
    $(prefix + "SVDate").removeAttr("disabled");
    $(prefix + "TVDate").removeAttr("disabled");
    $(prefix + "CVDate").removeAttr("disabled");
    $(prefix + "ALVDate").removeAttr("disabled");
    $(prefix + "MVDate").removeAttr("disabled");
    $(prefix + "WhId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "OutWhId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "InWhId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "ARVCode").removeAttr("disabled");
    $(prefix + "VenId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "DeptId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "OutDeptId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "InDeptId").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "Salesman").attr('disabled', false).trigger("liszt:updated");
    $(prefix + "ARVDate").removeAttr("disabled");
    $(prefix + "Memo").removeAttr("disabled");
    $(prefix + "BusType").attr('disabled', false).trigger("liszt:updated");
    //    $("#divForm").unblock(); 
    //    $("#divGrid").unblock();
}
function SetFormValue(jsonResult) {
    $(prefix + "Code").val(jsonResult.Code);
    if(jsonResult.RdDate)
        $(prefix + "RdDate").val($.format.date(new Date(parseInt(jsonResult.RdDate.substr(6))), "yyyy-MM-dd"));

    if (jsonResult.SVDate)
        $(prefix + "SVDate").val($.format.date(new Date(parseInt(jsonResult.SVDate.substr(6))), "yyyy-MM-dd"));
    if (jsonResult.TVDate)
        $(prefix + "TVDate").val($.format.date(new Date(parseInt(jsonResult.TVDate.substr(6))), "yyyy-MM-dd"));
    if (jsonResult.CVDate)
        $(prefix + "CVDate").val($.format.date(new Date(parseInt(jsonResult.CVDate.substr(6))), "yyyy-MM-dd"));
    if (jsonResult.ALVDate)
        $(prefix + "ALVDate").val($.format.date(new Date(parseInt(jsonResult.ALVDate.substr(6))), "yyyy-MM-dd"));
    if (jsonResult.MVDate)
        $(prefix + "MVDate").val($.format.date(new Date(parseInt(jsonResult.MVDate.substr(6))), "yyyy-MM-dd"));
    if (jsonResult.WhId)
        $(prefix + "WhId").val(jsonResult.WhId);
    if (jsonResult.OutWhId)
        $(prefix + "OutWhId").val(jsonResult.OutWhId);
    if (jsonResult.InWhId)     
        $(prefix + "InWhId").val(jsonResult.InWhId);
    if(jsonResult.ARVCode)
        $(prefix + "ARVCode").val(jsonResult.ARVCode);


    if (jsonResult.VenId) {
        $(prefix + "VenId").val(jsonResult.VenId);
    }
    else
        $(prefix + "VenId").val("");

    if(jsonResult.DeptId)
        $(prefix + "DeptId").val(jsonResult.DeptId);
    if (jsonResult.OutDeptId)
        $(prefix + "OutDeptId").val(jsonResult.OutDeptId);
    if (jsonResult.InDeptId)
        $(prefix + "InDeptId").val(jsonResult.OutDeptId);

    $(prefix + "Salesman").val(jsonResult.Salesman);
    if($(prefix + "ARVDate").length>0)
        $(prefix + "ARVDate").val("");
    if (jsonResult.ARVDate)
        $(prefix + "ARVDate").val($.format.date(new Date(parseInt(jsonResult.ARVDate.substr(6))), "yyyy-MM-dd"));
    $(prefix + "VerifyDate").val("");
    if (jsonResult.VerifyDate) {
        $(prefix + "VerifyDate").val($.format.date(new Date(parseInt(jsonResult.VerifyDate.substr(6))), "yyyy-MM-dd"));
    }
    $(prefix + "Memo").val(jsonResult.Memo);

    $(prefix + "Id").val(jsonResult.Id);

    $(prefix + "WhId").trigger("liszt:updated");
    if($(prefix + "OutWhId").length>0)
        $(prefix + "OutWhId").trigger("liszt:updated");
    if ($(prefix + "InWhId").length > 0)
        $(prefix + "InWhId").trigger("liszt:updated");
    if ($(prefix + "VenId").length > 0)
        $(prefix + "VenId").trigger("liszt:updated");

    $(prefix + "DeptId").trigger("liszt:updated");
    $(prefix + "OutDeptId").trigger("liszt:updated");
    $(prefix + "InDeptId").trigger("liszt:updated");
    $(prefix + "Salesman").trigger("liszt:updated");
    $(prefix + "IsModify").val(jsonResult.IsModify);
    $(prefix + "IsVerify").val(jsonResult.IsVerify);
    $(prefix + "MakeTime").val($.format.date(new Date(parseInt(jsonResult.MakeTime.substr(6))), "yyyy-MM-dd hh:mm:ss"));
    if (jsonResult.BusType && $(prefix + "BusType").length > 0) {
        $(prefix + "BusType").val(jsonResult.BusType);
        $(prefix + "BusType").trigger("liszt:updated");
    }
}

function FormatNumber(num) {
    return parseFloat(num);
}
function UnFormatNumber(num) {
    return num;
}
