

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
            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '< Ant',
                nextText: 'Sig >',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);
            $(".DataTable").DataTable({
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                }
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
            $(".DatePickerUI").datepicker();
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