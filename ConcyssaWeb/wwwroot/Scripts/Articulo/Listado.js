let table = '';

function ObtenerProveedores() {

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        let proveedor = JSON.parse(data);
        console.log(proveedor);
        let optionesselect = `<option value="0">SELECCIONE PROVEEDOR</option>`;
        for (var i = 0; i < proveedor.length; i++) {
            optionesselect += `<option value="` + proveedor[i].IdProveedor + `">` + proveedor[i].RazonSocial +`</option>`;

        }
        $("#txtProveedor").html(optionesselect);
    });
}


function addprecioproductoproveedor() {
    let varIdObra
    let IdArticulo=$("#txtId").val();
    let IdProveedor = $("#IdProveedor").val();
    let PrecioSoles = $("#PrecioSoles").val();
    let PrecioDolares = $("#PrecioDolares").val();
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
        swal("Informacion!", "Seleccione condicion de pago!");
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
        console.log(IdArticulo);
        listarPrecioProductoProveedor(IdArticulo);
        console.log(IdArticulo);
        LimpiarArticuloxProveedor()
    });
}

function LimpiarArticuloxProveedor() {
    $("#IdProveedor").val(0).change()
    $("#PrecioSoles").val("").change()
    $("#PrecioDolares").val("").change()
    $("#IdCondicionPagoProveedor").val(0).change()
    $("#numeroentrega").val("").change()
    $("#ObraArticulo").val(0).change()

}

function CargarCondicionPago() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarCondicionPago(condicionpago, "IdCondicionPagoProveedor", "Seleccione")
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



function CargarProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedor", "Seleccione")
        $("#IdProveedor").select2()
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

window.onload = function () {
    var url = "ObtenerArticulosxSociedad";
    ConsultaServidor(url);
    listarUnidadMedida();
    listarCodigoUbso();
    listarGrupoArticulo();
    CargarGrupoUnidadMedida();
    $("#cboIdCodigoUbso").select2();
    $("#divObraArticulo").hide()
    ObtenerObra()
  
};

function CargarGrupoUnidadMedida() {

    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerGrupoUnidadMedida", { 'estado': 1 }, function (data, status) {
        let grupounidad = JSON.parse(data);
        console.log(grupounidad);
        llenarComboGrupoUnidadMedida(grupounidad, "IdGrupoUnidadMedida", "Seleccione")
    });
}

function listarUnidadMedida() {
    $.ajax({
        url: "../UnidadMedida/ObtenerUnidadMedidasxEstado",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdUnidadMedida").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdUnidadMedida + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdUnidadMedida").html(options);
            }
        }
    });
}

function listarCodigoUbso() {
    $.ajax({
        url: "../CodigoUbso/ObtenerCodigoUbso",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdCodigoUbso").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdCodigoUbso + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdCodigoUbso").html(options);
            }
        }
    });
}

function listarGrupoArticulo() {
    $.ajax({
        url: "../GrupoArticulo/ObtenerGrupoArticulo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdGrupoArticulo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdGrupoArticulo + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdGrupoArticulo").html(options);
            }
        }
    });
}


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let articulos = JSON.parse(data);
        let total_articulos = articulos.length;

        for (var i = 0; i < articulos.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + articulos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + articulos[i].Descripcion1.toUpperCase() + '</td>' +
                '<td>' + articulos[i].Descripcion2.toUpperCase() + '</td>' +
                '<td>' + articulos[i].NombUnidadMedida.toUpperCase() + '</td>' +

                '<td>' +
             
                    `<div class="btn-group" role="group" aria-label="..." style="inline-size: max-content !important; ">
                        <button  style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-primary fa fa-pencil btn-xs" onclick = "ObtenerDatosxID(` + articulos[i].IdArticulo + `)" ></button > 
                        <button  style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + articulos[i].IdArticulo + `)"></button>
                    </div>
                </td>`+
                '</tr>';
        }

        $("#tbody_Articulos").html(tr);
        $("#spnTotalRegistros").html(total_articulos);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Articulo");
    $("#TablaAlmacenes").hide();


    AbrirModal("modal-form");
    ObtenerProveedores();
    CargarProveedor();
    CargarCondicionPago();
    //CargarPerfiles();
    //CargarSociedades();
    //CargarGrupoUnidadMedida();

    let IdPerfil = $("#ArtIdPerfil").val();
    //if (IdPerfil == "1014") {
    //    $('#chkArticulo').attr('disabled', true);
    //    $('#chkActivoFijo').attr('disabled', true);
    //} else {
    //    $('#chkArticulo').attr('disabled', false);
    //    $('#chkActivoFijo').attr('disabled', false);
    //}

}



function llenarComboGrupoUnidadMedida(lista, idCombo, primerItem) {
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


function CargarDefinicionxGrupo() {
    let IdGrupoUnidadMedida = $("#IdGrupoUnidadMedida").val();
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        let definicion = JSON.parse(data);
        llenarComboDefinicionGrupoUnidadItem(definicion, "IdUnidadMedidaInv", "Seleccione")
    });
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






function GuardarArticulo() {

    let varIdArticulo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion1 = $("#txtDescripcion1").val();
    let varDescripcion2 = $("#txtDescripcion2").val();
    let varIdUnidadMedida = $("#cboIdUnidadMedida").val();
    let IdCodigoUbso = $("#cboIdCodigoUbso").val();
    let varEstadoActivoFijo = false;
    let varEstadoActivoCatalogo = false;


    let varEstadoInvetario = false;
    let varVenta = false;
    let varCompra = false;

    if ($('#chkActivoFijo')[0].checked) {
        varEstadoActivoFijo = true;
    }
    if ($('#chkActivoCatalogo')[0].checked) {
        varEstadoActivoCatalogo = true;
    }

    if ($('#chkArticulo')[0].checked) {
        varEstadoInvetario = true;
    }
    if ($('#chkArticuloVenta')[0].checked) {
        varVenta = true;
    }
    if ($('#chkArticuloCompra')[0].checked) {
        varCompra = true;
    }

    varEstado = false;
    if ($('input:radio[name=inlineRadioOptions]:checked').val() == 1) {
        varEstado = true;
    }

    let IdGrupoUnidadMedida = $("#IdGrupoUnidadMedida").val();
    let IdUnidadMedidaInv = $("#IdUnidadMedidaInv").val();

    $.post('UpdateInsertArticulo', {
        'IdArticulo': varIdArticulo,
        'Codigo': varCodigo,
        'Descripcion1': varDescripcion1,
        'Descripcion2': varDescripcion2,
        'IdUnidadMedida': varIdUnidadMedida,
        'IdCodigoUbso': IdCodigoUbso,
        'ActivoFijo': varEstadoActivoFijo,
        'ActivoCatalogo': varEstadoActivoCatalogo,
        'Inventario': varEstadoInvetario,
        'Venta': varVenta,
        'Compra': varCompra,
        'Estado': varEstado,
        'IdGrupoUnidadMedida': IdGrupoUnidadMedida,
        'IdUnidadMedidaInv': IdUnidadMedidaInv,
        'IdProveedor': $("#txtProveedor").val()


    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerArticulosxSociedad");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
            ConsultaServidor("ObtenerArticulosxSociedad");
        }

    });
}

function ObtenerDatosxID(varIdArticulo) {
    limpiarDatos();
    $("#lblTituloModal").html("Editar Articulo");
    ObtenerProveedores();
    AbrirModal("modal-form");


    let IdPerfil = $("#ArtIdPerfil").val();
    //if (IdPerfil == "1014") {
    //    $('#chkArticulo').attr('disabled', true);
    //    $('#chkActivoFijo').attr('disabled', true);
    //} else {
    //    $('#chkArticulo').attr('disabled', false);
    //    $('#chkActivoFijo').attr('disabled', false);
    //}

    CargarProveedor();
    CargarCondicionPago();
    listarPrecioProductoProveedor(varIdArticulo);

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdArticulo': varIdArticulo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let articulos = JSON.parse(data);
            console.log(articulos);

            $("#txtId").val(articulos[0].IdArticulo);
            $("#txtCodigo").val(articulos[0].Codigo);
            $("#txtDescripcion1").val(articulos[0].Descripcion1);
            $("#txtDescripcion2").val(articulos[0].Descripcion2);
            $("#cboIdUnidadMedida").val(articulos[0].IdUnidadMedida);
            $("#cboIdCodigoUbso").val(articulos[0].IdCodigoUbso);

            $("#IdGrupoUnidadMedida").val(articulos[0].IdGrupoUnidadMedida).change();
            //$("#idIdUnidadMedidaInv").val(articulos[0].IdUnidadMedidaInv);
            $("#txtProveedor").val(articulos[0].IdProveedor);
            if (articulos[0].ActivoFijo) {
                $("#chkActivoFijo").prop('checked', true);
            }

            if (articulos[0].ActivoCatalogo) {
                $("#chkActivoCatalogo").prop('checked', true);
            }

            if (articulos[0].Inventario) {
                $("#chkArticulo").prop('checked', true);
            }
            if (articulos[0].Compra) {
                $("#chkArticuloCompra").prop('checked', true);
            }
            if (articulos[0].Venta) {
                $("#chkArticuloVenta").prop('checked', true);
            }


            ObtenerAlmacenes();
            $("#TablaAlmacenes").show();
            console.log(articulos[0].IdUnidadMedidaInv)
            $("#IdUnidadMedidaInv").val(articulos[0].IdUnidadMedidaInv);
        }

    });

}

function eliminar(varIdArticulo) {


    alertify.confirm('Confirmar', '¿Desea eliminar este articulo?', function () {
        $.post("EliminarArticulo", { 'IdArticulo': varIdArticulo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Articulo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerArticulosxSociedad");
                limpiarDatos();
            }

        });

    }, function () { });

}

function listarPrecioProductoProveedor(IdArticulo) {
    $.post("/Articulo/ListarPrecioProductoProveedorNuevo", { 'IdArticulo': IdArticulo }, function (data, status) {
        $("#tbody_tabledetalle_precioproveedor").html();
        let tr = "";
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    let ObraA
                    let TipoPrecio
                    if (datos[i].Obra == 0) {
                        ObraA = "-"
                        TipoPrecio = "General"
                    } else {
                        ObraA = datos[i].Obra
                        TipoPrecio = "Por Obra"
                    }
                    console.log("aaaaaaaaaaaaaa")
                    tr += `<tr>
    
                        <td>`+ datos[i].Proveedor + `</td>
                        <td>`+ datos[i].PrecioSoles + `</td>
                        <td>`+ datos[i].PrecioDolares + `</td>
                        <td>`+ datos[i].CondicionPago + `</td>
                        <td>`+ datos[i].numeroentrega + `</td>
                        <td>`+ TipoPrecio + `</td>
                        <td>`+ ObraA + `</td>
                        <td> <button class="btn btn-sm btn-danger" onclick="elimiarproductoproveedor(`+ datos[i].IdArticuloProveedor + `,` + datos[i].IdArticulo + `)">-</button></td>
                    </tr>`;
                }

                $("#tbody_tabledetalle_precioproveedor").html(tr);
            }
        }
      
    });
}
function cerrarModalArticuloProveedor() {
    $("#tbody_tabledetalle_precioproveedor").empty();
}

function elimiarproductoproveedor(IdProductoProveedor,IdArticulo) {
    $.post("/Articulo/EliminarProductoProveedor", { 'IdProductoProveedor': IdProductoProveedor }, function (data, status) {
        if (data == 0) {
            swal("Error!", "Ocurrio un Error")

        } else {
            swal("Exito!", "Articulo Eliminado", "success")
      

        }

    })
    listarPrecioProductoProveedor(IdArticulo);
}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion1").val("");
    $("#txtDescripcion2").val("");
    $("#chkActivo").prop('checked', false);
    $("#chkArticulo").prop('checked', false);
    $("#chkArticuloCompra").prop('checked', false);
    $("#chkArticuloVenta").prop('checked', false);
    $("#chkActivoFijo").prop('checked', false);
    $("#chkActivoCatalogo").prop('checked', false);
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function ObtenerAlmacenes() {

    let IdProducto = $("#txtId").val();
    console.log(IdProducto);


    $.post("/Articulo/ObtenerStockArticuloXAlmacen", { 'IdArticulo': IdProducto }, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let datos = JSON.parse(data);


        console.log(datos);

        for (var i = 0; i < datos.length; i++) {

            tr += '<tr style="font-size: 12px;font-weight: 400;color: #626262;outline: none;">' +
                '<td style="font-size: 12px;font-weight: 400;color: #626262;outline: none;">' + (i + 1) + '</td>' +
                '<td style="font-size: 12px;font-weight: 400;color: #626262;outline: none;">' + datos[i].Descripcion.toUpperCase() + '</td>' +
                '<td><input type="number" class="form-control" id="StockMinimo' + datos[i].IdAlmacen + '" name="StockMinimo[]" value="' + datos[i].StockMinimo + '"/></td>' +
                '<td><input type="number" class="form-control" id="StockMaximo' + datos[i].IdAlmacen + '" name="StockMaximo[]" value="' + datos[i].StockMaximo + '"/></td>' +
                '<td><input type="number" class="form-control" id="StockAlerta' + datos[i].IdAlmacen + '" name="StockAlerta[]" value="' + datos[i].StockAlerta + '"/></td>' +
                '<td>' + datos[i].StockAlmacen + '</td>' +
                '<td><button type="btn btn-primary btn-xs" onclick="GuardarStock(' + IdProducto + ',' + datos[i].IdAlmacen + ')">Guardar</button></td>' +
                '</tr>';
        }

        $("#tbody_inventario").html(tr);



    });

}


function GuardarStock(IdProducto, IdAlmacen) {
    let StockMinimo = $("#StockMinimo" + IdAlmacen).val();
    let StockMaximo = $("#StockMaximo" + IdAlmacen).val();
    let StockAlerta = $("#StockAlerta" + IdAlmacen).val();
    $.post("InsertStockArticuloAlmacen", { 'IdProducto': IdProducto, 'IdAlmacen': IdAlmacen, 'StockMinimo': StockMinimo, 'StockMaximo': StockMaximo, 'StockAlerta': StockAlerta },
        function (data) {

            if (data == "1") {
                swal("Exito!", "Articulo Eliminado", "success")
            } else {
                swal("Error!", "Ocurrio un Error")
            }


        })

    
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
        llenarComboObra(obra, "ObraArticulo", "Seleccione")
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

