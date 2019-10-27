$("#inputFechaFin").parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true
});

var i = 0;

var profesiones = ["Albañil", "Plomero", "Electricista",
    "Abogado", "Contador", "Trabajador social", "Psicóloga", "Psicopedagoga", "Cocinero",
    "Limpieza", "Otros"];

$('#tipoDonacion').change(function () {
        if ($(this).val() === '1') {
            $("#datosDonacion").empty();
            $("#datosDonacion").append("<label for='monto'>Monto</label>");
            $("#datosDonacion").append("<input type='number' step='0.01' class='form-control' id='monto' name='PropuestasDonacionesMonetarias[0].Dinero' />");
            $("#datosDonacion").append("<label for='cbu'>CBU</label>");
            $("#datosDonacion").append("<input type='text' class='form-control' id='cbu' name='PropuestasDonacionesMonetarias[0].CBU' />");
        }
        else
            if ($(this).val() === '2') {
                $("#datosDonacion").empty();
                var label1 = "<label for='nombreInsumos'>Descripción</label>";
                var input1 = "<input type='text' class='form-control' id='nombreInsumos' name='PropuestasDonacionesInsumos[" + i + "].Nombre' />";
                var div1 = "<div class='col-6'>" + label1 + input1 + "</div>";
                var label2 = "<label for='cantInsumos'>Cantidad</label>";
                var input2 = "<input type='number' class='form-control' id='cantInsumos' name='PropuestasDonacionesInsumos[" + i + "].Cantidad' />";
                var div2 = "<div class='col-6'>" + label2 + input2 + "</div>";

                var botonAgregar = $("<button type='button' class='btn btn-link pl-0' id='agregarItem'>Agregar Item</button>");

                botonAgregar.click(function () {
                    var label1 = "<label for='nombreInsumos" + i + "'>Descripción</label>";
                    var input1 = "<input type='text' class='form-control' id='nombreInsumos" + i + "' name='PropuestasDonacionesInsumos[" + i + "].Nombre' />";
                    var div1 = "<div class='col-6'>" + label1 + input1 + "</div>";
                    var label2 = "<label for='cantInsumos" + i + "'>Cantidad</label>";
                    var input2 = "<input type='number' class='form-control' id='cantInsumos" + i + "' name='PropuestasDonacionesInsumos[" + i + "].Cantidad' />";
                    var div2 = "<div class='col-6'>" + label2 + input2 + "</div>";
                    $("#masInsumos").append("<div class='row'>" + div1 + div2 + "</div>");
                    i++;
                });

                $("#datosDonacion").append("<div class='row'>" + div1 + div2 + "</div><div class='form-group' id='masInsumos'></div>");
                $("#datosDonacion").append(botonAgregar);
                i++;
            }
            else {
                $("#datosDonacion").empty();
                $("#datosDonacion").append("<label for='cantHoras'>Cantidad de horas</label>");
                $("#datosDonacion").append("<input type='number' class='form-control' id='cantHoras' name='PropuestasDonacionesHorasTrabajo[0].CantidadHoras' />");
                $("#datosDonacion").append("<label for='profesion'>Profesión</label>");

                var select = "<select class='form-control' id='tipoDonacion' name='PropuestasDonacionesHorasTrabajo[0].Profesion'><option disabled selected value''>Seleccionar</option>";

                $.each(profesiones, function (index, value) {
                    select += "<option value'" + value + "'>" + value + "</option>";
                });

                select += "</select>";

                $("#datosDonacion").append(select);
            }
});