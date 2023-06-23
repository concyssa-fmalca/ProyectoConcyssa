var tipoUsuarioLogin;
const C_ROL_PRINCIPAL = 6;

window.onload = function () {
    var url = "obtenerListaMenurol";
    enviarServidor(url, "get", mostrarLista, null);
    configurarBoton();
    pl();
}

function habilitarBotones() {
    if (tipoUsuarioLogin == "3") {
        var p = document.getElementsByClassName("btn btn-primary pull-right plu");
        np = p.length;
        for (var i = np; i--;) {
            p[i].disabled = true;
        }
    }
}

function configurarBoton() {

    var btnAplicar = document.getElementById("btnAplicar"),
        cboRol = document.getElementById("cboRol");

    


    btnAplicar.onclick = function () {



        if (cboRol.value != -1) {
            var ch = document.getElementsByName("permiso"),
                cl = document.getElementsByName("lectura"),
                nch = ch.length, c, l, cnt = "", id = cboRol.value;
            for (var i = 0; i < nch; i += 1) {
                c = ch[i];
                l = cl[i];
                if (c.checked) {
                    cnt += id;
                    cnt += "¦";
                    cnt += c.getAttribute("data-id");
                    cnt += "¦";
                    cnt += (l.checked ? "1" : "0");
                    cnt += "¦1";
                } else {
                    cnt += id;
                    cnt += "¦";
                    cnt += c.getAttribute("data-id");
                    cnt += "¦0¦0";
                }
                cnt += "¬";
            }
            cnt = cnt.substring(0, cnt.length - 1)
            console.log(cnt.split("¬"));
            //enviarServidor("grabarAcceso", "post", mostrarRpta, cnt);
            enviarServidor1("grabarAcceso", "post", mostrarRpta, cnt);
        }
    }
    cboRol.onchange = function () {

        if (this.value != "-1") {
            if (this.value == C_ROL_PRINCIPAL) {
                document.getElementById("btnAplicar").disabled = true;
                document.getElementById("btnSinAcceso").disabled = true;
                document.getElementById("btnAccesoTotal").disabled = true;
            }
            else {
                document.getElementById("btnAplicar").disabled = false;
                document.getElementById("btnSinAcceso").disabled = false;
                document.getElementById("btnAccesoTotal").disabled = false;
            }
            //enviarServidor("obtenerAccesos/?id=" + this.value, "get", mostrarAcceso, null);
            enviarServidor("obtenerAccesos/?IdPerfil=" + parseInt(this.value), "get", mostrarAcceso, null);
        } else {
            document.getElementById("btnAplicar").disabled = false;
            document.getElementById("btnSinAcceso").disabled = false;
            document.getElementById("btnAccesoTotal").disabled = false;
            limpiar();
        }
    }

}
function mostrarLista(rpta) {
    if (rpta != "") {
        var data = rpta.split("¯");
        var lstMenu = data[1].split("¬");
        var lstRol = data[0].split("¬");
        tipoUsuarioLogin = data[2];
        llenarCombo(lstRol, "cboRol", "Seleccione");
        crearMenu(lstMenu);
        habilitarBotones();
    }
}

function crearMenu(lst) {

    var nrgs = lst.length, rg, cn = "";
    for (var i = 0; i < nrgs; i += 1) {
        rg = lst[i].split("¦");
        if (rg[0] == rg[1]) {

            cn += "<tr><td style='font-weight:bold;cursor:pointer;'><span data-id='" + rg[1] + "-'>►</span>";
            cn += rg[2];
            cn += "</td><td><input type='checkbox' name='permiso' id='" + rg[1] + "-' data-id='" + rg[1] + "' data-p='1'/></td><td><input type='checkbox' name='lectura' data-v='" + rg[1] + "-' data-id='" + rg[1] + "' data-p='1'/></td></tr>";
            cn += crearSubMenu(lst, rg[1], 20, rg[1]);
        }
    }
    cn += "";

    document.getElementById("tbRol").innerHTML = cn;
    configurarToogle();
    configurarseleccionInd();
}
function crearSubMenu(lst, d, px, pr) {
    var nrgs = lst.length, rg, cn = "", cnn = "", id;
    for (var i = 0; i < nrgs; i += 1) {
        rg = lst[i].split("¦");
        if (d == rg[0] && d != rg[1]) {
            id = pr + "-" + rg[1];
            cn += "<tr data-id='" + id + "' style='display:none'>";
            cnn = crearSubMenu(lst, rg[1], (px + 15), id);
            if (cnn != "") {
                cn += "<td style='font-weight: bold;cursor:pointer'><span class='tl' style='padding-left:" + px + "px' data-id=" + id + ">▼</span>";
                cn += rg[2];
                cn += "</td><td><input type='checkbox' name='permiso' id='" + (pr != undefined ? pr : "") + "-" + rg[1] + "' data-id='" + rg[1] + "' data-p='1'/></td><td><input type='checkbox' name='lectura' data-v='" + (pr != undefined ? pr : "") + "-" + rg[1] + "' data-id='" + rg[1] + "' data-p='1'/></td>";

            } else {
                cn += "<td><span style='padding-left:" + px + "px'></span>";
                cn += rg[2];
                cn += "</td><td><input type='checkbox' name='permiso' id='" + (pr != undefined ? pr : "") + "-" + rg[1] + "' data-id='" + rg[1] + "'/></td><td><input type='checkbox' name='lectura' data-v='" + (pr != undefined ? pr : "") + "-" + rg[1] + "' data-id='" + rg[1] + "'/></td>";
            }
            cn += cnn;
            cn += "</tr>";
        }
    }
    cn += "";
    cn == "" ? cn = "" : "";
    return cn;
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

function enviarServidor1(url, tipo, metodo, frm) {

    $.post(url, { datos: frm }, function (data, status) {
        console.log(data);
        if (data == 1) {
            swal({
                title: "Exito",
                text: "Accesos Grabados",
                type: "success",
                showCancelButton: false
            });
        } else {
            swal({
                title: "Error",
                text: "Error al procesar información",
                type: "error",
                showCancelButton: false
            });
        }

    });

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

function configurarToogle() {

    var tb = document.getElementById("tblRol");
    tb.onclick = function (e) {
        var cnt = e.target || e.srcElement;
        var nd = cnt.nodeName;
        if (nd == "SPAN") {
            var id = cnt.getAttribute("data-id");
            var t = (cnt.innerHTML == "►" ? true : false);
            t ? cnt.innerHTML = "▼" : cnt.innerHTML = "►";
            mostrar(id, t);
        } else {
            if (nd == "TD") {
                var id = cnt.children[0].getAttribute("data-id");
                var t = (cnt.children[0].innerHTML == "►" ? true : false);
                t ? cnt.children[0].innerHTML = "▼" : cnt.children[0].innerHTML = "►";
                mostrar(id, t);
            }
        }
    }
}

function mostrar(j, t) {

    var tb = document.getElementById("tbRol");
    var rs = tb.rows.length, tr, a;
    var l = j.length, id;
    for (var i = 0; i < rs; i += 1) {
        tr = tb.rows[i];
        id = tr.getAttribute("data-id");
        if (id != null) {
            a = id.substring(0, l);
            if (a == j) {
                if (id != j) {
                    tr.style.display = t ? "table-row" : "none";
                    var hj = tr.cells[0].children[0].getAttribute("data-id");
                    if (hj != null) {
                        t ? tr.cells[0].children[0].innerHTML = "▼" : tr.cells[0].children[0].innerHTML = "►";
                    }
                }
            }
        }
    }
}

function configurarseleccionInd() {
    var ch = document.getElementsByName("permiso"),
        nch = ch.length, c;
    for (var i = 0; i < nch; i += 1) {
        c = ch[i];
        c.onchange = function (e) {
            var cnt = e.target || e.srcElement;
            var p = cnt.getAttribute("data-p");
            if (p != null) {
                seleccionarChild(cnt.getAttribute("id"), cnt.checked);
            } else {
                if (cnt.checked) {
                    var a = cnt.getAttribute("id").split("-");
                    var na = a.length - 1, v = "", cn;
                    for (var i = 0; i < na; i++) {
                        i == 0 ? v += a[i] + "-" : v += a[i];
                        cn = document.getElementById(v);
                        if (cn != null) {
                            !cn.checked ? cn.checked = cnt.checked : "";
                        }
                    }
                }
            }
        }
    }
    var lc = document.getElementsByName("lectura"),
        nlc = lc.length, l;
    for (var i = 0; i < nlc; i += 1) {
        l = lc[i];
        l.onchange = function (e) {
            var cnt = e.target || e.srcElement;
            var p = cnt.getAttribute("data-p");
            if (p != null) {
                seleccionarChild(cnt.getAttribute("data-v"), cnt.checked, 1);
            } else {
                var n = document.getElementById(cnt.getAttribute("data-v"));
                !n.checked && cnt.checked ? n.checked = true : "";
            }
        }
    }
}

function seleccionarChild(c, v, t) {
    var ch;
    t == undefined ? ch = document.getElementsByName("permiso") : ch = document.getElementsByName("lectura");
    var nch = ch.length, h, k, id, z;
    for (var i = 0; i < nch; i += 1) {
        h = ch[i];
        t == undefined ? k = h.getAttribute("id") : k = h.getAttribute("data-v");
        id = k.substring(0, c.length);
        if (id == c && c != k) {
            if (t != undefined) {
                z = document.getElementById(k)
                !z.checked && v ? z.checked = v : "";
            }
            h.checked = v;
        }
    }
    if (t != undefined) {
        var n = document.getElementById(c);
        !n.checked && v ? n.checked = v : "";
    }
}


function seleccionar(t) {
    var ch = document.getElementsByName("permiso"),
        cl = document.getElementsByName("lectura"),
        nch = ch.length, c, l;
    for (var i = 0; i < nch; i += 1) {
        c = ch[i];
        l = cl[i];
        if (t != undefined) {
            c.checked = true;
        } else {
            c.checked = false;
            l.checked = false;
        }
    }
}
function limpiar() {
    var ch = document.getElementsByName("permiso"),
        cl = document.getElementsByName("lectura"),
        nch = ch.length, c, l;
    for (var i = 0; i < nch; i += 1) {
        c = ch[i];
        l = cl[i];
        c.checked = false;
        l.checked = false;
    }
}

function mostrarRpta(rpta) {
    if (rpta != "") {
        var errorEmpresa = validarEmpresa(rpta);
        if (errorEmpresa) {
            return;
        }

        swal({
            title: "Exito",
            text: "Accesos Grabados",
            type: "success",
            showCancelButton: false
        });
    } else {
        swal({
            title: "Error",
            text: "Error al procesar información",
            type: "error",
            showCancelButton: false
        });
    }
}

function mostrarAcceso(rpta) {
    var ch = document.getElementsByName("permiso"), nch = ch.length, c,
        cl = document.getElementsByName("lectura"), l;
    for (var j = 0; j < nch; j += 1) {
        c = ch[j];
        l = cl[j];
        c.checked = false;
        l.checked = false;
    }
    if (rpta != "") {
        var errorEmpresa = validarEmpresa(rpta);
        if (errorEmpresa) {
            return;
        }

        var data = rpta.split("¬"), nrgs = data.length, r;

        for (var i = 0; i < nrgs; i += 1) {
            r = data[i].split("¦");
            for (var j = 0; j < nch; j += 1) {
                c = ch[j];
                l = cl[j];
                if (c.getAttribute("data-id") == r[1]) {
                    c.checked = true;
                    r[2] == "TRUE" ? l.checked = true : l.checked = false;
                    break;
                }
            }
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