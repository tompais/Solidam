var inputFechaNacimiento = $("#inputFechaNacimiento");

inputFechaNacimiento.parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true
});

inputFechaNacimiento.mask("00r00r0000", {
    translation: {
        'r': {
            pattern: /[\/]/,
            fallback: '/'
        },
        placeholder: "__/__/____"
    }
});

var mainCropper = $('#main-cropper');

var inputFileUploadProfilePic = $("#upload");

var basic = inicializarCroppie(mainCropper);

function inicializarCroppie(cropper) {
    var instance = cropper.croppie({
        viewport: { width: 150, height: 150 },
        boundary: { width: 150, height: 150 },
        showZoomer: false,
        enableExif: true
    });

    $(".cr-image").attr("src", "http://s3.amazonaws.com/37assets/svn/765-default-avatar.png").attr("width", 150);

    return instance;
}

function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
            $('#main-cropper').croppie('bind',
                {
                    url: e.target.result
                });
        }

        reader.readAsDataURL(input.files[0]);

        $(".file-btn span").text("Cambiar Foto");
    } else {
        mainCropper.croppie("destroy");
        basic = inicializarCroppie(mainCropper);
        $(".file-btn span").text("Subir Foto");
    }
}

$('.file-btn input').on('change', function () { readFile(this); });