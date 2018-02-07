$(function () {
    if ($(prefix + "Id").val().length > 0) {
        var url = $("#cururl").data('url');
        $.ajax({
            url: url,
            type: "GET",
            data: $(formId).serialize(),
            success: function (jsonResult) {
                navSetValue(jsonResult);
            }
        });
    }
});