let table = '';
let tablepedido = '';
let tableitemsaprobados;
let tableProductosAprobadosRQ;
let ultimaProductosAprobados = null;
let ultimaFila=null;
let colorOriginal = null;
function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCondicionPago + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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
    $("#cboMoneda").val(1).change();
}


function listarPedidoDt() {

    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerPedidoDT',
            type: 'POST',
            data: {

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

                    return `<button class="btn btn-primary editar  juntos fa fa-eye  btn-xs" style="width:30px;height:30px"  onclick="ObtenerDatosxID(` + full.IdPedido + `)"></button>
                            <button class="btn btn-danger  reporte  juntos fa fa-file-pdf-o btn-xs" onclick="ReporteOrdenComrpa(` + full.IdPedido + `,`+full.Conformidad+ `)"></button>`
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
                    return full.FechaDocumento.split("T")[0]
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
          
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombMoneda
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            
           
            {
                targets:6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.total_venta,2)
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
                        } else{
                            return "Rechazado"
                        }
                    }
                   
                },
            }

        ],
        "bDestroy": true
    }).DataTable();

}

function ReporteCuadroComparativo(id) {
    $.ajaxSetup({ async: false });
    $.post("GenerarReporte", { 'NombreReporte': 'CuadroComparativo', 'Formato': 'PDF', 'Id': id }, function (data, status) {
        let datos;
        if (validadJson(data)) {
            let datobase64;
            datobase64 = "data:application/octet-stream;base64,"
            datos = JSON.parse(data);
            datobase64 += datos.Base64ArchivoPDF;
            $("#reporteRPT").attr("download", 'Reporte.' + "pdf");
            $("#reporteRPT").attr("href", datobase64);
            $("#reporteRPT")[0].click();

        } else {
            respustavalidacion
        }
    });
}

function ReporteOrdenComrpa(id, conformidad) {
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
            } else {
                respustavalidacion
            }
        });
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

function CambiarAlmacenItem() {
    $("#cboAlmacenItem").val($("#IdAlmacen").val());
}



function ObtenerAlmacenxIdObra() {
    let IdObra = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        if (validadJson(data)) {
            let almacen = JSON.parse(data);
            llenarComboAlmacen(almacen, "IdAlmacen", "Seleccione")
            llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")



        } else {
            $("#IdAlmacen").html('<option value="0">SELECCIONE</option>')
            $("#cboAlmacenItem").html('<option value="0">SELECCIONE</option>')

        }
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

function CargarPedido() {
    $.ajaxSetup({ async: false });
    $.post("/TipoPedido/ObtenerTipoPedido", { 'estado': 1 }, function (data, status) {
        let tipopedido = JSON.parse(data);
        llenarComboTipoPedido(tipopedido, "IdTipoPedido", "Seleccione")
    });
}

function llenarComboTipoPedido(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoPedido + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
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


function ListarBasesxUsuario() {
    $.post("/Usuario/ObtenerBasesxIdUsuario", function (data, status) {
        let datos = JSON.parse(data);
        $("#IdBase").val(datos[0].IdBase).change();
        if (datos.length == 1) {
            //console.log(datos);
            if (datos[0].IdPerfil != 1) {
                $("#IdBase").prop("disabled", false);
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdBase").prop("disabled", true);
            }
        }
    });
}

function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();
    if (IdBase!=0) {
        $.ajaxSetup({ async: false });
        $.post("/Obra/ObtenerObraxIdBase", { 'IdBase': IdBase }, function (data, status) {
            let obra = JSON.parse(data);
            llenarComboObra(obra, "IdObra", "Seleccione")
        });
    }
    
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
}


window.onload = function () {
    $("#divObraArticulo").hide();
    $('#modalasignarproveedor').on('hidden.bs.modal', function (e) {
        $('#ModalProductosAprobados').css('overflow', 'auto');
    });


    listarPedidoDt();

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
                //console.log(data);
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
};

function AgregarLineaAnexo(Nombre) {

    let tr = '';
    tr += `<tr>
            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/Anexos/`+ Nombre + `" target="_blank" >Descargar</a>
            </td>
            <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash" onclick="quitarAnexo()"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}
function quitarAnexo() {
    console.log("hola")
    $("#tabla_files").find('tbody').prepend(tr);
}

function ModalNuevo() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    $("#txtFechaDocumento").val(today)
    $("#txtFechaContabilizacion").val(today)
    disabledmodal(false);
    $("#div_tabla_listadorequerimiento_items").hide();
    $("#div_tabla_listado_items").show();
    $("#btnVerItemModal2").hide();
    $("#btnVerItemModal1").show();
    CargarPedido();
    CargarSeries();
    CargarProveedor();
    CargarMoneda();
    CargarCondicionPago();
    $("#lblTituloModal").html("Pedido");
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }
    
    AbrirModal("modal-form");
    
    return true;
}

function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $("#txtTipoCambio").prop('disabled', true);
    $.magnificPopup.close();
    limpiarDatos();
}


function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "IdSerie", "Seleccione")
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
        if (lista[i].Documento ==4) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice=i }
            else { }
        }
        
    }

    
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    if (idCombo == "IdProveedorAsignar") {
        $("#" + idCombo).select2({
            dropdownParent: $("#modalasignarproveedor"),
        })
    } else {
        $("#" + idCombo).select2()
    }
    
}

function ObtenerProveedorxId() {
    //console.log(varIdUsuario);
    let IdProveedor = $("#IdProveedor").val();
    $.post('/Proveedor/ObtenerDatosxID', {
        'IdProveedor': IdProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let proveedores = JSON.parse(data);
            $("#Direccion").val(proveedores[0].DireccionFiscal);
            $("#Telefono").val(proveedores[0].Telefono);

        }

    });
}

function OpenModalItem() {
    $("#ModalItem").modal();
    CargarGrupoUnidadMedida();
    CargarUnidadMedidaItem();
    CargarIndicadorImpuesto();
    //Cuando se abre agregar Item
    //let ClaseArticulo = $("#cboClaseArticulo").val();
    //let Moneda = $("#cboMoneda").val();
    //if (ClaseArticulo == 0) {
    //    swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
    //} else if (Moneda == 0) {
    //    swal("Informacion!", "Debe Seleccionar Moneda!");
    //}
    //else {

    //    $("#cboAlmacenItem").val($("#cboAlmacen").val()); // que salga el almacen por defecto


    //    $("#cboPrioridadItem").val(2);
    //    $("#cboClaseArticulo").prop("disabled", true);
    //    $("#IdTipoProducto").prop("disabled", true);

    //    CargarUnidadMedidaItem();
    //CargarGrupoUnidadMedida();
    /* CargarProyectos();*/
    //CargarCentroCostos();f
    //CargarAlmacen();

}

function BuscarCodigoProducto() {

    let TipoItem = $("#cboClaseArticulo").val();
    let IdAlmacen = $("#cboAlmacenItem").val();
    let IdTipoProducto = $("#IdTipoProducto").val();

    if (IdAlmacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    $("#ModalListadoItem").modal();
    listadoItems();

    //$.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, }, function (data, status) {

    //    if (data == "error") {
    //        swal("Informacion!", "No se encontro Articulo")
    //    } else {
    //        let items = JSON.parse(data);
    //        console.log(items);
    //        let tr = '';

    //        for (var i = 0; i < items.length; i++) {
    //            /*if (items[i].Inventario == TipoItem) {*/
    //            if (items[i].Stock > 0) {
    //                tr += '<tr>' +
    //                    '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
    //                    '<td>' + items[i].Codigo + '</td>' +
    //                    '<td>' + items[i].Descripcion1 + '</td>' +
    //                    '<td>' + items[i].Stock + '</td>' +
    //                    '<td>' + items[i].UnidadMedida + '</td>' +
    //                    '</tr>';
    //            }
    //        }

    //        //} else {
    //        //    if (TipoItem == 2 && items[i].Inventario == false) {
    //        //tr += '<tr>' +
    //        //    '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
    //        //    '<td>' + items[i].Codigo + '</td>' +
    //        //    '<td>' + items[i].Descripcion1 + '</td>' +
    //        //    '<td>' + items[i].Stock + '</td>' +
    //        //    '<td>' + items[i].UnidadMedida + '</td>' +
    //        //    '</tr>';
    //        //    }
    //        //}

    //        //}

    //        $("#tbody_listado_items").html(tr);

    //        tableItems = $("#tabla_listado_items").DataTable({
    //            info: false, "language": {
    //                "paginate": {
    //                    "first": "Primero",
    //                    "last": "Último",
    //                    "next": "Siguiente",
    //                    "previous": "Anterior"
    //                },
    //                "processing": "Procesando...",
    //                "search": "Buscar:",
    //                "lengthMenu": "Mostrar _MENU_ registros"
    //            }
    //        });
    //    }

    //});

}


function listadoItems() {

    table = $('#tabla_listado_items').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: "/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoDT",
            type: 'POST',
            data: {
                'IdTipoProducto': $("#IdTipoProducto").val(),
                'IdAlmacen': $("#cboAlmacenItem").val(),
                'Estado': 1,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="radio" clase="" id="rdSeleccionado` + full.IdArticulo + `"  name="rdSeleccionado"  value = "` + full.IdArticulo + `" >`

                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Codigo.toUpperCase()
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Descripcion1.toUpperCase()
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Stock
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.UnidadMedida
                },
            }



        ],
        "bDestroy": true
    }).DataTable();
}

function VerItemModal(estado) {
    if (estado == 1) {
        $("#div_tabla_listado_items").hide();
        $("#div_tabla_listadorequerimiento_items").show();
        $("#btnVerItemModal2").show();
        $("#btnVerItemModal1").hide();


    } else {
        $("#div_tabla_listadorequerimiento_items").hide();
        $("#div_tabla_listado_items").show();
        $("#btnVerItemModal2").hide();
        $("#btnVerItemModal1").show();

    }

}

function SeleccionarItemListado() {

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();
    $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Articulo")
            listadoItems();
        } else {
            let datos = JSON.parse(data);
            //console.log(datos);
            $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
            $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
            $("#txtCodigoItem").val(datos[0].Codigo);
            $("#txtIdItem").val(datos[0].IdArticulo);
            $("#txtDescripcionItem").val(datos[0].Descripcion1);
      
            $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
            $("#txtStockAlmacenItem").val(datos[0].Stock);
            $("#txtPrecioUnitarioItem").val(datos[0].PrecioPromedio)
            //$("#txtPrecioUnitarioItem").prop("disabled", true);

            listadoItems();
        }
    });
}

function CargarDefinicionxGrupo() {
    //console.log('CargarDefinicionxGrupo');
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        if (validadJson(data)) {
            let definicion = JSON.parse(data);
            llenarComboDefinicionGrupoUnidadItem(definicion, "cboMedidaItem", "Seleccione")
        } else {
            $("#cboMedidaItem").html('<option value="0">SELECCIONE</option>')
        }

    });
}


function CargarUnidadMedidaItem() {
    //console.log('CargarUnidadMedidaItem');
    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        let unidad = JSON.parse(data);
        llenarComboUnidadMedidaItem(unidad, "cboMedidaItem", "Seleccione")
    });
}

function llenarComboUnidadMedidaItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function CargarGrupoUnidadMedida() {
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerGrupoUnidadMedida", { 'estado': 1 }, function (data, status) {
        let grupounidad = JSON.parse(data);
        //console.log(grupounidad);
        llenarComboGrupoUnidadMedidaItem(grupounidad, "cboGrupoUnidadMedida", "Seleccione")
    });
}


function llenarComboGrupoUnidadMedidaItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGrupoUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboDefinicionGrupoUnidadItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDefinicionGrupo + "'>" + lista[i].DescUnidadMedidaAlt + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

let contador = 0;
function AgregarLinea() {

    let IdItem = $("#txtIdItem").val();
    let CodigoItem = $("#txtCodigoItem").val();
    let MedidaItem = $("#cboMedidaItem").val();
    let DescripcionItem = $("#txtDescripcionItem").val();
    let PrecioUnitarioItem = $("#txtPrecioUnitarioItem").val();
    let CantidadItem = $("#txtCantidadItem").val();
    let ProyectoItem = $("#cboProyectoItem").val();
    let CentroCostoItem = $("#cboCentroCostoItem").val();
    let ReferenciaItem = $("#txtReferenciaItem").val();
    let AlmacenItem = $("#cboAlmacenItem").val();
    let PrioridadItem = $("#cboPrioridadItem").val();
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    let IdIndicadorImpuesto = $("#IdIndicadorImpuesto").val();

    //txtReferenciaItem

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;


    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    //valdiaciones

    let ValidartCentroCosto = $("#cboCentroCostoItem").val();
    let ValidartProducto = $("#cboMedidaItem").val();
    let ValidarCantidad = $("#txtCantidadItem").val();
    let ValidarPrecioItem = $("#txtPrecioUnitarioItem").val();

    //console.log(ValidarCantidad);
    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0") {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }

    if (ValidarPrecioItem == "" || ValidarPrecioItem == null || ValidarPrecioItem == "0") {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }
    if (ValidartCentroCosto == 0) {
        swal("Informacion!", "Debe Seleccionar Centro de Costo!");
        return;
    }
    if (ValidartProducto == 0) {
        swal("Informacion!", "Debe Seleccionar Producto!");
        return;
    }
   
    if (CantidadItem <0) {
        swal("Informacion!", "La cantidad del producto debe ser positiva!");
        return;
    }
    if ($("#txtPrecioUnitarioItem").val() <= 0) {
        swal("Informacion!", "El precio debe ser positivo");
        return;
    }

    if ($("#txtCantidadItem").val() <= 0) {
        swal("Informacion!", "La cantidad ser positivo");
        return;
    }



    //validaciones
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("../Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
    });

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    Proyecto = JSON.parse(data);
    //});

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        Moneda = JSON.parse(data);
    });

    contador++;
    let tr = '';

    //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
    //    <option value="0">Seleccione</option>
    //</select>
    tr += `<tr id="trcontador2` + contador +`">
            <td><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
    }
    tr += `</select>
            </td>
           
           
            <td input style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Moneda.length; i++) {
        tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()"></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>`
    tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < LineaNegocio.length; i++) {
    //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
    //}
    tr += `</select>
            </td>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
    //}

    //for (var i = 0; i < Proyecto.length; i++) {
    //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
    //}
    tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="eliminartr2(`+contador+`)"></button></td>
          <tr>`;

    $("#tabla").find('tbody').append(tr);


    let varMoneda = $("#cboMoneda").val();
    let varTipoCambio = $("#txtTipoCambio").val();
    let varimpuesto = $("#cboImpuesto").val();

    if (varMoneda) {
        $(".MonedaDeCabecera").val(varMoneda);
    }
    if (varTipoCambio) {
        $(".TipoCambioDeCabecera").val(varTipoCambio);
    }
    if (varimpuesto) {
        $(".ImpuestoCabecera").val(varimpuesto);
    }



    $("#txtIdArticulo" + contador).val(IdItem);
    $("#txtCodigoArticulo" + contador).val(CodigoItem);
    $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
    $("#cboUnidadMedida" + contador).val(MedidaItem);
    $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();
    $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
    $("#cboProyecto" + contador).val(ProyectoItem);
    $("#cboAlmacen" + contador).val(AlmacenItem);
    $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

    $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);
    CalcularTotalDetalle(contador);
    LimpiarModalItem();
}

function eliminartr2(count) {

    $("#trcontador2" + count).remove();
}

function ValidarDecimalesCantidades(input) {

    let cantidad = $("#" + input).val();
    let separar = cantidad.split(".");

    if (separar.length > 1) {
        if ((separar[1]).length > DecimalesCantidades) {
            $("#" + input).val(Number(cantidad).toFixed(DecimalesCantidades));
        }
    }
}


function CargarIndicadorImpuesto() {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", { 'estado': 1 }, function (data, status) {
        let indicadorimpuesto = JSON.parse(data);
        llenarComboIndicadorImppuesto(indicadorimpuesto, "IdIndicadorImpuesto", "Seleccione")
    });
}


function llenarComboIndicadorImppuesto(lista, idCombo, primerItem) {
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

function quitarFormato(number) {
    try {
        return Number(StringReplace(number, ',', ''));
    } catch (e) {
        number
    }
    
}
function CalcularTotalDetalle(contador) {

    let varIndicadorImppuesto = $("#cboIndicadorImpuestoDetalle" + contador).val();
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");

    //let varCantidadNecesaria = quitarFormato($("#txtCantidadNecesaria" + contador).val());
    //let varPrecioInfo = quitarFormato($("#txtPrecioInfo" + contador).val());




    let varCantidadNecesaria = quitarFormato($("#txtCantidadNecesaria" + contador).val());
    let varPrecioInfo = quitarFormato($("#txtPrecioInfo" + contador).val());



    //console.log(varCantidadNecesaria);

    //if (varCantidadNecesaria.indexOf(",")) {
    //    varCantidadNecesaria = indexOf($("#txtCantidadNecesaria" + contador).val());
    //}

    //if (varPrecioInfo.indexOf(",")) {
    //    varPrecioInfo = indexOf($("#txtPrecioInfo" + contador).val());
    //}

    let subtotal = varCantidadNecesaria * varPrecioInfo;



    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        //console.log("porcentaje=0");
        varTotal = subtotal;
    } else {
        //console.log("no porcentaje=0");
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }
    
    let numero = formatNumberDecimales(varTotal.toString(), 2);
    let numero1 = varTotal.toFixed(2)
    //$("#txtItemTotal" + contador).val(numero).change();
    $("#txtItemTotal" + contador).val(formatNumberDecimales(varTotal,2)).change();



}
function StringReplace(text, oldText, newText, flags) {
    var r = text;
    if (oldText instanceof Array) for (var i = 0; i < oldText.length; i++) r = r.replace(new RegExp(oldText[i], flags || 'g'), newText);
    else r = r.replace(new RegExp(oldText, flags || 'g'), newText);
    return r;
}
function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        let numero3 = $(elemento).val()
        let numero = StringReplace($(elemento).val(), ',', '');
         arrayCantidadNecesaria.push(numero);
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        let numero = StringReplace($(elemento).val(), ',', '');
        arrayPrecioInfo.push(numero);
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected', elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        let numero = StringReplace($(elemento).val(), ',', '');
        arrayTotal.push(Number(numero));
    });


       
    //console.log(arrayCantidadNecesaria);
    //console.log(arrayPrecioInfo);
    //console.log(arrayIndicadorImpuesto);

    let subtotal = 0;
    let impuesto = 0;
    let total = 0;

    for (var i = 0; i < arrayPrecioInfo.length; i++) {
        subtotal += (arrayCantidadNecesaria[i] * arrayPrecioInfo[i]);
        total += arrayTotal[i];
    }

    impuesto = total - subtotal;
    // dando Formato



    $("#txtTotalAntesDescuento").val(formatNumberDecimales(subtotal, 2));
    $("#txtImpuesto").val(formatNumberDecimales(impuesto, 2));
    $("#txtTotal").val(formatNumberDecimales(total, 2));

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
    //limpiar tablas


    $("#tabla_proveedor_aprobados tbody").html('');
    $("#tabla_aprobados_stock tbody").html('');
    $("#tabla_listado_ultimasventas tbody").html('');

 }

function GuardarPedido() {
    //validaciones
    if ($("#IdSerie").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione una Serie',
            'error'
        )
        return;
    }

    if ($("#IdObra").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione una Obra',
            'error'
        )
        return;
    }

    if ($("#IdAlmacen").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione un Almacen',
            'error'
        )
        return;
    }



    if ($("#IdProveedor").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione una Proveedor',
            'error'
        )
        return;
    }

    if ($("#IdTipoPedido").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione Tipo Pedido',
            'error'
        )
        return;
    }

    if ($("#IdCondicionPago").val() == 0) {
        Swal.fire(
            'Error!',
            'Seleccione Condicion de Pago',
            'error'
        )
        return;
    }






    //Cabecera
    let IdAlmacen = $("#IdAlmacen").val();
    let FechaDocumento = $("#txtFechaDocumento").val();
    let FechaContabilizacion = $("#txtFechaContabilizacion").val();
    let IdSerie = $("#cboSerie").val();
    let IdMoneda = $("#cboMoneda").val();
    let IdProveedor = $("#IdProveedor").val();
    let Direccion = $("#Direccion").val();
    let Telefono = $("#Telefono").val();
    let FechaEntrega = $("#FechaEntrega").val();
    let IdTipoPedido = $("#IdTipoPedido").val();
    //END Cabecera

    //detalle
    let arrayIdArticulo = new Array();
    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });
    let arraytxtDescripcionArticulo = new Array();
    $("input[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
        arraytxtDescripcionArticulo.push($(elemento).val());
    });

    let arrayImpuestosItem = new Array();
    $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
        arrayImpuestosItem.push($(elemento).val());
    });


    let arrayIdUnidadMedida = new Array();
    $("select[name='cboUnidadMedida[]']").each(function (indice, elemento) {
        arrayIdUnidadMedida.push($(elemento).val());
    });

    let arrayCantidadNecesaria = new Array();
    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val().replace(/,/g, ""));
    });

    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val().replace(/,/g, ""));
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val().replace(/,/g, ""));
    });

    let arrayAlmacen = new Array();
    $("input[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacen.push($(elemento).val());
    });

    let arrayReferencia = new Array();
    $("input[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });
    //end detalle


    //CAMPOS NUEVOS
    let DescOrigen = new Array();
    $("input[name='DescOrigen[]']").each(function (indice, elemento) {
        DescOrigen.push($(elemento).val());
    });

    let IdOrigen = new Array();
    $("input[name='IdOrigen[]']").each(function (indice, elemento) {
        IdOrigen.push($(elemento).val());
    });
    //END CAMPOS NUEVOS

    let IdSolicitud = new Array();
    $("input[name='IdSolicitud[]']").each(function (indice, elemento) {
        IdSolicitud.push($(elemento).val());
    });


    //AnexoDetalle
    let arrayTxtNombreAnexo = new Array();
    $("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
        arrayTxtNombreAnexo.push($(elemento).val());
    });

    let AnexoDetalle = [];
    for (var i = 0; i < arrayTxtNombreAnexo.length; i++) {
        AnexoDetalle.push({
            'NombreArchivo': arrayTxtNombreAnexo[i]
        });
    }

    let detalles = [];
    if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

        for (var i = 0; i < arrayIdArticulo.length; i++) {

            

            detalles.push({
                'IdPedidoDetalle': 0,
                'IdPedido': 0,
                'IdArticulo': parseInt(arrayIdArticulo[i]),
                'DescripcionArticulo': (arraytxtDescripcionArticulo[i]),
                'IdDefinicion': arrayIdUnidadMedida[i],
                'Cantidad': arrayCantidadNecesaria[i],
                'valor_unitario': arrayPrecioInfo[i],
                'precio_unitario': 0,
                'IdIndicadorImpuesto': arrayImpuestosItem[i],
                'total_base_igv': 0,
                'porcentaje_igv': 0,
                'total_igv': 0,
                'total_impuestos': arrayTotal[i] - (arrayPrecioInfo[i] * arrayCantidadNecesaria[i]),
                'total_valor_item': (arrayPrecioInfo[i] * arrayCantidadNecesaria[i]),
                'total_item': arrayTotal[i],
                'Referencia': arrayReferencia[i],

                'DescOrigen': DescOrigen[i],
                'IdOrigen': IdOrigen[i],
                'IdSolicitud': IdSolicitud[i]
            })
        }
        console.log("detalles");
        console.log(detalles);
        console.log("detalles");
    }



    $.ajax({
        url: "UpdateInsertPedido",
        type: "POST",
        async: true,
        data: {
            detalles,
            AnexoDetalle,
            //cabecera
            'IdAlmacen': IdAlmacen,
            'Serie': $("#IdSerie").val(),
            'IdProveedor': $("#IdProveedor").val(),
            'Direccion': $("#Direccion").val(),
            'Telefono': $("#Telefono").val(),
            'FechaEntrega': $("#FechaEntrega").val(),
            'FechaContabilizacion': $("#txtFechaContabilizacion").val(),
            'FechaDocumento': $("#txtFechaDocumento").val(),
            'IdTipoPedido': $("#IdTipoPedido").val(),
            'LugarEntrega': $("#LugarEntrega").val(),
            'IdCondicionPago': $("#IdCondicionPago").val(),
            'ElaboradoPor': 1,
            'IdMoneda': $("#cboMoneda").val(),
            'Observacion': $("#txtComentarios").val(),
            'TipoCambio': $("#txtTipoCambio").val(),


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
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                
                CerrarModal();
                //ObtenerDatosxID(data);
                listarPedidoDt();

                location.reload();

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }
            tablepedido.ajax.reload();

        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });
}

function ObtenerDatosxID(id) {
    $("#div_tabla_listadorequerimiento_items").hide();
    $("#div_tabla_listado_items").show();
    $("#btnVerItemModal2").hide();
    $("#btnVerItemModal1").show();
    CargarBase();
    CargarPedido();
    CargarSeries();
    CargarProveedor();
    CargarMoneda();
    CargarCondicionPago();
    $("#lblTituloModal").html("Editar Pedido");
    AbrirModal("modal-form");
  
    $.post('/Pedido/ObtenerDatosxID', {
        'IdPedido': id,
    }, function (data, status) {
        let pedido = JSON.parse(data);

        console.log(pedido);
        console.log("pedido");

        $("#IdBase").val(pedido.IdBase).change();
        $("#IdObra").val(pedido.IdObra).change();
        $("#IdAlmacen").val(pedido.IdAlmacen)
        $("#IdSerie").val(pedido.Serie)
        $("#IdProveedor").val(pedido.IdProveedor).change();
        $("#IdTipoPedido").val(pedido.IdTipoPedido)
        $("#LugarEntrega").val(pedido.LugarEntrega)
        $("#IdCondicionPago").val(pedido.IdCondicionPago)
        $("#txtNumeracion").val(pedido.Correlativo)
        $("#NombUsuario").html(pedido.NombUsuario)
        $("#total_items").html(pedido.detalles.length)
        $("#CreatedAt").html(pedido.CreatedAt);
        $("#txtTipoCambio").html(pedido.TipoCambio);
        $("#txtFechaDocumento").val((pedido.FechaDocumento).split("T")[0])

        $("#txtFechaContabilizacion").val((pedido.FechaContabilizacion).split("T")[0])
        //$("#cboMoneda").html(pedido.IdMoneda).change();
        $("#cboMoneda").val(pedido.IdMoneda).change();

        //AnxoDetalle
        let AnexoDetalle = pedido.AnexoDetalle;
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
                </tr>`;
        }
        $("#tabla_files").find('tbody').append(trAnexo);
        
        
        
        
        for (var p = 0; p < pedido.detalles.length; p++) {

            console.log(pedido.detalles[p])
            let IdItem = pedido.detalles[p].IdArticulo;
            let CodigoItem = pedido.detalles[p].CodigoProducto;
            let MedidaItem = pedido.detalles[p].IdDefinicion;
            let DescripcionItem = pedido.detalles[p].DescripcionArticulo;
            let PrecioUnitarioItem = pedido.detalles[p].valor_unitario;
            let CantidadItem = pedido.detalles[p].Cantidad;
            let ProyectoItem = 0;
            let CentroCostoItem = 0;
            let ReferenciaItem = pedido.detalles[p].Referencia;
            let AlmacenItem = 0;
            let PrioridadItem = 0;
            let IdGrupoUnidadMedida = pedido.detalles[p].IdGrupoUnidadMedida;
            let IdIndicadorImpuesto = pedido.detalles[p].IdIndicadorImpuesto;

            let SerieNumero = pedido.detalles[p].SerieNumero;

            //txtReferenciaItem

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();

            today = yyyy + '-' + mm + '-' + dd;


            let UnidadMedida;
            let IndicadorImpuesto;
            let Almacen;
            let Proveedor;
            let LineaNegocio;
            let CentroCosto;
            let Proyecto;
            let Moneda;



            //validaciones
            $.ajaxSetup({ async: false });


            $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
                UnidadMedida = JSON.parse(data);
            });

            $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
                IndicadorImpuesto = JSON.parse(data);
            });

            $.post("../Almacen/ObtenerAlmacen", function (data, status) {
                Almacen = JSON.parse(data);
            });

            $.post("/Proveedor/ObtenerProveedores", function (data, status) {
                Proveedor = JSON.parse(data);
            });



            $.post("/Moneda/ObtenerMonedas", function (data, status) {
                Moneda = JSON.parse(data);
            });

            contador++;
            let tr = '';

            //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
            //    <option value="0">Seleccione</option>
            //</select>
            tr += `<tr id="contador` + contador +`">
            <td>
                <input type="hidden" name="DescOrigen[]" value="0"/>
                <input type="hidden" name="IdOrigen[]" value="0"/>
                <input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" disabled />
            </td>

            <td>`+ CodigoItem +`</td>

            <td>
            <input disabled id="SerieCorrelativo`+ contador +`" class="form-control" type="text"/>
            <input style="display:none" disabled id="IdSolicitud`+ contador +`" name="IdSolicitud[]" class="form-control" value="0"/>
            </td>

            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" disabled /></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
            }
            tr += `</select>
            </td>
           
           
            <td input style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td><input class="form-control"  type="text" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td><input class="form-control" type="text" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
            tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" disabled>` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="text" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Almacen.length; i++) {
                tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>`
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            //for (var i = 0; i < LineaNegocio.length; i++) {
            //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
            //}
            tr += `</select>
            </td>`;
            //for (var i = 0; i < CentroCosto.length; i++) {
            //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
            //}

            //for (var i = 0; i < Proyecto.length; i++) {
            //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
            //}
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]" disabled></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="eliminartrpedidos(`+contador+`)"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);


            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();
            let varimpuesto = $("#cboImpuesto").val();

            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }
            if (varimpuesto) {
                $(".ImpuestoCabecera").val(varimpuesto);
            }


            $("#txtIdArticulo" + contador).val(IdItem);
            $("#txtCodigoArticulo" + contador).val(CodigoItem);
            $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
            $("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#txtCantidadNecesaria" + contador).val(formatNumberDecimales(CantidadItem, 1)).change();

            
            $("#txtPrecioInfo" + contador).val(formatNumberDecimales(PrecioUnitarioItem,2));
      
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);
            $("#txtItemTotal" + contador).val(formatNumberDecimales(pedido.detalles[p].total_item, 2));

            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

            $("#SerieCorrelativo" + contador).val(SerieNumero);
            

            $("#cboCentroCostos" + contador).val(CentroCostoItem);
            $("#txtReferencia" + contador).val(ReferenciaItem);

            $("#txtTotalAntesDescuento").val(formatNumberDecimales(pedido.total_valor, 2))
            $("#txtImpuesto").val(formatNumberDecimales(pedido.total_igv, 2))
            $("#txtTotal").val(formatNumberDecimales(pedido.total_venta, 2))
            NumeracionDinamica();
        }

    });
    disabledmodal(true);
}

function mostrarproductosaprobados() {
    $("#ModalProductosAprobados").modal('show');
    listarItemAprobados();
   
}

function mostrarParaGenerar() {
    $("#ModalOrdenAgrupadaporProductoAprobador").modal('show');
    listarProductosAsignadosRQAgrupados();
}

function eliminartrpedidos(count) {
    $("#contador" + count).remove();
}


function ValidarMonedaBase() {

    let varMoneda = $("#cboMoneda").val();

    $.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

        if (data == "error") {
            //swal("Error!", "Ocurrio un Error")
            return;
        } else {

            let datos = JSON.parse(data);
            if (datos[0].Base) {
                $("#txtTipoCambio").prop('disabled', true);
            } else {
                $("#txtTipoCambio").prop('disabled', false);
            }

        }
    });
    $(".MonedaDeCabecera").val(varMoneda).change();



    let Moneda = $("#cboMoneda").val();
    $.post("/FacturaProveedor/ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {
        let dato = JSON.parse(data);
        //console.log(dato);
        $("#txtTipoCambio").val(dato.venta);

    });
    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
    updateProvedorArticulos();

}
function updateProvedorArticulos() {
    let IdProveedor = +$("#IdProveedor").val();
    let TipoItem = +$("#cboClaseArticulo").val(); 

    let IdObra = +$("#IdObra").val(); 

    console.log(IdObra);

    $.post("ObtenerDatosProveedorXRQAsignados", { 'IdProveedor': IdProveedor, 'TipoItem': TipoItem, 'IdObra': IdObra }, function (data, status) {
        if (!validadJson(data)) {
            return "error";
        }
        let datos = JSON.parse(data);

        let tr = "";
        let contador = 0;
        for (var p = 0; p < datos.length; p++) {


           
            contador++;

            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();
            let varimpuesto = $("#cboImpuesto").val();

            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }
            if (varimpuesto) {
                $(".ImpuestoCabecera").val(varimpuesto);
            }


            $("#txtIdArticulo" + contador).val(datos[p].IdArticulo);
            $("#txtCodigoArticulo" + contador).val(datos[p].CodigoArticulo);
            $("#txtDescripcionArticulo" + contador).val(datos[p].NombArticulo);
            //$("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#cboUnidadMedida" + contador).val(datos[p].IdUnidadMedida);

            $("#txtCantidadNecesaria" + contador).val(formatNumberDecimales( datos[p].Cantidad, 2)).change();
            if (+varMoneda == 1) {
                $("#txtPrecioInfo" + contador).val(formatNumberDecimales(datos[p].Precio,2)).change();
            } else {
                $("#txtPrecioInfo" + contador).val(formatNumberDecimales(datos[p].PrecioExtrangero,2)).change();
            }
            
            //$("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(datos[p].IdAlmacen);
            //$("#cboPrioridadDetalle" + contador).val(PrioridadItem);

            $("#cboIndicadorImpuestoDetalle" + contador).val(1).change();

            //$("#cboCentroCostos" + contador).val(CentroCostoItem);
            //$("#txtReferencia" + contador).val(ReferenciaItem);
            CalcularTotalDetalle(contador);
        }

    });
} 

function listarProductosAsignadosRQAgrupados() {
    tableProductosAprobadosRQ = $('#tabla_listado_productosaprobadosrq').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: "ListarProductosAsignadosxProveedorxIdUsuarioDT",
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

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
                    return full.NombObra
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoItem
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
                    return full.RazonSocial
                },
            },
            //{
            //    targets: 5,
            //    orderable: false,
            //    render: function (data, type, full, meta) {
            //        return `<button class="btn btn-sm btn-danger">Seleccionar</button>`
            //    },
            //}



        ],
        "bDestroy": true
    }).DataTable();
    let ultimaFila;
    let colorOriginal;

    $('#tabla_listado_productosaprobadosrq tbody').unbind("dblclick");

    $('#tabla_listado_productosaprobadosrq tbody').on('dblclick', 'tr', function () {
        var data = tableProductosAprobadosRQ.row(this).data();
        //console.log(data);


        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        tableProductosAprobadosRQ.ajax.reload()
        //limpiar tabla
        let tr = "";
       
      //  $("#tabla").find('tbody').remove();

        ObtenerDatosProveedorXRQ(data.IdProveedor, data)
        $("#ModalOrdenAgrupadaporProductoAprobador").modal('hide');
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);
        $("#LugarEntrega").val(data.DireccionObra)
    });
}



function CambiarClaseArticulo() {

    let ClaseArticulo = $("#cboClaseArticulo").val();

    if (ClaseArticulo == "2") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
    } else {
        $("#IdTipoProducto").show();
        $("#IdTipoProducto").val(0);
    }

}


function ObtenerDatosProveedorXRQ(IdProveedor, dataa) {

    $("#IdBase").val(dataa.IdBase).change();
    $("#IdObra").val(dataa.IdObra).change();
    $("#IdAlmacen").val(dataa.IdAlmacen).change();
    $("#IdProveedor").val(dataa.IdProveedor).change();

    var TipoItem = 0;
    if (dataa.TipoItem == "SERVICIO") {
        TipoItem = 2;
        $("#IdTipoPedido").val(2);
        $("#cboClaseArticulo").val(2);

        $("#IdTipoProducto").hide();

    } else if (dataa.TipoItem == "PRODUCTO") {
        TipoItem = 1;
        $("#IdTipoPedido").val(1);
        $("#cboClaseArticulo").val(1);

        $("#IdTipoProducto").show();
    } else {
        TipoItem = 3;
        $("#IdTipoPedido").val(1);
        $("#cboClaseArticulo").val(3);

        $("#IdTipoProducto").show();
    }

    $("#tbody_detalle").html("");

    let IdObra = +$("#IdObra").val(); 

    console.log("aa");
    console.log(IdObra);





    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionxProveedor", { "IdProveedor": IdProveedor }, function (data, status) {

        if (validadJson(data)) {
            var datos = JSON.parse(data);
            $("#IdCondicionPago").val(datos[0].IdCondicionPago);
        }
    });



    $.post("ObtenerDatosProveedorXRQAsignados", { 'IdProveedor': IdProveedor, 'TipoItem': TipoItem, 'IdObra': IdObra }, function (data, status) {
        if (!validadJson(data)) {
            return "error";
        }
        let datos = JSON.parse(data);

        let tr = "";
        let contador = 0;
        //limpiar 
        for (var p = 0; p < datos.length; p++) {
            eliminartr3(p+1)
        }

        console.log("asssssssssssss");
        console.log(datos);
        console.log("asssssssssssss");

        for (var p = 0; p < datos.length; p++) {


            //for (var i = 0; i < datos.length; i++) {
            //    if (datos[i].IdCondicionPago == ) {

            //    }
            //}
            


            let IdGrupoUnidadMedida = datos[p].IdGrupoUnidadMedida;

            $.ajaxSetup({ async: false });
            $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
                UnidadMedida = JSON.parse(data);
            });


            $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
                IndicadorImpuesto = JSON.parse(data);
            });

            $.post("../Almacen/ObtenerAlmacen", function (data, status) {
                Almacen = JSON.parse(data);
            });

            $.post("/Proveedor/ObtenerProveedores", function (data, status) {
                Proveedor = JSON.parse(data);
            });

            $.post("/Moneda/ObtenerMonedas", function (data, status) {
                Moneda = JSON.parse(data);
            });

            contador++;
            tr = '';

            //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
            //    <option value="0">Seleccione</option>
            //</select>
            tr += `<tr id="eliminartr3`+contador+`">
            <td>
                <input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td>
            <input style="display:none;" class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />

                <input style="display:none" type="text" id="DescOrigen" name="DescOrigen[]" value="RQ"/>
                <input style="display:none" type="text" id="IdOrigen" name="IdOrigen[]" value="`+ datos[p].IdAsignadoPedidoRequerimiento + `"/>

            <input disabled class="form-control" type="text" value="`+ datos[p].CodigoArticulo + `" id="txtCodigoArticulo` + contador + `" name="txtCodigoArticulo[]" />
            </td>
            <td>
            <input disabled class="form-control" type="text" value="`+ datos[p].SerieCorrelativo +`"/>
            <input style="display:none" disabled id="IdSolicitud" name="IdSolicitud[]" class="form-control" type="text" value="`+ datos[p].IdSolicitud +`"/>  
            </td>

            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" disabled/></td>
            <td>
            
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
            }
            tr += `</select>
            </td>
           
           
            <td  style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td><input class="form-control"  type="text" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td><input class="form-control" type="text" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
            tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="text" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Almacen.length; i++) {
                tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>`
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
            tr += `  <option value="0">Seleccione</option>`;
            //for (var i = 0; i < LineaNegocio.length; i++) {
            //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
            //}
            tr += `</select>
            </td>`;
            //for (var i = 0; i < CentroCosto.length; i++) {
            //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
            //}

            //for (var i = 0; i < Proyecto.length; i++) {
            //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
            //}
            tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar  fa fa-trash" onclick="eliminartr3(`+ contador +`)" style="background-color:red"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);
            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();
            let varimpuesto = $("#cboImpuesto").val();

            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }
            if (varimpuesto) {
                $(".ImpuestoCabecera").val(varimpuesto);
            }

            //console.log(datos[p]);

            $("#txtIdArticulo" + contador).val(datos[p].IdArticulo);
            $("#txtCodigoArticulo" + contador).val(datos[p].CodigoArticulo);
            $("#txtDescripcionArticulo" + contador).val(datos[p].NombArticulo);
            //$("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#cboUnidadMedida" + contador).val(datos[p].IdUnidadMedida);


           // $("#txtCantidadNecesaria" + contador).val(formatNumberDecimales(datos[p].Cantidad,2) ).change();

            if (+varMoneda > 1) {
                $("#txtPrecioInfo" + contador).val((datos[p].PrecioExtrangero)).change();

                //$("#txtPrecioInfo" + contador).val(formatNumberDecimales(datos[p].PrecioExtrangero, 2)).change();
            }
            else {
                $("#txtPrecioInfo" + contador).val(datos[p].Precio).change();

                //$("#txtPrecioInfo" + contador).val(formatNumberDecimales(datos[p].Precio, 2)).change();
            }




            $("#txtCantidadNecesaria" + contador).val(datos[p].Cantidad).change();
            //$("#txtCantidadNecesaria" + contador).val(formatNumberDecimales(datos[p].Cantidad,2)).change();
            //$("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(datos[p].IdAlmacen);
            //$("#cboPrioridadDetalle" + contador).val(PrioridadItem);

           $("#cboIndicadorImpuestoDetalle" + contador).val(1).change();

            //$("#cboCentroCostos" + contador).val(CentroCostoItem);
            //$("#txtReferencia" + contador).val(ReferenciaItem);
            CalcularTotalDetalle(contador);
        }

    });


    NumeracionDinamica();

}



function eliminartr3(count) {
    $("#eliminartr3" + count).remove();
    CalcularTotales();
}
function AgregarLinea() {
    let IdItem = $("#txtIdItem").val();
    let CodigoItem = $("#txtCodigoItem").val();
    let MedidaItem = $("#cboMedidaItem").val();
    let DescripcionItem = $("#txtDescripcionItem").val();
    let PrecioUnitarioItem = $("#txtPrecioUnitarioItem").val();
    let CantidadItem = $("#txtCantidadItem").val();
    let ProyectoItem = $("#cboProyectoItem").val();
    let CentroCostoItem = $("#cboCentroCostoItem").val();
    let ReferenciaItem = $("#txtReferenciaItem").val();
    let AlmacenItem = $("#cboAlmacenItem").val();
    let PrioridadItem = $("#cboPrioridadItem").val();
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    let IdIndicadorImpuesto = $("#IdIndicadorImpuesto").val();

    //txtReferenciaItem

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;


    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    //valdiaciones

    let ValidartCentroCosto = $("#cboCentroCostoItem").val();
    let ValidartProducto = $("#cboMedidaItem").val();
    let ValidarCantidad = $("#txtCantidadItem").val();
    let ValidarPrecio = $("#txtPrecioUnitarioItem").val();
    //console.log(ValidarCantidad);
    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0" || !$.isNumeric(ValidarCantidad)) {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }

    if (ValidarPrecio == "" || ValidarPrecio == null || ValidarPrecio == "0" || !$.isNumeric(ValidarPrecio)) {
        swal("Informacion!", "Debe Especificar Precio!");
        return;
    }
    if (ValidartCentroCosto == 0) {
        swal("Informacion!", "Debe Seleccionar Centro de Costo!");
        return;
    }
    if (ValidartProducto == 0) {
        swal("Informacion!", "Debe Seleccionar Producto!");
        return;
    }

    if (CantidadItem<0) {
        swal("Informacion!", "La cantidad del produto debe mayor a 0!");
        return;
    }

    if ($("#IdIndicadorImpuesto").val() ==0) {
        swal("Informacion!", "Debe selecionar un indicador de impuesto!");
        return;
    }

    
    //validaciones
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("../Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
    });

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    Proyecto = JSON.parse(data);
    //});

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        Moneda = JSON.parse(data);
    });

    contador++;
    let tr = '';

    //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
    //    <option value="0">Seleccione</option>
    //</select>
    tr += `<tr  id="eliminartr4` + contador +`">
            <td><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
    }
    tr += `</select>
            </td>
           
           
            <td input style="display:none;">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Moneda.length; i++) {
        tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            <td style="display:none">
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>`
    tr += `
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < LineaNegocio.length; i++) {
    //    tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
    //}
    tr += `</select>
            </td>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
    //}

    //for (var i = 0; i < Proyecto.length; i++) {
    //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
    //}
    tr += `
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar tram33  fa fa-trash" onclick="eliminartr4(` + contador +`)"></button></td>

          <tr>`;

    $("#tabla").find('tbody').append(tr);


    let varMoneda = $("#cboMoneda").val();
    let varTipoCambio = $("#txtTipoCambio").val();
    let varimpuesto = $("#cboImpuesto").val();

    if (varMoneda) {
        $(".MonedaDeCabecera").val(varMoneda);
    }
    if (varTipoCambio) {
        $(".TipoCambioDeCabecera").val(varTipoCambio);
    }
    if (varimpuesto) {
        $(".ImpuestoCabecera").val(varimpuesto);
    }



    $("#txtIdArticulo" + contador).val(IdItem);
    $("#txtCodigoArticulo" + contador).val(CodigoItem);
    $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
    $("#cboUnidadMedida" + contador).val(MedidaItem);
    $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();
    $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
    $("#cboProyecto" + contador).val(ProyectoItem);
    $("#cboAlmacen" + contador).val(AlmacenItem);
    $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

    $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);
    CalcularTotalDetalle(contador);
    LimpiarModalItem();
}
function eliminartr4(count) {
    $("#eliminartr4" + count).remove();
    CalcularTotales();
}

function listarItemAprobados(IdDetalleSeleccionado = 0) {

    lenguaje_data.order = [[1, 'desc']];

    tableitemsaprobados = $('#tabla_listado_itemsaprobados').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: "ListarItemAprobadosxSociedadDT",
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    //return `<input disabled type="radio" clase="" IdProveedor="` + full.Idproveedor + `"  IdProductoLinea="` + full.IdArticulo + `" id="rdSeleccionado` + full.IdDetalle + `"  name="rdSeleccionado"  value = "` + full.IdDetalle + `" >`

                    return `<input style="display:none" disabled type="radio" clase="" IdProveedor="` + full.Idproveedor + `"  IdProductoLinea="` + full.IdArticulo + `" id="rdSeleccionado` + full.IdDetalle + `"  name="rdSeleccionado"  value = "` + full.IdDetalle + `" />`
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra.toUpperCase()
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroSolicitud.toUpperCase()
                },
            },

            {
                targets: 3,
                orderable: true,
                render: function (data, type, full, meta) {
                    return full.Fecha.split('T')[0]
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombArticulo.toUpperCase()
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUnidadMedida
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Cantidad
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Referencia
                },
            },
            {
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            }



        ],


        "bDestroy": true
    }).DataTable();
  


    $('#tabla_listado_itemsaprobados tbody').unbind("dblclick");

    $('#tabla_listado_itemsaprobados tbody').on('dblclick', 'tr', function () {
        for (var i = 0; i < tableitemsaprobados.rows().count(); i++) {
            let row = tableitemsaprobados.row(i);

            let data = row.data();
            
            if (data.IdDetalle != +IdDetalleSeleccionado) {
                $(row.node()).removeClass("selectA");;
            }
        }
        let row = tableitemsaprobados.row(this);
       // $(row.node()).css("background-color", "#f00");
        var data = row.data();
        //console.log(data);



        //colorOriginal = $("#" + data["DT_RowId"]).css('background-color');

        //$("#" + data["DT_RowId"]).addClass('selectA');
        //ultimaFila = $("#" + data["DT_RowId"]);

        //$("#rdSeleccionado" + data["IdDetalle"]).prop('checked', true);


        if (ultimaFila != null) {
            $("#" + ultimaFila).css({ "background-color": colorOriginal, "color": "#7f828f" });
            ultimaFila = data["IdDetalle"];
        }
        else {
            ultimaFila = data["IdDetalle"];
            colorOriginal = "transparent";
        }

        $("#" + data["IdDetalle"]).css({ "background-color": "var(--color-segundario)", "color": "white" });

        $("#rdSeleccionado" + data["IdDetalle"]).prop('checked', true);


        ObtenerStockxIdDetalleSolicitudRQ(data.IdDetalle)
        ObtenerProveedoresPrecioxProducto(data.IdArticulo, data.IdDetalle,  data.IdProveedor,data.IdObra)
        ObtenerPrecioxProductoUltimasVentas(data.IdArticulo, data.IdDetalle)
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}

function actualizarInfoAutorizados(IdDetalle) {

    let data= null
    for (var i = 0; i < tableitemsaprobados.rows().count(); i++) {
        let row = tableitemsaprobados.row(i);

        let dataTemp = row.data();
        
        if (dataTemp.IdDetalle != +IdDetalle) {
            $(row.node()).removeClass("selectA");
            //console.log(dataTemp.IdDetalle);
        }
        else {

            data = dataTemp;
        }
    }


    //colorOriginal = $("#" + data["DT_RowId"]).css('background-color');

    //$("#" + data["DT_RowId"]).addClass('selectA');
    //ultimaFila = $("#" + data["DT_RowId"]);

    //$("#rdSeleccionado" + data["IdDetalle"]).prop('checked', true);

    if (ultimaFila != null) {
        $("#" + ultimaFila).css({ "background-color": colorOriginal, "color": "#7f828f" });
        ultimaFila = data["DT_RowId"];
    }
    else {
        ultimaFila = data["DT_RowId"];
        colorOriginal = "transparent";
    }
    $("#" + data["DT_RowId"]).css({ "background-color": "var(--color-segundario)", "color": "white" });
    $("#rdSeleccionado" + data["IdDetalle"]).prop('checked', true);

    ObtenerStockxIdDetalleSolicitudRQ(data.IdDetalle)
    ObtenerProveedoresPrecioxProducto(data.IdArticulo, data.IdDetalle, data.IdProveedor,data.IdObra)
    ObtenerPrecioxProductoUltimasVentas(data.IdArticulo, data.IdDetalle)



} 
 

function ObtenerStockxIdDetalleSolicitudRQ(IdDetalle) {
    $.ajaxSetup({ async: false });
    $.post("ObtenerStockxIdDetalleSolicitudRQ", { 'IdDetalleRQ': IdDetalle }, function (data, status) {
        if (!validadJson(data)) {
            $("#tabla_aprobados_stock tbody").html('');
            return true;
        }
        let datos = JSON.parse(data);
        //console.log(datos);
        let tr = "";
        for (var i = 0; i < datos.length; i++) {
            tr += `<tr>
                
                <td>`+ datos[i].NombObra + `</td>
                <td>`+ datos[i].NombAlmacen + `</td>
                <td>`+ datos[i].Stock + `</td>
            </tr>`;
        }

        $("#tabla_aprobados_stock tbody").html(tr);
    });
}

function ObtenerProveedoresPrecioxProducto(IdArticulo, IdDetalleRq, IdProveedor,IdObra ) {

    $.ajaxSetup({ async: false });
    $.post("ObtenerProveedoresPrecioxProductoConObras", { 'IdObra':IdObra, 'IdArticulo': IdArticulo }, function (data, status) {
        let tr = "";
        if (!validadJson(data)) {
            tr += `<tr>
                <td colspan="4">NO HAY DATOS </td>
            </tr>`;
            $("#tabla_proveedor_aprobados tbody").html(tr);
            return true;
        }
        let datos = JSON.parse(data);
        console.log(datos);

        for (var i = 0; i < datos.length; i++) {

            if (IdProveedor == datos[i].IdProveedor) {
                tr += `<tr>
                <td>
                        <input type="hidden" id="idproducto`+ datos[i].IdProveedor + `" value="` + IdArticulo + `" /> 
                        <input type="hidden" id="IdDetalleRq`+ datos[i].IdProveedor + `" value="` + IdDetalleRq + `" />
                        ` + datos[i].RazonSocial + `</td>
                <td id="NombObra"`+ datos[i].IdProveedor+`>`+datos[i].NombObra+`</td>
                <td> <input class="form-control" id="precionacional`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioNacional + `"/></td>
                <td> <input class="form-control" id="precioextranjero`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioExtranjero + `"/></td>
                <td style="display:none"> <input class="form-control"  id="comentario`+ datos[i].IdProveedor + `" value=""/></td>
                <td> <button class="btn btn-danger editar solo" onclick="AsignarProveedorPrecio(`+ datos[i].IdProveedor + `)">ASIGNADO</button></td>
            </tr>`;
            }
            else {
                tr += `<tr>
                <td>
                        <input type="hidden" id="idproducto`+ datos[i].IdProveedor + `" value="` + IdArticulo + `" /> 
                        <input type="hidden" id="IdDetalleRq`+ datos[i].IdProveedor + `" value="` + IdDetalleRq + `" />
                        ` + datos[i].RazonSocial + `</td>
                <td id="NombObra"`+ datos[i].IdProveedor +`>`+ datos[i].NombObra +`</td>
                <td> <input class="form-control" id="precionacional`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioNacional + `"/></td>
                <td> <input class="form-control" id="precioextranjero`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioExtranjero + `"/></td>
                <td style="display:none"> <input class="form-control"  id="comentario`+ datos[i].IdProveedor + `" value=""/></td>
                <td> <button class="btn btn-info editar solo" onclick="AsignarProveedorPrecio(`+ datos[i].IdProveedor + `)">ASIGNAR</button></td>
            </tr>`;
            }
          
        }
        //console.log(tr);
        $("#tabla_proveedor_aprobados tbody").html(tr);
    });
}
function ObtenerPrecioxProductoUltimasVentas(IdArticulo, IdDetalleRq) {

    $.ajaxSetup({ async: false });
    $.post("ObtenerPrecioxProductoUltimasVentas", { 'IdArticulo': IdArticulo }, function (data, status) {
        let tr = "";
        if (!validadJson(data)) {
            tr += `<tr>
                <td colspan="4">NO HAY DATOS </td>
            </tr>`;
            $("#tabla_listado_ultimasventas tbody").html(tr);
            return true;
        }
        let datos = JSON.parse(data);

        for (var i = 0; i < datos.length; i++) {
            tr += `<tr>
                <td>
                        <input type="hidden" id="idproducto`+ datos[i].IdProveedor + `" value="` + IdArticulo + `" /> 
                        <input type="hidden" id="IdDetalleRq`+ datos[i].IdProveedor + `" value="` + IdDetalleRq + `" />
                        ` + datos[i].RazonSocial + `</td>
                <td> <input class="form-control" disabled id="precionacional`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioNacional + `"/></td>
                <td> <input class="form-control" disabled id="precioextranjero`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioExtranjero + `"/></td>
                <td> <input class="form-control" disabled id="cantidad`+ datos[i].IdProveedor + `" value="` + datos[i].Cantidad + `"/></td>
                <td> <input class="form-control" disabled id="cantidad`+ datos[i].IdProveedor + `" value="` + datos[i].Fecha + `"/></td>
                <td> <input class="form-control" disabled id="cantidad`+ datos[i].IdProveedor + `" value="` + datos[i].Obra + `"/></td>
                
            </tr>`;
        }
        //console.log(tr);
        $("#tabla_listado_ultimasventas tbody").html(tr);
    });
}

function AsignarProveedorPrecio(IdProveedor) {
   // if (IdProveedor==null)
     let precionacional = $("#precionacional" + IdProveedor).val();
    let precioextranjero = $("#precioextranjero" + IdProveedor).val();
    let idproducto = $("#idproducto" + IdProveedor).val();
    let IdDetalleRq = $("#IdDetalleRq" + IdProveedor).val();
    let Comentario = $("#comentario" + IdProveedor).val();
    let IdObra = $("#NombObra"+ IdProveedor).val()
           

    $.ajaxSetup({ async: false });
    $.post("ActualizarProveedorPrecio", { 'IdProveedor': IdProveedor, 'precionacional': precionacional, 'precioextranjero': precioextranjero, 'idproducto': idproducto, 'IdDetalleRq': IdDetalleRq, 'Comentario': Comentario }, function (data, status) {
        listarItemAprobados(IdDetalleRq);
        ObtenerProveedoresPrecioxProducto(idproducto, IdDetalleRq, IdProveedor,IdObra);
        actualizarInfoAutorizados(IdDetalleRq)
       
      

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


function agregarProvedoraProductoxSeparado() {
    $("#IdProveedorAsignar").val(0).change();
    $("#PrecioAsignar").val(0);
    $("#ComentarioPrecio").val('');

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').attr("idproductolinea");



    if (IdArticulo === undefined) {
        swal("", "Seleccione un Articulo", "warning")

    }
    else {
        $("#modalasignarproveedor").modal();
        ObtenerPrecioRQDetalle();
        $.ajaxSetup({ async: false });
        $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
            let proveedores = JSON.parse(data);
            llenarComboProveedor(proveedores, "IdProveedorAsignar", "Seleccione")
        });
        CargarCondicionPagoArticuloProveedor();
    }
    ObtenerObra()
    $("#PrecioAsignar").val(0);
}
function CargarCondicionPagoArticuloProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarCondicionPagoAP(condicionpago, "IdCondicionPagoProveedor", "Seleccione")
    });
}


function llenarCondicionPagoAP(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCondicionPago + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



function ObtenerPrecioRQDetalle() {
    let IdDetalleRq = $('input:radio[name=rdSeleccionado]:checked').val();
    $.post("/SolicitudRQ/ObtenerDatosRqDetallexID", { 'IdDetalleRq': IdDetalleRq }, function (data, status) {
        $("#PrecioAsignar").val(data)
   
    });
 
}

function GuardarAsignarProveedorProductoOld() {
    let ValidarPrecio = $("#PrecioAsignar").val();
    if (ValidarPrecio == "" || ValidarPrecio == null || ValidarPrecio == "0" || !$.isNumeric(ValidarPrecio)) {
        swal("Informacion!", "Debe Especificar Precio!");
        return;
    }

    let IdProveedor = $("#IdProveedorAsignar").val();
    let precionacional = $("#PrecioAsignar").val();
    let ComentarioPrecio = $("#ComentarioPrecio").val();
    let precioextranjero = 0;
    let idproducto = $('input:radio[name=rdSeleccionado]:checked').attr("idproductolinea");
    let IdDetalleRq = $('input:radio[name=rdSeleccionado]:checked').val();
    $.ajaxSetup({ async: false });
    $.post("ActualizarProveedorPrecio", { 'IdProveedor': IdProveedor, 'precionacional': precionacional, 'precioextranjero': precioextranjero, 'idproducto': idproducto, 'IdDetalleRq': IdDetalleRq, 'Comentario': ComentarioPrecio }, function (data, status) {
        listarItemAprobados();

        //ObtenerProveedoresPrecioxProducto(idproducto, IdDetalleRq);

    });
    $("#modalasignarproveedor").modal('hide');
}



function addprecioproductoproveedor() {
    let varIdObra
    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').attr("idproductolinea");
    let IdProveedorSeleccionado = $('input:radio[name=rdSeleccionado]:checked').attr("IdProveedor");
    let IdDetalleRq = $('input:radio[name=rdSeleccionado]:checked').val();
    let IdProveedor = $("#IdProveedorAsignar").val();
    let PrecioSoles = $("#PrecioAsignar").val();
    let PrecioDolares = $("#PrecioMonedaExtrangera").val();
    let IdCondicionPagoProveedor = $("#IdCondicionPagoProveedor").val();
    let numeroentrega = $("#numeroentrega").val();

    if (IdProveedor == "" || IdProveedor == null || IdProveedor == "0" || !$.isNumeric(IdProveedor)) {
        swal("Informacion!", "Seleccione Proveedor!");
        return;
    }
    if (PrecioSoles == "" || PrecioSoles == null || PrecioSoles < "0" || !$.isNumeric(PrecioSoles)) {
        swal("Informacion!", "Ingrese Precio Soles!");
        return;
    }

    if (PrecioDolares == "" || PrecioDolares == null || PrecioDolares < "0" || !$.isNumeric(PrecioDolares)) {
        swal("Informacion!", "Ingrese Precio dolares!");
        return;
    }

    if (IdCondicionPagoProveedor == "" || IdCondicionPagoProveedor == null || IdCondicionPagoProveedor == "0" || !$.isNumeric(IdCondicionPagoProveedor)) {
        swal("Informacion!", "Agregue condicion de pago en Maestro Socio de Negocio!");
        return;
    }

    if (numeroentrega == "" || numeroentrega == null || numeroentrega == "0" || !$.isNumeric(numeroentrega)) {
        swal("Informacion!", "Ingrese numero de entrega!");
        return;
    }
    if ($("#ObraArticulo").val() == 0) {
        varIdObra = 0
    } else {
        varIdObra = $("#ObraArticulo").val()
    }

    $.post("/Articulo/SavePrecioProveedorNuevo", {
        "IdArticulo": IdArticulo,
        "IdProveedor": IdProveedor,
        "PrecioSoles": PrecioSoles,
        "PrecioDolares": PrecioDolares,
        "IdCondicionPagoProveedor": IdCondicionPagoProveedor,
        "numeroentrega": numeroentrega,
        "IdObra": varIdObra,

    }, function (data, status) {
        swal("Exito!", "Proceso Realizado Correctamente", "success")
        
        //listarPrecioProductoProveedor(IdArticulo);

        actualizarInfoAutorizados(IdDetalleRq);
        // aqui el detalles
        LimpiarModalItemProveedor();
        //CerrarModalListadoItems();
        
        
    });
}
function LimpiarModalItemProveedor() {

    $("#IdProveedorAsignar").val(null);
    $("#PrecioAsignar").val(0);
    $("#PrecioMonedaExtrangera").val(0);
    $("#IdCondicionPagoProveedor").val(null);
    $("#numeroentrega").val(0);
}


function CerrarModalListadoItems() {
    let int = 1;
    $("#PrecioMonedaExtrangera").val(0)
    $("#numeroentrega").val(0)
    $("#PrecioAsignar").val(0)
}


function CargarBasesObraAlmacenSegunAsignado() {
    let respuesta = 'false';
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerBaseAlmacenxIdUsuarioSession", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            //console.log(datos[0]);
            contadorBase = datos[0].CountBase;
            contadorObra = datos[0].CountObra;
            contadorAlmacen = datos[0].CountAlmacen;
            AbrirModal("modal-form");
            CargarBase();
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen == 1) {
                $.ajaxSetup({ async: false });
                //console.log(1)
                $("#IdBase").val(datos[0].IdBase).change();
                //console.log(2)
                $("#IdObra").val(datos[0].IdObra).change();
                //console.log(3)
                $("#IdAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
                $("#IdAlmacen").prop('disabled', true);

            }
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#IdAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
            }

            if (contadorBase == 1 && contadorObra != 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#IdAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
            }
            if (contadorBase != 1 && contadorObra != 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
            }


            respuesta = 'true';
        } else {
            respuesta = 'false';cb
        }
    });
    return respuesta;
}

function limpiarDatos() {
    $("#IdBase").val(0);
    $("#IdObra").val(0);
    $("#IdAlmacen").val(0);
    $("#IdSerie").val(0);
    $("#IdProveedor").val(0);
    $("#Direccion").html('');
    $("#Telefono").val('');
    $("#IdTipoPedido").val(0);
    $("#LugarEntrega").val('');
    $("#SerieNumeroRef").val('');
    $("#IdCondicionPago").val(0);
    $("#tbody_detalle").html('');
    $("#txtTipoCambio").val('');
   
}

function disabledmodal(valorbolean) {

    $("#IdBase").prop('disabled', valorbolean);
    $("#txtTipoCambio").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#IdAlmacen").prop('disabled', valorbolean);
    //$("#IdSerie").prop('disabled', valorbolean);
    $("#IdProveedor").prop('disabled', valorbolean);
    $("#Direccion").prop('disabled', valorbolean);
    $("#Telefono").prop('disabled', valorbolean);
    $("#IdTipoPedido").prop('disabled', valorbolean);
    $("#LugarEntrega").prop('disabled', valorbolean);
    $("#SerieNumeroRef").prop('disabled', valorbolean);
    $("#SerieNumeroRef").prop('disabled', valorbolean);
    $("#IdCondicionPago").prop('disabled', valorbolean);
    $("#FechaEntrega").prop('disabled', valorbolean);
    $("#txtComentarios").prop('disabled', valorbolean);
    $("#txtFechaDocumento").prop('disabled', valorbolean);
    $("#txtFechaContabilizacion").prop('disabled', valorbolean);
    $("#txtFechaContabilizacion").prop('disabled', valorbolean);
    $("#cboMoneda").prop('disabled', valorbolean);

    $("#btn_agregaritem").prop('disabled', valorbolean);
    if (valorbolean) {
        $("#btnGrabar").hide()
      
        $("#btnExtorno").show();
        //$("#total_editar").show();
        //$("#total_nuevo").hide();
        $("#btnNuevo").show();

    } else {
        $("#btnExtorno").hide();
        //$("#total_editar").hide();
        //$("#total_nuevo").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
    }
}


function nuevo() {
    CerrarModal();
    CalcularTotales();
    ModalNuevo();
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
            //console.log(datosrespuesta.FechaRelacion[i]);
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

function TipoPedidoOrden() {
    let IdTipoPedido = $("#IdTipoPedido").val();

    if (IdTipoPedido == 1) {
        $("#IdSerie").val(20010)
    } else {
        $("#IdSerie").val(20009)

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



function BuscarCondicionPago() {

    var IdProveedor = $("#IdProveedorAsignar").val();
    console.log(IdProveedor);

    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionxProveedor", { "IdProveedor": IdProveedor }, function (data, status) {

        if (validadJson(data)) {
            var datos = JSON.parse(data);
            if (datos[0].IdCondicionPago == 0) {
                $("#IdCondicionPagoProveedor").prop("disabled", false)
                $("#IdCondicionPagoProveedor").val(0);
            } else {
                $("#IdCondicionPagoProveedor").val(datos[0].IdCondicionPago);
                $("#IdCondicionPagoProveedor").prop("disabled", true)
            }
        } 
        
    });
    


}

function ObtenerDiasEntrega() {
    var IdProveedor = $("#IdProveedorAsignar").val();
    console.log(IdProveedor);

    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerDatosxID", { "IdProveedor": IdProveedor }, function (data, status) {

        if (validadJson(data)) {
            var datos = JSON.parse(data);
            $("#numeroentrega").val(datos[0].DiasEntrega);
        }

    });
}






function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
    });
}
function alertar() {
    if ($("#PrecioXGeneral").is(":checked")) {
        $("#divObraArticulo").hide()
        $("#ObraArticulo").val(0)
    } else if ($("#PrecioXObra").is(":checked")) {
        $("#divObraArticulo").show()
    }
}
function ObtenerObra() {
    $.ajaxSetup({ async: false });

    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObraArt(obra, "ObraArticulo", "Seleccione")
    });
}

function llenarComboObraArt(lista, idCombo, primerItem) {
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
