let contadorBase = 0;
let contadorObra = 0;
let contadorAlmacen = 0;
let contarclick = 0;
var ultimaFila = null;
var colorOriginal;
let table;
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;
let limitador = 0;
let valorfor = 1



var DatosModalCargados = false;
function CargarDatosModal() {

    if (!DatosModalCargados) {
        $.ajaxSetup({ async: false });
        CargarBasesObraAlmacenSegunAsignado();
        ObtenerTiposDocumentos();
        CargarCentroCosto();
        CargarTipoDocumentoOperacion()
        CargarSeries();
        CargarMotivoTraslado();
        CargarProveedor();
        CargarVehiculos();
        CargarSolicitante(1);
        CargarSucursales();
        CargarMoneda();
        CargarImpuestos();
        CargarTodosUbigeo();

        DatosModalCargados = true;
    }
    return DatosModalCargados;
}

//document.addEventListener('keydown', function (event) {

//    if (event.key === "Enter") {
//        if ($("#cantidadnuevo").is(":focus")) {
//            AgregarItemV2()    
//        }
//    }
//});

//document.addEventListener('keydown', function (event) {
//    if (event.key === "F2") {
//        $("#NumRef").focus()
//    }
//});

function EsActivo() {
    let TipoArticulo = $("#cboClaseArticulo").val()
    if (TipoArticulo == 3) {
        $("#IdTipoProducto").hide()
    } else {
        $("#IdTipoProducto").show()
    }

}
function getDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
        console.log("PRECIOS " + DecimalesPrecios)
    });
}

function ListarBasesxUsuario() {
    $.post("/Usuario/ObtenerBasesxIdUsuario", function (data, status) {
        let datos = JSON.parse(data);
        $("#IdBase").val(datos[0].IdBase).change();
        if (datos.length == 1) {
            console.log(datos);
            if (datos[0].IdPerfil != 1) {
                $("#IdBase").prop("disabled", false);
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdBase").prop("disabled", true);
            }
        }
    });
}

function validarseriescontableParaCrear() {
    let IdSerie = $("#cboSerie").val();
    let IdDocumento = 1;
    let Fecha = $("#txtFechaContabilizacion").val();
    let Orden = 1;
    let datosrespuesta;
    let estado = 0;
    datosrespuesta = ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden);
    console.log(datosrespuesta);
    if (datosrespuesta.FechaRelacion.length == 0) {

        return false;
    }

    if (datosrespuesta.FechaRelacion.length > 0) {
        for (var i = 0; i < datosrespuesta.FechaRelacion.length; i++) {
            console.log(datosrespuesta.FechaRelacion[i]);
            if (datosrespuesta.FechaRelacion[i].StatusPeriodo == 1) {
                estado = 1;
            }
        }

    }

    if (estado == 0) {
        return false;
    }
    return true
}


function Editar() {
    let varIdMovimiento = $("#txtId").val();
    let varIdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let varNumSerieTipoDocumentoRef = $("#SerieNumeroRef").val();
    let varComentario = $("#txtComentarios").val();
    let varIdCuadrilla = $("#IdCuadrilla").val();
    let varIdResponsable = $("#EntregadoA").val();
    let varTipoTransporte = $("#IdTipoTransporte").val();
    let varIdDestinatario = $("#IdDestinatario").val();
    let varIdMotivoTraslado = $("#IdMotivoTraslado").val();
    let varIdTransportista = $("#IdTransportista").val();
    let varPlacaVehiculo = $("#PlacaVehiculo").val();
    let varMarcaVehiculo = $("#MarcaVehiculo").val();
    let varNumIdentidadConductor = $("#NumIdentidadConductor").val();
    let varNombreConductor = $("#NombreConductor").val();
    let varApellidoConductor = $("#ApellidoConductor").val();
    let varLicenciaConductor = $("#LicenciaConductor").val();
    let varPeso = $("#Peso").val();
    let varBulto = $("#Bulto").val();

    let SGI = $("#txtSGI").val();
    let Anexo = $("#Anexo").val();
    let Ubigeo = $("#DistritoLlegada").val();
    let DistritoLlegada = $("#DistritoLlegada").find('option:selected').text();
    let DireccionLlegada = $("#Direccion").val();

    var TipDoc = $("#IdTipoDocumentoRef").val();
    if (TipDoc != "1") {
        varTipoTransporte = null,
            varIdDestinatario = null,
            varIdMotivoTraslado = null,
            varIdTransportista = null,
            varPlacaVehiculo = null,
            varMarcaVehiculo = null,
            varNumIdentidadConductor = null,
            varNombreConductor = null,
            varApellidoConductor = null,
            varLicenciaConductor = null,
            varPeso = null,
            varBulto = null
    }

    $.post('/Movimientos/UpdateMovimientoSalida', {
        'IdMovimiento': varIdMovimiento,
        'IdTipoDocumentoRef': varIdTipoDocumentoRef,
        'NumSerieTipoDocumentoRef': varNumSerieTipoDocumentoRef,
        'Comentario': varComentario,
        'IdCuadrilla': varIdCuadrilla,
        'IdResponsable': varIdResponsable,
        'TipoTransporte': varTipoTransporte,
        'IdDestinatario': varIdDestinatario,
        'IdMotivoTraslado': varIdMotivoTraslado,
        'IdTransportista': varIdTransportista,
        'PlacaVehiculo': varPlacaVehiculo,
        'MarcaVehiculo': varMarcaVehiculo,
        'NumIdentidadConductor': varNumIdentidadConductor,
        'NombreConductor': varNombreConductor,
        'ApellidoConductor': varApellidoConductor,
        'LicenciaConductor': varLicenciaConductor,
        'Peso': varPeso,
        'Bulto': varBulto,
        'SGI': SGI,
        'CodigoAnexoLlegada': Anexo,
        'CodigoUbigeoLlegada': Ubigeo,
        'DistritoLlegada': DistritoLlegada,
        'DireccionLlegada': DireccionLlegada,
        'IdProveedor': $("#cboProveedor").val(),
        'NroRef': $("#NumRef").val(),
        'FechaDocumento': $("#txtFechaDocumento").val()
    }, function (data, status) {

        if (data != 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            CerrarModal()
            ConsultaServidor()
        } else {
            swal("Error!", "Ocurrio un Error")
            CerrarModal()
        }

    });

}



/*USANDO */
function ObtenerCuadrillas() {
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrilla", { 'estado': 1 }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrilla(cuadrilla, "IdCuadrilla", "Seleccione")
    });
}

function llenarComboCuadrilla(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Codigo + " - " + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(0).change();
}




function ObtenerAlmacenxIdObra() {
    let IdObra = $("#IdObra").val();
    let AlmacenxObra = [];

    for (var i = 0; i < DatosAlmacen.length; i++) {
        if (DatosAlmacen[i].IdObra == IdObra) {
            AlmacenxObra.push(DatosAlmacen[i])
        }
    }

    llenarComboAlmacen(AlmacenxObra, "cboAlmacen", "seleccione")
    llenarComboAlmacen(AlmacenxObra, "cboAlmacenItem", "seleccione")
}

DatosCuadrilla = [];

function ObtenerCuadrillasxIdObra() {

    let IdObra = $("#IdObra").val()

    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        try {
            DatosCuadrilla = JSON.parse(data);
        } catch (e) {
            DatosCuadrilla = [];
        }
    });
    llenarComboCuadrilla(DatosCuadrilla, "IdCuadrilla", "Seleccione")
}


function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();

    let ObrasxBase = [];

    for (var i = 0; i < DatosObra.length; i++) {
        if (DatosObra[i].IdBase == IdBase) {
            ObrasxBase.push(DatosObra[i])
        }
    }

    llenarComboObra(ObrasxBase, "IdObra", "Seleccione")
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
    $("#" + idCombo).prop("selectedIndex", 1).change();
}



function ObtenerTiposDocumentos() {
    $.ajaxSetup({ async: false });
    $.post("/TiposDocumentos/ObtenerTiposDocumentos", { 'estado': 1 }, function (data, status) {
        let tiposdocumentos = JSON.parse(data);
        llenarTiposDocumentos(tiposdocumentos, "IdTipoDocumentoRef", "Seleccione")
    });
}

function llenarTiposDocumentos(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#IdTipoDocumentoRef").val(14)
}


function CargarBase() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBase(base, "IdBase", "Seleccione")
    });
}

function llenarComboBase(lista, idCombo, primerItem) {
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
}


function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")

    });
}

function CargarCentroCosto() {
    $.ajaxSetup({ async: false });
    $.post("/CentroCosto/ObtenerCentroCosto", function (data, status) {
        let centrocosto = JSON.parse(data);
        llenarComboCentroCosto(centrocosto, "cboCentroCosto", "Seleccione")

    });
}

function llenarComboCentroCosto(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCentroCosto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboCentroCosto").val(7)
}
/*END USANDO */






window.onload = function () {
    CargarUnidadMedidaItem()
    CargarGrupoUnidadMedida()
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    CargarBaseFiltro()
    $("#EntregadoA").select2()
    //ObtenerConfiguracionDecimales();
    getDecimales();
    $("#SubirAnexos").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData($("#SubirAnexos")[0]);
        $.ajax({
            url: "GuardarFile",
            type: "POST",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (datos) {
                let data = JSON.parse(datos);
                console.log(data);
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
    $("#cboAgregarArticulo").select2({
        dropdownParent: $("#ModalItem")
    })
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

function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboObraFiltro")

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
    $("#cboObraFiltro").val($("#cboObraFiltro option:first").val());
    ConsultaServidor()

}





function ConsultaServidor() {
    let varIdBaseFiltro = $("#cboObraFiltro").val()
    $.post("../Movimientos/ObtenerMovimientosSalida", { 'IdBase': varIdBaseFiltro, 'FechaInicial': $("#txtFechaInicio").val(), 'FechaFinal': $("#txtFechaFin").val(), 'OpRef': $("#txtOpRef").val() }, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            console.log("ERRRORR")
            return;
        }

        let tr = '';
        $("#tbody_Solicitudes").html(tr);
        let movimientos = JSON.parse(data);
        let total_solicitudes = movimientos.length;
        console.log("cabecera");
        console.log(movimientos);
        for (var i = 0; i < movimientos.length; i++) {
            let varSNGE = "-"
            let varEstadoGE = "-"
            if (movimientos[i].SerieGuiaElectronica) {
                varSNGE = movimientos[i].SerieGuiaElectronica.toUpperCase() + "-" + movimientos[i].NumeroGuiaElectronica
                if (movimientos[i].EstadoFE == 1) {
                    varEstadoGE = "ACEPTADO"
                } else {
                    varEstadoGE = "NO ACEPTADO"
                }
            }
            console.log(movimientos[i].OrigenDespacho)
            if (movimientos[i].OrigenDespacho != "") {
                varSNGE = "APP MOVIL SD"+ movimientos[i].OrigenDespacho.split("SD")[1]??""
            }
            let datosext
            if (movimientos[i].IdDocExtorno == 1) {
                $.post("/Movimientos/ValidarExtorno", { 'IdMovimiento': movimientos[i].IdMovimiento }, function (data, status) {

                    datosext = data.split("|");
                    console.log(datosext);
                });

               
                tr += '<tr>' +
                    '<td>' + (i + 1) + '</td>' +
                    '<td>' + movimientos[i].FechaDocumento.split('T')[0] + '</td>' +
                    '<td>' + movimientos[i].NombUsuario + '</td>' +
                    '<td style="color:red">' + movimientos[i].NombTipoDocumentoOperacion.toUpperCase() + '</td>' +
                    '<td style="color:blue;cursor:pointer;text-decoration:underline" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ',' + movimientos[i].IdDocExtorno + ')">' + movimientos[i].NombSerie.toUpperCase() + '-' + movimientos[i].Correlativo + '</td>' +
                    '<td>' + movimientos[i].TDocumento.toUpperCase() + '</td>' +
                    '<td> Extornado con Doc N°: ' + datosext[1] + '</td>' +
                    '<td>' + varEstadoGE + '</td>' +
                    '<td>' + movimientos[i].NombMoneda + '</td>' +

                    '<td>' + formatNumberDecimales(movimientos[i].Total, 3) + '</td>' +
                    /*    '<td>' + movimientos[i].NombObra + '</td>' +*/

                    '<td>' + movimientos[i].NombAlmacen + '</td>' +
                    '<td>' + movimientos[i].NroRef + '</td>' +
                    '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
                    // '<button class="btn btn-primary" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ',' + movimientos[i].IdDocExtorno + ')">R</button></td>' +
                    //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                    '</tr>';
            } else {
               
                tr += '<tr>' +
                    '<td>' + (i + 1) + '</td>' +
                    '<td>' + movimientos[i].FechaDocumento.split('T')[0] + '</td>' +
                    '<td>' + movimientos[i].NombUsuario + '</td>' +
                    '<td>' + movimientos[i].NombTipoDocumentoOperacion.toUpperCase() + '</td>' +
                    '<td style="color:blue;cursor:pointer;text-decoration:underline" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ',' + movimientos[i].IdDocExtorno + ')">' + movimientos[i].NombSerie.toUpperCase() + '-' + movimientos[i].Correlativo + '</td>' +
                    '<td>' + movimientos[i].TDocumento.toUpperCase() + '</td>' +
                    '<td>' + varSNGE + '</td>' +
                    '<td>' + varEstadoGE + '</td>' +
                    '<td>' + movimientos[i].NombMoneda + '</td>' +

                    '<td>' + formatNumberDecimales(movimientos[i].Total, 3) + '</td>' +
                    /*    '<td>' + movimientos[i].NombObra + '</td>' +*/

                    '<td>' + movimientos[i].NombAlmacen + '</td>' +
                    '<td>' + movimientos[i].NroRef + '</td>' +
                    '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
                    // '<button class="btn btn-primary" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ',' + movimientos[i].IdDocExtorno + ')">R</button></td>' +
                    //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                    '</tr>';
            }



        }

        if (table) {
            table.destroy();

        }
        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);


        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    CargarDatosModal()
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    $("#txtFechaDocumento").val(today)
    $("#txtFechaContabilizacion").val(today)
    $("#lblTituloModal").html("Nueva Salida");
    $("#btnEditar").hide()
    disabledmodal(false);
    let seguiradelante = 'false';
    seguiradelante = ValidacionBases;
    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }




    $("#cboImpuesto").val(2).change();
    //$("#cboSerie").val(1).change();

    $("#cboMoneda").val(1);
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    AbrirModal("modal-form");


    $("#IdCuadrilla").select2();

    //setearValor_ComboRenderizado("cboCodigoArticulo");
    validarseriescontable();

    $("#cboTipoDocumentoOperacion").val(332).change();
    $("#IdTipoDocumentoRef").val(14).change();

    $("#IdDestinatario").val(24163).change();
    $("#IdTransportista").val(24154).change();
    $("#IdMotivoTraslado").val(13).change();

    $("#Peso").val(1);
    $("#Bulto").val(1);

    

    $("#btnGenerarPDF").hide();

    BuscarItemsExp()
}


function OpenModalItem() {
    if ($("#cboClaseArticulo").val() == 1 && $("#IdTipoProducto").val() == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
        return;
    }


    //Cuando se abre agregar Item
    let ClaseArticulo = $("#cboClaseArticulo").val();
    let Moneda = $("#cboMoneda").val();
    if (ClaseArticulo == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
    } else if (Moneda == 0) {
        swal("Informacion!", "Debe Seleccionar Moneda!");
    }
    else {

        $("#cboAlmacenItem").val($("#cboAlmacen").val()); // que salga el almacen por defecto


        $("#cboPrioridadItem").val(2);
        $("#cboClaseArticulo").prop("disabled", true);
        $("#IdTipoProducto").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarGrupoUnidadMedida();
        /* CargarProyectos();*/
        //CargarCentroCostos();
        //CargarAlmacen();
    }
    BuscarCodigoProducto()
}
let contadorAnexo = 0;
function AgregarLineaAnexo(Nombre) {
    contadorAnexo++
    let tr = '';
    tr += `<tr id="filaAnexo` + contadorAnexo + `">
            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/Anexos/`+ Nombre + `" target="_blank">Descargar</a>
            </td>
            <td><button type="button" class="btn btn-xs btn-danger borrar" onclick="EliminarAnexoEnMemoria(`+ contadorAnexo + `)">-</button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}
function EliminarAnexoEnMemoria(contAnexo) {
    $("#filaAnexo" + contAnexo).remove();
}
function AgregarLineaDetalleAnexo(Id, Nombre) {

    let tr = '';
    tr += `<tr>
            <td style="display:none"><input  class="form-control" type="text" value="`+ Id + `" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/SolicitudRQ/Download?ImageName=`+ Nombre + `" >Descargar</a>
            </td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ Id + `,this)"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}


function openContenido(evt, Name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}


function EliminarAnexo(Id, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("/SolicitudRQ/EliminarAnexoSolicitud", { 'IdSolicitudRQAnexos': Id }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });

}


let contador = 0;

function AgregarLinea() {
    let stockproducto = $("#txtStockAlmacenItem").val();
    let IdItem = $("#txtIdItem").val();
    let CodigoItem = $("#txtCodigoItem").val();
    let MedidaItem = $("#cboMedidaItem").val();
    let DescripcionItem = $("#txtDescripcionItem").val();
    let PrecioUnitarioItem = $("#txtPrecioUnitarioItem").val();
    let CantidadItem = $("#txtCantidadItem").val();
    let ProyectoItem = $("#cboProyectoItem").val();
    let CentroCostoItem = $("#cboCentroCostoItem").val();
    let ReferenciaItem = $("#txtReferenciaItem").val();
    let AlmacenItem = $("#cboAlmacenItem").val();
    let PrioridadItem = $("#cboPrioridadItem").val();
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    //txtReferenciaItem

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;


    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    //valdiaciones

    let ValidartCentroCosto = $("#cboCentroCostoItem").val();
    let ValidartProducto = $("#cboMedidaItem").val();
    let ValidarCantidad = $("#txtCantidadItem").val();
    console.log(ValidarCantidad);
    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0" || !$.isNumeric(ValidarCantidad)) {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }
    if (ValidartCentroCosto == 0) {
        swal("Informacion!", "Debe Seleccionar Centro de Costo!");
        return;
    }
    if (ValidartProducto == 0) {

        swal("Informacion!", "Debe Seleccionar Producto!");
        return;
    }



    //validaciones
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    
    IndicadorImpuesto = DatosIndicadorImpuestos
    

    
        Almacen = DatosAlmacen
   


        Proveedor = DatosProveedor
  

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    Proyecto = JSON.parse(data);
    //});

    
    Moneda = DatosMonedas
   
    let arrayIdArticulo = new Array();
    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });

    for (var i = 0; i < arrayIdArticulo.length; i++) {
        if (IdItem == arrayIdArticulo[i]) {
            swal("Informacion!", "Este Item ya fue Agregado");
            return;
        }
    }


    if (limitador >= 30) {
        swal("Informacion!", "Solo se pueden agregar Hasta 30 items");
        return;
    }

    for (var J = 0; J < valorfor; J++) {
        console.log("VUELTAAAAAAAAAAA: " + J)

        limitador++
        contador++;
        let tr = '';

        //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
        //    <option value="0">Seleccione</option>
        //</select>
        tr += `<tr  id="tritem` + contador + `">
   
            <td style="display:none;"><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />

            <input type="hidden" name="txtIdOrigen[]" value="0" id="txtIdOrigen` + contador + `" >
                <input type="hidden" name="txtIdOrigenTabla[]" value="" id="txtIdOrigenTabla` + contador + `" >


            </td>
 <td>`+ CodigoItem + `</td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
        tr += `  <option value="0">Seleccione</option>`;
        for (var i = 0; i < UnidadMedida.length; i++) {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
        tr += `</select>
            </td>
           
           
            <td input style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
        tr += `  <option value="0">Seleccione</option>`;
        for (var i = 0; i < Moneda.length; i++) {
            tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td>
                
                <input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">
                <input class="form-control"  type="hidden" name="txtstockproducto[]" value="`+ stockproducto + `" id="txtstockproducto` + contador + `" disabled>

            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
        
            <td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
        tr += `  <option impuesto="0" value="0">Seleccione</option>`;
        for (var i = 0; i < IndicadorImpuesto.length; i++) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
        tr += `  <option value="0">Seleccione</option>`;
        for (var i = 0; i < Almacen.length; i++) {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>`
        tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
        tr += `  <option value="0">Seleccione</option>`;
        //for (var i = 0; i < LineaNegocio.length; i++) {
        //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
        //}
        tr += `</select>
            </td>`;
        //for (var i = 0; i < CentroCosto.length; i++) {
        //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
        //}

        //for (var i = 0; i < Proyecto.length; i++) {
        //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
        //}
        tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `);restarLimitador()"></button></td>
          </tr>`;

        $("#tabla").find('tbody').append(tr);


        let varMoneda = $("#cboMoneda").val();
        let varTipoCambio = $("#txtTipoCambio").val();
        let varimpuesto = $("#cboImpuesto").val();

        if (varMoneda) {
            $(".MonedaDeCabecera").val(varMoneda);
        }
        if (varTipoCambio) {
            $(".TipoCambioDeCabecera").val(varTipoCambio);
        }
        if (varimpuesto) {
            $(".ImpuestoCabecera").val(varimpuesto);
        }



        $("#txtIdArticulo" + contador).val(IdItem);
        $("#txtCodigoArticulo" + contador).val(CodigoItem);
        $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
        $("#cboUnidadMedida" + contador).val(MedidaItem);
        $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();
        $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
        $("#cboProyecto" + contador).val(ProyectoItem);
        $("#cboAlmacen" + contador).val(AlmacenItem);
        $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

        $("#cboCentroCostos" + contador).val(CentroCostoItem);
        $("#txtReferencia" + contador).val(ReferenciaItem);
        LimpiarModalItem();
        NumeracionDinamica();

        $("#stocknuevo").val('')
        $("#cantidadnuevo").val('')
        $("#cboAgregarArticulo").val(0).change()
    }

    
}


//function LimpiarDatosModalItems() {

//}
function restarLimitador() {
    limitador = limitador - 1
}
function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#cboAlmacen").prop('disabled', valorbolean);
    $("#cboCentroCosto").prop('disabled', valorbolean);
    $("#cboMoneda").prop('disabled', valorbolean);
    $("#cboSerie").prop('disabled', valorbolean);
    //$("#txtFechaDocumento").prop('disabled', valorbolean);
    //$("#txtFechaContabilizacion").prop('disabled', valorbolean);
    $("#cboTipoDocumentoOperacion").prop('disabled', valorbolean);
    $("#IdTipoDocumentoRef").prop('disabled', valorbolean);
    //$("#SerieNumeroRef").prop('disabled', valorbolean);
    $("#IdCuadrilla").prop('disabled', valorbolean);
    $("#IdResponsable").prop('disabled', valorbolean);
    $("#txtComentarios").prop('disabled', valorbolean)
    $("#btn_agregaritem").prop('disabled', valorbolean)
    $("#EntregadoA").prop('disabled', valorbolean)

    if (valorbolean) {
        $("#btnGrabar").hide()

        $("#btnGenerarGuia").show();

        $("#btnExtorno").show();
        $("#total_editar").show();
        $("#total_nuevo").hide();
        $("#btnNuevo").show();
        $("#div_copiarde").hide()

    } else {
        $("#btnGenerarGuia").hide();
        $("#div_copiarde").show()
        $("#btnExtorno").hide();
        $("#total_editar").hide();
        $("#total_nuevo").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
    }

}

let DatosEmpleados = [];

function listarEmpleados() {
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorIdBase", { 'IdBase': $("#IdBase").val() }, function (data, status) {
        try {
            DatosEmpleados = JSON.parse(data);
        } catch (e) {
            DatosEmpleados = [];
        }
    });
    llenarComboEmpleados(DatosEmpleados, "EntregadoA", "Seleccione")
}

function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}


var DatosIndicadorImpuestos = []
function CargarImpuestos() {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        DatosIndicadorImpuestos = JSON.parse(data);
        llenarComboImpuesto(DatosIndicadorImpuestos, "cboImpuesto", "Seleccione")
    });
}

function CargarUnidadMedidaItem() {
    console.log('CargarUnidadMedidaItem');
    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        let unidad = JSON.parse(data);
        llenarComboUnidadMedidaItem(unidad, "cboMedidaItem", "Seleccione")
    });
}

function CargarGrupoUnidadMedida() {
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerGrupoUnidadMedida", { 'estado': 1 }, function (data, status) {
        let grupounidad = JSON.parse(data);
        console.log(grupounidad);
        llenarComboGrupoUnidadMedidaItem(grupounidad, "cboGrupoUnidadMedida", "Seleccione")
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


function CargarDefinicionxGrupo() {
    console.log('CargarDefinicionxGrupo');
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        if (validadJson(data)) {
            let definicion = JSON.parse(data);
            llenarComboDefinicionGrupoUnidadItem(definicion, "cboMedidaItem", "Seleccione")
        } else {
            $("#cboMedidaItem").html('<option value="0">SELECCIONE</option>')
        }

    });
}










function CargarSolicitante(identificar) {
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerUsuarios", function (data, status) {
        let solicitante = JSON.parse(data);
        if (identificar == 1) {
            llenarComboSolicitante(solicitante, "cboTitular", "Seleccione")
        } else {
            llenarComboSolicitante(solicitante, "cboEmpleado", "Seleccione")
        }


    });
}

function CargarSucursales() {
    //$.ajaxSetup({ async: false });
    //$.post("/Sucursal/ObtenerSucursales", function (data, status) {
    //    let sucursales = JSON.parse(data);
    //    llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    //});
}

function CargarDepartamentos() {
    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    });
}

var DatosMonedas = [];

function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        DatosMonedas = JSON.parse(data);
        llenarComboMoneda(DatosMonedas, "cboMoneda", "Seleccione")
    });
}

//function CargarProyectos() {
//    $.ajaxSetup({ async: false });
//    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
//        let proyecto = JSON.parse(data);
//        llenarComboProyectoItem(proyecto, "cboProyectoItem", "Seleccione")
//    });
//}

//function CargarCentroCostos() {
//    $.ajaxSetup({ async: false });
//    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
//        let proyecto = JSON.parse(data);
//        llenarComboCentroCostoItem(proyecto, "cboCentroCostoItem", "Seleccione")
//    });
//}



function CargarTipoDocumentoOperacion() {
    $.ajaxSetup({ async: false });
    $.post("/TipoDocumentoOperacion/ObtenerTipoDocumentoOperacion", { estado: 1 }, function (data, status) {
        let tipodocumentooperacion = JSON.parse(data);
        llenarComboTipoDocumentoOperacion(tipodocumentooperacion, "cboTipoDocumentoOperacion", "Seleccione")
    });
}



function llenarComboSerie(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 2) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i; }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change()
}

function llenarComboImpuesto(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdIndicadorImpuesto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboUnidadMedidaItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboGrupoUnidadMedidaItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGrupoUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboDefinicionGrupoUnidadItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDefinicionGrupo + "'>" + lista[i].DescUnidadMedidaAlt + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



function llenarComboProyectoItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProyecto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboCentroCostoItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCentroCosto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



function llenarComboSolicitante(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUsuario + "'>" + lista[i].NombreUsuario + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboSucursal(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSucursal + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDepartamento + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboMoneda(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdMoneda + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboMoneda").val(1);
}

function llenarComboAlmacen(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdAlmacen + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboAlmacen").prop("selectedIndex", 1).change();
}


function llenarComboTipoDocumentoOperacion(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Tabla == 'OIGE') {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>"; }
        }


    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



//llenarComboTipoDocumentoOperacion
//$(document).on('click', '.borrar', function (event) {
//    event.preventDefault();
//    $(this).closest('tr').remove();

//    let filas = $("#tabla").find('tbody tr').length;
//    console.log("filas");
//    console.log(filas);
//});



function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
    $("#IdTipoProducto").prop("disabled", false)
}


function SetImpuestoDetalle() {

    let varImpuestoCabecera = $("#cboImpuesto").val();
    $(".ImpuestoCabecera").val(varImpuestoCabecera).change();

}





function ValidarMonedaBase() {

    let varMoneda = $("#cboMoneda").val();

    $.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

        if (data == "error") {
            swal("Error!", "Ocurrio un Error")
            return;
        } else {

            let datos = JSON.parse(data);
            if (datos[0].Base) {
                $("#txtTipoCambio").prop('disabled', true);
            } else {
                $("#txtTipoCambio").prop('disabled', false);
            }

        }
    });
    $(".MonedaDeCabecera").val(varMoneda).change();



    let Moneda = $("#cboMoneda").val();
    //$.post("ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {
    //    let dato = JSON.parse(data);
    //    //console.log(dato);
    //    $("#txtTipoCambio").val(dato[0].Rate);

    //});
    let Fecha = $("#txtFechaDocumento").val();
    $.post("ObtenerTipoCambio", { 'Moneda': Moneda, 'Fecha': Fecha }, function (data, status) {
        let dato = JSON.parse(data);
        console.log(dato);
        $("#txtTipoCambio").val(dato.venta);

    });

    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
}


function ObtenerNumeracion() {

    let varSerie = $("#cboSerie").val();

    $.post("/Serie/ValidarNumeracionSerieSolicitudRQ", { 'IdSerie': varSerie }, function (data, status) {
        if (data == 'sin datos') {
            $.post("/Serie/ObtenerDatosxID", { 'IdSerie': varSerie }, function (data, status) {
                let valores = JSON.parse(data);
                $("#txtNumeracion").val(valores[0].NumeroInicial);
            });
        } else {
            let values = JSON.parse(data);
            let Numero = Number(values[0].NumeroInicial);
            $("#txtNumeracion").val(Numero + 1);

        }
    });

}


function GuardarSolicitud() {

    //CerrarModal();
    //ObtenerDatosxID(7463);
    ////table.destroy();
    //ConsultaServidor();
    //return;


    let ArrayGeneral = new Array();


    let arrayFechaNecesaria = new Array();
    let arrayIdMoneda = new Array();
    let arrayTipoCambio = new Array();

    let arrayIndicadorImpuesto = new Array();


    let arrayProveedor = new Array();
    let arrayNumeroFabricacion = new Array();
    let arrayNumeroSerie = new Array();
    let arrayLineaNegocio = new Array();
    let arrayCentroCosto = new Array();
    let arrayProyecto = new Array();

    let arrayIdSolicitudDetalle = new Array();

    //let arrayIdAlmacen = new Array();
    let arrayPrioridad = new Array();
    //$("select[name='cboMoneda[]']").each(function (indice, elemento) {
    //    arrayIdMoneda.push($(elemento).val());
    //});

    //$("input[name='txtTipoCambio[]']").each(function (indice, elemento) {
    //    arrayTipoCambio.push($(elemento).val());
    //});

    //$("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
    //    arrayIndicadorImpuesto.push($(elemento).val());
    //});



    let arrayIdArticulo = new Array();
    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });


    let arraytxtDescripcionArticulo = new Array();
    $("input[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
        arraytxtDescripcionArticulo.push($(elemento).val());
    });



    let arrayIdUnidadMedida = new Array();
    $("select[name='cboUnidadMedida[]']").each(function (indice, elemento) {
        arrayIdUnidadMedida.push($(elemento).val());
    });

    let arrayCantidadNecesaria = new Array();
    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });

    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val());
    });

    let arrayAlmacen = new Array();
    $("input[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacen.push($(elemento).val());
    });

    let arrayReferencia = new Array();
    $("input[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });


    let arrayIdOrigen = new Array();
    $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
        arrayIdOrigen.push($(elemento).val());
    });

    let arrayTablaOrigen = new Array();
    $("input[name='txtIdOrigenTabla[]']").each(function (indice, elemento) {
        arrayTablaOrigen.push($(elemento).val());
    });

    let arrayTxtNombreAnexo = new Array();
    $("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
        arrayTxtNombreAnexo.push($(elemento).val());
    });


    //let arrayCboCuadrillaTabla = new Array();
    //$(".cboCuadrillaTabla").each(function (indice, elemento) {
    //    arrayCboCuadrillaTabla.push($(elemento).val());
    //});

    //let arrayCboResponsableTabla = new Array();
    //$(".cboResponsableTabla").each(function (indice, elemento) {
    //    arrayCboResponsableTabla.push($(elemento).val());
    //});






    //Cabecera
    let IdAlmacen = $("#cboAlmacen").val();
    let IdTipoDocumento = $("#cboTipoDocumentoOperacion").val();
    let IdSerie = $("#cboSerie").val();
    let Correlativo = $("#txtNumeracion").val();
    let IdMoneda = $("#cboMoneda").val();
    let TipoCambio = $("#txtTipoCambio").val();
    let FechaContabilizacion = $("#txtFechaContabilizacion").val();
    let FechaDocumento = $("#txtFechaDocumento").val();
    let IdCentroCosto = $("#cboCentroCosto").val();
    let Comentario = $("#txtComentarios").val();
    let SubTotal = $("#txtTotalAntesDescuento").val();
    let Impuesto = $("#txtImpuesto").val();
    let Total = $("#txtTotal").val();
    let IdCuadrilla = 2582;
    let EntregadoA = 24151
    let IdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let SerieNumeroRef = $("#SerieNumeroRef").val();

    let SGI = $("#txtSGI").val();
    let Anexo = $("#Anexo").val();
    let Ubigeo = $("#DistritoLlegada").val();
    let DistritoLlegada = $("#DistritoLlegada").find('option:selected').text();
    let DireccionLlegada = $("#Direccion").val();

    //END Cabecera

    //Validaciones
    if ($("#cboTipoDocumentoOperacion").val() == 0) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo de Movimiento',
            'error'
        )
        return;
    }
    if ($("#IdCuadrilla").val() == 0) {
        Swal.fire(
            'Error!',
            'Complete el campo Cuadrilla',
            'error'
        )
        return;
    }

    if ($("#EntregadoA").val() == 0) {
        Swal.fire(
            'Error!',
            'Complete el campo Entregado A',
            'error'
        )
        return;
    }

    if ($("#cboTipoDocumentoOperacion").val() != 329) {
        if ($("#IdCuadrilla").val() == 0) {
            Swal.fire(
                'Error!',
                'Complete el campo Cuadrilla',
                'error'
            )
            return;
        }

    }

    if ($("#cboTipoDocumentoOperacion").val() == '329') {
        if ($("#cboProveedor").val() == 0) {
            Swal.fire(
                'Error!',
                'Complete el campo Proveedor',
                'error'
            )
            return;
        }
    }



    if ($("#IdTipoDocumentoRef").val() == 1) {
      
        if ($("#EntregadoA").val() == 0) {
            Swal.fire(
                'Error!',
                'Complete el campo Entregado A',
                'error'
            )
            return;
        }

        if ($("#Peso").val() == 0) {
            Swal.fire(
                'Error!',
                'Peso debe ser mayor a 0',
                'error'
            )
            return;
        }
        if ($("#Bulto").val() == 0) {
            Swal.fire(
                'Error!',
                'Bulto debe ser mayor a 0',
                'error'
            )
            return;
        }
    }



    //End Validaciones




    //let oMovimientoDetalleDTO = {};
    //oMovimientoDetalleDTO.Total = arrayTotal

    let detalles = [];
    if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

        for (var i = 0; i < arrayIdArticulo.length; i++) {
            detalles.push({
                'IdArticulo': parseInt(arrayIdArticulo[i]),
                'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                'IdAlmacen': IdAlmacen,
                'Cantidad': arrayCantidadNecesaria[i],
                'Igv': 0,
                'PrecioUnidadBase': arrayPrecioInfo[i],
                'PrecioUnidadTotal': arrayPrecioInfo[i],
                'TotalBase': arrayTotal[i],
                'Total': arrayTotal[i],
                'CuentaContable': 1,
                'IdCentroCosto': IdCentroCosto,
                'IdAfectacionIgv': 1,
                'Descuento': 0,
                'Referencia': arrayReferencia[i],
                'IdOrigen': arrayIdOrigen[i],
                'TablaOrigen': arrayTablaOrigen[i],
                'IdCuadrilla': $("#IdCuadrilla").val(),
                'IdResponsable': $("#EntregadoA").val(),

            })
        }

    }

    //AnexoDetalle
    let AnexoDetalle = [];
    for (var i = 0; i < arrayTxtNombreAnexo.length; i++) {
        AnexoDetalle.push({
            'NombreArchivo': arrayTxtNombreAnexo[i]
        });
    }
    if (validarseriescontableParaCrear() == false) {
        Swal.fire("Error!", "No Puede Crear este Documento en una Fecha No Habilitada", "error")
        return
    }

    $.ajax({
        url: "UpdateInsertMovimiento",
        type: "POST",
        async: true,
        data: {
            detalles,
            AnexoDetalle,
            //cabecera
            'IdAlmacen': IdAlmacen,
            'IdTipoDocumento': IdTipoDocumento,
            'IdSerie': IdSerie,
            'Correlativo': Correlativo,
            'IdMoneda': IdMoneda,
            'TipoCambio': TipoCambio,
            'FechaContabilizacion': FechaContabilizacion,
            'FechaDocumento': FechaDocumento,
            'IdCentroCosto': IdCentroCosto,
            'Comentario': Comentario,
            'SubTotal': SubTotal,
            'Impuesto': Impuesto,
            'Total': Total,
            'IdCuadrilla': 2582,
            'EntregadoA': 24151,
            'IdTipoDocumentoRef': IdTipoDocumentoRef,
            'NumSerieTipoDocumentoRef': $("#SerieNumeroRef").val(),
            'IdDestinatario': $("#IdDestinatario").val(),
            'IdMotivoTraslado': $("#IdMotivoTraslado").val(),
            'IdTransportista': $("#IdTransportista").val(),
            'PlacaVehiculo': $("#PlacaVehiculo").val(),
            'MarcaVehiculo': $("#MarcaVehiculo").val(),
            'NumIdentidadConductor': $("#NumIdentidadConductor").val(),

            'NombreConductor': $("#NombreConductor").val(),
            'ApellidoConductor': $("#ApellidoConductor").val(),
            'LicenciaConductor': $("#LicenciaConductor").val(),
            'TipoTransporte': $("#IdTipoTransporte").val(),
            //'UbigeoPartida': $("#UbigeoPartida").val(),
            //'UbigeoLlegada': $("#UbigeoLlegada").val(),

            'Peso': $("#Peso").val(),
            'Bulto': $("#Bulto").val(),

            'SGI': SGI,
            'CodigoAnexoLlegada': Anexo,
            'CodigoUbigeoLlegada': Ubigeo,
            'DistritoLlegada': DistritoLlegada,
            'DireccionLlegada': DireccionLlegada,
            'IdProveedor': $("#cboProveedor").val(),
            'NroRef': $("#NumRef").val()

            //end cabecera

            //DETALLE
            /* 'detalles': JSON.parse(detalles)*/

            //END DETALLE
            //'IdSolicitudRQ': varIdSolicitud,
            //'IdAlmacen': varSerie,
            //'Serie': varSerieDescripcion,
            //'Numero': varNumero,
            //'IdSolicitante': varSolicitante,
            //'IdSucursal': varSucursal,
            //'IdDepartamento': varDepartamento,
            //'IdClaseArticulo': varClaseArticulo,
            //'IdTitular': varTitular,
            //'IdMoneda': varMoneda,
            //'TipoCambio': varTipoCambio,
            //'IndicadorImpuesto': 0,
            //'TotalAntesDescuento': varTotalAntesDescuento,
            //'Impuesto': varImpuesto,
            //'Total': varTotal,
            //'FechaContabilizacion': varFechaContabilizacion,
            //'FechaValidoHasta': varFechaValidoHasta,
            //'FechaDocumento': varFechaDocumento,
            //'Comentarios': varComentarios,
            //'Estado': varEstado,
            //'Prioridad': varPrioridad,
            //'IdArticulo': arrayIdArticulo,
            //'Prioridad': arrayPrioridad,
            //'IdUnidadMedida': arrayIdUnidadMedida,
            //'FechaNecesaria': arrayFechaNecesaria,
            //'IdItemMoneda': arrayIdMoneda,
            //'ItemTipoCambio': arrayTipoCambio,
            //'CantidadNecesaria': arrayCantidadNecesaria,
            //'PrecioInfo': arrayPrecioInfo,
            //'IdIndicadorImpuesto': arrayIndicadorImpuesto,
            //'ItemTotal': arrayTotal,
            //'IdAlmacen': arrayAlmacen,
            //'IdProveedor': arrayProveedor,
            //'NumeroFabricacion': arrayNumeroFabricacion,
            //'NumeroSerie': arrayNumeroSerie,
            //'IdLineaNegocio': arrayLineaNegocio,
            //'IdCentroCostos': arrayCentroCosto,
            //'IdProyecto': arrayProyecto,
            //'Referencia': arrayReferencia,
            //'IdSolicitudRQDetalle': arrayIdSolicitudDetalle,
            //'DetalleAnexo': arrayGeneralAnexo
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                //CerrarModal();
                //ModalNuevo();
                CerrarModal();
                ObtenerDatosxID(data);
                //table.destroy();
                ConsultaServidor();
                $.post("/EntradaMercancia/GenerarReporte", { 'NombreReporte': 'SalidaMercancia', 'Formato': 'PDF', 'Id': data }, function (data, status) {
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
                    }
                });


            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!' +data,
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });
}

function limpiarDatos() {

    $("#txtId").val('');
    //$("#cboSerie").val('');
    $("#txtNumeracion").val('');
    $("#cboMoneda").val('');
    $("#txtTipoCambio").val('');
    //$("#cboClaseArticulo").val('');
    //$("#cboEmpleado").val('');
    //$("#txtFechaContabilizacion").val(strDate);
    $("#cboSucursal").val('');
    //$("#txtFechaValidoHasta").val(strDate);
    //$("#cboDepartamento").val('');
    //$("#txtFechaDocumento").val(strDate);
    $("#cboTitular").val('');
    $("#txtTotalAntesDescuento").val('');
    $("#txtComentarios").val('');
    $("#txtImpuesto").val('');
    $("#txtTotal").val('');
    $("#txtEstado").val(1);
    $("#total_items").html('-')
    $("#NombUsuario").html('-')
    $("#CreatedAt").html('-')
    $("#EditedAt").html('-')
    $("#NombUsuarioEdicion").html('-')
    $("#PlacaVehiculo").val('');
    $("#MarcaVehiculo").val('');
    $("#NumIdentidadConductor").val('');
    $("#NombreConductor").val('');
    $("#ApellidoConductor").val('');
    $("#LicenciaConductor").val('');
    $("#cboEstadoFE").val('0');
    $("#SerieNumeroRef").val('');
    $("#NumRef").val('')
    limitador = 0;




}


function ObtenerDatosxID(IdMovimiento) {

    CargarDatosModal()

    $("#txtId").val(IdMovimiento);


    $("#lblTituloModal").html("Editar Salida");
    AbrirModal("modal-form");
    disabledmodal(true);
    CargarProveedor();
    CargarMotivoTraslado();





    CargarVehiculos();

    let EstaExtornado = false

    let IdUsuario = 0;

    $.post('../Movimientos/ObtenerDatosxIdMovimientoOLD', {
        'IdMovimiento': IdMovimiento,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);

            if (movimiento.IdDocExtorno != 0) {
                EstaExtornado = true
            }
            IdUsuario = movimiento.IdUsuario,
                $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(movimiento.SubTotal)
            $("#txtImpuesto").val(movimiento.Impuesto)
            $("#txtTotal").val(formatNumberDecimales(movimiento.Total, 3))
            $("#NumRef").val(movimiento.NroRef)
            $("#cboCentroCosto").val(7)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)

            OcultarCampos();


            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#txtComentarios").val(movimiento.Comentario)
            if (movimiento.NombUsuarioEdicion == "") {
                $("#NombUsuarioEdicion").html("-")
            } else {
                $("#NombUsuarioEdicion").html(movimiento.NombUsuarioEdicion)
            }

            if (movimiento.FechaEdicion == '1990-01-01T00:00:00') {
                $("#EditedAt").html("-")
            } else {
                $("#EditedAt").html(movimiento.FechaEdicion.replace("T", " "))
            }
            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);
            $("#txtComentarios").val(movimiento.Comentario)
            $("#txtTotal_editar").val(formatNumberDecimales(movimiento.Total, 2))
            $("#txtNumeracion").val(movimiento.Correlativo)
            $("#txtTipoCambio").val(formatNumberDecimales(movimiento.TipoCambio, 2));


            $("#IdDestinatario").val(movimiento.IdDestinatario).change()
            $("#IdMotivoTraslado").val(movimiento.IdMotivoTraslado).change()
            $("#IdTransportista").val(movimiento.IdTransportista).change()
            $("#PlacaVehiculo").val(movimiento.PlacaVehiculo).change()
            $("#MarcaVehiculo").val(movimiento.MarcaVehiculo)
            $("#NumIdentidadConductor").val(movimiento.NumIdentidadConductor)

            $("#NombreConductor").val(movimiento.NombreConductor)
            $("#ApellidoConductor").val(movimiento.ApellidoConductor)
            $("#LicenciaConductor").val(movimiento.LicenciaConductor)
            $("#IdTipoTransporte").val(movimiento.TipoTransporte)
            $("#cboProveedor").val(movimiento.IdProveedor).change()


            CargarTodosUbigeo();

            $("#txtSGI").val(movimiento.SGI);
            $("#Anexo").val(movimiento.CodigoAnexoLlegada);
            //$("#DistritoLlegada").val(movimiento.DistritoLlegada);
            $("#DistritoLlegada option").filter(function () {
                return $(this).text() === movimiento.DistritoLlegada.trim();
            }).prop("selected", true);

            $("#Direccion").val(movimiento.DireccionLlegada);



            $("#SerieNumeroRef").val(movimiento.SerieGuiaElectronica + "-" + movimiento.NumeroGuiaElectronica)

            $("#cboEstadoFE").val(movimiento.EstadoFE);
            $("#txtFechaDocumento").val((movimiento.FechaDocumento).split("T")[0])

            $("#txtFechaContabilizacion").val((movimiento.FechaContabilizacion).split("T")[0])
            $("#Peso").val(movimiento.Peso)
            $("#Bulto").val(movimiento.Bulto)
            $("#IdCuadrilla").val("")
            $("#IdCuadrilla").prop("disabled", true)

            // $("#EntregadoA").val(movimiento.EntregadoA).change();


            //AnxoDetalle
            let AnexoDetalle = movimiento.AnexoDetalle;
            let trAnexo = '';


            var TipDoc = $("#IdTipoDocumentoRef").val();
            if (TipDoc == "1") {
                $("#btnGenerarPDF").show();
                $("#btnGenerarGuia").show();
            } else {
                $("#btnGenerarGuia").hide();
                $("#btnGenerarPDF").hide();
            }

            if (movimiento.EstadoFE == 1 && TipDoc == "1") {
                $("#btnGenerarPDF").show();
                $("#btnGenerarGuia").hide();
                //extra
                $("#btnEditar").hide()
                $("#IdTipoDocumentoRef").prop("disabled", true)
                $("#IdCuadrilla").prop("disabled", true)
                $("#EntregadoA").prop("disabled", true)
                $("#SerieNumeroRef").prop("disabled", true)
                $("#txtComentarios").prop("disabled", true)

                $("#IdTipoTransporte").prop("disabled", true)
                $("#IdDestinatario").prop("disabled", true)
                $("#IdMotivoTraslado").prop("disabled", true)
                $("#IdTransportista").prop("disabled", true)
                $("#PlacaVehiculo").prop("disabled", true)
                $("#MarcaVehiculo").prop("disabled", true)
                $("#NumIdentidadConductor").prop("disabled", true)
                $("#NombreConductor").prop("disabled", true)
                $("#ApellidoConductor").prop("disabled", true)
                $("#LicenciaConductor").prop("disabled", true)
                $("#Peso").prop("disabled", true)
                $("#Bulto").prop("disabled", true)
                $("#cboEstadoFE").prop("disabled", true)


            } else if (movimiento.EstadoFE == 0 && TipDoc == "1") {
                $("#btnGenerarPDF").hide();
                $("#btnGenerarGuia").show();
                //extra
                $("#btnEditar").show()
                $("#IdTipoDocumentoRef").prop("disabled", false)
                $("#IdCuadrilla").prop("disabled", false)
                $("#EntregadoA").prop("disabled", false)
                $("#SerieNumeroRef").prop("disabled", true)
                $("#SerieNumeroRef").val("");
                $("#txtComentarios").prop("disabled", false)

                $("#IdTipoTransporte").prop("disabled", false)
                $("#IdDestinatario").prop("disabled", false)
                $("#IdMotivoTraslado").prop("disabled", false)
                $("#IdTransportista").prop("disabled", false)
                $("#PlacaVehiculo").prop("disabled", false)
                $("#MarcaVehiculo").prop("disabled", false)
                $("#NumIdentidadConductor").prop("disabled", false)
                $("#NombreConductor").prop("disabled", false)
                $("#ApellidoConductor").prop("disabled", false)
                $("#LicenciaConductor").prop("disabled", false)
                $("#Peso").prop("disabled", false)
                $("#Bulto").prop("disabled", false)
                $("#cboEstadoFE").prop("disabled", false)
            }
            else {
                $("#btnGenerarPDF").hide();
                $("#btnGenerarGuia").hide();

                //extra
                $("#btnEditar").show()
                $("#IdTipoDocumentoRef").prop("disabled", false)
                $("#IdCuadrilla").prop("disabled", false)
                $("#EntregadoA").prop("disabled", false)
                $("#SerieNumeroRef").prop("disabled", false)
                $("#txtComentarios").prop("disabled", false)

                $("#IdTipoTransporte").prop("disabled", false)
                $("#IdDestinatario").prop("disabled", false)
                $("#IdMotivoTraslado").prop("disabled", false)
                $("#IdTransportista").prop("disabled", false)
                $("#PlacaVehiculo").prop("disabled", false)
                $("#MarcaVehiculo").prop("disabled", false)
                $("#NumIdentidadConductor").prop("disabled", false)
                $("#NombreConductor").prop("disabled", false)
                $("#ApellidoConductor").prop("disabled", false)
                $("#LicenciaConductor").prop("disabled", false)
                $("#Peso").prop("disabled", false)
                $("#Bulto").prop("disabled", false)
                $("#cboEstadoFE").prop("disabled", false)
            }



            for (var k = 0; k < AnexoDetalle.length; k++) {
                trAnexo += `
                <tr>
                   
                    <td>
                       `+ AnexoDetalle[k].NombreArchivo + `
                    </td>
                    <td>
                       <a target="_blank" href="`+ AnexoDetalle[k].ruta + `"> Descargar </a>
                    </td>
                    <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)"></button></td>
                </tr>`;
            }
            $("#tabla_files").find('tbody').append(trAnexo);

            //agrega detalle
            let tr = '';

            let Detalle = movimiento.detalles;
            $("#total_items").html(Detalle.length)
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {
                AgregarLineaDetalle(i, Detalle[i]);
                $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
                $("#IdCuadrilla").val(Detalle[0].IdCuadrilla).change()
                console.log(Detalle[0].IdResponsable)
                $("#EntregadoA").val(Detalle[0].IdResponsable).change()
            }


            //let DetalleAnexo = solicitudes[0].DetallesAnexo;
            //for (var i = 0; i < DetalleAnexo.length; i++) {
            //    AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            //}


        }

    });


    if (EstaExtornado == true || IdUsuario !== +$("#IdUsuarioSesion").val()) {
        $("#btnExtorno").hide();
        $("#btnEditar").hide();
    } else {
        $("#btnExtorno").show();
        $("#btnEditar").show();
    }
    //$("#SerieNumeroRef").prop("disabled", false)
    //$("#IdTipoDocumentoRef").prop("disabled", false)
    //$("#IdCuadrilla").prop("disabled", false)
    //$("#EntregadoA").prop("disabled", false)
    //$("#txtComentarios").prop("disabled", false)
    if ($("#cboTipoDocumentoOperacion").val() == '329') {


        $("#IdCuadrilla").prop('disabled', true)
        $("#EntregadoA").prop('disabled', true)
        $(".devolucion").show()
        $(".cuadrillas").hide()
    } else {

        $("#IdCuadrilla").prop('disabled', false)
        $("#EntregadoA").prop('disabled', false)
        $(".devolucion").hide()
        $(".cuadrillas").show()
    }


}


function CalcularTotalDetalle(contador) {

    let stockdetalleproducto = $("#txtstockproducto" + contador).val();
    let varCantidadNecesaria = $("#txtCantidadNecesaria" + contador).val();
    console.log(stockdetalleproducto + '<' + varCantidadNecesaria)
    if (parseFloat(stockdetalleproducto) < parseFloat(varCantidadNecesaria)) {
        swal("Informacion!", "La Cantidad Supera al stock :" + stockdetalleproducto);
        $("#txtCantidadNecesaria" + contador).val(stockdetalleproducto).change();
        return true;
    }


    let varIndicadorImppuesto = $("#cboIndicadorImpuestoDetalle" + contador).val();
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");


    let varPrecioInfo = $("#txtPrecioInfo" + contador).val();

    let subtotal = varCantidadNecesaria * varPrecioInfo;

    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        varTotal = subtotal;
    } else {
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }
    $("#txtItemTotal" + contador).val(varTotal.toFixed(2)).change();


}

function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected', elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(Number($(elemento).val()));
    });

    //console.log(arrayTotal);
    //console.log(arrayIndicadorImpuesto);

    let subtotal = 0;
    let impuesto = 0;
    let total = 0;

    for (var i = 0; i < arrayPrecioInfo.length; i++) {
        subtotal += (arrayCantidadNecesaria[i] * arrayPrecioInfo[i]);
        total += arrayTotal[i];
    }

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(subtotal.toFixed(2));
    $("#txtImpuesto").val(impuesto.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));

}


function AgregarLineaDetalle(contador, detalle) {

    let tr = '';
    let UnidadMedida;
    let Almacen;
    let IdUnidadMedida = detalle.IdUnidadMedida
    let IdAlmacen = detalle.IdAlmacen
    console.log(detalle);



    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ListarDefinicionGrupoxIdDefinicionSelect", { 'IdDefinicionGrupo': detalle.IdDefinicionGrupoUnidad }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });


    
        Almacen = DatosAlmacen
   

    tr = `<tr>
        <td>`+ detalle.CodigoArticulo + `</td>
        <td>`+ detalle.CodigoArticulo + `</td>

        <td>
          <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo + `" disabled/>
        </td>
        <td>
             <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedidaAlt == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedidaAlt + `" selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedidaAlt + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" value="`+ formatNumberDecimales(detalle.Cantidad, 1) + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="`+ formatNumberDecimales(detalle.PrecioUnidadTotal, 2) + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td style="display:none"><input class="form-control" value="`+ detalle.NombCuadrilla + `" disabled></input></td>
        <td style="display:none"><input class="form-control" value="`+ detalle.NombResponsable + `" disabled></input></td>
        <td>
            <input class="form-control changeTotal" type="text" style="width:100px" value="`+ formatNumberDecimales(detalle.Total, 3) + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
        </td>
        <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == IdAlmacen) {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `" selected>` + Almacen[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="text" style="width:100px" value="`+ detalle.Referencia + `" disabled>
        </td>
        <td>
            <button type="button" class="btn-sm btn btn-danger borrar fa fa-trash" disabled></button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);

    NumeracionDinamica();


}

function EliminarDetalle(IdSolicitudRQDetalle, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleSolicitud", { 'IdSolicitudRQDetalle': IdSolicitudRQDetalle }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });



}

function EnviarTipoCambioDetalle() {

    //let Moneda = $("#cboMoneda").val();

    //$.post("ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {

    //    console.log(data);


    let TpCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(TpCambio);

    //});

}





function BuscarCodigoProducto() {

    let TipoItem = $("#cboClaseArticulo").val();
    let IdAlmacen = $("#cboAlmacenItem").val();
    let IdTipoProducto = $("#IdTipoProducto").val();

    if (IdAlmacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    //$("#ModalListadoItem").modal();
    if (TipoItem == 1) {
        $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, }, function (data, status) {

            if (data == "error") {
                swal("Informacion!", "No se encontro Articulo")
            } else {
                let items = JSON.parse(data);
                console.log(items);
                let tr = '';

                for (var i = 0; i < items.length; i++) {
                    /*if (items[i].Inventario == TipoItem) {*/
                    if (items[i].Stock > 0) {
                        tr += '<tr onclick="SeleccionTrItem(' + items[i].IdArticulo + ')">' +
                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                            '<td>' + items[i].Codigo + '</td>' +
                            '<td>' + items[i].Descripcion1 + '</td>' +
                            '<td>' + items[i].Stock + '</td>' +
                            '<td>' + items[i].UnidadMedida + '</td>' +
                            '</tr>';
                    }
                }

                //} else {
                //    if (TipoItem == 2 && items[i].Inventario == false) {
                //tr += '<tr>' +
                //    '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
                //    '<td>' + items[i].Codigo + '</td>' +
                //    '<td>' + items[i].Descripcion1 + '</td>' +
                //    '<td>' + items[i].Stock + '</td>' +
                //    '<td>' + items[i].UnidadMedida + '</td>' +
                //    '</tr>';
                //    }
                //}

                //}

                $("#tbody_listado_items").html(tr);

                tableItems = $("#tabla_listado_items").DataTable({
                    info: false, "language": {
                        "paginate": {
                            "first": "Primero",
                            "last": "Último",
                            "next": "Siguiente",
                            "previous": "Anterior"
                        },
                        "processing": "Procesando...",
                        "search": "Buscar:",
                        "lengthMenu": "Mostrar _MENU_ registros"
                    }
                });
            }

        });
    } else {
        $.post("/Articulo/ObtenerArticulosActivoFijo",

            function (data, status) {

                if (data == "error") {
                    swal("Informacion!", "No se encontro Articulo")

                } else {

                    let items = JSON.parse(data);
                    //console.log(items);
                    let tr = '';

                    for (var i = 0; i < items.length; i++) {
                        /* if (items[i].Inventario == TipoItem) {*/
                        tr += '<tr onclick="SeleccionTrItem(' + items[i].IdArticulo + ')">' +
                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                            '<td>' + items[i].Codigo + '</td>' +
                            '<td>' + items[i].Descripcion1 + '</td>' +
                            '<td>' + items[i].Stock + '</td>' +
                            '<td>' + items[i].UnidadMedida + '</td>' +
                            '</tr>';
                        //} else {
                        //    if (TipoItem == 2 && items[i].Inventario==false) {
                        //        tr += '<tr>' +
                        //            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
                        //            '<td>' + items[i].Codigo + '</td>' +
                        //            '<td>' + items[i].Descripcion1 + '</td>' +
                        //            '<td>' + items[i].Stock + '</td>' +
                        //            '<td>' + items[i].UnidadMedida + '</td>' +
                        //            '</tr>';
                        //    }
                        //}

                    }

                    $("#tbody_listado_items").html(tr);

                    tableItems = $("#tabla_listado_items").DataTable({
                        "iDisplayLength": 100,
                        "bDestroy": true,
                        info: false, "language": {
                            "paginate": {
                                "first": "Primero",
                                "last": "Último",
                                "next": "Siguiente",
                                "previous": "Anterior"
                            },
                            "processing": "Procesando...",
                            "search": "Buscar:",
                            "lengthMenu": "Mostrar _MENU_ registros"
                        },
                    });

                }

            });
    }

}



//function BuscarListadoProyecto() {

//    $("#ModalListadoProyecto").modal();

//    $.post("/Proyecto/ObtenerProyectos", function (data, status) {

//        if (data == "error") {
//            swal("Info!", "No se encontro Proyectos")
//        } else {
//            let proyectos = JSON.parse(data);
//            console.log(proyectos);
//            let tr = '';

//            for (var i = 0; i < proyectos.length; i++) {

//                tr += '<tr>' +
//                    '<td><input type="radio" clase="" id="rdSeleccionadoProyecto' + proyectos[i].IdProyecto + '"  name="rdSeleccionadoProyecto"  value = "' + proyectos[i].IdProyecto + '" ></td>' +
//                    '<td>' + proyectos[i].Codigo + '</td>' +
//                    '<td>' + proyectos[i].Descripcion + '</td>' +
//                    '</tr>';
//            }

//            $("#tbody_listado_proyecto").html(tr);

//            tableProyecto = $("#tabla_listado_proyecto").DataTable({
//                info: false, "language": {
//                    "paginate": {
//                        "first": "Primero",
//                        "last": "Último",
//                        "next": "Siguiente",
//                        "previous": "Anterior"
//                    },
//                    "processing": "Procesando...",
//                    "search": "Buscar:",
//                    "lengthMenu": "Mostrar _MENU_ registros"
//                }
//            });
//        }

//    });

//}



//function BuscarListadoCentroCosto() {

//    $("#ModalListadoCentroCosto").modal();

//    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {

//        if (data == "error") {
//            swal("Info!", "No se encontro Centro de Costo")
//        } else {
//            let centroCosto = JSON.parse(data);
//            console.log(centroCosto);
//            let tr = '';

//            for (var i = 0; i < centroCosto.length; i++) {

//                tr += '<tr>' +
//                    '<td><input type="radio" clase="" id="rdSeleccionadoCentroCosto' + centroCosto[i].IdCentroCosto + '"  name="rdSeleccionadoCentroCosto"  value = "' + centroCosto[i].IdCentroCosto + '" ></td>' +
//                    '<td>' + centroCosto[i].Codigo + '</td>' +
//                    '<td>' + centroCosto[i].Descripcion + '</td>' +
//                    '</tr>';
//            }

//            $("#tbody_listado_centroCosto").html(tr);

//            tableCentroCosto = $("#tabla_listado_centroCosto").DataTable({
//                info: false, "language": {
//                    "paginate": {
//                        "first": "Primero",
//                        "last": "Último",
//                        "next": "Siguiente",
//                        "previous": "Anterior"
//                    },
//                    "processing": "Procesando...",
//                    "search": "Buscar:",
//                    "lengthMenu": "Mostrar _MENU_ registros"
//                }
//            });
//        }

//    });

//}

function BuscarListadoAlmacen() {
    console.log('BuscarListadoAlmacen');

    let IdObra = $("#IdObra").val();
    console.log(IdObra);

    $("#ModalListadoAlmacen").modal();
    //$.post("../Almacen/ObtenerAlmacen", function (data, status) {
    $.post("../Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Almacen")
        } else {
            let almacen = JSON.parse(data);
            console.log(almacen);
            let tr = '';

            for (var i = 0; i < almacen.length; i++) {

                tr += '<tr>' +
                    '<td><input type="radio" clase="" id="rdSeleccionadoAlmacen' + almacen[i].IdAlmacen + '"  name="rdSeleccionadoAlmacen"  value = "' + almacen[i].IdAlmacen + '" ></td>' +
                    '<td>' + almacen[i].Codigo + '</td>' +
                    '<td>' + almacen[i].Descripcion + '</td>' +
                    '</tr>';
            }

            $("#tbody_listado_almacen").html(tr);

            tableAlmacen = $("#tabla_listado_almacen").DataTable({
                info: false, "language": {
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "lengthMenu": "Mostrar _MENU_ registros"
                }
            });
        }

    });

}





function SeleccionarItemListado() {

    console.log("focus")
    $("#cantidadnuevo").focus()

    let IdArticulo = $('#cboAgregarArticulo').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacen").val();
    if (TipoItem == 3) {
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                try {
                    let datos = JSON.parse(data);
                    console.log(1);
                    $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
                    $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
                    $("#cboGrupoUnidadMedida").prop('disabled', true);
                    $("#txtCodigoItem").val(datos[0].Codigo);
                    $("#txtIdItem").val(datos[0].IdArticulo);
                    $("#txtDescripcionItem").val(datos[0].Descripcion1);
                    $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
                    $("#txtStockAlmacenItem").val(datos[0].Stock);
                    $("#stocknuevo").val(datos[0].Stock);
                    $("#txtPrecioUnitarioItem").val((datos[0].PrecioPromedio).toFixed(DecimalesPrecios))
                } catch (e) {
                    console.log("data")
                }
          
            }
        });
    } else {
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                try {
                    let datos = JSON.parse(data);
                    console.log(datos);
                    $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
                    $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
                    $("#txtCodigoItem").val(datos[0].Codigo);
                    $("#txtIdItem").val(datos[0].IdArticulo);
                    $("#txtDescripcionItem").val(datos[0].Descripcion1);

                    $("#txtPrecioUnitarioItem").val((datos[0].UltimoPrecioCompra).toFixed(DecimalesPrecios));


                    $("#txtStockAlmacenItem").val(datos[0].Stock);
                    $("#stocknuevo").val(datos[0].Stock);
                    $("#txtPrecioUnitarioItem").val((datos[0].PrecioPromedio).toFixed(DecimalesPrecios))
                    $("#txtPrecioUnitarioItemOriginal").val(datos[0].PrecioPromedio);
                    $("#txtPrecioUnitarioItem").prop("disabled", true);
                } catch (e) {
                    console.log("data")
                }

            }
        });
    }

}

function CerrarModalListadoItems() {
    tableItems.destroy();
}


function SeleccionarProyectoListado() {

    let IdProyecto = $('input:radio[name=rdSeleccionadoProyecto]:checked').val();

    $.post("/Proyecto/ObtenerDatosxID", { 'IdProyecto': IdProyecto }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Proyecto")
            tableProyecto.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboProyectoItem").val(datos[0].IdProyecto);
            tableProyecto.destroy();

        }
    });
}

function CerrarModalListadoProyecto() {
    tableProyecto.destroy();
}


function validarStock() {
    let stockalmacen = $("#txtStockAlmacenItem").val();
    let txtCantidadItem = $("#txtCantidadItem").val();
    if (parseFloat(stockalmacen) < parseFloat(txtCantidadItem)) {
        swal("Informacion!", "La Cantidad Supera al stock :" + stockalmacen);
        $("#txtCantidadItem").val(stockalmacen);
    }
}


function SeleccionarCentroCostoListado() {

    let IdCentroCosto = $('input:radio[name=rdSeleccionadoCentroCosto]:checked').val();

    $.post("/CentroCosto/ObtenerDatosxID", { 'IdCentroCosto': IdCentroCosto }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Centro Costo")
            tableCentroCosto.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboCentroCostoItem").val(datos[0].IdCentroCosto);
            tableCentroCosto.destroy();

        }
    });
}


function CerrarModalListadoCentroCosto() {
    tableCentroCosto.destroy();
}


function SeleccionarAlmacenListado() {

    let IdAlmacen = $('input:radio[name=rdSeleccionadoAlmacen]:checked').val();

    $.post("/Almacen/ObtenerDatosxID", { 'IdAlmacen': IdAlmacen }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Almacen")
            tableAlmacen.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboAlmacenItem").val(datos[0].IdAlmacen);
            tableAlmacen.destroy();

        }
    });
}

function CerrarModalListadoAlmacen() {
    tableAlmacen.destroy();
}

//function ObtenerConfiguracionDecimales() {

//    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

//        let datos = JSON.parse(data);
//        DecimalesCantidades = datos[0].Cantidades;
//        DecimalesImportes = datos[0].Importes;
//        DecimalesPrecios = datos[0].Precios;
//        DecimalesPorcentajes = datos[0].Porcentajes;

//    });
//}



function ValidarDecimalesCantidades(input) {

    let cantidad = $("#" + input).val();
    let separar = cantidad.split(".");

    if (separar.length > 1) {
        if ((separar[1]).length > DecimalesCantidades) {
            $("#" + input).val(Number(cantidad).toFixed(DecimalesCantidades));
        }
    }
}

function ValidarDecimalesImportes(input) {
    let cantidad = $("#" + input).val();
    let separar = cantidad.split(".");

    if (separar.length > 1) {
        if ((separar[1]).length > DecimalesCantidades) {
            $("#" + input).val(Number(cantidad).toFixed(DecimalesImportes));
        }
    }
}

function LimpiarModalItem() {

    $("#txtCodigoItem").val("");
    $("#txtDescripcionItem").val("");
    $("#txtStockAlmacenItem").val("");
    $("#txtPrecioUnitarioItem").val("");
    $("#txtCantidadItem").val("");
    $("#cboMedidaItem").val(0);
    $("#cboProyectoItem").val(0);
    //$("#cboCentroCostoItem").val(0);
    //$("#cboAlmacenItem").val(0);
    $("#txtReferenciaItem").val("");

}


function GenerarExtorno() {
    let IdMovimiento = $("#txtId").val();
    Swal.fire({
        title: 'DESEA GENERAR EXTORNO?',
        html: "Se validara si los productos ingresados se encuentran con Stock </br>" +
            "</br>" +
            "Serie Para Extorno </br>" +
            "<Select id='cboSerieExtorno' class='form-control'></select>" +
            "</br>" +
            "Fecha De Documento para Extorno </br>" +
            "<input id='FechDocExtorno' type='date' class='form-control'/>" +
            "</br>" +
            "Fecha de Contabilizacion para Extorno </br>" +
            "<input id='FechContExtorno' type='date' disabled class='form-control'/>" +
            "</br>" +
            "<p>* Las Fechas que se muestran por defecto son las mismas que el documento seleccionado</p>",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {

            var parar = 0;
            $.post("/Movimientos/ValidarExtorno", { 'IdMovimiento': IdMovimiento }, function (data, status) {

                var datos = data.split("|");
                console.log(datos);

                if (Number(datos[0]) > 0) {
                    Swal.fire(
                        'Error!',
                        'Este documento ya ha sido extornado! ' + datos[1],
                        'error'
                    )
                    parar++;
                }


            });

            $.post("ValidarTieneNC", { 'IdMovimiento': IdMovimiento }, function (data, status) {

                if (data > 0) {
                    Swal.fire(
                        'Error!',
                        'Este Documento ya Tiene una Nota de Credito Relacionada',
                        'error'
                    )
                    parar++;
                }


            });

            if (parar > 0) {
                return;
            }




            $.ajax({
                url: "GenerarSalidaExtorno",
                type: "POST",
                async: true,
                data: {
                    'IdMovimiento': IdMovimiento,
                    'Serie': $("#cboSerieExtorno").val(),
                    'FechaDoc': $("#FechDocExtorno").val(),
                    'FechaCont': $("#FechContExtorno").val()
                },
                beforeSend: function () {
                    Swal.fire({
                        title: "Cargando...",
                        text: "Por favor espere",
                        showConfirmButton: false,
                        allowOutsideClick: false
                    });
                },
                success: function (data) {
                    if (data == 1) {
                        Swal.fire(
                            'Correcto',
                            'Proceso Realizado Correctamente',
                            'success'
                        )
                        //swal("Exito!", "Proceso Realizado Correctamente", "success")
                        //table.destroy();
                        ConsultaServidor();

                    } else {
                        Swal.fire(
                            'Error!',
                            'Ocurrio un Error!',
                            'error'
                        )

                    }


                }
            }).fail(function () {
                Swal.fire(
                    'Error!',
                    'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
                    'error'
                )
            });
        }
    })
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerieExtorno(series, "cboSerieExtorno", "Seleccione")
    });
}

function llenarComboSerieExtorno(lista, idCombo, primerItem) {

    $("#FechDocExtorno").val($("#txtFechaDocumento").val())
    $("#FechContExtorno").val($("#txtFechaContabilizacion").val())

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;

    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 1) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();
}


var ValidacionBases = 'false';
function CargarBasesObraAlmacenSegunAsignado() {
    ValidacionBases = 'false';
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerBaseAlmacenxIdUsuarioSession", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            console.log(datos[0]);
            contadorBase = datos[0].CountBase;
            contadorObra = datos[0].CountObra;
            contadorAlmacen = datos[0].CountAlmacen;
            CargarDatosBaseObraAlmacen();
            AbrirModal("modal-form");
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen == 1) {
                console.log("Entro");
                console.log(datos[0].IdObra);
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
                $("#cboAlmacen").prop('disabled', true);

            }
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
            }

            if (contadorBase == 1 && contadorObra != 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
            }
            if (contadorBase != 1 && contadorObra != 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
            }


            ValidacionBases = 'true';
        } else {
            ValidacionBases = 'false';
        }
    });
    return ValidacionBases;
}


function ObtenerDatosDefinicion() {
    let IdDefinicionGrupo = $("#cboMedidaItem").val();
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxIdDefinicionGrupo", { 'IdDefinicionGrupo': IdDefinicionGrupo }, function (data, status) {
        let datos = JSON.parse(data);
        $("#txtPrecioUnitarioItem").val(($("#txtPrecioUnitarioItemOriginal").val() * datos.CantidadAlt).toFixed(DecimalesPrecios));
    });
}

function borrartditem(contador) {
    $("#tritem" + contador).remove()
    CalcularTotales();
    NumeracionDinamica();
    ValidarItems()
}


function nuevo() {
    CerrarModal();
    ModalNuevo();
}

function validarseriescontable() {
    let IdSerie = $("#cboSerie").val();
    let IdDocumento = 1;
    let Fecha = $("#txtFechaContabilizacion").val();
    let Orden = 1;
    let datosrespuesta;
    let estado = 0;
    datosrespuesta = ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden);

    if (datosrespuesta.FechaRelacion.length == 0) {
        swal("Informacion!", "No  se encuentra Fecha de Contabilizacion Activa");
        return true;
    }

    if (datosrespuesta.FechaRelacion.length > 0) {
        for (var i = 0; i < datosrespuesta.FechaRelacion.length; i++) {
            console.log(datosrespuesta.FechaRelacion[i]);
            if (datosrespuesta.FechaRelacion[i].StatusPeriodo == 1) {
                estado = 1;
            }
        }

    }

    if (estado == 0) {
        swal("Informacion!", "No se encuentra Fecha de contabilizacion,verificar periodo contable");
        return true;
    }

}


function ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden) {
    let respustavalidacion;
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerDatosSerieValidacion", { IdSerie, IdDocumento, Fecha, Orden }, function (data, status) {
        if (validadJson(data)) {
            respustavalidacion = JSON.parse(data);
        } else {
            respustavalidacion
        }
    });
    return respustavalidacion;
}


function CargarMotivoTraslado() {
    $.ajaxSetup({ async: false });
    $.post("/EntradaMercancia/ObtenerMotivoTraslado", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            let option = "";
            option = `<option value="0">SELECCIONE MOTIVO TRASLADO</option>`
            for (var i = 0; i < datos.length; i++) {
                option += `<option value="` + datos[i].IdMotivoTraslado + `">` + datos[i].CodigoSunat + '-' + datos[i].Descripcion + `</option>`
            }
            $("#IdMotivoTraslado").html(option);
            $("#IdMotivoTraslado").select2();
        } else {

        }
    });
}


function CargarVehiculos() {
    $.ajaxSetup({ async: false });
    $.post("/Vehiculo/ObtenerVehiculo", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            let option = '<option value="0">SELECCIONE</option>';
            for (var i = 0; i < datos.length; i++) {
                option += `<option value="` + datos[i].Placa + `">` + datos[i].Placa + `</option>`
            }
            $("#PlacaVehiculo").html(option);
            $("#PlacaVehiculo").select2();
        } else {

        }
    });
}


function BuscarVehiculoxPlaca() {

    var Placa = $("#PlacaVehiculo").val();
    $.ajaxSetup({ async: false });
    $.post("/Vehiculo/ObtenerDatosConductorxPlaca", { 'Placa': Placa }, function (data, status) {

        if (validadJson(data)) {
            let datos = JSON.parse(data);
            console.log(datos);
            $("#MarcaVehiculo").val(datos[0].MarcaDescripcion);
            $("#LicenciaConductor").val(datos[0].Licencia);
            $("#NumIdentidadConductor").val(datos[0].NumeroDocumento);

            var NombreApellido = datos[0].RazonSocial;
            var a = NombreApellido.split(" ");
            var Nombre = a[2];
            var Apellido = a[0] + " " + a[1];
            $("#NombreConductor").val(Nombre);
            $("#ApellidoConductor").val(Apellido);
        } else {

        }

    });


}

var DatosProveedor = []

function CargarProveedor() {
    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
        DatosProveedor = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PROVEEDOR</option>`;
        console.log(Proveedor);
        for (var i = 0; i < Proveedor.length; i++) {
            option += `<option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].NumeroDocumento + `-` + Proveedor[i].RazonSocial + `</option>`
        }

        $("#IdTransportista").html(option);
        $("#IdDestinatario").html(option);
        $("#cboProveedor").html(option);
        $("#IdTransportista").select2();
        $("#IdDestinatario").select2();

    });
}

function GenerarGuia() {
    let IdMovimiento = $("#txtId").val();
    Swal.fire({
        title: 'DESEA GENERAR GUIA?',
        text: "Desea generar guia electronica",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar Guia Electronica!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Movimientos/GenerarGuia",
                type: "POST",
                async: true,
                data: {
                    'IdMovimiento': IdMovimiento
                },
                beforeSend: function () {
                    Swal.fire({
                        title: "Cargando...",
                        text: "Por favor espere",
                        showConfirmButton: false,
                        allowOutsideClick: false
                    });
                },
                success: function (data) {
                    let respuetaguia = JSON.parse(data);
                    console.log(respuetaguia);
                    Swal.fire(
                        'Correcto',
                        respuetaguia.Message,
                        'success'
                    )

                    CerrarModal();
                    ObtenerDatosxID(IdMovimiento);
                    ConsultaServidor()

                    //if (data == 1) {
                    //    Swal.fire(
                    //        'Correcto',
                    //        'Proceso Realizado Correctamente',
                    //        'success'
                    //    )
                    //    //swal("Exito!", "Proceso Realizado Correctamente", "success")
                    //    table.destroy();


                    //} else {
                    //    Swal.fire(
                    //        'Error!',
                    //        'Ocurrio un Error!',
                    //        'error'
                    //    )

                    //}


                }
            }).fail(function () {
                Swal.fire(
                    'Error!',
                    'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
                    'error'
                )
            });
        }
    })
}



function AbrirModalEntregas() {
    $("#ModalListadoEntrega").modal();
    listarentregadt()
}

function listarentregadt() {
    tableentrega = $('#tabla_listado_entregas').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/EntradaMercancia/ListarOPDNDTModalOPCH',
            type: 'POST',
            data: {

                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox" id="checkIdOpdn` + full.IdOPDN + `" value="` + full.IdOPDN + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckOPDN()" class="checkIdOpdn"> <label for="cbox2"></label>`
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split("T")[0].toString()
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Referencia
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Total
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_entregas tbody').unbind("dblclick");

    $('#tabla_listado_entregas tbody').on('dblclick', 'tr', function () {
        var data = tableentrega.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        $("#IdProveedor").val(data["IdProveedor"]).change();
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarOPNDDetalle(data);
        $('#ModalListadoEntrega').modal('hide');
        tableentrega.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}


function AgregarSeleccionadOPDN() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    $('.checkIdOpdn').each(function () {
        if (this.checked) {

            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionados").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        let numerospedidos = "";
        $('.checkIdOpdn').each(function () {
            if (this.checked) {
                var data = tableentrega.row($("#" + ($(this).val()))).data();

                AgregarOPNDDetalle(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo'] + ' / ';
            }
        });

        $("#txtComentarios").html('BASADOS EN LAS ENTREGAS ' + numerospedidos)
        $('#ModalListadoEntrega').modal('hide');
        tableentrega.ajax.reload()
    }
}



function AgregarOPNDDetalle(data) {

    $("#IdOPDN").val(data['IdOPDN']);
    $("#IdBase").val(data['IdBase']).change();
    $("#IdObra").val(data['IdObra']).change();
    $("#cboAlmacen").val(data['IdAlmacen']).change();

    let serieycorrelativo = data['NombSerie'] + '-' + data['Correlativo'];

    $.ajaxSetup({ async: false });
    $.post("/EntradaMercancia/ObtenerOPDNDetalle", { 'IdOPDN': data['IdOPDN'] }, function (data, status) {
        let datos = JSON.parse(data);
        for (var k = 0; k < datos.length; k++) {
            /*AGREGAR LINEA*/
            let IdItem = datos[k]['IdArticulo'];
            let CodigoItem = "xxx";
            let MedidaItem = datos[k]['IdDefinicionGrupoUnidad'];
            let DescripcionItem = datos[k]['DescripcionArticulo'];
            let PrecioUnitarioItem = datos[k]['valor_unitario'];
            let CantidadItem = datos[k]['Cantidad'] - datos[k]['CantidadUsada'] - datos[k]['CantidadDevolucion'];

            let CantidadMaxima = datos[k]['Cantidad'] - datos[k]['CantidadUsada'] - datos[k]['CantidadDevolucion'];
            let ProyectoItem = datos[k]['IdArticulo'];
            let CentroCostoItem = datos[k]['IdArticulo'];
            let ReferenciaItem = datos[k]['IdArticulo'];
            let AlmacenItem = datos[k]['IdArticulo'];
            let PrioridadItem = datos[k]['IdArticulo'];
            let IdGrupoUnidadMedida = datos[k]['IdGrupoUnidadMedida'];
            let IdIndicadorImpuesto = datos[k]['IdIndicadorImpuesto'];


            //txtReferenciaItem

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();

            today = yyyy + '-' + mm + '-' + dd;


            let UnidadMedida;
            let IndicadorImpuesto;
            let Almacen;
            let Proveedor;
            let LineaNegocio;
            let CentroCosto;
            let Proyecto;
            let Moneda;

            //valdiaciones

            let ValidartCentroCosto = $("#cboCentroCostoItem").val();
            let ValidartProducto = $("#cboMedidaItem").val();
            let ValidarCantidad = datos[k]['Cantidad'];
            console.log(ValidarCantidad);
            if (CodigoItem.length == 0) {
                swal("Informacion!", "Debe Especificar Un Producto!");
                return;
            }

            if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0") {
                swal("Informacion!", "Debe Especificar Cantidad!");
                return;
            }
            if (ValidartCentroCosto == 0) {
                swal("Informacion!", "Debe Seleccionar Centro de Costo!");
                return;
            }
            if (ValidartProducto == 0) {
                swal("Informacion!", "Debe Seleccionar Producto!");
                return;
            }

            //validaciones
            $.ajaxSetup({ async: false });
            $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
                UnidadMedida = JSON.parse(data);

            });

            
            IndicadorImpuesto = DatosIndicadorImpuestos
            

            
                Almacen = DatosAlmacen
            

                Proveedor = DatosProveedor
            

            //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
            //    LineaNegocio = JSON.parse(data);
            //});

            //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
            //    CentroCosto = JSON.parse(data);
            //});

            //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
            //    Proyecto = JSON.parse(data);
            //});

            
            Moneda = DatosMonedas
          

            contador++;
            let tr = '';

            //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
            //    <option value="0">Seleccione</option>
            //</select>
            tr += `<tr id="tritem` + contador + `">
            <td><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
            }
            tr += `</select>
            </td>
           
           
            <td input style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td>
                <input type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdOPDNDetalle'] + `" id="txtIdOrigen` + contador + `" >
                <input type="hidden" name="txtIdOrigenTabla[]" value="OPDN" id="txtIdOrigenTabla` + contador + `" >
            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
            tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()"></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Almacen.length; i++) {
                tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>`
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            //for (var i = 0; i < LineaNegocio.length; i++) {
            //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
            //}
            tr += `</select>
            </td>`;
            //for (var i = 0; i < CentroCosto.length; i++) {
            //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
            //}

            //for (var i = 0; i < Proyecto.length; i++) {
            //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
            //}
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td> <input class="form-control" type="text" value="BASADO EN LA ENTREGA `+ serieycorrelativo + `" id="txtReferencia` + contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar" onclick="borrartditem(`+ contador + `)">-</button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);


            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();


            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }




            $("#txtIdArticulo" + contador).val(IdItem);
            $("#txtCodigoArticulo" + contador).val(CodigoItem);
            $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
            $("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();
            $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
            /*$("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto).change();;*/



            $("#cboCentroCostos" + contador).val(CentroCostoItem);


            LimpiarModalItem();
        }

        /*AGREGAR LINEA*/

    });


}


function CalculaCantidadMaxima(conta) {
    if (parseFloat($("#txtCantidadNecesaria" + conta).val()) > parseFloat($("#txtCantidadNecesariaMaxima" + conta).val())) {

        $("#txtCantidadNecesaria" + conta).val($("#txtCantidadNecesariaMaxima" + conta).val());
        Swal.fire(
            'Error!',
            'El Item del Pedido tiene como maximo ' + $("#txtCantidadNecesariaMaxima" + conta).val() + ' Cantidades.',
            'error'
        )
    }
}

function OcultarCampos() {
    if ($("#IdTipoDocumentoRef").val() == 1) {
        console.log("mostrars")
        $(".ocultate").show()
        $("#IdDestinatario").val(24163).change();
        $("#IdTransportista").val(24154).change();
        $("#IdMotivoTraslado").val(14).change();
        $("#PlacaVehiculo").val(0).change();
        $("#Peso").val(1);
        $("#Bulto").val(1);
        $("#IdTipoTransporte").val('02')
        $("#SerieNumeroRef").prop("disabled", true)
        $("#SerieNumeroRef").val("")
    } else {
        console.log("ocultars")
        $(".ocultate").hide()
        $("#SerieNumeroRef").prop("disabled", false)
    }

}



function GenerarReporte(id, idextorno) {
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    setTimeout(() => {
        if (idextorno == 1) {
            console.log("extornado")
            $.ajaxSetup({ async: false });
            $.post("/EntradaMercancia/GenerarReporte", { 'NombreReporte': 'SalidaMercanciaExtornado', 'Formato': 'PDF', 'Id': id }, function (data, status) {
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
                    Swal.fire("Error!", "No se pudo Cargar el Reporte", "error")
                }
            });
        }
        else {
            $.ajaxSetup({ async: false });
            $.post("/EntradaMercancia/GenerarReporte", { 'NombreReporte': 'SalidaMercancia', 'Formato': 'PDF', 'Id': id }, function (data, status) {
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
                    Swal.fire("Error!", "No se pudo Cargar el Reporte", "error")
                }
            });
        }
    }, 100)
}


function ObtenerEmpleadosxIdCuadrilla() {

    let IdCuadrilla = $("#IdCuadrilla").val();
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        try {
            let empleados = JSON.parse(data);
            llenarComboEmpleados(empleados, "EntregadoA", "Seleccione")
        } catch (e) {
            console.log(e)
        }
    });
}

function llenarComboEmpleados(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    console.log("Empleados: " + lista.length)
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#EntregadoA").val(0).change();
}
function ObtenerCapataz() {
    let IdCuadrilla = $("#IdCuadrilla").val();
    /* setTimeout(() => {*/
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrilla }, function (data, status) {
        try {
            let capataz = JSON.parse(data);
            $("#EntregadoA").select2("val", capataz[0].IdEmpleado);
        } catch (e) {
            console.log("sin cuadrilla")
        }
    })
    /*}, 1000);*/

}


function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
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
function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}



function GenerarPDF() {

    let IdMovimiento = $("#txtId").val();
    $.post("/Movimientos/GenerarPDF", { 'IdMovimiento': IdMovimiento }, function (data, status) {
        //CerrarModal();
        //ObtenerDatosxID(IdMovimiento);

        Swal.fire(
            'Correcto',
            data.message,
            'success')

        CerrarModal();
        ObtenerDatosxID(IdMovimiento);

        return;
        //let datos;
        //if (validadJson(data)) {
        //    let datobase64;
        //    datobase64 = "data:application/octet-stream;base64,"
        //    datos = JSON.parse(data);
        //    verBase64PDF(datos)
        //} else {
        //    respustavalidacion
        //}
    });

}

function ObtenerCuadrillasTabla(contador) {
    let IdObra = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrillaTabla(cuadrilla, "cboCuadrillaTablaId" + contador, "Seleccione")
    });
}
function llenarComboCuadrillaTabla(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Codigo + " - " + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    //var cbo = document.getElementById(idCombo);
    //if (cbo != null) cbo.innerHTML = contenido;
    $("#cboCuadrillaTablaId" + contador).html(contenido)
    $("#cboCuadrillaTablaId" + contador).val($("#IdCuadrilla").val())
}

function ObtenerEmpleadosxIdCuadrillaTabla(contador) {

    let IdCuadrilla = $("#IdCuadrilla").val();
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTabla(empleados, "cboResponsableTablaId" + contador, "Seleccione")
    });
}

function llenarComboEmpleadosTabla(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    console.log("Empleados: " + lista.length)
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    $("#cboResponsableTablaId" + contador).html(contenido)
    $("#cboResponsableTablaId" + contador).val($("#EntregadoA").val())


}

function SetCuadrillaTabla() {
    //let SetCuadrilla = $("#IdCuadrilla").val()
    //$(".cboCuadrillaTabla").val(SetCuadrilla).change()
}
function SeleccionarEmpleadosTabla(contador) {

    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTablaFila(empleados, "cboResponsableTablaId" + contador, "Seleccione", contador)
    });

}
function llenarComboEmpleadosTablaFila(lista, idCombo, primerItem, contador) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    console.log("Empleados: " + lista.length)
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    $("#cboResponsableTablaId" + contador).html(contenido)


    ObtenerCapatazTablaFila(contador)
}
function ObtenerCapatazTablaFila(contador) {
    let IdCuadrillaFila = $("#cboCuadrillaTablaId" + contador).val();
    /* setTimeout(() => {*/
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrillaFila }, function (data, status) {
        try {
            let capataz = JSON.parse(data);
            $("#cboResponsableTablaId" + contador).val(capataz[0].IdEmpleado).change();
        } catch (e) {
        }
    })
    /*}, 1000);*/

}
function SetearEmpleadosEnTabla() {
    $(".cboResponsableTabla").val($("#EntregadoA").val())

}


function BuscarSGI() {

    let SGI = $("#txtSGI").val();
    $.post("/SalidaMercancia/ObtenerSGI", { 'SGI': SGI }, function (data, status) {
        let datos = JSON.parse(data);

        //$("#Ubigeo").val(datos[0].ubigeo);
        $("#DistritoLlegada option").filter(function () {
            return $(this).text() === datos[0].Distrito.trim();
        }).prop("selected", true);

        //$("#DistritoLlegada").val(datos[0].Distrito.trim());
        $("#Direccion").val(datos[0].Direccion.trim());

    })

}



function CargarTodosUbigeo() {
    $.ajaxSetup({ async: false });
    $.post("/Ubigeo/ObtenerTodosUbigeo", function (data, status) {
        let ubigeo = JSON.parse(data);
        llenarComboUbigeos(ubigeo, "DistritoLlegada", "Seleccione")
    });
}


function llenarComboUbigeos(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function ValidarCamposDevolucion() {
    if ($("#cboTipoDocumentoOperacion").val() == '329') {
        CargarProveedorDevolucion()
        $("#IdCuadrilla").val(2582).change()
        $("#EntregadoA").val(24143).change()
        $("#IdCuadrilla").prop('disabled', true)
        $("#EntregadoA").prop('disabled', true)
        $(".devolucion").show()
        $(".cuadrillas").hide()
    } else {
        $("#IdCuadrilla").val(0).change()
        $("#EntregadoA").val(0).change()
        $("#cboProveedor").val(0).change()
        $("#IdCuadrilla").prop('disabled', false)
        $("#EntregadoA").prop('disabled', false)
        $(".devolucion").hide()
        $(".cuadrillas").show()
    }
}


function CargarProveedorDevolucion() {
    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PROVEEDOR</option>`;

        for (var i = 0; i < Proveedor.length; i++) {
            option += `<option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].NumeroDocumento + `-` + Proveedor[i].RazonSocial + `</option>`
        }

        $("#cboProveedor").html(option);

        $("#cboProveedor").select2();


    });
}

function SeleccionTrItem(ItemCodigo) {

    $("#rdSeleccionado" + ItemCodigo).prop("checked", true);

}

function BuscarItemsExp() {
   
  
    $("#cboAgregarArticulo").select2({
        language: "es",
        width: '100%',

        //theme: "classic",
        async: false,
        ajax: {
            url: "/Articulo/ListarArticulosxAlmacenSelect2",
            type: "post",
            dataType: 'json',
            delay: 250,
            data: function (params) {

                return {
                    searchTerm: params.term, // search term
                    IdTipoProducto: $("#IdTipoProducto").val(),
                    IdAlmacen: $("#cboAlmacen").val()
                };



            },
            processResults: function (response) {

                var results = [];
                $.each(response, function (index, item) {
                    if (item.Stock > 0) {

                        results.push({ id: item.IdArticulo, text: item.Codigo + '-' + item.Descripcion1 })
                    }
                });


                return { results }


            },
            cache: true,
        },
        placeholder: 'Ingrese Nombre de Producto',
        minimunInputLength: 3
    });
}

function AgregarItemV2() {

    if ($("#cboAgregarArticulo").val() == 0 || $("#cboAgregarArticulo").val() == null) {
        swal("Error", "Seleccione un Articulo")
        return
    }

    if ($("#cantidadnuevo").val() <= 0 || $("#cantidadnuevo").val() == null) {
        swal("Error", "Ingrese una Cantidad Valida")
        return
    }


    $("#txtCantidadItem").val($("#cantidadnuevo").val())
    if (+$("#cantidadnuevo").val() > +$("#stocknuevo").val()) {
        swal("Error", "La Cantidad Supera al Stock")
        return
    }
    AgregarLinea()
    ValidarItems()
    $('#cboAgregarArticulo').select2('open');
    
}

function ValidarItems(){
    let arrayIdArticulo = new Array();
    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });

    if (arrayIdArticulo.length > 0) {
        $("#cboClaseArticulo").prop("disabled", true)
        $("#IdTipoProducto").prop("disabled", true)
        $("#IdBase").prop("disabled", true)
        $("#IdObra").prop("disabled", true)
        $("#cboAlmacen").prop("disabled", true)
    } else {
        $("#cboClaseArticulo").prop("disabled", false)
        $("#IdTipoProducto").prop("disabled", false)
        $("#IdBase").prop("disabled", false)
        $("#IdObra").prop("disabled", false)
        $("#cboAlmacen").prop("disabled", false)
    }
}



var DatosBase = [];
var DatosObra = [];
var DatosAlmacen = [];
function CargarDatosBaseObraAlmacen() {


    try {
        $.ajaxSetup({ async: false });
        $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
            DatosBase = JSON.parse(data);

        });
        llenarComboBase(DatosBase, "IdBase", "Seleccione")

    } catch (e) {
        Swal.fire("Error!", "No se pudieron Cargar las Bases", "error");
        return;
    }

    try {
        $.ajaxSetup({ async: false });
        $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
            DatosObra = JSON.parse(data);

        });

    } catch (e) {
        Swal.fire("Error!", "No se pudieron Cargar las Obra", "error");
        return;
    }
    try {
        $.ajaxSetup({ async: false });
        $.post("/Almacen/ObtenerAlmacenxIdUsuario", function (data, status) {
            DatosAlmacen = JSON.parse(data);

        });

    } catch (e) {
        Swal.fire("Error!", "No se pudieron Cargar los Almacenes", "error");
        return;
    }


}