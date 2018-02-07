jQuery(function () {
    jQuery('ul.sf-menu').superfish();
    $.datepicker.setDefaults({
        dateFormat: 'yy-mm-dd',
        showOtherMonths: true,
        selectOtherMonths: true,
        showButtonPanel: true,
        changeMonth: true,
        changeYear: true,
        firstDay: 0,
        constrainInput: true
     });
    jQuery.validator.methods["date"] = function (value, element) {
        try { jQuery.datepicker.parseDate('yy-mm-dd', value); return true; }
        catch (e) { return false; }
    };
    var myurl = $("#IsAMSApp").data('url');
    if (myurl == false) {
        var isNoActiveXCheck = $("#IsNoActiveXCheck").data('url');
        if (isNoActiveXCheck == true) return true;
        if (!Verify()) {
            var url = $("#LogOffUrl").data('url');
            window.location.href = url;
        }
    }    
});
$.ajaxSetup({
//    error: function (x, e) {
//        if (x.status == 0) {
//            alert('You are offline!!\n Please Check Your Network.');
//        } else if (x.status == 404) {
//            alert('Requested URL not found.');
//        } else if (x.status == 500) {
//            alert('Internel Server Error.' + x.responseText);
//        } else if (e == 'parsererror') {
//            alert('Error.\nParsing JSON Request failed.');
//        } else if (e == 'timeout') {
//            alert('Request Time out.');
//        } else {
//            alert('Unknow Error.\n' + x.responseText);
//        }
//        return false;
//    },
    cache: false
});