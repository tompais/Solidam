$(function () {

    $(".causa").slice(0, 3).show();

    $("body").on('click touchstart', '.load-more', function (e) {

        e.preventDefault();

        $(".causa:hidden").slice(0, 3).slideDown();

        if ($(".causa:hidden").length == 0) {

            $(".load-more").css('visibility', 'hidden');
        }

        $('html,body').animate({
            scrollTop: $(this).offset().top
        }, 1000);

    });

});
