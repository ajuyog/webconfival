function BannerInicioSuperior() {
    var valid = Valid();
    if (valid == 0) {
        // Loader //
        $("#form-banner").addClass("hide-info");
        $("#loader-confival").removeClass("hide-info");
        // Loader FIN //
        var file = $("#file-banner").get(0).files[0];
        var form = new FormData();
        form.append("obj", file);
        $.ajax({
            type: "POST",
            url: '/Banner/InicioSuperior',
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
                    $("#form-banner").removeClass("hide-info");
                    $("#loader-confival").addClass("hide-info");
                    $("#api-error").modal('show');
                }
            },
            error: function () {
                $("#loader-confival").addClass("hide-info");
                $("#form-banner").removeClass("hide-info");
                $("#api-error").modal('show');
            }
        });
    }
};
function BannerContactoSuperior() {
    var valid = Valid();
    if (valid == 0) {
        // Loader //
        $("#form-banner").addClass("hide-info");
        $("#loader-confival").removeClass("hide-info");
        // Loader FIN //
        var file = $("#file-banner").get(0).files[0];
        var form = new FormData();
        form.append("obj", file);
        $.ajax({
            type: "POST",
            url: '/Banner/ContactoSuperior',
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
                    $("#form-banner").removeClass("hide-info");
                    $("#loader-confival").addClass("hide-info");
                    $("#api-error").modal('show');
                }
            },
            error: function () {
                $("#loader-confival").addClass("hide-info");
                $("#form-banner").removeClass("hide-info");
                $("#api-error").modal('show');
            }
        });
    }
};
function Valid() {
    var count = 0;
    var file = $("#file-banner").get(0).files[0];
    var height = "";
    var width = "";
    var result = $("#file-banner").get(0);
    $.each(result, function (element, index) {
        height = index.dropify.file.height;
        width = index.dropify.file.width;
        return false;
    });
    if (file == null) {
        $("#invalid-file-banner").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-file-banner").css("display", "none");
    }
    if (height != 524 || width != 1920) {
        $("#invalid-file-banner-hw").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-file-banner-hw").css("display", "none");
    }
    return count;
}