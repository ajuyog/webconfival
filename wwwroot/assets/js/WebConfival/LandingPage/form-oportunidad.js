var nombres = "";

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
    $("#uncheckedPrimarySwitch-politica").val("on");
});

// --- VALIDACION DE POLITICA TRATAMIENTO DE DATOS --- //
function politica() {
    var statusPolitica = $("#uncheckedPrimarySwitch-politica").val();
    if (statusPolitica == "on" || statusPolitica == "false") {
        $("#uncheckedPrimarySwitch-politica").val("true");
    }
    if (statusPolitica == "true") {
        $("#uncheckedPrimarySwitch-politica").val("false");
    }
};

// --- VALIDACION DE CORREO ELECTRONICO CON OTP --- //
function showOTPCorreo()     {
    var mail = $("#mail").val();
    var validacion = MailRegex(mail);
    var politica = $("#uncheckedPrimarySwitch-politica").val();
    if (politica == "on" || politica == "false") {
        $("#invalid-politica").css("display", "block");
    } else {
        $("#invalid-politica").css("display", "none");
    }
    if (validacion == 0 && politica == "true") {
        // lOADER
        $("#loader1").removeClass("hide-info");
        $("#email-hide").addClass("hide-info");

        // FRONT
        $("#uncheckedPrimarySwitch-politica").val("false");
        ResendMailAPI(mail);
    }
};
function activeTimer() {
    $('#timer-countercallback').countdown({
        from: 20,
        to: 0,
        timerEnd: function () {
            this.animate({ 'opacity': .5 }, 500).css({ 'text-decoration': 'line-through' });
            $("#put-timer").children("div").remove();
            $("#correo-otp").addClass("hide-info");
            $("#mesage-resend").removeClass("hide-info");
            $("#new-otp-mail").removeClass("hide-info");
            $("#numero-uno").val("");
            $("#numero-dos").val("");
            $("#numero-tres").val("");
            $("#numero-cuatro").val("");
            $("#numero-cinco").val("");
            $("#numero-seis").val("");
            $("#verifica-correo").addClass("hide-info");
            $("#invalid-otp-mail").css("display", "none");
        }
    });
};
function ResendMail() {
    // lOADER
    $("#loader1").removeClass("hide-info");
    $("#email-hide").addClass("hide-info");

    // FRONT
    $("#new-otp-mail").addClass("hide-info");
    $("#mesage-resend").addClass("hide-info");
    $("#put-timer").children("div").remove();
    var mail = $("#mail").val();
    var validacion = MailRegex(mail);
    if (validacion == 0) {
        ResendMailAPI(mail);
    }
};  
function ShowDocumento() {
    // lOADER
    $("#loader1").removeClass("hide-info");
    $("#correo-otp").addClass("hide-info");
    $("#put-timer").addClass("hide-info");

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
        url: '/Oportunidad/ValidOTP',
        data: { llave: OTP, entrada: mail },
        success: function (result) {
            if (result == "success") {
                // lOADER
                $("#loader1").addClass("hide-info");
                $("#correo-otp").removeClass("hide-info");
                $("#put-timer").removeClass("hide-info");

                // FRONT
                $("#documento-hide").removeClass("hide-info");
                $("#otp-mail").css("pointer-events", "none");
                $("#invalid-otp-mail").css("display", "none");
                $("#email-hide").addClass("hide-info");
                $("#confirmacion-mail").removeClass("hide-info");
                $("#start-mail").addClass("hide-info");
                $("#validacion-dos").removeClass("hide-info");

                //SAVE INPUT
                $("#correo-ok").val(mail);

            } else {
                console.log(result);
                // lOADER
                $("#loader1").addClass("hide-info");
                $("#correo-otp").removeClass("hide-info");
                $("#put-timer").removeClass("hide-info");

                // FRONT
                $("#invalid-otp-mail").css("display", "block");
                $("#invalid-otp-mail").get(0).innerHTML = result;

            }
        },
        error: function () {
            // lOADER
            $("#loader1").addClass("hide-info");
            $("#correo-otp").removeClass("hide-info");
            $("#put-timer").removeClass("hide-info");

            // FRONT
            $("#api-error-form-oportunidad").modal('show');
        }
    });
};

// --- VALIDACION DE DOCUMENTO DE IDENTIDAD --- //
function ValidarDocumento() {
    var numeroDocumento = $("#documento").val();
    var tipoDocumentoFront = $("#tipo-documento").val();
    var nombres = $("#nombre-completo-lead").val();
    var apellidos = $("#apellidos-lead").val();
    var fExpedicion = $("#fecha-expedicion-user").val();
    var fnacimiento = $("#fecha-nacimiento-user").val();

    // -- CONTROL DE INVALID FEEDBACK -- //
    if (numeroDocumento.length == 0) {
        $("#invalid-documento").css("display", "block");
    } else {
        $("#invalid-documento").css("display", "none");
    }
    if (tipoDocumentoFront <= 0) {
        $("#invalid-tipo-documento").css("display", "block");
    }
    if (tipoDocumentoFront > 0) {
        $("#invalid-tipo-documento").css("display", "none");
    }
    if (nombres.length == 0) {
        $("#invalid-nombre-completo-lead").css("display", "block");
    } else {
        $("#invalid-nombre-completo-lead").css("display", "none");
    }
    if (apellidos.length == 0) {
        $("#invalid-apellidos-lead").css("display", "block");
    } else {
        $("#invalid-apellidos-lead").css("display", "none");
    }
    
    // -- CONSUMO API REGISTRADURIA -- //
    if (numeroDocumento.length > 0 && tipoDocumentoFront.length > 0 && nombres.length > 0 && apellidos.length > 0) {
        // lOADER
        $("#loader3").removeClass("hide-info");
        $("#documento-hide").addClass("hide-info");
        $("#validacion-dos").addClass("hide-info");

        // FRONT
        $("#documento").addClass("disable-writing");
        $("#nombre-completo-lead").addClass("disable-writing");
        $("#apellidos-lead").addClass("disable-writing");
        $("#tipo-documento").prop('disabled', 'disabled');
        $("#btn-validar-documento").addClass("hide-info");
        $.ajax({
            type: "GET",
            url: '/Oportunidad/RegistraduriaCol',
            data: { documento: numeroDocumento, nombre: nombres, apellido: apellidos, tipoDocumento: tipoDocumentoFront },
            success: function (result) {
                if (result.id != 0) {
                    // LOADER
                    $("#loader3").addClass("hide-info");

                    //FRONT
                    $("#confirmar-documento").removeClass("hide-info");
                    $("#documento-hide").addClass("hide-info");

                    //Result.fullname
                    $("#full-name").get(0).innerHTML = result.nombreCompleto;
                    $("#document-full-name").get(0).innerHTML = result.tipoDocumento + ": " + result.numeroDocumento;

                    //SAVE INPUT
                    $("#tp-documento-ok").val(result.tipoDocumento);
                    $("#nombres-ok").val(result.nombres);
                    $("#apellidos-ok").val(result.apellidos);
                    $("#no-documento-ok").val(result.numeroDocumento);
                    $("#fNacimiento-ok").val(fnacimiento.toString());
                    $("#fExpedicion-ok").val(fExpedicion.toString());

                } else {
                    $("#intentos-sms-msg").get(0).innerHTML = "Haz alcanzado el maximo de intentos permitidos para verificar tu documento de identidad"
                    $('#intentos-verifik-count').html(function (i, val) { return val * 1 + 1 });
                    if (parseInt($('#intentos-verifik-count').get(0).innerHTML) >= 2) {
                        // LOADER
                        $("#loader3").addClass("hide-info");
                        $("#documento-hide").removeClass("hide-info");

                        $("#documento-hide").addClass("hide-info");
                        $("#intentos-verifik-superados").removeClass("hide-info");

                    } else {
                        // LOADER
                        $("#loader3").addClass("hide-info");
                        $("#documento-hide").removeClass("hide-info");

                        // FRONT -- Limpio el formulario
                        $("#documento").removeClass("disable-writing");
                        $("#documento").val("");

                        $("#tipo-documento").prop('disabled', false);
                        $("#tipo-documento").val("0");
                        $("#select2-tipo-documento-container").text("Seleccione..");

                        CleanDocumento(result.error);
                    }
                }
            },
            error: function () {
                // LOADER
                $("#loader3").addClass("hide-info");
                $("#documento-hide").removeClass("hide-info");

                // FRONT
                $("#api-error-form-oportunidad").modal('show');
            }
        });
    }
};
function Cancel() {
    location.reload();
};
function ShowCelular() {

    // FRONT
    $("#validacion-cuatro").removeClass("hide-info");
    $("#documento-hide").addClass("hide-info");
    $("#confirmar-documento").addClass("hide-info");
    $("#celular-hide").removeClass("hide-info");

    //UPDATE PROGRESS BAR
    $("#progres-66").removeClass("hide-info");
    $("#progres-33").addClass("hide-info");
    $("#confirmacion-documento-status").removeClass("hide-info");
    $("#confirmacion-correo-status").removeClass("hide-info");
    
};

// --- VALIDACION DE CELULAR DE CONTACTO CON OTP --- //
function showOTPCelular() {
    var celular = $("#celular").val();
    var validNDC = new RegExp(/^[3]/);
    var validLength = new RegExp(/^[0-9]{10}$/);
    if (validLength.test(celular) == false || validNDC.test(celular) == false) {
        $("#invalid-celular").css("display", "block");
    } else {
        // LOADER
        $("#loader2").removeClass("hide-info");
        $("#validacion-cuatro").addClass("hide-info");

        $("#validacion-dos").addClass("hide-info");
        $("#invalid-celular").css("display", "none");
        $("#btn-validar-celular").addClass("hide-info");
        $("#celular").addClass("disable-writing");
        $.ajax({
            type: "GET",
            url: '/Oportunidad/SendOTP',
            data: { entrada: celular },
            success: function (result) {
                if (result == true) {
                    // LOADER
                    $("#loader2").addClass("hide-info");

                    // FRONT
                    $("#mensaje-otp").removeClass("hide-info");
                    $("#put-timer2").append('<div class="col-xl-12 col-sm-12 col-xs-12"><div class="card"><div class="card-header border-bottom" style="padding: 0; padding-bottom: 15px;"><h5 class="card-title" style="font-size: 0.9rem;">Hemos enviado un codigo OTP a tu numero celular, ingresalo en el siguiente campo antes de que la cuenta regresiva se acabe.</h5></div><div class="card-body" style="padding: 0;"><div class="example bg-primary-transparent border-primary text-primary" style="padding:1rem;"><div class="d-sm-flex"><span class="mb-sm-0 mb-3"><i class="fs-30 fe fe-clock"></i></span><div class="ms-sm-5 mb-sm-0 mb-3"><span id="timer-countercallback2" class="h3"></span><h5 class="mb-0 mt-1" id="verificar-celular">Verifica tu numero celular!!!</h5></div><span class="h1 text-center ms-auto mb-0 mb-sm-0 mb-3 "></span></div></div></div></div></div>')
                    activeTimer2();
                } else {
                    // LOADER
                    $("#loader2").addClass("hide-info");
                    $("#validacion-cuatro").removeClass("hide-info");

                    // FRONT
                    $("#api-error-form-oportunidad").modal('show');
                }
            },
            error: function () {
                // LOADER
                $("#loader2").addClass("hide-info");

                // FRONT
                $("#api-error-form-oportunidad").modal('show');
            }
        });
    }
};
function activeTimer2() {
    $('#timer-countercallback2').countdown({
        from: 30,
        to: 0,
        timerEnd: function () {
            this.animate({ 'opacity': .5 }, 500).css({ 'text-decoration': 'line-through' });
            $("#put-timer2").children("div").remove();
            $("#mensaje-otp").addClass("hide-info");
            $("#mesage-resend-celular").removeClass("hide-info");
            $("#new-otp-celular").removeClass("hide-info");
            $("#numero-uno-m").val("");
            $("#numero-dos-m").val("");
            $("#numero-tres-m").val("");
            $("#numero-cuatro-m").val("");
            $("#numero-cinco-m").val("");
            $("#numero-seis-m").val("");
            $("#verificar-celular").addClass("hide-info");
            $("#invalid-otp-celular").css("display", "none");
        }
    });
};
function ResendMesage() {
    $("#new-otp-celular").addClass("hide-info");
    $("#mesage-resend-celular").addClass("hide-info");
    $("#put-timer2").children("div").remove();
    showOTPCelular();
};
function ShowOportunidad() {
    // LOADER
    $("#loader2").removeClass("hide-info");
    $("#celular-hide").addClass("hide-info");

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
        url: '/Oportunidad/ValidOTP',
        data: { llave: OTP, entrada: celular },
        success: function (result) {
            if (result == "success") {
                // LOADER
                $("#loader2").addClass("hide-info");

                // FRONT
                $("#invalid-otp-celular").css("display", "none");
                $("#celular-hide").addClass("hide-info");
                $("#confirmacion-celular").removeClass("hide-info");
                $("#validacion-tres").removeClass("hide-info");
                $("#validacion-cuatro").addClass("hide-info");
                $("#oportunidad-hide").removeClass("hide-info");

                // INPUT SAVE

                $("#no-contacto-ok").val(celular);

                // PROGRES BAR
                $("#progres-33").addClass("hide-info");
                $("#progres-66").addClass("hide-info");
                $("#progres-100").removeClass("hide-info");
                $("#icon-info-oportunidad").removeClass("hide-info");
                $("#confirmacion-correo-status").addClass("hide-info");
                $("#confirmacion-documento-status").addClass("hide-info");
            } else {
                // LOADER
                $("#loader2").addClass("hide-info");
                $("#celular-hide").removeClass("hide-info");

                // FRONT
                $("#invalid-otp-celular").css("display", "block");
                $("#invalid-otp-celular").get(0).innerHTML = result;
            }
        },
        error: function () {
            // LOADER
            $("#loader2").addClass("hide-info");

            // FRONT
            $("#api-error-form-oportunidad").modal('show');
        }
    });
};
function LeadOportunidad() {
    var validacion = ValidarLeadOportunidad();
    if (validacion == 0) {
        const formElement = document.querySelector("form");
        var formData = new FormData(formElement);
        $.ajax({
            url: "Oportunidad/SaveForm",
            type: 'POST',
            data: formData,
            success: function (data) {
                console.log(data);
                if (data) {
                    $("#success-leadOportunidad").modal("show");
                    window.location.reload();
                } else {
                    notif({
                        msg: 'Algo malo ha pasado, vuelve a intentar',
                        type: "danger",
                        multiline: true,
                        position: "center"
                    });
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
    }
}

function ValidarLeadOportunidad() {
    var count = 0;
    var tipoFallo = $("#fallo").val();
    var tipoRegimen = $("#tipo-regimen").val();
    var medioContro = $("#medio-control").val();
    var entidad = $("#entidad-pagaduria").val();
    var tipoCorporacion = $("#tipo-corporacion").val();
    var corporacion = $("#corporacion").val();

    if (tipoFallo == null) {
        $("#invalid-feedback-tipo-fallo").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-tipo-fallo").css("display", "none");
    }

    if (tipoRegimen == null) {
        $("#invalid-feedback-tipo-regimen").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-tipo-regimen").css("display", "none");
    }

    if (medioContro == null) {
        $("#invalid-feedback-medio-control").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-medio-control").css("display", "none");
    }

    if (entidad == null) {
        $("#invalid-feedback-entidad-pagaduria").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-entidad-pagaduria").css("display", "none");
    }

    if (tipoCorporacion == null) {
        $("#invalid-feedback-tipo-corporacion").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-tipo-corporacion").css("display", "none");
    }

    if (corporacion == null) {
        $("#invalid-feedback-corporacion").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-feedback-corporacion").css("display", "none");
    }
    return count;
}



// --- UTILIDAD --- //
function MailRegex(data) {
    var count = 0;
    var validMail = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);
    if (validMail.test(data) == false) {
        $("#invalid-mail").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-mail").css("display", "none");
        count = 0;
    }
    return count;


};
function ResendMailAPI(data) {

    // FRONT
    $("#btn-validar-celular").removeClass("hide-info");
    $("#btn-validar-correo").addClass("hide-info");
    $("#politica-hide").addClass("hide-info");
    $("#invalid-mail").css("display", "none");
    $("#invalid-politica").css("display", "none");
    $("#mail").addClass("disable-writing");
    $.ajax({
        type: "GET",
        url: '/Oportunidad/SendOTP',
        data: { entrada: data },
        success: function (result) {
            if (result == true) {
                // lOADER
                $("#loader1").addClass("hide-info");
                $("#email-hide").removeClass("hide-info");

                // FRONT
                $("#correo-otp").removeClass("hide-info");
                $("#put-timer").append('<div><div class="card"><div class="card-header border-bottom" style="padding: 0; padding-bottom: 15px;"><h5 class="card-title" style="font-size: 0.9rem;">Hemos enviado un codigo OTP a tu correo electronico, ingresalo en el siguiente campo antes de que la cuenta regresiva se acabe.</h5></div><div class="card-body" style="padding: 0;"><div class="example bg-primary-transparent border-primary text-primary" style="padding:1rem;"><div class="d-sm-flex"><span class="mb-sm-0 mb-3"><i class="fs-30 fe fe-clock"></i></span><div class="ms-sm-5 mb-sm-0 mb-3"><span id="timer-countercallback" class="h3"></span><h5 class="mb-0 mt-1" id="verifica-correo">Verifica tu correo electronico!!!</h5></div><span class="h1 text-center ms-auto mb-0 mb-sm-0 mb-3 "></span></div></div></div></div></div>')
                activeTimer();
            } else {
                // lOADER
                $("#loader1").addClass("hide-info");
                $("#email-hide").removeClass("hide-info");

                // FRONT
                $("#api-error-form-oportunidad").modal('show');
            }
        },
        error: function () {
            // lOADER
            $("#loader1").addClass("hide-info");
            $("#email-hide").removeClass("hide-info");

            // FRONT
            $("#api-error-form-oportunidad").modal('show');
        }
    });

}
function CleanDocumento(data) {
    $("#nombre-completo-lead").val("");
    $("#nombre-completo-lead").removeClass("disable-writing");

    $("#apellidos-lead").val("");
    $("#apellidos-lead").removeClass("disable-writing");

    $("#mensaje-error-verifica").get(0).innerHTML = data;
    $("#segundo-intento").removeClass("hide-info");
    $("#btn-validar-documento").removeClass("hide-info");
}

$("#tipo-corporacion").on("change", function () {
    var select = $("#corporacion");
    var idCorporacion = $("#tipo-corporacion").val();
    $.ajax({
        type: "GET",
        url: '/LandingPage/CorporacionId',
        data: { id: idCorporacion },
        success: function (result) {
            if (result != null) {
                select.children().remove();
                select.append('<option selected disabled value="">Seleccione..</option>')
                $.each(result, function (element, index) {
                    select.append('<option value="' + index.id + '">' + index.nombre + '</option>')
                });
            }
        },
        error: function () {
            console.log("error");
        }
    });
});


// --- CONTADOR SMS --- //
$('#intentos-sms').click(function () {
    var count = $('#intentos-sms-count');
    /*var intentos = GetAttempts("SMS");*/
    if (count.get(0).innerHTML == "1") {
        $("#celular-hide").addClass("hide-info");
        $("#intentos-sms-superados").removeClass("hide-info");
        $("#intentos-sms-msg").get(0).innerHTML = "Haz alcanzado el maximo de intentos permitidos para solicitar un SMS"
    } else {
        $('#intentos-sms-count').html(function (i, val) { return val * 1 + 1 });
    }
})














