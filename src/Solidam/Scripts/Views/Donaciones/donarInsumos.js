$("#btnContinuarInsumos").click(function (event) {
    event.preventDefault();
    var validation = false;
    $.each($(".check"),
        function(index, value) {
            if ($(value).is(":checked")) {
                validation = true;
            }
        });
    if (validation) {
        $(".error").addClass("d-none");
        $("form").submit();
    } else
        $(".error").removeClass("d-none");
    //
});