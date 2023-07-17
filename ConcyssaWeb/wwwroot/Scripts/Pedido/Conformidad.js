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
             {"className": "text-center", "targets": "_all"},
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<button class="btn btn-primary btn-xs" onclick="MostrarOC(` + full.IdPedido+`,`+full.Conformidad + `)">OC</button>
                      <button class="btn btn-danger btn-xs" onclick="MostrarCC(` + full.IdPedido + `)">CC</button>`
                },
            },
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombBase
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split("T")[0]
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoPedido.toUpperCase()
                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombMoneda
                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.total_venta, 2)
                },
            },
            {
                data: null,
                targets: 9,
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
function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
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
function MostrarOC(id, conformidad) {
    Swal.fire({
        title: "Buscando Orden de Compra...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    setTimeout(() => {
        if (conformidad == 0) {
            $.ajaxSetup({ async: false });
            $.post("GenerarReporte", { 'NombreReporte': 'OrdenCompraNoValido', 'Formato': 'PDF', 'Id': id }, function (data, status) {
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
                        'Orden de Compra Encontrada',
                        'success'
                    )
                } else {
                    console.log("error");
                }
            });
        } else {
            $.ajaxSetup({ async: false });
            $.post("GenerarReporte", { 'NombreReporte': 'OrdenCompra', 'Formato': 'PDF', 'Id': id }, function (data, status) {
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
                        'Orden de Compra Encontrada',
                        'success'
                    )
                } else {
                    respustavalidacion
                }
            });
        }
    }, 100)
}
function MostrarCC(id) {
    Swal.fire({
        title: "Buscando Cuadro Comparativo...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    setTimeout(() => {
        $.ajaxSetup({ async: false });
        $.post("GenerarReporte", { 'NombreReporte': 'CuadroComparativo', 'Formato': 'PDF', 'Id': id }, function (data, status) {
            let datos;
            if (validadJson(data)) {
                let datobase64;
                datobase64 = "data:application/octet-stream;base64,"
                datos = JSON.parse(data);
                /*datobase64 += datos.Base64ArchivoPDF;*/
                //$("#reporteRPT").attr("download", 'Reporte.' + "pdf");
                //$("#reporteRPT").attr("href", datobase64);
                //$("#reporteRPT")[0].click();
                verBase64PDF(datos)

                Swal.fire(
                    'Correcto',
                    'Cuadro Comparativo Encontrado',
                    'success'
                )

            } else {
                respustavalidacion
            }
        });
    }, 100)

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
                    <td>`+ formatNumberDecimales(pedido.detalles[i].Cantidad,2) + `</td>
                    <td>`+ formatNumberDecimales(pedido.detalles[i].valor_unitario,2) + `</td>`;
            if (pedido.IdMoneda == 1) {
                tabletr += `<td>Soles</td>`;
            } else {
                tabletr += `<td>Dolares</td>`;
            }
            

            tabletr += `<td>` + formatNumberDecimales(pedido.detalles[i].total_valor_item,2) +`</td>
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
    $("#IdPedido").val(data.IdPedido);

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