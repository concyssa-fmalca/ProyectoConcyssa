
window.onload = function () {
    var url = "obtenerPeriodo";
    enviarServidor(url, "get", mostrarLista, null);
    configurarBotonGrabar();
    var lstm = ["1¦Enero", "2¦Febrero", "3¦Marzo", "4¦Abril", "5¦Mayo", "6¦Junio", "7¦Julio", "8¦Agosto", "9¦Septiembre", "10¦Octubre", "11¦Noviembre", "12¦Diciembre"];
    var lsta = ["2010¦2010", "2011¦2011", "2012¦2012", "2013¦2013", "2014¦2014", "2015¦2015", "2016¦2016", "2017¦2017"];
    llenarCombo(lstm, "cboMes");
    llenarCombo(lsta, "cboAnio");
}
function mostrarLista(rpta) {
    if (rpta != "") {
        var data = rpta.split("-");
        document.getElementById("cboMes").value = data[0];
        document.getElementById("cboAnio").value = data[1];
    }
}
function enviarServidor(url, tipo, metodo, frm) {
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
    }
    xhr.send(frm);
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
function configurarBotonGrabar() {

    var doc = document,
        btng = doc.getElementById("btnGrabar"),
        cbom = doc.getElementById("cboMes"),
        cboa = doc.getElementById("cboAnio");
    btng.onclick = function () {
        var prdo = cbom.value + "-" + cboa.value;
        enviarServidor("cambiarPeriodo/?prdo=" + prdo, "get", mostrarRpta, null)
    }
}
function mostrarRpta(rpta) {
    if (rpta != "") {
        swal({
            title: "Exito",
            text: "Perido Cambiado",
            type: "success",
            showCancelButton: false
        });
    } else {
        swal({
            title: "error",
            text: "Error al procesar",
            type: "error",
            showCancelButton: false
        });
    }
}