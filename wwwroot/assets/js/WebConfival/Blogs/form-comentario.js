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
function EnviarComentario() {
    var nombre = $("#comentario-nombre").val();
    var profesion = $("#comentario-profesion").val();
    var comentario = $("#comentario-comentario").val();
    if (nombre.length == 0) {
        $("#invalid-comentario-nombre").css("display", "block");
    } else {
        $("#invalid-comentario-nombre").css("display", "none");
    }
    if (profesion.length == 0) {
        $("#invalid-comentario-profesion").css("display", "block");
    } else {
        $("#invalid-comentario-profesion").css("display", "none");
    }
    if (comentario.length == 0) {
        $("#invalid-comentario-comentario").css("display", "block");
    } else {
        $("#invalid-comentario-comentario").css("display", "none");
    }
};
function responder(data) {
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
function Cancelar(data) {
    var newFrom = $("#" + data.toString());
    newFrom.removeClass("card-respuesta-comentario");
    newFrom.siblings(".boton-edit").children("span").children("a").css("display", "block");
    newFrom.children("div").remove();
}
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
}
function EnviarRespuesta(data) {
    var validacion = ValidarFormulario();
    if (validacion == 0) {
        var newFrom = $("#" + data.toString());
        newFrom.children("div").remove();
    }
}