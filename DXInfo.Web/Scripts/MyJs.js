function Verify() {
    var myDate = new Date();
    var currentDate = new Date();
    myDate.setFullYear(2011, 5, 20);
    if (typeof (kk) == "undefined") {
        if (currentDate > myDate) {
            alert("请安装ekey插件");
            return false;
        }
        return confirm("请安装ekey插件");
    }
    if (kk.object == null) {
        if (currentDate > myDate) {
            alert("请安装ekey插件");
            return false;
        }
        return confirm("请安装ekey插件");
    }
    if (kk.Verify()) {
        return true;
    }
    else {
        if (currentDate > myDate) {
            alert("请插ekey");
            return false;
        }
        return confirm("请插ekey");
    }
    return confirm("请安装ekey插件");
}
function ReadCard() {
    if (typeof (cc) == "undefined") {
        return "";
    }
    if (cc.object == null) {
        return "";
    }
    return cc.ReadCard();
}
function PutCard(CardNo) {
    if (typeof (cc) == "undefined") {
        return "";
    }
    if (cc.object == null) {
        return "";
    }
    return cc.PutCard(CardNo);
}
function EnterKeyPress(e) {
    var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
    if (key == 13) {
    }
    if (key == 13) {
        var inputs = $(this).parents("form").eq(0).find(":input");
        var idx = inputs.index(this);
        if (idx == inputs.length - 2) {
            inputs[0].select()
        } else {
            if ($(inputs[idx + 1]).attr("disabled") == "disabled") {
                for (var i = idx + 2; i < inputs.length - 1; i++) {
                    if ($(inputs[i]).attr("disabled") != "disabled") {
                        inputs[i].focus();
                        return false;
                    }
                }
            }
            inputs[idx + 1].focus(); 
        }
        return false;
    }
}
function validateCard(value, url) {
    if (value.length != 5) {
        return [false, "请输入5位卡号"];
    }
    else {
        var myurl = url;
        var json = (function () {
            var json = null;
            $.ajax({
                "type": "GET",
                "async": false,
                "global": false,
                "url": myurl,
                "data": "cardNo=" + value,
                "dataType": "json",
                "success": function (data) {
                    json = data;
                }
            });
            return json;
        })();
        if (json.IsCard) return [false, "卡号已存在"];
        if (!PutCard(value)) return [false, "发卡失败"];
        return [true, ""];
    }
}
function addIS() {
    jQuery("select").AddIncSearch({
        maxListSize: 20,
        maxMultiMatch: 50,
        warnNoMatch: "没找到",
        warnMultiMatch: "前{0}个匹配项"
    });
}
function removeIS() {
    jQuery("select").RemoveIncSearch();
}
function GetKey() {
    var un = $("#UserName").val();
    var url = urls.IsNoActiveXCheckUrl;
    var IsNoActiveXCheck = false;
    $.ajax({
        url: url + "?userName=" + un,
        type: "POST",
        async: false,
        success: function (json) {
            if (json.success == true) {
                IsNoActiveXCheck = true;
            }
        }
    });
    if (IsNoActiveXCheck) {
        return true;
    }
    else {
        var iskey = Verify();
        if (iskey) {
            var hId = GetHId();
            var cardNo = GetKeyNo();
            $("#HardwareID").val(hId);
            $("#CardNo").val(cardNo);
        }
        return iskey;
    }
}
function GetHId() {
    if (typeof (kk) == "undefined") {
        return "";
    }
    if (kk.object == null) {
        return "";
    }
    return kk.GetHardwareID();
}
function GetKeyNo() {
    if (typeof (kk) == "undefined") {
        return "";
    }
    if (kk.object == null) {
        return "";
    }
    var kkk = kk.GetKeyNo();
    return kkk.substring(0, 9);
}
function GetMacAddress() {
    //clientMac();
    return true;
}
function clientMac() {
    var locator = new ActiveXObject("WbemScripting.SWbemLocator");
    var service = locator.ConnectServer(".");
    var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
    var e = new Enumerator(properties);
    for (; !e.atEnd(); e.moveNext()) {
        var p = e.item();
        var mystring = new String(p.Caption);
        var myregExp = 'PCI';
        var answerIdx = mystring.search(myregExp)
        if (answerIdx != -1 && p.MACAddress != null) {
            $("#MacAddress").val(p.MACAddress);
        }
        else {
            var mystring = new String(p.Caption);
            var myregExp = 'NIC';
            var answerIdx = mystring.search(myregExp)
            if (answerIdx != -1 && p.MACAddress != null) {
                $("#MacAddress").val(p.MACAddress);
            }
        }
    }
}
function check() {
    var isAMSApp = urls.IsAMSApp;    
    if (isAMSApp == "true") {
        return GetMacAddress();
    }
    else {
        return GetKey();
    }
    return false;
}
function getSelectedText() {
    var text = '';
    if (window.getSelection) {
        text = window.getSelection();
    } else if (document.getSelection) {
        text = document.getSelection();
    } else if (document.selection) {
        text = document.selection.createRange().text;
    }
    return typeof (text) === 'string' ? text : text.toString();
}
function createContexMenuFromNavigatorButtons(grid, pager) {
        var buttons = $('table.navtable .ui-pg-button', pager),
                        myBinding = {},
                        menuId = 'menu_' + grid[0].id,
                        menuDiv = $('<div>').attr('id', menuId).hide(),
                        menuUl = $('<ul>');

        menuUl.appendTo(menuDiv);
        buttons.each(function () {
            var $div = $(this).children('div.ui-pg-div:first'), $spanIcon, text, $td, id, $li, gridId = grid[0].id;

            if ($div.length === 1) {
                text = $div.text();
                $td = $div.parent();
                if (text === '') {
                    text = $td.attr('title');
                }
                if (this.id !== '' && text !== '') {
                    id = 'menuitem_' + this.id;
                    if (id.length > gridId.length + 2) {
                        id = id.substr(0, id.length - gridId.length - 1);
                    }
                } else {
                    id = $.jgrid.randId();
                }
                $li = $('<li>').attr('id', id);
                $spanIcon = $div.children('span.ui-icon');
                $li.append($spanIcon.clone().css("float", "left"));
                $li.append($('<span>').css({
                    'font-size': '11px',
                    'font-family': 'Verdana',
                    'margin-left': '0.5em'
                }).text(text));
                menuUl.append($li);
                myBinding[id] = (function ($button) {
                    return function () { $button.click(); };
                } ($div));
            }
        });
        menuDiv.appendTo('.ui-layout-center');
        grid.contextMenu(menuId, {
            bindings: myBinding,
            onContextMenu: function (e) {
                var rowId = $(e.target).closest("tr.jqgrow").attr("id"), p = grid[0].p, i, lastSelId;
                if (rowId && getSelectedText() === '') {
                    i = $.inArray(rowId, p.selarrrow);
                    if (p.selrow !== rowId && i < 0) {
                        grid.jqGrid('setSelection', rowId);
                    } else if (p.multiselect) {
                        lastSelId = p.selarrrow[p.selarrrow.length - 1];
                        if (i !== p.selarrrow.length - 1) {
                            p.selarrrow[p.selarrrow.length - 1] = rowId;
                            p.selarrrow[i] = lastSelId;
                            p.selrow = rowId;
                        }
                    }
                    return true;
                } else {
                    return false; 
                }
            },
            menuStyle: {
                backgroundColor: '#fcfdfd',
                border: '1px solid #a6c9e2',
                maxWidth: '600px',
                width: '100%'
            },
            itemHoverStyle: {
                border: '1px solid #79b7e7',
                color: '#1d5987',
                backgroundColor: '#d0e5f5'
            }
        });
    }
function submitToDiv(submitId) {
        $('#' + submitId + '').submit(function () { 
            $.ajax({ 
                data: $(this).serialize(), 
                type: $(this).attr('method'), 
                url: $(this).attr('action'), 
                success: function (response) { 
                    $('.ui-layout-center').html(response);
                    resizeForm();
                },
                beforeSend: function () {
                    myBlockUI();
                },
                complete: function () {
                    myUnBlockUI();
                },
            });
            return false; 
        });
    }
function addToDiv(linkurl) {
        urlToDiv(linkurl.href);
    }
function urlToDiv(url) {
        if(url.indexOf("mht")>0){
             window.open(url, '_blank ');
            return false;
        }
        var opts = {
            lines: 13, 
            length: 0, 
            width: 10, 
            radius: 20,
            corners: 1,
            rotate: 0, 
            direction: 1, 
            color: '#000',
            speed: 0.5, 
            trail: 10, 
            shadow: false,
            hwaccel: false, 
            className: 'spinner', 
            zIndex: 2e9, 
            top: 'auto', 
            left: 'auto'
        };
        var target = $(".ui-layout-center");
        var spinner = new Spinner(opts).spin($("body")[0]);
        target.load(url, function (response, status, xhr) {
            spinner.stop();
            resizeGrid();
            //resizeForm();
            if (status == "error") {
                alert(xhr.status + " " + xhr.statusText);
            }
        });
    }   
function CheckEkey(){
    var myurl = urls.IsAMSApp;
    if (myurl == false) {
        var isNoActiveXCheck = urls.IsNoActiveXCheck;
        if (isNoActiveXCheck == true) return true;
        if (!Verify()) {
            var url = urls.LogOffUrl;
            window.location.href = url;
        }
    }
}
function SetTextCss(ctrlId) {
    $("#"+ctrlId)
    .button()
    .css({
        'font': 'inherit',
        'color': 'inherit',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text',
        'background': 'none',
        'outline': 'none',
        'cursor': 'text',
    });
}
function FormatNumber(num) {
    if (num!= undefined && num.length > 0) {
        return parseFloat(num);
    } 
    return '';
}
function UnFormatNumber(num) {
    return num;
}
function formatImage(cellValue, options, rowObject) {
    var imageHtml = "<img style='width:100px;' src='/ckfinder/userfiles/images/" + cellValue + "' originalValue='" + cellValue + "' />";
    return imageHtml;
}
function unformatImage(cellValue, options, cellObject) {
    return $(cellObject.html()).attr("originalValue");
} 
function FormatRanking(cellValue, options, rowObject) {         
    var rowID = options.rowId;
    //var rateit = "<input type='hidden' id='backing-"+rowID+"' value='"+cellValue+"'><div id='rateit-"+rowID+"'/>";
    var rateit = '<div id="rateit-'+rowID+'" data-score="'+cellValue+'"></div>';
    return rateit;
}
function UnFormatRanking(cellValue, options, cellObject) {         
    //var ret = $(cellObject).raty('score');//$(cellObject).find("input").val();
    //console.log(options);
    //console.log(cellObject);
    //return ret;
    var ret = $('#rateit-'+options.rowId).raty('score');
    return ret;
}     
function createStarsEditElement(cellValue, editOptions) {
    return $("<span></span>").raty({ 
    score: cellValue ,
    cancel        : true,
    });//.rateit({step:1,value:cellValue});
}
function getStarsElementValue(elem, two, value) {
    if (two == "get") {
        var ret = elem.raty('score');//rateit("value");
        if(ret == undefined){
            ret = 0;
        }
        return ret;
    }
    if(two=="set"){
        elem.raty({
        score         :value,
        cancel        : true,
        });
    }
}   
function myBlockUI() {
        $.blockUI({
            message: '<h1>请等待...</h1>',
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
    }
function myUnBlockUI() {
        $.unblockUI({
            onUnblock: function () {
                $.blockUI({ message: '<h1>完成</h1>', css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
                });
                setTimeout($.unblockUI, 100);
            }
        });
    }
function IsExist(ctrlId){
    if($("#"+ctrlId).length > 0){
        return true;
    }
    return false;
}
function DisabledCtrl(ctrlId){
    if(IsExist(ctrlId)){
        $("#"+ctrlId).prop('disabled', true);
    }
}
function DisabledCtrls(ctrls) {        
    $.each(ctrls, function (key, value) {
        DisabledCtrl(value.name);
    });
}
function EnabledCtrl(ctrlId){
    if(IsExist(ctrlId)){
        $("#"+ctrlId).prop('disabled', false);
    }
}
function EnabledCtrls(ctrls) {
    $.each(ctrls, function (key, value) {
        EnabledCtrl(value.name);
    });
}
function buildSelect(data) {
    var s = '<select><option></option>';
    $.each(eval(data), function (key, value) {
        s += '<option value="' + value.Id + '">' + value.Name + '</option>';
    });
    return s + "</select>";
}
function s4() {
  return Math.floor((1 + Math.random()) * 0x10000)
             .toString(16)
             .substring(1);
};
function guid() {
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
         s4() + '-' + s4() + s4() + s4();
}
function getColumnIndex(columnName) {
    var cm = $(this).jqGrid('getGridParam', 'colModel'), i, l = cm.length;
    for (i = 0; i < l; i++) {
        if ((cm[i].index || cm[i].name) === columnName) {
            return i; 
        }
    }
    return -1;
}
function onclickSubmitLocal(options, postdata, frmoper) {    
    var $this = $(this), grid_p = this.p,
        idname = grid_p.prmNames.id,
        grid_id = this.id,
        id_in_postdata = grid_id + "_id",
        rowid = postdata[id_in_postdata],
        addMode = rowid === "_empty",
        oldValueOfSortColumn,
        new_id,
        tr_par_id,
        colModel = grid_p.colModel,
        cmName,
        iCol,
        cm;
    if(grid_p.datatype!=="local"){
        return {};
    }
    if (addMode) {
        new_id = guid();
        while ($("#" + new_id).length !== 0) {
            new_id = guid();
        }
        postdata[idname] = String(new_id);
    } else if (typeof postdata[idname] === "undefined") {
        postdata[idname] = rowid;
    }
    delete postdata[id_in_postdata];
        
    if (grid_p.treeGrid === true) {
        if (addMode) {
            tr_par_id = grid_p.treeGridModel === 'adjacency' ? grid_p.treeReader.parent_id_field : 'parent_id';
            postdata[tr_par_id] = grid_p.selrow;
        }
        
        $.each(grid_p.treeReader, function (i) {
            if (postdata.hasOwnProperty(this)) {
                delete postdata[this];
            }
        });
    }
        
    if (grid_p.autoencode) {
        $.each(postdata, function (n, v) {
            postdata[n] = $.jgrid.htmlDecode(v); 
        });
    }
        
    oldValueOfSortColumn = grid_p.sortname === "" ? undefined : grid.jqGrid('getCell', rowid, grid_p.sortname);
        
    if (grid_p.treeGrid === true) {
        if (addMode) {
            $this.jqGrid("addChildNode", new_id, grid_p.selrow, postdata);
        } else {
            $this.jqGrid("setTreeRow", rowid, postdata);
        }
    } else {
        if (addMode) {              
            for (cmName in postdata) {                    
                if (postdata.hasOwnProperty(cmName)) {
                    iCol = getColumnIndex.call(this, cmName);                        
                    if (iCol >= 0) {
                        cm = colModel[iCol];
                        if (cm && cm.formatter === "date") {
                            postdata[cmName] = $.unformat.date.call(this, postdata[cmName], cm);
                        }
                    }
                }
            }
                
            $this.jqGrid("addRowData", new_id, postdata, options.addedrow);
        } else {
            $this.jqGrid("setRowData", rowid, postdata);
        }
    }
        
    if ((addMode && options.closeAfterAdd) || (!addMode && options.closeAfterEdit)) {
        $.jgrid.hideModal("#editmod" + grid_id, {
            gb: "#gbox_" + grid_id,
            jqm: options.jqModal,
            onClose: options.onClose
        });
    }
        
    if (postdata[grid_p.sortname] !== oldValueOfSortColumn) {
        setTimeout(function () {
            $this.trigger("reloadGrid", [{current: true}]);
        }, 100);
    }
        
    options.processing = true;
    return {};
}
function InitCtrl(ctrls) {
    if(ctrls){
        $.each(ctrls, function (key, value) {
        if (IsExist(value.name)) {
            switch (value.type) {
                case "text":
                    SetTextCss(value.name);
                    break;
                case "select":
                    $("#" + value.name).customSelect();
                    break;
                case "date":
                    SetTextCss(value.name);
                    $("#" + value.name).datepicker();
                    break;
                case "hidden-select":
                    if($("#" + value.name).is("select")){
                        $("#" + value.name).customSelect();
                    }
                    break;
               case "checkbox":
                    $("#" + value.name).iCheck({
                        checkboxClass: 'icheckbox_minimal-blue',
                        radioClass: 'iradio_minimal-blue'
                      });
                    break;
            }
        }
    });
    }
}
function FormToInit(ctrls) {
    $.each(ctrls, function (key, value) {
        if (IsExist(value.name) && value.type!="hidden"&& value.type!="datetime") {
            $("#" + value.name).val("");
            if (value.type == "select") {
                $("#" + value.name).trigger('update');
            }
        }
    });
}
function judgeState(ctrlId){
    if(IsExist(ctrlId)){
        return $("#"+ctrlId).val().toLowerCase()=="true";        
    }
    return false;
}
function isVerify(){
    return judgeState("IsVerify");
}
function isModify(){
    return judgeState("IsModify");
}
function isLocator(){
    var islr = judgeState("IsLocator");
    var vt = $("#VouchType").val();
    if(vt){
        switch(vt){
            case "010":
                islr = false;
            break;
            case "012":
                islr=false;
            break;
        }
    }
    return islr;
}
function isBatch(){
    var isbh = judgeState("IsBatch");
    var vt = $("#VouchType").val();
    if(vt){
        switch(vt){
            case "012":
                isbh=false;
            break;
        }
    }
    return isbh;
}
function isShelfLife(){
    return judgeState("IsShelfLife");
}
function SetFormValue(data,ctrls) {
    for (var name in data) {
        var temps = $.grep(ctrls, function (ctrl, i) { return ctrl.name == name; });
        if (temps.length == 1) {
            if(data[name] === null){
                if (IsExist(name)) {
                    $("#" + name).val("");
                    if(temps[0].type==="select"){
                        $("#" + name).trigger('update');
                    }
                }
            }else{
                switch (temps[0].type) {
                    case "text":
                    case "hidden":
                        if (IsExist(name)) {
                            $("#" + name).val(data[name]);
                        }
                        break;
                    case "date":
                        if (IsExist(name)) {
                            $("#" + name).val(new Date(parseInt(data[name].substr(6))).format("isoDate"));
                        }
                        break;
                    case "select":
                        if (IsExist(name)) {
                            $("#" + name).val(data[name]);
                            $("#" + name).trigger('update');
                        }
                        break;
                    case "datetime":
                        if (IsExist(name)) {
                            //console.info(data[name]);
                            //console.info(data[name].substr(6));
                            $("#" + name).val(new Date(parseInt(data[name].substr(6))).format("fullDateTime"));
                        }
                        break;
                    case "hidden-select":
                        if (IsExist(name)) {
                            $("#" + name).val(data[name]);
                            $("#" + name).trigger('update');
                        }
                        break;
                }
            }
        }
    }
}
function resetValidation() {
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');

    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');

    $('.validation-summary-errors').addClass('validation-summary-valid');
    $('.validation-summary-errors').removeClass('validation-summary-errors');
}
function MyAjax(frm,myUrl,ctrls){
    var data = frm.mySerializeArray();
    MyAjaxBase(myUrl,data,ctrls);
}
function MyAjaxBase(myUrl,data,ctrls) {
    $.ajax({
        url: myUrl,
        type: "POST",
        data: data,
        dataType: "json",
        success: function (jsonResult) {
            if (jsonResult) {
                if (jsonResult.Sucess == false) {
                    alert(jsonResult.Message);
                }
                else {
                    if(jsonResult.Sucess == true) {
                        FormToInit(ctrls);
                        setButtonState(0,ctrls);
                    }else{
                        SetFormValue(jsonResult,ctrls);
                        resetValidation();                    
                        if (jsonResult.IsVerify) {
                            grid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid', [{ page: 1}]);                            
                            setButtonState(3,ctrls);
                        }
                        else {
                            if(jsonResult.IsModify){
                                grid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid', [{ page: 1}]);
                                setButtonState(2,ctrls);
                            }else{
                                grid.jqGrid('setGridParam', { datatype: 'local' }).trigger('reloadGrid', [{ page: 1}]);
                                setButtonState(1,ctrls);                            
                            }                           
                        }
                    }
                }
            }
        },
        beforeSend: function () {
            myBlockUI();
        },
        complete: function () {
            myUnBlockUI();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ":" + errorThrown);
        }
    });
}
function setButton(urls,frm,grid,vouchType,ctrls) {
        var btns = [{ name: "add", icon: "ui-icon-plus" },
                    { name: "modify", icon: "ui-icon-pencil" },
                    { name: "delete", icon: "ui-icon-trash" },
                    { name: "verify", icon: "ui-icon-check" },
                    { name: "unverify", icon: "ui-icon-close" },
                    { name: "print", icon: "ui-icon-print" },
                    { name: "search", icon: "ui-icon-search" },
                    { name: "start", icon: "ui-icon-seek-start" },
                    { name: "prev1", icon: "ui-icon-seek-prev" },
                    { name: "next1", icon: "ui-icon-seek-next" },
                    { name: "end", icon: "ui-icon-seek-end" },
                    { name: "save", icon: "ui-icon-disk" },
                    { name: "cancel", icon: "ui-icon-close" }];
        $.each(btns, function (key, value) {
            $("#"+value.name).button({
                text: true,
                icons: {
                    primary: value.icon
                }
            });
        });
        $("#add").button().click(function () {
            var url = urls.addurl;
            MyAjax(frm,url,ctrls);
        });
        $("#modify").button().click(function () {
            if (isVerify()) {
                alert("已审核不能修改");
            }
            else {
                setButtonState(1,ctrls);
            }
        });
        $("#save").button().click(function () {
            if (frm.valid()) {
                if (!isModify()) {
                    var url = urls.saveaddurl;
                    var rowData = grid.jqGrid("getRowData");                    
                    var formData = frm.serializeObject();
                    formData["lVouchs"] = rowData;  
                    grid.jqGrid("setGridParam", {  datatype: 'local',url:urls.Vouchs_RequestData});
                    MyAjaxBase(url,{vouchsGridModel:JSON.stringify(formData)},ctrls);
                }
                else {
                    var url = urls.savemodifyurl;
                    MyAjax(frm,url,ctrls);
                }
            }
        });
        $("#cancel").button().click(function () {
            resetValidation();
            if (!isModify()) {
                FormToInit(ctrls);                
                grid.jqGrid('setGridParam', { datatype: 'local' }).trigger('reloadGrid', [{ page: 1}]);
                setButtonState(0,ctrls);
            }
            else {
                if (isVerify()) {
                    setButtonState(3,ctrls);
                }
                else {
                    setButtonState(2,ctrls);
                }
            }
        });
        $("#delete").button().click(function () {
            if (isVerify()) {
                alert("已审核不能删除");
            }
            else {
                if (confirm("是否确认删除？")) {
                    var url = urls.deleteurl;
                    MyAjax(frm,url,ctrls); 
                }
            }
        });
        $("#verify").button().click(function () {
            if (frm.valid()) {
                var url = urls.verifyurl;
                MyAjax(frm,url,ctrls); 
            }
        });
        $("#unverify").button().click(function () {
            var url = urls.unverifyurl;
            MyAjax(frm,url,ctrls); 
        });
        $("#start").button().click(function () {
            var url = urls.starturl;
            MyAjax(frm,url,ctrls); 
        });
        $("#prev1").button().click(function () {
            var url = urls.prevurl;
            MyAjax(frm,url,ctrls); 
        });
        $("#next1").button().click(function () {
            var url = urls.nexturl;
            MyAjax(frm,url,ctrls); 
        });
        $("#end").button().click(function () {
            var url = urls.endurl;
            MyAjax(frm,url,ctrls); 
        });
        $("#print").button().click(function () {
            var url = urls.printurl;
            window.open(url + '?Id=' + $("#Id").val() + '&vouchType=' + vouchType, '_blank');
            return false;
        });
        $("#search").button().click(function () {
            var url = urls.searchurl;
            urlToDiv(url + "?VouchType=" + vouchType);
        });
        if (IsExist("AddReceiver")) {
            $("#AddReceiver").button();
            $("#AddReceiver").button().click(function () {
                AddReceiver();
                return false;
            });
        }
    }
function setButtonState(operFlag,ctrls) {
        switch (operFlag) {
            case 0: 
                $("#save").button("disable");
                $("#cancel").button("disable");
                $("#add").button("enable");
                $("#modify").button("disable");
                $("#delete").button("disable");
                $("#verify").button("disable");
                $("#unverify").button("disable");
                $("#print").button("disable");
                $(".ui-pg-button").addClass('ui-state-disabled');
                DisabledCtrls(ctrls);              
                break;
            case 1: 
                $("#save").button("enable");
                $("#cancel").button("enable");
                $("#add").button("disable");
                $("#modify").button("disable");
                $("#delete").button("disable");
                $("#verify").button("disable");
                $("#unverify").button("disable");
                $("#print").button("disable");
                $(".ui-pg-button").removeClass('ui-state-disabled');
                EnabledCtrls(ctrls)
                break;
            case 2: 
                $("#save").button("disable");
                $("#cancel").button("disable");
                $("#add").button("enable");
                $("#modify").button("enable");
                $("#delete").button("enable");
                $("#verify").button("enable");
                $("#unverify").button("disable");
                $("#print").button("enable");
                $(".ui-pg-button").addClass('ui-state-disabled');
                DisabledCtrls(ctrls);
                break;
            case 3: 
                $("#save").button("disable");
                $("#cancel").button("disable");
                $("#add").button("enable");
                $("#modify").button("disable");
                $("#delete").button("disable");
                $("#verify").button("disable");
                $("#unverify").button("enable");
                $("#print").button("enable");
                $(".ui-pg-button").addClass('ui-state-disabled');
                DisabledCtrls(ctrls);
                break;
        }
    }
function resizeGrid(grid) {    
//    var $gridView = grid.closest("div.ui-jqgrid-view"),
//        $gbox = grid.closest("div.ui-jqgrid");
//    $(">.ui-jqgrid-pager>.ui-pager-control>.ui-pg-table>tbody>tr>td", $gbox[0]).each(function () {
//                            this.style.width = "";
//                        });
//     $(">.ui-jqgrid-toppager>.ui-pager-control>.ui-pg-table>tbody>tr>td", $gridView[0]).each(function () {
//                        this.style.width = "";
//                    });
} 
function resizeForm() {    
    var $frm = $(".form-overflow");
    if($frm.length>0){
        var $gridParent = $(".form-overflow").parent(),
        heightFudge = 0;
        var $brothers = $gridParent.children(":visible:not(.form-overflow)");
        $.each($brothers,function(key,value){
            heightFudge += $(this).outerHeight(true);
        });
        $frm.width($gridParent.width());
        $frm.height($gridParent.height() - heightFudge);
    }
}  
function resizePaneAccordions(x, ui) {
        var $P = ui.jquery ? ui : $(ui.newPanel || ui.panel);
        $P.find(".ui-accordion:visible").each(function () {
            var $E = $(this);
            if ($E.data("accordion"))
                $E.accordion("resize");
            if ($E.data("ui-accordion"))	
                $E.accordion("refresh");            
        });
}