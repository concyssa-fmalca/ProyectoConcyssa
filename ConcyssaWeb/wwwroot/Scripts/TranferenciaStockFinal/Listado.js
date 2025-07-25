let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;


let validarGuardado = 0;


window.onload = function () {
    CargarBaseFiltro()
    ObtenerConfiguracionDecimales();

    //$("#SubirAnexos").on("submit", function (e) {
    //    e.preventDefault();
    //    var formData = new FormData($("#SubirAnexos")[0]);
    //    $.ajax({
    //        url: "GuardarFile",
    //        type: "POST",
    //        data: formData,
    //        cache: false,
    //        contentType: false,
    //        processData: false,
    //        success: function (datos) {
    //            let data = JSON.parse(datos);
    //            console.log(data);
    //            if (data.length > 0) {
    //                AgregarLineaAnexo(data[0]);
    //            }

    //        }
    //    });
    //});

};

function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboBaseFiltro","seleccione")

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
    $("#cboObraFiltro").prop("selectedIndex", 1);
    ConsultaServidor()
}

function ConsultaServidor() {
    let varIdObraDestinoFiltro = $("#cboObraFiltro").val()
    $.post("../Movimientos/ObtenerMovimientosTranferenciasFinal", { 'IdObraDestino': varIdObraDestinoFiltro }, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let movimientos = JSON.parse(data);
        let total_solicitudes = movimientos.length;
        console.log("cabecera");
        console.log(movimientos);

        for (var i = 0; i < movimientos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' + 
                '<td>' + movimientos[i].SerieCorrelativo + '</td>' + 
                '<td>' + movimientos[i].UsuarioCreador + '</td>' + 
                '<td>' + movimientos[i].NomObraOrigen + '</td>' + 
                '<td>' + movimientos[i].AlmacenInicial + '</td>' +
                '<td>' + movimientos[i].NomObraDestino + '</td>' +
                '<td>' + movimientos[i].Guia + '</td>' + 
                '<td>' + movimientos[i].FechaDocumento.split("T")[0] + '</td>' +
                //'<td>' + movimientos[i].Total + '</td>' +
                //'<td>' + movimientos[i].Estado + '</td>' +

                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
                '<button class="btn btn-danger fa fa-ban btn-xs" onclick="OcultarTransferencia(' + movimientos[i].IdMovimiento + ')"></button></td>' +
                '</tr>';


        }
        if (table) {
            table.destroy()
        }

        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}






function ObtenerDatosxID(IdMovimiento) {
    
    validarGuardado = 0;

    $("#txtId").val(IdMovimiento)
    CargarMotivoTraslado();
    CargarProveedor();
    ObtenerTiposDocumentos();

    CargarCentroCosto();
    CargarAlmacen();
    CargarTipoDocumentoOperacion();
    CargarSeries();
    CargarSolicitante(1);
    //CargarSucursales();
    CargarMoneda();
    CargarBase();

    ObtenerObrasDestino();
    ObtenerCuadrillas();
    

    $("#lblTituloModal").html("Validar Transferencia");
    AbrirModal("modal-form");





    $.post('../Movimientos/ObtenerDatosxIdMovimientoOLD', {
        'IdMovimiento': IdMovimiento,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            //console.log("aqui");
            console.log(movimiento);
            //console.log("aqui");
            $("#txtNumeracion").val(movimiento.Correlativo)
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(movimiento.SubTotal)
            $("#txtImpuesto").val(movimiento.Impuesto)
            $("#txtTotal").val(formatNumberDecimales(movimiento.Total,2))

            $("#IdBase").val(movimiento.IdBase)
            $("#IdObraDestino").val(movimiento.IdObraDestino)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            
            ObtenerAlmacenxIdObraDestino();

            $("#IdAlmacenDestino").val(movimiento.IdAlmacenDestino)
            $("#IdCuadrilla").val(movimiento.IdCuadrilla);

            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)


            $("#IdDestinatario").val(movimiento.IdDestinatario).change()
            $("#IdMotivoTraslado").val(movimiento.IdMotivoTraslado).change()
            $("#IdTransportista").val(movimiento.IdTransportista).change()
            $("#PlacaVehiculo").val(movimiento.PlacaVehiculo)
            $("#NumIdentidadConductor").val(movimiento.NumIdentidadConductor)
            $("#Peso").val(movimiento.Peso)
            $("#Bulto").val(movimiento.Bulto)
            $("#txtComentarios").html(movimiento.Comentario)
            $("#cboTipoDocumentoOperacion").val(333)

            if (movimiento.existeObraDestinoUsuario == 0) {
                $("#btnGrabar").hide()
            } else {
                $("#btnGrabar").show()
            }
            //AnexoDetalle
            //AnxoDetalle
            let AnexoDetalle = movimiento.AnexoDetalle;
            let trAnexo = '';
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
            //console.log(Detalle);
            //console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {
                AgregarLineaDetalle(i, Detalle[i]);
                $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
            }

      
            //let DetalleAnexo = solicitudes[0].DetallesAnexo;
            //for (var i = 0; i < DetalleAnexo.length; i++) {
            //    AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            //}


        }

    });
    disabledmodal(true);
    BloquearDatos()



}
function BloquearDatos() {
    $.post('../Obra/ObtenerObraxIdUsuarioSession'
        , function (data, status) {
        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            for (var i = 0; i < data.length; i++) {
                console.log("Obras : "+ data.IdObra)
            }

        }

    });

}

function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    //limpiarDatos();
}



function CalcularTotalDetalle(contador) {

    if ($("#txtCantidadNecesaria" + contador).val() == 0) {
        Swal.fire("ERROR!", "La cantidad no puede ser cero, si no recibió el articulo elimine la fila con el botón a la derecha", "error");
        $("#txtCantidadNecesaria" + contador).val($("#txtCantidadPrevia"+contador).val())
    }




    let varIndicadorImppuesto = ($("#cboIndicadorImpuestoDetalle" + contador).val()).replace(/,/g,"");
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");

    let varCantidadNecesaria = ($("#txtCantidadNecesaria" + contador).val()).replace(/,/g, "");
    let varPrecioInfo = ($("#txtPrecioInfo" + contador).val()).replace(/,/g, "");

    let subtotal = varCantidadNecesaria * varPrecioInfo;

    varPorcentaje = 0;
    console.log(subtotal);
    console.log(varPorcentaje);

    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        varTotal = subtotal;
    } else {
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }

    console.log(varTotal);

    $("#txtItemTotal" + contador).val(varTotal.toFixed(2)).change();


}

function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push(($(elemento).val()).replace(/,/g, ""));
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push(($(elemento).val()).replace(/,/g, ""));
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected', elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(($(elemento).val()).replace(/,/g, ""));
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

    //console.log(total);
    //console.log(subtotal);

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(subtotal.toFixed(2));
    $("#txtImpuesto").val(impuesto.toFixed(2));
    $("#txtTotal").val(formatNumberDecimales(total,2));

}

function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#cboAlmacen").prop('disabled', valorbolean);
    $("#cboCentroCosto").prop('disabled', valorbolean);
    $("#cboMoneda").prop('disabled', valorbolean);
    $("#cboSerie").prop('disabled', valorbolean);
    $("#txtFechaDocumento").prop('disabled', valorbolean);
    //$("#txtFechaContabilizacion").prop('disabled', valorbolean);
    $("#cboTipoDocumentoOperacion").prop('disabled', valorbolean);
    $("#IdTipoDocumentoRef").prop('disabled', valorbolean);
    $("#SerieNumeroRef").prop('disabled', valorbolean);
    //$("#IdCuadrilla").prop('disabled', valorbolean);
    $("#IdResponsable").prop('disabled', valorbolean);
    $("#txtComentarios").prop('disabled', valorbolean)
    $("#btn_agregaritem").prop('disabled', valorbolean)
    $("#EntregadoA").prop('disabled', valorbolean)

    $("#IdObraDestino").prop('disabled', valorbolean)
    //$("#IdAlmacenDestino").prop('disabled', valorbolean)

    if (valorbolean) {
        //$("#btnGrabar").hide()
        $("#btnExtorno").show();
        $("#total_editar").show();
        $("#total_nuevo").hide();
        $("#btnNuevo").show();

    } else {
        $("#btnExtorno").hide();
        $("#total_editar").hide();
        $("#total_nuevo").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
    }

}


function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}




function AgregarLineaDetalle(contador, detalle) {

    let tr = '';
    let UnidadMedida;
    let Almacen;
    let AlmacenDestino;
    let IdUnidadMedida = detalle.IdUnidadMedida
    let IdAlmacen = detalle.IdAlmacen
    //console.log(detalle);



    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ListarDefinicionGrupoxIdDefinicionSelect", { 'IdDefinicionGrupo': detalle.IdDefinicionGrupoUnidad }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });


    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    console.log("ssssssssssssssss");
    console.log(Almacen);


    let IdObra = $("#IdObraDestino").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        AlmacenDestino = JSON.parse(data);
    });


    tr = `<tr id="DetalleTrans`+contador+`">
        <td>
          <input class="form-control" type="text" value="`+ (contador + 1) + `" disabled />
        </td>
        <td>
           <input style="display:none" class="form-control" type="text" id="txtIdMovimientoDetalle`+ contador + `" name="txtIdMovimientoDetalle[]" value="` + detalle.IdMovimientoDetalle + `" />
           <input style="display:none" class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" value="` + detalle.IdArticulo + `" />
           <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo + `" disabled/>
           <input style="display:none" class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" value="` + detalle.DescripcionArticulo + `"/>
        </td>
        <td>
             <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedidaAlt == IdUnidadMedida) {
        tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `" selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td style="display:none">
            <input class="form-control" type="number" name="txtStock[]" value="0" id="txtStock` + contador + `" disabled>
        </td>
        <td>
            <input class="form-control" type="number" name="txtCantidadPrevia[]" value="`+ detalle.Cantidad + `" id="txtCantidadPrevia` + contador + `" disabled>
        </td>
        <td>
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" value="`+ formatNumberDecimales(detalle.Cantidad,2) + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)">
        </td>
        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="` + detalle.PrecioUnidadTotal + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control changeTotal" type="text" style="width:100px" value="`+ formatNumberDecimales(detalle.Total,2) + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
        </td>


        <td>
            <select disabled class="form-control" style="width:100px" id="cboAlmacenOrigen`+ contador + `" name="cboAlmacenOrigen[]" >`;
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
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" >`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < AlmacenDestino.length; i++) {
        if (AlmacenDestino[i].IdAlmacen == IdAlmacenDestino) {
            tr += `  <option value="` + AlmacenDestino[i].IdAlmacen + `" selected>` + AlmacenDestino[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + AlmacenDestino[i].IdAlmacen + `">` + AlmacenDestino[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
        </td>



        <td>
            <input class="form-control" type="text" style="width:100px" id="txtReferencia`+ contador + `" name="txtReferencia[]" value="` + detalle.Referencia+`" disabled>
        </td>
        <td>
            <button type="button" class="btn-sm btn btn-danger borrar fa fa-trash" onclick="borrartdIdtem(`+contador+`)"></button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);



    var AlmacenOrigen = $("#cboAlmacenOrigen" + contador).val();
    var IdArticulo = $("#txtIdArticulo" + contador).val();

    $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': AlmacenOrigen }, function (data, status) {
        let datos = JSON.parse(data);
        console.log(datos);
        $("#txtStock" + contador).val(datos[0].Stock);
    });




}


function borrartdIdtem(Id) {
    $("#DetalleTrans"+Id).remove()

}


function GuardarSolicitud() {
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



    let arrayIdMovimientoDetalle = new Array();
    $("input[name='txtIdMovimientoDetalle[]']").each(function (indice, elemento) {
        arrayIdMovimientoDetalle.push($(elemento).val());
    });

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
        arrayCantidadNecesaria.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayStock = new Array();
    $("input[name='txtStock[]']").each(function (indice, elemento) {
        arrayStock.push($(elemento).val());
    });




    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayAlmacenOrigen = new Array();
    $("select[name='cboAlmacenOrigen[]']").each(function (indice, elemento) {
        arrayAlmacenOrigen.push($(elemento).val());
    });


    let arrayAlmacenDestino = new Array();
    $("select[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacenDestino.push($(elemento).val());
    });



    let arrayReferencia = new Array();
    $("input[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });





    //for (var i = 0; i < arrayCantidadNecesaria.length; i++) {
    //    if (Number(arrayCantidadNecesaria[i]) > Number(arrayStock[i])) {
    //        Swal.fire(
    //            'Error!',
    //            'Stock cae a negativo linea: ' + (i + 1),
    //            'error'
    //        )
    //        return;
    //    }
    //}




    //Cabecera
    let IdMovimiento = $("#txtId").val();
    let IdAlmacen = $("#cboAlmacen").val();
    let IdTipoDocumento = $("#cboTipoDocumentoOperacion").val();
    let IdSerie = $("#cboSerie").val();
    let Correlativo = $("#txtNumeracion").val();
    let IdMoneda = $("#cboMoneda").val();
    let TipoCambio = $("#txtTipoCambio").val();
    let FechaContabilizacion = $("#txtFechaContabilizacion").val();
    let FechaDocumento = $("#txtFechaDocumento").val();
    let IdCentroCosto = $("#cboCentroCosto").val();
    let Comentario = $("#txtComentariosAceptar").val();
    let SubTotal = $("#txtTotalAntesDescuento").val();
    let Impuesto = $("#txtImpuesto").val();
    let Total = ($("#txtTotal").val()).replace(/,/g, "");
    let IdTipoProducto = $("#IdTipoProducto").val();
    let IdCuadrilla = $("#IdCuadrilla").val();
    let IdAlmacenDestino = $("#IdAlmacenDestino").val();


    if (Total == "-") {
        Total = 0
    }

    //END Cabecera



    //var ObraInicio = $("#IdObra").val();
    var ObraFin = $("#IdObraDestino").val();
    //var DatoValidarIngresoSalidaOAmbos = 0;


    //if (ObraInicio == ObraFin) {
    //    DatoValidarIngresoSalidaOAmbos = 2;

    //} else {
    //    DatoValidarIngresoSalidaOAmbos = 2;

    //}

    if (IdAlmacenDestino == 0) {
        Swal.fire(
            'Error!',
            'Debe colocar almacen destino!',
            'error'
        )
        return;
    }

    for (var i = 0; i < arrayAlmacenDestino.length; i++) {
        if (arrayAlmacenDestino[i] == 0) {
            Swal.fire(
                'Error!',
                'Debe colocar almacen destino detalle!, linea:' + (i + 1),
                'error'
            )
            return;
        }
    }



    let detalles = [];
    if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

        for (var i = 0; i < arrayIdArticulo.length; i++) {
            detalles.push({
                'IdArticulo': parseInt(arrayIdArticulo[i]),
                'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                'IdAlmacen': arrayAlmacenOrigen[i],
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
                'IdAlmacenDestino': arrayAlmacenDestino[i],
                'ValidarIngresoSalidaOAmbos': 1, //solo ingreso,
                'IdMovimiento': IdMovimiento,
                'IdMovimientoDetalle': arrayIdMovimientoDetalle[i]
            })
        }

    }


    let MovimientoEnviar = []

    MovimientoEnviar.push({
        detalles,
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
        'IdTipoProducto': IdTipoProducto,
        'IdAlmacenDestino': IdAlmacenDestino,
        'IdCuadrilla': IdCuadrilla,
        'IdMovimiento': IdMovimiento,

        'IdObraDestino': ObraFin,

        'IdTipoDocumentoRef': $("#IdTipoDocumentoRef").val(),
        'NumSerieTipoDocumentoRef': $("#SerieNumeroRef").val(),
        'IdDestinatario': $("#IdDestinatario").val(),
        'IdMotivoTraslado': $("#IdMotivoTraslado").val(),
        'IdTransportista': $("#IdTransportista").val(),
        'PlacaVehiculo': $("#PlacaVehiculo").val(),
        'NumIdentidadConductor': $("#NumIdentidadConductor").val(),
        'Peso': $("#Peso").val(),
        'Bulto': $("#Bulto").val()


        //end cabecera
    })


    if (validarGuardado == 0) {
        $.ajax({
            url: "UpdateInsertMovimientoFinalString",
            type: "POST",
            async: true,
            data: {
             

                'JsonDatosEnviar': JSON.stringify(MovimientoEnviar)
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
            
            
                    ConsultaServidor();

                    CerrarModal();

                    validarGuardado++;

                } else {
                    Swal.fire(
                        'Error!',
                        data,
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
    } else {
        CerrarModal();
    }

    
}




function AlmacenDestinoDetalle() {

    let AlmacenDestino = $("#IdAlmacenDestino").val();

    let AlmacenOrigen = $("#cboAlmacen").val();

    if (AlmacenDestino == AlmacenOrigen) {
        Swal.fire("El Almacén de Destino no puede ser el mismo de origen")
        $("#IdAlmacenDestino").val(0)
        return;
    }

    $("select[name='cboAlmacen[]']").val(AlmacenDestino);

}





function ObtenerObrasDestino() {

    //var BaseInicio = $("#IdBase").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObra", { 'Estado': 1 }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObraDestino(obra, "IdObraDestino", "Seleccione")
    });
    console.log(1);
}





function llenarComboObraDestino(lista, idCombo, primerItem) {
    console.log(2);
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
}




function ObtenerAlmacenxIdObraDestino() {
    console.log(3);
    let IdObra = $("#IdObraDestino").val();
    console.log(IdObra);
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "IdAlmacenDestino", "Seleccione")
        //llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
    });
    console.log(4);
}



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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function CargarBase() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBase", function (data, status) {
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




function CargarProveedor() {
    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PROVEEDOR</option>`;
        console.log(Proveedor);
        for (var i = 0; i < Proveedor.length; i++) {
            option += `<option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].NumeroDocumento + `-` + Proveedor[i].RazonSocial + `</option>`
        }

        $("#IdTransportista").html(option);
        $("#IdDestinatario").html(option);
        $("#IdTransportista").select2();
        $("#IdDestinatario").select2();

    });
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
}



function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")

    });
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
}



function CargarTipoDocumentoOperacion() {
    $.ajaxSetup({ async: false });
    $.post("/TipoDocumentoOperacion/ObtenerTipoDocumentoOperacion", { estado: 1 }, function (data, status) {
        let tipodocumentooperacion = JSON.parse(data);
        llenarComboTipoDocumentoOperacion(tipodocumentooperacion, "cboTipoDocumentoOperacion", "Seleccione")
    });
}

function llenarComboTipoDocumentoOperacion(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) {
            if (lista[i].IdTipoDocumento == "333" || lista[i].IdTipoDocumento == "1337") {
                contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>";
            }

        }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}

function llenarComboSerie(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 3) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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


function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
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
}



function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
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
function LimpiarObra() {
    $("#cboObraFiltro").prop("selectedIndex", 0)
}


function OcultarTransferencia(IdMovimiento) {
    Swal.fire({
        title: 'DESEA RECHAZAR ESTA TRANSFERENCIA?',
        html: "Esta acción no se puede revertir y No se podrá realizar el ingreso </br>",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Movimientos/OcultarTransferenciaPendiente",
                type: "POST",
                async: true,
                data: {
                    IdMovimiento,
                },
                success: function (data) {
                    if (data > 0) {
                        ConsultaServidor()
                        Swal.fire(
                            'Exito!',
                            'Se oculto la transferencia!',
                            'error'
                        )
                    }else {
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


}