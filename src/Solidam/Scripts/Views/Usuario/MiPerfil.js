var divInvisibleRectangle = $("#divInvisibleRectangle");

$(window).on("load", function () {
    divInvisibleRectangle.height($("header").innerHeight());
});