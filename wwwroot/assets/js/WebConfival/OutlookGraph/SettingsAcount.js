function SolicitarPermiso() {
    var validacion = ValidarFormulario();
    if (validacion == 0) {
        var usuarioNombre = $("#userName").val();
        var recipents = $("#UserMail").val();
        var lstPermisos = $("#permisos-solicitados").val();
        var comentario = $("#comentario-permisos").val();
        var hidenToken = $("#hiden-token").val();
        var array = JSON.stringify(lstPermisos);
        
        $.ajax({
            type: "GET",
            url: '/Graph/RequestPermissions',
            data: { usuario: usuarioNombre, mail: recipents, permisos: array, mensaje: comentario, hToken: hidenToken },
            success: function (result) {
                if (result == true) {
                    $("#panel-permisos").children().remove();
                    $("#panel-permisos").append('<div class="card"><div class="card-body"><div class="alert alert-default pad-text" role="alert"><span class="alert-inner--icon me-2"><i class="fe fe-thumbs-up"></i></span><span class="alert-inner--text"><strong>Tu solicitud de permisos esta siendo analizada,</strong> Uno de nuestros colaboradores te notificara al correo electronico cuando sea aprovada !!!</span><div class="row"><div class="col-12"><a href="/home" class="btn btn-success" style="margin-top:20px;">Aceptar</a></div></div></div></div></div> ');
                } else {
                    $("#error-solicitud-permisos").modal("show");
                }
            },
            error: function () {

            }
        });
        
    }
};

function ValidarFormulario() {
    var count = 0;
    var usuarioNombre = $("#userName").val();
    var usuarioApellido = $("#userLastName").val();
    var mail = $("#UserMail").val();
    var cargo = $("#UserJobTitle").val();
    var permisos = $("#permisos-solicitados").val();

    if (usuarioNombre.length == 0) {
        $("#invalid-userName").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-userName").css("display", "none");
    }
    if (usuarioApellido.length == 0) {
        $("#invalid-userLastName").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-userLastName").css("display", "none");
    }
    if (mail.length == 0) {
        $("#invalid-UserMail").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-UserMail").css("display", "none");
    }
    if (cargo.length == 0) {
        $("#invalid-UserJobTitle").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-UserJobTitle").css("display", "none");
    }
    if (permisos.length == 0) {
        $("#invalid-permisos-solicitados").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-permisos-solicitados").css("display", "none");
    }
    return count;
}