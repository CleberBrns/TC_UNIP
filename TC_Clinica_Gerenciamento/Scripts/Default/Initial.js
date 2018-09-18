$(document).ajaxStart(function () {
    /*
     * Iniciar o AJAX
     */
    $('body').addClass("loading");

    //$('body').loading();
}).ajaxSend(function () {
    /*
     * Enviar os dados do AJAX
     */
}).ajaxSuccess(function () {
    /*
     * AJAX realizado com sucesso
     */
}).ajaxError(function (event, request, settings) {
    /*
     * Erro ao enviar o AJAX
     */

    //console.log(event);
    //console.log(request);
    //console.log(setting);
}).ajaxComplete(function () {
    /*
     * Finalização do AJAX independente se foi realizado com sucesso ou não!
     */
}).ajaxStop(function () {
    /*
     * Terminar o AJAX
     */

    $('body').removeClass("loading");
    //$('body').loading('stop');
});


$(document).ready(function () {

    $.ajaxSetup({
        'beforeSend': function () {
            $('body').addClass("loading");
        },
        'complete': function () {
            $('body').removeClass("loading");
        }
    });

});