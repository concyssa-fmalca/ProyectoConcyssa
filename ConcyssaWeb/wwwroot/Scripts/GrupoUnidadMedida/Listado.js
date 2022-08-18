let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0


let contadoritem = 0;

window.onload = function () {
    var url = "ObtenerGrupoUnidadMedida";
    //ObtenerConfiguracionDecimales();
    ConsultaServidor(url);
};



function AgregarLinea() {
   
    let tr = `<tr>
                                                <td>
                                                    <input class="form-control" type="hidden" id="IdDefinicionGrupo`+ contadoritem +`" name="IdDefinicionGrupo[]" value="0" />      
                                                    <input class="form-control" type="text" id="cantidadabase`+ contadoritem +`" name="CantidadBase[]" />
                                                </td>
                                                <td>
                                                    <select id="UnidadMedidaBase`+ contadoritem +`" class="form-control select2" name="UnidadMedidaBase[]">
                                                        <option value="0">Unidad Medida</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input class="form-control" type="text" id="CantidadAlt`+ contadoritem +`" name="CantidadAlt[]" />
                                                </td>
                                                <td>
                                                    <select id="UnidadMedidaAlt`+ contadoritem +`" class="form-control select2" name="UnidadMedidaAlt[]">
                                                        <option value="0">Unidad Medida</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <button class="btn btn-xs btn-danger"  onclick="EliminarDetalle()">-</button>
                                                </td>
                                            </tr>`;
    $("#tbody_detalle").append(tr);
    CargarUnidadMedida("UnidadMedidaBase" + contadoritem);
    CargarUnidadMedida("UnidadMedidaAlt" + contadoritem);
    contadoritem++
}


function CargarUnidadMedida(idCombo) {
    console.log(idCombo);
    $.ajaxSetup({ async: false });
    $.post("../UnidadMedida/ObtenerUnidadMedidasxEstado", { estado: 1 }, function (data, status) {
        let unidades = JSON.parse(data);
        llenarComboUnidadMedida(unidades, idCombo, "Seleccione")
        llenarComboUnidadMedida(unidades, idCombo, "Seleccione")
    });
}

function llenarComboUnidadMedida(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}





function ConsultaServidor(url) {
    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let grupounidades = JSON.parse(data);
        let total_solicitudes = grupounidades.length;

        console.log(grupounidades);
        let estado = "Desactivado";
        for (var i = 0; i < grupounidades.length; i++) {
            if (grupounidades[i].Estado) {
                estado = "Activado";
            }

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + grupounidades[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + grupounidades[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + estado + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + grupounidades[i].IdGrupoUnidadMedida +')"></button>' +
                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_GrupoUnidadMedida").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {

    $("#lblTituloModal").html("Nuevo Grupo Unidad Medida");
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
}



$(document).on('click', '.borrar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();

    let filas = $("#tabla").find('tbody tr').length;
    console.log("filas");
    console.log(filas);
});



function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
}



function GuardarGrupoUnidadMedida() {
    let IdGrupoUnidadMedida = $("#IdGrupoUnidadMedida").val();
    let Codigo = $("#Codigo").val();
    let Descripcion = $("#Descripcion").val();


    let CantidadBase = new Array();
    let UnidadMedidaBase = new Array();
    let CantidadAlt = new Array();
    let UnidadMedidaAlt = new Array();
    let IdDefinicionGrupo = new Array();
    $("input[name='CantidadBase[]']").each(function (indice, elemento) {
        CantidadBase.push($(elemento).val());
    });
    $("select[name='UnidadMedidaBase[]']").each(function (indice, elemento) {
        UnidadMedidaBase.push($(elemento).val());
    });
    $("input[name='CantidadAlt[]']").each(function (indice, elemento) {
        CantidadAlt.push($(elemento).val());
    });
    $("select[name='UnidadMedidaAlt[]']").each(function (indice, elemento) {
        UnidadMedidaAlt.push($(elemento).val());
    });
    $("input[name='IdDefinicionGrupo[]']").each(function (indice, elemento) {
        IdDefinicionGrupo.push($(elemento).val());
    });

    let lstDefinicionGrupoUnidadDTO = [];
    for (var i = 0; i < UnidadMedidaAlt.length; i++) {
        console.log(CantidadBase[i]);
        lstDefinicionGrupoUnidadDTO.push({
            'IdDefinicionGrupo': IdDefinicionGrupo[i],
            'IdGrupoUnidadMedida': IdGrupoUnidadMedida,
            'IdUnidadMedidaBase': UnidadMedidaBase[i],
            'CantidadBase': parseFloat(CantidadBase[i]),
            'IdUnidadMedidaAlt': UnidadMedidaAlt[i],
            'CantidadAlt': parseFloat(CantidadAlt[i]),
        })
    }
    $.ajax({
        url: "UpdateInsertGrupoUnidadMedida",
        type: "POST",
        async: true,
        data: {
            //cabecera
            'IdGrupoUnidadMedida': IdGrupoUnidadMedida,
            'Codigo': Codigo,
            'Descripcion': Descripcion,
            //end cabecera
            //DETALLE
            lstDefinicionGrupoUnidadDTO
            // END Detalle
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
                table.destroy();
                ConsultaServidor("ObtenerGrupoUnidadMedida");

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

function limpiarDatos() {

    $("#txtId").val('');
    $("#cboSerie").val('');
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
}


function ObtenerDatosxID(IdGrupoUnidadMedida) {
    $("#lblTituloModal").html("Editar Grupo Unidad Medida");
    AbrirModal("modal-form");


    $.post('ObtenerDatosxID', {
        'IdGrupoUnidadMedida': IdGrupoUnidadMedida,
    }, function (data, status) {
        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupounidadmedida = JSON.parse(data);

            let cabecera = (JSON.parse(grupounidadmedida[0]));
            let detalle = (JSON.parse(grupounidadmedida[1]));
            
            $("#Codigo").val(cabecera[0].Codigo)
            $("#Descripcion").val(cabecera[0].Descripcion)
            $("#IdGrupoUnidadMedida").val(IdGrupoUnidadMedida);
            let tr = '';
            for (var i = 0; i < detalle.length; i++) {

                tr = `<tr>
                                                <td>
                                                    <input class="form-control" type="hidden" id="IdDefinicionGrupo`+ detalle[i].IdDefinicionGrupo + `" name="IdDefinicionGrupo[]" value="` + detalle[i].IdDefinicionGrupo +`" />      
                                                    <input class="form-control" type="text" id="cantidadabase`+ detalle[i].IdDefinicionGrupo + `" name="CantidadBase[]" value="` + detalle[i].CantidadBase +`" />
                                                </td>
                                                <td>
                                                    <select id="UnidadMedidaBase`+ detalle[i].IdDefinicionGrupo + `" class="form-control select2" name="UnidadMedidaBase[]">
                                                        <option value="0">Unidad Medida</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input class="form-control" type="text" id="CantidadAlt`+ detalle[i].IdDefinicionGrupo + `" name="CantidadAlt[]" value="` + detalle[i].CantidadAlt +`"/>
                                                </td>
                                                <td>
                                                    <select id="UnidadMedidaAlt`+ detalle[i].IdDefinicionGrupo + `" class="form-control select2" name="UnidadMedidaAlt[]">
                                                        <option value="0">Unidad Medida</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <button class="btn btn-xs btn-danger"  onclick="EliminarDetalle()">-</button>
                                                </td>
                                            </tr>`;
                $("#tbody_detalle").append(tr);
                CargarUnidadMedida("UnidadMedidaBase" + detalle[i].IdDefinicionGrupo);
                CargarUnidadMedida("UnidadMedidaAlt" + detalle[i].IdDefinicionGrupo);

                $("#UnidadMedidaBase" + detalle[i].IdDefinicionGrupo).val(detalle[i].IdUnidadMedidaBase);
                $("#UnidadMedidaAlt" + detalle[i].IdDefinicionGrupo).val(detalle[i].IdUnidadMedidaAlt);
            }



        }

    });

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






function CerrarModalListadoItems() {
    tableItems.destroy();
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