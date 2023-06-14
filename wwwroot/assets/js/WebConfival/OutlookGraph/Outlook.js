function DetalleCorreo(data) {
    $("#body").children().remove(),
    $("#body").append('<p>' + data + '</p>')
    $("#DetalleCorreo").modal('show');
}