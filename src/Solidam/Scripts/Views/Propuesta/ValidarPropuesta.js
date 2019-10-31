var nombreRef1 = $("#nombreRef1").val();
var nombreRef2 = $("#nombreRef2").val();
var telefonoRef1 = $("#telefonoRef1").val();
var telefonoRef2 = $("#telefonoRef2").val();
var monto = ("#monto").val();
var cbu = ("#cbu").val();
var cantHoras = ("#cantHoras").val();
var tipoProfesion = $("#tipoProfesion").val();


$("#btnEnviar").click(function (e) {
    $(".error").fadeOut();
    e.preventDefault();

    if (validarReferencias() && validarDonaciones())
        e.currentTarget.submit();
});

function validarReferencias() {
    var valor = true;

    if (nombreRef1 === null || nombreRef1 === "" || nombreRef1.length === 0) {
        $("#errorRN1").find("span").text("Debe ingresar una referencia");
        $("#errorRN1").fadeIn("slow");
        valor = false;
    }

    if (nombreRef2 === null || nombreRef2 === "" || nombreRef2.length === 0) {
        $("#errorRN2").find("span").text("Debe ingresar una referencia");
        $("#errorRN2").fadeIn("slow");
        valor = false;
    }

    if (telefonoRef1 === null || telefonoRef1 === "" || telefonoRef1.length === 0) {
        $("#errorRT1").find("span").text("Debe ingresar un teléfono");
        $("#errorRT1").fadeIn("slow");
        valor = false;
    }

    if (telefonoRef2 === null || telefonoRef2 === "" || telefonoRef2.length === 0) {
        $("#errorRT2").find("span").text("Debe ingresar un teléfono");
        $("#errorRT2").fadeIn("slow");
        valor = false;
    }

    return valor;
}

function validarDonaciones() {
    var valor = true;

    switch (tipoDonacion) {
        case 1:
            break;
        case 2:
            break;
        case 3:
            break;
    }

    return valor;
}