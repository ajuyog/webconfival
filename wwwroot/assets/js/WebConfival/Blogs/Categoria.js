function CrearCategoria() {
    var nombreCorregido = CorregirNombre();
    var valid = Valid();
    if (valid == 0) {
        var existente = validarExistente(nombreCorregido);
        if (existente >= 1) {
            $("#invalid-nombre-categoria-existe").css("display", "block");
        }
        //if (existente == 0) {
        //    $("#invalid-nombre-categoria-existe").css("display", "none");
        //    GuardarCategoria(nombreCorregido);
        //}
    }
};

function validarExistente(nombre) {
    var count = 0;
    $.ajax({
        type: "GET",
        url: '/Blog/ConsultarCategorias',
        success: function (result) {
            console.log(result);
            $.each(result, function (element, index) {
                if (index.nombre == nombre) {
                    count = count + 1;
                }
            })
        }
    });
    return count;
};

function GuardarCategoria(nombre){
    $.ajax({
        type: "GET",
        url: '/Blog/SaveCategoria',
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
}


