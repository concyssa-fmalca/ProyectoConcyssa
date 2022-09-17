let table = '';
let tablepedido = '';

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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdMoneda + "'>" + lista[i].Descripcion + "</option>"; }
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

                        return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdAlmacen + `)"></button>
                            <button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + full.IdAlmacen + `)"></button>`
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
                        return 3
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
                        return 0
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
    $.post("/TipoPedido/ObtenerTipoPedido", {'estado': 1}, function (data, status) {
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
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdBase", { 'IdBase': IdBase }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "IdObra", "Seleccione")
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
}


window.onload = function () {
    listarPedidoDt();
};


function ModalNuevo() {
    $("#div_tabla_listadorequerimiento_items").hide();
    $("#div_tabla_listado_items").show();
    $("#btnVerItemModal2").hide();
    $("#btnVerItemModal1").show();
    ListarBasesxUsuario();
    CargarBase()
    CargarPedido();
    CargarSeries();
    CargarProveedor();
    CargarMoneda();
    CargarCondicionPago();
    $("#lblTituloModal").html("Pedido");
    AbrirModal("modal-form");
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
        else { }
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
    let IdProveedor=$("#IdProveedor").val();
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
            $("#cboGrupoUnidadMedida").val(0).change();
            $("#txtCodigoItem").val(datos[0].Codigo);
            $("#txtIdItem").val(datos[0].IdArticulo);
            $("#txtDescripcionItem").val(datos[0].Descripcion1);
            $("#cboMedidaItem").val(datos[0].IdUnidadMedida);
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
    console.log(ValidarCantidad);
    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0") {
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
                'total_item': arrayTotal[i]
            })
        }

    }
    console.log(detalles);
    $.ajax({
        url: "UpdateInsertPedido",
        type: "POST",
        async: true,
        data: {
            detalles,
            //cabecera
            'IdAlmacen': IdAlmacen,
            'IdSerie': IdSerie,
            'IdProveedor': $("#IdProveedor").val(),
            'Direccion': $("#Direccion").val(),
            'Telefono': $("#Telefono").val(),
            'FechaEntrega': $("#FechaEntrega").val(),
            'FechaContabilizacion': $("#txtFechaContabilizacion").val(),
            'FechaDocumento': $("#txtFechaDocumento").val(),
            'IdTipoPedido': $("#IdTipoPedido").val(),
            'LugarEntrega': $("#LugarEntrefa").val(),
            'IdCondicionPago': $("#IdCondicionPago").val(),
            'ElaboradoPor': 1,
            'IdMoneda': $("#cboMoneda").val(),
            'Observacion': $("#txtComentarios").val(),
            'TipoCambio': 1,
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