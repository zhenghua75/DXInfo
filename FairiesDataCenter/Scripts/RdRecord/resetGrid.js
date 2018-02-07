function resetGrid() {
    $(gridId).jqGrid("setGridParam", { search: true }).trigger("reloadGrid", [{ page: 1}]);
}

function populateAdd() {
    populate();
}
function populate(locator, batch,inLocator,subInvId) {
    $(invId).attr("data-placeholder", "选择存货");
    $(invId).css("width", "300px");
    $(invId).chosen({ allow_single_deselect: true, no_results_text: "没有找到", search_contains: true });
    if ($(prefix + "WhId").val() || $(prefix + "OutWhId").val() || $(prefix + "InWhId").val()) {
        if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
        ||$("#vouchType_Code").val() == "003"
        || $("#vouchType_Code").val() == "006")) {
            if ($(prefix + "OutWhId").val()) {
                updateInvByWh($(prefix + "OutWhId").val(), subInvId);
            }
            else {
                updateInvByWh($(prefix + "WhId").val(), subInvId);
            }
        }
    }
    else {
        $(invId).attr('disabled', true).trigger("liszt:updated");
    }

    $("#somediv").dialog({ autoOpen: false, modal: true, width: 1200, height: 620, beforeClose: function (event, ui) { updateInvs(); } });
    $("#aInvId").click(function () {
        var url = $("#addInventoryUrl").data('url');
        var $dialog = $("#somediv").html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>')
        $("#somediv").dialog('open');
    });
    $("#STUnitName").attr("disabled", "disabled");
    $("#Specs").attr("disabled", "disabled");
    $("#AvaNum").attr("disabled", "disabled");

    $(invId).bind("change", function (e) {
        updateInvCallBack($(invId).val());
        if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
        || $("#vouchType_Code").val() == "006" || $("#vouchType_Code").val() == "012")) {
            if ($("#vouchType_Code").val() == "009") {
                updateBatchCallBack($(prefix + "OutWhId").val(), $(invId).val(), batch);
                updateLocatorCallBack2($(prefix + "OutWhId").val(), $(invId).val(), batch, locator, "Locator");
            }
            else {
                updateBatchCallBack($(prefix + "WhId").val(), $(invId).val(), batch);
                updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), batch, locator, "Locator");
            }
        }
    });
    $("#Price").bind("change", function (e) { updateAmount(); updateAddInAmount(); updateAddOutAmount(); });
    $("#Num").bind("change", function (e) { updateAmount(); updateAddInAmount(); updateAddOutAmount(); });
    $("#Batch").bind("change", function (e) {
        var isLocator = $("#IsLocator").val();
        if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
        || $("#vouchType_Code").val() == "006" || $("#vouchType_Code").val() == "003")) {
            if ($("#vouchType_Code").val() == "009") {
                if (isLocator == "True") {
                    updateLocatorCallBack2($(prefix + "OutWhId").val(), $(invId).val(), $("#Batch").val(), locator, "Locator")
                }
                else {
                    updateAvaNumCallBack($(prefix + "OutWhId").val(), "", $(invId).val(), $("#Batch").val())
                }
            }
            else if ($("#vouchType_Code").val() == "011") {
                if (isLocator == "True") {
                    updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), $("#Batch").val(), locator, "OutLocator")
                }
                else {
                    updateAvaNumCallBack($(prefix + "WhId").val(), "", $(invId).val(), $("#Batch").val())
                }
            }
            else {
                if (isLocator == "True") {
                    updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), $("#Batch").val(), locator, "Locator")
                }
                else {
                    updateAvaNumCallBack($(prefix + "WhId").val(), "", $(invId).val(), $("#Batch").val())
                }
            }
        }
    });
    $("#Locator").bind("change", function (e) {
        if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
        || $("#vouchType_Code").val() == "006")) {
            if ($("#vouchType_Code").val() == "009") {
                updateAvaNumCallBack($(prefix + "OutWhId").val(), $("#Locator").val(), $(invId).val(), $("#Batch").val())
            }
            else{
                updateAvaNumCallBack($(prefix + "WhId").val(), $("#Locator").val(), $(invId).val(), $("#Batch").val())
            }
        }
    });
    $("#OutLocator").bind("change", function (e) {
        updateAvaNumCallBack($(prefix + "WhId").val(), $("#OutLocator").val(), $(invId).val(), $("#Batch").val())
    });
    if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
    || $("#vouchType_Code").val() == "006")) {
        if ($("#vouchType_Code").val() == "009") {
            updateBatchCallBack($(prefix + "OutWhId").val(), $(invId).val(), batch);
        } else {
            updateBatchCallBack($(prefix + "WhId").val(), $(invId).val(), batch);
        }
    }
    if ($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007" || $("#vouchType_Code").val() == "006"
    || $("#vouchType_Code").val() == "003") {
        updateLocatorCallBack($(prefix + "WhId").val(), locator, "Locator");
    }

    if (!($("#vouchType_Code").val() == "001" || $("#vouchType_Code").val() == "007"
    || $("#vouchType_Code").val() == "006" || $("#vouchType_Code").val() == "003")) {
        if ($("#vouchType_Code").val() == "009") {
            updateLocatorCallBack2($(prefix + "OutWhId").val(), $(invId).val(), batch, locator, "Locator");
            updateAvaNumCallBack($(prefix + "OutWhId").val(), locator, $(invId).val(), batch);
        }
        else if ($("#vouchType_Code").val() == "011") {
            updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), batch, locator, "OutLocator");
            updateAvaNumCallBack($(prefix + "WhId").val(), locator, $(invId).val(), batch);
        }
        else {
            updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), batch, locator, "Locator");
            updateAvaNumCallBack($(prefix + "WhId").val(), locator, $(invId).val(), batch);
        }
    }
    if ($("#vouchType_Code").val() == "011") {
        //updateLocatorCallBack($(prefix + "WhId").val(), locator, "OutLocator");
        updateLocatorCallBack($(prefix + "WhId").val(), inLocator, "InLocator");
    }
//    updateLocatorCallBack($(prefix + "OutWhId").val(), locator, "OutLocator");
//    updateLocatorCallBack($(prefix + "InWhId").val(), locator, "InLocator");
}
function populateEdit() {
    var rowid = $(gridId).jqGrid("getGridParam", "selrow");
    if (rowid) {
        var ret = $(gridId).jqGrid("getRowData", rowid);
        populate(ret.Locator, ret.Batch,null,ret.InvId);

    }
}
function populateEdit2() {
    var rowid = $(gridId).jqGrid("getGridParam", "selrow");
    if (rowid) {
        var ret = $(gridId).jqGrid("getRowData", rowid);
        populate(ret.OutLocator, ret.Batch, ret.InLocator, ret.InvId);

    }
}
function updateLocatorCallBack(wh, locator,locatorId) {   
    if (wh) {
        var url = $("#getLocatorByWh").data('url');
        $.ajax({
            url: url + "?wh=" + wh,
            type: "GET",
            success: function (locatorJson) {
                var locators = eval(locatorJson);
                var locatorHtml = "";
                $(locators).each(function (i, option) {
                    locatorHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                });
                $("#" + locatorId).removeAttr("disabled").html(locatorHtml);
                if (typeof (locator) != "undefined") {
                    $("#" + locatorId).val(locator);
                }
            }
        });
    }
}
function updateLocatorCallBack2(wh,inv,batch, locator, locatorId) {
    if (wh&&inv&&batch) {
        var url = $("#getLocatorByWhBatchUrl").data('url');
        $.ajax({
            url: url + "?wh=" + wh+"&inv="+inv+"&batch="+batch,
            type: "GET",
            success: function (locatorJson) {
                var locators = eval(locatorJson);
                var locatorHtml = '<option value=""></option>';
                $(locators).each(function (i, option) {
                    locatorHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                });
                $("#" + locatorId).removeAttr("disabled").html(locatorHtml);
                if (typeof (locator) != "undefined") {
                    $("#" + locatorId).val(locator);
                }
            }
        });
    }
}
function updateInvByWh(wh,inv) {
    if (wh) {
        var url = $("#GetInvByWh").data('url');
        $.ajax({
            url: url + "?wh=" + wh,
            type: "GET",
            success: function (invJson) {
                var invs = eval(invJson);
                var invsHtml = '<option value=""></option>';
                $(invs).each(function (i, option) {
                    invsHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                });
                $(invId).html(invsHtml);
                if (inv) {
                    $(invId).val(inv);
                }
                $(invId).attr('disabled', false).trigger("liszt:updated");
            }
        });
    }
}
function updateAvaNumCallBack(wh,locator, inv, batch) {
    if (wh && inv && batch) {
        var url = $("#getAvaNumUrl").data('url');
        $.ajax({
            url: url + "?wh=" + wh +"&locator="+locator+ "&inv=" + inv + "&batch=" + batch,
            type: "GET",
            success: function (avaNum) {
                $("#AvaNum").val(avaNum);
            }
        });
    }
}
function updateInvCallBack(inv) {
    var url = $("#getInvInfo").data('url');
    $.ajax({
        url: url + "?inv=" + inv,
        type: "GET",
        success: function (invInfo) {
            //alert(invInfo.Sucess);
            if (invInfo.Sucess != undefined && !invInfo.Sucess) {
                alert(invInfo.Message);
                $("#Specs").val("");
                $("#STUnitName").val("");
                $("#ShelfLife").val("");
                $("#ShelfLifeType").val("");
                $("#Price").val("");
            }
            else {
                var invs = eval(invInfo);
                if (invs) {
                    $("#Specs").val(invs.Specs);
                    $("#STUnitName").val(invs.StockUnitName);
                    $("#ShelfLife").val(invs.ShelfLife);
                    $("#ShelfLifeType").val(invs.ShelfLifeType);
                    $("#Price").val(invs.SalePrice);
                }
            }
        }
    });
}
function updateBatchCallBack(wh, inv,batch) {
    if (wh && inv) {
        var url = $("#getBatchUrl").data('url');
        if (typeof (batch) == "undefined") {
            batch = "";
        }
        $.ajax({
            url: url + "?wh=" + wh+"&inv="+inv+"&batch="+batch,
            type: "GET",
            success: function (batchJson) {
                var batchs = eval(batchJson);
                var batchHtml = '<option value=""></option>';
                $(batchs).each(function (i, option) {
                    batchHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                });
                $("#Batch").removeAttr("disabled").html(batchHtml);
                if (typeof (batch) != "undefined") {
                    $("#Batch").val(batch);
                }
            }
        });
    }
}
function updateQuantity() {
    $("#Quantity").val(parseFloat($("#Num").val()) * parseFloat($("#ExchRate").val()));
}
function updateAddInQuantity() {
    $("#AddInQuantity").val(0);
    if(parseFloat($("#Quantity").val()) > parseFloat($("#CQuantity").val()))
        $("#AddInQuantity").val(parseFloat($("#Quantity").val()) - parseFloat($("#CQuantity").val()));
}
function updateAddOutQuantity() {
    $("#AddOutQuantity").val(0);
    if (parseFloat($("#Quantity").val()) < parseFloat($("#CQuantity").val()))
        $("#AddOutQuantity").val(parseFloat($("#CQuantity").val()) - parseFloat($("#Quantity").val()));
}
function updateNum() {
    $("#Num").val(parseFloat($("#Quantity").val()) / parseFloat($("#ExchRate").val()));
}
function updateAddInNum() {
    $("#AddInNum").val(0);
    if (parseFloat($("#Num").val()) > parseFloat($("#CNum").val()))
        $("#AddInNum").val(parseFloat($("#Num").val()) - parseFloat($("#CNum").val()));
}
function updateAddOutNum() {
    $("#AddOutNum").val(0);
    if (parseFloat($("#CNum").val()) > parseFloat($("#Num").val()))
        $("#AddOutNum").val(parseFloat($("#CNum").val()) - parseFloat($("#Num").val()));
}
function updateAmount() {
    if ($("#Num").val() && $("#Price").val()) {
        var num = parseFloat($("#Num").val());
        var price = parseFloat($("#Price").val());

        var amount = num * price;
        $("#Amount").val(amount); //.toFixed(9)
    }
    else {
        $("#Amount").val(0);
    }
}
function updateAddInAmount() {
    $("#AddInAmount").val(0);
    if (parseFloat($("#Amount").val()) > parseFloat($("#CAmount").val()))
        $("#AddInAmount").val(parseFloat($("#Amount").val()) - parseFloat($("#CAmount").val()));
}
function updateAddOutAmount() {
    $("#AddOutAmount").val(0);
    if (parseFloat($("#CAmount").val()) > parseFloat($("#Amount").val()))
        $("#AddOutAmount").val(parseFloat($("#CAmount").val()) - parseFloat($("#Amount").val()));
}
function beforeSubmit(postdata, formid) {
    if(mainId=="RdId")
        postdata.RdId = $(prefix + "Id").val();
    if (mainId == "SVId")
        postdata.SVId = $(prefix + "Id").val();
    if (mainId == "TVId")
        postdata.TVId = $(prefix + "Id").val();
    if (mainId == "CVId")
        postdata.CVId = $(prefix + "Id").val();
    if (mainId == "ALVId")
        postdata.ALVId = $(prefix + "Id").val();
    if (mainId == "MVId")
        postdata.MVId = $(prefix + "Id").val();
    var sdata={vouchType:$("#vouchType_Code").val()};
    $.extend(postdata, sdata);
    //postdata.vouchType=$("vouchType_Code").val();
    return [true, ''];
}
function beforeDelSubmit(id) {
    return [true, ''];
}
function beforeRefresh() {
    resetGrid();
}
function serializeGridData(postData) {
    var sdata = {
        searchField: mainId,
        searchString: $(prefix + "Id").val(),
        searchOper: "eq",
        vouchType: $("#vouchType_Code").val()  
    };
    var newPostData = $.extend(postData, sdata);
    return $.param(newPostData);
}
function updateInvs() {
        var url = $("#getInvsUrl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            success: function (invsJson) {
                var invsSel = $("#InvId");
                invsSel.empty();
                var invs = eval(invsJson);
                var invsHtml = "";
                $(invs).each(function (i, option) {
                    invsHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                });
                $("#InvId").html(invsHtml);
                $("#InvId").val("").trigger("liszt:updated");
            }
        });
}
function beforeShow() {
    //$("#pData #nData", formid).hide();
    //alert(formid);
}

function afterclickPgButtons(whichbutton, formid, rowid) {
    //alert(rowid);
    $(invId).trigger("liszt:updated");
    var ret = $(gridId).jqGrid("getRowData", rowid);
    var batch = ret.Batch;
    var locator = ret.Locator;
    var inLocator = ret.InLocator;
    var outLocator = ret.OutLocator;

    if (!($("#vouchType_Code").val() == "001" || 
    $("#vouchType_Code").val() == "007" ||
    $("#vouchType_Code").val() == "006" ||
    $("#vouchType_Code").val() == "012" || 
    $("#vouchType_Code").val() == "003")) {
        if ($("#vouchType_Code").val() == "009") {
            updateBatchCallBack($(prefix + "OutWhId").val(), $(invId).val(), batch);
            updateLocatorCallBack2($(prefix + "OutWhId").val(), $(invId).val(), batch, locator, "Locator");
        }
        else {
            updateBatchCallBack($(prefix + "WhId").val(), $(invId).val(), batch);
            if ($("#vouchType_Code").val() == "011") {
                updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), batch, outLocator, "OutLocator")
                updateLocatorCallBack($(prefix + "WhId").val(), inLocator, "InLocator");
            }
            else {
                updateLocatorCallBack2($(prefix + "WhId").val(), $(invId).val(), batch, locator, "Locator");
            }
        }
    }
}