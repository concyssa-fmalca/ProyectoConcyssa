
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
    CargarArticulos();
    CargarAlmacen();    
    $("#IdArticulo").select2();
    $("#IdAlmacen").select2();

    


};


function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "IdAlmacen", "Seleccione")
    });
}

function CargarArticulos() {
    $.ajaxSetup({ async: false });
    $.post("/Articulo/ObtenerArticulosxSociedad", {estado:1}, function (data, status) {
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdArticulo + "'>" + lista[i].Codigo.toUpperCase() +' - ' + lista[i].Descripcion1.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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
    let IdArticulo= $("#IdArticulo").val();
    let IdAlmacen=$("#IdAlmacen").val();
    let FechaInicio= $("#FechaInicio").val();
    let FechaTermino = $("#FechaTermino").val();
    if (FechaInicio =='') {
        FechaInicio='2022-01-01'
    }

    if (FechaTermino == '') {
        FechaTermino = '2050-01-01'
    }
   
    //ListarDatatableKardex('ListarKardex',IdArticulo,IdAlmacen,FechaInicio, FechaTermino)
    ListarDatatableKardexDT('ListarKardexDT', IdArticulo, IdAlmacen, FechaInicio, FechaTermino)
}


function ListarDatatableKardex(url,IdArticulo, IdAlmacen, FechaInicio, FechaTermino) {
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
             
                '<td>' + division[i].FechaRegistro+ '</td>';
            if (division[i].DetalleOPDN != 0) {
                tr += '<td>ENTREGA MERCANCIA</td>';

            } else {
                tr += '<td>' + division[i].TipoTransaccion.toUpperCase() + '</td>';

            }
              
                tr += '<td>' + division[i].DescSerie.toUpperCase() + `-` + division[i].Correlativo+ '</td>' +
                '<td>' +'-'+ '</td>' +
                '<td>' + division[i].FechaDocumento.split('T')[0] + '</td>' +
                '<td>' + division[i].DescUnidadMedidaBase + '</td>' +
                '<td>' + formatNumber( division[i].CantidadBase ) + '</td>' +
                '<td>' + formatNumber(division[i].PrecioRegistro) + '</td>' +
                '<td>' + formatNumber( (division[i].CantidadBase * division[i].PrecioPromedio)) + '</td>' +
                '<td>' + division[i].Saldo + '</td>' +
                
                '<td>' + formatNumber(division[i].PrecioPromedio) + '</td>'+
                '<td>' + formatNumber(Math.abs(division[i].PrecioPromedio * division[i].Saldo)) + '</td>' +
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
    tablekardex = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: url,
            type: 'POST',
            data: {
                'IdArticulo': IdArticulo,
                'IdAlmacen': IdAlmacen,
                'FechaInicio': FechaInicio,
                'FechaTermino': FechaTermino,
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
                    return full.Modulo
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaRegistro
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoTransaccion
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.DescSerie.toUpperCase() + `-` + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '-'
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split('T')[0]
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.DescUnidadMedidaBase
                },
            },
            {
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.CantidadBase)
                },
            },
            {
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.PrecioRegistro)
                },
            },
            {
                targets: 10,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber((full.CantidadBase * full.PrecioRegistro))
                },
            },
            {
                targets: 11,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Saldo
                },
            },
            {
                targets: 12,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.PrecioPromedio
                },
            },
            {
                targets: 13,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(Math.abs(full.PrecioPromedio * full.Saldo))
                },
            },
            {
                targets: 14,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario
                },
            },
            {
                targets: 15,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Comentario
                },
            }
        ],
        "bDestroy": true
    }).DataTable();

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



