function EditComment(id, comentario) {
    var modal = $("#approve-item");
    var contenido = $("#message-approve");
    contenido.children().remove();
    contenido.append('<p class="text-muted">Esta seguro de aprobar el comentario: ' + comentario + '</p>' +
        '<button aria-label="Close" class="btn btn-info pd-x-25" data-bs-dismiss="modal">Cancelar</button>' +
        '<button class="btn btn-success pd-x-25" onclick="ApproveComment(' + id + ')" style="margin-left: 15px;">Aprobar</button>');
    modal.modal("show");
}
function ApproveComment(data) {
    var blog = $("#blog-id").val();
    var modalConfirm = $("#approve-item");
    var modalError = $("#error-approve");
    $.ajax({
        type: "GET",
        url: '/Comentarios/ApproveComment',
        data: { id: data, idBlog: blog },
        success: function (result) {
            if (result == true) {
                location.reload();
            } else {
                modalConfirm.modal("hide");
                modalError.modal("show");
            }
        }
    });
}