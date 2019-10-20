$('#tipoDonacion').change(function () {
    if ($(this).val() === '0')
        $("#datosDonacion").empty();
    else
        if ($(this).val() === '1') {
            $("#datosDonacion").append("<label for='monto'>Monto</label>");
            $("#datosDonacion").append("<input type='number' step='0.01' class='form-control' id='monto' name='PropuestaDonacionMonetaria.Dinero' />");
            $("#datosDonacion").append("<label for='cbu'>CBU</label>");
            $("#datosDonacion").append("<input type='text' class='form-control' id='cbu' name='PropuestaDonacionMonetaria.CBU' />");
        }
        else
            if ($(this).val() === '2') {

            }
            else {
                $("#datosDonacion").append("<label for='cantHoras'>Cantidad de horas</label>");
                $("#datosDonacion").append("<input type='number' class='form-control' id='cantHoras' name='PropuestaDonacionHorasTrabajo.CantidadHoras' />");
                $("#datosDonacion").append("<label for='profesion'>Profesión</label>");
                $("#datosDonacion").append("<input type='text' class='form-control' id='profesion' name='PropuestaDonacionHorasTrabajo.Profesion' />");
            }
});