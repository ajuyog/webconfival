function SaveBanner() {
    var valid = ValidBanner();
        if (valid == 0) {
        var nombre = $("#banner-nombre").val();
        var url = $("#url-banner").val();
        var file = $("#file-banner").get(0).files[0];
        var form = new FormData();
        form.append("obj", file);
        form.append("name", nombre);
        form.append("URL", url);
        $.ajax({
            type: "POST",
            url: '/Banner/Save',
            data: form,
            cache: false,
            contentType: false,//stop jquery auto convert form type to default x-www-form-urlencoded
            processData: false,
            success: function (result) {
                if (result == true) {
                    $("#banner-success").css("display", "block");
                    $("#form-banner-principal").css("display", "none");
                }
            }
        });
    }
}

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