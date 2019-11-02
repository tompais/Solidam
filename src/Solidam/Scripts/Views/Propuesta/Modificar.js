﻿$("#inputFechaFin").parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true,
    startDate: "tomorrow"
});

$(document).ready(function ($) {

    // Disable scroll when focused on a number input.
    $('form').on('focus', 'input[type=number]', function (e) {
        $(this).on('wheel', function (e) {
            e.preventDefault();
        });
    });

    // Restore scroll on number inputs.
    $('form').on('blur', 'input[type=number]', function (e) {
        $(this).off('wheel');
    });

    // Disable up and down keys.
    $('form').on('keydown', 'input[type=number]', function (e) {
        if (e.which == 38 || e.which == 40)
            e.preventDefault();
    });

});

console.log(window.fecha);
if (fecha == false) {
    console.log("entre");
    $("#formPropuesta input").prop('disabled', true);
    $("#descripcionPropuesta").prop('disabled', true);
    $("#tipoProfesion").prop('disabled', true);
    $("#btnEnviar").attr('disabled', true);
}