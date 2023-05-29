$(function () {
    var seccion = $("#cotizar-contact").get(0).innerHTML;
    if (seccion == "Contact") {
        window.location.hash = '#' + seccion;
    }
});

function widthConfival() {
    $(".slick-slide").css("width", "100%");
    $(".slick-track").css("width", "100%");
}
