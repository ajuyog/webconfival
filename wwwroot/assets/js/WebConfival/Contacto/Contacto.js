function Create() {
    var valid = Valid();
    if (valid == 0) {
        var correo = $("#mail").val();
        var nombre = $("#name").val();
        var celular = $("#phone").val();
        var reparacionDirecta = $("#reparacion").val();
        var modalSuccess = $("#modal-success-lead");
        var modalError = $("#modal-error-lead")
        if (valid == 0) {
            $.ajax({
                type: "GET",
                url: '/Contacto/CreateLead',
                data: { name: nombre, email: correo, phone: celular, reparacion: reparacionDirecta },
                success: function (result) {
                    if (result == true) {
                        $("#mail").val("");
                        $("#name").val("");
                        $("#phone").val("");
                        $("#reparacion").val("");
                        $("#reparacion option[value='']")[0].selected = true;
                        modalSuccess.modal("show");
                    } else {
                        modalError.modal("show");
                    }
                },
                error: function () {
                    modalError.modal("show");
                }
            });
        }
    }
}

// -- Utilidad -- //
function Valid() {
    var count = 0;
    var correo = $("#mail").val();
    var nombre = $("#name").val();
    var celular = $("#phone").val();
    var reparacion = $("#reparacion").val();
    if (correo.length == 0) {
        $("#invalid-email-lead").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-email-lead").css("display", "none");
    }
    if (nombre.length == 0) {
        $("#invalid-name-lead").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-name-lead").css("display", "none");
    }
    if (celular.length == 0) {
        $("#invalid-phone-lead").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-phone-lead").css("display", "none");
    }
    if (reparacion == null) {
        $("#invalid-reparacionDirecta-lead").css("display", "block");
        count = count + 1;
    }
    if (reparacion != null) {
        $("#invalid-reparacionDirecta-lead").css("display", "none");
    }
    var regexMail = MailRegex(correo);
    count + regexMail;
    var phoneRegex = PhoneRegex(celular);
    count + phoneRegex;
    return count;
}
function MailRegex(data) {
    var count = 0;
    var validMail = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);
    if (validMail.test(data) == false) {
        $("#invalid-mail-regex-lead").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-mail-regex-lead").css("display", "none");
        count = 0;
    }
    return count;
};
function PhoneRegex(celular) {
    var count = 0;
    var validNDC = new RegExp(/^[3]/);
    var validLength = new RegExp(/^[0-9]{10}$/);
    if (validLength.test(celular) == false || validNDC.test(celular) == false) {
        $("#invalid-celular-regex-lead").css("display", "block");
        count = count + 1;
    } else {
        $("#invalid-celular-regex-lead").css("display", "none");
    }
}