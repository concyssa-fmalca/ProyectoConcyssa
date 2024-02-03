let tablepedido = "";
let contarclick = 0;
var ultimaFila = null;
var colorOriginal;
let tableopdn;
let valorfor = 1

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
            try {
                let proveedores = JSON.parse(data);
                $("#Direccion").val(proveedores[0].DireccionFiscal);
                $("#Telefono").val(proveedores[0].Telefono);
            }
            catch (e) {
                console.log("No hay Proveedor Seleccionado")
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].NumeroDocumento + "-"+ lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

//function ObtenerConfiguracionDecimales() {
//    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

//        let datos = JSON.parse(data);
//        DecimalesCantidades = datos[0].Cantidades;
//        DecimalesImportes = datos[0].Importes;
//        DecimalesPrecios = datos[0].Precios;
//        DecimalesPorcentajes = datos[0].Porcentajes;
//    });
//}

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



/*USANDO */
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
function ObtenerEmpleadosxIdCuadrilla() {
    let IdCuadrilla = $("#IdCuadrilla").val();
    console.log(IdCuadrilla)
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", { 'IdCuadrilla': IdCuadrilla }, function (data, status) {
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCuadrilla + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
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
            llenarComboAlmacen(almacen, "cboAlmacen","seleccione")
            llenarComboAlmacen(almacen, "cboAlmacenItem","seleccione")
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

    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrilla(cuadrilla, "IdCuadrilla", "Seleccione")
    });
}



function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();

    console.log(IdBase);
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
        llenarComboBase(base, "IdBase")
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
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    var url = "../Movimientos/ObtenerMovimientosIngresos";
    CargarBaseFiltro()
    ObtenerConfiguracionDecimales();
    Decimales();
    /* ConsultaServidor(url);*/
    //listaropdnDT();
    $("#IdProveedor").select2();
    $("#IdCuadrilla").select2();

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
    listaropdnDT()
    $("#IdCuadrila").select2();
    $("#IdResponsable").select2();

};



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
        console.log("cabecera");
        console.log(movimientos);

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
    $("#IdProveedor").prop("disabled", false);
    $("#Direccion").prop("disabled", false);
    $("#Telefono").prop("disabled", false);
    $("#cboMoneda").prop("disabled", false);
    $("#btnEditar").hide()
    $("#btnExtorno").hide()
    $("#btnGrabar").show()
    $("#IdProveedor").val(0).change()
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    $("#txtFechaDocumento").val(today)
    $("#txtFechaContabilizacion").val(today)

    $("#IdPedido").val(0);
    $("#IdOPCH").val(0);
    $("#lblTituloModal").html("Entrega Mercancia");
    $(".ocultarDiv").show()
    $("#btn_agregaritem").prop("disabled", false)
    disabledmodal(false)
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

 
    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }

  


    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    //CargarAlmacen()
    //CargarBase()
    CargarTipoDocumentoOperacion()
    //ObtenerCuadrillas();
   
    CargarSeries();
    CargarCondicionPago();
    CargarProveedor();

    //CargarObra();
    //AgregarLinea();



    CargarSolicitante(1);
    //CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();

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
    //ListarBasesxUsuario();
    $("#cboTipoDocumentoOperacion").val(20);
    //setearValor_ComboRenderizado("cboCodigoArticulo");

    //ObtenerObraxIdBase();
    $("#IdTipoDocumentoRef").val(1);
    $("#cboCentroCosto").val(7);
    $("#btn_agregaritem").prop("disabled",false)
    $("#IdProveedor").val(0)
    $("#Direccion").val("")
    $("#Telefono").val("")

}





function OpenModalItem() {

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
            $("#divTipoServicio").show();
            $("#SNinguno").prop('checked', true) 
            $("#txtStockAlmacenItem").hide();
            $("#lblStockItem").hide();
        } else if (ClaseArticulo == 3) { //activo
            $("#BtnBuscarListadoAlmacen").prop("disabled", false);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
            $("#txtDescripcionItem").prop("disabled", true);

            $("#IdTipoProducto").hide();
            //$("#IdTipoProducto").val(0);
            $("#divTipoServicio").hide();
            $("#SNinguno").prop('checked', true)
            $("#SNinguno").prop('checked', false) 
            $("#txtStockAlmacenItem").show();
            $("#lblStockItem").show();
        } else {//Producto
            $("#txtDescripcionItem").prop("disabled", true);
            $("#BtnBuscarListadoAlmacen").prop("disabled", false);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
            $("#IdTipoProducto").show();
            //$("#IdTipoProducto").val(0);
            $("#divTipoServicio").hide();
            $("#SNinguno").prop('checked', true)
            $("#SNinguno").prop('checked', false) 
            $("#txtStockAlmacenItem").show();
            $("#lblStockItem").show();
        }
        $("#cboAlmacenItem").val($("#cboAlmacen").val()); // que salga el almacen por defecto


        $("#cboPrioridadItem").val(2);
        $("#cboClaseArticulo").prop("disabled", true);
        $("#IdTipoProducto").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarGrupoUnidadMedida();
        /* CargarProyectos();*/
        //CargarCentroCostos();
        //CargarAlmacen();
    }
}
let contadorAnexo = 0;
function AgregarLineaAnexo(Nombre) {
    contadorAnexo++
    let tr = '';
    tr += `<tr id="filaAnexo` + contadorAnexo +`">
            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ Nombre + `
               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
            </td>
            <td>
               <a href="/Anexos/`+ Nombre + `" target="_blank" >Descargar</a>
            </td>
            <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexoEnMemoria(`+ contadorAnexo +`)"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}
function EliminarAnexoEnMemoria(contAnexo) {
    $("#filaAnexo" + contAnexo).remove();
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
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ Id + `,this)"></button></td>
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


function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboObraFiltro")

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
    $("#cboObraFiltro").val($("#cboObraFiltro option:first").val());

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
let limitador = 0;

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
    let TipoServicio 
    //txtReferenciaItem
    if ($("#SNinguno").is(":checked")) {
        TipoServicio = 'Ninguno'
    } else if ($("#SPreventivo").is(":checked")) {
        TipoServicio = 'Preventivo'
        ReferenciaItem = ReferenciaItem + "(Servicio Preventivo)"
    } else if ($("#SCorrectivo").is(":checked")) {
        TipoServicio = 'Correctivo'
        ReferenciaItem = ReferenciaItem + "(Servicio Correctivo)"
    } else {
        TipoServicio = 'No Aplica'
    }

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
    if (CodigoItem.length == 0) {
        swal("Informacion!", "Debe Especificar Un Producto!");
        return;
    }

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

    if (ValidarCantidad<=0) {
        swal("Informacion!", "La Cantidad debe ser mayor a 0!");
        return;
    }

    if ($("#txtPrecioUnitarioItem").val() <= 0) {
        swal("Informacion!", "El precio debe ser mayor a 0!");
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
    if (limitador >= 30) {
        swal("Informacion!", "Solo se pueden agregar Hasta 30 items");
        return;
    }
     //AGREGADOR MASIVO
    for (var J = 0; J < valorfor; J++) {
        console.log("VUELTAAAAAAAAAAA: " + J)

        limitador++
        contador++;
        let tr = '';

        //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
        //    <option value="0">Seleccione</option>
        //</select>
        tr += `<tr id="tritem` + contador + `">
            <td style="display:none;"><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
<td>`+ limitador + `</td>
            <td input style="display:none;"> 
             <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />         
            <td> <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" /></td>
           
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
            <td><input class="form-control" id="txtCantidadBloq`+ contador + `" disabled></input></td>
            <td><input class="form-control" type="text" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input class="form-control" type="text" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td >
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
        tr += `  <option impuesto="0" value="0">Seleccione</option>`;
        for (var i = 0; i < IndicadorImpuesto.length; i++) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
            <td><select class="form-control  cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador + `)" id="cboCuadrillaTablaId` + contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador + `"></select></td>
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
<td style="display:none"><input style="width:50px" class="form-control" type="text" value="" id="txtTipoServicio`+ contador + `" name="txtTipoServicio[]"></input></td>

<td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `);restarLimitador()"></button></td>
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

        //$('#tabla').find('tr').each(function () {
        //    console.log("ESCONDIENDOOOOOOOOOOOOO")
        //    $(this).find('td:eq(' + 9 + '), th:eq(' + 9 + ')').show();
        //    $(this).find('td:eq(' + 10 + '), th:eq(' + 10 + ')').show();
        //});


        $("#txtIdArticulo" + contador).val(IdItem);
        $("#txtCodigoArticulo" + contador).val(CodigoItem);
        $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
        $("#cboUnidadMedida" + contador).val(MedidaItem);
        $("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
        $("#txtCantidadBloq" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();

        $("#cboProyecto" + contador).val(ProyectoItem);
        $("#cboAlmacen" + contador).val(AlmacenItem);
        $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

        $("#cboCentroCostos" + contador).val(CentroCostoItem);
        $("#txtReferencia" + contador).val(ReferenciaItem);
        $("#txtPrecioInfo" + contador).val(formatNumberDecimales( PrecioUnitarioItem,2)).change();
        $("#cboIndicadorImpuestoDetalle" + contador).val(1).change();
        $("#txtTipoServicio" + contador).val(TipoServicio);
        CalcularTotalDetalle(contador)
        CalcularTotalDetalle(contador)
        LimpiarModalItem();
        ObtenerCuadrillasTabla(contador)
        ObtenerEmpleadosxIdCuadrillaTabla(contador)
        $(".cboCuadrillaTabla").select2()
        $(".cboResponsableTabla").select2()
        $("#cboCuadrillaTablaId").val($("#IdCuadrilla").val()).change()
        $("#cboResponsableTablaId").val($("#IdResponsable").val()).change()
    }
}


//function LimpiarDatosModalItems() {

//}

function borrartditem(contador) {
    $("#tritem" + contador).remove()
}
function restarLimitador() {
    limitador = limitador - 1
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
    console.log('CargarUnidadMedidaItem');
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

function CargarSucursales() {
    $.ajaxSetup({ async: false });
    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
        let sucursales = JSON.parse(data);
        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    });
}

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
        if (lista[i].Documento == 5) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice =i}
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
            if (lista[i].CodeExt == "20") {
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



//llenarComboTipoDocumentoOperacion
//$(document).on('click', '.borrar', function (event) {
//    event.preventDefault();
//    $(this).closest('tr').remove();

//    let filas = $("#tabla").find('tbody tr').length;
//    console.log("filas");
//    console.log(filas);
//});



function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
    $("#IdTipoProducto").prop("disabled", false)
    $("#IdTipoProducto").val(0)
    limpiarDatos();
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
    let Fecha = $("#txtFechaDocumento").val();
    $.post("ObtenerTipoCambio", { 'Moneda': Moneda, 'Fecha': Fecha }, function (data, status) {
        let dato = JSON.parse(data);
        console.log(dato);
        $("#txtTipoCambio").val(dato.venta);

    });


    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
}


function ObtenerNumeracion() {

    let varSerie = $("#cboSerie").val();

    $.post("/Serie/ValidarNumeracionSerieSolicitudRQ", { 'IdSerie': varSerie }, function (data, status) {
        if (data == 'sin datos') {
            $.post("/Serie/ObtenerDatosxID", { 'IdSerie': varSerie }, function (data, status) {
                let valores = JSON.parse(data);
                $("#txtNumeracion").val(valores[0].NumeroInicial);
            });
        } else {
            let values = JSON.parse(data);
            let Numero = Number(values[0].NumeroInicial);
            $("#txtNumeracion").val(Numero + 1);

        }
    });

}

function validarseriescontableParaCrear() {
    let IdSerie = $("#cboSerie").val();
    let IdDocumento = 1;
    let Fecha = $("#txtFechaContabilizacion").val();
    let Orden = 1;
    let datosrespuesta;
    let estado = 0;
    datosrespuesta = ValidarFechaContabilizacionxDocumentoM(IdSerie, IdDocumento, Fecha, Orden);
    console.log(datosrespuesta);
    if (datosrespuesta.FechaRelacion.length == 0) {

        return false;
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
        return false;
    }
    return true
}

function GuardarSolicitud() {

    if (validarseriescontableParaCrear() == false) {
        Swal.fire("Error!", "No Puede Crear este Documento en una Fecha No Habilitada", "error")
        return
    }


    //Validaciones
    if ($("#cboTipoDocumentoOperacion").val() == 0 || $("#cboTipoDocumentoOperacion").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo de Movimiento',
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

    if ($("#IdTipoDocumentoRef").val() == 0 || $("#IdTipoDocumentoRef").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo Documento Referencia',
            'error'
        )
        return;
    }

    //if ($("#IdCuadrilla").val() == 0 || $("#IdCuadrilla").val() == null) {
    //    Swal.fire(
    //        'Error!',
    //        'Complete el campo  Cuadrilla',
    //        'error'
    //    )
    //    return;
    //}


    //if ($("#IdResponsable").val() == 0 || $("#IdResponsable").val() == null) {
    //    Swal.fire(
    //        'Error!',
    //        'Complete el campo Responsable',
    //        'error'
    //    )
    //    return;
    //}
    if ($("#cboCentroCosto").val() == 0 || $("#cboCentroCosto").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo Centro de Costo',
            'error'
        )
        return;
    }

    if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo Proveedor',
            'error'
        )
        return;
    }

    //if ($("#IdCondicionPago").val() == 0 || $("#IdCondicionPago").val() == null) {
    //    Swal.fire(
    //        'Error!',
    //        'Complete el campo Centro de Costo',
    //        'error'
    //    )
    //    return;
    //}




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
        arrayCantidadNecesaria.push(($(elemento).val()).replace(/,/g, ""));
    });

    for (var i = 0; i < arrayCantidadNecesaria.length; i++) {
        if (arrayCantidadNecesaria[i] <= 0) {
            Swal.fire("Advertencia", "Error en Linea " + (i+1) + " , La cantidad no puede ser Menor o Igual a Cero </br> En caso esta linea no cuente con entregas solo quitela con el botón eliminar a la derecha de la misma linea", "info")
            return
        }
    }

    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push(($(elemento).val()).replace(/,/g,""));
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(($(elemento).val()).replace(/,/g, ""));
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
    let arrayCboCuadrillaTabla = new Array();
    $(".cboCuadrillaTabla").each(function (indice, elemento) {
        arrayCboCuadrillaTabla.push($(elemento).val());
    });

    let arrayCboResponsableTabla = new Array();
    $(".cboResponsableTabla").each(function (indice, elemento) {
        arrayCboResponsableTabla.push($(elemento).val());
    });
    let arrayTipoServicio = new Array();
    $("input[name='txtTipoServicio[]']").each(function (indice, elemento) {
        arrayTipoServicio.push($(elemento).val());
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

    let FechaEntrega = $("#FechaEntrega").val();

    let IdCentroCosto = $("#cboCentroCosto").val();
    let Comentario = $("#txtComentarios").val();
    let SubTotal = ($("#txtTotalAntesDescuento").val()).replace(/,/g, "");
    let Impuesto = ($("#txtImpuesto").val()).replace(/,/g, "");
    let Total = ($("#txtTotal").val()).replace(/,/g, "");
    let IdCuadrilla = $("#IdCuadrilla").val();

    let IdResponsable = $("#IdResponsable").val();
    let IdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let SerieNumeroRef = $("#SerieNumeroRef").val();
    //END Cabecera

    //let oMovimientoDetalleDTO = {};
    //oMovimientoDetalleDTO.Total = arrayTotal

    //Validar Items de Otra Tabla
    let NombTablaOrigen = "";

    if ($("#IdPedido").val() != 0) {
        NombTablaOrigen = 'PEDIDO';
    }
    if ($("#IdOPCH").val() != 0) {
        NombTablaOrigen = 'OPCH';
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
                'Cantidad': Number(SinFormato(arrayCantidadNecesaria[i])),
                'Igv': 0,
                'PrecioUnidadBase': Number(SinFormato(arrayPrecioInfo[i])),
                'PrecioUnidadTotal': Number(SinFormato(arrayPrecioInfo[i])),
                'TotalBase': Number(SinFormato(arrayTotal[i])),
                'Total': Number(SinFormato(arrayTotal[i])),
                'CuentaContable': 1,
                'IdCentroCosto': IdCentroCosto,
                'IdAfectacionIgv': 1,
                'Descuento': 0,
                'valor_unitario': Number(SinFormato(arrayPrecioInfo[i])),
                'precio_unitario': 0,
                'IdIndicadorImpuesto': arrayImpuestosItem[i],
                'total_base_igv': 0,
                'porcentaje_igv': 0,
                'total_igv': 0,
                'total_impuestos': Number(SinFormato(arrayTotal[i])) - (Number(SinFormato(arrayPrecioInfo[i])) * Number(SinFormato(arrayCantidadNecesaria[i]))),
                'total_valor_item': (Number(SinFormato(arrayPrecioInfo[i])) * Number(SinFormato(arrayCantidadNecesaria[i]))),
                'total_item': Number(SinFormato(arrayTotal[i])),
                'Referencia': arrayReferencia[i],
                'NombTablaOrigen': NombTablaOrigen,
                'IdOrigen': arraytxtIdOrigen[i],
                'IdCuadrilla': arrayCboCuadrillaTabla[i],
                'IdResponsable': arrayCboResponsableTabla[i],
                'TipoServicio' : arrayTipoServicio[i],
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

    let MovimientoEnviar = []

    MovimientoEnviar.push({
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
        'FechaEntrega': FechaEntrega,
        'IdCentroCosto': IdCentroCosto,
        'Comentario': Comentario,
        'SubTotal': SubTotal,
        'Impuesto': Impuesto,
        'Total': Total,
        'IdCuadrilla': IdCuadrilla,
        'IdResponsable': IdResponsable,
        'IdTipoDocumentoRef': IdTipoDocumentoRef,
        'NumSerieTipoDocumentoRef': SerieNumeroRef,
        'IdProveedor': $("#IdProveedor").val(),
        'IdCondicionPago': $("#IdCondicionPago").val()
    })

    $.ajax({
        url: "UpdateInsertMovimientoEMLogisticaString",
        type: "POST",
        async: true,
        data: {
            'JsonDatosEnviar': JSON.stringify(MovimientoEnviar)
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
            if (data >0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )

                CerrarModal();
                ObtenerDatosxIDOPDN(data)
              /*  ModalNuevo();*/
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                listaropdnDT()

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

function limpiarDatos() {
    $("#SerieNumeroRef").val('')
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
    limitador = 0;
}


function ObtenerDatosxIDOPDN(IdOPDN) {
    $("#btnEditar").show()
    $("#btnExtorno").show()
    $("#btnGrabar").hide()

    disabledmodal(true);
    $("#txtId").val(IdOPDN);
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    CargarTipoDocumentoOperacion()
    CargarBase()
  /*  CargarTipoDocumentoOperacion()*/
    ObtenerCuadrillas()
    CargarSeries();
    CargarCondicionPago()
    CargarMoneda();
    CargarProveedor();
    CargarImpuestos();
   

    let IdUsuario = 0;
    $("#lblTituloModal").html("Editar Ingreso");
    AbrirModal("modal-form");


    let EstaExtornado = false


    $.post('ObtenerDatosxIDOPDN', {
        'IdOPDN': IdOPDN,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);

            console.log(movimiento);
            if (movimiento.IdDocExtorno != 0) {
                EstaExtornado = true
            }
            IdUsuario = movimiento.IdUsuario,
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios)))
            $("#txtImpuesto").val(formatNumber(movimiento.Impuesto.toFixed(DecimalesPrecios)))
            //$("#txtTotal").val(formatNumber(movimiento.Total.toFixed(DecimalesPrecios)))
            console.log(DecimalesPrecios)
            $("#txtTotal").val(formatNumberDecimales(movimiento.Total,2))
            $("#IdCuadrilla").val(movimiento.IdCuadrilla)
            $("#IdResponsable").val(movimiento.IdResponsable)
            $("#cboCentroCosto").val(movimiento.IdCentroCosto)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)
            $("#IdProveedor").val(movimiento.IdProveedor).change();
            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#txtNumeracion").val(movimiento.Correlativo);
            $("#txtTipoCambio").val(formatNumberDecimales(movimiento.TipoCambio,2));
            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);
            $("#txtComentarios").html(movimiento.Comentario)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef.toUpperCase())
            $("#IdCuadrilla").val(movimiento.IdCuadrilla)
            $("#IdResponsable").val(movimiento.IdResponsable)
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
            $("#txtFechaDocumento").val(movimiento.FechaDocumento.split("T")[0]);
            $("#txtFechaContabilizacion").val(movimiento.FechaContabilizacion.split("T")[0]);
            $("#FechaEntrega").val(movimiento.FechaEntrega.split("T")[0]);
            $("#txtComentarios").html(movimiento.Comentario)
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
                    <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `,this)"></button></td>
                </tr>`;
            }
            $("#tabla_files").find('tbody').append(trAnexo);
            
            
            //agrega detalle
            let tr = '';

            let Detalle = movimiento.detalles;
            $("#total_items").html(Detalle.length)
            //console.log(Detalle);
            //console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {
                AgregarLineaDetalle(i, Detalle[i]);
                $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
            }


            //let DetalleAnexo = solicitudes[0].DetallesAnexo;
            //for (var i = 0; i < DetalleAnexo.length; i++) {
            //    AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            //}


        }

    });
    $(".ocultarDiv").hide()
    $("#IdTipoDocumentoRef").prop("disabled",false)
    $("#SerieNumeroRef").prop("disabled",false)
    $("#txtComentarios").prop("disabled",false)
    if (EstaExtornado == true || IdUsuario !== +$("#IdUsuarioSesion").val()) {
        $("#btnExtorno").hide();
        $("#btnEditar").hide();
    } else {
        $("#btnExtorno").show();
        $("#btnEditar").show();
    }
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
            //console.log(movimiento);

            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios)))
            $("#txtImpuesto").val(formatNumber(movimiento.Impuesto.toFixed(DecimalesPrecios)))
            $("#txtTotal").val(formatNumber(movimiento.Total.toFixed(DecimalesPrecios)))
            $("#IdCuadrilla").val(movimiento.IdCuadrilla)
            $("#IdResponsable").val(movimiento.IdResponsable)
            $("#cboCentroCosto").val(movimiento.IdCentroCosto)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)

            $("#IdBase").val(movimiento.IdBase).change();
            $("#IdObra").val(movimiento.IdObra).change();
            $("#cboAlmacen").val(movimiento.IdAlmacen);

            $("#CreatedAt").html(movimiento.CreatedAt.replace("T", " "));
            $("#NombUsuario").html(movimiento.NombUsuario);

            //agrega detalle
            let tr = '';

            let Detalle = movimiento.detalles;
            $("#total_items").html(Detalle.length)
            //console.log(Detalle);
            //console.log("Detalle");
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

    let varIndicadorImppuesto = ($("#cboIndicadorImpuestoDetalle" + contador).val()).replace(/,/g,"");
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle" + contador).attr("impuesto");

    let varCantidadNecesaria = ($("#txtCantidadNecesaria" + contador).val()).replace(/,/g, "");
    let varPrecioInfo = ($("#txtPrecioInfo" + contador).val()).replace(/,/g, "");

    let subtotal = varCantidadNecesaria * varPrecioInfo;

    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        varTotal = subtotal;
    } else {
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }
    console.log("hhh");
    console.log(formatNumber(varTotal.toFixed(2)));

    var Totali = formatNumber(varTotal.toFixed(2));

    $("#txtItemTotal" + contador).val(formatNumberDecimales(varTotal,2)).change();


}

function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push(($(elemento).val()).replace(/,/g,""));
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push(($(elemento).val()).replace(/,/g, ""));
    });
    $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected', elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(($(elemento).val()).replace(/,/g, ""));
    });

    
    

    let subtotal = 0;
    let impuesto = 0;
    let total = 0;

    for (var i = 0; i < arrayPrecioInfo.length; i++) {

        //if (arrayTotal[i].includes(",")) {
        //    arrayTotal[i].replace(",","");
        //}

        subtotal += (Number(arrayCantidadNecesaria[i]) * Number(arrayPrecioInfo[i]));

        total += Number(arrayTotal[i].replace(",", ""));
    }

    console.log(total);
    console.log(subtotal);

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(formatNumber(subtotal.toFixed(2)));
    $("#txtImpuesto").val(formatNumber(impuesto.toFixed(2)));
    $("#txtTotal").val(formatNumber(total.toFixed(2)));

}


function AgregarLineaDetalle(contador, detalle) {

    let tr = '';
    let UnidadMedida;
    let Almacen;
    let IdUnidadMedida = detalle.IdDefinicionGrupoUnidad
    let IdAlmacen = detalle.IdAlmacen
    let IndicadorImpuesto;



    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ListarDefinicionGrupoxIdDefinicionSelect", { 'IdDefinicionGrupo': detalle.IdDefinicionGrupoUnidad }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });
    console.log('*******************');
    console.log(detalle);
    console.log('*******************')

    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    tr = `<tr>
        <td style="display:none">
          <input  class="form-control" type="text" value="`+ (contador + 1) + `" disabled />
        </td>

        <td>`+ detalle.CodigoArticulo +`</td>
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
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `" selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdDefinicionGrupo + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" value="`+ formatNumberDecimales(detalle.Cantidad, 1) + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>

        <td><input disabled class="form-control" value="`+formatNumberDecimales(detalle.CantidadDePedido,1)+`"/></td>

        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="`+ formatNumberDecimales(detalle.valor_unitario,2) + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
                tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        if (detalle.IdIndicadorImpuesto == IndicadorImpuesto[i].IdIndicadorImpuesto) {

            tr += `<option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" selected>` + IndicadorImpuesto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
                   
                }
                tr += `</select>
        </td>
            <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador + `)" id="cboCuadrillaTablaId` + contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador + `"></select></td>
            <td style="display:none"><input style="200px" class="form-control txtIdEntregaDetalle" value="`+ detalle.IdOPDNDetalle +`" ></input></td>

        <td>
            <input class="form-control changeTotal" type="text" style="width:100px" value="`+ formatNumber(detalle.total_item.toFixed(DecimalesPrecios)) + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
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
            <input class="form-control" type="text" style="width:100px" value="`+ detalle.Referencia +`" disabled>
        </td>
        <td>
            <button type="button" class="btn-sm btn btn-danger borrar fa fa-trash" disabled></button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);
    ObtenerCuadrillasTabla(contador)
    ObtenerEmpleadosxIdCuadrillaTabla(contador)
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
   
    console.log("--------detalle.IdCuadrilla")
    console.log("#cboCuadrillaTablaId" + contador)
    console.log(detalle.IdCuadrilla)
   
    if (detalle.TipoServicio == 'No Aplica') {
        $("#cboCuadrillaTablaId" + contador).prop("disabled", true)
        $("#cboResponsableTablaId" + contador).prop("disabled", true)
    } else {
        $("#cboCuadrillaTablaId" + contador).prop("disabled", false)
        $("#cboResponsableTablaId" + contador).prop("disabled", false)
    }
    $("#cboCuadrillaTablaId" + contador).val(detalle.IdCuadrilla).change()
    $("#cboResponsableTablaId" + contador).val(detalle.IdResponsable).change()
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



function SeleccionTrItem(ItemCodigo) {

    $("#rdSeleccionado" + ItemCodigo).prop("checked", true);

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
    if (TipoItem == 1 || TipoItem == 2) {
        $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, 'TipoItem': TipoItem }, function (data, status) {

            if (data == "error") {
                swal("Informacion!", "No se encontro Articulo")
            } else {
                let items = JSON.parse(data);
                console.log(items);
                let tr = '';

                for (var i = 0; i < items.length; i++) {
                    /* if (items[i].Inventario == TipoItem) {*/
                    tr += '<tr onclick="SeleccionTrItem(' + items[i].IdArticulo + ')">' +
                        '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                        '<td>' + items[i].Codigo + '</td>' +
                        '<td>' + items[i].Descripcion1 + '</td>' +
                        '<td>' + items[i].Stock + '</td>' +
                        '<td>' + items[i].UnidadMedida + '</td>' +
                        '</tr>';
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
    } else {
        $.post("/Articulo/ObtenerArticulosActivoFijo",

            function (data, status) {

                if (data == "error") {
                    swal("Informacion!", "No se encontro Articulo")

                } else {

                    let items = JSON.parse(data);
                    //console.log(items);
                    let tr = '';

                    for (var i = 0; i < items.length; i++) {
                        /* if (items[i].Inventario == TipoItem) {*/
                        tr += '<tr onclick="SeleccionTrItem(' + items[i].IdArticulo + ')">' +
                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                            '<td>' + items[i].Codigo + '</td>' +
                            '<td>' + items[i].Descripcion1 + '</td>' +
                            '<td>' + items[i].Stock + '</td>' +
                            '<td>' + items[i].UnidadMedida + '</td>' +
                            '</tr>';
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
                        "iDisplayLength": 100,
                        "bDestroy": true,
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
                        },
                    });

                }

            });
    }
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

    if ($("#cboClaseArticulo").val() == 1) {
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

    } else if ($("#cboClaseArticulo").val() == 2) {
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
        $("#txtDescripcionItem").prop("disabled", false)
    } else {
        let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
        let TipoItem = $("#cboClaseArticulo").val();
        let Almacen = $("#cboAlmacenItem").val();
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                let datos = JSON.parse(data);
                console.log(1);
                $("#cboGrupoUnidadMedida").val(datos[0].IdGrupoUnidadMedida).change();
                $("#cboMedidaItem").val(datos[0].IdUnidadMedidaInv);
                $("#cboGrupoUnidadMedida").prop('disabled', true);
                $("#txtCodigoItem").val(datos[0].Codigo);
                $("#txtIdItem").val(datos[0].IdArticulo);
                $("#txtDescripcionItem").val(datos[0].Descripcion1);
                $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
                $("#txtStockAlmacenItem").val(datos[0].Stock);
                $("#txtPrecioUnitarioItem").val((datos[0].PrecioPromedio).toFixed(DecimalesPrecios))
                tableItems.destroy();
            }
        });
    }
    $("#cboMedidaItem").prop("disabled",true)
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
    console.log("DECIMALES dentro "+ DecimalesPrecios)
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
        console.log("DECIMALES mas dentro"+ DecimalesPrecios )
    });
}
function Decimales() {
    console.log("DECIMALES dentro " + DecimalesPrecios)
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
        console.log("DECIMALES " + DecimalesPrecios)
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
    $("#btn_checkSeleccionados").hide();
    listarpedidosdt()
}

function listarpedidosdt() {

    let IdObra = $("#IdObra").val();

    tablepedido = $('#tabla_listado_pedidos').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/Pedido/ObtenerPedidosEntregaLDT',
            type: 'POST',
            data: {
                'IdObra': IdObra,
                'IdProveedor': $("#IdProveedor").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            //{
            //    targets: -1,
            //    orderable: false,
            //    render: function (data, type, full, meta) {

            //        return 0
            //    },
            //},
            {
                data:null,
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
                    return `
                        <input type="checkbox" id="CheckIdPedido`+ full.IdPedido + `" value="` + full.IdPedido + `" IdProveedor="` + full.IdProveedor + `" NombTipoPedido="` + full.NombTipoPedido + `" onchange="ValidacionesCheckPedido()" class="checkIdPedidos"> <label for="cbox2"></label>`
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<a style="color:blue !important;text-decoration:underline;cursor:pointer" onclick="AbrirOC(' + full.IdPedido + ')">' + full.NombSerie.toUpperCase() + '-' + full.Correlativo +'</a>' 
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoPedido.toUpperCase()
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor.toUpperCase()
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombMoneda.toUpperCase()
                },
            },
            {
                data: null,
                targets:7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.total_venta)
                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaContabilizacion.split("T")[0]
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
        //$('#tabla').find('tr').each(function () {
        //    console.log("ESCONDIENDOOOOOOOOOOOOO")
        //    $(this).find('td:eq(' + 9 + '), th:eq(' + 9 + ')').hide();
        //    $(this).find('td:eq(' + 10 + '), th:eq(' + 10 + ')').hide();
        //});
        //$(".ocultarDiv").hide()
        $("#btn_agregaritem").prop("disabled",true)
        AgregarPedidoToEntradaMercancia(data);
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });
}

function AgregarPedidoToEntradaMercancia(data) {
    $("#IdPedido").val(data['IdPedido']);
    $("#IdOPCH").val(0);

    $("#cboMoneda").val(data.IdMoneda).change();

    $("#IdBase").val(data['IdBase']).change();
    $("#IdObra").val(data['IdObra']).change();
    $("#cboAlmacen").val(data['IdAlmacen']).change();
    $("#IdProveedor").val(data['IdProveedor']).change();
    $("#IdCondicionPago").val(data['IdCondicionPago']).change();
    let dataa = data;
    let varTipoDocumento = 'disabled'
    $(".ocultarDiv").hide()
    if (data['NombTipoPedido'] == 'Orden Servicio') {
        varTipoDocumento = 'enabled'
        $(".ocultarDiv").show()
    }
    $.ajaxSetup({ async: false });
    $.post("/Pedido/ObtenerPedidoDetalle", { 'IdPedido': data['IdPedido'] }, function (data, status) {
        let datos = JSON.parse(data);

        console.log("gggggg");
        console.log(datos);
        console.log("gggggg");

        let pasarsiguiente = 0;
        let pasardato = 0;
        for (var k = 0; k < datos.length; k++) {
            pasardato = 0;
            $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
                if (datos[k]['IdPedidoDetalle'] == ($(elemento).val())) {
                    swal("Informacion!", "Hay un producto que ya se cargo previamente!");
                    pasarsiguiente++;
                    return ;
                }
            });
            if (pasarsiguiente > 0) {
                return;
            }

            if ((datos[k]['Cantidad'] - datos[k]['CantidadObtenida']) == 0) {
                console.log(datos[k]['Cantidad']);
                console.log(datos[k]['CantidadObtenida']);
                pasardato = 1;
            }
            /*AGREGAR LINEA*/
            let IdItem = datos[k]['IdArticulo'];
            let CodigoItem = datos[k]['CodigoProducto']; ///"xxx";
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
            let TipoServicio = datos[k]['TipoServicio']


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
            <td style="display:none;"><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            
            <input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" />
            </td>

            <td>`+ CodigoItem +`</td>


            <td><input class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]"/></td>
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
            <td>
                 <input class="form-control" type="number" name="txtCantidadNecesaria[]" value="`+ datos[k]['Cantidad'] + `" id="txtCantidadNecesaria` + contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                 <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ datos[k]['CantidadObtenida'] + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                 <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdPedidoDetalle'] + `" id="txtIdOrigen` + contador + `" >
            </td>

            <td><input class="form-control" value="`+ datos[k]['Cantidad'] +`" disabled /></td>

            <td><input class="form-control" type="text" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" readonly></td>
            
            <td>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
            tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
            tr += `</select>
            </td>
            <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador +`)" id="cboCuadrillaTablaId`+ contador + `" `+varTipoDocumento+`></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador + `" ` + varTipoDocumento +`></select></td>
            <td><input class="form-control changeTotal" type="text" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" readonly></td>
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
            <td style="display:none"><input style="width:50px" class="form-control" type="text" value="" id="txtTipoServicio`+ contador + `" name="txtTipoServicio[]"></input></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `);CalcularTotales()"></button></td>
          </tr>`;
            if (pasardato==0) {

            
                $("#tabla").find('tbody').append(tr);


                let varMoneda = $("#cboMoneda").val();
                let varTipoCambio = $("#txtTipoCambio").val();


                if (varMoneda) {
                    $(".MonedaDeCabecera").val(varMoneda);
                }
                if (varTipoCambio) {
                    $(".TipoCambioDeCabecera").val(varTipoCambio);
                }


                let PrecioUnitarioItemFormato = formatNumberDecimales(PrecioUnitarioItem,2)
                console.log(PrecioUnitarioItemFormato)
                $("#txtIdArticulo" + contador).val(IdItem);
                $("#txtCodigoArticulo" + contador).val(CodigoItem);
                $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
                $("#cboUnidadMedida" + contador).val(MedidaItem);
                $("#txtCantidadNecesariaMaxima" + contador).val((CantidadMaxima)).change();
                $("#txtCantidadNecesaria" + contador).val((CantidadItem)).change();
                $("#txtPrecioInfo" + contador).val(PrecioUnitarioItemFormato).change();
                $("#cboProyecto" + contador).val(ProyectoItem);
                $("#cboAlmacen" + contador).val(AlmacenItem);
                $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
                $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto).change();
                $("#cboCentroCostos" + contador).val(CentroCostoItem);
                $("#txtReferencia" + contador).val('BASADO EN PEDIDO ' + dataa['NombSerie'] + '-' + dataa['Correlativo']);
                $("#txtTipoServicio" + contador).val(TipoServicio);
                ObtenerCuadrillasTabla(contador)
                ObtenerEmpleadosxIdCuadrillaTabla(contador)
                $(".cboCuadrillaTabla").select2()
                $(".cboResponsableTabla").select2()
                //$("#txtItemTotal" + contador).val(formatNumber(datos[k].total_item));
                NumeracionDinamica();
                LimpiarModalItem();
            }
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



function listaropdnDT() { 
    let varIdBaseFiltro = $("#cboObraFiltro").val()
    tablepedido = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/EntradaMercancia/ListarOPDNDT',
            type: 'POST',
            data: {
                'IdBase' : varIdBaseFiltro,
                'EstadoOPDN': 'ABIERTO',
                'FechaInicio': $("#txtFechaInicio").val(),
                'FechaFin': $("#txtFechaFin").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                data:null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    //<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + full.IdOPDN + `)"></button>`
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxIDOPDN(` + full.IdOPDN + `)"></button>
                            `
                },
            },
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
                    return full.FechaDocumento
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdDocExtorno == 1) {
                        return '<p style="color:red">' + full.NombTipoDocumentoOperacion +'</p>'
                    } else {
                        return full.NombTipoDocumentoOperacion
                    }
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdPedido == 0) {
                        return full.NOC
                    } else {
                        return '<a style="color:blue;text-decoration:underline;cursor:pointer" onclick="AbrirOC('+full.IdPedido+')">'+full.NOC+'</a>'   
                    }
                    
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<a style="color:blue;text-decoration:underline;cursor:pointer" onclick="GenerarReporteOPDN(' + full.IdOPDN + ')">' + full.NombSerie + '-' + full.Correlativo + '</a>'   
                    
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef.toUpperCase()
                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.Total,2)
                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra.toUpperCase()
                },
            },
            {
                data: null,
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen.toUpperCase()
                },
            }


        ],
        "bDestroy": true
    }).DataTable();


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
                console.log("Entro");
                console.log(datos[0].IdObra);
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


function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#cboAlmacen").prop('disabled', valorbolean);
    //$("#cboCentroCosto").prop('disabled', valorbolean);
    $("#cboMoneda").prop('disabled', valorbolean);
    $("#cboSerie").prop('disabled', valorbolean);
    $("#txtFechaDocumento").prop('disabled', valorbolean);
    //$("#txtFechaContabilizacion").prop('disabled', valorbolean);
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


function ValidacionesCheckPedido() {
    let checkSeleccionados = 0;
    let IdProveedor = 0;
    let sumaproveedor = 0;
    let TipoPedido = 0;
    let SumaTipoPedido = 0;
    $('.checkIdPedidos').each(function () {
        if (this.checked) {
            
            checkSeleccionados++
            sumaproveedor += parseInt($(this).attr('IdProveedor'));
            IdProveedor = parseInt($(this).attr('IdProveedor'));
            console.log($(this).attr('NombTipoPedido'))
            if ($(this).attr('NombTipoPedido') == 'Orden Compra') {
                SumaTipoPedido += 1
                TipoPedido = 1
            } else {
                SumaTipoPedido += 2
                TipoPedido = 2
            }
        }
    });
    if (!((checkSeleccionados * IdProveedor) == sumaproveedor)) {
        swal("Error!", "Seleccione el Mismo Proveedor")
        $("#btn_checkSeleccionados").hide();
        return;
    } 
    if (!((checkSeleccionados * TipoPedido) == SumaTipoPedido)) {
        swal("Error!", "No puede Combiar Ordenes de Compra con Ordenes de Servicio")
        $("#btn_checkSeleccionados").hide();
        return;
    } 
   
 
   
    if (checkSeleccionados > 0) {
        $("#btn_checkSeleccionados").show();
    } else {
        $("#btn_checkSeleccionados").hide();
    }
}

function AgregarSeleccionadoPedido() {
    for (var i = 0; i < 10; i++) {
        borrartditem(i);
    }
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
        $("#btn_checkSeleccionados").hide();
        return;
    }

    if (checkSeleccionados > 0) {
        let numerospedidos = "";
        $('.checkIdPedidos').each(function () {
            if (this.checked) {
                var data = tablepedido.row($("#" + ($(this).val()))).data();
                console.log(data);
                AgregarPedidoToEntradaMercancia(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo']+' / ';
            }
        });
        
        $("#txtComentarios").html('BASADOS EN LOS PEDIDOS '+numerospedidos)
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
    } 
    $("#cboClaseArticulo").val(0);
    $("#cboClaseArticulo").prop("disabled", true);
    $("#IdTipoProducto").val(0);
    $("#IdTipoProducto").prop("disabled", true);
    $("#btn_agregaritem").prop("disabled", true);
    $("#IdProveedor").prop("disabled", true);
    $("#Direccion").prop("disabled", true);
    $("#Telefono").prop("disabled", true);
    $("#cboMoneda").prop("disabled", true);
}



function nuevo() {
    CerrarModal();
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

function enviarguia() {
    $.post("/EntradaMercancia/GenerarGuiaElectronica", function (data, status) {
        console.log("ddd");
    });
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
function CambiarClaseArticulo() {

    let ClaseArticulo = $("#cboClaseArticulo").val();
    console.log(ClaseArticulo)
    if (ClaseArticulo == "2") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
       // $("#cboTipoDocumentoOperacion").val(1338)
        $("#txtDescripcionItem").prop("disabled", false)
    } else if (ClaseArticulo == "3") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
    } else {
        $("#IdTipoProducto").show();
        $("#IdTipoProducto").val(0);
        //$("#cboTipoDocumentoOperacion").val(18)
    }

}


function ObtenerCuadrillasTabla(contador) {
    let IdObra = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrillaTabla(cuadrilla, "cboCuadrillaTablaId" + contador, "Seleccione")
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
    $("#" + idCombo).html(contenido)
    $("#" + idCombo).val($("#IdCuadrilla").val())
}

function ObtenerEmpleadosxIdCuadrillaTabla(contador) {

    let IdCuadrilla = $("#IdCuadrilla").val();
    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTabla(empleados, "cboResponsableTablaId" + contador, "Seleccione")
    });
}

function llenarComboEmpleadosTabla(lista, idCombo, primerItem) {
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
    $("#" + idCombo).html(contenido)
    $("#" + idCombo).val($("#IdResponsable").val())


}

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
    console.log(11111111111)
    ObtenerCapatazTablaFila(contador)
}
function ObtenerCapatazTablaFila(contador) {
    let IdCuadrillaFila = $("#cboCuadrillaTablaId" + contador).val();
    /* setTimeout(() => {*/
    console.log(222222222)
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrillaFila }, function (data, status) {
        try {
            let capataz = JSON.parse(data);
            $("#cboResponsableTablaId" + contador).val(capataz[0].IdEmpleado).change();
        } catch (e) {
            console.log('sin capataz')
        }
    })
    /*}, 1000);*/

}
function SetearEmpleadosEnTabla() {
    $(".cboResponsableTabla").val($("#IdResponsable").val()).change();

}
function Editar() {
    let varIdOPDN = $("#txtId").val();
    let varIdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let varNumSerieTipoDocumentoRef = $("#SerieNumeroRef").val();
    let varComentario = $("#txtComentarios").val();

    let arrayCboCuadrillaTabla = new Array();
    $(".cboCuadrillaTabla").each(function (indice, elemento) {
        arrayCboCuadrillaTabla.push($(elemento).val());
    });

    let arrayCboResponsableTabla = new Array();
    $(".cboResponsableTabla").each(function (indice, elemento) {
        arrayCboResponsableTabla.push($(elemento).val());
    });
    let arraytxtIdEntregaDetalle = new Array();
    $(".txtIdEntregaDetalle").each(function (indice, elemento) {
        arraytxtIdEntregaDetalle.push($(elemento).val());
    });

    $.post('UpdateOPDN', {
        'IdOPDN': varIdOPDN,
        'IdTipoDocumentoRef': varIdTipoDocumentoRef,
        'NumSerieTipoDocumentoRef': varNumSerieTipoDocumentoRef,
        'Comentario': varComentario,

    }, function (data, status) {

        if (data != 0) {
            for (var i = 0; i < arraytxtIdEntregaDetalle.length; i++) {
                $.post('UpdateCuadrillasOPDN', {
                    'IdOPDNDetalle': arraytxtIdEntregaDetalle[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],

                }, function (data, status) {
                    if (data != 0) {
                        swal("Exito!", "Proceso Realizado Correctamente", "success")
                        CerrarModal()
                        listaropdnDT()
                    } else {
                        swal("Error!", "Ocurrio un Error")
                        CerrarModal()
                    }

                });



            }
        } else {
            swal("Error!", "Ocurrio un Error")
            CerrarModal()
        }

    });
}

function llenarComboSerieExtorno(lista, idCombo, primerItem) {

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

function Extornar() {
    let IdOPDN = $("#txtId").val();
   

    Swal.fire({
        title: 'DESEA GENERAR EXTORNO?',
        html: "Se validara si los productos ingresados se encuentran con Stock </br>" +
            "</br>" +
            "Serie Para Extorno </br>" +
            "<Select id='cboSerieExtorno' class='form-control'></select>"+
            "</br>" +
            "Fecha De Documento para Extorno  </br>" +
            "<input id='FechDocExtorno' type='date' class='form-control'/>"+
            "</br>" +
            "Fecha de Contabilizacion para Extorno  </br>" +
            "<input id='FechContExtorno' type='date' class='form-control'/>" +
            "</br>" + 
            "<p>* Las Fechas que se muestran por defecto son las mismas que el documento seleccionado</p>"  ,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var parar = 0;
            $.post("ValidarExtorno", { 'IdOPDN': IdOPDN }, function (data, status) {

                var datos = data.split("|");
                console.log(datos);
                if (Number(datos[0]) > 0) {
                    Swal.fire(
                        'Error!',
                        'Este documento ya ha sido extornado! ' + datos[1],
                        'error'
                    )
                    parar++;
                }

            });

            $.post("ValidarFacturaOPDN", { 'IdOPDN': IdOPDN }, function (data, status) {
                if (data > 0) {
                    Swal.fire(
                        'Error!',
                        'Este documento ya cuenta con Factura! ',
                        'error'
                    )
                    parar++;
                } 

            });


            if (parar > 0) {
                return;
            }

            $.ajax({
                url: "GenerarOPDNExtorno",
                type: "POST",
                async: true,
                data: {
                    'IdOPDN': IdOPDN
                },
                //beforeSend: function () {
                //    Swal.fire({
                //        title: "Cargando Verificacion de Stock...",
                //        text: "Por favor espere",
                //        showConfirmButton: false,
                //        allowOutsideClick: false
                //    });
                //},
                success: function (data) {

                    if (data == "bien") {
                        //DATOS PARA SALIDA
                        let Total = $("#txtTotal").val()
                        let SubTotal = Total / (1.18)
                        let Impuesto = Total - SubTotal
                        let AnexoDetalle = [];
                        let anioactual = new Date().getFullYear();
                        
                        let anio = (anioactual.toString()).substring(2,4)

                        let arrayIdArticulo = new Array();
                        $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
                            arrayIdArticulo.push($(elemento).val());
                        });
                        let arraytxtDescripcionArticulo = new Array();
                        $("input[name='txtCodigoArticulo[]']").each(function (indice, elemento) {
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


                        let arrayIdOrigen = new Array();
                        $("input[name='txtIdOrigen[]']").each(function (indice, elemento) {
                            arrayIdOrigen.push($(elemento).val());
                        });



                        let arrayCboCuadrillaTabla = new Array();
                        $(".cboCuadrillaTabla").each(function (indice, elemento) {
                            arrayCboCuadrillaTabla.push($(elemento).val());
                        });

                        let arrayCboResponsableTabla = new Array();
                        $(".cboResponsableTabla").each(function (indice, elemento) {
                            arrayCboResponsableTabla.push($(elemento).val());
                        });

                        let detalles = [];
                        if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

                            for (var i = 0; i < arrayIdArticulo.length; i++) {
                                detalles.push({
                                    'IdArticulo': parseInt(arrayIdArticulo[i]),
                                    'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                                    'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                                    'IdAlmacen': $("#cboAlmacen").val(),
                                    'Cantidad': arrayCantidadNecesaria[i],
                                    'Igv': 0,
                                    'PrecioUnidadBase': arrayPrecioInfo[i],
                                    'PrecioUnidadTotal': arrayPrecioInfo[i],
                                    'TotalBase': arrayTotal[i],
                                    'Total': arrayTotal[i],
                                    'CuentaContable': 1,
                                    'IdCentroCosto': 7,
                                    'IdAfectacionIgv': 1,
                                    'Descuento': 0,
                                    'Referencia': arrayReferencia[i],
                                    //'IdOrigen': arrayIdOrigen[i],
                                    'TablaOrigen': 'Entregas',
                                    'IdCuadrilla': 0,
                                    'IdResponsable': 0,

                                })
                            }

                        }

                        console.log(detalles)
                        console.log(1111)
                        let TipoProductos
                        //VALIDA SI ES SERVICIO O NO PARA CREAR LA SALIDA
                        $.post('ValidaTipoProductoOPDN', { 'ArticuloMuestra': arrayIdArticulo[0] }, function (data, status) {

                            let validacion = JSON.parse(data);
                            console.log(validacion)
                            TipoProductos = validacion[0].TipoArticulos


                        });
                        if (TipoProductos != 'Servicio') {
                            //CREAR LA SALIDA
                            $.ajax({
                                url: "/SalidaMercancia/UpdateInsertMovimiento",
                                type: "POST",
                                async: true,
                                data: {
                                    detalles,
                                    AnexoDetalle,
                                    //cabecera
                                    'IdAlmacen': $("#cboAlmacen").val(),
                                    'IdTipoDocumento': 1340,
                                    'IdSerie': $("#cboSerieExtorno").val(),
                                    'Correlativo': '',
                                    'IdMoneda': $("#cboMoneda").val(),
                                    'TipoCambio': $("#txtTipoCambio").val(),
                                    'FechaContabilizacion': $("#FechContExtorno").val(),
                                    'FechaDocumento': $("#FechDocExtorno").val(),
                                    'IdCentroCosto': 7,
                                    'Comentario': $("#txtComentarios").val(),
                                    'SubTotal': SubTotal,
                                    'Impuesto': Impuesto,
                                    'Total': Total,
                                    'IdCuadrilla': 2582,
                                    'EntregadoA': 24151,
                                    'IdTipoDocumentoRef': 10,
                                    'NumSerieTipoDocumentoRef': 'Extorno de la Entrega ' + $("#cboSerie option:selected").text() + '-' + $("#txtNumeracion").val(),
                                    'IdDestinatario': '',
                                    'IdMotivoTraslado': '',
                                    'IdTransportista': '',
                                    'PlacaVehiculo': '',
                                    'MarcaVehiculo': '',
                                    'NumIdentidadConductor': '',

                                    'NombreConductor': '',
                                    'ApellidoConductor': '',
                                    'LicenciaConductor': '',
                                    'TipoTransporte': '',

                                    'Peso': 0,
                                    'Bulto': 0,

                                    'SGI': '',
                                    'CodigoAnexoLlegada': '',
                                    'CodigoUbigeoLlegada': '',
                                    'DistritoLlegada': '',
                                    'DireccionLlegada': ''


                                },
                                //beforeSend: function () {
                                //    Swal.fire({
                                //        title: "Cargando Cambios de Extorno...",
                                //        text: "Por favor espere",
                                //        showConfirmButton: false,
                                //        allowOutsideClick: false
                                //    });
                                //},
                                success: function (data) {
                                    if (data > 0) {
                                        $.post('ExtornoConfirmado', {
                                            'IdOPDN': IdOPDN,
                                            'EsServicio': TipoProductos,
                                        }, function (data, status) {

                                            if (data != 0) {
                                                swal("Exito!", "Proceso Realizado Correctamente", "success")
                                                CerrarModal()
                                                listaropdnDT()
                                            } else {
                                                swal("Error!", "Ocurrio un Error")
                                                CerrarModal()
                                            }

                                        });


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
                            $.post('ExtornoConfirmado', {
                                'IdOPDN': IdOPDN,
                                'EsServicio': TipoProductos,
                            }, function (data, status) {

                                if (data != 0) {
                                    swal("Exito!", "Proceso Realizado Correctamente", "success")
                                    CerrarModal()
                                    listaropdnDT()
                                } else {
                                    swal("Error!", "Ocurrio un Error")
                                    CerrarModal()
                                }

                            });
                        }
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
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerieExtorno(series, "cboSerieExtorno", "Seleccione")
    });
}

function AbrirOC(IdPedido) {
    Swal.fire({
        title: "Buscando Orden de Compra...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

                $.ajaxSetup({ async: false });
                $.post("/Pedido/GenerarReporte", { 'NombreReporte': 'OrdenCompra', 'Formato': 'PDF', 'Id': IdPedido }, function (data, status) {
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
                        Swal.fire(
                            'Correcto',
                            'Orden Encontrada',
                            'success'
                        )
                    } else {
                        respustavalidacion
                    }
                });       
       
    }, 100)
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

function GenerarReporteOPDN(IdOPDN) {
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        $.ajaxSetup({ async: false });
        $.post("GenerarReporteOPDN", { 'NombreReporte': 'EntregaMercancia', 'Formato': 'PDF', 'IdOPDN': IdOPDN }, function (data, status) {
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
                Swal.fire(
                    'Correcto',
                    'Reporte Generado Correctamente',
                    'success'
                )
            } else {
                Swal.fire(
                    'Error',
                    'Ocurrió un error',
                    'error'
                )
            }
        });

    }, 100)
}