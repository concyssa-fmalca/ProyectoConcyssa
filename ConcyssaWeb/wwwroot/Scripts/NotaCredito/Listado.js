let tablepedido = "";
let tableentrega;
let tablenotacredito;
let contarclick = 0;
var ultimaFila = null;
let tablesalida;
let tablefacturaproveedor;
var colorOriginal;
function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}
function getDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}

function ListarBasesxUsuario() {
    $.post("/Usuario/ObtenerBasesxIdUsuario", function (data, status) {
        let datos = JSON.parse(data);
        $("#IdBase").val(datos[0].IdBase).change();
        if (datos.length == 1) {

            if (datos[0].IdPerfil != 1) {
                $("#IdBase").prop("disabled", false);
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdBase").prop("disabled", true);
            }
        }
    });
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
function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboBaseFiltro", "seleccione")

    });

}


function llenarComboBaseFiltro(lista, idCombo, primerItem) {
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
    $("#cboBaseFiltro").prop("selectedIndex", 1);
    listarOrpc();
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





/*USANDO */
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

            let proveedores = JSON.parse(data);

            $("#Direccion").val(proveedores[0].DireccionFiscal);
            $("#Telefono").val(proveedores[0].Telefono);



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
    $("#IdCondicionPago").val(1)
    $("#IdCondicionPago").prop("disabled",true)
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
    $("#IdTipoDocumentoRef").val(13)
    $("#IdTipoDocumentoRef").prop("disabled",true)
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

function ObtenerCuadrillas() {
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrilla", { 'estado': 1 }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrilla(cuadrilla, "IdCuadrilla", "Seleccione")
    });
}
function ObtenerCuadrillasxIdObra() {
    let IdObraCuadrilla = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObraCuadrilla }, function (data, status) {
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


//function CargarAlmacen() {
//    $.ajaxSetup({ async: false });
//    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
//        let almacen = JSON.parse(data);
//        llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
//        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")

//    });
//}

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
/*END USANDO */






window.onload = function () {
    getDecimales();
    $("#DivGiro").hide()
    CargarBaseFiltro()
    var url = "../Movimientos/ObtenerMovimientosIngresos";
    $("#cboCentroCosto").val(7)
    $("#IdResponsable").select2();
    $("#IdCuadrilla").select2();
    ObtenerConfiguracionDecimales();
    
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
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
};






function ConsultaServidor(url) {
    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let movimientos = JSON.parse(data);
        let total_solicitudes = movimientos.length;


        for (var i = 0; i < movimientos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + movimientos[i].FechaDocumento.split('T')[0] + '</td>' +
                '<td>' + movimientos[i].NombTipoDocumentoOperacion.toUpperCase() + '</td>' +
                '<td>' + movimientos[i].NombSerie.toUpperCase() + '-' + movimientos[i].Correlativo + '</td>' +
                '<td>' + formatNumber(movimientos[i].Total) + '</td>' +
                '<td>' + movimientos[i].DescCuadrilla + '</td>' +

                '<td>' + movimientos[i].NombObra + '</td>' +
                '<td>' + movimientos[i].NombAlmacen + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {

    $("#cboGlosaContable").prop("disabled", false)
    $("#IdTipoRegistro").prop("disabled", false)
    $("#IdSemana").prop("disabled", false)
    console.log("ref: " + $("#IdTipoDocumentoRef").val())
    $("#IdPedido").val(0);
    $("#IdOPDN").val(0);
    disabledmodal(false)
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }



    $("#lblTituloModal").html("Nueva Nota Credito Proveedor");
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    //CargarAlmacen()

    CargarTipoDocumentoOperacion()
    ObtenerCuadrillasxIdObra()
    CargarSeries();


    //AgregarLinea();




    CargarSolicitante(1);
    //CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();
    CargarGlosaContable();
    CargarImpuestos();
    $("#cboImpuesto").val(2).change();
    //$("#cboSerie").val(1).change();

    $("#cboMoneda").val(1);
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#total_items").html("");
    $("#NombUsuario").html("");
    $("#CreatedAt").html("");
    $("#SerieNumeroRef").val("");
    AbrirModal("modal-form");
    CargarProveedor();
    CargarCondicionPago();
    CargarTipoRegistro()
    CargarSemana()
    //setearValor_ComboRenderizado("cboCodigoArticulo");
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


function OpenModalItem() {
        //if ($("#IdTipoProducto").val() == 0) {
        //    swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
        //    return;
        //}
        //Cuando se abre agregar Item
        let ClaseArticulo = $("#cboClaseArticulo").val();

        let Moneda = $("#cboMoneda").val();
        if (ClaseArticulo == 0) {
            swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
        } else if (Moneda == 0) {
            swal("Informacion!", "Debe Seleccionar Moneda!");
        }
        else {
            if (ClaseArticulo == 2) { //servicio
                $("#BtnBuscarListadoAlmacen").prop("disabled", false);
                $("#BtnBuscarCodigoProducto").prop("disabled", false);
                $("#txtDescripcionItem").prop("disabled", true);

                $("#IdTipoProducto").hide();
                $("#IdTipoProducto").val(1);

                $("#txtStockAlmacenItem").hide();
                $("#lblStockItem").hide();
            } else if (ClaseArticulo == 3) { //activo
                $("#BtnBuscarListadoAlmacen").prop("disabled", false);
                $("#BtnBuscarCodigoProducto").prop("disabled", false);
                $("#txtDescripcionItem").prop("disabled", true);

                $("#IdTipoProducto").show();
                //$("#IdTipoProducto").val(0);

                $("#txtStockAlmacenItem").show();
                $("#lblStockItem").show();
            } else {//Producto
                $("#txtDescripcionItem").prop("disabled", true);
                $("#BtnBuscarListadoAlmacen").prop("disabled", false);
                $("#BtnBuscarCodigoProducto").prop("disabled", false);
                $("#IdTipoProducto").show();
                //$("#IdTipoProducto").val(0);

                $("#txtStockAlmacenItem").show();
                $("#lblStockItem").show();
            }
            $("#cboAlmacenItem").val($("#cboAlmacen").val()); // que salga el almacen por defecto


            $("#cboPrioridadItem").val(2);

            $("#ModalItem").modal();
            CargarUnidadMedidaItem();
            CargarGrupoUnidadMedida();
            CargarIndicadorImpuesto()
            /* CargarProyectos();*/
            //CargarCentroCostos();
            //CargarAlmacen();
            $('#checkstock').on('click', function () {
                if ($(this).is(':checked')) {
                    console.log("checked");
                    //tableItems.destroy();
                    //tablaItems.clear().draw();

                    TablaItemsDestruida = 1;
                    BuscarCodigoProducto(1);
                } else {
                    console.log("no checked");
                    //tableItems.destroy();
                    //tablaItems.clear().draw();

                    TablaItemsDestruida = 1;
                    BuscarCodigoProducto(0);
                }
            });


            if (ClaseArticulo == 3) {
                $("#txtCodigoItem").val("ACF00000000000000000");
                $("#txtIdItem").val("ACF00000000000000000");

                $("#txtPrecioUnitarioItem").val(1);
                $("#txtStockAlmacenItem").val("0");
                //$("#cboMedidaItem").val("11"); //11 titan
                $("#cboMedidaItem").val("NIU");
            }
        }
    
}

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
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="eventDefault(event)"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}


function AgregarLineaDetalleAnexo(Id, Nombre) {

    let tr = '';
    tr += `<tr>
            <td style="display:none"><input  class="form-control" type="text" value="`+ Id + `" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/SolicitudRQ/Download?ImageName=`+ Nombre + `" >Descargar</a>
            </td>
            <td><button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ Id + `,this)">-</button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

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


function EliminarAnexo(Id, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarAnexoSolicitud", { 'IdSolicitudRQAnexos': Id }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });

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

    if (CodigoItem.length == 0) {
        swal("Informacion!", "Debe Especificar Un Producto!");
        return;
    }

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

    if (PrecioUnitarioItem <= 0) {
        swal("Informacion!", "El precio debe ser mayor a 0!");
        return;
    }

    if (ValidarCantidad <= 0) {
        swal("Informacion!", "La cantidad debe ser mayor a 0!");
        return;
    }




    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });


    //validaciones
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
        UnidadMedida = JSON.parse(data);
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
    tr += `<tr id="tritem` + contador + `">
            <td  style="display:none;"><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>
            <td>`+ CodigoItem + `</td>
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
            <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador +`)" id="cboCuadrillaTablaId`+ contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador +`"></select></td>
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
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)"></button></td>
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
    //$("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
    $("#txtCantidadNecesaria" + contador).val(CantidadItem)

    $("#cboProyecto" + contador).val(ProyectoItem);
    $("#cboAlmacen" + contador).val(AlmacenItem);
    $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);
    $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
    $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);
    CalcularTotalDetalle(contador)
    ObtenerCuadrillasTabla()
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
    LimpiarModalItem();
    NumeracionDinamica();
    $("#cboClaseArticulo").prop("disabled", true)
    if ($("#cboClaseArticulo").val() != 2) {
        for (var i = 0; i < 50; i++) {
            $("#txtDescripcionArticulo" + i).prop("disabled", true)
        } 
    }
}


//function LimpiarDatosModalItems() {

//}

function borrartditem(contador) {
    $("#tritem" + contador).remove()
    NumeracionDinamica();
}


function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}

function CargarImpuestos() {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        let impuestos = JSON.parse(data);
        llenarComboImpuesto(impuestos, "cboImpuesto", "Seleccione")
    });
}

function CargarUnidadMedidaItem() {
    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        let unidad = JSON.parse(data);
        llenarComboUnidadMedidaItem(unidad, "cboMedidaItem", "Seleccione")
    });
}

function CargarGrupoUnidadMedida() {
    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ObtenerGrupoUnidadMedida", { 'estado': 1 }, function (data, status) {
        let grupounidad = JSON.parse(data);
        console.log(grupounidad);
        llenarComboGrupoUnidadMedidaItem(grupounidad, "cboGrupoUnidadMedida", "Seleccione")
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

//function CargarSucursales() {
//    $.ajaxSetup({ async: false });
//    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
//        let sucursales = JSON.parse(data);
//        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
//    });
//}

function CargarDepartamentos() {
    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    });
}

function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
}

//function CargarProyectos() {
//    $.ajaxSetup({ async: false });
//    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
//        let proyecto = JSON.parse(data);
//        llenarComboProyectoItem(proyecto, "cboProyectoItem", "Seleccione")
//    });
//}

//function CargarCentroCostos() {
//    $.ajaxSetup({ async: false });
//    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
//        let proyecto = JSON.parse(data);
//        llenarComboCentroCostoItem(proyecto, "cboCentroCostoItem", "Seleccione")
//    });
//}



function CargarTipoDocumentoOperacion() {
    $.ajaxSetup({ async: false });
    $.post("/TipoDocumentoOperacion/ObtenerTipoDocumentoOperacion", { estado: 1 }, function (data, status) {
        let tipodocumentooperacion = JSON.parse(data);
        llenarComboTipoDocumentoOperacion(tipodocumentooperacion, "cboTipoDocumentoOperacion", "Seleccione")
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
        if (lista[i].Documento == 9) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();
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



function llenarComboProyectoItem(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProyecto + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboCentroCostoItem(lista, idCombo, primerItem) {
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

//function llenarComboSucursal(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    var nCampos;
//    var campos;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSucursal + "'>" + lista[i].Descripcion + "</option>"; }
//        else { }
//    }
//    var cbo = document.getElementById(idCombo);
//    if (cbo != null) cbo.innerHTML = contenido;
//}

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDepartamento + "'>" + lista[i].Descripcion + "</option>"; }
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
                if (PrimerId==0) {
                    PrimerId = lista[0].IdTipoDocumento;
                    contenido += "<option value='" + lista[i].IdTipoDocumento + "'>NOTA CREDITO</option>";
                }
                

                //contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>";

            }
        }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(PrimerId)
}



//llenarComboTipoDocumentoOperacion()


function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
    $("#cboClaseArticulo").prop("disabled",false)
    $("#IdTipoProducto").prop("disabled",false)
    $("#btn_agregar_limpio").prop("disabled",false)

}


function SetImpuestoDetalle() {

    let varImpuestoCabecera = $("#cboImpuesto").val();
    $(".ImpuestoCabecera").val(varImpuestoCabecera).change();

}





function ValidarMonedaBase() {

    let varMoneda = $("#cboMoneda").val();

    $.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

        if (data == "error") {
            swal("Error!", "Ocurrio un Error")
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
    let Fecha = $("#txtFechaContabilizacion").val();
    $.post("ObtenerTipoCambio", { 'Moneda': Moneda, 'Fecha': Fecha }, function (data, status) {
        let dato = JSON.parse(data);
        //console.log(dato);
        $("#txtTipoCambio").val(dato.venta);

    });


    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
}


function ObtenerNumeracion() {

    let varSerie = $("#cboSerie").val();

    //$.post("/Serie/ValidarNumeracionSerieSolicitudRQ", { 'IdSerie': varSerie }, function (data, status) {
    //    if (data == 'sin datos') {
    //        $.post("/Serie/ObtenerDatosxID", { 'IdSerie': varSerie }, function (data, status) {
    //            let valores = JSON.parse(data);
    //            $("#txtNumeracion").val(valores[0].NumeroInicial);
    //        });
    //    } else {
    //        let values = JSON.parse(data);
    //        let Numero = Number(values[0].NumeroInicial);
    //        $("#txtNumeracion").val(Numero + 1);

    //    }
    //});

}


function GuardarSolicitud() {
    if ($("#IdTipoRegistro").val() != 4) {
        //PROVISION Y CAJA CHICA
        $("#cboCentroCosto").val(7)
        //Validaciones
        if ($("#cboTipoDocumentoOperacion").val() == 0 || $("#cboTipoDocumentoOperacion").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Tipo de Movimiento',
                'error'
            )
            return;
        }

        if ($("#IdTipoDocumentoRef").val() == 0 || $("#IdTipoDocumentoRef").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Documento de referencia',
                'error'
            )
            return;
        }


        if ($("#cboSerie").val() == 0 || $("#cboSerie").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo Serie',
                'error'
            )
            return;
        }

        if ($("#IdCondicionPago").val() == 0 || $("#IdCondicionPago").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Condicion de Pago',
                'error'
            )
            return;
        }

        if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Proveedor',
                'error'
            )
            return;
        }
        if ($("#IdSemana").val() == 0 || $("#IdSemana").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Semana',
                'error'
            )
            return;
        }
        if ($("#IdTipoRegistro").val() == 0 || $("#IdTipoRegistro").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Tipo Registro',
                'error'
            )
            return;
        }

        let ArrayGeneral = new Array();


        let arrayFechaNecesaria = new Array();
        let arrayIdMoneda = new Array();
        let arrayTipoCambio = new Array();

        let arrayIndicadorImpuesto = new Array();


        let arrayProveedor = new Array();
        let arrayNumeroFabricacion = new Array();
        let arrayNumeroSerie = new Array();
        let arrayLineaNegocio = new Array();
        let arrayCentroCosto = new Array();
        let arrayProyecto = new Array();

        let arrayIdSolicitudDetalle = new Array();

        //let arrayIdAlmacen = new Array();
        let arrayPrioridad = new Array();
        //$("select[name='cboMoneda[]']").each(function (indice, elemento) {
        //    arrayIdMoneda.push($(elemento).val());
        //});

        //$("input[name='txtTipoCambio[]']").each(function (indice, elemento) {
        //    arrayTipoCambio.push($(elemento).val());
        //});

        //$("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        //    arrayIndicadorImpuesto.push($(elemento).val());
        //});



        let arrayIdArticulo = new Array();
        $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
            arrayIdArticulo.push($(elemento).val());
        });


        let arraytxtDescripcionArticulo = new Array();
        $("input[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
            arraytxtDescripcionArticulo.push($(elemento).val());
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



        let arrayImpuestosItem = new Array();
        $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
            arrayImpuestosItem.push($(elemento).val());
        });

        let arraytxtIdOrigen = new Array();
        $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
            arraytxtIdOrigen.push($(elemento).val());
        });

        let arraytxtTablaOrigen = new Array();
        $("input[name='txtTablaOrigen[]']").each(function (indice, elemento) {
            arraytxtTablaOrigen.push($(elemento).val());
        });
        let arrayCboCuadrillaTabla = new Array();
        $(".cboCuadrillaTabla").each(function (indice, elemento) {
            arrayCboCuadrillaTabla.push($(elemento).val());
        });

        let arrayCboResponsableTabla = new Array();
        $(".cboResponsableTabla").each(function (indice, elemento) {
            arrayCboResponsableTabla.push($(elemento).val());
        });





        //Cabecera
        let IdAlmacen = $("#cboAlmacen").val();
        let IdTipoDocumento = $("#cboTipoDocumentoOperacion").val();
        let IdSerie = $("#cboSerie").val();
        let Correlativo = $("#txtNumeracion").val();
        let IdMoneda = $("#cboMoneda").val();
        let TipoCambio = $("#txtTipoCambio").val();
        let FechaContabilizacion = $("#txtFechaContabilizacion").val();
        let FechaDocumento = $("#txtFechaDocumento").val();
        let IdCentroCosto = $("#cboCentroCosto").val();
        let Comentario = $("#txtComentarios").val();
        let SubTotal = $("#txtTotalAntesDescuento").val();
        let Impuesto = $("#txtImpuesto").val();
        let Total = $("#txtTotal").val();
        let IdCuadrilla = $("#IdCuadrilla").val();

        let IdResponsable = $("#IdResponsable").val();
        let IdTipoDocumentoRef = 13
        let SerieNumeroRef = $("#SerieNumeroRef").val();
        //END Cabecera

        //let oMovimientoDetalleDTO = {};
        //oMovimientoDetalleDTO.Total = arrayTotal


        let NombTablaOrigen = "";

        if ($("#IdPedido").val() != 0) {
            NombTablaOrigen = 'PEDIDO';
        }
        if ($("#IdOPDN").val() != 0) {
            NombTablaOrigen = 'OPDN';
        }
        //Validar Items de Otra Tabla


        let detalles = [];
        if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

            for (var i = 0; i < arrayIdArticulo.length; i++) {
                detalles.push({
                    'IdArticulo': parseInt(arrayIdArticulo[i]),
                    'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                    'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                    'IdAlmacen': IdAlmacen,
                    'Cantidad': arrayCantidadNecesaria[i],
                    'Igv': 0,
                    'PrecioUnidadBase': arrayPrecioInfo[i],
                    'PrecioUnidadTotal': arrayPrecioInfo[i],
                    'TotalBase': arrayTotal[i],
                    'Total': arrayTotal[i],
                    'CuentaContable': 1,
                    'IdCentroCosto': IdCentroCosto,
                    'IdAfectacionIgv': 1,
                    'Descuento': 0,
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
                    'NombTablaOrigen': arraytxtTablaOrigen[i],
                    'IdOrigen': arraytxtIdOrigen[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],




                })
            }

        }

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

        $.ajax({
            url: "UpdateInsertMovimientoNotaCredito",
            type: "POST",
            async: true,
            data: {
                detalles,
                AnexoDetalle,
                //cabecera
                'IdAlmacen': IdAlmacen,
                'IdTipoDocumento': IdTipoDocumento,
                'IdSerie': IdSerie,
                'Correlativo': Correlativo,
                'IdMoneda': IdMoneda,
                'TipoCambio': TipoCambio,
                'FechaContabilizacion': FechaContabilizacion,
                'FechaDocumento': FechaDocumento,
                'IdCentroCosto': IdCentroCosto,
                'IdGlosaContable': $("#cboGlosaContable").val(),
                'Comentario': Comentario,
                'SubTotal': SubTotal,
                'Impuesto': Impuesto,
                'Total': Total,
                'IdCuadrilla': 2582,
                'IdResponsable': 24151,
                'IdTipoDocumentoRef': IdTipoDocumentoRef,
                'NumSerieTipoDocumentoRef': SerieNumeroRef,
                'IdProveedor': $("#IdProveedor").val(),
                'IdCondicionPago': $("#IdCondicionPago").val(),
                'IdTipoRegistro': $("#IdTipoRegistro").val(),
                'IdSemana': $("#IdSemana").val()
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
                    ObtenerDatosxIDORPC(data)
                    //swal("Exito!", "Proceso Realizado Correctamente", "success")
                    listarOrpc()

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
    } else {
        //RENDICION
        $("#cboCentroCosto").val(7)
        //Validaciones
        if ($("#cboTipoDocumentoOperacion").val() == 0 || $("#cboTipoDocumentoOperacion").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Tipo de Movimiento',
                'error'
            )
            return;
        }

        if ($("#IdTipoDocumentoRef").val() == 0 || $("#IdTipoDocumentoRef").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Documento de referencia',
                'error'
            )
            return;
        }

        if ($("#cboSerie").val() == 0 || $("#cboSerie").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo Serie',
                'error'
            )
            return;
        }

        if ($("#IdCondicionPago").val() == 0 || $("#IdCondicionPago").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Condicion de Pago',
                'error'
            )
            return;
        }

        if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Proveedor',
                'error'
            )
            return;
        }
        if ($("#IdGiro").val() == 0 || $("#IdGiro").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Giro',
                'error'
            )
            return;
        }
        if ($("#IdTipoRegistro").val() == 0 || $("#IdTipoRegistro").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Tipo Registro',
                'error'
            )
            return;
        }

        let ArrayGeneral = new Array();


        let arrayFechaNecesaria = new Array();
        let arrayIdMoneda = new Array();
        let arrayTipoCambio = new Array();

        let arrayIndicadorImpuesto = new Array();


        let arrayProveedor = new Array();
        let arrayNumeroFabricacion = new Array();
        let arrayNumeroSerie = new Array();
        let arrayLineaNegocio = new Array();
        let arrayCentroCosto = new Array();
        let arrayProyecto = new Array();

        let arrayIdSolicitudDetalle = new Array();

        //let arrayIdAlmacen = new Array();
        let arrayPrioridad = new Array();
        //$("select[name='cboMoneda[]']").each(function (indice, elemento) {
        //    arrayIdMoneda.push($(elemento).val());
        //});

        //$("input[name='txtTipoCambio[]']").each(function (indice, elemento) {
        //    arrayTipoCambio.push($(elemento).val());
        //});

        //$("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        //    arrayIndicadorImpuesto.push($(elemento).val());
        //});



        let arrayIdArticulo = new Array();
        $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
            arrayIdArticulo.push($(elemento).val());
        });


        let arraytxtDescripcionArticulo = new Array();
        $("input[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
            arraytxtDescripcionArticulo.push($(elemento).val());
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



        let arrayImpuestosItem = new Array();
        $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
            arrayImpuestosItem.push($(elemento).val());
        });

        let arraytxtIdOrigen = new Array();
        $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
            arraytxtIdOrigen.push($(elemento).val());
        });

        let arraytxtTablaOrigen = new Array();
        $("input[name='txtTablaOrigen[]']").each(function (indice, elemento) {
            arraytxtTablaOrigen.push($(elemento).val());
        });

        //Cabecera
        let IdAlmacen = $("#cboAlmacen").val();
        let IdTipoDocumento = $("#cboTipoDocumentoOperacion").val();
        let IdSerie = $("#cboSerie").val();
        let Correlativo = $("#txtNumeracion").val();
        let IdMoneda = $("#cboMoneda").val();
        let TipoCambio = $("#txtTipoCambio").val();
        let FechaContabilizacion = $("#txtFechaContabilizacion").val();
        let FechaDocumento = $("#txtFechaDocumento").val();
        let IdCentroCosto = $("#cboCentroCosto").val();
        let Comentario = $("#txtComentarios").val();
        let SubTotal = $("#txtTotalAntesDescuento").val();
        let Impuesto = $("#txtImpuesto").val();
        let Total = $("#txtTotal").val();
        let IdCuadrilla = $("#IdCuadrilla").val();

        let IdResponsable = $("#IdResponsable").val();
        let IdTipoDocumentoRef = 13
        let SerieNumeroRef = $("#SerieNumeroRef").val();
        //END Cabecera

        //let oMovimientoDetalleDTO = {};
        //oMovimientoDetalleDTO.Total = arrayTotal


        let NombTablaOrigen = "";

        if ($("#IdPedido").val() != 0) {
            NombTablaOrigen = 'PEDIDO';
        }
        if ($("#IdOPDN").val() != 0) {
            NombTablaOrigen = 'OPDN';
        }
        //Validar Items de Otra Tabla


        let detalles = [];
        if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

            for (var i = 0; i < arrayIdArticulo.length; i++) {
                detalles.push({
                    'IdArticulo': parseInt(arrayIdArticulo[i]),
                    'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                    'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                    'IdAlmacen': IdAlmacen,
                    'Cantidad': arrayCantidadNecesaria[i],
                    'Igv': 0,
                    'PrecioUnidadBase': arrayPrecioInfo[i],
                    'PrecioUnidadTotal': arrayPrecioInfo[i],
                    'TotalBase': arrayTotal[i],
                    'Total': arrayTotal[i],
                    'CuentaContable': 1,
                    'IdCentroCosto': IdCentroCosto,
                    'IdAfectacionIgv': 1,
                    'Descuento': 0,
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
                    'NombTablaOrigen': arraytxtTablaOrigen[i],
                    'IdOrigen': arraytxtIdOrigen[i],



                })
            }

        }

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

        $.ajax({
            url: "UpdateInsertMovimientoNotaCredito",
            type: "POST",
            async: true,
            data: {
                detalles,
                AnexoDetalle,
                //cabecera
                'IdAlmacen': IdAlmacen,
                'IdTipoDocumento': IdTipoDocumento,
                'IdSerie': IdSerie,
                'Correlativo': Correlativo,
                'IdMoneda': IdMoneda,
                'TipoCambio': TipoCambio,
                'FechaContabilizacion': FechaContabilizacion,
                'FechaDocumento': FechaDocumento,
                'IdCentroCosto': IdCentroCosto,
                'IdGlosaContable': $("#cboGlosaContable").val(),
                'Comentario': Comentario,
                'SubTotal': SubTotal,
                'Impuesto': Impuesto,
                'Total': Total,
                'IdCuadrilla': 2582,
                'IdResponsable': 24151,
                'IdTipoDocumentoRef': IdTipoDocumentoRef,
                'NumSerieTipoDocumentoRef': SerieNumeroRef,
                'IdProveedor': $("#IdProveedor").val(),
                'IdCondicionPago': $("#IdCondicionPago").val(),
                'IdTipoRegistro': $("#IdTipoRegistro").val(),
                'IdSemana': $("#IdGiro").val()
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
                    ObtenerDatosxIDORPC(data)
                    //swal("Exito!", "Proceso Realizado Correctamente", "success")
                    listarOrpc()

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
    $("#txtComentarios").val('');
    $("#txtImpuesto").val('');
    $("#txtTotal").val('');
    $("#txtEstado").val(1);
}


function ObtenerDatosxID(IdMovimiento) {
    $("#txtId").val(IdMovimiento);
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    CargarBase()
    CargarTipoDocumentoOperacion()
    ObtenerCuadrillas()
    CargarSeries();
    CargarSeries();
    CargarMoneda();


    $("#lblTituloModal").html("Editar Ingreso");
    AbrirModal("modal-form");





    $.post('../Movimientos/ObtenerDatosxIdMovimiento', {
        'IdMovimiento': IdMovimiento,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            console.log(movimiento);

            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios)))
            $("#txtImpuesto").val(formatNumber(movimiento.Impuesto.toFixed(DecimalesPrecios)))
            $("#txtTotal").val(formatNumber(movimiento.Total.toFixed(DecimalesPrecios)))
            $("#IdCuadrilla").val(movimiento.IdCuadrilla)
            $("#IdResponsable").val(movimiento.IdResponsable)
            $("#cboCentroCosto").val(7)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(13)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)

            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboGlosaContable").val(movimiento.IdGlosaContable).change()
            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);

            //agrega detalle
            let tr = '';

            let Detalle = movimiento.detalles;
            $("#total_items").html(Detalle.length)
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {
                AgregarLineaDetalle(i, Detalle[i]);
                $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
            }


            let DetalleAnexo = solicitudes[0].DetallesAnexo;
            for (var i = 0; i < DetalleAnexo.length; i++) {
                AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            }


        }

    });

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
    console.log(formatNumber(varTotal.toFixed(2)));
    $("#txtItemTotal" + contador).val((varTotal.toFixed(2))).change();


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

    $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
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

    $("#txtTotalAntesDescuento").val(formatNumber(subtotal.toFixed(2)));
    $("#txtImpuesto").val(formatNumber(impuesto.toFixed(2)));
    $("#txtTotal").val(formatNumber(total.toFixed(2)));

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
        <td>
          <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo + `" disabled/>
        </td>
        <td>
             <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdDefinicionGrupo == IdDefinicionGrupoUnidad) {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupoUnidad + `" selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupoUnidad + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" value="`+ formatNumber(detalle.Cantidad.toFixed(DecimalesCantidades)) + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)">
        </td>
        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="`+ formatNumberDecimales(detalle.valor_unitario, 2) + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>



       <td input>
                <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" disabled>` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
        </td>

  <td><input class="form-control" value="`+ detalle.NombCuadrilla + `" disabled></input></td>
        <td><input class="form-control" value="`+ detalle.NombResponsable +`" disabled></input></td>



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
        <td>
            <button type="button" class="btn-sm btn btn-danger" disabled> - </button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);

    NumeracionDinamica();


}

function EliminarDetalle(IdSolicitudRQDetalle, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleSolicitud", { 'IdSolicitudRQDetalle': IdSolicitudRQDetalle }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });



}

function EnviarTipoCambioDetalle() {

    //let Moneda = $("#cboMoneda").val();

    //$.post("ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {

    //    console.log(data);


    let TpCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(TpCambio);

    //});

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

    $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1,'TipoItem':TipoItem}, function (data, status) {

        if (data == "error") {
            swal("Informacion!", "No se encontro Articulo")
        } else {
            let items = JSON.parse(data);
            console.log(items);
            let tr = '';

            for (var i = 0; i < items.length; i++) {
                /* if (items[i].Inventario == TipoItem) {*/
                if (items[i].Stock > 0) {
                    tr += '<tr>' +
                        '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                        '<td>' + items[i].Codigo + '</td>' +
                        '<td>' + items[i].Descripcion1 + '</td>' +
                        '<td>' + items[i].Stock + '</td>' +
                        '<td>' + items[i].UnidadMedida + '</td>' +
                        '</tr>';
                }

                //} else {
                //    if (TipoItem == 2 && items[i].Inventario==false) {
                //        tr += '<tr>' +
                //            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
                //            '<td>' + items[i].Codigo + '</td>' +
                //            '<td>' + items[i].Descripcion1 + '</td>' +
                //            '<td>' + items[i].Stock + '</td>' +
                //            '<td>' + items[i].UnidadMedida + '</td>' +
                //            '</tr>';
                //    }
                //}

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



//function BuscarListadoProyecto() {

//    $("#ModalListadoProyecto").modal();

//    $.post("/Proyecto/ObtenerProyectos", function (data, status) {

//        if (data == "error") {
//            swal("Info!", "No se encontro Proyectos")
//        } else {
//            let proyectos = JSON.parse(data);
//            console.log(proyectos);
//            let tr = '';

//            for (var i = 0; i < proyectos.length; i++) {

//                tr += '<tr>' +
//                    '<td><input type="radio" clase="" id="rdSeleccionadoProyecto' + proyectos[i].IdProyecto + '"  name="rdSeleccionadoProyecto"  value = "' + proyectos[i].IdProyecto + '" ></td>' +
//                    '<td>' + proyectos[i].Codigo + '</td>' +
//                    '<td>' + proyectos[i].Descripcion + '</td>' +
//                    '</tr>';
//            }

//            $("#tbody_listado_proyecto").html(tr);

//            tableProyecto = $("#tabla_listado_proyecto").DataTable({
//                info: false, "language": {
//                    "paginate": {
//                        "first": "Primero",
//                        "last": "Último",
//                        "next": "Siguiente",
//                        "previous": "Anterior"
//                    },
//                    "processing": "Procesando...",
//                    "search": "Buscar:",
//                    "lengthMenu": "Mostrar _MENU_ registros"
//                }
//            });
//        }

//    });

//}



//function BuscarListadoCentroCosto() {

//    $("#ModalListadoCentroCosto").modal();

//    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {

//        if (data == "error") {
//            swal("Info!", "No se encontro Centro de Costo")
//        } else {
//            let centroCosto = JSON.parse(data);
//            console.log(centroCosto);
//            let tr = '';

//            for (var i = 0; i < centroCosto.length; i++) {

//                tr += '<tr>' +
//                    '<td><input type="radio" clase="" id="rdSeleccionadoCentroCosto' + centroCosto[i].IdCentroCosto + '"  name="rdSeleccionadoCentroCosto"  value = "' + centroCosto[i].IdCentroCosto + '" ></td>' +
//                    '<td>' + centroCosto[i].Codigo + '</td>' +
//                    '<td>' + centroCosto[i].Descripcion + '</td>' +
//                    '</tr>';
//            }

//            $("#tbody_listado_centroCosto").html(tr);

//            tableCentroCosto = $("#tabla_listado_centroCosto").DataTable({
//                info: false, "language": {
//                    "paginate": {
//                        "first": "Primero",
//                        "last": "Último",
//                        "next": "Siguiente",
//                        "previous": "Anterior"
//                    },
//                    "processing": "Procesando...",
//                    "search": "Buscar:",
//                    "lengthMenu": "Mostrar _MENU_ registros"
//                }
//            });
//        }

//    });

//}

function BuscarListadoAlmacen() {
    console.log('BuscarListadoAlmacen');

    $("#ModalListadoAlmacen").modal();
    $.post("../Almacen/ObtenerAlmacen", function (data, status) {

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





function SeleccionarItemListado() {
    if ($("#cboClaseArticulo").val() != 2) {
        $("#IdIndicadorImpuesto").val(1)
        let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
        let TipoItem = $("#cboClaseArticulo").val();
        let Almacen = $("#cboAlmacenItem").val();
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                let datos = JSON.parse(data);

                $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
                $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
                $("#txtCodigoItem").val(datos[0].Codigo);
                $("#txtIdItem").val(datos[0].IdArticulo);
                $("#txtDescripcionItem").val(datos[0].Descripcion1);
                $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
                $("#txtStockAlmacenItem").val(datos[0].Stock);

                tableItems.destroy();
            }
        });
    } else {
        $("#IdIndicadorImpuesto").val(1)
        let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
        let TipoItem = $("#cboClaseArticulo").val();
        let Almacen = $("#cboAlmacenItem").val();
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProductoConServicios", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                let datos = JSON.parse(data);

                $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
                $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
                $("#txtCodigoItem").val(datos[0].Codigo);
                $("#txtIdItem").val(datos[0].IdArticulo);
                $("#txtDescripcionItem").val(datos[0].Descripcion1);
                $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
                $("#txtStockAlmacenItem").val(datos[0].Stock);

                tableItems.destroy();
            }
        });
        $("#txtDescripcionItem").prop("disabled",false)
    }

}

function CerrarModalListadoItems() {
    tableItems.destroy();
}


function SeleccionarProyectoListado() {

    let IdProyecto = $('input:radio[name=rdSeleccionadoProyecto]:checked').val();

    $.post("/Proyecto/ObtenerDatosxID", { 'IdProyecto': IdProyecto }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Proyecto")
            tableProyecto.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboProyectoItem").val(datos[0].IdProyecto);
            tableProyecto.destroy();

        }
    });
}

function CerrarModalListadoProyecto() {
    tableProyecto.destroy();
}



function SeleccionarCentroCostoListado() {

    let IdCentroCosto = $('input:radio[name=rdSeleccionadoCentroCosto]:checked').val();

    $.post("/CentroCosto/ObtenerDatosxID", { 'IdCentroCosto': IdCentroCosto }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Centro Costo")
            tableCentroCosto.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboCentroCostoItem").val(datos[0].IdCentroCosto);
            tableCentroCosto.destroy();

        }
    });
}


function CerrarModalListadoCentroCosto() {
    tableCentroCosto.destroy();
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

function CerrarModalListadoAlmacen() {
    tableAlmacen.destroy();
}

function ObtenerConfiguracionDecimales() {

    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;

    });
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

function ValidarDecimalesImportes(input) {
    let cantidad = $("#" + input).val();
    let separar = cantidad.split(".");

    if (separar.length > 1) {
        if ((separar[1]).length > DecimalesCantidades) {
            $("#" + input).val(Number(cantidad).toFixed(DecimalesImportes));
        }
    }
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

function GenerarExtorno() {
    let IdMovimiento = $("#txtId").val();
    Swal.fire({
        title: 'DESEA GENERAR EXTORNO?',
        text: "Se validara si los productos ingresados se encuentran con Stock",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "GenerarIngresoExtorno",
                type: "POST",
                async: true,
                data: {
                    'IdMovimiento': IdMovimiento
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
                        //swal("Exito!", "Proceso Realizado Correctamente", "success")
                        table.destroy();
                        ConsultaServidor("../Movimientos/ObtenerMovimientosIngresos");

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
    })
}


function AbrirModalPedidos() {
    $("#ModalListadoPedido").modal();
    listarpedidosdt()
}

function AbrirModalEntregas() {
    $("#ModalListadoEntrega").modal();
    listarentregadt()
}

function listarentregadt() {
    tableentrega = $('#tabla_listado_entregas').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/EntradaMercancia/ListarOPDNDTModalORPC',
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
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox" id="checkIdOpdn` + full.IdOPDN + `" value="` + full.IdOPDN + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckOPDN()" class="checkIdOpdn"> <label for="cbox2"></label>`
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split("T")[0].toString()
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Referencia
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Total
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_entregas tbody').unbind("dblclick");

    $('#tabla_listado_entregas tbody').on('dblclick', 'tr', function () {
        var data = tableentrega.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        $("#IdProveedor").val(data["IdProveedor"]).change();
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarOPNDDetalle(data);
        $('#ModalListadoEntrega').modal('hide');
        tableentrega.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}




function listarpedidosdt() {
    tablepedido = $('#tabla_listado_pedidos').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/Pedido/ObtenerPedidosEntregaLDT',
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
                    return `
                        <input type="checkbox" id="CheckIdPedido`+ full.IdPedido + `" value="` + full.IdPedido + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckPedido()" class="checkIdPedidos"> <label for="cbox2"></label>`
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor.toUpperCase()
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.total_venta
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_pedidos tbody').on('dblclick', 'tr', function () {
        var data = tablepedido.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarPedidoToEntradaMercancia(data);
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}

function AgregarPedidoToEntradaMercancia(data) {
    for (var i = 0; i < 50; i++) {
        borrartditem(i)
    }
    $("#IdPedido").val(data['IdPedido']);

    if ($("#IdOPDN").val() != 0) {
        swal("Informacion!", "Solo puede agregar Entrega de Mercancia!");
        $("#IdPedido").val(0)
        return;
    }

    $("#IdBase").val(data['IdBase']).change();
    $("#IdObra").val(data['IdObra']).change();
    $("#cboAlmacen").val(data['IdAlmacen']).change();
    let serieynumero = data['NombSerie'] + '-' + data['Correlativo'];
    $.ajaxSetup({ async: false });
    $.post("/Pedido/ObtenerPedidoDetalle", { 'IdPedido': data['IdPedido'] }, function (data, status) {
        let datos = JSON.parse(data);
        let pasarsiguiente = 0;
        for (var k = 0; k < datos.length; k++) {

            $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
                if (datos[k]['IdPedidoDetalle'] == ($(elemento).val())) {
                    swal("Informacion!", "Hay un producto que ya se cargo previamente!");
                    pasarsiguiente++;
                    return;
                }
            });
            if (pasarsiguiente > 0) {
                return;
            }
            /*AGREGAR LINEA*/
            let IdItem = datos[k]['IdArticulo'];
            let CodigoItem = "xxx";
            let MedidaItem = datos[k]['IdDefinicion'];
            let DescripcionItem = datos[k]['DescripcionArticulo'];
            let PrecioUnitarioItem = datos[k]['valor_unitario'];
            let CantidadItem = datos[k]['Cantidad'] - datos[k]['CantidadObtenida'];
            let CantidadMaxima = datos[k]['Cantidad'] - datos[k]['CantidadObtenida'];
            let ProyectoItem = datos[k]['IdArticulo'];
            let CentroCostoItem = datos[k]['IdArticulo'];
            let ReferenciaItem = datos[k]['IdArticulo'];
            let AlmacenItem = datos[k]['IdArticulo'];
            let PrioridadItem = datos[k]['IdArticulo'];
            let IdGrupoUnidadMedida = datos[k]['IdGrupoUnidadMedida'];
            let IdIndicadorImpuesto = datos[k]['IdIndicadorImpuesto'];


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
            let ValidarCantidad = datos[k]['Cantidad'];
            console.log(ValidarCantidad);
            if (CodigoItem.length == 0) {
                swal("Informacion!", "Debe Especificar Un Producto!");
                return;
            }

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
            tr += `<tr id="tritem` + contador + `">
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
            <td>
                <input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">
                 <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdPedidoDetalle'] + `" id="txtIdOrigen` + contador + `" >
            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td>
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
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);


            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();


            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }




            $("#txtIdArticulo" + contador).val(IdItem);
            $("#txtCodigoArticulo" + contador).val(CodigoItem);
            $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
            $("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
            $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);



            $("#cboCentroCostos" + contador).val(CentroCostoItem);
            $("#txtReferencia" + contador).val('BASADO EN PEDIDOS: ' + serieynumero);

            LimpiarModalItem();
        }

        /*AGREGAR LINEA*/

    });


}



function AgregarOPNDDetalle(data) {

    $("#IdOPDN").val(data['IdOPDN']);
    $("#IdBase").val(data['IdBase']).change();

    if ($("#IdPedido").val() != 0) {
        swal("Informacion!", "Solo puede agregar Pedido!");
        $("#IdOPDN").val(0);
        return;
    }



    $("#IdObra").val(data['IdObra']).change();
    $("#cboAlmacen").val(data['IdAlmacen']).change();
    $("#IdProveedor").val(data['IdProveedor']).change();
    $("#IdCondicionPago").val(data['IdCondicionPago']);
    let serieycorrelativo = data['NombSerie'] + '-' + data['Correlativo'];

    $.ajaxSetup({ async: false });
    $.post("/EntradaMercancia/ObtenerOPDNDetalle", { 'IdOPDN': data['IdOPDN'] }, function (data, status) {
        let datos = JSON.parse(data);
        for (var k = 0; k < datos.length; k++) {
            let pasarsiguiente = 0;
            $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
                if (datos[k]['IdOPDNDetalle'] == ($(elemento).val())) {
                    swal("Informacion!", "Hay un producto que ya se cargo previamente!");
                    pasarsiguiente++;
                    return;
                }
            });
            if (pasarsiguiente > 0) {
                return;
            }

            /*AGREGAR LINEA*/
            let IdItem = datos[k]['IdArticulo'];
            let CodigoItem = "xxx";
            let MedidaItem = datos[k]['IdDefinicionGrupoUnidad'];
            let DescripcionItem = datos[k]['DescripcionArticulo'];
            let PrecioUnitarioItem = datos[k]['valor_unitario'];
            let CantidadItem = datos[k]['Cantidad'] - datos[k]['CantidadUsada'];

            let CantidadMaxima = datos[k]['Cantidad'] - datos[k]['CantidadUsada'];
            let ProyectoItem = datos[k]['IdArticulo'];
            let CentroCostoItem = datos[k]['IdArticulo'];
            let ReferenciaItem = datos[k]['IdArticulo'];
            let AlmacenItem = datos[k]['IdArticulo'];
            let PrioridadItem = datos[k]['IdArticulo'];
            let IdGrupoUnidadMedida = datos[k]['IdGrupoUnidadMedida'];
            let IdIndicadorImpuesto = datos[k]['IdIndicadorImpuesto'];


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
            let ValidarCantidad = datos[k]['Cantidad'];
            console.log(ValidarCantidad);
            if (CodigoItem.length == 0) {
                swal("Informacion!", "Debe Especificar Un Producto!");
                return;
            }

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
            tr += `<tr id="tritem` + contador + `">
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
            <td>
                <input class="form-control" type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdOPDNDetalle'] + `" id="txtIdOrigen` + contador + `" >
            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td>
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
            <td> <input class="form-control" type="text" value="BASADO EN LA ENTREGA `+ serieycorrelativo + `" id="txtReferencia` + contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);


            let varMoneda = $("#cboMoneda").val();
            let varTipoCambio = $("#txtTipoCambio").val();


            if (varMoneda) {
                $(".MonedaDeCabecera").val(varMoneda);
            }
            if (varTipoCambio) {
                $(".TipoCambioDeCabecera").val(varTipoCambio);
            }




            $("#txtIdArticulo" + contador).val(IdItem);
            $("#txtCodigoArticulo" + contador).val(CodigoItem);
            $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
            $("#cboUnidadMedida" + contador).val(MedidaItem);
            $("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
            $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto).change();;



            $("#cboCentroCostos" + contador).val(CentroCostoItem);


            LimpiarModalItem();
        }

        /*AGREGAR LINEA*/

    });


}

function CalculaCantidadMaxima(conta) {
    if (parseFloat($("#txtCantidadNecesaria" + conta).val()) > parseFloat($("#txtCantidadNecesariaMaxima" + conta).val())) {

        $("#txtCantidadNecesaria" + conta).val($("#txtCantidadNecesariaMaxima" + conta).val());
        Swal.fire(
            'Error!',
            'El Item del Pedido tiene como maximo ' + $("#txtCantidadNecesariaMaxima" + conta).val() + ' Cantidades.',
            'error'
        )
    }
}



function listarOrpc() {
    let varIdBaseFiltro = $("#cboBaseFiltro").val()
    tablepedido = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarORPCDT',
            type: 'POST',
            data: {
                'IdBase':varIdBaseFiltro,
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
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxIDORPC(` + full.IdORPC + `)"></button>
                            `
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
                    let FechaDoc = full.FechaDocumento
                    const fechaTabla = new Date(FechaDoc);
                    const diaTabla = fechaTabla.getDate();
                    const mesTabla = fechaTabla.getMonth() + 1;
                    const anioTabla = fechaTabla.getFullYear();

                    // Añadir ceros iniciales si es necesario
                    const diaFormateado = diaTabla < 10 ? "0" + diaTabla : diaTabla;
                    const mesFormateado = mesTabla < 10 ? "0" + mesTabla : mesTabla;

                    const fechaFormateada = `${diaFormateado}/${mesFormateado}/${anioTabla}`;

                    return fechaFormateada
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario.toString().toUpperCase() 
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return 'NOTA CREDITO'
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoDocumentoRef
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor.toString().toUpperCase() 
                },
            },
            {
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.Total, 2)
                },
            },
            {
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                targets: 10,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                targets: 11,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Moneda
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_pedidos tbody').on('dblclick', 'tr', function () {
        var data = tablepedido.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarPedidoToEntradaMercancia(data);
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}



function ObtenerDatosxIDORPC(IdOrpc) {
    $("#IdSemana").prop("disabled",true)
    $("#IdGiro").prop("disabled",true)
    $("#IdTipoRegistro").prop("disabled",true)
    $("#txtId").val(IdOrpc);
    $("#cboGlosaContable").prop("disabled",true)
    CargarGlosaContable();
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
   // CargarSucursales();
    CargarMoneda();
    CargarImpuestos();
    ListarBasesxUsuario();
    CargarProveedor();
    CargarCondicionPago();


    $("#cboImpuesto").val(2).change();
    $("#cboSerie").val(1).change();
    /*END NUEVO*/



    $("#lblTituloModal").html("Editar Nota Credito Proveedor");
    AbrirModal("modal-form");





    $.post('ObtenerDatosxIdOrpc', {
        'IdOrpc': IdOrpc,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            console.log(movimiento);
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios)))
            $("#txtImpuesto").val(formatNumber(movimiento.Impuesto.toFixed(DecimalesPrecios)))
            //alert(formatNumberDecimales(movimiento.Total, 2));
            $("#txtTotal").val(formatNumberDecimales(movimiento.Total, 2))

            $("#cboCentroCosto").val(7)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(13)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)

            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            CargarTipoRegistro()
            $("#IdTipoRegistro").val(movimiento.IdTipoRegistro).change()
           
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#IdProveedor").val(movimiento.IdProveedor).change();



            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);
            $("#txtNumeracion").val(movimiento.Correlativo);
            $("#txtTipoCambio").val(formatNumberDecimales(movimiento.TipoCambio, 2))

            $("#IdCondicionPago").val(movimiento.idCondicionPago)
            $("#cboGlosaContable").val(movimiento.IdGlosaContable)
            if ($("#IdTipoRegistro").val() != 4) {
                $("#IdSemana").val(movimiento.IdSemana)
            } else {
                $("#IdGiro").val(movimiento.IdSemana)
            }
            
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


            $("#IdCuadrilla").val("")
            $("#IdResponsable").val("")
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
                    <td><button class="btn btn-xs btn-danger" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)">-</button></td>
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

}

function ValidacionesCheckOPDN() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    $('.checkIdOpdn').each(function () {
        if (this.checked) {
            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionados").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        $("#btn_checkSeleccionados").show();
    } else {
        $("#btn_checkSeleccionados").hide();
    }
}

function AgregarSeleccionadOPDN() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    $('.checkIdOpdn').each(function () {
        if (this.checked) {

            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionados").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        let numerospedidos = "";
        $('.checkIdOpdn').each(function () {
            if (this.checked) {
                var data = tableentrega.row($("#" + ($(this).val()))).data();

                AgregarOPNDDetalle(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo'] + ' / ';
            }
        });

        $("#txtComentarios").html('BASADOS EN LAS ENTREGAS ' + numerospedidos)
        $('#ModalListadoEntrega').modal('hide');
        tableentrega.ajax.reload()
    }
}

function ValidacionesCheckPedido() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    $('.checkIdPedidos').each(function () {
        if (this.checked) {

            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionadosPedido").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        $("#btn_checkSeleccionadosPedido").show();
    } else {
        $("#btn_checkSeleccionadosPedido").hide();
    }
}

function AgregarSeleccionadoPedido() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    $('.checkIdPedidos').each(function () {
        if (this.checked) {

            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionadosPedido").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        let numerospedidos = "";
        $('.checkIdPedidos').each(function () {
            if (this.checked) {
                var data = tablepedido.row($("#" + ($(this).val()))).data();

                AgregarPedidoToEntradaMercancia(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo'] + ' / ';
            }
        });

        $("#txtComentarios").html('BASADOS EN LOS PEDIDOS ' + numerospedidos)
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
    }
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
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
                $("#cboAlmacen").prop('disabled', true);

            }
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled', true);
                $("#IdObra").prop('disabled', true);
            }

            if (contadorBase == 1 && contadorObra != 1 && contadorAlmacen != 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
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

function validarseriescontable() {
    let IdSerie = $("#cboSerie").val();
    let IdDocumento = 1;
    let Fecha = $("#txtFechaContabilizacion").val();
    let Orden = 1;
    let datosrespuesta;
    let estado = 0;
    datosrespuesta = ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden);
    console.log("RESPUESTA VALICACIOON ")
    console.log(datosrespuesta.FechaRelacion.length)
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

function AbrirModalSalida() {
    $("#ModalListadoSalida").modal();
    listarmodalsalida()
}

function AbrirModalFacturaProveedor() {
    $("#ModalListadoOPCH").modal();
    listarFacturaProveedor()
}

function listarmodalsalida() {
    tablesalida = $('#tabla_listado_salida').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/SalidaMercancia/ListarSalidaModalDT',
            type: 'POST',
            data: {
                IdAlmacen: $("#cboAlmacen").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox" id="checkIdOpdn` + full.IdOPDN + `" value="` + full.IdOPDN + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckOPDN()" class="checkIdOpdn"> <label for="cbox2"></label>`
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
                    return full.FechaDocumento.split("T")[0].toString()
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Total
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_salida tbody').unbind("dblclick");

    $('#tabla_listado_salida tbody').on('dblclick', 'tr', function () {
        var data = tablesalida.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        $("#IdProveedor").val(data["IdProveedor"]).change();
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarSalidaDetalle(data);
        $('#ModalListadoSalida').modal('hide');
        tablesalida.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}



function AbrirModalFacturaProveedor() {
    $("#ModalListadoOPCH").modal();
    listarFacturaProveedor()
}

function listarFacturaProveedor() {
    tablefacturaproveedor = $('#tabla_listado_facturaproveedor').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/FacturaProveedor/ListarOPCHDTModal',
            type: 'POST',
            data: {
                IdAlmacen: $("#cboAlmacen").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox" id="checkIdOpdn` + full.IdOPDN + `" value="` + full.IdOPDN + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckOPDN()" class="checkIdOpdn"> <label for="cbox2"></label>`
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
                    return full.FechaDocumento.split("T")[0].toString()
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Total
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    $('#tabla_listado_facturaproveedor tbody').unbind("dblclick");

    $('#tabla_listado_facturaproveedor tbody').on('dblclick', 'tr', function () {
        var data = tablefacturaproveedor.row(this).data();
        console.log(data);
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        $("#IdProveedor").val(data["IdProveedor"]).change();
        colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
        $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
        ultimaFila = $("#" + data["DT_RowId"]);
        AgregarOpchDetalle(data);
        $('#ModalListadoOPCH').modal('hide');
        tablefacturaproveedor.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}

function AgregarSalidaDetalle(datos) {
    let datarecibida;
    let detalles;
    let UnidadMedida;
    $.ajaxSetup({ async: false });
    $.post("/Movimientos/ObtenerDatosxIdMovimiento", { 'IdMovimiento': datos.IdMovimiento }, function (data, status) {
        datarecibida = JSON.parse(data);



        console.log(datarecibida);
        detalles = datarecibida.detalles;
        for (var d = 0; d < detalles.length; d++) {
            let IdGrupoUnidadMedida = detalles[d].IdGrupoUnidadMedida;
            let serieycorrelativo = datos.NumSerieTipoDocumentoRef


            let UnidadMedida;
            $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
                UnidadMedida = JSON.parse(data);
            });

            let Moneda;
            $.post("/Moneda/ObtenerMonedas", function (data, status) {
                Moneda = JSON.parse(data);
            });

            let IndicadorImpuesto;
            $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
                IndicadorImpuesto = JSON.parse(data);
            });

            let Almacen
            $.post("../Almacen/ObtenerAlmacen", function (data, status) {
                Almacen = JSON.parse(data);
            });

            contador++;
            let tr = '';


            //PARA EL DETALLE
            let Cantidad = detalles[d].Cantidad - detalles[d].CantidadNotaCredito;
            let CantidadMaxima = detalles[d].Cantidad - detalles[d].CantidadNotaCredito;
            //END PARA EL DETALLE






            //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
            //    <option value="0">Seleccione</option>
            //</select>
            tr += `<tr id="tritem` + contador + `">
            <td><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" value="`+ detalles[d].IdArticulo + `" id="txtIdArticulo` + contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalles[d].CodigoArticulo + `"/>
            </td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" value="` + detalles[d].DescripcionArticulo + `"/></td>
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
            <td>
                <input type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ detalles[d].IdMovimientoDetalle + `" id="txtIdOrigen` + contador + `" >
                <input type="hidden" name="txtTablaOrigen[]" value="MOVIMIENTOSALIDA" id="txtIdOrigen` + contador + `" >
            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td>
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
            <td> <input class="form-control" type="text" value="BASADO EN LA FACTURA PROVEEDOR `+ serieycorrelativo + `" id="txtReferencia` + contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);
            $("#cboUnidadMedida" + contador).val(detalles[d].IdDefinicionGrupoUnidad);
            $("#cboMonedaDetalle" + contador).val(datarecibida.IdMoneda);
            $("#txtCantidadNecesaria" + contador).val(Cantidad).change();
            $("#txtPrecioInfo" + contador).val(detalles[d].PrecioUnidadBase);
            $("#cboIndicadorImpuestoDetalle" + contador).val(1).change();
            $("#cboAlmacen" + contador).val(detalles[d].IdAlmacen)
        }
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

function AgregarOpchDetalle(datos) {
    for (var i = 0; i < 50; i++) {
        borrartditem(i)
    }
    $("#cboClaseArticulo").prop("disabled", true)
    $("#IdTipoProducto").prop("disabled", true)
    $("#btn_agregar_limpio").prop("disabled", true)
    let datarecibida;
    let detalles;
    let UnidadMedida;
    $("#cboMoneda").val(datos.IdMoneda).change();
    $("#IdProveedor").val(datos['IdProveedor']).change();

    $.ajaxSetup({ async: false });
    $.post("/FacturaProveedor/ObtenerOPCHDetalle", { 'IdOPCH': datos.IdOPCH }, function (data, status) {

        datarecibida = JSON.parse(data);
        detalles = datarecibida;
        for (var d = 0; d < detalles.length; d++) {
      
            let IdGrupoUnidadMedida = detalles[d].IdGrupoUnidadMedida;
            let serieycorrelativo = datos.NumSerieTipoDocumentoRef

            let UnidadMedida;
            $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxGrupo", { 'IdGrupoUnidadMedida': IdGrupoUnidadMedida }, function (data, status) {
                UnidadMedida = JSON.parse(data);
            });

            let Moneda;
            $.post("/Moneda/ObtenerMonedas", function (data, status) {
                Moneda = JSON.parse(data);
            });

            let IndicadorImpuesto;
            $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
                IndicadorImpuesto = JSON.parse(data);
            });

            let Almacen
            $.post("../Almacen/ObtenerAlmacen", function (data, status) {
                Almacen = JSON.parse(data);
            });

            contador++;
            let tr = '';


            //PARA EL DETALLE
            let Cantidad = detalles[d].Cantidad - detalles[d].CantidadNotaCredito;
            let CantidadMaxima = detalles[d].Cantidad - detalles[d].CantidadNotaCredito;
            //END PARA EL DETALLE






            //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
            //    <option value="0">Seleccione</option>
            //</select>
            tr += `<tr id="tritem` + contador + `">
            <td><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" value="`+ detalles[d].IdArticulo + `" id="txtIdArticulo` + contador + `" name="txtIdArticulo[]" />
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalles[d].CodigoArticulo + `"/>
            </td>
            <td>`+ detalles[d].CodigoArticulo+ `</td>   
            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" value="` + detalles[d].DescripcionArticulo + `"/></td>
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
            <td>
                <input class="form-control" type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ detalles[d].IdOPCHDetalle + `" id="txtIdOrigen` + contador + `" disabled>
                <input type="hidden" name="txtTablaOrigen[]" value="OPCH" id="txtIdOrigen` + contador + `" disabled>
            </td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
            tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
        <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador +`)" id="cboCuadrillaTablaId`+ contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador +`"></select></td>

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
            <td> <input class="form-control" type="text" value="BASADO EN LA ENTREGA `+ serieycorrelativo + `" id="txtReferencia` + contador + `" name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)"></button></td>
          </tr>`;

            $("#tabla").find('tbody').append(tr);
            $("#cboUnidadMedida" + contador).val(detalles[d].IdDefinicionGrupoUnidad);
            $("#cboMonedaDetalle" + contador).val(datarecibida.IdMoneda);
            $("#txtCantidadNecesaria" + contador).val(Cantidad).change();
            $("#txtPrecioInfo" + contador).val(detalles[d].PrecioUnidadBase);
            $("#cboIndicadorImpuestoDetalle" + contador).val(1).change();
            $("#cboAlmacen" + contador).val(detalles[d].IdAlmacen)
            NumeracionDinamica();
        }

    });
    ObtenerCuadrillasTabla()
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGlosaContable + "'>" + lista[i].Descripcion + "</option>"; }
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

function ObtenerEmpleadosxIdCuadrilla() {
    let IdCuadrilla = $("#IdCuadrilla").val();
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleados(empleados, "IdResponsable", "Seleccione")
    });
}

function llenarComboEmpleados(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(lista[ultimoindice].IdEmpleado).change();
    ObtenerCapataz()
}
function ObtenerCapataz() {
    let IdCuadrilla = $("#IdCuadrilla").val();
    //setTimeout(() => {
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrilla }, function (data, status) {
        let capataz = JSON.parse(data);
        $("#IdResponsable").select2("val", capataz[0].IdEmpleado);
    })
    /*  }, 1000);*/


}


function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
    });
}
function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}
function redondear() {
    let valorSinR = +$("#txtTotalAntesDescuento").val() + +$("#txtImpuesto").val()
    let valorRedondeo = +$("#txtRedondeo").val()
    $("#txtTotal").val((valorSinR+valorRedondeo).toFixed(DecimalesPrecios))
}
function esRendicion() {
    if ($("#IdTipoRegistro").val() != 4) {
        $("#DivGiro").hide()
        $("#DivSemana").show()
    } else {
        $("#DivGiro").show()
        $("#DivSemana").hide()
        CargarGiros()
    }
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
function ResetRegistroSemana() {
    $("#IdTipoRegistro").prop("selectedIndex", 0)
    $("#IdSemana").prop("selectedIndex", 0)
}
function ObtenerCuadrillasTabla() {
    let IdObra = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrillaTabla(cuadrilla, "cboCuadrillaTabla", "Seleccione")
    });
}
function llenarComboCuadrillaTabla(lista, idCombo, primerItem) {
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
    $(".cboCuadrillaTabla").html(contenido)
}

//function ObtenerEmpleadosxIdCuadrillaTabla() {

//    let IdCuadrilla = $("#IdCuadrilla").val();
//    $.ajaxSetup({ async: false });
//    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
//        let empleados = JSON.parse(data);
//        llenarComboEmpleadosTabla(empleados, "cboResponsableTabla", "Seleccione")
//    });
//}

//function llenarComboEmpleadosTabla(lista, idCombo, primerItem) {
//    var contenido = "";
//    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
//    var nRegistros = lista.length;
//    console.log("Empleados: " + lista.length)
//    var nCampos;
//    var campos;
//    let ultimoindice = 0;
//    for (var i = 0; i < nRegistros; i++) {

//        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
//        else { }
//    }
//    $(".cboResponsableTabla").html(contenido)


//    ObtenerCapatazTabla()
//}
//function ObtenerCapatazTabla() {
//    let IdCuadrilla = $("#IdCuadrilla").val();
//    /* setTimeout(() => {*/
//    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrilla }, function (data, status) {
//        let capataz = JSON.parse(data);
//        $(".cboResponsableTabla").val(capataz[0].IdEmpleado).change();
//    })
//    /*}, 1000);*/

//}
function SetCuadrillaTabla() {
    let SetCuadrilla = $("#IdCuadrilla").val()
    $(".cboCuadrillaTabla").val(SetCuadrilla).change()
}
function SeleccionarEmpleadosTabla(contador) {

    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTablaFila(empleados, "cboResponsableTablaId" + contador, "Seleccione", contador)
    });

}
function llenarComboEmpleadosTablaFila(lista, idCombo, primerItem, contador) {
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


    ObtenerCapatazTablaFila(contador)
}
function ObtenerCapatazTablaFila(contador) {
    let IdCuadrillaFila = $("#cboCuadrillaTablaId" + contador).val();
    /* setTimeout(() => {*/
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrillaFila }, function (data, status) {
        let capataz = JSON.parse(data);
        $("#cboResponsableTablaId" + contador).val(capataz[0].IdEmpleado).change();
    })
    /*}, 1000);*/

}