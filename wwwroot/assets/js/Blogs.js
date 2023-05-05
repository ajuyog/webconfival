function BlogsCategoria(data) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Blog/ConsultarPorCategoria',
        data: { id: data },
        success: function ( result ) {
            var div = $("#blogs-categoria") 
            div.children("div").remove();
            var name = result[0].categorias[0].nombre;
            div.append('<div class="card-confival-blogs border-confival-blogs"><div class="card-header border-bottom"><h4 class="card-title header-family">Principales blogs de ' + name + '</h4></div><div class="card-body"><div class="item-list add-confival-dos"><ul class="list-group mb-0 add-confival"></ul></div></div></div>');
            $.each(result, function (element, index) {
                if (result.length > count) {
                    count = count + 1;
                    $(".add-confival").append('<li class="list-group-item d-flex pb-4 pt-0 px-0 border-bottom-0"><img src="' + index.imgBlog + '" class="avatar br-5 avatar-lg me-3 my-auto" alt="avatar-img"><div><span class="d-block text-muted">' + index.categorias[0].nombre + '</span><a href="#" class="text-dark text-16 font-weight-semibold">' + index.titulo + '</a><small class="d-block text-gray">2 day ago</small></div></li>')
                }
            })
        }
    });
};

// --- BOTONES ADICIONAR PARRAFOS --- //
function AgregarBotones() {
    var titulo = $("#titulo-blog").val();
    var primerParrafoB = $("#primer-parrafo").val();
    var imagenB = $("#imagen-blog").get(0).files;
    var seccionBotonoes = $("#seccion-btn-publicar");
    if (titulo.length != 0 && primerParrafoB.length != 0 && imagenB.length != 0) {
        seccionBotonoes.removeClass("hide-info");
    }
};

function AgregarBotonesDos() {
    var subtitulo = $("#subtitulo-uno-blog").val();
    var segundoParrafo = $("#segundo-parrafo").val();
    var seccionBotonoesDos = $("#seccion-btn-publicar-dos");
    if (subtitulo.length != 0 && segundoParrafo.length != 0) {
        seccionBotonoesDos.removeClass("hide-info");
    }
};

function AgregarBotonesDosTextArea() {
    var segundoParrafoTextArea = $("#segundo-parrafo-textarea").val();
    var seccionBotonoesDos = $("#seccion-btn-publicar-dos");
    if (segundoParrafoTextArea.length != 0) {
        seccionBotonoesDos.removeClass("hide-info");
    }
};

function AgregarBotonesIterar() {
    var subtituloIterar = $("#subtitulo-iterar").val();
    var parrafoIterar = $("#parrafo-iterar").val();
    var seccionBotonesIterar = $("#seccion-btn-publicar-iterar");
    if (subtituloIterar.length != 0 && parrafoIterar != 0) {
        seccionBotonesIterar.removeClass("hide-info");
    }
};

function AgregarBotonesIterarTextArea() {
    var parrafoTextAreaIterar = $("#parrafo-iterar-textarea").val();
    var seccionBotonesIterar = $("#seccion-btn-publicar-iterar");
    if (parrafoTextAreaIterar.length != 0) {
        seccionBotonesIterar.removeClass("hide-info");
    }
};

// --- ONCHAGE INPUTPS --- //
$("#titulo-blog").on("change", function () {
    var div = $("#contenido-blog-visualizar");
    var titulo = $("#titulo-blog").val();
    div.children().remove();
    div.append('<h2>' + titulo + '</h2><br>');
    AgregarBotones();
});

$("#imagen-blog").on("change", function () {
    $("#img-onchange").removeClass("hide-info");
    readURL(this);
    AgregarBotones();
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
};

$("#primer-parrafo").on("change", function () {
    var divDos = $("#contenido-blog-visualizar-parrafo");
    divDos.children().remove();
    var primerParrafo = $("#primer-parrafo").val();
    divDos.append('<p>' + primerParrafo + '</p> ');
    AgregarBotones();
});

$("#subtitulo-uno-blog").on("change", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-dos");
    var subTituloUno = $("#subtitulo-uno-blog").val();
    if (subTituloUno.length != 0) {
        $("#invalid-segundo-parrafo").css("display", "none");
    }
    divTres.append('<h3>' + subTituloUno + '</h3><br>');
    AgregarBotonesDos();
});

$("#segundo-parrafo").on("change", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-dos");
    var segundoParrafo = $("#segundo-parrafo").val();
    var stPrimero = $("#subtitulo-uno-blog").val();

    if (stPrimero.length == 0) {
        $("#invalid-segundo-parrafo").css("display", "block");
        $("#segundo-parrafo").val("");
    } else {
        divTres.append('<p>' + segundoParrafo + '</p> ');
        AgregarBotonesDos();
    }
});

$("#segundo-parrafo-textarea").on("change", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-dos");
    var segundoParrafoTextArea = $("#segundo-parrafo-textarea").val();
    divTres.append('<p>' + segundoParrafoTextArea + '</p> ');
    AgregarBotonesDosTextArea();
});

$("#subtitulo-iterar").on("change", function () {
    var divIterar = $("#contenido-blog-visualizar-iterar");
    var subtituloIterar = $("#subtitulo-iterar").val();
    if (subtituloIterar.length != 0) {
        $("#invalid-parrafo-iterar").css("display", "none");
    }
    divIterar.append('<h3>' + subtituloIterar + '</h3><br>')
    AgregarBotonesIterar();
});

$("#parrafo-iterar").on("change", function () {
    var divIterar = $("#contenido-blog-visualizar-iterar");
    var parrafoIterar = $("#parrafo-iterar").val();
    var stPrimeroIterar = $("#subtitulo-iterar").val();
    if (stPrimeroIterar.length == 0) {
        $("#invalid-parrafo-iterar").css("display", "block");
        $("#parrafo-iterar").val("");
    } else {
        divIterar.append('<p>' + parrafoIterar + '</p>');
        AgregarBotonesIterar();
    }
});

$("#parrafo-iterar-textarea").on("change", function () {
    var divIterar = $("#contenido-blog-visualizar-iterar");
    var parrafoIterar = $("#parrafo-iterar-textarea").val();
    divIterar.append('<p>' + parrafoIterar + '</p>');
    AgregarBotonesIterarTextArea();
});

// --- BOTONES FINALIZAR --- //
function Publicar() {
    $("#seccion-uno-publicar").children().remove();
    $("#seccion-dos-publicar").children().remove();
    $("#seccion-btn-publicar").children().remove();
    $("#form-blog-create-success").append('<div class="alert alert-default pad-text" role="alert"><span class="alert-inner--icon me-2"><i class="fe fe-thumbs-up"></i></span><span class="alert-inner--text"><strong>Tu publicación esta siendo analisada</strong> Uno de nuestros colaboradores, se te notificara al correo electronico cuando sea aprovada !!!</span><div class="row"><div class="col-12"><a href="#PublicarBlog" class="btn btn-success" onclick="restartBlog()" style="margin-top:20px;">Quiero publicar otro Blog!!</a></div></div></div>')
};

function restartBlog() {
    location.reload(true);
};

// --- BUCLE ADICIONAR PARRAFOS --- //
function Continuar() {
    var seccionUno = $("#seccion-uno-publicar").children();
    var seccionBotonoes = $("#seccion-btn-publicar").children();
    var seccionDos = $("#seccion-dos-publicar");

    seccionUno.remove();
    seccionBotonoes.remove();
    seccionDos.removeClass("hide-info");
};

function AddSubtitulo() {
    var seccionDos = $("#seccion-dos-publicar");
    var seccionBotones = $("#seccion-btn-publicar");
    var seccionUno = $("#seccion-uno-publicar");

    seccionBotones.addClass("hide-info");
    seccionUno.addClass("hide-info");
    seccionDos.removeClass("hide-info");
};

function AddParrafo() {
    var seccionDos = $("#seccion-dos-publicar-textarea");
    var seccionBotones = $("#seccion-btn-publicar");
    var seccionUno = $("#seccion-uno-publicar");

    seccionUno.addClass("hide-info");
    seccionBotones.addClass("hide-info");
    seccionDos.removeClass("hide-info");
};

function AddSubtituloIterar() {
    var seccionIterar = $("#seccion-publicar-iterar");
    var seccionBotonesDos = $("#seccion-btn-publicar-dos");

    seccionIterar.removeClass("hide-info");
    seccionBotonesDos.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
};

function AddParrafoIterar() {
    var seccionIterarTextArea = $("#seccion-publicar-iterar-textarea");
    var seccionBotonesDos = $("#seccion-btn-publicar-dos");

    seccionIterarTextArea.removeClass("hide-info");
    seccionBotonesDos.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
};

function LimpiarSubtituloIterar() {
    var seccionBotonesIterar = $("#seccion-btn-publicar-iterar");
    var inputSubtitulo = $("#subtitulo-iterar");
    var textArea = $("#parrafo-iterar");
    var textAreaS = $("#parrafo-iterar-textarea");

    $("#seccion-publicar-iterar-textarea").addClass("hide-info");
    $("#seccion-publicar-iterar").removeClass("hide-info");

    inputSubtitulo.val("");
    textArea.val("");
    textAreaS.val("");
    seccionBotonesIterar.addClass("hide-info");
};

function LimpiarParrafoIterar() {
    var seccionBotonesIterar = $("#seccion-btn-publicar-iterar");
    var textAreaS = $("#parrafo-iterar-textarea");
    var inputSubtitulo = $("#subtitulo-iterar");
    var textArea = $("#parrafo-iterar");

    $("#seccion-publicar-iterar-textarea").removeClass("hide-info");
    $("#seccion-publicar-iterar").addClass("hide-info");

    seccionBotonesIterar.addClass("hide-info");
    textAreaS.val("");
    inputSubtitulo.val("");
    textArea.val("");

}


