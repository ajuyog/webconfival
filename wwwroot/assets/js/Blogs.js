function BlogsCategoria(data) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Blog/ConsultarPorCategoria',
        data: { id: data },
        success: function ( result ) {
            var div = $("#blogs-categoria") 
            div.children("div").remove();
            div.append('<div class="card border"><div class="card-header border-bottom"><h4 class="card-title header-family">Blogs por categoria</h4></div><div class="card-body"><div class="item-list add-confival-dos"><ul class="list-group mb-0 add-confival"></ul></div></div></div>');
            $.each(result, function (element, index) {
                if (result.length > count) {
                    count = count + 1;

                    $(".add-confival").append('<li class="list-group-item d-flex pb-4 pt-0 px-0 border-bottom-0"><img src="' + index.imgBlog + '" class="avatar br-5 avatar-lg me-3 my-auto" alt="avatar-img"><div><span class="d-block text-muted">' + index.categorias[0].nombre + '</span><a href="#" class="text-dark text-16 font-weight-semibold">' + index.titulo + '</a><small class="d-block text-gray">2 day ago</small></div></li>')
                }
            })
        }
    });
}