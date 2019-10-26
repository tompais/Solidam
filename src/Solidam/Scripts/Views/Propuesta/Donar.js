$(".check").change(function () {
    if (this.checked) {
        $(this).parent().parent().find("input[type=number]").removeClass("d-none");
    } else {
        $(this).parent().parent().find("input[type=number]").addClass("d-none");
    }
});

$("#btnConfirmar").click(function () {
    var donaciones = [];
    $(".check:checked").each(function (index, value) {
        var donacionInsumo = {};
        donacionInsumo.idPropuestaDonacionInsumo = $(value).attr("don-id");
        donacionInsumo.cantidad = $(value).parent().parent().find("input[type=number]").val();
        donaciones.push(donacionInsumo);
    });
    $.ajax({
        url: window.pathCrearDonacionInsumo,
        data: { donaciones: donaciones },
        type: 'POST',
        success: function(data) {
            window.location.reload();
        }
    });
});