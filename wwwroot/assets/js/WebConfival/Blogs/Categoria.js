function CrearCategoria() {
    var nombreCorregido = CorregirNombreCategoria();
    var valid = ValidCategoria();
    var count = 0;
    if (valid == 0) {
        $.ajax({
            type: "GET",
            url: '/Blog/ConsultarCategorias',
            success: function (result) {
                console.log(result);
                $.each(result, function (element, index) {
                    if (index.nombre == nombreCorregido) {
                        count = count + 1;
                        $("#invalid-nombre-categoria-existe").css("display", "block");
                        return false
                    }
                })
                if (count == 0) {
                    $("#invalid-nombre-categoria-existe").css("display", "none");
                    GuardarCategoria(nombreCorregido);
                }
            }
        });
    }
};
function GuardarCategoria(nombreCorregido){
    $.ajax({
        type: "GET",
        url: '/Categoria/SaveCategoria',
        data: { nombre: nombreCorregido },
        success: function (result) {
            if (result == true) {
                $("#form-categoria").addClass("hide-info");
                $("#categoria-guardada").removeClass("hide-info");
            } else {
                $("#api-error").modal('show');
            }
        }
    });
};
function CorregirNombreCategoria() {
    var nombre = $("#nombre-categoria").val();
    var primeraLetra = nombre.substring(0, 1).toUpperCase();
    var nombreSinPrimeraLetra = nombre.substring(1).toLowerCase();
    return primeraLetra + nombreSinPrimeraLetra;
};
function ValidCategoria() {
    var nombre = $("#nombre-categoria").val();
    var count = 0;
    if (nombre.length == 0) {
        $("#invalid-nombre-categoria").css("display", "block");
        $("#invalid-nombre-categoria-existe").css("display", "none");
        count = count + 1
    } else {
        $("#invalid-nombre-categoria").css("display", "none");
    }
    return count;
};
function restartCategoria() {
    location.reload(true);
};
function EditarCategoria() {
    var nombreCorregido = CorregirNombreCategoria();
    var valid = ValidCategoria();
    var count = 0;
    if (valid == 0) {
        $.ajax({
            type: "GET",
            url: '/Blog/ConsultarCategorias',
            success: function (result) {
                console.log(result);
                $.each(result, function (element, index) {
                    if (index.nombre == nombreCorregido) {
                        count = count + 1;
                        $("#invalid-nombre-categoria-existe").css("display", "block");
                        $("#invalid-nombre-categoria").css("display", "none");
                        return false
                    }
                })
                if (count == 0) {
                    $("#invalid-nombre-categoria-existe").css("display", "none");
                    EditarCategoriaDB(nombreCorregido);
                    
                }
            }
        });
    }
};
function EditarCategoriaDB(nombreCorregido) {
    var idCategoria = $("#id-categoria").val();
    $.ajax({
        type: "GET",
        url: '/Categoria/EditCategoryDB',
        data: { nombre: nombreCorregido, id: idCategoria},
        success: function (result) {
            if (result == true) {
                $("#form-categoria").addClass("hide-info");
                $("#categoria-guardada").removeClass("hide-info");
            } else {
                $("#api-error").modal('show');
            }
        }
    });
}



