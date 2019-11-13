$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
    }

    var modalNotificacionCompletarPerfil = $("#modalNotificacionCompletarPerfil");


    if (modalNotificacionCompletarPerfil !== undefined && modalNotificacionCompletarPerfil !== null && modalNotificacionCompletarPerfil.length !== 0) {
        modalNotificacionCompletarPerfil.modal("show");
    }
});