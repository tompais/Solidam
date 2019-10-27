//$(".check").change(function () {
//    if (this.checked) {
//        $(this).parent().parent().find("input[type=number]").removeClass("d-none");
//    } else {
//        $(this).parent().parent().find("input[type=number]").addClass("d-none");
//        $(this).parent().parent().find(".error").addClass("d-none");
//    }
//});

//$("#btnConfirmar").click(function () {
//    //var donaciones = [];
//    //var validacion = true;
//    //$(".check:checked").each(function (index, value) {
//    //    //if (!validarNumero(parseInt($(value).parent().parent().find("input[type=number]").val()), $(value))) validacion = false;
//    //    var donacionInsumo = {};
//    //    donacionInsumo.idPropuestaDonacionInsumo = $(value).attr("don-id");
//    //    donacionInsumo.nombre = $(value).parent().parent().find(".nombre-item").text();
//    //    //donacionInsumo.cantidad = $(value).parent().parent().find("input[type=number]").val();
//    //    donaciones.push(donacionInsumo);
//    //});
//    //if ($(".check:checked").length > 0) {
//    //    //$.ajax({
//    //    //    url: window.pathCrearDonacionInsumo,
//    //    //    data: { donaciones: donaciones },
//    //    //    type: 'POST',
//    //    //    success: function(data) {
//    //    //        window.location.reload();
//    //    //    }
//    //    //});
//    //    $("#modalInsumos").modal("show");
//    //    dibujarItems(donaciones);
//    //}
//    $("#modalInsumos").modal("show");
//    dibujarItems();
//});

function dibujarItems() {
    var form = $("<form method='post' action='" + window.pathCrearDonacionInsumo + "'>");
    var hiddenPropuesta = $("<input type='hidden' name='Propuesta.IdPropuesta' value='"+window.propuestaId+"'>");
    $.each($(".check:checked"),
        function (index, value) {
            var idPropuestaDonacionInsumo = $(value).attr("don-id");
            var nombre = $(value).parent().parent().find(".nombre-item").text();
            var formGroup = $("<div class='form-group'>");
            var label = $("<label>");
            label.text("Ingrese la cantidad de '"+ nombre+"'");
            var input = $("<input class='form-control' type='number' name='DonacionInsumos[" + index + "].Cantidad'>");
            formGroup.append(label);
            formGroup.append(input);
            form.append(formGroup);
            form.append(hiddenPropuesta);
            var hiddenItemRequerido = $("<input type='hidden' value='"+idPropuestaDonacionInsumo+"' name='DonacionInsumos[" + index + "].IdPropuestaDonacionInsumo'>");
            form.append(hiddenItemRequerido);
        });
    $(".modal-body").append(form);
}

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

$("#btnDonar").click(function() {
    $(this).parent().parent().find("form").submit();
});
$('#modalInsumos').on('hidden.bs.modal', function (e) {
    $(".modal-body form").empty();
});