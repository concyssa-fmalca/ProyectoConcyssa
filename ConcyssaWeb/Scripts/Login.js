alert('sssssss');
window.onload = function () {
    var url = "Sociedad/ObtenerSociedades";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        if (data == "error") {
            return;
        }

        let tr = '';

        let sociedades = JSON.parse(data);

        for (var i = 0; i < sociedades.length; i++) {

            tr += '<tr>' +
                '<td>  <input type="radio" clase="" id="rdSeleccionado' + sociedades[i].IdSociedad + '" valor="' + sociedades[i].IdSociedad + '"  name="rdSeleccionado"  value = "' + sociedades[i].CadenaConexion + '" ></td>' +
                '<td>' + sociedades[i].NombreSociedad.toUpperCase() + '</td>' +
                '<td>' + sociedades[i].NombreBd.toUpperCase() + '</td>' +
                '</tr>';
        }

        $("#tbodyDetalle").html(tr);
    });

}


$("#frmAcceso").on('submit', function (e) {
    e.preventDefault();

    usuario = $("#txtUsuario").val();
    password = $("#txtPassword").val();


    CadenaConexion = $('input:radio[name=rdSeleccionado]:checked').val();

    if (CadenaConexion == undefined) {
        var lblMensaje = document.getElementById("mensajeErr");
        lblMensaje.style.visibility = 'visible';
        lblMensaje.innerHTML = "Debe seleccionar una sociedad";
        //console.log("hola");
        return;
    }


    $.post("/Home/login", { "usuario": usuario, "password": password, "cadenaConexion": CadenaConexion }, function (data) {


        if (data) {
            //console.log(data);
            //$("#btn_iniciar").html(`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Bienvenido...`);
            //alertify.log("Usuario y Contraseña correcto");
            $(location).attr("href", URLactual + "Home/about");
        }
        else {
            if (data) {
                //$("#btn_iniciar").html(`Iniciar Sesión`);
                //alertify.error("Usuario o contraseña Incorrectos");
            }
        }
    });


});