$.extend({
    getUrlVars: function () {
        var vars = [], hash;        
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});
//$.fn.panel.defaults.onBeforeDestroy = function () {/* 回收内存 */
//    //alert('test');
//    var frame = $('iframe', this);
//    if (frame.length > 0) {
//        //alert('test');
//        frame[0].contentWindow.document.write('');
//        frame[0].contentWindow.close();
//        frame.remove();
//        if ($.browser.msie) {
//            CollectGarbage();
//        }
//    }
//};