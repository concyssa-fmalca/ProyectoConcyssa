
window.onload = function () {
    var url = "Sociedad/ObtenerSociedades";
    ConsultaServidor(url);
};

function is_json(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        if (data == "error") {
            return;
        }

        let tr = '';
        if (is_json(data)) {
            let sociedades = JSON.parse(data);

            for (var i = 0; i < sociedades.length; i++) {

                tr += '<tr>' +
                    '<td>  <input type="radio" clase="" id="rdSeleccionado' + sociedades[i].IdSociedad + '" value="' + sociedades[i].IdSociedad + '"  name="rdSeleccionado"  ></td>' +
                    '<td>' + sociedades[i].NombreSociedad.toUpperCase() + '</td>' +
                    '<td>' + sociedades[i].Descripcion.toUpperCase() + '</td>' +
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
    idsociedad = $('input:radio[name=rdSeleccionado]:checked').val();
    console.log(idsociedad);

    if (idsociedad == undefined) {
        var lblMensaje = document.getElementById("mensajeErr");
        lblMensaje.style.visibility = 'visible';
        lblMensaje.innerHTML = "Debe seleccionar una sociedad";
        return;
    }


    $.post("/Home/login", { "usuario": usuario, "password": password, "idsociedad": idsociedad }, function (data) {
        


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
    });


});