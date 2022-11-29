let table = '';
var ultimaFila = null;
var colorOriginal;


window.onload = function () {
    listarPedidoDtConfirmidad();
};



function listarPedidoDtConfirmidad() {
    let TipoAlmacen = $("#TipoAlmacen").val();
    let Conformidad = $("#Conformidad").val();
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerPedidoDTConfirmidad',
            type: 'POST',
            data: {
                Conformidad: Conformidad,
                TipoAlmacen: TipoAlmacen,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return 0
                    /*return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdPedido + `)"></button>`*/
                    /*  <button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + full.IdPedido + `)"></button>`*/
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombBase
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split("T")[0]
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.total_venta, 2)
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.Conformidad == 0) {
                        return "SIN CONFIRMAR"
                    } else {
                        if (full.Conformidad == 1) {
                            return "Confirmado"
                        } else {
                            return "Rechazado"
                        }
                    }

                },
            }

        ],
        "bDestroy": true
    }).DataTable();

    $('#table_id tbody').unbind("dblclick");
    $('#table_id tbody').on('dblclick', 'tr', function () {
        var data = table.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        $("#IdProveedor").val(data["IdProveedor"]).change();
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        abrirModalPedidoConformidad(data);
        //AgregarOPNDDetalle(data);
        //$('#ModalListadoEntrega').modal('hide');
        //tableentrega.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}


function abrirModalPedidoConformidad(data) {
    $("#npedido").val(data.NombSerie + "-" + data.Correlativo);
    $("#nombproveedor").val(data.NombreProveedor);
    $("#fechapedido").val(data.FechaDocumento.split("T")[0]);
    $("#almacen").val(data.NombAlmacen);
    $("#IdPedido").val(data.IdPedido);

    let Conformidad = data.Conformidad;
    $("#ConformidadModal").val(data.Conformidad);
    if (Conformidad != 0) {
        $("#ConformidadModal").prop('disabled', true);
        $("#comentarioconformidad").prop('disabled', true);
        $("#btnGrabar").hide();
        

    } else {
        $("#ConformidadModal").prop('disabled', false);
        $("#comentarioconformidad").prop('disabled', false);
        $("#btnGrabar").show();

    }
    

    $.post('/Pedido/ObtenerDatosxID', { 'IdPedido': data.IdPedido }, function (data, status) {
        let pedido = JSON.parse(data);
        console.log(pedido);
        $("#CreatedAt").html(pedido.CreatedAt)
        $("#NombUsuario").html(pedido.NombUsuario)
        $("#total_items").html(pedido.detalles.length)
        $("#comentarioconformidad").html(pedido.ComentarioConformidad)
        let tabletr = "";
        for (var i = 0; i < pedido.detalles.length; i++) {
            tabletr += `<tr>
                    <td>`+ pedido.detalles[i].CodigoProducto + `</td>
                    <td>`+ pedido.detalles[i].DescripcionArticulo + `</td>
                    <td>`+ pedido.detalles[i].Cantidad + `</td>
                    <td>`+ pedido.detalles[i].precio_unitario + `</td>`;
            if (pedido.detalles[i].IdMoneda == 1) {
                tabletr += `<td>Soles</td>`;
            } else {
                tabletr += `<td>Dolares</td>`;
            }
            

            tabletr += `<td>` + pedido.detalles[i].total_item +`</td>
                    <td>`+ pedido.NombCondicionPago+`</td>
                    <td>`+ pedido.detalles[i].Referencia+`</td>
                    <td>

                        <input type="hidden" name="IdPedidoDetalle[]" id="IdPedidoDetalle`+ pedido.detalles[i].IdPedidoDetalle +`" value="`+pedido.detalles[i].IdPedidoDetalle+`"/>
                        <textarea class="form-contorl input-sm" name="comentarioconformidaddetalle[]" rows="2" id="comentarioconformidaddetalle`+ pedido.detalles[i].IdPedidoDetalle + `" style="width:100%">
                         `+ pedido.detalles[i].ComentarioConformidad+`</textarea>
                    </td>
                </tr>`
            //console.log(pedido.detalles[i].ComentarioConformidad);
            //$("#comentarioconformidaddetalle" + pedido.detalles[i].IdPedidoDetalle).html(pedido.detalles[i].ComentarioConformidad);
        }
       
        $("#tbody_pedidomodal").html(tabletr);
    });

    AbrirModal("modal-form");
 /*   $('#modal-form').modal('show');*/
}

function GuardarPedidoConformidad() {
    let ConformidadModal = $("#ConformidadModal").val();
    let IdPedido = $("#IdPedido").val();
    let comentarioconformidad = $("#comentarioconformidad").val();
    console.log(comentarioconformidad);
    if (ConformidadModal==0) {
        swal("Error!", "Para poder guardar los datos, debe registrar un estado diferente a Pendiente");
        return;
    }

    let arrayIdPedidoDetalle = new Array()
    $("input[name='IdPedidoDetalle[]']").each(function (indice, elemento) {
        arrayIdPedidoDetalle.push($(elemento).val());
    });


    let arrayComentarioDetalle = new Array()
    $("textarea[name='comentarioconformidaddetalle[]']").each(function (indice, elemento) {
        arrayComentarioDetalle.push($(elemento).val());
    });
    let detalles = new Array();
    for (var i = 0; i < arrayIdPedidoDetalle.length; i++) {
        detalles.push({
            'IdPedidoDetalle': arrayIdPedidoDetalle[i],
            'ComentarioConformidadDetalle': arrayComentarioDetalle[i],
        });
    }

    $.ajax({
        url: "updatedInsertConformidadPedido",
        type: "POST",
        async: true,
        data: {
            detalles,
            'Conformidad': ConformidadModal,
            'ComentarioConformidad': comentarioconformidad,
            'IdPedido': IdPedido
           
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
            if (data >= 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                CerrarModal();

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
    listarPedidoDtConfirmidad()
   
}

function CerrarModal() {
    $.magnificPopup.close();
}