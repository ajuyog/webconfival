
$(document).ready(function () {
    var url = $("#frame").attr('src');
    $("#modalcarousel0").on('hide.bs.modal', function () {
        $("#frame").attr('src', '');
    });
    $("#modalcarousel0").on('show.bs.modal', function () {
        $("#frame").attr('src', url);
    });

    var url2 = $("#frame2").attr('src');
    $("#modalcarousel1").on('hide.bs.modal', function () {
        $("#frame2").attr('src', '');
    });
    $("#modalcarousel1").on('show.bs.modal', function () {
        $("#frame2").attr('src', url2);
    });

    var url3 = $("#frame3").attr('src');
    $("#modalcarousel2").on('hide.bs.modal', function () {
        $("#frame3").attr('src', '');
    });
    $("#modalcarousel2").on('show.bs.modal', function () {
        $("#frame3").attr('src', url3);
    });

});



