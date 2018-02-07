function ExportToExcel() {
    var input = $("<input>").attr("type", "hidden").attr("name", "isExportToExcel").val("true");
    $("#ExportToExcel").append($(input));
    $("#ExportToExcel").submit();
}