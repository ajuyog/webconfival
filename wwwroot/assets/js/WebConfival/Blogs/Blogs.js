let parrafo = 1;
let imgGallery = 1;
let galeriaBlog = [];
function AddTitulo() {
    $("#invalid-titulo-blog").css("display", "none");
    $("#invalid-imagen-blog-principal").css("display", "none");

    var div = $("#contenido-blog-visualizar");
    var titulo = $("#titulo-blog").val();
    div.children().remove();
    div.append('<h2>' + titulo + '</h2><br>');

    var inputF = $("#imagen-blog");

    let imagenB = inputF[0].files
    var seccionTitulo = $("#seccion-uno-publicar");
    let seccionContenido = $("#seccion-contenido");

    if (titulo.length != 0 && imagenB.length != 0) {
        readURL(inputF[0]);
        $("#img-onchange").removeClass("hide-info");
        seccionTitulo.addClass("hide-info")
        seccionContenido.removeClass("hide-info");
    } else {
        if (titulo.length == 0) {
            $("#invalid-titulo-blog").css("display", "block");
        }
        if (imagenB.length == 0) {
            $("#invalid-imagen-blog-principal").css("display", "block");
        }
    }
};
function AddContenido() {
    let contenidoBlog = $("#contenido-blog");

    if (parrafo > 1) {
        let contenidoB = document.getElementById("parrafo" + (parrafo - 1));

        let cloneB = contenidoB.cloneNode(true);
        cloneB.id = "parrafo" + parrafo;
        contenidoBlog.append(cloneB);
    }
    let parrafoC = $("#parrafo" + parrafo);
    parrafoC.empty();
    var subtitulo = $("#subtitulo-blog").val();
    var parrafoG = $("#parrafo-general").val();
    if (subtitulo.length != 0) {
        parrafoC.append('<h3>' + subtitulo + '</h3>');
    }
    if (parrafoC.length != 0) {
        parrafoC.append('<p class="px-3 py-2 text-17 br-5">' + parrafoG + '</p>');
        $("#parrafo" + parrafo).removeClass("hide-info");
        parrafo++;

        $("#subtitulo-blog").val("");
        $("#parrafo-general").val("");
    }
};
function ShowFormGallery() {
    $("#seccion-uno").addClass("hide-info");
    $("#seccion-galeria").removeClass("hide-info");
};
function addImgGallery() {
    let inputImg = $("#imagen-g");
    let imagenB = inputImg[0].files
    galeriaBlog.push(imagenB[0]);
    if (imagenB.length == 0) {
        $("#invalid-imagen-g").css("display", "block")
    } else {
        $("#invalid-imagen-g").css("display", "none")
        let contenidoGallery = $("#lightgallery");
        if (imgGallery > 1) {
            let contenidoB = document.getElementById("lightgalleryLi" + (imgGallery - 1));
            let cloneB = contenidoB.cloneNode(true);

            cloneB.id = "lightgalleryLi" + imgGallery;
            contenidoGallery.append(cloneB);

            let contenidoBJ = $("#lightgalleryLi" + imgGallery);
            let imgNew = contenidoBJ.children().children();
            imgNew.attr('id', "imgGalleryBlog" + imgGallery);
        }
        var inputF = $("#imagen-g");
        let imagenB = inputF[0];
        readUrlGallery(imagenB, imgGallery);
        imgGallery++;
    }
};
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var dinamico = "";
            if (input.id == "imagen-blog") {
                dinamico = $('#blah');
            }
            dinamico.attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
};
function readUrlGallery(imagenB, id) {
    if (imagenB.files && imagenB.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var dinamico = $('#imgGalleryBlog' + id);
            dinamico.attr('src', e.target.result);

        }
        reader.readAsDataURL(imagenB.files[0]);
    }
};
function backContenido() {
    var seccionTitulo = $("#seccion-uno-publicar");
    let seccionContenido = $("#seccion-contenido");
    seccionTitulo.addClass("hide-info");
    seccionContenido.removeClass("hide-info");

    $("#seccion-uno").removeClass("hide-info");
    $("#seccion-galeria").addClass("hide-info");
};
function NextGallery() {
    // Remove Secciones
    $("#seccion-uno").addClass("hide-info");
    $("#seccion-galeria").addClass("hide-info");

    // Mostrar seccion categoria
    $("#seccion-categorias").removeClass("hide-info");
};

function Publicar() {
    $("#visualizador-campos").addClass("hide-info");
    $("#visualizar-blog-principal").addClass("hide-info");
    $("#loader-create-blog").removeClass("hide-info");

    let tituloBlog = null;
    if ($("#contenido-blog-visualizar").children("h2").length == 0) {
        tituloBlog = null;
    } else {
        tituloBlog = $("#contenido-blog-visualizar").children("h2").get(0).innerHTML;
    }

    let imagenPrincipalBlog = $("#imagen-blog")[0].files[0];
    let lstContenido = $("#contenido-blog").children();
    let contenidoBlog = "";
    $.each(lstContenido, function (element, index) {
        contenidoBlog = contenidoBlog + index.innerHTML;
    });
    let categoriaPrincipal = $("#categorias-principal").val();
    let arrayCategorias = $("#lst-categorias").val();
    let subcategorias = JSON.stringify(arrayCategorias);

    var formData = new FormData();
    formData.append('titulo', tituloBlog);
    formData.append('imagenPrincipal', imagenPrincipalBlog);
    formData.append('contenido', contenidoBlog);
    formData.append('categoria', categoriaPrincipal);
    formData.append('lstCategorias', subcategorias);
    $.each(galeriaBlog, function (element, index) {
        var dataTemporal = index;
        formData.append('lstGaleria', dataTemporal)
    });
    $.ajax({    
        type: "POST",
        url: '/Blog/Publicar',
        data: formData,
        cache: false,
        contentType: false,//stop jquery auto convert form type to default x-www-form-urlencoded
        processData: false,
        success: function (result) {
            if (result == "success") {
                $("#loader-create-blog").addClass("hide-info");
                $("#visualizar-blog-confirmar").removeClass("hide-info");
            }
            if (result == "campos null") {
                $("#campos-null").modal('show');
            }
            if (result == "error") {
                $("#api-error").modal('show');
            }

        },
        error: function () {
            $("#api-error").modal('show');
        }
    });

};

$("#categorias-principal").on("change", function () {
    var cartegoriaPrincipal = $("#categorias-principal").val();
    if (cartegoriaPrincipal.length > 0) {
        $("#seccion-btn-finalizar").removeClass("hide-info");
        var idCategoria = $("#categorias-principal").val();
        var lstCategorias = $("#lst-categorias").children();
        $.ajax({
            type: "GET",
            url: '/Blog/ConsultarCategorias',
            data: { id: idCategoria },
            success: function (result) {
                lstCategorias.remove();
                $("#lst-categorias").append('<option disabled value="">Seleccione..</option>')
                $.each(result, function (element, index) {
                    $("#lst-categorias").append('<option value="' + index.id + '">' + index.nombre + '</option>')
                });
            }
        });
    }
    if (cartegoriaPrincipal.length == 0) {
        $("#seccion-btn-finalizar").addClass("hide-info");
    }
});

