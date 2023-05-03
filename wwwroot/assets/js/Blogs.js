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
function ValidarFormulario() {
    var titulo = $("#titulo-blog").val();
    var contenido = $(".note-editable").children().get();
    var categorias = $("#categorias-publicar").val();
    var imgBlog = $("#img-blog-publicar");
    var nombre = $("#nombre-publicar");
    var primerApellido = $("#p-apellido-publicar");
    var segundoApellido = $("#s-apellido-publicar");
    var correo = $("#correo-publicar");
    var descripcionAutor = $("#descripcion-publicar");
    var avatarAutor = $("#avatar-publicar");
    if (titulo.length == 0) {
        $("#invalid-titulo-blog").css("display", "block");
    } else {
        $("#invalid-titulo-blog").css("display", "none");
    }
    var html = " ";
    var text = " ";
    if (contenido != null) {
        $.each(contenido, function (element, index) {
            html = html + index.innerHTML;
            text = text + index.innerText;
        })
    }
    if (text.length < 4000) {
        $("#invalid-summernote").css("display", "block");
    } else {
        $("#invalid-summernote").css("display", "none");
    }
    var lstCategoriasId = [];
    if (categorias != null) {
        $.each(categorias, function (element, index) {
            lstCategoriasId.push(index)
        })
    }
    if (lstCategoriasId.length == 0) {
        $("#invalid-categorias-publicar").css("display", "block");
    } else {
        $("#invalid-categorias-publicar").css("display", "none");

    }
    if (imgBlog.get(0).files.length  == 0) {
        $("#invalid-img-blog-publicar").css("display", "block");
    } else {
        $("#invalid-img-blog-publicar").css("display", "none");
    }
    if (nombre.val().length == 0) {
        $("#invalid-nombre-publicar").css("display", "block");
    } else {
        $("#invalid-nombre-publicar").css("display", "none");
    }
    if (primerApellido.val().length == 0) {
        $("#invalid-p-apellido-publicar").css("display", "block");
    } else {
        $("#invalid-p-apellido-publicar").css("display", "none");
    }
    if (segundoApellido.val().length == 0) {
        $("#invalid-s-apellido-publicar").css("display", "block");
    } else {
        $("#invalid-s-apellido-publicar").css("display", "none");
    }
    var correo = $("#correo-publicar").val();
    var validCorreo = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);
    if (validCorreo.test(correo) == false) {
        $("#invalid-correo-publicar").css("display", "block");
    } else {
        $("#invalid-correo-publicar").css("display", "none");
    }
    if (descripcionAutor.val().length < 200) {
        $("#invalid-descripcion-publicar").css("display", "block");
    } else {
        $("#invalid-descripcion-publicar").css("display", "none");
    }
    if (avatarAutor.get(0).files.length == 0) {
        $("#invalid-avatar-publicar").css("display", "block");
    } else {
        $("#invalid-avatar-publicar").css("display", "none");
    }


    

    
};

function PublicarBlog() {
    ValidarFormulario();
}