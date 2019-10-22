const regexEmail = /^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\-\.]+$/;

var inputEmail = $('#inputEmail');
var inputPassword = $('#inputPassword');
var btnIniciar = $("#btnIniciar");

function validarEmail() {

    var email = inputEmail.val();

    var validacion = false;

    if (email == null || email.length === 0 || email === "") {

        $("#errorEmail").find("span").text("Ingrese su Email");
        $("#errorEmail").fadeIn("slow");
    } else if (!regexEmail.test(email)) {

        $("#errorEmail").find("span").text("Ingrese un Email valido");
        $("#errorEmail").fadeIn("slow");
    } else {
        validacion = true;
    }

    return validacion;
}

function validarPassword() {

    var password = inputPassword.val();

    var validacion = false;

    if (password == null || password.length === 0 || password === "") {
        $("#errorPass").find("span").text("Ingrese su contraseña");
        $("#errorPass").fadeIn("slow");
    } else if (password.length < 8 || password.length > 15) {
        $("#errorPass").find("span").text("Su contraseña debe tener entre 8 a 15 caracteres");
        $("#errorPass").fadeIn("slow");
    } else if ((!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/.test(password)))) {
        $("#errorPass").find("span").text("Su contraseña debe tener letras, (minúsculas y mayúsculas) y números");
        $("#errorPass").fadeIn("slow");
    } else {
        validacion = true;
    }

    return validacion;
}



function validarRegistro() {
    return validarEmail() && validarPassword();
}

btnIniciar.click(function () {

    $(".errorSolidam").fadeOut();

    validarRegistro();

    if (validarRegistro() === true) {
        $("#formularioIniciar").submit();
    }

});

$(function () {

    $("input[type='password'][data-eye]").each(function (i) {
        var $this = $(this),
            id = 'eye-password-' + i,
            el = $('#' + id);

        $this.wrap($("<div/>", {
            style: 'position:relative',
            id: id
        }));

        $this.css({
            paddingRight: 60
        });
        $this.after($("<div/>", {
            html: '<i class="fa fa-eye"></i>',
            class: 'btn btn-primary btn-sm',
            id: 'passeye-toggle-' + i,
        }).css({
            position: 'absolute',
            right: 10,
            top: ($this.outerHeight() / 2) - 12,
            padding: '3px 7px',
            fontSize: 12,
            cursor: 'pointer',
            background: '#028817',
            border: '#000000'
        }));

        $this.after($("<input/>", {
            type: 'hidden',
            id: 'passeye-' + i
        }));

        var invalid_feedback = $this.parent().parent().find('.invalid-feedback');

        if (invalid_feedback.length) {
            $this.after(invalid_feedback.clone());
        }

        $this.on("keyup paste", function () {
            $("#passeye-" + i).val($(this).val());
        });
        $("#passeye-toggle-" + i).on("click", function () {
            if ($this.hasClass("show")) {
                $this.attr('type', 'password');
                $this.removeClass("show");
                $(this).removeClass("btn-outline-primary");
            } else {
                $this.attr('type', 'text');
                $this.val($("#passeye-" + i).val());
                $this.addClass("show");
                $(this).addClass("btn-outline-primary");
            }
        });
    });

});

