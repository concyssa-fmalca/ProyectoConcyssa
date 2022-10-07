let table = '';



window.onload = function () {
    var url = "ObtenerArticulosxSociedad";
    ConsultaServidor(url);
    listarUnidadMedida();
    listarCodigoUbso();
    listarGrupoArticulo();
    CargarGrupoUnidadMedida();
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
                    options += `<option value="` + datos[i].IdUnidadMedida +`">` + datos[i].Descripcion +`</option>`;
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

                '<td style="display:none">' + articulos[i].ActivoFijo + '</td>' +
                '<td style="display:none">' + articulos[i].IdCodigoUbso + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + articulos[i].IdArticulo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + articulos[i].IdArticulo + ')"></button></td >' +
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
    //CargarPerfiles();
    //CargarSociedades();
    //CargarGrupoUnidadMedida();

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
    $("#IdUnidadMedidaInv").val(29)
  
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
    if ($('input:radio[name=inlineRadioOptions]:checked').val()==1) {
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
        'IdUnidadMedidaInv': IdUnidadMedidaInv

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
    
    AbrirModal("modal-form");

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
            $("#idIdUnidadMedidaInv").val(articulos[0].IdUnidadMedidaInv);
   
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
                '<td><input type="number" class="form-control" id="StockMinimo' + datos[i].IdAlmacen + '" name="StockMinimo[]" value="' + datos[i].StockMinimo+'"/></td>' +
                '<td><input type="number" class="form-control" id="StockMaximo' + datos[i].IdAlmacen + '" name="StockMaximo[]" value="' + datos[i].StockMaximo +'"/></td>' +
                '<td><input type="number" class="form-control" id="StockAlerta' + datos[i].IdAlmacen + '" name="StockAlerta[]" value="' + datos[i].StockAlerta +'"/></td>' +
                '<td>' + datos[i].StockAlmacen+'</td>' +
                '<td><button type="btn btn-primary btn-xs" onclick="GuardarStock(' + IdProducto + ',' + datos[i].IdAlmacen + ')">Guardar</button></td>' +
                '</tr>';
        }

        $("#tbody_inventario").html(tr);



    });

}


function GuardarStock(IdProducto, IdAlmacen) {
    let StockMinimo = $("#StockMinimo" + IdAlmacen).val();
    let StockMaximo= $("#StockMaximo" + IdAlmacen).val();
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