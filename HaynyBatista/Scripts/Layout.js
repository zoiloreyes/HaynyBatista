

$(document).ready(function () {
    var LayoutModule = (function () {
        var InitNav = function () {
            $("#MenuDivider").css("width", "90%");
            $('.dropdown-menu').parent().on('show.bs.dropdown', function (e) {
                $(this).find('.dropdown-menu').first().stop(true, true).slideDown(300);
            });

            $('.dropdown-menu').parent().on('hide.bs.dropdown', function (e) {
                $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
            });
        }
        return {
            Start: InitNav
        }
    })();

    LayoutModule.Start();
});