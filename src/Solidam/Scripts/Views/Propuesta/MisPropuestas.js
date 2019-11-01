$(document).ready(function () {

    $(".activas").click(function () {

        if ($('.activas').prop('checked')) {
            url = "http://localhost:23371/Propuesta/MisPropuestasActivas";
            $(location).attr('href', url);
        }
        else {
            url = "http://localhost:23371/Propuesta/MisPropuestas";
            $(location).attr('href', url);
        }
    });

});