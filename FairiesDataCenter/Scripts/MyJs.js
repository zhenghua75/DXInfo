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
    //var key = e.charCode || e.keyCode;
    var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
    if (key == 13) {
    }
    /* ENTER PRESSED*/
    if (key == 13) {
        /* FOCUS ELEMENT */
        var inputs = $(this).parents("form").eq(0).find(":input");
        var idx = inputs.index(this);
        //console.log(idx);
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
            inputs[idx + 1].focus(); //  handles submit buttons
            //inputs[idx + 1].select();
        }
        return false;
    }
}
function validateCard(value, column) {
    if (value.length != 5) {
        return [false, "请输入5位卡号"];
    }
    else {
        var myurl = $("#IsHaveCardNoUrl").data('url');
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
    var url = $("#IsNoActiveXCheck").data('url');
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
    //return "123456789";
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
    clientMac();
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
    var isAMSApp = $("#IsAMSApp").data('url');
    if (isAMSApp == true) {
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
                    // for custom buttons
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
        menuDiv.appendTo('body');
        grid.contextMenu(menuId, {
            bindings: myBinding,
            onContextMenu: function (e) {
                var rowId = $(e.target).closest("tr.jqgrow").attr("id"), p = grid[0].p, i, lastSelId;
                if (rowId && getSelectedText() === '') {
                    i = $.inArray(rowId, p.selarrrow);
                    if (p.selrow !== rowId && i < 0) {
                        // prevent the row from be unselected
                        // the implementation is for "multiselect:false" which we use,
                        // but one can easy modify the code for "multiselect:true"
                        grid.jqGrid('setSelection', rowId);
                    } else if (p.multiselect) {
                        // Edit will edit FIRST selected row.
                        // rowId is allready selected, but can be not the last selected.
                        // Se we swap rowId with the first element of the array p.selarrrow
                        lastSelId = p.selarrrow[p.selarrrow.length - 1];
                        if (i !== p.selarrrow.length - 1) {
                            p.selarrrow[p.selarrrow.length - 1] = rowId;
                            p.selarrrow[i] = lastSelId;
                            p.selrow = rowId;
                        }
                    }
                    return true;
                } else {
                    return false; // no contex menu
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