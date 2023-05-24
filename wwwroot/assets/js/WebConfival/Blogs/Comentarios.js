function EditarComentario(data) {
    $.ajax({
        type: "GET",
        url: '/Comentarios/Editar',
        data: { id: data },
        success: function (result) {
            var div = $("#model-detalles");
            div.replaceWith(result);
        }
    });

}