
$(document).ready(function () {
    var url = $("#frame").attr('src');
    $("#modaldemo8").on('hide.bs.modal', function () {
        $("#frame").attr('src', '');
    });
    $("#modaldemo8").on('show.bs.modal', function () {
        $("#frame").attr('src', url);
    });

    var url2 = $("#frame2").attr('src');
    $("#modaldemo10").on('hide.bs.modal', function () {
        $("#frame2").attr('src', '');
    });
    $("#modaldemo10").on('show.bs.modal', function () {
        $("#frame2").attr('src', url2);
    });

    var url3 = $("#frame3").attr('src');
    $("#modaldemo11").on('hide.bs.modal', function () {
        $("#frame3").attr('src', '');
    });
    $("#modaldemo11").on('show.bs.modal', function () {
        $("#frame3").attr('src', url3);
    });

});



