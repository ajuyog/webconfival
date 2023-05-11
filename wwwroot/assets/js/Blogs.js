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
function AgregarBotonesTres() {
    var subtitulo = $("#subtitulo-tres-blog").val();
    var parrafo = $("#parrafo-tres-blog").val();
    var seccionBotonesTres = $("#seccion-btn-publicar-tres");
    if (subtitulo.length != 0 && parrafo != 0) {
        seccionBotonesTres.removeClass("hide-info");
    }
};
function AgregarBotonesTresTextArea() {
    var parrafo = $("#parrafo-tres-textarea").val();
    var seccionBotones = $("#seccion-btn-publicar-tres");
    if (parrafo.length != 0) {
        seccionBotones.removeClass("hide-info");
    }
};
function AgregarBotonesCuatro() {
    var subtitulo = $("#subtitulo-cuatro-blog").val();
    var parrafo = $("#parrafo-cuatro-blog").val();
    var botones = $("#seccion-btn-publicar-cuatro");
    if (subtitulo.length != 0 && parrafo.length != 0) {
        botones.removeClass("hide-info");
    }
};
function AgregarBotonesCuatroTextArea() {
    var parrafo = $("#parrafo-cuatro-textarea").val();
    var botones = $("#seccion-btn-publicar-cuatro");
    if (parrafo.length != 0) {
        botones.removeClass("hide-info");
    }
};
function IniciarGaleria() {
    var subtitulo = $("#subtitulo-cinco-blog").val();
    var parrafo = $("#parrafo-cinco-blog").val();
    var boton = $("#seccion-btn-publicar-cinco");
    if (subtitulo.length != 0 && parrafo.length != 0) {
        boton.removeClass("hide-info");
    }
};
function IniciarGaleriaTextArea() {
    var parrafo = $("#parrafo-cinco-textarea").val();
    var botones = $("#seccion-btn-publicar-cinco");
    if (parrafo.length != 0) {
        botones.removeClass("hide-info");
    }
};

// --- ONCHAGE INPUTPS --- //
/// --- seccion 1 --- ///
$("#titulo-blog").on("keyup", function () {
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
$("#primer-parrafo").on("keyup", function () {
    var divDos = $("#contenido-blog-visualizar-parrafo");
    divDos.children().remove();
    var primerParrafo = $("#primer-parrafo").val();
    divDos.append('<p>' + primerParrafo + '</p> ');
    AgregarBotones();
});

/// --- seccion 2 --- ///
$("#subtitulo-uno-blog").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-subtitulo-dos");
    var subTituloUno = $("#subtitulo-uno-blog").val();
    if (subTituloUno.length != 0) {
        $("#invalid-segundo-parrafo").css("display", "none");
    }
    divTres.children("h3").remove();
    divTres.append('<h3>' + subTituloUno + '</h3>');
    AgregarBotonesDos();
});
$("#segundo-parrafo").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-dos");
    var segundoParrafo = $("#segundo-parrafo").val();
    var stPrimero = $("#subtitulo-uno-blog").val();

    if (stPrimero.length == 0) {
        $("#invalid-segundo-parrafo").css("display", "block");
        $("#segundo-parrafo").val("");
    } else {
        divTres.children("p").remove();
        divTres.append('<p>' + segundoParrafo + '</p> ');
        AgregarBotonesDos();
    }
});
$("#segundo-parrafo-textarea").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-dos");
    var segundoParrafoTextArea = $("#segundo-parrafo-textarea").val();
    divTres.children("p").remove();
    divTres.append('<p>' + segundoParrafoTextArea + '</p> ');
    AgregarBotonesDosTextArea();
});

/// --- seccion 3 --- ///
$("#subtitulo-tres-blog").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-subtitulo-tres");
    var subtituloTres = $("#subtitulo-tres-blog").val();
    if (subtituloTres.length != 0) {
        $("#invalid-parrafo-tres").css("display", "none");
    }
    divTres.children("h3").remove();
    divTres.append('<h3>' + subtituloTres + '</h3>')
    AgregarBotonesTres();
});
$("#parrafo-tres-blog").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-tres");
    var parrafoTres = $("#parrafo-tres-blog").val();
    var stPrimero = $("#subtitulo-tres-blog").val();
    if (stPrimero.length == 0) {
        $("#invalid-parrafo-tres").css("display", "block");
        $("#parrafo-tres-blog").val("");
    } else {
        divTres.children("p").remove();
        divTres.append('<p>' + parrafoTres + '</p>');
        AgregarBotonesTres();
    }
});
$("#parrafo-tres-textarea").on("keyup", function () {
    var divTres = $("#contenido-blog-visualizar-parrafo-tres");
    var parrafo = $("#parrafo-tres-textarea").val();
    divTres.children("p").remove();
    divTres.append('<p>' + parrafo + '</p>');
    AgregarBotonesTresTextArea();
});

/// --- seccion 4 --- ///
$("#subtitulo-cuatro-blog").on("keyup", function () {
    var divCuatro = $("#contenido-blog-visualizar-subtitulo-cuatro");
    var subtitulo = $("#subtitulo-cuatro-blog").val();
    if (subtitulo.length != 0) {
        $("#invalid-parrafo-cuatro").css("display", "none");
    }
    divCuatro.children("h3").remove();
    divCuatro.append('<h3>' + subtitulo + '</h3>')
    AgregarBotonesCuatro();
});
$("#parrafo-cuatro-blog").on("keyup", function () {
    var divCuatro = $("#contenido-blog-visualizar-parrafo-cuatro");
    var parrafo = $("#parrafo-cuatro-blog").val();
    var subtituloInvalid = $("#subtitulo-cuatro-blog").val()
    if (subtituloInvalid.length == 0) {
        $("#invalid-parrafo-cuatro").css("display", "block");
        $("#parrafo-cuatro-blog").val("");
    }
    divCuatro.children("p").remove();
    divCuatro.append('<p>' + parrafo + '<p>')
    AgregarBotonesCuatro();

});
$("#parrafo-cuatro-textarea").on("keyup", function () {
    var divCuatro = $("#contenido-blog-visualizar-parrafo-cuatro");
    var parrafo = $("#parrafo-cuatro-textarea").val();
    divCuatro.children("p").remove();
    divCuatro.append('<p>' + parrafo + '</p>');
    AgregarBotonesCuatroTextArea();
});

/// --- seccion 5 --- ///
$("#subtitulo-cinco-blog").on("keyup", function () {
    var divCinco = $("#contenido-blog-visualizar-subtitulo-quinto");
    var subtitulo = $("#subtitulo-cinco-blog").val();
    if (subtitulo.length != 0) {
        $("#invalid-parrafo-cinco").css("display", "none");
    }
    divCinco.children("h3").remove();
    divCinco.append('<h3>' + subtitulo + '</h3>');
    IniciarGaleria();
});
$("#parrafo-cinco-blog").on("keyup", function () {
    var divCinco = $("#contenido-blog-visualizar-parrafo-quinto");
    var parrafo = $("#parrafo-cinco-blog").val();
    var subtituloInvalid = $("#subtitulo-cinco-blog").val()
    if (subtituloInvalid.length == 0) {
        $("#invalid-parrafo-cinco").css("display", "block");
        $("#parrafo-cinco-blog").val("");
    }
    divCinco.children("p").remove();
    divCinco.append('<p>' + parrafo + '<p>');
    IniciarGaleria();
});
$("#parrafo-cinco-textarea").on("keyup", function () {
    var divCinco = $("#contenido-blog-visualizar-parrafo-quinto");
    var parrafo = $("#parrafo-cinco-textarea").val();
    divCinco.children("p").remove();
    divCinco.append('<p>' + parrafo + '</p>');
    IniciarGaleriaTextArea();
});

/// --- seccion 6 --- ///
$("#imagen-g-uno").on("change", function () {
    var file = $("#imagen-g-uno").get(0).files;
    var imagenUno = $("#seccion-btn-imagen-uno-g");
    if (file.length > 0) {
        imagenUno.removeClass("hide-info");
    } 
});

/// --- seccion 7 --- ///
$("#imagen-g-dos").on("change", function () {
    var file = $("#imagen-g-dos").get(0).files;
    var imagenDos = $("#seccion-btn-imagen-dos-g");
    if (file.length > 0) {
        imagenDos.removeClass("hide-info");
    }
});

/// --- seccion 8 --- ///
$("#imagen-g-tres").on("change", function () {
    var file = $("#imagen-g-tres").get(0).files;
    var imagenTres = $("#seccion-btn-imagen-tres-g");
    if (file.length > 0) {
        imagenTres.removeClass("hide-info");
    }
});

/// --- seccion 8 --- ///
$("#imagen-g-cuatro").on("change", function () {
    var file = $("#imagen-g-cuatro").get(0).files;
    var imagenCuatro = $("#seccion-btn-imagen-cuatro-g");
    if (file.length > 0) {
        imagenCuatro.removeClass("hide-info");
    }
});

// --- BOTONES FINALIZAR --- //
function Publicar() {
    var visualizador = $("#visualizar-blog-principal");
    var visualizadorConfirmar = $("#visualizar-blog-confirmar");
    var visualizadorCampos = $("#visualizador-campos");

    visualizador.addClass("hide-info");
    visualizadorCampos.addClass("hide-info");
    visualizadorConfirmar.removeClass("hide-info");
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
function AddSubtituloTres() {
    var seccionTres = $("#seccion-tres-publicar");
    var seccionBotonesDos = $("#seccion-btn-publicar-dos");

    seccionTres.removeClass("hide-info");
    seccionBotonesDos.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
};
function AddParrafoTres() {
    var seccionIterarTextArea = $("#seccion-publicar-tres-textarea");
    var seccionBotonesDos = $("#seccion-btn-publicar-dos");

    seccionIterarTextArea.removeClass("hide-info");
    seccionBotonesDos.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
};
function AddSubtituloCuatro() {
    var seccionCuatro = $("#seccion-cuatro-publicar");
    var seccionBotonesTres = $("#seccion-btn-publicar-tres");

    seccionCuatro.removeClass("hide-info");
    seccionBotonesTres.addClass("hide-info");

    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
    $("#seccion-publicar-tres-textarea").addClass("hide-info");
    $("#seccion-tres-publicar").addClass("hide-info");
};
function AddParrafoCuatro() {
    var seccion = $("#seccion-publicar-cuatro-textarea");
    var seccionBotones = $("#seccion-btn-publicar-tres");

    seccion.removeClass("hide-info");
    seccionBotones.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
    $("#seccion-publicar-tres-textarea").addClass("hide-info");
    $("#seccion-tres-publicar").addClass("hide-info");
};
function AddSubtituloQuinto() {
    var seccionCinco = $("#seccion-cinco-publicar");
    var seccionBotonesCuatro = $("#seccion-btn-publicar-cuatro");

    seccionCinco.removeClass("hide-info");
    seccionBotonesCuatro.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
    $("#seccion-publicar-tres-textarea").addClass("hide-info");
    $("#seccion-tres-publicar").addClass("hide-info");
    $("#seccion-publicar-cuatro-textarea").addClass("hide-info");
    $("#seccion-cuatro-publicar").addClass("hide-info");
};
function AddParrafoQuinto() {
    var seccion = $("#seccion-publicar-cinco-textarea");
    var seccionBotones = $("#seccion-btn-publicar-cuatro");

    seccion.removeClass("hide-info");
    seccionBotones.addClass("hide-info");
    // Limpio todas las secciones anteriores //
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
    $("#seccion-publicar-tres-textarea").addClass("hide-info");
    $("#seccion-tres-publicar").addClass("hide-info");
    $("#seccion-publicar-cuatro-textarea").addClass("hide-info");
    $("#seccion-cuatro-publicar").addClass("hide-info");
};


function AddGallery() {
    var galeria = $("#seccion-galeria-uno");
    var botones = $("#seccion-btn-publicar-cinco");
    var parrafoCinco = $("#seccion-cinco-publicar");
    var parrafoCincoTexArea = $("#seccion-publicar-cinco-textarea")

    galeria.removeClass("hide-info");
    botones.addClass("hide-info");
    parrafoCinco.addClass("hide-info");
    parrafoCincoTexArea.addClass("hide-info");

};
function AddGallery2() {
    var imagenAnterior = $("#imagen-g-uno").get(0).files;
    if (imagenAnterior.length == 0) {
        $("#invalid-imagen-g-uno").css("display", "block");
    } else {
        var galeria = $("#seccion-galeria-dos");
        var botones = $("#seccion-btn-imagen-uno-g");
        var seccionImagenAnterior = $("#seccion-galeria-uno");

        galeria.removeClass("hide-info");
        botones.addClass("hide-info");
        seccionImagenAnterior.addClass("hide-info");
    }
};
function AddGallery3() {
    var imagenAnterior = $("#imagen-g-dos").get(0).files;
    if (imagenAnterior.length == 0) {
        $("#invalid-imagen-g-dos").css("display", "block");
    } else {
        var galeria = $("#seccion-galeria-tres");
        var botones = $("#seccion-btn-imagen-dos-g");
        var seccionImagenAnterior = $("#seccion-galeria-dos");

        galeria.removeClass("hide-info");
        botones.addClass("hide-info");
        seccionImagenAnterior.addClass("hide-info");
    }
};
function AddGallery4() {
    var imagenAnterior = $("#imagen-g-tres").get(0).files;
    if (imagenAnterior.length == 0) {
        $("#invalid-imagen-g-tres").css("display", "block");
    } else {
        var galeria = $("#seccion-galeria-cuatro");
        var botones = $("#seccion-btn-imagen-tres-g");
        var seccionImagenAnterior = $("#seccion-galeria-tres");

        galeria.removeClass("hide-info");
        botones.addClass("hide-info");
        seccionImagenAnterior.addClass("hide-info");
    }
};
function AddGallery5() {
    var imagenAnterior = $("#imagen-g-cuatro").get(0).files;
    if (imagenAnterior.length == 0) {
        $("#invalid-imagen-g-cuatro").css("display", "block");
    } else {
        var categorias = $("#seccion-categorias");
        var botones = $("#seccion-btn-imagen-cuatro-g");
        var seccionImagenAnterior = $("#seccion-galeria-cuatro");

        categorias.removeClass("hide-info");
        botones.addClass("hide-info");
        seccionImagenAnterior.addClass("hide-info");
    }
};

function NextGallery() {
    // Remove Secciones
    $("#seccion-uno-publicar").addClass("hide-info");
    $("#seccion-dos-publicar").addClass("hide-info");
    $("#seccion-dos-publicar-textarea").addClass("hide-info");
    $("#seccion-tres-publicar").addClass("hide-info");
    $("#seccion-publicar-tres-textarea").addClass("hide-info");
    $("#seccion-cuatro-publicar").addClass("hide-info");
    $("#seccion-publicar-cuatro-textarea").addClass("hide-info");
    $("#seccion-cinco-publicar").addClass("hide-info");
    $("#seccion-publicar-cinco-textarea").addClass("hide-info");

    // Remove btn Secciones
    $("#seccion-btn-publicar").addClass("hide-info");
    $("#seccion-btn-publicar-dos").addClass("hide-info");
    $("#seccion-btn-publicar-tres").addClass("hide-info");
    $("#seccion-btn-publicar-cuatro").addClass("hide-info");

    // Mostrar seccion uno de galeria
    $("#seccion-btn-publicar-cinco").removeClass("hide-info");

};
function NextCategoria() {
    // Remove Secciones
    $("#seccion-galeria-uno").addClass("hide-info");
    $("#seccion-galeria-dos").addClass("hide-info");
    $("#seccion-galeria-tres").addClass("hide-info");
    $("#seccion-galeria-cuatro").addClass("hide-info");

    // Remove btn Secciones
    $("#seccion-btn-publicar-cinco").addClass("hide-info");
    $("#seccion-btn-imagen-uno-g").addClass("hide-info");
    $("#seccion-btn-imagen-dos-g").addClass("hide-info");
    $("#seccion-btn-imagen-tres-g").addClass("hide-info");

    // Mostrar seccion categoria
    $("#seccion-categorias").removeClass("hide-info");




}