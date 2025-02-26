
function GenerarReporte(Formato) {
    let IdAlmacen = $("#IdAlmacen").val();
    let IdTipoProducto = $("#cboTipoArticulo").val();
    let AnioI = $("#txtAnioI").val();
    let AnioF = $("#txtAnioF").val();
    let MesI = $("#txtMesI").val();
    let MesF = $("#txtMesF").val();

    let respustavalidacion = "";

    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {


            $.ajaxSetup({ async: false });
        $.post("GenerarReporteProyeccion", { 'Formato': Formato, 'IdAlmacen': IdAlmacen, 'IdTipoProducto': IdTipoProducto, 'AnioI': AnioI, 'AnioF': AnioF, 'MesI': MesI, 'MesF': MesF}, function (data, status) {
                let datos;
            if (validadJson(data)) {
                if (Formato == 'excel') {
                    try {
                        datos = JSON.parse(data)

                        const byteString = window.atob(datos.Base64ArchivoPDF);
                        const arrayBuffer = new ArrayBuffer(byteString.length);
                        const int8Array = new Uint8Array(arrayBuffer);
                        for (let i = 0; i < byteString.length; i++) {
                            int8Array[i] = byteString.charCodeAt(i);
                        }

                        const blob = new Blob([int8Array], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
                        const link = document.createElement("a");
                        link.href = window.URL.createObjectURL(blob);
                        link.download = `ProyeccionConsumo.xlsx`;
                        link.click();
                        Swal.close()
                    } catch (e) {
                        Swal.fire("Error", "No se Pudo Generar el Archivo Excel", "error")
                    }
                } else {

                    let datobase64;
                    datobase64 = "data:application/octet-stream;base64,"
                    datos = JSON.parse(data);
                    verBase64PDF(datos)
                    Swal.fire(
                        'Correcto',
                        'Reporte Generado',
                        'success'
                    )
                }
                } else {
                    respustavalidacion;
                    Swal.fire(
                        'Error',
                        'Ocurrio un Error',
                        'error'
                    )
                }
            });
        

    }, 200)

}



function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function CargarObra() {
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", {  }, function (data, status) {
        try {
            let bases = JSON.parse(data);
            llenarComboObra(bases, "IdObra", "TODOS")
        } catch (e) {
            $("#IdObra").html("<option value='0'>TODOS</option>")
        }
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", {"IdObra" : $("#IdObra").val()}, function (data, status) {
        try {
            let bases = JSON.parse(data);
            llenarComboAlmacen(bases, "IdAlmacen", "Seleccione")
        } catch (e) {
            $("#IdAlmacen").html("<option value='0'>TODOS</option>")
        }
    });
}

function llenarComboAlmacen(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdAlmacen + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#IdAlmacen").prop("selectedIndex",1)
}

window.onload = function () {
    CargarObra()
    CargarAnios()
    cargarMeses()
    CargarAlmacen()
};

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
function getCurrentDate() {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    var formattedDate = year + '-' + month + '-' + '01';
    return formattedDate;
}
function getCurrentDateFinal() {
    var date = new Date();

    var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var formattedDate = year + '-' + month + '-' + ultimoDia.getDate();
    return formattedDate
}


function CargarAnios() {
    const anioActual = new Date().getFullYear();
    const listaAnios = [];

    for (let i = 2024; i <= anioActual; i++) {
        listaAnios.push(i);
    }


    let option ='<option value="0">Seleccione</option>'

    for (var i = 0; i < listaAnios.length; i++) {
        option += '<option value="' + listaAnios[i] + '">' + listaAnios[i] +'</option>'
    }

    $("#txtAnio").html(option)
    $("#txtAnio").html(option)


    $("#txtAnio").prop("selectedIndex", listaAnios.length)
    $("#txtAnio").prop("selectedIndex", listaAnios.length)


}


function cargarMeses() {
    let option = '<option value="1">ENERO</option>'
    option += '<option value="2">FEBRERO</option>'
    option += '<option value="3">MARZO</option>'
    option += '<option value="4">ABRIL</option>'
    option += '<option value="5">MAYO</option>'
    option += '<option value="6">JUNIO</option>'
    option += '<option value="7">JULIO</option>'
    option += '<option value="8">AGOSTO</option>'
    option += '<option value="9">SEPTIEMBRE</option>'
    option += '<option value="10">OCTUBRE</option>'
    option += '<option value="11">NOVIEMBRE</option>'
    option += '<option value="12">DICIEMBRE</option>'

    $("#txtMes").html(option)
    $("#txtMes").html(option)
}


function GenerarExcel() {

    let IdAlmacen = $("#IdAlmacen").val();
    let Mes = $("#txtMes").val();
    let Anio = $("#txtAnio").val();
    let CodOp = $("#txtCodigoOP").val();
    let LetraCorrelativo = $("#cboLetraInicial").val();


   
    if (CodOp == '') {
        Swal.fire("Digite un Codigo de Operacion");
        return;
    }

    Swal.fire({
        title: "Cargando...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        $.post('GenerarExcelKardexTributario', {
       
            'IdAlmacen': IdAlmacen,
            'Mes': Mes,
            'Anio': Anio,
            'CodOp': CodOp,
            'LetraCorrelativo': LetraCorrelativo
        }, function (data, status) {
        
            let datos = JSON.parse(data);

            if (datos.status) {
                window.open("/Anexos/" + datos.NombreArchivo + ".xlsx", '_blank', 'noreferrer');
                Swal.fire("Existo!", "Excel Generado Correctamente", "sucess");
            } else {
                Swal.fire("Error!", datos.mensaje, "error");
            }
        });

    }, 200)



}