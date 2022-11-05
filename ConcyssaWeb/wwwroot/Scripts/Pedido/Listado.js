let table = '';
let tablepedido = '';
let tableitemsaprobados;
let tableProductosAprobadosRQ;
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
    $("#cboMoneda").val(1);
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

                    return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdPedido + `)"></button>`
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
                    return full.NombObra
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
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
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoPedido
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.LugarEntrega
                },
            },
            {
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.total_venta,2)
                },
            }

        ],
        "bDestroy": true
    }).DataTable();

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
            console.log(datos);
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
                console.log(data);
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
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}


function ModalNuevo() {
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
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento ==4) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
            else { }
        }
        
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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
            console.log(datos);
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
    console.log('CargarDefinicionxGrupo');
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
    console.log('CargarUnidadMedidaItem');
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
        console.log(grupounidad);
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

    console.log(ValidarCantidad);
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
            <td><button class="btn btn-xs btn-danger borrar" onclick="eliminartr2(`+contador+`)">-</button></td>
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

function CalcularTotalDetalle(contador) {

    let varIndicadorImppuesto = $("#cboIndicadorImpuestoDetalle" + contador).val();
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");

    let varCantidadNecesaria = $("#txtCantidadNecesaria" + contador).val();
    let varPrecioInfo = $("#txtPrecioInfo" + contador).val();

    let subtotal = varCantidadNecesaria * varPrecioInfo;

    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        varTotal = subtotal;
    } else {
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }
    $("#txtItemTotal" + contador).val(varTotal.toFixed(2)).change();


}

function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected', elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(Number($(elemento).val()));
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

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(subtotal.toFixed(2));
    $("#txtImpuesto").val(impuesto.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));

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
        arrayCantidadNecesaria.push($(elemento).val());
    });

    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val());
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
                'IdOrigen': IdOrigen[i]
            })
        }

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
            'TipoCambio': 1


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
                ObtenerDatosxID(data);

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
        $("#CreatedAt").html(pedido.CreatedAt)


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
                    <td><button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)">-</button></td>
                </tr>`;
        }
        $("#tabla_files").find('tbody').append(trAnexo);
        
        
        
        
        for (var p = 0; p < pedido.detalles.length; p++) {

            console.log(pedido.detalles[p])
            let IdItem = pedido.detalles[p].IdArticulo;
            let CodigoItem = pedido.detalles[p].IdArticulo;
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
            <td><button class="btn btn-xs btn-danger borrar" onclick="eliminartrpedidos(`+contador+`)">-</button></td>
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
            $("#txtCantidadNecesaria" + contador).val(formatNumberDecimales(CantidadItem,1)).change();
            $("#txtPrecioInfo" + contador).val(formatNumberDecimales(PrecioUnitarioItem,2));
      
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);
            $("#txtItemTotal" + contador).val(formatNumberDecimales(pedido.detalles[p].total_item, 2));

            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

            

            $("#cboCentroCostos" + contador).val(CentroCostoItem);
            $("#txtReferencia" + contador).val(ReferenciaItem);

            $("#txtTotalAntesDescuento").val(formatNumberDecimales(pedido.total_valor, 2))
            $("#txtImpuesto").val(formatNumberDecimales(pedido.total_igv, 2))
            $("#txtTotal").val(formatNumberDecimales(pedido.total_venta, 2))
    
        }

    });
    disabledmodal(true);
}

function mostrarproductosaprobados() {
    $("#ModalProductosAprobados").modal('show');
    listarItemAprobados();
}

function mostrarParaGenerar() {
    $("#ModalOrdenAgrupadaporProductoAprobador").modal();
    listarProductosAsignadosRQAgrupados();
}

function eliminartrpedidos(count) {
    $("#contador" + count).remove();
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
                    return 1
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
                    return full.NombBase
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.RazonSocial
                },
            }, {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<button class="btn btn-sm btn-danger">=</button>`
                },
            }



        ],
        "bDestroy": true
    }).DataTable();
    let ultimaFila;
    let colorOriginal;

    $('#tabla_listado_productosaprobadosrq tbody').unbind("dblclick");

    $('#tabla_listado_productosaprobadosrq tbody').on('dblclick', 'tr', function () {
        var data = tableProductosAprobadosRQ.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        tableProductosAprobadosRQ.ajax.reload()
        ObtenerDatosProveedorXRQ(data.IdProveedor, data)
        $("#ModalOrdenAgrupadaporProductoAprobador").hide();
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}




function ObtenerDatosProveedorXRQ(IdProveedor, dataa) {
    $("#IdBase").val(dataa.IdBase).change();
    $("#IdObra").val(dataa.IdObra).change();
    $("#IdAlmacen").val(dataa.IdAlmacen).change();
    $("#IdProveedor").val(dataa.IdProveedor).change();




    $.post("ObtenerDatosProveedorXRQAsignados", { 'IdProveedor': IdProveedor }, function (data, status) {
        if (!validadJson(data)) {
            return "error";
        }
        let datos = JSON.parse(data);

        let tr = "";
        let contador = 0;
        for (var p = 0; p < datos.length; p++) {


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
                <input type="hidden" name="DescOrigen[]" value="RQ"/>
                <input type="hidden" name="IdOrigen[]" value="`+ datos[p].IdAsignadoPedidoRequerimiento + `"/>

                <input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
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
            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
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
            <td><button class="btn btn-xs btn-danger borrar" onclick="eliminartr3(`+contador+`)">-</button></td>
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

            console.log(datos[p]);

            $("#txtIdArticulo" + contador).val(datos[p].IdArticulo);
            $("#txtCodigoArticulo" + contador).val('');
            $("#txtDescripcionArticulo" + contador).val(datos[p].NombArticulo);
            //$("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#cboUnidadMedida" + contador).val(29);

            $("#txtCantidadNecesaria" + contador).val(datos[p].Cantidad).change();
            $("#txtPrecioInfo" + contador).val(datos[p].Precio).change();
            //$("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(datos[p].IdAlmacen);
            //$("#cboPrioridadDetalle" + contador).val(PrioridadItem);

            //$("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);

            //$("#cboCentroCostos" + contador).val(CentroCostoItem);
            //$("#txtReferencia" + contador).val(ReferenciaItem);
            CalcularTotalDetalle(contador);
        }

    });
}


function eliminartr3(count) {
    $("#eliminartr3" + count).remove();
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
    console.log(ValidarCantidad);
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
    tr += `<tr>
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
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
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

function listarItemAprobados() {
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
                    return `<input type="radio" clase="" IdProductoLinea="` + full.IdArticulo + `" id="rdSeleccionado` + full.IdDetalle + `"  name="rdSeleccionado"  value = "` + full.IdDetalle + `" >`
                },
            },

            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroSolicitud.toUpperCase()
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra.toUpperCase()
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombArticulo.toUpperCase()
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUnidadMedida
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Cantidad
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            }



        ],
        "bDestroy": true
    }).DataTable();
    let ultimaFila;
    let colorOriginal;

    $('#tabla_listado_itemsaprobados tbody').unbind("dblclick");

    $('#tabla_listado_itemsaprobados tbody').on('dblclick', 'tr', function () {
        var data = tableitemsaprobados.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);

        $("#rdSeleccionado" + data["IdDetalle"]).prop('checked', true);

        ObtenerStockxIdDetalleSolicitudRQ(data.IdDetalle)
        ObtenerProveedoresPrecioxProducto(data.IdArticulo, data.IdDetalle)
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}


function ObtenerStockxIdDetalleSolicitudRQ(IdDetalle) {
    $.ajaxSetup({ async: false });
    $.post("ObtenerStockxIdDetalleSolicitudRQ", { 'IdDetalleRQ': IdDetalle }, function (data, status) {
        if (!validadJson(data)) {
            $("#tabla_aprobados_stock tbody").html('');
            return true;
        }
        let datos = JSON.parse(data);

        let tr = "";
        for (var i = 0; i < datos.length; i++) {
            tr += `<tr>
                <td>`+ datos[i].NombAlmacen + `</td>
                <td>`+ datos[i].NombAlmacen + `</td>
                <td>`+ datos[i].NombArticulo + `</td>
                <td>`+ datos[i].Stock + `</td>
            </tr>`;
        }

        $("#tabla_aprobados_stock tbody").html(tr);
    });
}

function ObtenerProveedoresPrecioxProducto(IdArticulo, IdDetalleRq) {

    $.ajaxSetup({ async: false });
    $.post("ObtenerProveedoresPrecioxProducto", { 'IdArticulo': IdArticulo }, function (data, status) {
        let tr = "";
        if (!validadJson(data)) {
            tr += `<tr>
                <td colspan="4">NO HAY DATOS </td>
            </tr>`;
            $("#tabla_proveedor_aprobados tbody").html(tr);
            return true;
        }
        let datos = JSON.parse(data);

        for (var i = 0; i < datos.length; i++) {
            tr += `<tr>
                <td>
                        <input type="hidden" id="idproducto`+ datos[i].IdProveedor + `" value="` + IdArticulo + `" /> 
                        <input type="hidden" id="IdDetalleRq`+ datos[i].IdProveedor + `" value="` + IdDetalleRq + `" />
                        ` + datos[i].RazonSocial + `</td>
                <td> <input class="form-control" id="precionacional`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioNacional + `"/></td>
                <td> <input class="form-control" id="precioextranjero`+ datos[i].IdProveedor + `" value="` + datos[i].PrecioExtranjero + `"/></td>
                <td> <button class="btn btn-info" onclick="AsignarProveedorPrecio(`+ datos[i].IdProveedor + `)">ASIGNAR</button></td>
            </tr>`;
        }
        console.log(tr);
        $("#tabla_proveedor_aprobados tbody").html(tr);
    });
}

function AsignarProveedorPrecio(IdProveedor) {
    let precionacional = $("#precionacional" + IdProveedor).val();
    let precioextranjero = $("#precioextranjero" + IdProveedor).val();
    let idproducto = $("#idproducto" + IdProveedor).val();
    let IdDetalleRq = $("#IdDetalleRq" + IdProveedor).val();

    $.ajaxSetup({ async: false });
    $.post("ActualizarProveedorPrecio", { 'IdProveedor': IdProveedor, 'precionacional': precionacional, 'precioextranjero': precioextranjero, 'idproducto': idproducto, 'IdDetalleRq': IdDetalleRq }, function (data, status) {
        listarItemAprobados();
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
    $("#modalasignarproveedor").modal();
    ObtenerPrecioRQDetalle();
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedorAsignar", "Seleccione")
    });
}

function ObtenerPrecioRQDetalle() {
    let IdDetalleRq = $('input:radio[name=rdSeleccionado]:checked').val();
    $.post("/SolicitudRQ/ObtenerDatosRqDetallexID", { 'IdDetalleRq': IdDetalleRq }, function (data, status) {
        $("#PrecioAsignar").val(data)
   
    });
 
}

function GuardarAsignarProveedorProducto() {
    let ValidarPrecio = $("#PrecioAsignar").val();
    if (ValidarPrecio == "" || ValidarPrecio == null || ValidarPrecio == "0" || !$.isNumeric(ValidarPrecio)) {
        swal("Informacion!", "Debe Especificar Precio!");
        return;
    }

    let IdProveedor = $("#IdProveedorAsignar").val();
    let precionacional = $("#PrecioAsignar").val();
    let precioextranjero = 0;
    let idproducto = $('input:radio[name=rdSeleccionado]:checked').attr("idproductolinea");
    let IdDetalleRq = $('input:radio[name=rdSeleccionado]:checked').val();
    $.ajaxSetup({ async: false });
    $.post("ActualizarProveedorPrecio", { 'IdProveedor': IdProveedor, 'precionacional': precionacional, 'precioextranjero': precioextranjero, 'idproducto': idproducto, 'IdDetalleRq': IdDetalleRq }, function (data, status) {
        listarItemAprobados();
    });
    $("#modalasignarproveedor").modal('hide');
}


function CargarBasesObraAlmacenSegunAsignado() {
    let respuesta = 'false';
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerBaseAlmacenxIdUsuarioSession", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            console.log(datos[0]);
            contadorBase = datos[0].CountBase;
            contadorObra = datos[0].CountObra;
            contadorAlmacen = datos[0].CountAlmacen;
            AbrirModal("modal-form");
            CargarBase();
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen == 1) {
                $.ajaxSetup({ async: false });
                console.log(1)
                $("#IdBase").val(datos[0].IdBase).change();
                console.log(2)
                $("#IdObra").val(datos[0].IdObra).change();
                console.log(3)
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
            respuesta = 'false';
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
    $()
}

function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#IdAlmacen").prop('disabled', valorbolean);
    $("#IdSerie").prop('disabled', valorbolean);
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
