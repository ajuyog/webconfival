$(document).ready(function () {
    // --- INPUTS OTP CORREO --- //
    $(".inputs").keyup(function () {
        if (this.value.length == this.maxLength) {
            var $next = $(this).next('.inputs');
            if ($next.length) {
                $(this).next('.inputs').focus();
            } else {
                $(this).blur();
            }
        }

    });
});

// --- VALIDACION DE CORREO ELECTRONICO CON OTP --- //
function showOTPCorreo() {
    var mail = $("#mail").val();
    var validMail = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);
    if (validMail.test(mail) == false) {
        $("#invalid-mail").css("display", "block");
    } else {
        $("#btn-validar-celular").removeClass("hide-info");
        $("#btn-validar-correo").addClass("hide-info");
        $("#invalid-mail").css("display", "none");
        $("#mail").addClass("disable-writing");
        $.ajax({
            type: "GET",
            url: '/Oportunidad/SendMail',
            data: { correo: mail },
            success: function () {
                $("#correo-otp").removeClass("hide-info");
                $("#put-timer").append('<div class="col-xl-12 col-sm-12 col-xs-12"><div class="card"><div class="card-header border-bottom"><h5 class="card-title">Hemos enviado un codigo OTP a tu correo electronico, ingresalo en el siguiente campo antes de que la cuenta regresiva se acabe.</h5></div><div class="card-body"><div class="example bg-primary-transparent border-primary text-primary"><div class="d-sm-flex"><span class="mb-sm-0 mb-3"><i class="fs-30 fe fe-clock"></i></span><div class="ms-sm-5 mb-sm-0 mb-3"><span id="timer-countercallback" class="h3"></span><h5 class="mb-0 mt-1">Verifica tu correo electronico!!!</h5></div><span class="h1 text-center ms-auto mb-0 mb-sm-0 mb-3 "></span></div></div></div></div></div>')
                activeTimer();
            }
        });
    }
}
function activeTimer() {
    $('#timer-countercallback').countdown({
        from: 30,
        to: 0,
        timerEnd: function () {
            this.animate({ 'opacity': .5 }, 500).css({ 'text-decoration': 'line-through' });
            $("#correo-otp").addClass("hide-info");
            $("#mesage-resend").removeClass("hide-info");
            $("#new-otp-mail").removeClass("hide-info");
            $("#numero-uno").val("");
            $("#numero-dos").val("");
            $("#numero-tres").val("");
            $("#numero-cuatro").val("");
            $("#numero-cinco").val("");
            $("#numero-seis").val("");
            $("#invalid-otp-mail").css("display", "none");
        }
    });
}
function resendMail() {
    $("#new-otp-mail").addClass("hide-info");
    $("#mesage-resend").addClass("hide-info");
    $("#put-timer").children("div").remove();
    showOTPCorreo();
}
function showCelular() {
    var number1 = $("#numero-uno").val();
    var number2 = $("#numero-dos").val();
    var number3 = $("#numero-tres").val();
    var number4 = $("#numero-cuatro").val();
    var number5 = $("#numero-cinco").val();
    var number6 = $("#numero-seis").val();
    var OTP = number1 + number2 + number3 + number4 + number5 + number6;
    var mail = $("#mail").val();
    $.ajax({
        type: "GET",
        url: '/Oportunidad/GetOTPMail',
        data: { correo: mail },
        success: function (result) {
            if (result == OTP) {
                $("#celular-hide").removeClass("hide-info");
                $("#otp-mail").css("pointer-events", "none");
                $("#invalid-otp-mail").css("display", "none");
                $("#email-hide").addClass("hide-info");
                $("#confirmacion-mail").removeClass("hide-info");
                $("#start-mail").addClass("hide-info");
                $("#validacion-dos").removeClass("hide-info");
                notificacionCorreo(mail);
            } else {
                $("#invalid-otp-mail").css("display", "block");
            }
        }
    });
}
function notificacionCorreo(data) {
    notif({
        msg: "Validacion correcta " + data,
        type: "success"
    });
}
function restart() {
    $("#btn-validar-correo").removeClass("hide-info");
    $("#mail").removeClass("disable-writing");
    $("#mail").val("");
    $("#put-timer").children("div").remove();
    $("#new-otp-mail").addClass("hide-info");
    $("#mesage-resend").addClass("hide-info");
}

// --- VALIDACION DE CELULAR DE CONTACTO CON OTP --- //
function showOTPCelular() {
    var celular = $("#celular").val();
    var validNDC = new RegExp(/^[3]/);
    var validLength = new RegExp(/^[0-9]{10}$/);
    if (validLength.test(celular) == false || validNDC.test(celular) == false) {
        $("#invalid-celular").css("display", "block");
    } else {
        $("#validacion-dos").addClass("hide-info");
        $("#invalid-celular").css("display", "none");
        $("#btn-validar-celular").addClass("hide-info");
        $("#celular").addClass("disable-writing");
        $.ajax({
            type: "GET",
            url: '/Oportunidad/SendMessage',
            data: { numero: celular },
            success: function () {
                $("#mensaje-otp").removeClass("hide-info");
                $("#put-timer2").append('<div class="col-xl-12 col-sm-12 col-xs-12"><div class="card"><div class="card-header border-bottom"><h5 class="card-title">Hemos enviado un codigo OTP a tu numero celular, ingresalo en el siguiente campo antes de que la cuenta regresiva se acabe.</h5></div><div class="card-body"><div class="example bg-primary-transparent border-primary text-primary"><div class="d-sm-flex"><span class="mb-sm-0 mb-3"><i class="fs-30 fe fe-clock"></i></span><div class="ms-sm-5 mb-sm-0 mb-3"><span id="timer-countercallback2" class="h3"></span><h5 class="mb-0 mt-1">Verifica tu numero celular!!!</h5></div><span class="h1 text-center ms-auto mb-0 mb-sm-0 mb-3 "></span></div></div></div></div></div>')
                activeTimer2();
            }
        });
    }
}
function activeTimer2() {
    $('#timer-countercallback2').countdown({
        from: 30,
        to: 0,
        timerEnd: function () {
            this.animate({ 'opacity': .5 }, 500).css({ 'text-decoration': 'line-through' });
            $("#mensaje-otp").addClass("hide-info");
            $("#mesage-resend-celular").removeClass("hide-info");
            $("#new-otp-celular").removeClass("hide-info");
            $("#numero-uno-m").val("");
            $("#numero-dos-m").val("");
            $("#numero-tres-m").val("");
            $("#numero-cuatro-m").val("");
            $("#numero-cinco-m").val("");
            $("#numero-seis-m").val("");
            $("#invalid-otp-celular").css("display", "none");
        }
    });
}
function resendMesage() {
    $("#new-otp-celular").addClass("hide-info");
    $("#mesage-resend-celular").addClass("hide-info");
    $("#put-timer2").children("div").remove();
    showOTPCelular();
}
function restart2() {
    // -- Limpio el input de celular -- //
    $("#celular-hide").addClass("hide-info");
    $("#celular").removeClass("disable-writing");
    $("#celular").val("");
    $("#put-timer2").children("div").remove();
    $("#mesage-resend-celular").addClass("hide-info");
    $("#new-otp-celular").addClass("hide-info");

    // -- Limpio el panel de validaciones -- //
    $("#start-mail").removeClass("hide-info");
    $("#validacion-dos").addClass("hide-info");

    // -- Limpio el input de correo -- //
    $("#email-hide").removeClass("hide-info");
    $("#put-timer").children("div").remove();
    $("#mail").removeClass("disable-writing");
    $("#mail").val("");
    $("#mesage-resend").addClass("hide-info");
    $("#new-otp-mail").addClass("hide-info");
    $("#btn-validar-correo").removeClass("hide-info");
    $("#confirmacion-mail").addClass("hide-info");
}

// --- VALIDACION DE DOCUMENTO DE IDENTIDAD --- //
function showDatosPersonales() {
    var number1 = $("#numero-uno-m").val();
    var number2 = $("#numero-dos-m").val();
    var number3 = $("#numero-tres-m").val();
    var number4 = $("#numero-cuatro-m").val();
    var number5 = $("#numero-cinco-m").val();
    var number6 = $("#numero-seis-m").val();
    var OTP = number1 + number2 + number3 + number4 + number5 + number6;
    var celular = $("#celular").val();
    $.ajax({
        type: "GET",
        url: '/Oportunidad/GetOTPCelular',
        data: { numero: celular },
        success: function (result) {
            if (result == OTP) {
                $("#invalid-otp-celular").css("display", "none");
                $("#celular-hide").addClass("hide-info");
                $("#confirmacion-celular").removeClass("hide-info");
                $("#validacion-tres").removeClass("hide-info");
                $("#progres-33").addClass("hide-info");
                $("#progres-66").removeClass("hide-info");
                $("#documento-hide").removeClass("hide-info");
                notificacionCorreo(celular);
            } else {
                $("#invalid-otp-celular").css("display", "block");
            }
        }
    });
}
function validarNumero() {
    var numeroDocumento = $("#documento").val();
    var tipoDocumento = $("#tipo-documento").val();

    // -- CONTROL DE INVALID FEEDBACK -- //
    if (numeroDocumento.length == 0) {
        $("#invalid-documento").css("display", "block");
    } else {
        $("#invalid-documento").css("display", "none");
    }
    if (tipoDocumento <= 0) {
        $("#invalid-tipo-documento").css("display", "block");
    }
    if(tipoDocumento > 0) {
        $("#invalid-tipo-documento").css("display", "none");
    }

    // -- CONSUMIR API REGISTRADURIA -- //
    if (numeroDocumento.length > 0 && tipoDocumento > 0) {
        var numeroDocumento = $("#documento").val();
        $("#segundo-intento").addClass("hide-info");
        $("#documento").addClass("disable-writing");
        $("#tipo-documento").prop('disabled', 'disabled');
        $("#btn-validar-documento").addClass("hide-info");
        $.ajax({
            type: "GET",
            url: '/Oportunidad/RegistraduriaCol',
            data: { documento: numeroDocumento },
            success: function (result) {
                if (result == 1032437606) {
                    $("#confirmar-documento").removeClass("hide-info");
                    $("#nombres").val("Steeven");
                    $("#primer-apellido").val("Morales");
                    $("#segundo-apellido").val("Medina");
                    $("#validacion-tres").addClass("hide-info");
                } else {
                    $("#validacion-tres").addClass("hide-info");
                    $("#segundo-intento").addClass("hide-info");
                    $("#confirmar-lead").removeClass("hide-info");
                }
            }
        });
    }
}
function crearLead() {
    $("#confirmar-lead").addClass("hide-info");
    $("#crear-lead").removeClass("hide-info");
}
function restart3() {
    $("#confirmar-documento").addClass("hide-info");
    $("#documento").removeClass("disable-writing");
    $("#documento").val("");
    $("#tipo-documento").prop('disabled', false);
    $("#tipo-documento").val("0");
    $("#select2-tipo-documento-container").text("Seleccione..")
    $("#btn-validar-documento").removeClass("hide-info");
    $("#segundo-intento").removeClass("hide-info");
    $("#confirmar-lead").addClass("hide-info");

}
function saveLead() {
    $("#validacion-cuatro").removeClass("hide-info");
    $("#documento-hide").addClass("hide-info");
    $("#confirmar-documento").addClass("hide-info");
    $("#oportunidad-hide").removeClass("hide-info");
    $("#crear-lead").addClass("hide-info");

    // -- UPDATE PROGRESS BAR -- //
    $("#progres-66").addClass("hide-info");
    $("#progres-100").removeClass("hide-info");
    $("#confirmacion-celular").addClass("hide-info");
    $("#confirmacion-celular-iz").addClass("hide-info");
    $("#icon-info-oportunidad").removeClass("hide-info");

}
function saveLeadNew() {

    var nombres = $("#nombres-new").val();
    var pApellido = $("#primer-apellido-new").val();
    var sApellido = $("#segundo-apellido-new").val();

    // -- CONTROL DE INVALID FEEDBACK -- //
    if (nombres.length == 0) {
        $("#invalid-nombres-new").css("display", "block");
    } else {
        $("#invalid-nombres-new").css("display", "none");

    }
    if (pApellido.length == 0) {
        $("#invalid-primer-apellido-new").css("display", "block");
    } else {
        $("#invalid-primer-apellido-new").css("display", "none");

    }
    if (sApellido.length == 0) {
        $("#invalid-segundo-apellido-new").css("display", "block");
    } else {
        $("#invalid-segundo-apellido-new").css("display", "none");

    }
    // -- CREO AL USUARIO QUE NO ESTA EN REGISTRADURIACOL Y LO ENVIO AL FORMULARIO OPORTUNIDAD -- //

    if (nombres.length > 0 && pApellido.length > 0 && sApellido.length > 0 ) {
        // Usar API que me registre a esta nueva persona puesto que no esta en registraduriaCol
        $("#validacion-cuatro").removeClass("hide-info");
        $("#documento-hide").addClass("hide-info");
        $("#confirmar-documento").addClass("hide-info");
        $("#oportunidad-hide").removeClass("hide-info");
        $("#crear-lead").addClass("hide-info");

        // -- UPDATE PROGRESS BAR -- //
        $("#progres-66").addClass("hide-info");
        $("#progres-100").removeClass("hide-info");
        $("#confirmacion-celular").addClass("hide-info");
        $("#confirmacion-celular-iz").addClass("hide-info");
        $("#icon-info-oportunidad").removeClass("hide-info");

    }




}

function widthConfival() {
    $(".slick-slide").css("width", "100%");
    $(".slick-track").css("width", "100%");
}



