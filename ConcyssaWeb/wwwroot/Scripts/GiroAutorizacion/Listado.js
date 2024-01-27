let table = '';
let tableCabecera = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;
var ultimaFila = null;
var ultimaFilaRequerimiento = null;
var colorOriginal = null;



var ultimaFila1 = null;
var colorOriginal1;
window.onload = function () {
  

    $.post('/Usuario/ObtenerUsuariosAutorizadores', function (data, status) {
        $.ajaxSetup({ async: false });
        let datos = JSON.parse(data);
        llenarComboAutorizadores(datos, "CboAutorizador", "Seleccione")
    });


    $("#FechaInicio").datepicker();
    $("#FechaFinal").datepicker();
    $("#FechaInicio").datepicker("option", "dateFormat", 'dd/mm/yy');
    $("#FechaFinal").datepicker("option", "dateFormat", 'dd/mm/yy');



    //var url = "ObtenerGirosxAutorizar";
    //ObtenerConfiguracionDecimales();
    //ConsultaServidor(url);

    var url = "ObtenerGirosCabeceraxAutorizar";
    ObtenerConfiguracionDecimales();
    ConsultaServidorCabecera(url);

};


function BuscarPorFechas() {
    if (table) {
        table.destroy();
    }
   
    $("#tbody_detalle").html("");
    $("#NumeracionSolicitud").html("");
    ConsultaServidorCabecera("ObtenerGirosCabeceraxAutorizar");

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
    if (nRegistros == 1) {
        $("#" + idCombo).val(lista[nRegistros - 1].IdUsuario);
        //BuscarPorFechas();
    }
}


function ConsultaServidorCabecera(url) {

    let FechaInicio = $("#FechaInicio").val();
    let FechaFinal = $("#FechaFinal").val();
    let Estado = $("#Estado").val();
    let Autorizador = $("#CboAutorizador").val();

    if (tableCabecera) {
        tableCabecera.destroy();
    }

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


    $.post(url, { 'FechaInicio': FechaInicio, 'FechaFinal': FechaFinal, 'Estado': Estado, 'IdAutorizador': Autorizador }, function (data, status) {


        if (data == "error") {
            $("#tbody_datos").html("");
            tableCabecera = $("#table_id_cabecera").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let girosCabecera = JSON.parse(data);
        console.log(girosCabecera);
        let total_giros_cabecera = girosCabecera.length;


        for (var i = 0; i < girosCabecera.length; i++) {

            tr += `<tr id="rdSeleccionado` + girosCabecera[i].IdGiro +`">
                  <td>`+ (i + 1) +`</td>
                  <td>`+ girosCabecera[i].SerieNumGiro +`</td>
                  <td>`+ girosCabecera[i].Creador +`</td>
                  <td>`+ girosCabecera[i].Fecha.split("T")[0] +`</td>
                  <td>`+ girosCabecera[i].Solicitante+`</td>
                  <td>`+ girosCabecera[i].Obra +`</td>
                  <td>`+ girosCabecera[i].Semana +`</td>
                  <td>`+ formatNumber(girosCabecera[i].MontoSoles) +`</td>
                  <td>`+ formatNumber(girosCabecera[i].MontoDolares) +`</td>
                  </tr>`;
        }


        $("#tbody_datos").html(tr);
        $("#spnTotalRegistros").html(total_giros_cabecera);

        lenguaje.order = [[0, 'desc']];
        tableCabecera = $("#table_id_cabecera").DataTable(lenguaje);


    });




    $('#table_id_cabecera tbody').unbind("dblclick");
    $('#table_id_cabecera tbody').on('dblclick', 'tr', function () {
        var data = tableCabecera.row(this).data();
        console.log(data);
        //if (ultimaFila1 != null) {
        //    ultimaFila1.css('background-color', colorOriginal1)
        //}
        //colorOriginal1 = $("#" + data["DT_RowId"]).css('background-color');
        //$("#" + data["DT_RowId"]).css('background-color', '#effafc');
        //ultimaFila1 = $("#" + data["DT_RowId"]);

        if (ultimaFila1 != null) {
            $("#" + ultimaFila1).css({ "background-color": colorOriginal, "color": "#7f828f" });
            ultimaFila1 = data["DT_RowId"];
        }
        else {
            ultimaFila1 = data["DT_RowId"];
            colorOriginal = "transparent";
        }

        $("#" + data["DT_RowId"]).css({ "background-color": "var(--color-segundario)", "color": "white" });



        ConsultaServidorDetalle(data["DT_RowId"]);
    });





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




function ConsultaServidorDetalle(IdGiro) {

    var art = IdGiro.split("rdSeleccionado");

    if (table) {
        table.destroy();
    }
    

    var url = "ObtenerGirosxAutorizar";

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

    if (Estado == 0) {
        $("#IdAprobarTodo").prop("disabled", false);
        $("#IdRechazarTodo").prop("disabled", false);
        $("#IdGuardarAuto").prop("disabled", false);
    } else {
        $("#IdAprobarTodo").prop("disabled", true);
        $("#IdRechazarTodo").prop("disabled", true);
        $("#IdGuardarAuto").prop("disabled", true);
    }


    $.post(url, { 'FechaInicio': FechaInicio, 'FechaFinal': FechaFinal, 'Estado': Estado, 'IdAutorizador': Autorizador, 'IdGiro': art[1] }, function (data, status) {

        //var errorEmpresa = validarEmpresa(data);
        //if (errorEmpresa) {
        //    return;
        //}


        //console.log(data);
        if (data == "error") {
            $("#tbody_GiroAutorizacion").html("");
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let giros = JSON.parse(data);
        let total_giros = giros.length;

        console.log(giros);

        let contador = 0;
        for (var i = 0; i < giros.length; i++) {
            contador++;
            tr += `<tr id="giro` + giros[i].IdGiroDetalle + `">`;
            tr += `<td>` + giros[i].Fecha.split("T")[0] + `</td>`
            tr += `<td>
                    <input  class="form-control" type="hidden" value="`+ giros[i].IdGiroDetalle + `" id="txtIdGiroDetalle" name="txtIdGiroDetalle[]"/>
                    <input  class="form-control" type="hidden" value="`+ giros[i].IdUsuario + `" id="txtUsuarioAprobador" name="txtUsuarioAprobador[]"/>
                    <input  class="form-control" type="hidden" value="`+ giros[i].IdGiroModelo + `" id="txtIdGiroModelo" name="txtIdGiroModelo[]"/>
                    <input  class="form-control" type="hidden" value="`+ giros[i].IdGiro + `" id="txtIdGiro" name="txtIdGiro[]"/>
                    <input  class="form-control" type="hidden" value="`+ giros[i].IdGiroDetalle + `" id="txtIdDetalle" name="txtIdDetalle[]"/>

                ` + giros[i].Proveedor + `</td>`;
            tr += `<td>` + giros[i].NumeroDocumento + `</td>`;
            tr += `<td>` + giros[i].Moneda + `</td>`;
            tr += `<td>` + formatNumber(giros[i].Monto) + `</td>`;
            tr += `<td><strong><a style="color:blue;text-decoration:underline" href="/Requerimiento/` + giros[i].Anexo + `"target="_blank" > ` + giros[i].Anexo + `</a></strong></td>`;
            tr += `<td id="txtIdComentario" name="txtIdComentario[]">` + giros[i].Comentario + `</td>`
            //tr += `<td>` + giros[i].NombUsuario + `</td>`;
            tr += `<td>`;

            if (giros[i].Accion == 0) {
                tr += ` <select class="form-control EstadoItem" name="cboEstadoItem[]">
                    <option value="0">Pendiente</option>
                    <option value="1">Rechazado</option>
                    <option value="2">Aprobado</option>`;
            } else {
                switch (giros[i].Accion) {
                    case 1:
                        tr += "Rechazado";
                        break;
                    case 2:
                        tr += "Aceptado";
                        break;
                }
            }

            tr += `</td>`;
            tr += '</tr>';
        }

        $("#tbody_GiroAutorizacion").html(tr);
        $("#spnTotalRegistros").html(total_giros);

        lenguaje.order = [[0, 'desc']];
        table = $("#table_id").DataTable(lenguaje);

        $('#table_id tbody').unbind("dblclick");

        $('#table_id tbody').on('dblclick', 'tr', function (key) {
            var data = table.row(this).data();


            let re = data[1];
            let htmlObject = $(re);
            let varIdArticulo = +htmlObject[8].value;//input#txtIdArticulo.form-
            let varIdDetalle = +htmlObject[12].value;//input#txtIdDetalle.form-

            if (ultimaFilaRequerimiento != null) {
                $("#giro" + ultimaFilaRequerimiento).css({ "background-color": colorOriginal, "color": "#7f828f" });
                ultimaFilaRequerimiento = varIdDetalle;
            }
            else {
                ultimaFilaRequerimiento = varIdDetalle;
                colorOriginal = "transparent";
            }


            $("#giro" + varIdDetalle).css({ "background-color": "var(--color-segundario)", "color": "white" });

            //ObtenerStockxIdDetalleSolicitudRQ(varIdDetalle)
            //ObtenerPrecioxProductoUltimasVentas(varIdArticulo, varIdDetalle)

            //document.getElementById("tabla_aprobados_stock").focus();

        });

    });
}

        //let SolicitudSeleccionada = $("#SolicitudSeleccionadaIdSolicitud").val();

        //if (SolicitudSeleccionada != "" || SolicitudSeleccionada != null) {

        //    if (ultimaFila != null) {
        //        ultimaFila.css('background-color', colorOriginal)
        //    }
        //    colorOriginal = $("#solicitud" + SolicitudSeleccionada).css('background-color');
        //    $("#solicitud" + SolicitudSeleccionada).css('background-color', '#effafc');
        //    ultimaFila = $("#solicitud" + SolicitudSeleccionada);

        //}



//    });
//}




function GuardarDetallesAutorizar() {

    let ArrayGeneral = new Array();
    let arrayIdGiro = new Array();
    let IdAutorizador = new Array();
    let IdGiroModelo = new Array();
/*    let IdArticulo = new Array();*/
    let EstadoItem = new Array();
    //let CantidadItem = new Array();
    //let PrecioItem = new Array();
    let IdDetalle = new Array();

    $("input[name='txtIdGiro[]']").each(function (indice, elemento) {
        arrayIdGiro.push($(elemento).val());
    });

    $("input[name='txtUsuarioAprobador[]']").each(function (indice, elemento) {
        IdAutorizador.push($(elemento).val());
    });

    $("input[name='txtIdGiroModelo[]']").each(function (indice, elemento) {
        IdGiroModelo.push($(elemento).val());
    });

    //$("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
    //    IdArticulo.push($(elemento).val());
    //});

    $("select[name='cboEstadoItem[]']").each(function (indice, elemento) {
        EstadoItem.push($(elemento).val());
    });

    //$("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
    //    CantidadItem.push($(elemento).val());
    //});

    //$("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
    //    PrecioItem.push($(elemento).val());
    //});

    $("input[name='txtIdDetalle[]']").each(function (indice, elemento) {
        IdDetalle.push($(elemento).val());
    });




    //if (IdArticulo.length == 0) {
    //    swal("Error!", 'No hay seleccionados')
    //    return;
    //}

    for (var i = 0; i < IdDetalle.length; i++) {
        ArrayGeneral.push({ 'IdGiroModeloAprobaciones': 0, 'IdGiroModelo': IdGiroModelo[i], 'IdAutorizador': IdAutorizador[i], 'IdDetalleGiro': IdDetalle[i], 'Accion': EstadoItem[i], 'IdDetalle': IdDetalle[i] })
    }


    //console.log(ArrayGeneral);
    //return;
    //console.log(ArrayGeneral);
    //return;

    let GiroModeloAprobacionesDTO = ArrayGeneral;
    //console.log(solicitudRQModeloAprobacionesDTO);
    $.ajax({
        url: "UpdateInsertModeloAprobacionesGiro",
        type: "POST",
        async: true,
        data: { GiroModeloAprobacionesDTO },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (resultado) {

            //var errorEmpresa = validarEmpresaUpdateInsert(resultado);
            //if (errorEmpresa) {
            //    return;
            //}

            Swal.fire(
                'Correcto',
                'Proceso Realizado Correctamente',
                'success'
            )

            

            //table.destroy();

            //ConsultaServidorDetalle("ObtenerGirosxAutorizar");
            ConsultaServidorCabecera("ObtenerGirosCabeceraxAutorizar");


            $("#tbody_GiroAutorizacion").html("");
            //actualzia detalle
            //let artIdSolicitudRQ = $("#SolicitudSeleccionadaIdSolicitud").val();
            //let artUsuarioAprobador = $("#SolicitudSeleccionadaIdAutorizador").val();
            //let artIdSolicitudModelo = $("#SolicitudSeleccionadaIdSolicitudModelo").val();
            //let artIdEtapa = $("#SolicitudSeleccionadaIdEtapa").val();
            //let artNumeroSolicitud = $("#NumeracionSolicitud").text();
            //let artIdClaseArticulo = $("#SolicitudIdClaseArticulo").val();

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










function AprobarTodo() {
    $(".EstadoItem").val(2);
}
function RechazarTodo() {
    $(".EstadoItem").val(1);
}