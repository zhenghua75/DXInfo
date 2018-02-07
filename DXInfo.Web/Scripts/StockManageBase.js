    var ctrls = [{ name: "Code", type: "text" },
                 { name: "RdDate", type: "date" },
                 { name: "ARVCode", type: "text" },
                 { name: "VenId", type: "select" },
                 { name: "Salesman", type: "select" },
                 { name: "ARVDate", type: "date"},
                 { name: "VerifyDate", type: "date" },
                 { name: "Memo", type: "text" },
                 { name: "SVDate", type: "date" },
                 { name: "TVDate", type: "date" },
                 { name: "CVDate", type: "date"},
                 { name: "ALVDate", type: "date"},
                 { name: "MVDate", type: "date" },
                 { name: "WhId", type: "select" },
                 { name: "OutWhId", type: "select" },
                 { name: "InWhId", type: "select" },
                 { name: "DeptId", type: "select" },
                 { name: "OutDeptId", type: "select" },
                 { name: "InDeptId", type: "select"},
                 { name: "Id", type: "hidden" },
                 { name: "SVId", type: "hidden" },
                 { name: "TVId", type: "hidden" },
                 { name: "CVId", type: "hidden" },
                 { name: "ALVId", type: "hidden" },
                 { name: "MVId", type: "hidden" },
                 { name: "IsModify", type: "hidden" },
                 { name: "IsVerify", type: "hidden" },
                 { name: "BusType", type: "hidden-select" },
                 { name: "MakeTime", type: "datetime" } ];
    
    var whId = "";    
    function BatchIsDropDown(){
        var vt = $("#VouchType").val();
        var isDropDown = true;
        if(vt){
            switch(vt){
                case "001":
                    isDropDown = false;
                break;
                case "003":
                    isDropDown = false;
                break;
                case "006":
                    isDropDown=false;
                break;
                case "007":
                    isDropDown=false;
                break;
            }
        }
        return isDropDown;
    }
    function beforeInitData(frmid, frmoper) {
        //myOper = frmoper;
        var ret = false;
        if (IsExist("WhId")) {
            if ($("#WhId").val()) {
                whId = $("#WhId").val();        
                ret = true;
            }
            else {
                alert("请选择仓库");
            }
        }
        if (IsExist("OutWhId")) {
            if ($("#OutWhId").val()) {
                whId = $("#OutWhId").val();
                ret = true;
            }
            else {
                alert("请选择转出仓库");
            }
        }
        if(ret){
            var url = urls.getInvsUrl+"?InvType="+$("#InvType").val();
            $.ajax({
                url: url,
                type: "GET",
                async:false,
                success: function (data) {
                    var vl = ":"; 
                    $.each(data,function(key,value){
                        vl +=";"+value.Id+":"+value.Name;
                     }); 
                     grid.jqGrid('setColProp', "InvId", { editoptions: { value: vl} });                  
                }
            });  
            if(isLocator()){
                var url = urls.getLocatorByWh;
                $.ajax({
                    url: url + "?WhId=" + whId,
                    type: "GET",
                    async:false,
                    success: function (data) {  
                        var vl = ":"; 
                        $.each(data,function(key,value){
                            vl +=";"+value.Id+":"+value.Name;
                         }); 
                         grid.jqGrid('setColProp', "Locator", { editoptions: { value: vl} });              
                    }
                });
            }
        }
        return ret;
    }

    function SetInvInfo(jsonResult) {
        if (jsonResult.Sucess != undefined && !jsonResult.Sucess) {
            alert(invInfo.Message);
            $("#Specs").val("");
            $("#STUnitName").val("");
            $("#Price").val("");
            if (isShelfLife) {
                $("#ShelfLife").val("");
                $("#ShelfLifeType").val("");
            }
        }
        else {
            var invs = eval(jsonResult);
            if (invs) {
                $("#Specs").val(invs.Specs);
                $("#STUnitName").val(invs.StockUnitName);
                $("#Price").val(invs.SalePrice);
                if (isShelfLife) {
                    $("#ShelfLife").val(invs.ShelfLife);
                    $("#ShelfLifeType").val(invs.ShelfLifeType);
                }
            }
        }
    }
    function InvIdChange() {
        var url = urls.getInvInfo;
        $.ajax({
            url: url + "?inv=" + $(this).val(),
            type: "GET",
            success: function (jsonResult) {
                SetInvInfo(jsonResult);
            }
        });
        if (isBatch()&&BatchIsDropDown()) {
            BatchCallBack(whId, $(this).val());
        }        
    }
    function InvIdCallBack() {
        var url = urls.getInvsUrl;
        $.ajax({
            url: url+"?InvType="+$("#InvType").val(),
            type: "GET",
            async:false,
            success: function (data) {
                var invsSel = $("#InvId");
                invsSel.empty();
                var invsHtml = buildSelect(data);
                $("#InvId").html(invsHtml);
                $("#InvId").trigger("chosen:updated");
            }
        });
    }
    function InvIdColumnDataInit(el,opt) {
        $(el).chosen(
                {
                    allow_single_deselect: true,
                    no_results_text: '没有找到',
                    search_contains: true,
                    placeholder_text_single: '请选择......',
                    width:"300px",
                }).trigger("chosen:updated")
                .on("chosen:showing_dropdown", function () { 
                    $("#editmodVouchsGrid").css("overflow", "visible");
                    $("#FrmGrid_VouchsGrid").css("overflow", "visible");
                })
                .on("chosen:hiding_dropdown", function () {
                    $("#FrmGrid_VouchsGrid").css("overflow", "auto");
                    $("#editmodVouchsGrid").css("overflow", "hidden");
                 });
                 
        if(isBatch()&&BatchIsDropDown()&&$(el).val()){
           var url = urls.getBatchUrl;
            $.ajax({
                url: url + "?wh=" + whId + "&inv=" + $(el).val(),
                type: "GET",
                async:false,
                success: function (data) {
                    var vl = ":"; 
                    $.each(data,function(key,value){
                        vl +=";"+value.Id+":"+value.Name;
                     }); 
                     grid.jqGrid('setColProp', "Batch", { editoptions: { value: vl} });   
                }
            });      
       }
    }

    function LocatorChange() {
        if (BatchIsDropDown() && IsExist("AvaNum")) {
            AvaNumCallBack(whId, $("#Locator").val(), $("#InvId").val(), $("#Batch").val())
        }
    }
    function LocatorCallBack(wh, inv, batch) {
        if (wh && inv && batch) {
            var url = urls.getLocatorByWhBatchUrl;
            $.ajax({
                url: url + "?wh=" + wh + "&inv=" + inv + "&batch=" + batch,
                type: "GET",
                async:false,
                success: function (locatorJson) {
                    var locators = eval(locatorJson);
                    var locatorHtml = '<option value=""></option>';
                    $(locators).each(function (i, option) {
                        locatorHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                    });
                    $("#Locator").html(locatorHtml);                    
                }
            });
        }
    }    

    function BatchChange() {
        if(BatchIsDropDown()){
            if (isLocator()) {
                LocatorCallBack(whId, $("#InvId").val(), $("#Batch").val())
            }
            else {
                if (IsExist("AvaNum")) {
                    AvaNumCallBack(whId, "", $("#InvId").val(), $("#Batch").val())
                }
            }
        }
    }
    function BatchCallBack(wh, inv) {
        if (wh && inv) {
            var url = urls.getBatchUrl;
            $.ajax({
                url: url + "?wh=" + wh + "&inv=" + inv,
                type: "GET",
                async:false,
                success: function (batchJson) {
                    var batchs = eval(batchJson);
                    var batchHtml = '<option value=""></option>';
                    $(batchs).each(function (i, option) {
                        batchHtml += '<option value="' + option.Id + '">' + option.Name + '</option>';
                    });
                    $("#Batch").removeAttr("disabled").html(batchHtml);
                }
            });
        }
    }

    function AvaNumCallBack(wh, locator, inv, batch) {
        if (wh && inv && batch) {
            var url = urls.getAvaNumUrl;
            $.ajax({
                url: url + "?wh=" + wh + "&locator=" + locator + "&inv=" + inv + "&batch=" + batch,
                type: "GET",
                success: function (avaNum) {
                    $("#AvaNum").val(avaNum);
                }
            });
        }
    }

    function NumChange() {
        updateAmount();
        updateAddInAmount();
        updateAddOutAmount();
    }
    function PriceChange() {
        updateAmount();
        updateAddInAmount();
        updateAddOutAmount();
    }        
    function updateAmount() {
        if ($("#Num").val() && $("#Price").val()) {
            var num = parseFloat($("#Num").val());
            var price = parseFloat($("#Price").val());
            var amount = num * price;
            $("#Amount").val(amount); 
        }
        else {
            $("#Amount").val("");
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

    function beforeCheckValues(postdata, formid, mode) {
        var invName = $("#InvId", formid).find("option:selected").text();
        var locatorName = $("#Locator", formid).find("option:selected").text();
        var sdata = {
            InvName: invName,
            LocatorName: locatorName
        };
        $.extend(postdata, sdata);
        return postdata;
    }
    function beforeSubmit(postdata, formid) {
        var vouchType = $("#VouchType").val();
        var sdata = {
            VouchType: vouchType,
            RdId:$("#Id").val(),
            SVId : $("#Id").val(),
            TVId : $("#Id").val(),
            CVId : $("#Id").val(),
            ALVId : $("#Id").val(),
            MVId : $("#Id").val(),
        };
        $.extend(postdata, sdata);
        return [true, ''];
    }
    function serializeGridData(postData) {
        var sdata = {
            _search: true,
            VouchType:$("#VouchType").val(),
            RdId:$("#Id").val(),
            SVId : $("#Id").val(),
            TVId : $("#Id").val(),
            CVId : $("#Id").val(),
            ALVId : $("#Id").val(),
            MVId : $("#Id").val(),
        };
        var newPostData = $.extend(postData, sdata);
        return $.param(newPostData);
    }

    function afterclickPgButtons(whichbutton, formid, rowid) {
        $("#InvId").trigger("chosen:updated");
        var rowData = grid.jqGrid("getRowData", rowid);
        if(BatchIsDropDown()){
            if (isBatch()) {
                BatchCallBack(whId, rowData.InvId);
                $("#Batch").val(rowData.Batch);
            }
            if (isLocator()) {
                LocatorCallBack(whId, rowData.InvId, rowData.Batch);
                $("#Locator").val(rowData.Locator);
            }
            if (IsExist("AvaNum")) {
                AvaNumCallBack(whId, rowData.Locator, rowData.InvId, rowData.Batch)
            }
        }
    }

    function associateRetail() {
        var strdate = $("#RdDate").val();
        var strdeptid = $("#WhId").val();
        if (strdate.length==0) {
            alert("请输入出库日期");
            return;
        }
        if (strdeptid.length==0) {
            alert("请选择出库仓库");
            return;
        }
        var url = $("#CreateSell").data('url');
        myBlockUI();
        $.ajax({
            url: url + "?date=" + strdate + "&whId=" + strdeptid,
            type: "GET",
            success: function (invInfo) {
                myUnBlockUI();
                if (!invInfo.Sucess) {
                    alert(invInfo.Message);
                }
                else {
                    //刷新
                    $(gridId).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid', [{ page: 1}]);
                }
            }
        });
    }
    function VenIdCallBack(){
        var url = urls.GetReceivers;
        $.ajax({
            url: url,
            type: "GET",
            success: function (receiverJson) {
                var receivers = eval(receiverJson);
                var receiverHtml = "";
                $(receivers).each(function (i, option) {
                    receiverHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                });
                $("#rdRecord_VenId").html(receiverHtml);
            }
        });
    }
    
    function OpenInvDialog() {        
        var url = urls.addInventoryUrl;
        $("#InvIdDiv").load(url, function (response, status, xhr) {
            if (status == "error") {
                alert(xhr.status + " " + xhr.statusText);
            }
        })
        .dialog({ 
                    title:"存货档案",
                    modal: false,
                    width:1650,  
                    close: function (event, ui) {$(this).dialog('destroy');$(this).empty(); },
                    beforeClose: function (event, ui) { InvIdCallBack(); } ,
                    buttons:{'关闭': function(){$(this).dialog('close');return false;}},
                });
        ;  
        return false;      
    }