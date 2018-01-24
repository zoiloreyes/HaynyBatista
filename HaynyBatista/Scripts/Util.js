﻿$.fn.pxWidth = function () {
    return $(this).width() + "px";
};

$.fn.pxHeight = function () {
    return $(this).height() + "px";
};

$.fn.addTempClass = function (options) {
    var settings = $.extend({
        // These are the defaults.
        className: "",
        timeOut: 3000
    }, options);

    return this.each(function () {
        if (settings.className) {
            var element = $(this);
            element.addClass(settings.className);
            setTimeout(function () {
                element.removeClass(settings.className);
            }, settings.timeOut)
        }
    });
};

var Notificar = function (classes, content) {
    var snack = $("<div>", {
        class: "snackbar show " + classes
    });
    snack.append(content);

    snack.appendTo("body");

    setTimeout(function () {

        snack.removeClass("show")
        setTimeout(function () {

            //snack.remove();
        }, 600);
    },3000)
}