function SeeGallery(data, data2) {
    var modal = $("#modal-gallery");
    var indicadores = $("#ol-indicators");
    var srcImagenes = $("#src-imagen");
    var titulo = $("#modal-header-galeria");
    var posicion = 0;
    var active = "";

    indicadores.children().remove();
    srcImagenes.children().remove();
    titulo.children().remove();

    titulo.append('<h5 class="modal-title" >' + data2 + '</h5>' +
        '<button  class="btn-close" data-bs-dismiss="modal" aria-label="Close">' +
        '<span aria-hidden="true">×</span>' +
        '</button');

    $.ajax({
        type: "GET",
        url: '/Blog/SeeGallery',
        data: { id: data },
        success: function (result) {
            $.each(result.galeria, function (element, index) {
                if (posicion == 0) {
                    active = "active";
                } else {
                    active = "";
                }
                indicadores.append('<li data-bs-target="#carousel-indicators" data-bs-slide-to="' + posicion + '" class="' + active + '"></li>');
                srcImagenes.append('<div class="carousel-item ' + active + '">' +
                    '<img class="d-block w-100 responsive-css-banner-principal" alt="" src="' + index + '" data-bs-holder-rendered="true">' +
                    '</div>')
                posicion = posicion + 1;
            })
        }
    });
    modal.modal("show");
};

function ApproveBlog(id, nombre) {
    var modal = $("#approve-blog");
    var contenido = $("#message-approve-blog");
    contenido.children().remove();
    contenido.append('<p class="text-muted">Esta seguro de aprobar la publicaciòn: ' + nombre + '</p>' +
        '<button aria-label="Close" class="btn btn-info pd-x-25" data-bs-dismiss="modal">Cancelar</button>' +
        '<button class="btn btn-success pd-x-25" onclick="Approve(' + id + ')" style="margin-left: 15px;">Aprobar</button>');
    modal.modal("show");
};

function Approve(data) {
    var modalConfirm = $("#approve-blog");
    var modalError = $("#error-approve-blog");
    $.ajax({
        type: "GET",
        url: '/Blog/Approve',
        data: { id: data },
        success: function (result) {
            if (result == true) {
                window.location.href = '/Blog/Edit'
            } else {
                modalConfirm.modal("hide");
                modalError.modal("show");
            }
        }
    });
}
