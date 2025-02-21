// npm package: jquery-steps
// github link: https://github.com/rstaib/jquery-steps/

$(function () {
    'use strict';

    //$("#wizard-register").steps({
    //    headerTag: "h2",
    //    bodyTag: "section",
    //    transitionEffect: "fade",
    //    onStepChanged: function (event, currentIndex, priorIndex) {
    //        if (currentIndex === 1) {
    //            $('.js-example-basic-single').select2('destroy').select2({
    //                escapeMarkup: function (markup) {
    //                    return markup;
    //                }
    //            }).css('width', '100%');
    //        }
    //    }
    //});

    $("#wizardVertical").steps({
        headerTag: "h2",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        stepsOrientation: 'vertical'
    });

});