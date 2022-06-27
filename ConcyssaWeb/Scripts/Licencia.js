window.onload = function () {
    var url = "cargarNumero";
    enviarServidor(url, mostrarNumero);
}
function mostrarNumero(rpta) {
    var txtNumero = document.getElementById("txtNumero");
    txtNumero.value = rpta;
}

function generarCodigo() {
    var url = "generarCodigo";
    enviarServidor(url, mostrarRespuesta);
}
function mostrarRespuesta(rpta) {
    var txtNumero = document.getElementById("txtNumero");
    txtNumero.value = rpta;
}
function validarCodigo() {
    
    var url = "validarCodigo";
    enviarServidorPost(url, mostrarLicencia);
}
function mostrarLicencia(rpta) {
    alert(rpta);
}

function enviarServidorPost(url, metodo) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open("post", url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
    };
    var frm = new FormData(); //HTML5
    var txtNumero = document.getElementById("txtNumero");
    var txtCodigo = document.getElementById("txtCodigo");
    frm.append("codigo", txtCodigo.value);
    frm.append("numero", txtNumero.value);
    xhr.send(frm);
}