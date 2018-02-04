$.fn.pxWidth = function () {
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

function validarForm(form) {
    var retorno = {
        Estado: true,
        Errores: []
    }
    function elemento(objeto, msg) {
        this.objeto = objeto;
        this.msg = msg;
    }
    var value = true;

    $(form).find(".requerido:enabled").each(function () {
        if ($(this).val().trim() == "" || $(this).val() == 0) {
            retorno.Estado = false;
            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " es requerido");
            retorno.Errores.push(problematico);
        } else {

        }
    });
    $(form).find(".tinyMCErequerido:enabled").each(function () {
        if (window.parent.tinyMCE.get($(this).attr("id")).getContent("").length <= 0) {
            retorno.Estado = false;
            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " es requerido");
            retorno.Errores.push(problematico);
        }
    });
    $(form).find(".numero:enabled").each(function () {
        if (!$.isNumeric($(this).val().trim())) {
            retorno.Estado = false;
            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " debe ser un numero");
            retorno.Errores.push(problematico);
        } else {

        }
    });
    $(form).find(".email:enabled").each(function () {
        var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
        if (pattern.test($(this).val())) {

        } else {
            retorno.Estado = false;
            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " debe ser un email");
            retorno.Errores.push(problematico);
        }
    });
    $(form).find(".longitud:enabled").each(function () {
        if ($(this).val().trim().length <= $(this).data("longitud")) {

            retorno.Estado = false;
            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " es muy corto");
            retorno.Errores.push(problematico);
        } else {

        }
    });
    $(form).find(".repetido:enabled").each(function () {
        var valorComparado = $(this).data("repetido");

        if ($(this).val() != $(valorComparado).val()) {

            retorno.Estado = false;

            var problematico = new elemento($(this), "El campo " + $(this).data("title") + " no es igual a " + $(valorComparado).data("title"));
            retorno.Errores.push(problematico);
        } else {

        }
    });

    return retorno;
}

$.fn.clickOff = function (callback, selfDestroy) {
    var clicked = false;
    var parent = this;
    var destroy = selfDestroy || true;

    parent.click(function () {
        clicked = true;
    });

    $(document).click(function (event) {
        if (!clicked) {
            callback(parent, event);
        }
        if (destroy) {
            //parent.clickOff = function() {};
            //parent.off("click");
            //$(document).off("click");
            //parent.off("clickOff");
        };
        clicked = false;
    });
};

//Formatea una fecha iso a dd/mm/yyyy
function ISOToDate(isodate) {
    var date = new Date(isodate);

    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}
function ISOToDateUnFormat(isodate) {
    var date = new Date(isodate);

    return ("0" + date.getDate()).slice(-2) + "" + ("0" + (date.getMonth() + 1)).slice(-2) + date.getFullYear();
}

//dd/mm/yyyy a mm/dd/yyyy
function ESdateToUSdate(ESdate) {
    return USdate = ESdate.split(/\//).reverse().join('/');
}
function objectifyForm(jqueryForm) {
    var paramObj = {};
    $.each(jqueryForm.serializeArray(), function (_, kv) {
        paramObj[kv.name] = kv.value;
    });
    return paramObj;
}

function isSameTime(date1, date2) {
    if (date1.getHours() == date2.getHours() && date1.getMinutes() == date2.getMinutes() && date1.getSeconds() == date2.getSeconds()) {
        return true;
    } else {
        return false;
    }
}