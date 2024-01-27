
window.onload = function () {
    
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

                     tr += '<tr onclick="clickCheck('+i+')">' +
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


            if (data) {
                if (datos.IdPerfil == 1021) {
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
            lblMensaje.innerHTML = "Usuario y Contraseña Incorrectos";
        }

    });


});