
let table = '';
let tablekardex;
function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;


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


window.onload = function () {

    console.log('ddddddddddddddddddddd');
    CargarAlmacen();
    $("#IdArticulo").select2();
    $("#IdAlmacen").select2();
    $("#FechaInicio").val(getCurrentDate())
    $("#FechaTermino").val(getCurrentDateFinal())

    //tablekardex = $("#table_id").dataTable();

};

function BuscarCodigoProducto() {

    let TipoItem = $("#cboClaseArticulo").val();
    let IdAlmacen = $("#IdAlmacen").val();
    let IdTipoProducto = $("#IdTipoProducto").val();
    if (IdAlmacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    $("#ModalListadoItem").modal();
    if (TipoItem == 1) {
        $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, }, function (data, status) {

            if (data == "error") {
                swal("Informacion!", "No se encontro Articulo")
            } else {
                let articulo = JSON.parse(data);
                llenarComboArticulo(articulo, "IdArticulo", "TODOS")
            }

        });
    } else {
        $.post("/Articulo/ObtenerArticulosActivoFijo",

            function (data, status) {

                if (data == "error") {
                    swal("Informacion!", "No se encontro Articulo")

                } else {

                    let articulo = JSON.parse(data);
                    llenarComboArticulo(articulo, "IdArticulo", "TODOS")
                }

            });
    }
}
function ValidarAlmacen() {
    if ($("#IdAlmacen").val() !== '0') {
        $("#cboClaseArticulo").prop('disabled',false)
        $("#IdTipoProducto").prop('disabled',true)
    } else {
        $("#cboClaseArticulo").prop('disabled', true)
        $("#IdTipoProducto").prop('disabled', true)
    }
}
function ValidarTipoArticulo() {
    if ($("#cboClaseArticulo").val() == '0') {
        $("#IdTipoProducto").prop('disabled', true)
        $("#divOcultar").show()
    } else if ($("#cboClaseArticulo").val() == '1') {
        $("#IdTipoProducto").prop('disabled', false)
        $("#divOcultar").show()

    } else if ($("#cboClaseArticulo").val() == '3') {
        BuscarCodigoProducto()
        $("#divOcultar").hide()
    }
}

function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdUsuario", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "IdAlmacen", "Seleccione")
    });
}

function CargarArticulos() {
    $.ajaxSetup({ async: false });
    $.post("/Articulo/ObtenerArticulosxSociedad", { estado: 1 }, function (data, status) {
        let articulo = JSON.parse(data);
        llenarComboArticulo(articulo, "IdArticulo", "Seleccione")
    });
}

function llenarComboArticulo(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdArticulo + "'>" + lista[i].Codigo.toUpperCase() + ' - ' + lista[i].Descripcion1.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#IdArticulo").prop('selectedIndex', 0).change();
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


function BuscarKardex() {
    let IdArticulo = $("#IdArticulo").val();
    let IdAlmacen = $("#IdAlmacen").val();
    let FechaInicio = $("#FechaInicio").val();
    let FechaTermino = $("#FechaTermino").val();
    if (FechaInicio == '') {
        FechaInicio = '2022-01-01'
    }

    if (FechaTermino == '') {
        FechaTermino = '2050-01-01'
    }
    if ($("#IdAlmacen").val()==0) {
        swal("Informacion!", "Seleccione un Almacen");
        return;
    }

    if ($("#cboClaseArticulo").val() == 0) {
        swal("Informacion!", "Seleccione el Tipo de Articulo");
        return;
    }
    if ($("#cboClaseArticulo").val() == 1) {
        if ($("#IdTipoProducto").val() == 0) {
            swal("Informacion!", "Seleccione el Tipo de Producto");
            return;
        }
    }

    if ($("#FechaInicio").val() == '') {
        swal("Informacion!", "Seleccione una Fecha de Inicio");
        return;
    }
    //ListarDatatableKardex('ListarKardex',IdArticulo,IdAlmacen,FechaInicio, FechaTermino)
    ListarDatatableKardexDT('ListarKardexDT', IdArticulo, IdAlmacen, FechaInicio, FechaTermino)
}


function ListarDatatableKardex(url, IdArticulo, IdAlmacen, FechaInicio, FechaTermino) {
    $.post(url, { 'IdArticulo': IdArticulo, 'IdAlmacen': IdAlmacen, 'FechaInicio': FechaInicio, 'FechaTermino': FechaTermino }, function (data, status) {
        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let division = JSON.parse(data);
        let total_division = division.length;
        let entrada = 0;
        let salida = 0;
        for (var i = 0; i < division.length; i++) {
            entrada = 0;
            salida = 0;
            if (division[i].CantidadBase >= 0) {
                entrada = division[i].CantidadBase;
            } else {
                salida = division[i].CantidadBase;
            }

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +

                '<td>' + division[i].FechaRegistro + '</td>';
            if (division[i].DetalleOPDN != 0) {
                tr += '<td>ENTREGA MERCANCIA</td>';

            } else {
                tr += '<td>' + division[i].TipoTransaccion.toUpperCase() + '</td>';

            }

            tr += '<td>' + division[i].DescSerie.toUpperCase() + `-` + division[i].Correlativo + '</td>' +
                '<td>' + '-' + '</td>' +
                '<td>' + division[i].FechaDocumento.split('T')[0] + '</td>' +
                '<td>' + division[i].DescUnidadMedidaBase + '</td>' +
                '<td>' + formatNumber(division[i].CantidadBase) + '</td>' +
                '<td>' + formatNumber(division[i].PrecioRegistro) + '</td>' +
                '<td>' + formatNumber((division[i].CantidadBase * division[i].PrecioPromedio)) + '</td>' +
                '<td>' + division[i].Saldo + '</td>' +

                '<td>' + formatNumber(division[i].PrecioPromedio) + '</td>' +
                //'<td>' + formatNumber(Math.abs(division[i].PrecioPromedio * division[i].Saldo)) + '</td>' +
                '<td>' + (division[i].PrecioPromedio * division[i].Saldo) + '</td>' +
                '<td>' + division[i].NombUsuario + '</td>' +
                '<td>' + division[i].Comentario + '</td>' +

                '</tr>';
        }

        $("#tbody_division").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}



function ListarDatatableKardexDT(url, IdArticulo, IdAlmacen, FechaInicio, FechaTermino) {

    Swal.fire({
        title: "Cargando...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        tablekardex = $('#table_id').dataTable({
        

            language: {
                lenguaje_data,
                loadingRecords: "No se Encontraron datos para este Artículo",
            },
            responsive: false,
            scrollX: true,
            ajax: {
                url: url,
                type: 'POST',
                error: function (xhr, status, error) {
                    console.error(error);
                },
                data: {
                    'IdArticulo': IdArticulo,
                    'IdAlmacen': IdAlmacen,
                    'FechaInicio': FechaInicio + " 00:00:00",
                    'FechaTermino': FechaTermino + " 23:59:59",
                    'ClaseArticulo': $("#cboClaseArticulo").val(),
                    'TipoArticulo': $("#IdTipoProducto").val(),
                    pagination: {
                        perpage: 50,
                    },                
                },
            },

            columnDefs: [
                // {"className": "text-center", "targets": "_all"},
                //{
                //    targets: -1,
                //    orderable: false,
                //    render: function (data, type, full, meta) {

                //        return 0
                //    },
                //},
                {
                    data: null,
                    targets: 0,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return "<input style='display:none' type='text' value='" + meta.row + 1 +"' class='datos'/>"+ (meta.row + 1)
                    },
                },
                {
                    data: null,
                    targets: 1,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.Modulo
                    },
                },
                {
                    data: null,
                    targets: 2,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.CodigoArticulo
                    },
                },
                {
                    data: null,
                    targets: 3,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.DescArticulo
                    },
                },
                {
                    data: null,
                    targets: 4,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        let fecha = full.FechaRegistro.split('T')[0]
                        if(fecha == "1999-01-01") return '-'
                        return fecha.split('-')[2] + '-'+fecha.split('-')[1] + '-' + fecha.split('-')[0]+' '+ full.FechaRegistro.split('T')[1]
                    },
                },
                {
                    data: null,
                    targets: 5,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.TipoTransaccion
                    },
                },
                {
                    data: null,
                    targets: 6,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        if(full.DescSerie == '-') return '-'
                        return full.DescSerie.toUpperCase() + `-` + full.Correlativo
                    },
                },
                {
                    data: null,
                    targets: 7,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        let fechaDoc = full.FechaDocumento.split('T')[0]
                        if (fechaDoc == "1999-01-01") return '-'
                        return fechaDoc.split('-')[2] + '-' + fechaDoc.split('-')[1] + '-' + fechaDoc.split('-')[0] 
                    },
                },
                {
                    data: null,
                    targets:8,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        let fechaCont = full.FechaContabilizacion.split('T')[0]
                        if (fechaCont == "1999-01-01") return '-'
                        return fechaCont.split('-')[2] + '-' + fechaCont.split('-')[1] + '-' + fechaCont.split('-')[0]
                    },
                },
                {
                    data: null,
                    targets: 9,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.TipoDocumentoRef
                    },
                },
                {
                    data: null,
                    targets: 10,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        if (full.NumSerieTipoDocumentoRef == '-0') {
                            return '-'
                        } else {
                            return full.NumSerieTipoDocumentoRef.toUpperCase()
                        }
                  
                    },
                },
                {
                    data: null,
                    targets: 11,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.DescUnidadMedidaBase
                    },
                },
                {
                    data: null,
                    targets: 12,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumber(full.CantidadBase)
                    },
                },
                {
                    data: null,
                    targets: 13,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumber(full.PrecioBase)
                    },
                },
                {
                    data: null,
                    targets: 14,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumber((full.CantidadBase * full.PrecioBase))
                    },
                },
                {
                    data: null,
                    targets: 15,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.Saldo
                    },
                },
                {
                    data: null,
                    targets: 16,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumberDecimales(full.PrecioPromedio, 2)
                    },
                },
                {
                    data: null,
                    targets: 17,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumber(Math.abs(full.PrecioPromedio * full.Saldo))
                    },
                },
                {
                    data: null,
                    targets: 18,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.NombUsuario
                    },
                },   
                {
                    data: null,
                    targets: 19,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.Cuadrilla
                    },
                },   
                {
                    data: null,
                    targets: 20,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.NumRef
                    },
                },   
            ],
            //"drawCallback": function (settings) {
            //    var api = this.api();

            //    // Iterar a través de las filas de la tabla
            //    api.rows().every(function (rowIdx, tableLoop, rowLoop) {
            //        var row = this.node();

            //        // Obtener el valor de la columna 3 (índice 2) usando "render"
            //        var valorColumna3 = api.cell({ row: rowIdx, column: 3 }).render().CodigoArticulo;
            //        console.log(valorColumna3)

            //        // Comprobar si el valor ha cambiado
            //        if (valorColumna3 !== $(row).data('valorColumna3')) {
            //            // Cambiar el color de fondo de la fila
            //            $(row).css('background-color', 'lemonchiffon');

            //            // Actualizar el valor de referencia para futuras comparaciones
            //            $(row).data('valorColumna3', valorColumna3);
            //        } else {
            //            // Restablecer el color de fondo
            //            $(row).css('background-color', '');
            //        }
            //    });
            //},
    
            "bDestroy": true
        });
        Swal.close()
    }, 200)
    
   



       

    //$('#tabla_listado_pedidos tbody').on('dblclick', 'tr', function () {
    //    var data = tablepedido.row(this).data();
    //    console.log(data);
    //    if (ultimaFila != null) {
    //        ultimaFila.css('background-color', colorOriginal)
    //    }
    //    colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
    //    $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
    //    ultimaFila = $("#" + data["DT_RowId"]);
    //    AgregarPedidoToEntradaMercancia(data);
    //    $('#ModalListadoPedido').modal('hide');
    //    tablepedido.ajax.reload()
    //    //$("#tbody_detalle").find('tbody').empty();
    //    //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    //});
}

















function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let division = JSON.parse(data);
        let total_division = division.length;

        for (var i = 0; i < division.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + division[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + division[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + division[i].IdDivision + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + division[i].IdDivision + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_division").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Division");
    AbrirModal("modal-form");
}




function GuardarDivision() {
    let varIdDivision = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertDivision', {
        'IdDivision': varIdDivision,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerDivision");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdDivision) {
    $("#lblTituloModal").html("Editar Division");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdDivision': varIdDivision,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let division = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(division[0].IdDivision);
            $("#txtCodigo").val(division[0].Codigo);
            $("#txtDescripcion").val(division[0].Descripcion);
            if (division[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Division?', function () {
        $.post("EliminarLineaNegocio", { 'IdLineaNegocio': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Division Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerDivision");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}




function ReporteKardex() {
    let IdArticulo = $("#IdArticulo").val();
    let IdAlmacen = $("#IdAlmacen").val();
    let FechaInicio = $("#FechaInicio").val();
    let FechaTermino = $("#FechaTermino").val();
    if (FechaInicio == '') {
        FechaInicio = '2022-01-01'
    }

    if (FechaTermino == '') {
        FechaTermino = '2050-01-01'
    }

    $.ajaxSetup({ async: false });
    $.post("GenerarReporte", {
        'NombreReporte': 'Kardex', 'Formato': 'PDF', 'IdArticulo': IdArticulo, 'IdAlmacen': IdAlmacen, 'FechaInicio': FechaInicio, 'FechaTermino': FechaTermino
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
        } else {
            respustavalidacion
        }
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

function ImportarExcel() {

    let contadorDatos = 0;
    $(".datos").each(function (indice, elemento) {
        contadorDatos++
    });

    if (contadorDatos == 0) {
        Swal.fire("No se encontraron Datos en la Tabla")
        return
    }


    let IdArticulo = $("#IdArticulo").val();
    let IdAlmacen = $("#IdAlmacen").val();
    let FechaInicio = $("#FechaInicio").val();
    let FechaTermino = $("#FechaTermino").val();

    $.post('GenerarExcel', {
        'IdArticulo': IdArticulo,
        'IdAlmacen': IdAlmacen,
        'FechaInicio': FechaInicio,
        'FechaTermino': FechaTermino,
        'ClaseArticulo': $("#cboClaseArticulo").val(),
        'TipoArticulo': $("#IdTipoProducto").val(),
        'NombreAlmacen': $("#IdAlmacen option:selected").text()
    }, function (data, status) {
        0
    
        if (data) {
            window.open("/Anexos/"+ data+".xlsx", '_blank', 'noreferrer');
        }
    });


   
}