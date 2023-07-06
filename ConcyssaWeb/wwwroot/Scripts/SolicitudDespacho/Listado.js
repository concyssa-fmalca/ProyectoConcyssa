
var table = '';
var tableStock = "";
var tablePrincipal = "";
window.onload = function () {
    //var url = "ObtenerUsuarios";
    //ConsultaServidor(url);
    //CargarEstadosGiro();
    ConsultaServidor();

    $(document).on('click', '.borrar', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });

};


function ConsultaServidor() {

    $.ajax({
        url: "/SolicitudDespacho/ObtenerSolicitudesDespacho",
        type: "POST",
        async: true,
        data: {
            
        },
        beforeSend: function () {

        },
        success: function (data) {

            if (data != "error") {
                var datos = JSON.parse(data);

                console.log(datos);

                var tr = '';

                for (var i = 0; i < datos.length; i++) {
                    tr += `<tr>
                            <td>`+ (i + 1) +`</td>
                            <td><button class="btn btn-primary btn-xs fa fa-eye" onclick="MostrarModal(this)"></button></td>
                            <td><input type="hidden" value="`+ datos[i].Numero+`" />`+ datos[i].Numero +`</td>
                            <td><input type="hidden" value="`+ datos[i].DescripcionCuadrilla +`" />`+ datos[i].DescripcionCuadrilla +`</td>
                            <td><input type="hidden" value="`+ datos[i].DescripcionObra +`" />`+ datos[i].DescripcionObra +`</td>
                            <td><input type="hidden" value="`+ datos[i].DescripcionBase +`" />`+ datos[i].DescripcionBase + `</td>
                            <td><input type="hidden" value="`+ datos[i].FechaDocumento.split("T")[0] +`" />`+ datos[i].FechaDocumento.split("T")[0] +`</td>
                            <td><button class="btn btn-primary btn-xs fa fa-edit" onclick="ObtenerDatosxID(`+ datos[i].Id+`)"></button></td>
                            </tr>`;
                }

                $("#tbody_Solicitudes").html(tr);
                table = $("#table_id").DataTable(lenguaje);

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


function ModalNuevo() {

    //$("#ModalFormulario").modal("show");
    AbrirModal("modal-form");

    $("#cboSerie").prop("disabled", false);
    CargarSeries();

    $("#txtId").val("0");

    $("#cboClaseArticulo").prop('disabled', false);
    $("#IdTipoProducto").prop('disabled', false);

    $("#lblTituloModal").html("Nuevo");
    $("#IdCuadrilla").select2();

    CargarCuadrillaxUsuario();
    $("#IdCuadrilla").val("0").change();

    
}

function CerrarModal() {

    $.magnificPopup.close();
    $("#tabla").find('tbody').empty();
    limpiarDatos();

}

function CargarCuadrillaxUsuario() {
    $.ajaxSetup({ async: false });
    $.post("/SolicitudDespacho/ObtenerCuadrillaxUsuario", function (data, status) {

        var datos = JSON.parse(data);
        console.log(datos);
        //let sociedades = JSON.parse(data);
        llenarComboCuadrilla(datos, "IdCuadrilla", "Seleccione")

    });

}

function llenarComboCuadrilla(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0' selected>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].DescripcionCuadrilla.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0' >" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option selected value='" + lista[i].IdObra + "'>" + lista[i].DescripcionObra.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboBase(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0' >" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option selected value='" + lista[i].IdBase + "'>" + lista[i].DescripcionBase.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function CargarObraBasexUsuario() {

    var IdCuadrilla = $("#IdCuadrilla").val();

    $.ajaxSetup({ async: false });
    $.post("/SolicitudDespacho/ObtenerObraBasexCuadrilla", { 'IdCuadrilla':IdCuadrilla }, function (data, status) {

        var datos = JSON.parse(data);
        console.log(datos);

        llenarComboObra(datos, "IdObra", "Seleccione");
        llenarComboBase(datos, "IdBase", "Seleccione");
        //let sociedades = JSON.parse(data);
        //llenarComboCuadrilla(datos, "IdCuadrilla", "Seleccione")

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






function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let almacen = JSON.parse(data);
        //llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
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



function BuscarListadoAlmacen() {

    let IdObra = $("#IdObra").val();

    $("#ModalListadoAlmacen").modal();

    $.post("../Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Almacen")
        } else {
            let almacen = JSON.parse(data);
            console.log(almacen);
            let tr = '';

            for (var i = 0; i < almacen.length; i++) {

                tr += '<tr>' +
                    '<td><input type="radio" clase="" id="rdSeleccionadoAlmacen' + almacen[i].IdAlmacen + '"  name="rdSeleccionadoAlmacen"  value = "' + almacen[i].IdAlmacen + '" ></td>' +
                    '<td>' + almacen[i].Codigo + '</td>' +
                    '<td>' + almacen[i].Descripcion + '</td>' +
                    '</tr>';
            }

            $("#tbody_listado_almacen").html(tr);

            tableAlmacen = $("#tabla_listado_almacen").DataTable({
                info: false, "language": {
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "lengthMenu": "Mostrar _MENU_ registros"
                }
            });
        }

    });

}

function CerrarModalListadoAlmacen() {
    tableAlmacen.destroy();
}

function SeleccionarAlmacenListado() {

    let IdAlmacen = $('input:radio[name=rdSeleccionadoAlmacen]:checked').val();

    $.post("/Almacen/ObtenerDatosxID", { 'IdAlmacen': IdAlmacen }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Almacen")
            tableAlmacen.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboAlmacenItem").val(datos[0].IdAlmacen);
            tableAlmacen.destroy();

        }
    });
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

    $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, }, function (data, status) {

        if (data == "error") {
            swal("Informacion!", "No se encontro Articulo")
        } else {
            let items = JSON.parse(data);
            console.log(items);
            let tr = '';

            for (var i = 0; i < items.length; i++) {
                /*if (items[i].Inventario == TipoItem) {*/
                if (items[i].Stock > 0) {
                    tr += '<tr>' +
                        '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                        '<td>' + items[i].Codigo + '</td>' +
                        '<td>' + items[i].Descripcion1 + '</td>' +
                        '<td>' + items[i].Stock + '</td>' +
                        '<td>' + items[i].UnidadMedida + '</td>' +
                        '</tr>';
                }
            }


            $("#tbody_listado_items").html(tr);

            tableItems = $("#tabla_listado_items").DataTable({
                info: false, "language": {
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "lengthMenu": "Mostrar _MENU_ registros"
                }
            });
        }

    });

}



function OpenModalItem() {
    if ($("#IdTipoProducto").val() == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
        return;
    }

    let IdBase = $("#IdBase").val();
    if (!IdBase) {
        swal("Informacion!", "Debe Seleccionar Base!");
        return;
    }

    CargarAlmacen();

    $("#cboAlmacenItem").prop("selectedIndex", 1)

    //Cuando se abre agregar Item
    let ClaseArticulo = $("#cboClaseArticulo").val();
   
    if (ClaseArticulo == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
        return;
    }
    else {
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarGrupoUnidadMedida();
    }
}



function SeleccionarItemListado() {

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();


    $.post("/Articulo/ObtenerArticuloxIdArticuloRequerimiento", { 'IdArticulo': IdArticulo, 'TipoItem': TipoItem, 'IdAlmacen': Almacen }, function (data, status) {


        if (data == "error") {
            swal("Info!", "No se encontro Articulo")
            tableItems.destroy();
        } else {
            let datos = JSON.parse(data);
            //console.log(datos);

            $("#txtCodigoItem").val(datos.Codigo);
            $("#txtIdItem").val(datos.IdArticulo);
            $("#txtDescripcionItem").val(datos.Descripcion1);
            $("#cboMedidaItem").val(datos.IdUnidadMedida);
            $("#txtStockAlmacenItem").val(datos.Stock);


            tableItems.destroy();

        }
    });

    //TablaItemsDestruida = 1;
    //ObtenerItemsPendientes(IdArticulo)
    ListarStockTodasObras(IdArticulo)

}


function ListarStockTodasObras(IdArticulo) {
    $.post("/Articulo/ObtenerArticulosConStockObras", { 'IdArticulo': IdArticulo }, function (data, status) {

        //console.log(data);
        if (data == "error") {
            tableStock = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let area = JSON.parse(data);

        for (var i = 0; i < area.length; i++) {


            tr += '<tr>' +
                '<td>' + area[i].Descripcion1 + '</td>' +
                '<td>' + area[i].Obra + '</td>' +
                '<td>' + area[i].Almacen + '</td>' +
                '<td>' + area[i].Stock + '</td>' +
                '</tr>';
        }
        if (tableStock) {
            tableStock.destroy();
        }

        $("#tbody_stockOtrosAlmacenes").html(tr);

        tableStock = $("#tablaStockOtrosAlmacenes").DataTable(lenguaje);

    });


}



function CerrarModalListadoItems() {
    tableItems.destroy();
}


let contador = 0;

function AgregarLinea() {
    let stockproducto = $("#txtStockAlmacenItem").val();
    let IdItem = $("#txtIdItem").val();
    let CodigoItem = $("#txtCodigoItem").val();
    let MedidaItem = $("#cboMedidaItem").val();
    let MedidaItemDescripcion = $("#cboMedidaItem").find('option:selected').text();
    let DescripcionItem = $("#txtDescripcionItem").val();
    //let PrecioUnitarioItem = $("#txtPrecioUnitarioItem").val();
    let CantidadItem = $("#txtCantidadItem").val();
    let AlmacenItem = $("#cboAlmacenItem").val();
    let IdGrupoUnidadMedida = $("#cboGrupoUnidadMedida").val();
    //txtReferenciaItem

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;


    let UnidadMedida;
    let Almacen;

    //valdiaciones

    let ValidartProducto = $("#cboMedidaItem").val();
    let ValidarCantidad = $("#txtCantidadItem").val();

    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0" || !$.isNumeric(ValidarCantidad)) {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }

    if (ValidartProducto == 0) {
        swal("Informacion!", "Debe Seleccionar Producto!");
        return;
    }



    //validaciones
    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("../Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    contador++;
    let tr = '';

    tr += `<tr  id="tritem` + contador + `">
  
            <td></td>
            <td><button class="btn btn-primary btn-xs fa fa-edit" onclick="MostrarModalDetalle(this)"></button></td>
            <td>`+ CodigoItem + `</td>

            <td style="display:none;">
                <input  input style="display:none;" class="form-control omitir" type="text" value="0" id="txtIdSolicitudDespachoDetalle" name="txtIdSolicitudDespachoDetalle[]"/>
                <input class="form-control omitir" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
                <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
         
            <td><input disabled class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
            <input type="hidden" value="" id="inputUnidadMedida`+ contador + `" />
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Codigo + `</option>`;
    }
    tr += `</select>
            </td>
          
            <td>
                <input class="form-control"  type="number" name="txtCantidad[]" value="0" id="txtCantidad`+ contador + `" disabled>
            </td>
          
          <td><button class="btn btn-xs btn-danger borrar fa fa-trash"></button></td>
          </tr>`;

    $("#tabla").find('tbody').append(tr);


    $("#txtIdArticulo" + contador).val(IdItem);
    $("#txtCodigoArticulo" + contador).val(CodigoItem);
    $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
    $("#txtCantidad" + contador).val(CantidadItem);
    $("#cboUnidadMedida" + contador).val(MedidaItem);
    $("#inputUnidadMedida" + contador).val(MedidaItemDescripcion);
    

    $("#cboAlmacen" + contador).val(AlmacenItem);
    //$("#cboPrioridadDetalle" + contador).val(PrioridadItem);


    //ObtenerCuadrillasTabla()
    LimpiarModalItem();
    NumeracionDinamica();
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

function validarStock() {
    let stockalmacen = $("#txtStockAlmacenItem").val();
    let txtCantidadItem = $("#txtCantidadItem").val();
    if (parseFloat(stockalmacen) < parseFloat(txtCantidadItem)) {
        swal("Informacion!", "La Cantidad Supera al stock" + stockalmacen);
        $("#txtCantidadItem").val(stockalmacen);
    }
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

function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
    });
}


function LimpiarModalItem() {

    $("#txtCodigoItem").val("");
    $("#txtDescripcionItem").val("");
    $("#txtStockAlmacenItem").val("");
    $("#txtPrecioUnitarioItem").val("");
    $("#txtCantidadItem").val("");
    $("#cboMedidaItem").val(0);

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


function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}




function GuardarSolicitud() {

    let Id = $("#txtId").val(); 
    let IdCuadrilla = $("#IdCuadrilla").val();
    let IdClaseProducto = $("#cboClaseArticulo").val();
    let IdTipoProducto = $("#IdTipoProducto").val();
    let IdObra = $("#IdObra").val();
    let IdBase = $("#IdBase").val();
    let IdSerie = $("#cboSerie").val();
    let Serie = $("#cboSerie").find('option:selected').text();
    let Numero = 0;
    let FechaDocumento = $("#txtFechaDocumento").val();
    let FechaContabilizacion = $("#txtFechaContabilizacion").val();
    let Comentario = $("#txtComentarios").val();

    let ArrayGeneral = new Array();
    let ArrayId = new Array();
    let ArrayIdItem = new Array();
    let ArrayDescripcion = new Array();
    let ArrayIdUnidadMedida = new Array();
    let ArrayCantidad = new Array();

    $("input[name='txtIdSolicitudDespachoDetalle[]']").each(function (indice, elemento) {
        ArrayId.push($(elemento).val());
    });
    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        ArrayIdItem.push($(elemento).val());
    });
    $("input[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
        ArrayDescripcion.push($(elemento).val());
    });
    $("select[name='cboUnidadMedida[]']").each(function (indice, elemento) {
        ArrayIdUnidadMedida.push($(elemento).val());
    });
    $("input[name='txtCantidad[]']").each(function (indice, elemento) {
        ArrayCantidad.push($(elemento).val());
    });


    for (var i = 0; i < ArrayId.length; i++) {
        ArrayGeneral.push({
            'Id': ArrayId[i],
            'IdItem': ArrayIdItem[i],
            'Descripcion': ArrayDescripcion[i],
            'IdUnidadMedida': ArrayIdUnidadMedida[i],
            'Cantidad': ArrayCantidad[i]
        });
    }



    $.ajax({
        url: "UpdateInsertSolicitudDespacho",
        type: "POST",
        async: true,
        data: {
            'Id': Id,
            'IdSerie': IdSerie,
            'Serie': Serie,
            'Numero': Numero,
            'IdClaseProducto': IdClaseProducto,
            'IdTipoProducto': IdTipoProducto,
            'IdCuadrilla': IdCuadrilla,
            'IdObra': IdObra,
            'IdBase': IdBase,
            'FechaDocumento': FechaDocumento,
            'FechaContabilizacion': FechaContabilizacion,
            'Comentario': Comentario,
            'Detalle': ArrayGeneral
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

            console.log(data);


            if (data == "1") {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )

                //table.destroy();
                CerrarModal();
                ConsultaServidor();
                
                //ObtenerDatosxID();
                $("#tabla").find('tbody').empty();

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

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



function ObtenerDatosxID(IdSolicitudDespacho) {


    AbrirModal("modal-form");

    $("#cboClaseArticulo").prop('disabled', true);
    $("#IdTipoProducto").prop('disabled', true);

    $("#lblTituloModal").html("Editar Solicitud");
    //let seguiradelante = 'false';
    //seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    //if (seguiradelante == 'false') {
    //    swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
    //    return true;
    //}


    $.post('ObtenerDatosxID', {
        'IdSolicitudDespacho': IdSolicitudDespacho,
    }, function (data, status) {



        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let solicitudes = JSON.parse(data);
            console.log(solicitudes);

            CargarSeries();

            $("#cboSerie").prop("disabled", true);
            $("#cboSerie").val(solicitudes[0].IdSerie);

            $("#IdCuadrilla").select2();
            CargarCuadrillaxUsuario();

            $("#IdCuadrilla").val(solicitudes[0].IdCuadrilla).change();

            var fechaSplit = (solicitudes[0].FechaDocumento.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            $("#txtFechaDocumento").val(fecha);

            var fechaSplit1 = (solicitudes[0].FechaContabilizacion.substring(0, 10)).split("-");
            var fecha1 = fechaSplit1[0] + "-" + fechaSplit1[1] + "-" + fechaSplit1[2];
            $("#txtFechaContabilizacion").val(fecha1);

            $("#txtComentarios").val(solicitudes[0].Comentario);
            $("#txtId").val(solicitudes[0].Id);

            $("#cboClaseArticulo").val(solicitudes[0].IdClaseProducto);
            $("#IdTipoProducto").val(solicitudes[0].IdTipoProducto);
            //CargarSeries();
            //CargarMoneda();
            //CargarImpuestos();

            //$("#txtId").val(solicitudes[0].IdSolicitudRQ);
            //$("#cboSerie").val(solicitudes[0].IdSerie);
            //$("#txtNumeracion").val(solicitudes[0].Numero);
            //$("#cboMoneda").val(solicitudes[0].IdMoneda);
            //$("#txtTipoCambio").val(solicitudes[0].TipoCambio);
            //$("#cboClaseArticulo").val(solicitudes[0].IdClaseArticulo);
            //$("#cboEmpleado").val(solicitudes[0].IdSolicitante);
            //$("#cboPrioridad").val(solicitudes[0].Prioridad);
            //$("#txtEstado").val(solicitudes[0].Estado);
            let Detalle = solicitudes[0].Detalles;

            console.log(solicitudes);

            for (var i = 0; i < Detalle.length; i++) {

                AgregarLineaDetalle(Detalle[i].Id,
                    Detalle[i].CodigoArticulo,
                    Detalle[i].IdItem,
                    Detalle[i].Descripcion,
                    Detalle[i].Cantidad,
                    Detalle[i].IdUnidadMedida,
                    Detalle[i].IdGrupoUnidadMedida,
                    Detalle[i].IdDefinicionGrupoUnidad
                    );

                contador++;
            }


            //AnxoDetalle
            //let AnexoDetalle = solicitudes[0].AnexoDetalle;
            //let trAnexo = '';
            //for (var k = 0; k < AnexoDetalle.length; k++) {
            //    trAnexo += `
            //    <tr>
                   
            //        <td>
            //           `+ AnexoDetalle[k].NombreArchivo + `
            //        </td>
            //        <td>
            //           <a target="_blank" href="`+ AnexoDetalle[k].ruta + `"> Descargar </a>
            //        </td>
            //        <td><button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)">-</button></td>
            //    </tr>`;
            //}
            //$("#tabla_files").find('tbody').append(trAnexo);


        }

    });

}



function AgregarLineaDetalle(Id, CodigoArticulo, IdItem, Descripcion, Cantidad, IdUnidadMedida, IdGrupoUnidadMedida, IdDefinicionGrupoUnidad) {


    let UnidadMedida;
    let Almacen;


    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        Almacen = JSON.parse(data);
    });


    let UnidadMed = "";
    let tr = '';

    tr += `<tr  id="tr` + contador + `">
  
            <td></td>
           <td><button class="btn btn-primary btn-xs fa fa-edit" onclick="MostrarModalDetalleEditar(this)"></button></td>
            <td>`+ CodigoArticulo + `</td>

            <td style="display:none;">
                <input input style="display:none;" class="form-control omitir" type="text" value="`+Id+`" id="txtIdSolicitudDespachoDetalle" name="txtIdSolicitudDespachoDetalle[]"/>
                <input class="form-control omitir" type="text" value="`+ IdItem +`" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
                <input class="form-control" type="text" value="`+ CodigoArticulo +`" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
         
            <td><input disabled class="form-control" type="text" value="`+ Descripcion +`" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
            <td>
             <input type="hidden" value="" id="inputUnidadMedida`+ contador + `" />
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
            tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
                    UnidadMed = UnidadMedida[i].Codigo;
                    tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `" selected>` + UnidadMedida[i].Codigo + `</option>`;
                } else {
                    tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Codigo + `</option>`;
                }
            }
    tr += `</select>
            </td>
          
            <td>
                <input class="form-control"  type="number" name="txtCantidad[]" value="`+Cantidad+`" id="txtCantidad`+ contador + `">
            </td>
          
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarDetalle(`+ Id +`,this)"></button></td>
          </tr>`;


    $("#tabla").find('tbody').append(tr);

    $("#inputUnidadMedida" + contador).val(UnidadMed);

    NumeracionDinamica();
    //$("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);


}

function borrartr(id) {
    $("#" + id).remove();
}


function EliminarDetalle(IdSolicitudDespachoDetalle, dato) {
    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {
        $.post("EliminarDetalleSolicitud", { 'IdSolicitudDespachoDetalle': IdSolicitudDespachoDetalle }, function (data, status) {
            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });

}


function limpiarDatos() {

    let date = new Date();
    let mes = date.getMonth() + 1
    let fechaReset = date.getFullYear() +

        "-" + (mes > 9 ? mes : "0" + mes) +
        "-" + ((date.getDate()) > 9 ? date.getDate() : "0" + date.getDate()) 

    
    $("#IdObra").val("0");
    $("#IdBase").val("0");
    $("#txtComentarios").val("");
    $("#txtFechaDocumento").val(fechaReset);
    $("#txtFechaContabilizacion").val(fechaReset);
    $("#IdTipoProducto").val("0");

    
}


function MostrarModal(boton) {

    $("#ModalListadoDetalle").modal("show");

    var fila = boton.parentNode.parentNode;
    var inputs = fila.getElementsByTagName('input');

    var descripcion = "";
    // Crear el contenido del modal
    var modalContenido = '';


    for (var i = 0; i < inputs.length; i++) {

        if (i == 0) {
            descripcion = "NUMERO";
        } else if (i == 1) {
            descripcion = "CUADRILLA";
        } else if (i == 2) {
            descripcion = "OBRA";
        } else if (i == 3) {
            descripcion = "BASE";
        } else if (i == 4) {
            descripcion = "FECHA";
        }

        modalContenido += `<tr>
                            <td>`+ descripcion + `</td>
                            <td>`+ inputs[i].value + `</td>
                            </tr>`;


    }

    $("#tbody_listado_detalle").html(modalContenido);

}


function MostrarModalDetalle(boton) {

    $("#ModalListadoDetalle").modal("show");

    var fila = boton.parentNode.parentNode;
    var inputs = fila.getElementsByTagName('input');

    var descripcion = "";
    // Crear el contenido del modal
    var modalContenido = '';


    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].classList.contains('omitir')) {
            continue; // Ignorar el input y pasar al siguiente
        }


        if (i == 2) {
            descripcion = "CODIGO";
        } else if (i == 3) {
            descripcion = "DESCRIPCION";
        } else if (i == 4) {
            descripcion = "UM";
        } else if (i == 5) {
            descripcion = "CANTIDAD";
        }

        modalContenido += `<tr>
                            <td>`+ descripcion + `</td>
                            <td>`+ inputs[i].value + `</td>
                            </tr>`;


    }

    $("#tbody_listado_detalle").html(modalContenido);

}


function MostrarModalDetalleEditar(boton) {

    $("#ModalListadoDetalle").modal("show");

    var fila = boton.parentNode.parentNode;
    var inputs = fila.getElementsByTagName('input');

    var descripcion = "";
    // Crear el contenido del modal
    var modalContenido = '';

    console.log(inputs);

    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].classList.contains('omitir')) {
            continue; // Ignorar el input y pasar al siguiente
        }


        if (i == 2) {
            descripcion = "CODIGO";
        } else if (i == 3) {
            descripcion = "DESCRIPCION";
        } else if (i == 4) {
            descripcion = "UM";
        } else if (i == 5) {
            descripcion = "CANTIDAD";
        }

        modalContenido += `<tr>
                            <td>`+ descripcion + `</td>
                            <td>`+ inputs[i].value + `</td>
                            </tr>`;


    }

    $("#tbody_listado_detalle").html(modalContenido);

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
        if (lista[i].Documento == 10 && lista[i].Estado == true) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();
}