function setButtonState(operFlag) {
    switch (operFlag) {
        case "0": //初始状态
            $("#save").button("disable");
            $("#cancel").button("disable");
            $("#add").button("enable");
            $("#modify").button("disable");
            $("#delete").button("disable");
            $("#verify").button("disable");
            $("#unverify").button("disable");
            $("#print").button("disable");

            $("#add_"+gridId2+"_top").addClass('ui-state-disabled');
            $("#edit_" + gridId2 + "_top").addClass('ui-state-disabled');
            $("#del_" + gridId2 + "_top").addClass('ui-state-disabled');
            break;
        case "1": //添加按钮按下
            $("#save").button("enable");
            $("#cancel").button("enable");
            $("#add").button("disable");
            $("#modify").button("disable");
            $("#delete").button("disable");
            $("#verify").button("disable");
            $("#unverify").button("disable");
            $("#print").button("disable");

            $("#add_" + gridId2 + "_top").removeClass('ui-state-disabled');
            $("#edit_" + gridId2 + "_top").removeClass('ui-state-disabled');
            $("#del_" + gridId2 + "_top").removeClass('ui-state-disabled');
            break;
        case "2": //导航按钮按下-审核
            $("#save").button("disable");
            $("#cancel").button("disable");
            $("#add").button("enable");
            $("#modify").button("enable");
            $("#delete").button("enable");
            $("#verify").button("enable");
            $("#unverify").button("disable");
            $("#print").button("enable");

            $("#add_" + gridId2 + "_top").removeClass('ui-state-disabled');
            $("#edit_" + gridId2 + "_top").removeClass('ui-state-disabled');
            $("#del_" + gridId2 + "_top").removeClass('ui-state-disabled');
            break;
        case "3": //导航按钮按下-弃审
            $("#save").button("disable");
            $("#cancel").button("disable");
            $("#add").button("enable");
            $("#modify").button("disable");
            $("#delete").button("disable");
            $("#verify").button("disable");
            $("#unverify").button("enable");
            $("#print").button("enable");

            $("#add_" + gridId2 + "_top").addClass('ui-state-disabled');
            $("#edit_" + gridId2 + "_top").addClass('ui-state-disabled');
            $("#del_" + gridId2 + "_top").addClass('ui-state-disabled');

            break;
    }
}
function cancelButtonState(operFlag) {
    switch (operFlag) {
        case "1":
            setButtonState("0");
            break;
    }
}