$("#btnEnviar").click(function (e) {
    $(".error").fadeOut();
    e.preventDefault();

    donaciones = validarDonaciones();
    referencias = validarReferencias();

    if (donaciones && referencias)
        $("#formPropuesta").submit();
});

function validarReferencias() {
    var nombreRef1 = $("#nombreRef1").val();
    var nombreRef2 = $("#nombreRef2").val();
    var telefonoRef1 = $("#telefonoRef1").val();
    var telefonoRef2 = $("#telefonoRef2").val();
    var valor = true;

    if (nombreRef1 === null || nombreRef1 === "" || nombreRef1.length === 0) {
        $("#errorRN1").find("span").text("Debe ingresar una referencia");
        $("#errorRN1").fadeIn("slow");
        valor = false;
    }
    else
        if (nombreRef1.length > 50) {
            $("#errorRN1").find("span").text("Debe ingresar una referencia de hasta 50 caracteres");
            $("#errorRN1").fadeIn("slow");
            valor = false;
        }

    if (nombreRef2 === null || nombreRef2 === "" || nombreRef2.length === 0) {
        $("#errorRN2").find("span").text("Debe ingresar una referencia");
        $("#errorRN2").fadeIn("slow");
        valor = false;
    }
    else
        if (nombreRef2.length > 50) {
            $("#errorRN2").find("span").text("Debe ingresar una referencia de hasta 50 caracteres");
            $("#errorRN2").fadeIn("slow");
            valor = false;
        }

    if (telefonoRef1 === null || telefonoRef1 === "" || telefonoRef1.length === 0) {
        $("#errorRT1").find("span").text("Debe ingresar un teléfono");
        $("#errorRT1").fadeIn("slow");
        valor = false;
    }
    else
        if (telefonoRef1.length > 50) {
            $("#errorRT1").find("span").text("Debe ingresar un teléfono de hasta 50 caracteres");
            $("#errorRT1").fadeIn("slow");
            valor = false;
        }

    if (telefonoRef2 === null || telefonoRef2 === "" || telefonoRef2.length === 0) {
        $("#errorRT2").find("span").text("Debe ingresar un teléfono");
        $("#errorRT2").fadeIn("slow");
        valor = false;
    }
    else
        if (telefonoRef2.length > 50) {
            $("#errorRT2").find("span").text("Debe ingresar un teléfono de hasta 50 caracteres");
            $("#errorRT2").fadeIn("slow");
            valor = false;
        }

    return valor;
}

function validarDonaciones() {
    var valor = true

    switch (tipoDonacion) {
        case 1:
            var monto = $("#monto").val();
            var cbu = $("#cbu").val();

            if (monto === null || monto.length === 0 || monto <= 0 || monto.toString().length > 18) {
                $("#errorMonto").find("span").text("Debe ingresar un monto válido");
                $("#errorMonto").fadeIn("slow");
                valor = false;
            }
            if (cbu === null || cbu === "" || cbu.length === 0) {
                $("#errorCBU").find("span").text("Debe ingresar un CBU");
                $("#errorCBU").fadeIn("slow");
                valor = false;
            }
            else
                if (cbu.length > 80) {
                    $("#errorCBU").find("span").text("Debe ser de menos de 80 caracteres");
                    $("#errorCBU").fadeIn("slow");
                    valor = false;
                }
            break;
        case 2:
            var j = 0;
            while (j < i && valor) {
                if ($("#nombreInsumos-" + j + "").val() === null || $("#nombreInsumos-" + j + "").val() === "" || $("#nombreInsumos-" + j + "").val().length === 0 || $("#nombreInsumos-" + j + "").val().length > 50)
                    valor = false;
                else
                    if ($("#cantInsumos-" + j + "").val() === null || $("#cantInsumos-" + j + "").val().length === 0 || $("#cantInsumos-" + j + "").val() <= 0 || $("#cantInsumos-" + j + "").val() > maximo)
                        valor = false;
                j++;
            }
            if (!valor) {
                $("#errorInsumos").find("span").text("Debe completar todos los campos de los insumos correctamente");
                $("#errorInsumos").fadeIn("slow");
            }
            break;
        case 3:
            var cantHoras = $("#cantHoras").val();
            var tipoProfesion = $("#tipoProfesion").val();
            if (cantHoras === null || cantHoras.length === 0 || cantHoras <= 0 || cantHoras > maximo) {
                $("#errorCantHoras").find("span").text("Debe ingresar una cantidad de horas válida");
                $("#errorCantHoras").fadeIn("slow");
                valor = false;
            }
            if (tipoProfesion === null) {
                $("#errorProfesion").find("span").text("Debe seleccionar una profesión");
                $("#errorProfesion").fadeIn("slow");
                valor = false;
            }
            break;
    }

    return valor;
}