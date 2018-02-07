function moveDialog(dialogId) {
    $('#' + dialogId).panel('move', {
        left: window.event.clientX ,
        top: window.event.clientY
    });
}