
window.onload = function () {
    $("#txtClaveNueva").on("keyup", function () {
        var password = $(this).val();

        // Mostrar el contenedor de fortaleza solo si hay texto
        if (password.length > 0) {
            $("#passwordStrengthContainer").show();
        } else {
            $("#passwordStrengthContainer").hide();
            return;
        }

        // Verificar cada requisito
        var lengthValid = password.length >= 8;
        var lowercaseValid = /[a-z]/.test(password);
        var uppercaseValid = /[A-Z]/.test(password);
        var numberValid = /[0-9]/.test(password);
        var specialValid = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);

        // Actualizar indicadores visuales para cada requisito
        updateRequirement("req-length", lengthValid);
        updateRequirement("req-lowercase", lowercaseValid);
        updateRequirement("req-uppercase", uppercaseValid);
        updateRequirement("req-number", numberValid);
        updateRequirement("req-special", specialValid);

        // Calcular la fortaleza general (de 0 a 100)
        var strength = 0;
        if (lengthValid) strength += 20;
        if (lowercaseValid) strength += 20;
        if (uppercaseValid) strength += 20;
        if (numberValid) strength += 20;
        if (specialValid) strength += 20;

        // Actualizar la barra de progreso
        $("#passwordStrength")
            .css("width", strength + "%")
            .attr("aria-valuenow", strength);

        // Cambiar el color de la barra según la fortaleza
        if (strength < 40) {
            $("#passwordStrength").removeClass("bg-warning bg-success").addClass("bg-danger");
        } else if (strength < 80) {
            $("#passwordStrength").removeClass("bg-danger bg-success").addClass("bg-warning");
        } else {
            $("#passwordStrength").removeClass("bg-danger bg-warning").addClass("bg-success");
        }

        // Agregar mensaje de error al campo si no cumple todos los requisitos
        if (strength < 100) {
            $("[data-valmsg-for='Password']").text("La contraseña no cumple con todos los requisitos").show();
        } else {
            $("[data-valmsg-for='Password']").text("").hide();
        }
    });
    $("#DivUpdateClave").hide()
    ConsultaServidor();
};

function is_json(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function ConsultaServidor() {
         $.post('/Home/CargarConexiones', function (data, status) {

             if (data == "error") {
                 return;
             }

             let tr = '';
             if (is_json(data)) {
                 let sociedades = JSON.parse(data);

                 for (var i = 0; i < sociedades.length; i++) {

                     let chktxt = "";
                     if(i==0) chktxt = 'checked'

                     //tr += '<tr onclick="clickCheck(' + i + ')">' +
                     tr += '<tr>' +
                         '<td>  <input type="radio" clase="" id="rdSeleccionado' + sociedades[i].IdSociedad + '" value="' + sociedades[i].BaseDatos + '" ' + chktxt + ' idSociedad="' + sociedades[i].IdSociedad + '" name="rdSeleccionado" alias="' + sociedades[i].Alias+'"  ></td>' +
                         '<td>' + sociedades[i].Alias.toUpperCase() + '</td>' +
                         '<td>' + sociedades[i].BaseDatos.toUpperCase() + '</td>' +
                         '</tr>';
                 }

                 $("#tbodyDetalle").html(tr);
             } else {
                 alert(data);
             }

         });    
}


$("#frmAcceso").on('submit', function (e) {
    e.preventDefault();

    usuario = $("#txtUsuario").val();
    password = $("#txtPassword").val();
    BaseDatos = $('input:radio[name=rdSeleccionado]:checked').val();
    idsociedad = $('input:radio[name=rdSeleccionado]:checked').attr('idSociedad');
    alias = $('input:radio[name=rdSeleccionado]:checked').attr('alias');
    console.log(idsociedad);

    if (idsociedad == undefined) {
        var lblMensaje = document.getElementById("mensajeErr");
        lblMensaje.style.visibility = 'visible';
        lblMensaje.innerHTML = "Debe seleccionar una sociedad";
        return;
    }


    $.post("/Home/login", { "usuario": usuario, "password": password, "idsociedad": idsociedad, "BaseDatos": BaseDatos, "Alias": alias }, function (data) {
        
        try {

            let datos = JSON.parse(data);


            if (datos.status) {


                let Usuario = datos.Usuario

                if (datos.ClaveExpira) {
                    
                    $("#frmAcceso").hide()
                    $("#DivUpdateClave").show()
                    return
                }

                if (Usuario.IdPerfil == 1021) {
                    $(location).attr("href", URLactual + "Responsive/Index");
                } else {
                    $(location).attr("href", URLactual + "Home/about");
                }

            }
            else {

                var lblMensaje = document.getElementById("mensajeErr");
                lblMensaje.style.visibility = 'visible';
                lblMensaje.innerHTML = "Usuario y Contraseña Incorrectos";

            }

        } catch (e) {
            var lblMensaje = document.getElementById("mensajeErr");
            lblMensaje.style.visibility = 'visible';
            lblMensaje.innerHTML = "Ocurrió un error";
        }

    });


});
function MostrarClaves() {

    if ($("#chkVerClave").prop("checked")) {
        $(".clave").each(function (indice, elemento) {
            $(elemento).attr('type', 'text');
        });

    } else {
        $(".clave").each(function (indice, elemento) {
            $(elemento).attr('type', 'password');
        });
    }

}

function CambiarClave() {
    let ClaveActual = $("#txtClaveActual").val()
    let ClaveNueva = $("#txtClaveNueva").val()
    let ClaveNueva2 = $("#txtClaveNueva2").val()

    if (ClaveActual == "" || ClaveNueva == "" || ClaveNueva2 == "") {
        Swal.fire("Advertencia", "Complete todos los campos", "info");
        return;
    }

    if (ClaveActual == ClaveNueva) {
        Swal.fire("Error", "No puede usar la misma clave", "error");
        return;
    }

    if (ClaveNueva != ClaveNueva2) {
        Swal.fire("Error", "La Nueva Clave y la Confirmación no coinciden", "error");
        return;
    }
    var isValid = validatePassword(ClaveNueva);

    if (!isValid) {
        Swal.fire("Error", "La contraseña no cumple con todos los requisitos de seguridad. Por favor revise los requisitos indicados.", "error");
        return
    }

    $.post("/Usuario/CambiarPassword", { ClaveActual, ClaveNueva }, function (data, status) {
        let datos = JSON.parse(data);

        if (datos.status) {
            Swal.fire({
                title: 'Exito!',
                text: 'Clave Actualizada Correctamente',
                timer: 1000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(
                function () { },
                // handling the promise rejection
                function (dismiss) {
                    if (dismiss === 'timer') {
                        //console.log('I was closed by the timer')
                    }
                }
            )
            setTimeout(() => {
                $(location).attr("href", URLactual + "Home/about");
            }, 1000)
        } else {
            Swal.fire("Error", datos.mensaje, "error")
        }

    });


}
function validatePassword(password) {
    if (password.length < 8) return false;
    if (!/[a-z]/.test(password)) return false;
    if (!/[A-Z]/.test(password)) return false;
    if (!/[0-9]/.test(password)) return false;
    if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)) return false;

    return true;
}

// Función para actualizar cada requisito en la lista
function updateRequirement(id, isValid) {
    if (isValid) {
        $("#" + id)
            .removeClass("text-danger")
            .addClass("text-success")
            .find("i")
            .removeClass("fa-times-circle")
            .addClass("fa-check-circle");
    } else {
        $("#" + id)
            .removeClass("text-success")
            .addClass("text-danger")
            .find("i")
            .removeClass("fa-check-circle")
            .addClass("fa-times-circle");
    }
}
