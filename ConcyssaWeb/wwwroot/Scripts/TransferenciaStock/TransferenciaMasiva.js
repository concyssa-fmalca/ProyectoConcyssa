window.onload = function () {
cargarAlmacenes() 
};

function cargarAlmacenes() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let DatosAlmacen = JSON.parse(data);
        llenarComboAlmacen(DatosAlmacen, "IdAlmacenOrigen", "seleccione")
        llenarComboAlmacen(DatosAlmacen, "IdAlmacenDestino", "seleccione")
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




function ObtenerStockxAlmacen() {

        table = $('#table_id').dataTable({
            language: {
                lenguaje_data,
                loadingRecords: "No se Encontraron datos para este Almacen y Tipo Producto",
            },
            responsive: true,
            ajax: {
                url: '/InformeInventarioAlmacen/ListarArticuloStockXIdTipoProducto',
                type: 'POST',
                error: function (xhr, status, error) {
                    console.error(error);
                },
                data: {
                    'IdAlmacen': $("#IdAlmacenOrigen").val(),
                    'IdTipoProducto': $("#IdTipoProducto").val(),
                    pagination: {
                        perpage: 50,
                    },
                },
            },

            columnDefs: [
              
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
                        return full.Codigo.toUpperCase()
                    },
                },
                {
                    data: null,
                    targets: 2,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return full.NombArticulo.toUpperCase()
                    },
                },
                {
                    data: null,
                    targets: 3,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumberDecimales(full.Stock,2)
                    },
                },
                {
                    data: null,
                    targets: 4,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return formatNumberDecimales(full.PrecioPromedio,2)
                    },
                },
             
            ],
            "bDestroy": true
        }).DataTable();
    
}

function TransferenciaMasiva() {
    if ($("#IdAlmacenOrigen").val() == 0) {
        Swal.fire("Complete el Campo Almacen Origen")
        return;
    }
    if ($("#IdAlmacenDestino").val() == 0) {
        Swal.fire("Complete el Campo Almacen Destino")
        return;
    }


    if ($("#IdAlmacenOrigen").val() == $("#IdAlmacenDestino").val()) {
        Swal.fire("Los almacenes no pueden ser el mismo")
        return;
    }


    Swal.fire({
        title: 'DESEA GENERAR LA TRANSFERENCIA MASIVA?',
        html: "Se creará un Documento de Salida y otro de Entrada Respectivamente</br>" +
            "</br>" +
            "Serie Para Salida </br>" +
            "<Select id='cboSerieSalida' class='form-control'></select>" +
            "</br>" +
            "Serie Para Entrada </br>" +
            "<Select id='cboSerieEntrada' class='form-control'></select>" +
            "</br>",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {

            if ($("#cboSerieSalida").val() == 0) {
                Swal.fire("Complete el Campo Serie de Salida")
                return;
            }
            if ($("#cboSerieEntrada").val() == 0) {
                Swal.fire("Complete el Campo Serie de Entrada")
                return;
            }


            $.ajax({
                url: "RealizarTransferenciaMasiva",
                type: "POST",
                async: true,
                data: {
                    'IdAlmacenOrigen': $("#IdAlmacenOrigen").val(),
                    'IdAlmacenDestino': $("#IdAlmacenDestino").val(),
                    'IdTipoProducto': $("#IdTipoProducto").val(),
                    'IdSerieSalida': $("#cboSerieSalida").val(),
                    'IdSerieEntrada': $("#cboSerieEntrada").val(),
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
                    if (data == "OK") {
                        Swal.fire("Correcto", "Migración Realizada", "success");
                        ObtenerStockxAlmacen()
                    } else {
                        Swal.fire("Error", data, "error");
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
        llenarComboSerieSalida(series, "cboSerieSalida", "Seleccione")
        llenarComboSerieEntrada(series, "cboSerieEntrada", "Seleccione")
    });

}


function llenarComboSerieSalida(lista, idCombo, primerItem) {

    $("#FechDocExtorno").val($("#txtFechaDocumento").val())
    $("#FechContExtorno").val($("#txtFechaContabilizacion").val())

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;

    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 2) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();
}

function llenarComboSerieEntrada(lista, idCombo, primerItem) {

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