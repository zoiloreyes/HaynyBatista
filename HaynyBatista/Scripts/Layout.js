

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
            $("header + .container-fluid").css("min-height", $(window).height());

        }

        var InitPlugins = function () {
            tinymce.init({
                selector: '.tinymceTextArea',
                height: 500,
                theme: 'modern',
                plugins:[ 'advlist autolink lists link charmap print preview anchor textcolor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime table contextmenu paste code help wordcount'],
                toolbar1: 'insert | undo redo |  formatselect | bold italic backcolor fontselect  fontsizeselect | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
                image_advtab: true,
                templates: [
                    { title: 'Test template 1', content: 'Test 1' },
                    { title: 'Test template 2', content: 'Test 2' }
                ],
                language: 'es_MX',
                fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt"

            });
            autosize($(".ASTextArea"));
            $(".clamped").each(function () {
                var element = $(this)[0]
                $clamp(element, { clamp: 'auto' });
            });
        }
        var InitContent = function () {
            

            $('[data-toggle="tooltip"]').tooltip();

            $(".indirectClick").click(function () {
                $($(this).attr("href")).click();
            });
            $(".SNShareButton").on('click', function (e) {
                $(this).find(".fab").removeClass("no");
                if (e.target != this) return;
                $(this).find(".fab").toggleClass("active");
                //$('.share, .fab').toggleClass("active");
            });
            $(".SmoothScroll").click(function (event) {
                event.preventDefault();
                var objetivo = this.hash;
                $('html, body').animate({
                    scrollTop: $(objetivo).offset().top
                }, 1000, function () {
                    var $objetivo = $(objetivo);
                    $objetivo.focus();
                    if ($objetivo.is(":focus")) {
                        return false;
                    } else {
                        $objetivo.attr('tabindex', '-1');
                        $objetivo.focus();
                    };
                });
            });
        }
        var InitLayout = function () {
            InitNav();
            InitPlugins();
            InitContent();
        }
        
        return {
            Start: InitLayout
        }
    })();

    LayoutModule.Start();
});