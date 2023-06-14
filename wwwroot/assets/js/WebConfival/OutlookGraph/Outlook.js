function DetalleCorreo(data, dataSubject) {

    $("#header-mail").children().remove();
    $("#header-mail").append('<h5 class="modal-title">' + dataSubject + '</h5><button class="btn-close" data-bs-dismiss="modal" aria-label="Close"> <span aria-hidden="true">×</span> </button>')


    $("#body-mail").children().remove();
    $("#body-mail").empty();
    $("#body-mail").append(data);
    $("#DetalleCorreo").modal('show');
}