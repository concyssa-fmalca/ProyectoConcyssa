
let table = "";
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;


function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}

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
    let IdBaseFiltro = $("#cboBaseFiltro").val();

    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBaseFiltro }, function (data, status) {

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
    $("#cboObraFiltro").prop("selectedIndex", 1);
    ObtenerCuadrillaxIdObra()
}
function ObtenerCuadrillaxIdObra() {
    let tieneElementosParaQuitarn = $("#cboCuadrillaSeleccionada option").length > 0;

    if (tieneElementosParaQuitarn) {
        // Vacía el select "llenar" si ya tiene elementos
        $("#cboCuadrillaSeleccionada").empty();
    }
    console.log(3);
    let IdObra = $("#cboObraFiltro").val();
    console.log(IdObra);
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboCuadrilla(almacen, "cboCuadrillaFiltro")
    });
    console.log(4);
}



window.onload = function () {
    CargarBaseFiltro()
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
};

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

function formatNumber(num) {
    if (!num || num == 'NaN') return '-';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}

function llenarComboCuadrilla(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" +lista[i].Codigo +" - "+ lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboCuadrillaFiltro").prop("selectedIndex", 1);
}


function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function SeleccionarCuadrilla() {
    $("#cboCuadrillaFiltro option:selected").each(function () {
        let valor = $(this).val();
        let texto = $(this).text();

        // Verifica si el elemento ya existe en el select "llenar"
        var existeElemento = $("#cboCuadrillaSeleccionada option[value='" + valor + "']").length > 0;
        if (!existeElemento) {
            // Copia el elemento al select "llenar" si no existe
            $("#cboCuadrillaSeleccionada").append($('<option>', {
                value: valor,
                text: texto
            }));
        } else {
            swal("Aviso!", "Esta Cuadrilla ya fue seleccionada")
        }
    })
}

function QuitarCuadrilla() {
    var elementosSeleccionados = $("#cboCuadrillaSeleccionada option:selected");

    // Elimina los elementos seleccionados del select "llenar"
    elementosSeleccionados.each(function () {
        $(this).remove();
    });
}
function PasarTodasCuadrillas() {
    var tieneElementos = $("#cboCuadrillaSeleccionada option").length > 0;

    if (tieneElementos) {
        // Vacía el select "llenar" si ya tiene elementos
        $("#cboCuadrillaSeleccionada").empty();
    }

    // Obtén todos los elementos del select "nuevo"
    var elementosNuevo = $("#cboCuadrillaFiltro option");

    // Copia todos los elementos al select "llenar"
    elementosNuevo.each(function () {
        var valor = $(this).val();
        var texto = $(this).text();

        // Verifica si el elemento ya existe en el select "llenar"
        var existeElemento = $("#cboCuadrillaSeleccionada option[value='" + valor + "']").length > 0;
        if (!existeElemento) {
            // Copia el elemento al select "llenar" si no existe
            $("#cboCuadrillaSeleccionada").append($('<option>', {
                value: valor,
                text: texto
            }));
        }
    });
}

function QuitarTodasCuadrillas() {
    let tieneElementosParaQuitar = $("#cboCuadrillaSeleccionada option").length > 0;

    if (tieneElementosParaQuitar) {
        // Vacía el select "llenar" si ya tiene elementos
        $("#cboCuadrillaSeleccionada").empty();
    }
}

function GenerarReporte() {

    // Obtiene todos los elementos del select "llenar"
    var elementos = $("#cboCuadrillaSeleccionada option");

    // Crea un array para almacenar los valores
    var valores = [];

    // Itera sobre los elementos y agrega los valores al array
    elementos.each(function () {
        var valor = $(this).val();
        valores.push(valor);
    });

    // Muestra el array en la consola
    console.log(valores);

    if (valores.length == 0) {
        swal("Error!", "Seleccione al menos una Cuadrilla")
        return;
    }


    let CuadrillasParaConsulta = ""

    for (var i = 0; i < valores.length; i++) {
        if (i < (valores.length) - 1) {
            CuadrillasParaConsulta += valores[i] + ","
        } else {
            CuadrillasParaConsulta += valores[i]
        }
    }
    /*console.log(CuadrillasParaConsulta)*/
    let varCuadrillasRPT = CuadrillasParaConsulta
    let varMaterialesRPT 
    let varAuxiliaresRPT
    let varServiciosRPT
    let varExtornosRPT
    let varFechaInicioRPT = $("#txtFechaInicio").val()
    let varFechaFinRPT = $("#txtFechaFin").val()

    $("#chkIncMateriales").is(':checked') ? varMaterialesRPT = true : varMaterialesRPT = false
    $("#chkIncAuxiliares").is(':checked') ? varAuxiliaresRPT = true : varAuxiliaresRPT = false
    $("#chkIncServicios").is(':checked') ? varServiciosRPT = true : varServiciosRPT = false
    $("#chkIncExtornos").is(':checked') ? varExtornosRPT = true : varExtornosRPT = false


    if (varMaterialesRPT == false && varAuxiliaresRPT == false && varServiciosRPT == false && varExtornosRPT == false) {
        swal("Error!", "Seleccione al menos una Casilla de la Sección 'Incluir'")
        return;
    }

    $.ajaxSetup({ async: false });
    $.post("GenerarReporte", {                                                                                                                                    
        'NombreReporte': 'InformeCuadrillas', 'Formato': 'PDF', 'Cuadrillas': varCuadrillasRPT, 'Materiales': varMaterialesRPT, 'Auxiliares': varAuxiliaresRPT, 'Servicios': varServiciosRPT, 'Extornos': varExtornosRPT, 'FechaInicioS': varFechaInicioRPT, 'FechaFin': varFechaFinRPT
    },
        
        function (data, status) {
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
        } else {
            console.log("error");
        }
    });
}

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