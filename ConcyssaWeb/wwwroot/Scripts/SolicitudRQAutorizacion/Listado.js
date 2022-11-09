let table = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;
var ultimaFila = null;
var colorOriginal;

window.onload = function () {
    var url = "ObtenerSolicitudesxAutorizar";
    ObtenerConfiguracionDecimales();
    ConsultaServidor(url);

    $.post('/Usuario/ObtenerUsuariosAutorizadores', function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let datos = JSON.parse(data);
        llenarComboAutorizadores(datos, "CboAutorizador", "Seleccione")
    });

    $("#FechaInicio").datepicker();
    $("#FechaFinal").datepicker();
    $("#FechaInicio").datepicker("option", "dateFormat", 'dd/mm/yy');
    $("#FechaFinal").datepicker("option", "dateFormat", 'dd/mm/yy');


};

function BuscarPorFechas() {
    table.destroy();
    $("#tbody_detalle").html("");
    $("#NumeracionSolicitud").html("");
    ConsultaServidor("ObtenerSolicitudesxAutorizar");

}

function llenarComboAutorizadores(lista, idCombo, primerItem) {
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

function ConsultaServidor(url) {


    let FechaInicio = $("#FechaInicio").val();
    let FechaFinal = $("#FechaFinal").val();
    let Estado = $("#Estado").val();
    let Autorizador = $("#CboAutorizador").val();

    if (Autorizador == null || Autorizador == "" || Autorizador == "null") {
        Autorizador = 0;
    }
    //console.log(Autorizador);

    var date = new Date();
    var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    var mes = date.getMonth() + 1;
    var year = date.getFullYear();

    if (FechaInicio == "") {
        FechaInicio = '01/' + (mes < 10 ? '0' : '') + mes + '/' + year;
        //FechaInicio = "01-01-1999";
    }
    if (FechaFinal == "") {
        FechaFinal = ultimoDia.getDate() + '/' + (mes < 10 ? '0' : '') + mes + '/' + year;
        //FechaFinal = "01-01-2025";
    }

    if (Estado == 1) {
        $("#IdAprobarTodo").prop("disabled", false);
        $("#IdRechazarTodo").prop("disabled", false);
        $("#IdGuardarAuto").prop("disabled", false);
    } else {
        $("#IdAprobarTodo").prop("disabled", true);
        $("#IdRechazarTodo").prop("disabled", true);
        $("#IdGuardarAuto").prop("disabled", true);
    }


    $.post(url, { 'FechaInicio': FechaInicio, 'FechaFinal': FechaFinal, 'Estado': Estado, 'IdAutorizador': Autorizador }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


        //console.log(data);
        if (data == "error") {
            $("#tbody_SolicitudAutorizacion").html("");
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let solicitudes = JSON.parse(data);
        let total_solicitudes = solicitudes.length;

        console.log(solicitudes);

        for (var i = 0; i < solicitudes.length; i++) {

            if (Estado == 1) {
                let respuesta = validarsipuedeaprobar(solicitudes[i].IdSolicitud, solicitudes[i].IdEtapa);
                if (respuesta == false) {
                    tr += '<tr style="display:none" id="solicitud' + solicitudes[i].IdSolicitud + '" onclick="FilaSeleccionada(' + "'" + solicitudes[i].NumeroSolicitud + "'" + ',' + solicitudes[i].IdSolicitud + ',' + solicitudes[i].UsuarioAprobador + ',' + solicitudes[i].IdSolicitudModelo + ',' + solicitudes[i].IdEtapa + ',' + solicitudes[i].IdClaseArticulo + ')">';
                } else {
                    tr += '<tr id="solicitud' + solicitudes[i].IdSolicitud + '" onclick="FilaSeleccionada(' + "'" + solicitudes[i].NumeroSolicitud + "'" + ',' + solicitudes[i].IdSolicitud + ',' + solicitudes[i].UsuarioAprobador + ',' + solicitudes[i].IdSolicitudModelo + ',' + solicitudes[i].IdEtapa + ',' + solicitudes[i].IdClaseArticulo + ')">';
                }
            } else {
                tr += '<tr id="solicitud' + solicitudes[i].IdSolicitud + '" onclick="FilaSeleccionada(' + "'" + solicitudes[i].NumeroSolicitud + "'" + ',' + solicitudes[i].IdSolicitud + ',' + solicitudes[i].UsuarioAprobador + ',' + solicitudes[i].IdSolicitudModelo + ',' + solicitudes[i].IdEtapa + ',' + solicitudes[i].IdClaseArticulo + ')">';
            }





            tr += '<td>';
            if (solicitudes[i].Estado == "Aprobado" || solicitudes[i].FechaAprobacion != "") {
                tr += '<i class="fa fa-check" aria-hidden="true"></i>';
            } else if (solicitudes[i].Estado == "Rechazado") {
                tr += '<i class="fa fa-times" aria-hidden="true"></i>';
            } else {
                tr += '<input style="display:none" type = "checkbox" class="checkbox_inputs" name = "checkbox_inputs" id = "checkbox' + solicitudes[i].IdSolicitud + '" value = "' + solicitudes[i].IdSolicitud + '" IdAutorizador = "' + solicitudes[i].UsuarioAprobador + '" IdSolicitudModelo = "' + solicitudes[i].IdSolicitudModelo + '" />';
            }
            tr += '</td > ' +
                '<td>' + solicitudes[i].NumeroSolicitud + '</td>' +
                '<td>' + solicitudes[i].NombEtapa.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].Solicitante.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].Area.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].TipoArticulo.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].Moneda.toUpperCase() + '</td>' +
                //'<td>' + solicitudes[i].Impuesto.toUpperCase() + '</td>' +
                '<td>' + formatNumberDecimales(solicitudes[i].Total,2) + '</td>' +
                '<td>' + solicitudes[i].Prioridad.toUpperCase() + '</td>' +
                '<td> <button class="btn btn-sm-red" onclick="modalHistorialEstado(' + solicitudes[i].IdSolicitud +')">E</button> </td>';

            var fechaSplit = (solicitudes[i].FechaCreacion.substring(0, 10)).split("-");
            //var fecha = fechaSplit[0] + "/" + fechaSplit[1] + "/" + fechaSplit[2];
            var fecha = fechaSplit[2] + "/" + fechaSplit[1] + "/" + fechaSplit[0];
            tr += '<td>' + fecha + '</td>';

            var fechaSplitvalidohasta = (solicitudes[i].FechaValidoHasta.substring(0, 10)).split("-");
            //var fechavalidohasta = fechaSplitvalidohasta[0] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[2];
            var fechavalidohasta = fechaSplitvalidohasta[2] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[0];
            tr += '<td>' + fechavalidohasta + '</td>';
            //if (solicitudes[i].FechaAprobacion == "") {
            //    tr += '<td>-</td>';
            //} else {
            //    tr += '<td>' + solicitudes[i].FechaAprobacion + '</td>';
            //}

            tr += //'<td><button class="btn btn-primary btn-xs" onclick="ObtenerDatosxID(' + "'" + solicitudes[i].NumeroSolicitud + "'" + ',' + solicitudes[i].IdSolicitud + ',' + solicitudes[i].UsuarioAprobador + ',' + solicitudes[i].IdSolicitudModelo + ',' + solicitudes[i].IdEtapa + ',' + solicitudes[i].IdClaseArticulo +')"><img src="/assets/img/fa_eyes.png" height ="15" width="15" /></button>' +
                //'<button class="btn btn-info btn-xs  fa fa-file" onclick="GenerarPDF(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '<td><a href="/SolicitudRQAutorizacion/GenerarPDF?Id=' + solicitudes[i].IdSolicitud + '" target=”_blank” class="btn btn-primary btn-xs"><img src="/assets/img/fa_pdf.png" height ="15" width="15" /></a></td>';
            tr += '<td><button onclick="ModalAnexos(' + solicitudes[i].IdSolicitud + ')" class="btn btn-primary btn-xs"><img src="/assets/img/fa_folder.png" height ="15" width="15" /></button></td>';

            if (solicitudes[i].Estado == "Aprobado") {
                tr += '<td><button onclick="PasarPendiente(' + solicitudes[i].IdSolicitud + ')" class="btn btn-primary btn-xs"><img src="/assets/img/fa_retro.png" height ="15" width="15" /></button></td>';
            } else {
                tr += '<td><button class="btn btn-primary btn-xs" disabled><img src="/assets/img/fa_retro.png" height ="15" width="15" /></button></td>';
            }


            tr += '</tr>';
        }

        $("#tbody_SolicitudAutorizacion").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

    let SolicitudSeleccionada = $("#SolicitudSeleccionadaIdSolicitud").val();
    console.log(SolicitudSeleccionada);
    if (SolicitudSeleccionada != "" || SolicitudSeleccionada != null) {
        //$('#table_id').on('click', 'tr', function () {

        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#solicitud" + SolicitudSeleccionada).css('background-color');
        $("#solicitud" + SolicitudSeleccionada).css('background-color', '#effafc');
        ultimaFila = $("#solicitud" + SolicitudSeleccionada);

        //});
    }



}


function validarsipuedeaprobar(IdSolicitudRQ, IdEtapa) {
    let respuestaa = false;
    $.ajaxSetup({ async: false });
    $.post('ValidarSipuedeAprobar', { 'IdSolicitudRQ': IdSolicitudRQ, 'IdEtapa': IdEtapa }, function (data, status) {
        respuestaa = data
    });
    return respuestaa;

}


function FilaSeleccionada(NumeroSolicitud, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, IdEtapa, IdClaseArticulo) {

    let respuesta = validarsipuedeaprobar(IdSolicitudRQ, IdEtapa)
    if (respuesta == false) {
        Swal.fire(
            'No puede ingresar',
            'No puede ingresar a la fila seleccionada,la etapa anterior se encuentra en proceso',
            'warning'
        )
        $("#tbody_detalle").html('');
        return;
    }

    $('#table_id').on('click', 'tr', function () {

        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#solicitud" + IdSolicitudRQ).css('background-color');
        $("#solicitud" + IdSolicitudRQ).css('background-color', '#effafc');
        ultimaFila = $("#solicitud" + IdSolicitudRQ);

    });

    ObtenerDatosxID(NumeroSolicitud, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, IdEtapa, IdClaseArticulo);


}


function ObtenerConfiguracionDecimales() {

    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;

    });
}

//function AprobarSolicitud() {

//    let ArrayGeneral = new Array();
//    let arrayid = new Array();
//    let IdAutorizador = new Array();
//    let IdSolicitudModelo = new Array();

//    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
//        arrayid.push($(this).val());
//    });
//    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
//        IdAutorizador.push($(this).attr("IdAutorizador"));
//    });
//    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
//        IdSolicitudModelo.push($(this).attr("IdSolicitudModelo"));
//    });

//    if (arrayid.length == 0) {
//        swal("Error!", 'No hay seleccionados')
//        return;
//    }

//    for (var i = 0; i < arrayid.length; i++) {
//        ArrayGeneral.push({ 'IdSolicitudRQModeloAprobaciones': 0, 'IdSolicitud': arrayid[i], 'IdSolicitudModelo': IdSolicitudModelo[i], 'IdAutorizador': IdAutorizador[i],'Accion':1 })
//    }
//    let solicitudRQModeloAprobacionesDTO = ArrayGeneral;
//    //console.log(ArrayGeneral);

//    $.ajax({
//        url: "UpdateInsertModeloAprobaciones",
//        type: "POST",
//        data: { solicitudRQModeloAprobacionesDTO },
//        beforeSend: function () {
//        },
//        success: function (resultado) {
//            swal("Exito!", 'Se aprobaron las solicitudes correctamente!', "success")
//            table.destroy();
//            ConsultaServidor("ObtenerSolicitudesxAutorizar");
//        }
//    }).fail(function () {
//        swal("Error!", 'Comunicarse con el Area Soporte: smarcode@smartcode.pe !')
//    });
//}



function GuardarDetallesAutorizar() {

    let ArrayGeneral = new Array();
    let arrayIdSolicitud = new Array();
    let IdAutorizador = new Array();
    let IdSolicitudModelo = new Array();
    let IdArticulo = new Array();
    let EstadoItem = new Array();
    let CantidadItem = new Array();
    let PrecioItem = new Array();
    let IdDetalle = new Array();

    $("input[name='txtIdSolicitudRQ[]']").each(function (indice, elemento) {
        arrayIdSolicitud.push($(elemento).val());
    });

    $("input[name='txtUsuarioAprobador[]']").each(function (indice, elemento) {
        IdAutorizador.push($(elemento).val());
    });

    $("input[name='txtIdSolicitudModelo[]']").each(function (indice, elemento) {
        IdSolicitudModelo.push($(elemento).val());
    });

    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        IdArticulo.push($(elemento).val());
    });

    $("select[name='cboEstadoItem[]']").each(function (indice, elemento) {
        EstadoItem.push($(elemento).val());
    });

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        CantidadItem.push($(elemento).val());
    });

    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        PrecioItem.push($(elemento).val());
    });

    $("input[name='txtIdDetalle[]']").each(function (indice, elemento) {
        IdDetalle.push($(elemento).val());
    });




    if (IdArticulo.length == 0) {
        swal("Error!", 'No hay seleccionados')
        return;
    }

    for (var i = 0; i < IdArticulo.length; i++) {
        ArrayGeneral.push({ 'IdSolicitudRQModeloAprobaciones': 0, 'IdSolicitud': arrayIdSolicitud[i], 'IdSolicitudModelo': IdSolicitudModelo[i], 'IdAutorizador': IdAutorizador[i], 'IdArticulo': IdArticulo[i], 'Accion': EstadoItem[i], 'CantidadItem': CantidadItem[i], 'PrecioItem': PrecioItem[i], 'IdDetalle': IdDetalle[i] })
    }

    //console.log(ArrayGeneral);
    //return;

    let solicitudRQModeloAprobacionesDTO = ArrayGeneral;
    //console.log(ArrayGeneral);




    $.ajax({
        url: "UpdateInsertModeloAprobaciones",
        type: "POST",
        async: true,
        data: { solicitudRQModeloAprobacionesDTO },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (resultado) {

            var errorEmpresa = validarEmpresaUpdateInsert(resultado);
            if (errorEmpresa) {
                return;
            }


            Swal.fire(
                'Correcto',
                'Proceso Realizado Correctamente',
                'success'
            )

            $("#tablaDet").find('tbody').html("");

            table.destroy();
            ConsultaServidor("ObtenerSolicitudesxAutorizar");

            //actualzia detalle
            let artIdSolicitudRQ = $("#SolicitudSeleccionadaIdSolicitud").val();
            let artUsuarioAprobador = $("#SolicitudSeleccionadaIdAutorizador").val();
            let artIdSolicitudModelo = $("#SolicitudSeleccionadaIdSolicitudModelo").val();
            let artIdEtapa = $("#SolicitudSeleccionadaIdEtapa").val();
            let artNumeroSolicitud = $("#NumeracionSolicitud").text();
            let artIdClaseArticulo = $("#SolicitudIdClaseArticulo").val();

            //ObtenerDatosxID(artNumeroSolicitud, artIdSolicitudRQ, artUsuarioAprobador, artIdSolicitudModelo, artIdEtapa, artIdClaseArticulo);

        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });




}

function RechazarSolicitud() {

    let ArrayGeneral = new Array();
    let arrayid = new Array();
    let IdAutorizador = new Array();
    let IdSolicitudModelo = new Array();

    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
        arrayid.push($(this).val());
    });
    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
        IdAutorizador.push($(this).attr("IdAutorizador"));
    });
    $("input[name='checkbox_inputs']:checkbox:checked").each(function () {
        IdSolicitudModelo.push($(this).attr("IdSolicitudModelo"));
    });

    if (arrayid.length == 0) {
        swal("Error!", 'No hay seleccionados')
        return;
    }

    for (var i = 0; i < arrayid.length; i++) {
        ArrayGeneral.push({ 'IdSolicitudRQModeloAprobaciones': 0, 'IdSolicitud': arrayid[i], 'IdSolicitudModelo': IdSolicitudModelo[i], 'IdAutorizador': IdAutorizador[i], 'Accion': 2 })
    }
    let solicitudRQModeloAprobacionesDTO = ArrayGeneral;
    //console.log(ArrayGeneral);

    $.ajax({
        url: "UpdateInsertModeloAprobaciones",
        type: "POST",
        data: { solicitudRQModeloAprobacionesDTO },
        beforeSend: function () {
        },
        success: function (resultado) {

            var errorEmpresa = validarEmpresaUpdateInsert(resultado);
            if (errorEmpresa) {
                return;
            }

            swal("Exito!", 'Se aprobaron las solicitudes correctamente!', "success")
            table.destroy();
            ConsultaServidor("ObtenerSolicitudesxAutorizar");
        }
    }).fail(function () {
        swal("Error!", 'Comunicarse con el Area Soporte: smarcode@smartcode.pe !')
    });


}




function seleccionar_todo() {

    if ($('#checkbox_all').prop('checked')) {

        $(".checkbox_inputs").prop("checked", true);
    } else {
        $(".checkbox_inputs").prop("checked", false);
    }
}




function ObtenerDatosxID(NumeroSolicitud, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, IdEtapa, IdClaseArticulo) {

    $("#tablaDet").find('tbody').empty();
    $("#SolicitudSeleccionadaIdSolicitud").val(IdSolicitudRQ);
    $("#SolicitudSeleccionadaIdAutorizador").val(UsuarioAprobador);
    $("#SolicitudSeleccionadaIdSolicitudModelo").val(IdSolicitudModelo);
    $("#SolicitudSeleccionadaIdEtapa").val(IdEtapa);
    $("#SolicitudIdClaseArticulo").val(IdClaseArticulo);

    //$("#SolicitudSeleccionadaIdDetalle").val(IdClaseArticulo);

    $("#NumeracionSolicitud").html(NumeroSolicitud);




    $.ajax({
        url: "ObtenerDatosxID",
        type: "POST",
        data: {
            'IdSolicitudRQ': IdSolicitudRQ,
            'IdAprobador': UsuarioAprobador,
            'IdEtapa': IdEtapa
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

            //var errorEmpresa = validarEmpresa(data);
            //if (errorEmpresa) {
            //    return;
            //}


            SweetAlert.close();

            if (data == "Error") {
                swal("Error!", "Ocurrio un error")
                limpiarDatos();
            } else {

                let solicitudes = JSON.parse(data);
                console.log(solicitudes);
                let Detalle = solicitudes[0].Detalle;

                for (var i = 0; i < Detalle.length; i++) {
                    AgregarLineaDetalle(Detalle[i].IdDetalle, solicitudes[0].Numero, Detalle[i].Descripcion, IdClaseArticulo, Detalle[i].Prioridad, i, Detalle[i].EstadoDisabled, Detalle[i].EstadoItemAutorizado, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, Detalle[i].IdArticulo, Detalle[i].IdSolicitudRQDetalle, Detalle[i].IdUnidadMedida, Detalle[i].IdIndicadorImpuesto, Detalle[i].IdAlmacen, Detalle[i].IdProveedor, Detalle[i].IdLineaNegocio, Detalle[i].IdCentroCostos, Detalle[i].IdProyecto, Detalle[i].IdItemMoneda, Detalle[i].ItemTipoCambio, Detalle[i].CantidadNecesaria.toFixed(DecimalesCantidades), Detalle[i].PrecioInfo.toFixed(DecimalesPrecios), Detalle[i].ItemTotal.toFixed(DecimalesImportes), Detalle[i].NumeroFabricacion, Detalle[i].NumeroSerie, Detalle[i].FechaNecesaria, Detalle[i].Referencia, Detalle[i].DescripcionItem, Detalle[i].AprobadoAnterior);
                    $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
                }

            }

        }
    }).fail(function () {
        swal("Error!", 'Comunicarse con el Area Soporte: smarcode@smartcode.pe !')
    });


    //$.post('ObtenerDatosxID', {
    //    'IdSolicitudRQ': IdSolicitudRQ,
    //    'IdAprobador': UsuarioAprobador,
    //    'IdEtapa': IdEtapa
    //}, function (data, status) {


    //    if (data == "Error") {
    //        swal("Error!", "Ocurrio un error")
    //        limpiarDatos();
    //    } else {

    //        let solicitudes = JSON.parse(data);
    //        let Detalle = solicitudes[0].Detalle;

    //        for (var i = 0; i < Detalle.length; i++) {
    //            AgregarLineaDetalle(IdClaseArticulo, Detalle[i].Prioridad, i, Detalle[i].EstadoDisabled, Detalle[i].EstadoItemAutorizado, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, Detalle[i].IdArticulo, Detalle[i].IdSolicitudRQDetalle, Detalle[i].IdUnidadMedida, Detalle[i].IdIndicadorImpuesto, Detalle[i].IdAlmacen, Detalle[i].IdProveedor, Detalle[i].IdLineaNegocio, Detalle[i].IdCentroCostos, Detalle[i].IdProyecto, Detalle[i].IdItemMoneda, Detalle[i].ItemTipoCambio, Detalle[i].CantidadNecesaria.toFixed(DecimalesCantidades), Detalle[i].PrecioInfo.toFixed(DecimalesPrecios), Detalle[i].ItemTotal.toFixed(DecimalesImportes), Detalle[i].NumeroFabricacion, Detalle[i].NumeroSerie, Detalle[i].FechaNecesaria, Detalle[i].Referencia, Detalle[i].DescripcionItem);
    //            $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
    //        }

    //    }

    //});

}




function AgregarLineaDetalle(IdDetalle, Numero, DescripcionServicio, IdClaseArticulo, Prioridad, contador, EstadoDisabled, EstadoItemAutorizado, IdSolicitudRQ, UsuarioAprobador, IdSolicitudModelo, IdArticulo, IdSolicitudRQDetalle, IdUnidadMedida, IdIndicadorImpuesto, IdAlmacen, IdProveedor, IdLineaNegocio, IdCentroCosto, IdProyecto, IdMoneda, ItemTipoCambio, CantidadNecesaria, PrecioInfo, ItemTotal, NumeroFabricacion, NumeroSerie, FechaNecesaria, Referencia, DescripcionItem, AprobadoAnterior = 1) {

    console.log("detal");
    console.log(IdDetalle);

    console.log("detal");

    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Proveedor = JSON.parse(data);
    });

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    Proyecto = JSON.parse(data);
    //});

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Moneda = JSON.parse(data);
    });

    //let TipoItem = $("#cboClaseArticulo").val();
    if (IdClaseArticulo == 2) {

        DescripcionItem = DescripcionServicio;

    } else if (IdClaseArticulo == 3) {

        DescripcionItem = DescripcionServicio;

    } else {
        $.post('/Articulo/ObtenerDatosxID', {
            'IdArticulo': IdArticulo
        }, function (data, status) {
            var errorEmpresa = validarEmpresa(data);
            if (errorEmpresa) {
                return;
            }

            let articulos = JSON.parse(data);
            DescripcionItem = articulos[0].Descripcion1;
        });
    }




    let StockItem = 0;
    let Comprometido = 0;
    let EnPedido = 0;
    //if (IdClaseArticulo != 3) {
    //    $.post('/Articulo/ObtenerArticuloxCodigo', {
    //        'Codigo': IdArticulo,
    //        'TipoItem': IdClaseArticulo,
    //        'Almacen': IdAlmacen

    //    }, function (data, status) {
    //        var errorEmpresa = validarEmpresa(data);
    //        if (errorEmpresa) {
    //            return;
    //        }

    //        let articulos = JSON.parse(data);
    //        StockItem = articulos[0].Stock;
    //        Comprometido = articulos[0].Comprometido;
    //        EnPedido = articulos[0].EnPedido;
    //    });
    //}




    var fechaSplit = (FechaNecesaria.substring(0, 10)).split("-");
    var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];

    let tr = '';

    if (AprobadoAnterior == 0) {
        tr += `<tr style="display:none;">`;
    } else {
        tr += `<tr>`;
    }



    tr += `<td style="display:none;">

            <input  class="form-control" type="text" value="`+ IdSolicitudRQDetalle + `" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/>
            <input  class="form-control" type="text" value="`+ UsuarioAprobador + `" id="txtUsuarioAprobador" name="txtUsuarioAprobador[]"/>
            <input  class="form-control" type="text" value="`+ IdSolicitudModelo + `" id="txtIdSolicitudModelo" name="txtIdSolicitudModelo[]"/>
            <input  class="form-control" type="text" value="`+ IdSolicitudRQ + `" id="txtIdSolicitudRQ" name="txtIdSolicitudRQ[]"/>

            <input  class="form-control" type="text" value="`+ IdDetalle + `" id="txtIdDetalle" name="txtIdDetalle[]"/>


            </td>

            <td style="display:none">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        if (IndicadorImpuesto[i].IdIndicadorImpuesto == IdIndicadorImpuesto) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" selected>` + IndicadorImpuesto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>


            <td style="display:none">
            <input class="form-control" type="text" value="" id="txtCodigoArticulo" name="txtCodigoArticulo[]"/>
            </td>
            <td><input class="form-control" type="text" value="`+ IdArticulo + `" id="txtIdArticulo" name="txtIdArticulo[]" disabled/></td>
            <td><input class="form-control" value="`+ DescripcionItem + `" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]" disabled/></td>
            <td>
            <select style="display:none" class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `" selected>` + UnidadMedida[i].Codigo + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Codigo + `</option>`;
        }
    }
    tr += `</select>`;
    let SinUnidad = 0;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
            tr += `<input class="form-control" type="text"  value="` + UnidadMedida[i].Codigo + `" disabled/>`;
        } else {
            SinUnidad++;
        }
    }
    if (SinUnidad > 0 && IdClaseArticulo == 2) {
        tr += `<input class="form-control" type="text"  value="" disabled/>`;
    }

    tr += ` </td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" value="`+ CantidadNecesaria + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="`+ PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" ></td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" value="`+ ItemTotal + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `"  disabled></td>
            <td style="display:none">
            <select style="display:none" class="form-control" name="cboCentroCostos[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    if (CentroCosto[i].IdCentroCosto == IdCentroCosto) {
    //        tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `" selected>` + CentroCosto[i].IdCentroCosto + `</option>`;
    //    } else {
    //        tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].IdCentroCosto + `</option>`;
    //    }
    //}
    tr += `</select>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    if (CentroCosto[i].IdCentroCosto == IdCentroCosto) {
    //        tr += `<input class="form-control" type="text"  value="` + CentroCosto[i].IdCentroCosto + `" disabled/>`;
    //    }
    //}
    tr += `</td>
            <td>`;

    if (AprobadoAnterior == 0) {
        tr += ` <select class="form-control" id="cboEstadoItem` + contador + `" name="cboEstadoItem[]">
                <option value="1">Pendiente</option>
                <option value="2">Aprobado</option>
                <option value="3">Rechazado</option>
                
            </select>`;
    } else {
        tr += ` <select class="form-control EstadoItem" id="cboEstadoItem` + contador + `" name="cboEstadoItem[]">
                <option value="1">Pendiente</option>
                <option value="2">Aprobado</option>
                <option value="3">Rechazado</option>
                
            </select>`;
    }


    tr += `</td>

            <td style="display:none"><input class="form-control" type="number"  value="`+ StockItem + `" name="txtStockItem[]" id="txtStockItem` + contador + `" disabled></td>
            <td style="display:none"><input class="form-control" type="number"  value="`+ Comprometido + `" name="txtComprometido[]" id="txtComprometido` + contador + `" disabled></td>
            <td style="display:none"><input class="form-control" type="number"  value="`+ EnPedido + `" name="txtEnPedido[]" id="txtEnPedido` + contador + `" disabled></td>
            
            <td style="display:none">
            <select style="display:none" class="form-control" name="cboPrioridadItem[]" disabled>`;
    if (Prioridad == 1) {
        tr += `     <option value="1" selected>Alta</option>`;
    } else {
        tr += `  <option value="2" selected>Baja</option>`;
    }
    tr += `</select>`;

    if (Prioridad == 1) {
        tr += `<input type="text" class="form-control" value="Alta" disabled/>`;
    } else {
        tr += `<input type="text" class="form-control" value="Baja" disabled/>`;
    }
    tr += `
            </td>
            <td style="display:none">
            <select class="form-control" style="display:none" name="cboAlmacen[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == IdAlmacen) {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `" selected>` + Almacen[i].IdAlmacen + `</option>`;
        } else {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].IdAlmacen + `</option>`;
        }
    }
    tr += `</select>`;

    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == IdAlmacen) {
            tr += `<input class="form-control" type="text"  value="` + Almacen[i].IdAlmacen + `" disabled/>`;
        }
    }
    tr += `</td>
            
            <td style="display:none">
            <select class="form-control" name="cboProyecto[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < Proyecto.length; i++) {
    //    if (Proyecto[i].IdProyecto == IdProyecto) {
    //        tr += `  <option value="` + Proyecto[i].IdProyecto + `" selected>` + Proyecto[i].IdProyecto + `</option>`;
    //    } else {
    //        tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].IdProyecto + `</option>`;
    //    }
    //}
    tr += `</select>
            </td>
            <td ><textarea class="form-control" type="text" value="`+ Referencia + `"  name="txtReferencia[]" disabled>` + Referencia + `</textarea></td>
            <td><button class="btn btn-xs btn-warning" onclick="BuscarHistorial('`+ Numero + `','` + IdArticulo + `','` + IdAlmacen + `',` + IdClaseArticulo + `)">H</button></td>
         
          <tr>`;
    //<button class="btn btn-xs btn-danger" onclick="EliminarDetalle(`+ IdSolicitudRQDetalle + `,this)">-</button>
    //$("#tabla").find('tbody').append(tr);
    $("#tablaDet").find('tbody').append(tr);
    $("#cboEstadoItem" + contador).val(EstadoItemAutorizado);

    if (EstadoDisabled == 0) {
        $("#cboEstadoItem" + contador).prop("disabled", true);
        $("#txtCantidadNecesaria" + contador).prop("disabled", true);
        $("#txtPrecioInfo" + contador).prop("disabled", true);
    }

}

function AprobarTodo() {
    $(".EstadoItem").val(2);
}
function RechazarTodo() {
    $(".EstadoItem").val(3);
}
//function AgregarLineaDetalleAnexo(Id, Nombre) {

//    let tr = '';
//    tr += `<tr>
//            <td style="display:none"><input  class="form-control" type="text" value="`+ Id + `" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
//            <td>
//               `+ Nombre + `
//               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
//            </td>
//            <td>
//               <a href="/SolicitudRQ/Download?ImageName=`+ Nombre + `" >Descargar</a>
//            </td>
//            <td><button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ Id + `,this)">-</button></td>
//            </tr>`;

//    $("#tabla_files").find('tbody').append(tr);

//}



//function CargarSeries() {
//    $.ajaxSetup({ async: false });
//    $.post("/Serie/ObtenerSeries", function (data, status) {
//        let series = JSON.parse(data);
//        llenarComboSerie(series, "cboSerie", "Seleccione")
//    });
//}

//function llenarComboSerie(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}


//function CargarSolicitante() {
//    $.ajaxSetup({ async: false });
//    $.post("/Usuario/ObtenerUsuarios", function (data, status) {
//        let solicitante = JSON.parse(data);
//        console.log(solicitante);
//        llenarComboSolicitante(solicitante, "cboEmpleado", "Seleccione")
//        //llenarComboSolicitante(solicitante, "cboTitular", "Seleccione")
//    });
//}

//function llenarComboSolicitante(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUsuario + "'>" + lista[i].NombreUsuario + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

//function CargarSucursales() {
//    $.ajaxSetup({ async: false });
//    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
//        let sucursales = JSON.parse(data);
//        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
//    });
//}

//function llenarComboSucursal(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSucursal + "'>" + lista[i].Descripcion + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

//function CargarDepartamentos() {
//    $.ajaxSetup({ async: false });
//    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
//        let departamentos = JSON.parse(data);
//        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
//    });
//}

//function llenarComboDepartamento(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDepartamento + "'>" + lista[i].Descripcion + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

//function CargarMoneda() {
//    $.ajaxSetup({ async: false });
//    $.post("/Moneda/ObtenerMonedas", function (data, status) {
//        let monedas = JSON.parse(data);
//        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
//    });
//}

//function llenarComboMoneda(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdMoneda + "'>" + lista[i].Descripcion + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

//function CargarImpuestos() {
//    $.ajaxSetup({ async: false });
//    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
//        let impuestos = JSON.parse(data);
//        llenarComboImpuesto(impuestos, "cboImpuesto", "Seleccione")
//    });
//}

//function llenarComboImpuesto(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdIndicadorImpuesto + "'>" + lista[i].Descripcion + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

//function CerrarModal() {
//    $("#tabla").find('tbody').empty();
//    $("#tabla_files").find('tbody').empty();
//    $.magnificPopup.close();
//}

//function openContenido(evt, Name) {
//    var i, tabcontent, tablinks;
//    tabcontent = document.getElementsByClassName("tabcontent");
//    for (i = 0; i < tabcontent.length; i++) {
//        tabcontent[i].style.display = "none";
//    }
//    tablinks = document.getElementsByClassName("tablinks");
//    for (i = 0; i < tablinks.length; i++) {
//        tablinks[i].className = tablinks[i].className.replace(" active", "");
//    }
//    document.getElementById(Name).style.display = "block";
//    evt.currentTarget.className += " active";
//}


function CalcularTotalDetalle(contador) {

    let varIndicadorImppuesto = $("#cboIndicadorImpuestoDetalle" + contador).val();
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");

    let varCantidadNecesaria = $("#txtCantidadNecesaria" + contador).val();
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
    //console.log(varTotal);
    $("#txtItemTotal" + contador).val(varTotal.toFixed(2)).change();


}


function PasarPendiente(idSolicitud) {


    alertify.confirm('Confirmacion', '¿Desea Volver RQ a Pendiente de Aprobacion?', function () {

        $.ajax({
            url: "PasarPendiente",
            type: "POST",
            async: true,
            data: {
                'IdSolicitud': idSolicitud
            },
            beforeSend: function () {

                Swal.fire({
                    title: "Cargando...",
                    text: "Por favor espere",
                    showConfirmButton: false,
                    allowOutsideClick: false
                });

            },
            success: function (resultado) {

                var errorEmpresa = validarEmpresaUpdateInsert(resultado);
                if (errorEmpresa) {
                    return;
                }


                if (resultado == 1) {

                    Swal.fire(
                        'Correcto',
                        'Proceso Realizado Correctamente',
                        'success'
                    )


                    $("#tablaDet").find('tbody').html("");
                    table.destroy();
                    ConsultaServidor("ObtenerSolicitudesxAutorizar");

                } else {
                    swal("Error!", "Ocurrio un Error")
                }

            }
        }).fail(function () {
            Swal.fire(
                'Error!',
                'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
                'error'
            )
        });

    }, function () {
    });

}






function ModalAnexos(IdSolicitudRQ) {
    $("#ModalAnexos").modal();


    $.post('/SolicitudRQ/ObtenerDatosxID', {
        'IdSolicitudRQ': IdSolicitudRQ,
    }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
        } else {
            let solicitudes = JSON.parse(data);

            let DetalleAnexo = solicitudes[0].DetallesAnexo;
            console.log(DetalleAnexo);
            for (var i = 0; i < DetalleAnexo.length; i++) {
                AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            }
        }

    });

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

            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}

function CerrarModalAnexos() {
    $("#tabla_files").find('tbody').empty();
}








function BuscarHistorial(Numero, IdArticulo, IdAlmacen, IdClaseArticulo) {


    $.ajax({
        url: "/EnviarSAP/ObtenerHistorialSap",
        type: "POST",
        async: true,
        data: {
            'Numero': Numero,
            'IdArticulo': IdArticulo,
            'IdAlmacen': IdAlmacen,
            'IdClaseArticulo': IdClaseArticulo
        },
        beforeSend: function () {

            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });

        },
        success: function (resultado) {

            var errorEmpresa = validarEmpresa(resultado);
            if (errorEmpresa) {
                return;
            }

            //let rpta = JSON.parse(resultado);
            //console.log(rpta);

            if (resultado == "error") {
                swal("Informacion!", "No se Encontraron datos")
                Sweetalert2.close();
            } else {
                $("#ModalHistorial").modal();
                Sweetalert2.close();
                let rpta = JSON.parse(resultado);
                for (var i = 0; i < rpta.length; i++) {
                    AgregarLineaHistorial(rpta[i].Documento, rpta[i].DocNum, rpta[i].DocStatus, rpta[i].CANCELED, rpta[i].ItemCode, rpta[i].Dscription, rpta[i].Quantity, rpta[i].OpenQty, rpta[i].LineStatus);
                }
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



function AgregarLineaHistorial(Documento, DocNum, DocStatus, CANCELED, ItemCode, Dscription, Quantity, OpenQty, LineStatus) {

    let Estado = "";
    if (LineStatus == 'C') {
        Estado = "Cerrado"
    } else {
        Estado = "Abierto"
    }

    if (CANCELED == 'Y') {
        Estado = "Cancelado";
    }

    let tr = '';
    tr += `<tr>
            <td>
              `+ Documento + `
            </td>
            <td>
               `+ DocNum + `
            </td>
            <td>
               `+ Estado + `
            </td>
            <td>
               `+ ItemCode + `
            </td>
            <td>
               `+ Dscription + `
            </td>
            <td>
               `+ Quantity + `
            </td>
            <td>
               `+ OpenQty + `
            </td>
            </tr>`;

    $("#tabla_historial").find('tbody').append(tr);

}

function CerrarModalHistorial() {
    $("#tabla_historial").find('tbody').empty();
}












function validarEmpresa(rpta) {
    if (rpta == "SinBD") {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
}

function validarEmpresaUpdateInsert(rpta) {
    if (rpta == -999) {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
}

function modalHistorialEstado(IdSolicitudRQ) {
    $("#ModalHistorialEstados").modal();
    $("#div_modelos_aprobaciones").html('');
    let tablee = "";
    $.post("/SolicitudRQ/DatosSolicitudModeloAprobaciones", { 'IdSolicitudRQ': IdSolicitudRQ }, function (data, status) {
        let datos = JSON.parse(data);
        console.log(datos.ListSolicitudRqModelo);

        if (datos.ListSolicitudRqModelo.length > 0) {
            for (var i = 0; i < datos.ListSolicitudRqModelo.length; i++) {
                tablee += ` <p>ETAPA:` + (i + 1) + ` </p>
                        <table class="table" id="tabla_modal_estado">
                                        <thead>
                                            <tr>
                                                <th>Nomb. Producto</th>
                                                <th>Aprobacion</th>
                                            </tr>
                                        </thead>
                                    <tbody>`;
                if (datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO.length > 0) {
                    console.log(datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO)

                    for (var j = 0; j < datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO.length; j++) {
                        tablee += `<tr>
                            <td>`+ datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO[j].NombArticulo + `</td>
                           <td>`+ datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO[j].NombEstado + `</td>
                            </tr>`;
                    }
                }
                tablee += `</tbody></table>`;
                tablee += `</br>`;
            }

        }
        $("#div_modelos_aprobaciones").html(tablee);
        console.log(datos.ListSolicitudRqModelo);
    })
}