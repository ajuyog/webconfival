function SendComment(data) {
    var valid = Valid();
    if (valid > 0) {
        var idBlog = data;
        // Debo hacer una funcion Ajax para crear un comentario en DB


    }

};
function Reply(data) {
    var newFrom = $("#" + data.toString());
    var formsComment = $(".cancel-form-comment");
    var buttonReply = $(".btn-form-comment");
    $.each(formsComment, function (element, index) {
        if (index.children.length > 0) {
            let item = $(index).children("div");
            $(index).removeClass("card-respuesta-comentario");
            item.remove();
        }
    })
    $.each(buttonReply, function (element, index) {
        $(index).css("display", "block");
    })
    newFrom.append('<div class="row row-xs form-group-wrapper"><h4 class="card-title">Envia una respuesta a este comentario...</h4><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="nombre" placeholder="Nombre" type="text"><label for="name" class="form-label mb-1">Nombre</label></div><div class="invalid-feedback" id="invalid-nombre">El campo nombre es requerido</div></div><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="profesion" placeholder="Profesión" type="text"><label for="mail" class="form-label mb-1">Profesión</label></div><div class="invalid-feedback" id="invalid-profesion">El campo profesion es requerido</div></div><div class="col-md-12 mt-2"><div class= "main-form-group"><textarea name="message" id="comentario" class="form-control text-area border-0" placeholder="Comentario" rows="3"></textarea><label for="message" class="form-label mb-1">Comentario</label></div><div class="invalid-feedback" id="invalid-comentario">El campo comentario es requerido</div></div><div class="col-md-12 my-2"><button class="btn btn-primary text-white float-end" onclick="EnviarRespuesta(' + data + ')">Enviar</button></div><div class="col-md-12 my-2"><button class="btn btn-danger text-white float-end" onclick="Cancelar(' + data + ')">Cancelar !!</button></div></div>');
    newFrom.addClass("card-respuesta-comentario");
    newFrom.siblings(".boton-edit").children("span").children("a").css("display", "none");
};
function ValidarFormulario() {
    var nombre = $("#nombre").val();
    var profesion = $("#profesion").val();
    var comentario = $("#comentario").val();
    var count = 0;
    if (nombre.length == 0) {
        $("#invalid-nombre").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-nombre").css("display", "none");
    }
    if (profesion.length == 0) {
        $("#invalid-profesion").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-profesion").css("display", "none");
    }
    if (comentario.length == 0) {
        $("#invalid-comentario").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-comentario").css("display", "none");
    }
    return count;
};
function Cancelar(data) {
    var newFrom = $("#" + data.toString());
    newFrom.removeClass("card-respuesta-comentario");
    newFrom.siblings(".boton-edit").children("span").children("a").css("display", "block");
    newFrom.children("div").remove();
};
function responderDos(data, dataDos) {
    var newFrom = $("#" + data.toString() + "s" + dataDos.toString());
    var formsComment = $(".cancel-form-comment");
    var buttonReply = $(".btn-form-comment");
    $.each(formsComment, function (element, index) {
        if (index.children.length > 0) {
            let item = $(index).children("div");
            $(index).removeClass("card-respuesta-comentario");
            item.remove();
        }
    })
    $.each(buttonReply, function (element, index) {
        $(index).css("display", "block");
    })
    newFrom.append('<div class="row row-xs form-group-wrapper"><h4 class="card-title">Envia una respuesta a este comentario...</h4><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="nombre" placeholder="Nombre" type="text"><label for="name" class="form-label mb-1">Nombre</label></div><div class="invalid-feedback" id="invalid-nombre">El campo nombre es requerido</div></div><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="profesion" placeholder="Profesión" type="text"><label for="mail" class="form-label mb-1">Profesión</label></div><div class="invalid-feedback" id="invalid-profesion">El campo profesion es requerido</div></div><div class="col-md-12 mt-2"><div class= "main-form-group"><textarea name="message" id="comentario" class="form-control text-area border-0" placeholder="Comentario" rows="3"></textarea><label for="message" class="form-label mb-1">Comentario</label></div><div class="invalid-feedback" id="invalid-comentario">El campo comentario es requerido</div></div><div class="col-md-12 my-2"><button class="btn btn-primary text-white float-end" onclick="EnviarRespuesta(' + data + ')">Enviar</button></div><div class="col-md-12 my-2"><button class="btn btn-danger text-white float-end" onclick="CancelarDos(' + data.toString() + ', ' + dataDos.toString() + ')">Cancelar !!</button></div></div>');
    newFrom.addClass("card-respuesta-comentario");
    newFrom.siblings(".boton-edit-2").children("div").children("span").children("a").css("display", "none");
};
function CancelarDos(data, dataDos) {
    var newFrom = $("#" + data.toString() + "s" + dataDos.toString());
    newFrom.removeClass("card-respuesta-comentario");
    newFrom.siblings(".boton-edit-2").children("div").children("span").children("a").css("display", "block");
    newFrom.children("div").remove();
};
function EnviarRespuesta(data) {
    var validacion = ValidarFormulario();
    if (validacion == 0) {
        var newFrom = $("#" + data.toString());
        newFrom.children("div").remove();
    }
};

function TopCategoriaB(idBlog, nombre) {
    $.ajax({
        type: "GET",
        url: '/Blog/TopCategoria',
        data: { id: idBlog },
        success: function (result) {
            if (result != null) {
                $("#list-blogs").children().remove();
                $.each(result, function (element, index) {
                    $("#list-blogs").append('<ul class="list-group mb-0">' +
                        '<li class="list-group-item d-flex pb-4 pt-0 px-0 border-bottom-0">' +
                        '<img src="' + index.imagen + '" class="avatar br-5 avatar-lg me-3 my-auto" alt="avatar-img">' +
                        '<div> <span class="d-block text-muted">Categoria: ' + nombre + '</span> ' +
                        '<a href="/Blog/GetById/' + index.id + '" class="text-dark text-16 font-weight-semibold">' + index.titulo + '</a> ' +
                        '</div> ' +
                        '</li>' +
                        '</ul>'

                    );
                })
                $("#blogs-categoria").removeClass("hide-info");
            } else {
                console.log("fallo 'TopCategoriaB'");
            }
        },
        error: function () {
            console.log("fallo 'TopCategoriaB'");
        }
    });
}

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



// -- UTILIDAD -- //
function Valid() {
    count = 0;
    var nombre = $("#comentario-nombre").val();
    var profesion = $("#comentario-profesion").val();
    var comentario = $("#comentario-comentario").val();
    if (nombre.length == 0) {
        $("#invalid-comentario-nombre").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-comentario-nombre").css("display", "none");
    }
    if (profesion.length == 0) {
        $("#invalid-comentario-profesion").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-comentario-profesion").css("display", "none");
    }
    if (comentario.length == 0) {
        $("#invalid-comentario-comentario").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-comentario-comentario").css("display", "none");
    }
};