$.fn.pxWidth = function () {
    return $(this).width() + "px";
};

$.fn.pxHeight = function () {
    return $(this).height() + "px";
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