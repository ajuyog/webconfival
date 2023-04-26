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
    var validacion = ValidarFormulario();
}
function responder(data) {
    var autor = $("#Autor").val();
    var newFrom = $("#" + data.toString());
    newFrom.append('<div class="row row-xs form-group-wrapper"><h3 class="card-title">Envia una respuesta al comentario de:    ' + autor + '</h3><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="nombre" placeholder="Nombre" type="text"><label for="name" class="form-label mb-1">Nombre</label></div><div class="invalid-feedback" id="invalid-nombre">El campo nombre es requerido</div></div><div class="col-md-6"><div class="main-form-group my-1"><input class="form-control border-0" id="profesion" placeholder="Profesión" type="text"><label for="mail" class="form-label mb-1">Profesión</label></div><div class="invalid-feedback" id="invalid-profesion">El campo profesion es requerido</div></div><div class="col-md-12 mt-2"><div class= "main-form-group"><textarea name="message" id="comentario" class="form-control text-area border-0" placeholder="Comentario" rows="3"></textarea><label for="message" class="form-label mb-1">Comentario</label></div><div class="invalid-feedback" id="invalid-comentario">El campo comentario es requerido</div></div><div class="col-md-12 my-2"><button class="btn btn-primary text-white float-end" onclick="EnviarRespuesta(' + data + ')">Enviar</button></div><div class="col-md-12 my-2"><button class="btn btn-danger text-white float-end" onclick="Cancelar(' + data + ')">Cancelar !!</button></div></div>');
    newFrom.addClass("card-respuesta-comentario");
    $("#main-form-comentario").addClass("hide-info");
};
function EnviarRespuesta(data) {
    var validacion = ValidarFormulario();
    if (validacion == 0) {
        var newFrom = $("#" + data.toString());
        $("#main-form-comentario").removeClass("hide-info");
        newFrom.children("div").remove();
    }

};
function Cancelar(data) {
    var newFrom = $("#" + data.toString());
    $("#main-form-comentario").removeClass("hide-info");
    newFrom.removeClass("card-respuesta-comentario");
    newFrom.children("div").remove();
}