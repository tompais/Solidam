$(function () {
    $("#inputFechaFin").parent().datepicker({
        format: 'dd/mm/yyyy',
        language: 'es',
        autoclose: true
    });
});

var i = 0;

var profesiones = ["Albañil", "Plomero", "Electricista",
    "Abogado", "Contador", "Trabajador social", "Psicóloga", "Psicopedagoga", "Cocinero",
    "Limpieza", "Otros"];

$('#tipoDonacion').change(function () {
        if ($(this).val() === '1') {
            $("#datosDonacion").empty();
            $("#datosDonacion").append("<label for='monto'>Monto</label>");
            $("#datosDonacion").append("<input type='number' step='0.01' class='form-control' id='monto' name='PropuestaDonacionMonetaria.Dinero' />");
            $("#datosDonacion").append("<div id='errorMonto' class='error'><i class='fas fa - exclamation - triangle mr - 2'></i><span></span></div >");
            $("#datosDonacion").append("<label for='cbu'>CBU</label>");
            $("#datosDonacion").append("<input type='text' class='form-control' id='cbu' name='PropuestaDonacionMonetaria.CBU' />");
            $("#datosDonacion").append("<div id='errorCBU' class='error'><i class='fas fa - exclamation - triangle mr - 2'></i><span></span></div >");
        }
        else
            if ($(this).val() === '2') {
                $("#datosDonacion").empty();
                var label1 = "<label for='nombreInsumos'>Descripción</label>";
                var input1 = "<input type='text' class='form-control' id='nombreInsumos' name='PropuestaDonacionInsumo[" + i + "].Nombre' />";
                var div1 = "<div class='col'>" + label1 + input1 + "</div>";
                var label2 = "<label for='cantInsumos'>Cantidad</label>";
                var input2 = "<input type='number' class='form-control' id='cantInsumos' name='PropuestaDonacionInsumo[" + i + "].Cantidad' />";
                var div2 = "<div class='col'>" + label2 + input2 + "</div>";
                $("#datosDonacion").append("<div class='row'>" + div1 + div2 + "</div>");
                i++;
            }
            else {
                $("#datosDonacion").empty();
                $("#datosDonacion").append("<label for='cantHoras'>Cantidad de horas</label>");
                $("#datosDonacion").append("<input type='number' class='form-control' id='cantHoras' name='PropuestaDonacionHorasTrabajo.CantidadHoras' />");
                $("#datosDonacion").append("<div id='errorCantHs' class='error'><i class='fas fa - exclamation - triangle mr - 2'></i><span></span></div >");
                $("#datosDonacion").append("<label for='profesion'>Profesión</label>");

                var select = "<select class='form-control' id='tipoDonacion' name='PropuestaDonacionHorasTrabajo.Profesion'><option disabled selected value''>Seleccionar</option>";

                $.each(profesiones, function (index, value) {
                    select += "<option value'" + value + "'>" + value + "</option>";
                });

                select += "</select>";

                $("#datosDonacion").append(select);
            }
});