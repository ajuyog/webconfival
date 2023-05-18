function CrearCategoria() {
    var nombreCorregido = CorregirNombre();
    var valid = Valid();
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
                    $("#form-categoria").addClass("hide-info");
                    $("#categoria-guardada").removeClass("hide-info");
                }
            }
        });
    }
};



function GuardarCategoria(nombreCorregido){
    $.ajax({
        type: "GET",
        url: '/Blog/SaveCategoria',
        data: { nombre: nombreCorregido },
        success: function (result) {
            if (result == true) {
                console.log("se guardo");
            } else {
                console.log("fallo");
            }
        }
    });
};
function CorregirNombre() {
    var nombre = $("#nombre-categoria").val();
    var primeraLetra = nombre.substring(0, 1).toUpperCase();
    var nombreSinPrimeraLetra = nombre.substring(1).toLowerCase();
    return primeraLetra + nombreSinPrimeraLetra;
};
function Valid() {
    var nombre = $("#nombre-categoria").val();
    var count = 0;
    if (nombre.length == 0) {
        $("#invalid-nombre-categoria").css("display", "block");
        count = count + 1
    } else {
        $("#invalid-nombre-categoria").css("display", "none");
    }
    return count;
};

function restartCategoria() {
    location.reload(true);
}


