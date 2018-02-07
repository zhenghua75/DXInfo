function ExportToExcel() {
    var input1 = $("<input>").attr("type", "hidden").attr("name", "isExportToExcel").val("true");
    var input2 = $("<input>").attr("type", "hidden").attr("name", "vouchType").val($("#vouchType_Code").val());
    $("#ExportToExcel").append($(input1));
    $("#ExportToExcel").append($(input2));
    $("#ExportToExcel").submit();
}