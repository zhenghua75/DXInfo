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
function navSetValue(jsonResult) {
    if (jsonResult) {
        if (jsonResult.Sucess == false) {
            alert(jsonResult.Message);
            resetForm();
            resetGrid();
            setButtonState("0");
            resetValidation();
            DisabledDiv();
        }
        else {
            EnabledDiv();
            SetFormValue(jsonResult);
            resetGrid();
            resetValidation();
            if (jsonResult.IsVerify == true) {
                DisabledDiv();
                setButtonState("3");
            }
            else {
                setButtonState("2");
            }
        }
    }
    else {
        EnabledDiv();
        resetForm();
        resetGrid();
        setButtonState("1");
        resetValidation();
    }
}
function setButton() {
    $("#add").button({
        text: true,
        icons: {
            primary: "ui-icon-plus"
        }
    });
    $("#modify").button({
        text: true,
        icons: {
            primary: "ui-icon-pencil"
        }
    });
    $("#delete").button({
        text: true,
        icons: {
            primary: "ui-icon-trash"
        }
    });
    $("#verify").button({
        text: true,
        icons: {
            primary: "ui-icon-check"
        }
    });
    $("#unverify").button({
        text: true,
        icons: {
            primary: "ui-icon-close"
        }
    });
    $("#print").button({
        text: true,
        icons: {
            primary: "ui-icon-print"
        }
    });
    $("#search").button({
        text: true,
        icons: {
            primary: "ui-icon-search"
        }
    });
    $("#start").button({
        text: true,
        icons: {
            primary: "ui-icon-seek-start"
        }
    });
    $("#prev1").button({
        text: true,
        icons: {
            primary: "ui-icon-seek-prev"
        }
    });
    $("#next1").button({
        text: true,
        icons: {
            primary: "ui-icon-seek-next"
        }
    });
    $("#end").button({
        text: true,
        icons: {
            primary: "ui-icon-seek-end"
        }
    });
    $("#save").button({
        text: true,
        icons: {
            primary: "ui-icon-disk"
        }
    });
    $("#cancel").button({
        text: true,
        icons: {
            primary: "ui-icon-close"
        }
    });
    $("#add").button().click(function () {
        var url = $("#addurl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                if (jsonResult) {
                    navSetValue();
                    $(prefix + "Code").val(jsonResult.Code);
                    $(prefix + jsonResult.VouchDateId).val(jsonResult.CurDate);
                    $(prefix + "Salesman").val(jsonResult.Salesman);
                    if ($(prefix + "OutWhId").length > 0) {
                        //$("select" + prefix + "OutWhId").get(0).selectedIndex = 1;
                        $(prefix + "OutWhId")[0].selectedIndex = 1;
                    }
                }
            },
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#modify").button().click(function () {
        var isVerify = $(prefix + "IsVerify").val().toLowerCase();
        if (isVerify == "true") {
            alert("已审核不能修改");
        }
        else {
            setButtonState("1");
        }
    });
    $("#save").button().click(function () {
        if ($(formId).valid()) {
            var isModify = $(prefix + "IsModify").val().toLowerCase();
            if (isModify == "false") {
                //添加
                var url = $("#saveaddurl").data('url');
                $.ajax({
                    url: url,
                    type: "POST",
                    data: $(formId).serialize(),
                    success: function (jsonResult) {
                        navSetValue(jsonResult);
                    },
                    dataType: "json",
                    beforeSend: function () {
                        myBlockUI();
                    },
                    complete: function () {
                        myUnBlockUI();
                    }
                });
            }
            else {
                //修改
                var url = $("#savemodifyurl").data('url');
                $.ajax({
                    url: url,
                    type: "POST",
                    data: $(formId).serialize(),
                    success: function (jsonResult) {
                        navSetValue(jsonResult);
                    },
                    dataType: "json",
                    beforeSend: function () {
                        myBlockUI();
                    },
                    complete: function () {
                        myUnBlockUI();
                    }
                });
            }
        }
    });
    $("#cancel").button().click(function () {
        //alert($("#rdRecord_IsModify").val());
        var isModify = $(prefix + "IsModify").val().toLowerCase();
        //alert(isModify);
        if (isModify=="false") {
            resetForm();
            resetValidation();
            resetGrid();
            setButtonState("0");
            DisabledDiv();
        }
        else {
            var isVerify = $(prefix + "IsVerify").val().toLowerCase();
            if (isVerify == "true") {
                setButtonState("3");
            }
            else {
                setButtonState("2");
            }
        }
    });
    $("#delete").button().click(function () {
        if (confirm("是否确认删除？")) {
            var url = $("#deleteurl").data('url');
            $.ajax({
                url: url,
                type: "POST",
                data: $(formId).serialize(),
                success: function (jsonResult) {
                    navSetValue(jsonResult);
                },
                dataType: "json",
                beforeSend: function () {
                    myBlockUI();
                },
                complete: function () {
                    myUnBlockUI();
                }
            });
        }
    });
    $("#verify").button().click(function () {
        if ($(formId).valid()) {
            var url = $("#verifyurl").data('url');
            $.ajax({
                url: url,
                type: "POST",
                data: $(formId).serialize(),
                success: function (jsonResult) {
                    navSetValue(jsonResult);
                },
                dataType: "json",
                beforeSend: function () {
                    myBlockUI();
                },
                complete: function () {
                    myUnBlockUI();
                }
            });
        }
    });
    $("#unverify").button().click(function () {
        var url = $("#unverifyurl").data('url');
        $.ajax({
            url: url,
            type: "POST",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                navSetValue(jsonResult);
            },
            dataType: "json",
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#start").button().click(function () {
        var url = $("#starturl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                //$.unblockUI;
                navSetValue(jsonResult);
            },
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#prev1").button().click(function () {
        var url = $("#prevurl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                navSetValue(jsonResult);
            },
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#next1").button().click(function () {
        var url = $("#nexturl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                navSetValue(jsonResult);
            },
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#end").button().click(function () {
        var url = $("#endurl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                navSetValue(jsonResult);
            },
            beforeSend: function () {
                myBlockUI();
            },
            complete: function () {
                myUnBlockUI();
            }
        });
    });
    $("#print").button().click(function () {
        var url = $("#printurl").data('url');
        var prtId = $(prefix + "Id").val();
        window.open(url + '?Id=' + prtId + '&vouchType=' + $("#vouchType_Code").val(), '_blank');
        return false;
    });
    $("#search").button().click(function () {
        var url = $("#searchurl").data('url');
        self.location = url + "?vouchType=" + $("#vouchType_Code").val();
    });

}