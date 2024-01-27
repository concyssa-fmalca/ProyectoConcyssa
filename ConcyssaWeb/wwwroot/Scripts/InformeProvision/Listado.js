let table = '';
let contador = 0;
let table_series;

window.onload = function () {
    CargarBaseFiltro()
   /* CargarTipoRegistro();*/
    CargarObra();
    ObtenerTipoRegistroFiltro()
    KeyPressNumber($("#txtFondo"));
    $("#cboObraFiltro").prop("selectedIndex", 1);
    $("#cboPeriodo").val(2024);
    ObtenerSemanas()

};

function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboBaseFiltro", "seleccione")

    });

}


function llenarComboBaseFiltro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdBase + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboBaseFiltro").prop("selectedIndex", 1);
    ObtenerObraxIdBase()

}
function ObtenerObraxIdBase() {
    let IdBase = $("#cboBaseFiltro").val();

    console.log(IdBase);
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {

        if (validadJson(data)) {
            let obra = JSON.parse(data);
            llenarComboObra(obra, "cboObraFiltro", "Seleccione")
        } else {
            //$("#cboMedidaItem").html('<option value="0">SELECCIONE</option>')
        }

        //let obra = JSON.parse(data);
        //llenarComboObra(obra, "IdObra", "Seleccione")
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboObraFiltro").prop("selectedIndex", 0);
}
function ObtenerTipoRegistroFiltro() {

    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerTipoRegistrosAjax", function (data, status) {
        if (validadJson(data)) {
            let TipoRegistroData = JSON.parse(data);
            llenarComboTipoRegistroFiltro(TipoRegistroData, "cboTipoRegistroFiltro", "Seleccione")
        } else { }
    });
}

function llenarComboTipoRegistroFiltro(lista, idCombo, primerItem) {

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboTipoRegistroFiltro").prop("selectedIndex", 1);
}
function ObtenerSemanas() {
    console.log("semanas")
    let ObraSemana = $("#cboObraFiltro").val()
    let AnioSemana = $("#cboPeriodo").val()
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanasXIdObraAnioIdTipoRegistro", { 'IdObra': ObraSemana, 'Anio': AnioSemana, 'IdTipoRegistro': 1 }, function (data, status) {
        if (validadJson(data)) {
            let SemanaData = JSON.parse(data);
            llenarComboSemanaFiltro(SemanaData, "cboSemana", "Seleccione")
        } else {
            var cbo = document.getElementById("cboSemana");
            var contenido = "<option value=0>Seleccione</option>";
            cbo.innerHTML = contenido;
        }
    });
}

function llenarComboSemanaFiltro(lista, idCombo, primerItem) {

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSemana + "'>"+lista[i].Correlativo.toString().padStart(3, '0') + " - " + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboSemana").prop("selectedIndex", 0);
}
const formatear = f => {
    const año = f.getFullYear();
    const mes = ("0" + (f.getMonth() + 1)).substr(-2);
    const dia = ("0" + f.getDate()).substr(-2);
    return `${año}-${mes}-${dia}`
}
//function CargarTipoRegistro() {
//    $.ajaxSetup({ async: false });
//    $.post("ObtenerTipoRegistrosAjax", { 'estado': 1 }, function (data, status) {
//        let tipoRegistros = JSON.parse(data);
//        llenarComboTipoRegistro(tipoRegistros, "cboTipoRegistro", "Seleccione")
//    });
//}


//function llenarComboTipoRegistro(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

function CargarObra() {
    $.ajaxSetup({ async: false });
    $.post("/obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboObra(tipoRegistros, "cboObra", "Seleccione")
    });
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}
function ObtenerReporte() {
    let varIdObraReporte = $("#cboObraFiltro").val();
    let varAnioReporte = $("#cboPeriodo").val();
    let varIdSemanaReporte = $("#cboSemana").val();
    if (varIdSemanaReporte == 0 || varIdSemanaReporte == null ) {
        Swal.fire("Error!", "Seleccione una semana")
        return;
    }
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    setTimeout(() => { 
        $.ajaxSetup({ async: false });
        $.post("GenerarReporte", {
            'NombreReporte': 'InformeProvision', 'Formato': 'PDF', 'IdObra': varIdObraReporte, 'Anio': varAnioReporte, 'IdSemana': varIdSemanaReporte
        }, function (data, status) {
            let datos;
            if (validadJson(data)) {
                let datobase64;
                datobase64 = "data:application/octet-stream;base64,"
                datos = JSON.parse(data);
                //datobase64 += datos.Base64ArchivoPDF;
                //$("#reporteRPT").attr("download", 'Reporte.' + "pdf");
                //$("#reporteRPT").attr("href", datobase64);
                //$("#reporteRPT")[0].click();
                verBase64PDF(datos)
                Swal.fire(
                    'Correcto',
                    'Reporte Generado Correctamente',
                    'success'
                )
            } else {
                console.log("error");
            }
        });
    }, 100)
}


     //$.ajaxSetup({ async: false });
     //$.post("GenerarReporte", { 'NombreReporte': 'InformeProvision', 'Formato': 'PDF', 'IdObra': IdObraReporte, 'Anio' : varAnioReporte,'NumeroSemana':varNumeroSemanaReporte }, function (data, status) {
     //    let datos;
     //    if (validadJson(data)) {
     //        let datobase64;
     //        datobase64 = "data:application/octet-stream;base64,"
     //        datos = JSON.parse(data);
     //        datobase64 += datos.Base64ArchivoPDF;
     //        $("#reporteRPT").attr("download", 'Reporte.' + "pdf");
     //        $("#reporteRPT").attr("href", datobase64);
     //        $("#reporteRPT")[0].click();
     //    } else {
     //        respustavalidacion
     //    }
     //});
 function verBase64PDF(datos) {
    //var b64 = "JVBERi0xLjcNCiWhs8XXDQoxIDAgb2JqDQo8PC9QYWdlcyAyIDAgUiAvVHlwZS9DYXRhbG9nPj4NCmVuZG9iag0KMiAwIG9iag0KPDwvQ291bnQgMS9LaWRzWyA0IDAgUiBdL1R5cGUvUGFnZXM+Pg0KZW5kb2JqDQozIDAgb2JqDQo8PC9DcmVhdGlvbkRhdGUoRDoyMDIyMDkyODE2NDAzMCkvQ3JlYXRvcihQREZpdW0pL1Byb2R1Y2VyKFBERml1bSk+Pg0KZW5kb2JqDQo0IDAgb2JqDQo8PC9Db250ZW50cyA1IDAgUiAvTWVkaWFCb3hbIDAgMCA2MTIgNzkyXS9QYXJlbnQgMiAwIFIgL1Jlc291cmNlczw8L0ZvbnQ8PC9GMSA2IDAgUiA+Pi9Qcm9jU2V0IDcgMCBSID4+L1R5cGUvUGFnZT4+DQplbmRvYmoNCjUgMCBvYmoNCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMzExPj5zdHJlYW0NCnicvZPNasMwEITvBr/DHFtot7LiH/mYtMmhECjU9C5i2VGw5WDJpfTpayckhUCDSqGSDjsHfcPOshzPYbAowoBhun0dBg+rCIzxDEUVBklGsyxhyDgnLhhDUYbBDeZ41e2+UXh5WmGlx+IWxS4MlsWRdmRE7MBIc+LJ+DUVglImToxiqy3GJ2Fb2TQoVdsZ63rpdGdA+7JCNZHvvdhpTBmLT+zdYB2qrsdgFbSB2yq86d4NssFabbbS6I2FG1zXa9lYwrrrFZz6cIS5KdFO0sc24ZQl/GR7AfCRPiZckIjPuf0K/5P0sY1SEnl6tbfFmJ+p7/A5HR9zH18WUx6fR/nnVr1zTnJOec79cvbhjQUT/z63ZNzZaHZ9bhdy+a7MQRMeO+O0GVSJcQn3slbgIKJv3y8shB6ADQplbmRzdHJlYW0NCmVuZG9iag0KNiAwIG9iag0KPDwvQmFzZUZvbnQvSGVsdmV0aWNhL0VuY29kaW5nL1dpbkFuc2lFbmNvZGluZy9OYW1lL0YxL1N1YnR5cGUvVHlwZTEvVHlwZS9Gb250Pj4NCmVuZG9iag0KNyAwIG9iag0KWy9QREYvVGV4dF0NCmVuZG9iag0KeHJlZg0KMCA4DQowMDAwMDAwMDAwIDY1NTM1IGYNCjAwMDAwMDAwMTcgMDAwMDAgbg0KMDAwMDAwMDA2NiAwMDAwMCBuDQowMDAwMDAwMTIyIDAwMDAwIG4NCjAwMDAwMDAyMDkgMDAwMDAgbg0KMDAwMDAwMDM0MyAwMDAwMCBuDQowMDAwMDAwNzI2IDAwMDAwIG4NCjAwMDAwMDA4MjUgMDAwMDAgbg0KdHJhaWxlcg0KPDwNCi9Sb290IDEgMCBSDQovSW5mbyAzIDAgUg0KL1NpemUgOC9JRFs8NEY2MkQwQTkwNDlFOUM1N0NGQzRCODEzRTVCNjhDNUI+PDRGNjJEMEE5MDQ5RTlDNTdDRkM0QjgxM0U1QjY4QzVCPl0+Pg0Kc3RhcnR4cmVmDQo4NTUNCiUlRU9GDQo=";
    var b64 = datos.Base64ArchivoPDF;
    // aquí convierto el base64 en caracteres
    var characters = atob(b64);
    // aquí convierto todo a un array de bytes usando el codigo de cada caracter:
    var bytes = new Array(characters.length);
    for (var i = 0; i < characters.length; i++) {
        bytes[i] = characters.charCodeAt(i);
    }
    // en este punto ya tengo un array de bytes,
    // (supongo que es algo similar a lo que te llega de respuesta)
    // el siguiente paso sería convertir este array en un typed array
    // para construir el blob correctamente:
    var chunk = new Uint8Array(bytes);

    // se construye el blob con el mime type respectivo
    var blob = new Blob([chunk], {
        type: 'application/pdf'
    });

    // se crea un object url con el blob para usarlo:
    var url = URL.createObjectURL(blob);

    // y de esta manera simplemente lo abro en una nueva ventana:
    window.open(url, '_blank');
}
function ResetSemana() {

    $("#cboSemana").prop("selectedIndex", 0);
}


