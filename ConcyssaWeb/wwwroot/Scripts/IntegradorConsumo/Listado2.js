window.onload = function () {
    let link = window.location.href
    $("#txtIdDatosTrabajo").val(link.split('=')[1])
    consultaServidor()
    CargarSerie()
    $("#DivGiro").hide();
    ValidarEnvioSap()
    $("#txtFechaContabilizacion").val(getFechaHoy())
    $("#SubirAnexos").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData($("#SubirAnexos")[0]);
        $.ajax({
            url: "GuardarFile",
            type: "POST",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (datos) {
                let data = JSON.parse(datos);
                console.log(data);
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
};

function getFechaHoy() {
    var fechaHoy = new Date();
    var dia = String(fechaHoy.getDate()).padStart(2, "0");
    var mes = String(fechaHoy.getMonth() + 1).padStart(2, "0");
    var anio = fechaHoy.getFullYear();
    let FechaSalida = anio + "-" + mes + "-" + dia;
    return FechaSalida
}

function consultaServidor() {
    let GrupoCreacion = $("#txtIdDatosTrabajo").val()

    tablepedido = $('#tabla_integrador').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarEnviarSapConsumo',
            type: 'POST',
            dataSrc: '',
            data: {
                'GrupoCreacion': GrupoCreacion,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
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
                    return full.CuentaConsumo
                },
            },
            {
                data: null,
                targets: 19,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario
                },
            },  


        ],
        "bDestroy": true
    }).DataTable();
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

function listarEmpleados() {
    $.ajax({
        url: "../Empleado/ObtenerEmpleados",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdResponsable").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdEmpleado + `">` + datos[i].RazonSocial + `</option>`;
                }
                $("#IdResponsable").html(options);
            }
        }
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

function CargarBase() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
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
    let PrimerId = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) {
            if (lista[i].CodeExt == "18") {
                PrimerId = lista[0].IdTipoDocumento;
                contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>";

            }
        }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(PrimerId)
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Codigo + " - " + lista[i].Descripcion.toUpperCase() + "</option>"; }
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
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 6 || lista[i].Documento == 9) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();

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
    $("#cboMoneda").val(1);
}

function CargarImpuestos() {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        let impuestos = JSON.parse(data);
        llenarComboImpuesto(impuestos, "cboImpuesto", "Seleccione")
    });
}
function llenarComboImpuesto(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdIndicadorImpuesto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function ListarBasesxUsuario() {
    $.post("/Usuario/ObtenerBasesxIdUsuario", function (data, status) {
        let datos = JSON.parse(data);
        $("#IdBase").val(datos[0].IdBase).change();
        if (datos.length == 1) {
            console.log(datos);
            if (datos[0].IdPerfil != 1) {
                $("#IdBase").prop("disabled", false);
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdBase").prop("disabled", true);
            }
        }
    });
}


function CargarProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedor", "Seleccione")
    });
}

function llenarComboProveedor(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].NumeroDocumento + " - " + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#idCombo").select2();
}

function CargarCondicionPago() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarCondicionPago(condicionpago, "IdCondicionPago", "Seleccione")
    });
}


function llenarCondicionPago(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCondicionPago + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function CargarGlosaContable() {
    $.ajaxSetup({ async: false });
    $.post("/GlosaContable/ObtenerGlosaContableDivision", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboGlosaContable(tipoRegistros, "cboGlosaContable", "Seleccione");
    });
}


function llenarComboGlosaContable(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGlosaContable + "'>" + lista[i].Descripcion + " / " + lista[i].CuentaContable + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function CargarTipoRegistro() {
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerTipoRegistrosAjax", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboTipoRegistro(tipoRegistros, "IdTipoRegistro", "Seleccione")
    });
}


function llenarComboTipoRegistro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function CargarSemana() {
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanaAjax", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboSemana(tipoRegistros, "IdSemana", "Seleccione")
    });
}


function llenarComboSemana(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSemana + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function changeTipoRegistro() {

    let varIdTipoRegistro = $("#IdTipoRegistro").val();
    let varIdObra = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanaAjax", { 'estado': 1, 'IdTipoRegistro': varIdTipoRegistro, 'IdObra': varIdObra }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboSemana(tipoRegistros, "IdSemana", "Seleccione")

    });

}
let valorTDInicial = 0

function ObtenerDatosxEnviarSap(IdEnviarSap, TablaOriginal) {
    $("#btnEditar").show()
    $("#txtId").val(IdEnviarSap);
    $("#cboGlosaContable").prop("disabled", true)
    $("#btn_agregar_item").prop("disabled", true)
    /*NUEVO*/
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    //CargarAlmacen()
    CargarBase()
    CargarTipoDocumentoOperacion()
    ObtenerCuadrillas()
    CargarSeries();
    CargarSolicitante(1);
    CargarSucursales();
    CargarMoneda();
    CargarImpuestos();
    ListarBasesxUsuario();
    CargarProveedor();
    CargarCondicionPago();
    CargarGlosaContable();
    CargarTipoRegistro();
    CargarGrupoDet();
    CargarTasaDet();
    setTimeout(() => {
        CargarSemana()
    }, 100)
    setTimeout(() => {
        changeTipoRegistro()
    }, 150)

    $("#cboImpuesto").val(2).change();
    //$("#cboSerie").val(7).change();/*TODO: quitando el change
    /*END NUEVO*/




    AbrirModal("modal-form");





    $.post('ObtenerDatosxIdEnviarSap', {
        'IdEnviarSap': IdEnviarSap,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            console.log(movimiento);

            if (movimiento.TablaOriginal == 'OPCH') {
                $("#lblTituloModal").html("Editar Factura Proveedor");
                $("#divSerieDocBase").hide()
                $("#IdTipoDocumentoRef").prop("disabled", false)

            } else if (movimiento.TablaOriginal == 'ORPC') {
                $("#lblTituloModal").html("Editar Nota de Credito");
                $("#divSerieDocBase").show()
                $("#txtserieDocBase").val(movimiento.SerieDocBaseORPC)
                $("#txtserieDocBase").prop("disabled", true)
                $("#IdTipoDocumentoRef").prop("disabled", true)
            } else {
                $("#lblTituloModal").html("Editar ");
            }




            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#txtSerieSAP").val(movimiento.SerieSAP);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios), DecimalesPrecios))
            $("#txtImpuesto").val(formatNumberDecimales(movimiento.Impuesto, 2))
            if (movimiento.Impuesto == 0) {
                $("#txtImpuesto").val((0).toFixed(2))
            }
            $("#txtRedondeo").val(formatNumber(movimiento.Redondeo.toFixed(DecimalesPrecios), DecimalesPrecios))



            $("#txtTotal").val(formatNumberDecimales(movimiento.Total, DecimalesPrecios))


            $("#cboCentroCosto").val(movimiento.IdCentroCosto)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            valorTDInicial = movimiento.IdTipoDocumentoRef
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef.toUpperCase())

            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#IdProveedor").val(movimiento.IdProveedor).change();



            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);
            $("#txtNumeracion").val(movimiento.Correlativo);
            $("#txtTipoCambio").val(formatNumberDecimales(movimiento.TipoCambio, 2))
            if (movimiento.NombUsuarioEdicion == "") {
                $("#NombUsuarioEdicion").html("-")
            } else {
                $("#NombUsuarioEdicion").html(movimiento.NombUsuarioEdicion)
            }

            if (movimiento.FechaEdicion == '1990-01-01T00:00:00') {
                $("#EditedAt").html("-")
            } else {
                $("#EditedAt").html(movimiento.FechaEdicion.replace("T", " "))
            }
            if (movimiento.IdDocExtorno == 1) {
                $("#btnEditar").hide()
                $("#btnExtornar").hide()
            } else {
                $("#btnEditar").show()
                $("#btnExtornar").show()
            }
            $("#IdCondicionPago").val(movimiento.idCondicionPago)
            $("#IdTipoRegistro").val(movimiento.IdTipoRegistro)

            esRendicion()
            setTimeout(() => {
                $("#IdGiro").val(movimiento.IdSemana)
                $("#IdSemana").val(movimiento.IdSemana)

            }, 200)
            $("#cboGlosaContable").val(movimiento.IdGlosaContable)
            $("#IdCuadrilla").val("")
            $("#IdResponsable").val("")
            $("#cboGlosaContable").val(movimiento.IdGlosaContable)
            $("#txtFechaDocumento").val((movimiento.FechaDocumento).split("T")[0])
            $("#txtComentarios").html(movimiento.Comentario)
            $("#txtFechaContabilizacion").val((movimiento.FechaContabilizacion).split("T")[0])
            $("#txtOrigen").val(movimiento.TablaOrigen)
            changeTipoDocumento()

            $("#consumomM3").val(movimiento.ConsumoM3)
            $("#consumomHW").val(movimiento.ConsumoHW)
            $("#TasaDet").val(movimiento.TasaDetraccion)
            $("#GrupoDet").val(movimiento.GrupoDetraccion)
            $("#TipoIngreso").val(movimiento.Inventario)
            CargarCondPagoDet();
            //agrega detalle
            let tr = '';

            let Detalle = movimiento.detalles;
            $("#total_items").html(Detalle.length)
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {
                AgregarLineaDetalle(i, Detalle[i]);
                $("#cboIndicadorImpuestoDetalle" + i).val(Detalle[i].IdIndicadorImpuesto);
            }

            $("#CondicionPagoDet").val(movimiento.CondicionPagoDet)

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
                    <td><button type=""button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)">-</button></td>
                </tr>`;
            }
            $("#tabla_files").find('tbody').append(trAnexo);

            //let DetalleAnexo = solicitudes[0].DetallesAnexo;
            //for (var i = 0; i < DetalleAnexo.length; i++) {
            //    AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            //}



        }

    });
    disabledmodal(true);
    $("#cboTipoDocumentoOperacion").prop("disabled", true)
    $("#IdTipoDocumentoRef").prop("disabled", false)
    $("#SerieNumeroRef").prop("disabled", false)
    $("#IdCuadrilla").prop("disabled", false)
    $("#IdResponsable").prop("disabled", false)
    $("#IdCondicionPago").prop("disabled", false)
    $("#IdTipoRegistro").prop("disabled", true)
    $("#IdSemana").prop("disabled", true)
    $("#IdGiro").prop("disabled", true)
    $("#txtComentarios").prop("disabled", false)
    $("#txtFechaDocumento").prop("disabled", false)
    $("#txtFechaContabilizacion").prop("disabled", false)
    $("#cboGlosaContable").prop("disabled", false)
    $("#Telefono").prop("disabled", true)
    redondear();
    changeTipoDocumento()
}

function EliminarAnexo(Id, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarAnexoEnviarSap", { 'IdEnviarSapAnexos': Id }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });

}
let contadorAnexo = 0;
function AgregarLineaAnexo(Nombre) {
    contadorAnexo++
    let tr = '';
    tr += `<tr id="filaAnexo` + contadorAnexo + `">
            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control txtNombreAnexo" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/Anexos/`+ Nombre + `" target="_blank" >Descargar</a>
            </td>
            <td><button type="button" class="btn  btn-danger btn-xs borrar fa fa-trash" onclick="EliminarAnexoEnMemoria(`+ contadorAnexo + `)"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}
function EliminarAnexoEnMemoria(contAnexo) {
    $("#filaAnexo" + contAnexo).remove();
}
function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#cboAlmacen").prop('disabled', valorbolean);
    $("#cboCentroCosto").prop('disabled', valorbolean);
    $("#cboMoneda").prop('disabled', valorbolean);
    $("#cboSerie").prop('disabled', valorbolean);
    $("#txtFechaDocumento").prop('disabled', valorbolean);
    $("#txtFechaContabilizacion").prop('disabled', valorbolean);
    $("#cboTipoDocumentoOperacion").prop('disabled', valorbolean);
    $("#IdTipoDocumentoRef").prop('disabled', valorbolean);
    $("#SerieNumeroRef").prop('disabled', valorbolean);
    $("#IdCuadrilla").prop('disabled', valorbolean);
    $("#IdResponsable").prop('disabled', valorbolean);
    $("#txtComentarios").prop('disabled', valorbolean)
    $("#btn_agregaritem").prop('disabled', valorbolean)
    $("#IdProveedor").prop('disabled', valorbolean)
    $("#Direccion").prop('disabled', valorbolean)
    $("#FechaEntrega").prop('disabled', valorbolean)

    $("#IdCondicionPago").prop('disabled', valorbolean)
    $("#IdSemana").prop('disabled', valorbolean)
    $("#IdTipoRegistro").prop('disabled', valorbolean)
    $("#txtRedondeo").prop('disabled', valorbolean)

    if (valorbolean) {
        $("#btnGrabar").hide()
        $("#div_copiarde").hide()



        $("#btnNuevo").show();

    } else {
        $("#btnGrabar").show();
        $("#div_copiarde").show()
        $("#btnNuevo").hide();
    }
}

function validarseriescontable() {
    let IdSerie = $("#cboSerie").val();
    let IdDocumento = 1;
    let Fecha = $("#txtFechaContabilizacion").val();
    let Orden = 1;
    let datosrespuesta;
    let estado = 0;
    datosrespuesta = ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden);

    if (datosrespuesta.FechaRelacion.length == 0) {
        swal("Informacion!", "No  se encuentra Fecha de Contabilizacion Activa");
        return true;
    }

    if (datosrespuesta.FechaRelacion.length > 0) {
        for (var i = 0; i < datosrespuesta.FechaRelacion.length; i++) {
            console.log(datosrespuesta.FechaRelacion[i]);
            if (datosrespuesta.FechaRelacion[i].StatusPeriodo == 1) {
                estado = 1;
            }
        }

    }

    if (estado == 0) {
        swal("Informacion!", "No se encuentra Fecha de contabilizacion,verificar periodo contable");
        return true;
    }

}
function ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden) {
    let respustavalidacion;
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerDatosSerieValidacion", { IdSerie, IdDocumento, Fecha, Orden }, function (data, status) {
        if (validadJson(data)) {
            respustavalidacion = JSON.parse(data);
        } else {
            respustavalidacion
        }
    });
    return respustavalidacion;
}
function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function CargarSucursales() {
    $.ajaxSetup({ async: false });
    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
        let sucursales = JSON.parse(data);
        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    });
}
function llenarComboSucursal(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSucursal + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {

        if (validadJson(data)) {
            let obra = JSON.parse(data);
            llenarComboObra(obra, "IdObra", "Seleccione")
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
    $("#IdObra").prop("selectedIndex", 1)
}
function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}
function ObtenerAlmacenxIdObra() {
    let IdObra = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        if (validadJson(data)) {
            let almacen = JSON.parse(data);
            llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
            llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
        } else {
            $("#cboAlmacen").html('<option value="0">SELECCIONE</option>')
            $("#cboAlmacenItem").html('<option value="0">SELECCIONE</option>')
        }
    });
    $("#cboAlmacen").prop("selectedIndex", 1)
    $("#cboAlmacenItem").prop("selectedIndex", 1)
    ObtenerCuadrillasxIdObra(IdObra)

}
function ObtenerCuadrillasxIdObra(IdObra) {
    IdObra = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        console.log("OBRA ES : " + IdObra)
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrilla(cuadrilla, "IdCuadrilla", "Seleccione")
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
function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
    $("#cboClaseArticulo").prop("disabled", false)
    $("#IdTipoProducto").prop("disabled", false)
    $("#btn_agregar_item").prop("disabled", false)
    $("#cboClaseArticulo").val(0)
    $("#IdTipoProducto").val(0)
    arrayCboCuadrillaTabla = []
    arrayCboResponsableTabla = []

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
    $("#txtComentarios").html('');
    $("#txtImpuesto").val('');
    $("#txtTotal").val('');
    $("#txtEstado").val(1);
    $("#txtOrigen").val("");
    $("#txtOrigenId").val("");
    limitador = 0;
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
let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;

function redondear() {
    let CantidadRedondeo = +($("#txtRedondeo").val().replace(/,/g, ""))
    let TotalSinImp = +($("#txtTotalAntesDescuento").val().replace(/,/g, ""))
    let Imp = +($("#txtImpuesto").val().replace(/,/g, ""))
    let TotalSinR = TotalSinImp + Imp
    let NuevoTotal = TotalSinR + CantidadRedondeo
    $("#txtTotal").val(formatNumberDecimales(NuevoTotal, 2))


}
function esRendicion() {
    if ($("#IdTipoRegistro").val() != 4) {
        $("#DivGiro").hide()
        $("#DivSemana").show()
        if ($("#IdTipoRegistro").val() == 5) {
            $("#IdCondicionPago").val(1)
        }
    } else {
        $("#DivGiro").show()
        $("#DivSemana").hide()
        CargarGiros()
    }
}
function ResetRegistroSemana() {
    $("#IdTipoRegistro").prop("selectedIndex", 0)
    $("#IdSemana").prop("selectedIndex", 0)
}
function ObtenerProveedorxId() {
    //console.log(varIdUsuario);
    let IdProveedor = $("#IdProveedor").val();
    if (IdProveedor == 0) {
        IdProveedor = 5
    }
    $.post('/Proveedor/ObtenerDatosxID', {
        'IdProveedor': IdProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {

            try {
                let proveedores = JSON.parse(data);

                $("#Direccion").val(proveedores[0].DireccionFiscal);
                $("#Telefono").val(proveedores[0].Telefono);
                $("#IdCondicionPago").val(proveedores[0].CondicionPago);
            } catch (e) {
                console.log("1")
            }


        }

    });
}


function AgregarLineaDetalle(contador, detalle) {

    let tr = '';
    let UnidadMedida;
    let Almacen;
    let IdUnidadMedida = detalle.IdUnidadMedida
    let IdDefinicionGrupoUnidad = detalle.IdDefinicionGrupoUnidad
    let IdAlmacen = detalle.IdAlmacen
    let IndicadorImpuesto;
    console.log(detalle);



    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ListarDefinicionGrupoxIdDefinicionSelect", { 'IdDefinicionGrupo': detalle.IdDefinicionGrupoUnidad }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });


    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    tr = `<tr>
        <td>`+ detalle.CodigoArticulo + `</td>
        <td>`+ detalle.CodigoArticulo + `</td>
        <td style="display:none">
          <input class="form-control" type="text"  id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" value="` + detalle.IdArticulo + `" disabled/>
        </td>
        <td>
          <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo + `" disabled/>
        </td>
        <td>
             <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdDefinicionGrupo == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `" >` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `"selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" disabled value="`+ formatNumber(detalle.Cantidad.toFixed(DecimalesCantidades)) + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)">
        </td>
        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="`+ formatNumberDecimales(detalle.PrecioUnidadBase, 2) + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>



       <td input>
                <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" disabled>` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
        </td>
            <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador + `)" id="cboCuadrillaTablaId` + contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador + `"></select></td>
            <td style="display:none"><input style="200px" class="form-control txtIdFacturaDetalle" value="`+ detalle.IdEnviarSapDetalle + `" ></input></td>



        <td>
            <input class="form-control changeTotal" type="text" style="width:100px" value="`+ formatNumberDecimales(detalle.total_item, 2) + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
        </td>
        <td  style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" disabled>`;
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
            <input class="form-control" type="text" style="width:100px" value="" disabled>
        </td>
       
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);
    console.log("CONTADOR :" + contador)
    ObtenerCuadrillasTabla(contador)
    ObtenerEmpleadosxIdCuadrillaTabla(contador)
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
    $("#cboCuadrillaTablaId" + contador).val(detalle.IdCuadrilla).change()
    $("#cboResponsableTablaId" + contador).val(detalle.IdResponsable).change()
    NumeracionDinamica();
}
function ObtenerCuadrillasTabla(contador) {
    console.log("CONTADOR EN OBTENER CUADRILLA :" + contador)
    let IdObra = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrillaTabla(cuadrilla, "cboCuadrillaTablaId" + contador, "Seleccione", contador)
    });
}
function llenarComboCuadrillaTabla(lista, idCombo, primerItem, contador) {
    console.log("CONTADOR EN HTML OBTENER CUADRILLA :" + contador)
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Codigo + " - " + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    //var cbo = document.getElementById(idCombo);
    //if (cbo != null) cbo.innerHTML = contenido;
    $("#cboCuadrillaTablaId" + contador).html(contenido)
    $("#cboCuadrillaTablaId" + contador).val($("#IdCuadrilla").val())
}

function ObtenerEmpleadosxIdCuadrillaTabla(contador) {

    let IdCuadrilla = $("#IdCuadrilla").val();
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTabla(empleados, "cboResponsableTablaId" + contador, "Seleccione", contador)
    });
}

function llenarComboEmpleadosTabla(lista, idCombo, primerItem, contador) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    console.log("Empleados: " + lista.length)
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    $("#cboResponsableTablaId" + contador).html(contenido)
    $("#cboResponsableTablaId" + contador).val($("#IdResponsable").val())


}

function SetCuadrillaTabla() {
    let SetCuadrilla = $("#IdCuadrilla").val()
    $(".cboCuadrillaTabla").val(SetCuadrilla).change()
}
function SeleccionarEmpleadosTabla(contador) {
    let valorActual = $("#cboResponsableTablaId" + contador).val()
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTablaFila(empleados, "cboResponsableTablaId" + contador, "Seleccione", contador, valorActual)
    });

}
function llenarComboEmpleadosTablaFila(lista, idCombo, primerItem, contador, valorActual) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    console.log("Empleados: " + lista.length)
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    $("#cboResponsableTablaId" + contador).html(contenido)


    ObtenerCapatazTablaFila(contador, valorActual)
}
function ObtenerCapatazTablaFila(contador, valorActual) {
    let IdCuadrillaFila = $("#cboCuadrillaTablaId" + contador).val();
    /* setTimeout(() => {*/
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrillaFila }, function (data, status) {
        try {
            let capataz = JSON.parse(data);
            $("#cboResponsableTablaId" + contador).val(capataz[0].IdEmpleado).change();
        } catch (e) {
            $("#cboResponsableTablaId" + contador).val(valorActual).change();
        }
    })
    /*}, 1000);*/

}
function SetearEmpleadosEnTabla() {
    $(".cboResponsableTabla").val($("#IdResponsable").val()).change();

}
function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
    });
}

function CargarGiros() {
    let obraGiro = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/GestionGiro/ObtenerGiroAprobado", { 'IdObra': obraGiro }, function (data, status) {
        let base = JSON.parse(data);
        llenarComboGiros(base, "IdGiro", "seleccione")

    });

}


function llenarComboGiros(lista, idCombo, primerItem) {
    console.log(lista)
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGiro + "'>" + lista[i].Serie.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function eliminarMarcoTrabajo() {

    alertify.confirm('Confirmar', '¿Desea eliminar este Marco de Trabajo?', function () {

        $.post("EliminarMarcoTrabajo", { 'GrupoCreacion': $("#txtIdDatosTrabajo").val() }, function (data, status) {
            if (data > 0) {
                window.location.href = "/IntegradorV1/Listado?l=1"
            } else {
                Swal.fire("Error!", "Ocurrió un Error", "error");
            }


        });
    }, function () { });

}

function Editar() {
    let varIdEnviarSap = $("#txtId").val();
    let varFechaDocumento = $("#txtFechaDocumento").val();
    let varFechaContabilizacion = $("#txtFechaContabilizacion").val();
    let varTipoDocumento = $("#IdTipoDocumentoRef").val();
    let varSerieNum = $("#SerieNumeroRef").val();
    let varFormaPago = $("#IdCondicionPago").val();
    let varGlosa = $("#cboGlosaContable").val();
    let varComentario = $("#txtComentarios").val()

    let varConsumoM3 = $("#consumomM3").val()
    let varConsumoHW = $("#consumomHW").val()
    let varTasaDet = $("#TasaDet").val()
    let varGrupoDet = $("#GrupoDet").val()
    let varSerieSAP = $("#txtSerieSAP").val()
    let varCondicionPagoDet = $("#CondicionPagoDet").val()

    let arrayCboCuadrillaTabla = new Array();
    $(".cboCuadrillaTabla").each(function (indice, elemento) {
        arrayCboCuadrillaTabla.push($(elemento).val());
    });

    let arrayCboResponsableTabla = new Array();
    $(".cboResponsableTabla").each(function (indice, elemento) {
        arrayCboResponsableTabla.push($(elemento).val());
    });
    let arraytxtIdFacturaDetalle = new Array();
    $(".txtIdFacturaDetalle").each(function (indice, elemento) {
        arraytxtIdFacturaDetalle.push($(elemento).val());
    });

    let arrayNombreArchivo = new Array();
    $(".txtNombreAnexo").each(function (indice, elemento) {
        arrayNombreArchivo.push($(elemento).val());
    });

    console.log(arrayNombreArchivo.length)

    if (arrayNombreArchivo.length > 0) {
        for (var i = 0; i < arrayNombreArchivo.length; i++) {
            $.post('UpdateAnexosEnviarSAP', {
                'IdEnviarSap': varIdEnviarSap,
                'NombreArchivo': arrayNombreArchivo[i],

            }, function (data, status) {
                swal("Exito!", "Proceso Realizado Correctamente", "success")
                CerrarModal()
                consultaServidor();



            });
        }
    }


    $.post('UpdateEnviarSap', {
        'IdEnviarSap': varIdEnviarSap,
        'FechaContabilizacion': varFechaContabilizacion,
        'FechaDocumento': varFechaDocumento,
        'IdTipoDocumentoRef': varTipoDocumento,
        'NumSerieTipoDocumentoRef': varSerieNum,
        'IdCondicionPago': varFormaPago,
        'IdGlosaContable': varGlosa,
        'Comentario': varComentario,
        'ConsumoM3': varConsumoM3,
        'ConsumoHW': varConsumoHW,
        'TasaDetraccion': varTasaDet,
        'GrupoDetraccion': varGrupoDet,
        'SerieSAP': varSerieSAP,
        'CondicionPagoDet': varCondicionPagoDet

    }, function (data, status) {

        if (data != 0) {

            for (var i = 0; i < arraytxtIdFacturaDetalle.length; i++) {
                $.post('UpdateCuadrillasEnviarSAP', {
                    'IdEnviarSapDetalle': arraytxtIdFacturaDetalle[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],

                }, function (data, status) {
                    if (data != 0) {
                        swal("Exito!", "Proceso Realizado Correctamente", "success")
                        CerrarModal()
                        consultaServidor();

                    } else {
                        swal("Error!", "Ocurrio un Error")
                        CerrarModal()
                    }

                });



            }


        } else {
            swal("Error!", "Ocurrio un Error")

        }

    });


}


function openContenido(evt, Name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}
function changeTipoDocumento() {

    if (($("#IdTipoDocumentoRef").val() == 11 || $("#IdTipoDocumentoRef").val() == 16) && $("#TipoIngreso").val() == 'true') {
        swal("Error!", "No se puede Cambiar a este tipo de documento, ya que la factura es de tipo Articulo", "error")
        $("#IdTipoDocumentoRef").val(valorTDInicial)
        return;
    }
    let totalValor = ($("#txtTotal").val()).replace(/,/g, "")

    let TDocSelect = $("#IdTipoDocumentoRef").val()
    if (TDocSelect == 11) {
        $("#divDetraccion").hide()
    } else {
        if ($("#TipoIngreso").val() == 'false' && totalValor > 700) {
            $("#divDetraccion").show()
        } else {
            $("#divDetraccion").hide()
        }
    }
    if (TDocSelect == 16) {
        $("#divServPub").show()
        $("#divDetraccion").hide()
    } else {
        $("#divServPub").hide()
    }
}

function CargarTasaDet() {
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerTasaDetSAP", function (data, status) {
        let base = JSON.parse(data);
        llenarComboTasaDet(base, "TasaDet", "seleccione")

    });

}


function llenarComboTasaDet(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Code + "'>" + lista[i].Name.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboBaseFiltro").prop("selectedIndex", 1);
}

function CargarGrupoDet() {
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerGrupoDetSAP", function (data, status) {
        let base = JSON.parse(data);
        llenarComboGrupoDet(base, "GrupoDet", "seleccione")

    });

}


function llenarComboGrupoDet(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Code + "'>" + lista[i].Name.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboBaseFiltro").prop("selectedIndex", 1);
}

function CargarCondPagoDet() {
    let varGrupoDet = $("#TasaDet option:selected").text()
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerCondPagoDetSAP", { 'GrupoDet': varGrupoDet }, function (data, status) {
        try {
            let base = JSON.parse(data);
            llenarComboCondPagoDet(base, "CondicionPagoDet", "seleccione")

        } catch (e) {
            $("#CondicionPagoDet").html("<option value=0>Selecione</option>")
        }

    });

}


function llenarComboCondPagoDet(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].GroupNum + "'>" + lista[i].PymntGroup.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboBaseFiltro").prop("selectedIndex", 1);
}

function CargarSerie() {
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerSerieSAP", { 'ObjCode': 30 }, function (data, status) {
        try {
            let base = JSON.parse(data);
            llenarSerie(base, "cboSerie", "seleccione")

        } catch (e) {
            $("#cboSerie").html("<option value=0>Selecione</option>")
        }

    });

}
function llenarSerie(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Series + "'>" + lista[i].SeriesName.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboSerie").prop("selectedIndex", 1);
}


function CargarSerieNotaCredito() {
    $.ajaxSetup({ async: false });
    $.post("ObtenerSerieSAP", { 'ObjCode': 19 }, function (data, status) {
        try {
            let base = JSON.parse(data);
            llenarComboSerieNotaCredito(base, "cboSerieNotaCredito", "seleccione")

        } catch (e) {
            $("#cboSerieNotaCredito").html("<option value=0>Selecione</option>")
        }

    });

}
function llenarComboSerieNotaCredito(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Series + "'>" + lista[i].SeriesName.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboSerieNotaCredito").prop("selectedIndex", 1);
}

function MontoPorCuentas() {
    let GrupoCreacion = $("#txtIdDatosTrabajo").val()
    console.log(GrupoCreacion)
   
    $.post("ObtenerMontoDeCuentas", {
        'GrupoCreacion': GrupoCreacion
    }, function (data, status) {
        $("#tbody_resumenCuentas").empty();
        let datos = JSON.parse(data)
        let tr = "";
        for (var i = 0; i < datos.length - 1; i++) {

            let MontoDebido = 0;
            let MontoCredito = 0;

            if (datos[i].Monto <= 0) {
                MontoDebido = datos[i].Monto * -1
                MontoDebido = formatNumberDecimales(MontoDebido, 2)
                if (MontoDebido == '-') MontoDebido = 0
            } else {
                MontoCredito = datos[i].Monto;
                MontoCredito = formatNumberDecimales(MontoCredito, 2)
                if (MontoCredito == '-') MontoCredito = 0
            }

            tr += '<tr>' +
                '<td>Consumo</td>' +
                '<td><input type="text" class="txtNumeroCuenta form-control" value="' + datos[i].NumeroCuenta.toUpperCase() + '" disabled/></td>' +
                '<td><input type="text" class="txtMontoCuentaDebito form-control" value="' + MontoDebido + '" disabled /></td>' +
                '<td><input type="text" class="txtMontoCuentaCredito form-control" value="' + MontoCredito + '" disabled /></td>' +
                '</tr>';
        }
        $("#tbody_resumenCuentas").html(tr);

        cargarCuentaInv(datos[datos.length - 1].NumeroCuenta)
        //$("#CuentaInv").val(datos[datos.length-1].NumeroCuenta)
        //$("#MontoCuentaInv").val(formatNumberDecimales(datos[datos.length-1].Monto,2))
    });
}

function enviarSAP() {
    AbrirModal('modal-form')
    MontoPorCuentas()

}

function ProcesarIntegracion() {

    if ($("#cboSerie").val() == 0 || $("#cboSerie").val() == null || $("#cboSerie").val() == undefined) {
        Swal.fire("Advertencia", "Seleccione un Serie", "info")
        return
    }



    Swal.fire({
        title: "Enviando a SAP...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });


    let arrayCuenta = new Array();
    $(".txtNumeroCuenta").each(function (indice, elemento) {
        arrayCuenta.push($(elemento).val());
    });

    let arrayMontoDebito = new Array();
    $(".txtMontoCuentaDebito").each(function (indice, elemento) {
        arrayMontoDebito.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayMontoCredito = new Array();
    $(".txtMontoCuentaCredito").each(function (indice, elemento) {
        arrayMontoCredito.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayCuentaInv = new Array();;
    $(".CuentaInv").each(function (indice, elemento) {
        arrayCuentaInv.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayDebitoInv = new Array();;
    $(".TotalDebitoInv").each(function (indice, elemento) {
        arrayDebitoInv.push(($(elemento).val()).replace(/,/g, ""));
    });

    let arrayCreditoInv = new Array();;
    $(".TotalCreditoInv").each(function (indice, elemento) {
        arrayCreditoInv.push(($(elemento).val()).replace(/,/g, ""));
    });

    let ArrayConsumo = [];

    for (var i = 0; i < arrayCuenta.length; i++) {
        ArrayConsumo.push({
            'NumeroCuenta': arrayCuenta[i],
            'MontoDebito': arrayMontoDebito[i],
            'MontoCredito': arrayMontoCredito[i]
        })
    }

    for (var i = 0; i < arrayCuentaInv.length; i++) {
        ArrayConsumo.push({
            'NumeroCuenta': arrayCuentaInv[i],
            'MontoDebito': (arrayDebitoInv[i]).replace(/,/g, ""),
            'MontoCredito': (arrayCreditoInv[i]).replace(/,/g, "")
        })
    }
    setTimeout(() => {
        $.post("ProcesarIntegracion", {
            'Cuentas': ArrayConsumo,
            'FechaContabilizacion': $("#txtFechaContabilizacion").val(),
            'GrupoCreacion': $("#txtIdDatosTrabajo").val(),
            'Serie': $("#cboSerie").val()
        }, function (data, status) {
            closePopup()
            ValidarEnvioSap()
            Swal.fire("Resultados:", data, "info")
        });
    },200)


}

function ValidarEnvioSap() {
    $.post("ValidarEnvioSap", {
        'GrupoCreacion': $("#txtIdDatosTrabajo").val()
    }, function (data, status) {
        if (data > 0) {
            $("#btnEnviarSap").hide()
        } else {
            $("#btnEnviarSap").show()
        }
    });
}

function cargarCuentaInv(NumeroCuenta) {
    let MontoTotalDebito = 0;
    $(".txtMontoCuentaDebito").each(function (indice, elemento) {
        MontoTotalDebito += +($(elemento).val()).replace(/,/g, "");
    });

    let MontoTotalCredito = 0;
    $(".txtMontoCuentaCredito").each(function (indice, elemento) {
        MontoTotalCredito += +($(elemento).val()).replace(/,/g, "");
    });

    let newTr = "";


    if (MontoTotalDebito > 0) {
        newTr += '<tr>' +
            '<td>Inventario</td>' +
            '<td><input type="text" class="form-control CuentaInv"  value="' + NumeroCuenta + '" disabled/></td>' +
            '<td><input type="text" class="form-control TotalDebitoInv"  value="' + 0 + '" disabled /></td>' +
            '<td><input type="text" class="form-control TotalCreditoInv"  value="' + MontoTotalDebito + '" disabled /></td>' +
            '</tr>';
    }

    if (MontoTotalCredito > 0) {
        newTr += '<tr>' +
            '<td>Inventario</td>' +
            '<td><input type="text" class="form-control CuentaInv"  value="' + NumeroCuenta + '" disabled/></td>' +
            '<td><input type="text" class="form-control TotalDebitoInv"  value="' + MontoTotalCredito + '" disabled /></td>' +
            '<td><input type="text" class="form-control TotalCreditoInv"  value="' + 0 + '" disabled /></td>' +
            '</tr>';

    }

    $("#tbody_resumenCuentas").append(newTr)

}

