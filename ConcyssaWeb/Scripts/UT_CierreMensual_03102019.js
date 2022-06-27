window.onload = function () {
    var lstm = ["1¦Enero", "2¦Febrero", "3¦Marzo", "4¦Abril", "5¦Mayo", "6¦Junio", "7¦Julio", "8¦Agosto", "9¦Septiembre", "10¦Octubre", "11¦Noviembre", "12¦Diciembre"];
    llenarCombo(lstm, "cboMes");
    llenarCombo(lstm, "cboMes1");
    pl();
    configurarBotones();
}
function configurarBotones() {

    var doc = document, cboAnioP = doc.getElementById("cboAnioP"), cboMesp = doc.getElementById("cboMesP");
    doc.getElementById("txtPeriodo").value = cboAnioP.value;
    doc.getElementById("cboMes").value = cboMesp.value;
    doc.getElementById("cboMes1").value = cboMesp.value == 12 ? "1" : (cboMesp.value * 1) + 1;

    var btnGrabar = doc.getElementById("btnGrabar"),
        btnEliminar = doc.getElementById("btnEliminar");
    btnGrabar.onclick = function () {
        //enviarServidor("grabarCierre?t=0", mostrarRpta, "get", null);
        enviarServidor("grabarCierre?t=0" + "&PPeriodo=" + parseInt(document.getElementById("cboAnioP").value) + "&Mes=" + parseInt(document.getElementById("cboMesP").value) + "&BDEmpresaActual=" + document.getElementById("BDEmpresaActual").value, mostrarRpta, "get", null);
    }
    btnEliminar.onclick = function () {
        swal({
            title: "Info",
            text: "¿Esta seguro de eliminar el cierre de este mes?",
            type: "info",
            showCancelButton: true,
            confirmButtonText: "Si, Continuar",
        }).then(function () {
            //enviarServidor("eliminarCierre", mostrarRpta, "get", null);
            enviarServidor("eliminarCierre?PPeriodo=" + parseInt(document.getElementById("cboAnioP").value) + "&Mes=" + parseInt(document.getElementById("cboMesP").value) + "&BDEmpresaActual=" + document.getElementById("BDEmpresaActual").value, mostrarRpta, "get", null);
        }, function () {

        });
    }
    var doc = document;
    doc.onclick = function (e) {
        var btn = doc.getElementsByClassName("swal2-confirm styled");
        var lbl = doc.getElementsByClassName("swal2-content");
        if (btn.length > 0 && lbl.length > 0) {
            if (e.target == btn[0] && lbl[0].textContent == "¿Esta seguro de eliminar el cierre de este mes?") {
                //enviarServidor("eliminarCierre", mostrarRpta, "get", null);
                enviarServidor("eliminarCierre?PPeriodo=" + parseInt(document.getElementById("cboAnioP").value) + "&Mes=" + parseInt(document.getElementById("cboMesP").value) + "&BDEmpresaActual=" + document.getElementById("BDEmpresaActual").value, mostrarRpta, "get", null);
            }
        }
        if (btn.length > 0 && lbl.length > 0) {
            if (e.target == btn[0] && lbl[0].textContent == "El Mes Elegido para el cierre ya se encuentra Cerrado") {
                //enviarServidor("grabarCierre?t=1", mostrarRpta, "get", null);
                enviarServidor("grabarCierre?t=1" + "&PPeriodo=" + parseInt(document.getElementById("cboAnioP").value) + "&Mes=" + parseInt(document.getElementById("cboMesP").value) + "&BDEmpresaActual=" + document.getElementById("BDEmpresaActual").value, mostrarRpta, "get", null);
            }
        }
    }

}

function enviarServidor(url, metodo, tipo, text) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open(tipo, url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
    };
    xhr.send(text);
}
function llenarCombo(lst, id, item) {
    var nrgs = lst.length, cmps, cnt = "";
    if (item != undefined) {
        cnt += "<option value='-1'>" + item + "</option>";
    }
    for (var i = 0; i < nrgs; i += 1) {
        cmps = lst[i].split("¦");
        cnt += "<option value='";
        cnt += cmps[0];
        cnt += "'>";
        cnt += cmps[1];
        cnt += "</option>";
    }
    document.getElementById(id).innerHTML = cnt;
}
function validarSoloNumero() {
    var sns = document.getElementsByClassName("sn"), nsn = sns.length, snn;
    for (var i = nsn; i--;) {
        snn = sns[i];
        snn.onkeypress = function (e) {
            var key = e.which || e.keyCode
            if ((key >= 48 && key <= 57) || (key == 8)) {
                return true;
            } else { return false };
            e.stopPropagation();
        }
    }
}
function mostrarRpta(r) {
    if (r != "") {
        var errorEmpresa = validarEmpresa(r);
        if (errorEmpresa) {
            return;
        }

        switch (r) {
            case "-11":
                swal({
                    title: "Info",
                    text: "Este periodo se encuentra cerrado por Control de Meses",
                    type: "info",
                    showCancelButton: false
                });
                break;
            case "-12":
                swal({
                    title: "Info",
                    text: "No tiene cierre mensual",
                    type: "info",
                    showCancelButton: false
                });
                break;
            case "-1":
                swal({
                    title: "Error",
                    text: "Error al procesar información",
                    type: "error",
                    showCancelButton: false
                });
                break;
            case "0":
                swal({
                    title: "Exito",
                    text: "El Cierre de Mes ha sido Completado con Exito!!!",
                    type: "success",
                    showCancelButton: false
                });
                break;
            case "1":
                swal({
                    title: "Info",
                    text: "Debe Registrar el Tipo de Cambio de Cierre del Mes",
                    type: "info",
                    showCancelButton: false
                });
                break;
            case "2":
                swal({
                    title: "Error",
                    text: "El Periodo: " + document.getElementById("txtPeriodo").value + ", no esta Registrado en CONTABILIDAD",
                    type: "error",
                    showCancelButton: false
                });
                break;
            case "3":
                swal({
                    title: "Error",
                    text: "El Periodo: " + ((document.getElementById("txtPeriodo").value) * 1) + 1 + ", no está Registrado en CONTABILIDAD",
                    type: "error",
                    showCancelButton: false
                });
                break;
            case "4":
                swal({
                    title: "Info",
                    text: "Este periodo se encuentra cerrado",
                    type: "info",
                    showCancelButton: false
                });//.then(function () {
                //    //enviarServidor("grabarCierre?t=1", mostrarRpta, "get", null);
                //    enviarServidor("grabarCierre?t=1" + "&PPeriodo=" + parseInt(document.getElementById("cboAnioP").value) + "&Mes=" + parseInt(document.getElementById("cboMesP").value) + "&BDEmpresaActual=" + document.getElementById("BDEmpresaActual").value, mostrarRpta, "get", null);
                //}, function () { });
                break;
            case "5":
                swal({
                    title: "Exito",
                    text: "Se eliminó correctamente",
                    type: "success",
                    showCancelButton: false
                });
                break;

        }
        if (r.split("|")[0] == "3" && r.split("|")[1] != "" && r.split("|")[1] != undefined) {
            swal({
                title: "Info",
                text: "Debe hacer el cierre del mes: " + r.split("|")[1],
                type: "info",
                showCancelButton: false
            });
        }
        if (r.split("|")[0] == "5" && r.split("|")[1] != "" && r.split("|")[1] != undefined) {
            swal({
                title: "Info",
                text: "Debe eliminar el cierre del mes: " + r.split("|")[1],
                type: "info",
                showCancelButton: false
            });
        }
    }
}
function pl() {
    var p = document.getElementsByClassName("plu"),
        h = document.getElementById("hdfL").value,
        np = p.length;
    for (var i = np; i--;) {
        if (h == "1") {
            p[i].style.visibility = "hidden";
        }
    }
}

function validarEmpresa(rpta) {
    if (rpta == "SinBD") {   //Sin Session
        window.location.href = "/";
        return true;
    }
    if (rpta.split("-")[0] == "CambioBD") {
        swal({
            title: "Info", text: "Se cambió de empresa a: " + rpta.split("-")[1], type: 'info', showConfirmButton: true,
            onClose: function () {
                window.location.href = "/Home/About";
            }
        });
        return true;
    }
    return false;
}