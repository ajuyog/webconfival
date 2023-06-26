function SaveBannerStorage() {
    var valid = ValidBanner();
    if (valid == 0) {
        // Loader //
        $("#form-banner-principal").addClass("hide-info");
        $("#loader-confival").removeClass("hide-info");
        // Loader FIN //
        var nombre = $("#banner-nombre").val();
        var url = $("#url-banner").val();
        var file = $("#file-banner").get(0).files[0];
        var form = new FormData();
        form.append("obj", file);
        form.append("name", nombre);
        form.append("URL", url);
        $.ajax({
            type: "POST",
            url: '/Banner/SaveStorage',
            data: form,
            cache: false,
            contentType: false,//stop jquery auto convert form type to default x-www-form-urlencoded
            processData: false,
            success: function (result) {
                if (result == true) {
                    // Loader //
                    $("#loader-confival").addClass("hide-info");
                    $("#banner-success").removeClass("hide-info");

                } else {
                    $("#form-banner-principal").removeClass("hide-info");
                    $("#loader-confival").addClass("hide-info");
                    $("#api-error").modal('show');
                }
            },
            error: function () {
                $("#loader-confival").addClass("hide-info");
                $("#form-banner-principal").removeClass("hide-info");
                $("#api-error").modal('show');
            }
        });
    }
};

function ValidBanner() {
    var count = 0;
    var nombre = $("#banner-nombre").val();
    var url = $("#url-banner").val();
    var file = $("#file-banner").get(0).files[0];
    if (nombre.length == 0) {
        $("#invalid-banner-nombre").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-banner-nombre").css("display", "none");
    }
    if (url.length == 0) {
        $("#invalid-url-banner").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-url-banner").css("display", "none");
    }
    if (file == null) {
        $("#invalid-file-banner").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-file-banner").css("display", "none");
    }
    return count;
}