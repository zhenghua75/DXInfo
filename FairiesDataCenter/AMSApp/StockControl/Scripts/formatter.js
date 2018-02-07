function formatCheckBox(val, row) {
    if (val == 'on') {
        return '<input type="checkbox" checked disabled/>';
    } else {
        return '<input type="checkbox" disabled/>';
    }
}
function toFixed2(val, row) {
    var valFloat = parseFloat(val);
    if (isNaN(valFloat)) return "";
    return valFloat;
}
function formatNegative2(val, row) {
    var valFloat = parseFloat(val);
    if (isNaN(valFloat)) return "";
    if (valFloat < 0) {
        return '<span style="color:red;">(' + Math.abs(valFloat) + ')</span>';
    } else {
        return valFloat;
    }
}
function toFixed4(val, row) {
    var valFloat = parseFloat(val);
    if (isNaN(valFloat)) return "";
    return valFloat;
}
function formatNegative4(val, row) {
    var valFloat = parseFloat(val);
    if (isNaN(valFloat)) return "";
    if (valFloat < 0) {
        return '<span style="color:red;">(' + Math.abs(valFloat) + ')</span>';
    } else {
        return valFloat;
    }
} 
function formatDate(val, row) {
    var d = new Date(val);
    return d.format("yyyy-MM-dd");
}
