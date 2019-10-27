$(".check").change(function () {
    if (this.checked) {
        $(this).parent().parent().find("input[type=number]").removeClass("d-none");
    } else {
        $(this).parent().parent().find("input[type=number]").addClass("d-none");
        $(this).parent().parent().find(".error").addClass("d-none");
    }
});

$("#btnConfirmar").click(function () {
    var donaciones = [];
    var validacion = true;
    $(".check:checked").each(function (index, value) {
        if (!validarNumero(parseInt($(value).parent().parent().find("input[type=number]").val()), $(value))) validacion = false;
        var donacionInsumo = {};
        donacionInsumo.idPropuestaDonacionInsumo = $(value).attr("don-id");
        donacionInsumo.cantidad = $(value).parent().parent().find("input[type=number]").val();
        donaciones.push(donacionInsumo);
    });
    if (validacion && $(".check:checked").length > 0) {
        $.ajax({
            url: window.pathCrearDonacionInsumo,
            data: { donaciones: donaciones },
            type: 'POST',
            success: function(data) {
                window.location.reload();
            }
        });
    }
});

function validarNumero(cantidad,element) {
    var restante = element.parent().parent().find(".rest").text();
    if (cantidad > restante || cantidad <= 0 || isNaN(cantidad)) {
        element.parent().parent().find(".error").text(isNaN(cantidad) ? "Debe ingresar un valor" : "No se puede ingresar esa cantidad");
        element.parent().parent().find(".error").removeClass("d-none");
        return false;
    } else {
        element.parent().parent().find(".error").addClass("d-none");
        return true;
    }
}