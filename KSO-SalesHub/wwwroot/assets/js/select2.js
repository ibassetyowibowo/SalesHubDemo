$(function () {
    'use strict'

    if ($(".js-example-basic-single").length) {
        $('.js-example-basic-single').select2({
            escapeMarkup: function (markup) {
                return markup;
            },
            width: '100%'
        });
    }
    if ($(".js-example-basic-multiple").length) {
        $(".js-example-basic-multiple").select2();
    }
});