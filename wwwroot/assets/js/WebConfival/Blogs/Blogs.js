let parrafo = 1;
let imgGallery = 1;
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
}
function AddContenido() {
    let contenidoBlog = $("#contenido-blog");

    if (parrafo > 1) {
        let contenidoB = document.getElementById("parrafo" + (parrafo - 1));
        console.log(contenidoB);

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
        parrafoC.append('<p>' + parrafoG + '</p>');
        $("#parrafo" + parrafo).removeClass("hide-info");
        parrafo++;

        $("#subtitulo-blog").val("");
        $("#parrafo-general").val("");
    }

}

function ShowFormGallery() {
    $("#seccion-uno").addClass("hide-info");
    $("#seccion-galeria").removeClass("hide-info");
}

function addImgGallery() {
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
}

function backContenido() {
    var seccionTitulo = $("#seccion-uno-publicar");
    let seccionContenido = $("#seccion-contenido");
    seccionTitulo.addClass("hide-info");
    seccionContenido.removeClass("hide-info");

    $("#seccion-uno").removeClass("hide-info");
    $("#seccion-galeria").addClass("hide-info");
}

function NextCategoria() {
    // Remove Secciones
    $("#seccion-uno").addClass("hide-info");
    $("#seccion-galeria").addClass("hide-info");


    // Mostrar seccion categoria
    $("#seccion-categorias").removeClass("hide-info");




};