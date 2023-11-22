window.onload = function () {
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    CargarObras()
    //listarDatos()
    CargarDatosTrabajo()
};

function CargarObras() {
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let obras = JSON.parse(data);
        llenarComboObra(obras, "cboObraFiltro", "Seleccione")
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion + "</option>"; ultimoindice = i }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboObraFiltro").prop("selectedIndex", 1)
    

}

function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': $("#cboObraFiltro").val()}, function (data, status) {
        let obras = JSON.parse(data);
        llenarComboAlmacen(obras, "cboAlmacenFiltro", "Seleccione")
    });
}

function llenarComboAlmacen(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdAlmacen + "'>" + lista[i].Descripcion + "</option>"; ultimoindice = i }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboAlmacenFiltro").prop("selectedIndex", 1)

}

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

function listarDatos() {
    let varIdObraFiltro = $("#cboObraFiltro").val()
    let fechaIn = $("#txtFechaInicio").val()
    let fechaFn = $("#txtFechaFin").val()
    let TipoArticulo = $("#IdTipoProducto").val()
    let ClaseArticulo = $("#cboClaseArticulo").val()

    Swal.fire({
        title: "Cargando...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        tablepedido = $('#table_id').dataTable({
            pageLength: 10,
            language: lenguaje_data,
            responsive: true,

            ajax: {
                url: 'ListarMovimientosKardex',
                dataSrc: "",
                type: 'POST',
                data: {
                    'IdObra': varIdObraFiltro,
                    'FechaInicio': fechaIn,
                    'FechaTermino': fechaFn,
                    'ClaseArticulo': ClaseArticulo,
                    'TipoArticulo': TipoArticulo,
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
                        return "<input style='display:none' type='text' value='" + meta.row + 1 + "' class='datos'/>" + (meta.row + 1)
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
                        if (fecha == "1999-01-01") return '-'
                        return fecha.split('-')[2] + '-' + fecha.split('-')[1] + '-' + fecha.split('-')[0] + ' ' + full.FechaRegistro.split('T')[1]
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
                        if (full.DescSerie == '-') return '-'
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
                    targets: 8,
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
                        return full.DocEntrySap
                    },
                },  


            ],
            "bDestroy": true
        }).DataTable();
        Swal.close()
    }, 200)
}


function CargarDatosTrabajo() {
    $.ajaxSetup({ async: false });
    $.post("ObtenerListaDatosTrabajo", function (data, status) {
        if (data == "error") {
            contenido = "<option value='0'>NUEVO</option>"
            $("#cboDatosTrabajo").html(contenido)
        }
        let obras = JSON.parse(data);
        llenarComboObrae(obras, "cboDatosTrabajo", "NUEVO")

    });
}

function llenarComboObrae(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) {
            let fechagc = lista[i].FechaCreacionTabla.split('T')[0]
            let horagc = lista[i].FechaCreacionTabla.split('T')[1]
            contenido += "<option value='" + lista[i].GrupoCreacion + "'>" + lista[i].Usuario + '/' + fechagc + '/' + horagc + '/ ' + lista[i].EstadoEnviado + "</option>"; ultimoindice = i
        }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //$("#cboDatosTrabajo").prop("selectedIndex", 1)

}


function abrirDatosTrabajo() {

    let varIdObraFiltro = $("#cboObraFiltro").val()
    let fechaIn = $("#txtFechaInicio").val()
    let fechaFn = $("#txtFechaFin").val()
    let TipoArticulo = $("#IdTipoProducto").val()
    let ClaseArticulo = $("#cboClaseArticulo").val()

    let varGrupoCreacion = 0

    let contadorDatos = 0;
    $(".datos").each(function (indice, elemento) {
        contadorDatos++
    });



    $.post("ObtenerGrupoCreacionEnviarSap", function (data, status) {
        if (data > 0) {
            console.log(data)
            varGrupoCreacion = data
        } else {
            Swal.fire("Error!", "Ocurrió un Error", "error");
            return
        }
    })



    if ($("#cboDatosTrabajo").val() == 0) {

        if (contadorDatos == 0) {
            Swal.fire("No se encontraron Datos en la Tabla")
            return
        }

        $.post("MoverKardexAEnviarSapConsumo", {
            'IdObra': varIdObraFiltro,
            'FechaInicio': fechaIn,
            'FechaTermino': fechaFn,
            'ClaseArticulo': ClaseArticulo,
            'TipoArticulo': TipoArticulo,
            'GrupoCreacion': varGrupoCreacion
        }, function (data, status) {
                if (data > 0) {
                    console.log('Copiado')
                    window.location.href = "Listado2?l=" + varGrupoCreacion

                } else if(data == -1) {
                    console.log('No Copiado')
                    Swal.fire("Sin Datos!", "No hay Datos Pendientes de Migracion", "error");
                }else {
                    console.log('No Copiado')
                    Swal.fire("Error!", "Error al crear marco de Trabajo", "error");
                }
        });

    } else {
        window.location.href = "Listado2?l=" + $("#cboDatosTrabajo").val()
    }


}

function cambiarTextoBoton() {
    if ($("#cboDatosTrabajo").val() == 0) {
        $("#btn_abrirDatosTrabajo").text('Crear Datos de Trabajo')
    } else {
        $("#btn_abrirDatosTrabajo").text('Abrir Datos de Trabajo')
    }
}


//function ValidarObra() {
//    if ($("#cboObraFiltro").val() !== '0') {
//        $("#cboClaseArticulo").prop('disabled', false)
//        $("#IdTipoProducto").prop('disabled', true)
//    } else {
//        $("#cboClaseArticulo").prop('disabled', true)
//        $("#IdTipoProducto").prop('disabled', true)
//    }
//}
//function ValidarTipoArticulo() {
//    if ($("#cboClaseArticulo").val() == '0') {
//        $("#IdTipoProducto").prop('disabled', true)
//        $("#divOcultar").show()
//    } else if ($("#cboClaseArticulo").val() == '1') {
//        $("#IdTipoProducto").prop('disabled', false)
//        $("#divOcultar").show()

//    } else if ($("#cboClaseArticulo").val() == '3') {
//        BuscarCodigoProducto()
//        $("#divOcultar").hide()
//    }
//}

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
