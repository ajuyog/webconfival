function SaveBanner() {
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
        success: function () {
            console.log("ok")
        }
    });

}