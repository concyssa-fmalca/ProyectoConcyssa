let tablepedido = "";
let tableentrega;
let tablefacturaprovedor;
let contarclick = 0;
var ultimaFila = null;
var colorOriginal;
let limitador = 0;
let valorfor = 1;
let UltimoTipoRegistro = 0;
let UltimaSemana = 0;
let UltimoGiro = 0;

function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
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
function CambiarClaseArticulo() {
    
    let ClaseArticulo = $("#cboClaseArticulo").val();

    if (ClaseArticulo == "2") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
        $("#cboTipoDocumentoOperacion").val(1338)
        $("#txtDescripcionItem").prop("disabled", false)
    } else if(ClaseArticulo == "3") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
        $("#cboTipoDocumentoOperacion").val(18)
    } else {
        $("#IdTipoProducto").show();
        $("#IdTipoProducto").val(0);
        $("#cboTipoDocumentoOperacion").val(18)
    }

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
    //listarOpch();
}

function CargarGiros() {
    let obraGiro = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/GestionGiro/ObtenerGiroAprobado", {'IdObra':obraGiro}, function (data, status) {
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>"+ lista[i].NumeroDocumento +" - "+  lista[i].RazonSocial + "</option>"; }
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

function ObtenerCuadrillas() {
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrilla", { 'estado': 1}, function (data, status) {
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
    $("#cboAlmacen").prop("selectedIndex", 1)
    $("#cboAlmacenItem").prop("selectedIndex", 1)
    ObtenerCuadrillasxIdObra(IdObra)
    
}
function ObtenerCuadrillasxIdObra(IdObra) {
    IdObra = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        console.log("OBRA ES : "+IdObra)
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrilla(cuadrilla, "IdCuadrilla", "Seleccione")
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
    $("#IdObra").prop("selectedIndex", 1).change()
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
    $("#DivGiro").hide()
    CargarProveedorFiltro()
    CargarObras()
    CargarTipoRegistroFiltro()
    //$("#btn_nuevo_proveedor").prop("disabled", true);
    ObtenerConfiguracionDecimales();
    CargarBaseFiltro()
    var url = "../Movimientos/ObtenerMovimientosIngresos";
    $("#IdResponsable").select2();
    ObtenerConfiguracionDecimales();
    $("#IdCuadrilla").select2()

    DecimalesCantidades = GDecimalesCantidades;
    DecimalesImportes = GDecimalesImportes;
    DecimalesPrecios = GDecimalesPrecios;
    DecimalesPorcentajes = GDecimalesPorcentajes;


    //KeyPressNumber($("#txtRedondeo"));

    $("#IdProveedor").select2();

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
    contador = 0
    CargarTasaDet()
    CargarGrupoDet()
    $("#IdProveedor").val(0).change()
    $("#Direccion").val("").change()
    arrayCboCuadrillaTabla = []
    arrayCboResponsableTabla = []
    $("#btnEditar").hide()
    $("#btnExtornar").hide()
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    $("#txtFechaDocumento").val(today)
    $("#txtFechaContabilizacion").val(today)
    $("#cboGlosaContable").prop("disabled",false)
    $("#IdPedido").val(0);
    $("#IdOPDN").val(0);
    disabledmodal(false)
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }



    $("#lblTituloModal").html("Nueva Factura Proveedor");
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()
    //CargarAlmacen()

    CargarTipoDocumentoOperacion()
    //setTimeout(() => {
    //    ObtenerCuadrillasxIdObra()
    //}, 100);

    CargarSeries();
    CargarGlosaContable();

    //AgregarLinea();




    CargarSolicitante(1);
    //CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();

    CargarImpuestos();
    $("#cboImpuesto").val(2).change();
    $("#IdTipoDocumentoRef").val(2).change();


    //$("#cboSerie").val(1).change();

    $("#cboMoneda").val(1);
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#IdTipoProducto").prop("disabled", false);
    $("#cboClaseArticulo").val(0);
    $("#IdTipoProducto").val(0);
    $("#total_items").html("");
    $("#NombUsuario").html("");
    $("#CreatedAt").html("");
    $("#SerieNumeroRef").val("");
    AbrirModal("modal-form");
    CargarProveedor();
    CargarCondicionPago();
    CargarTipoRegistro();
    CargarSemana();

    console.log("varIdTipoRegistro")
    console.log(UltimoTipoRegistro)
    $("#IdTipoRegistro").val(UltimoTipoRegistro).change()
    $("#IdSemana").val(UltimaSemana).change()
    $("#IdGiro").val(UltimoGiro).change()

    $("#IdCuadrilla").val(1008).change();
    $("#IdResponsable").val(10781).change();
    $("#IdCondicionPago").val(1).change();

    
    
}


function OpenModalItem() {
    $("#btn_nuevo_proveedor").prop("disabled", false)
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
            $("#divTipoServicio").show();
            $("#SNinguno").prop('checked', true) 
            $("#txtStockAlmacenItem").hide();
            $("#lblStockItem").hide();
            $("#IdResponsable").prop("disabled", false)
            $("#IdCuadrilla").prop("disabled", false)
          
           
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
            $("#IdCuadrilla").val(0).change();
            $("#IdResponsable").prop("disabled",true)
            $("#IdCuadrilla").prop("disabled",true)

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
            $("#IdCuadrilla").val(0).change();
            $("#IdResponsable").prop("disabled", true)
            $("#IdCuadrilla").prop("disabled", true)
        }
        $("#cboAlmacenItem").val($("#cboAlmacen").val()); // que salga el almacen por defecto


        $("#cboPrioridadItem").val(2);
        $("#cboClaseArticulo").prop("disabled", true);
        $("#IdTipoProducto").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarGrupoUnidadMedida();
        CargarIndicadorImpuesto();
        /* CargarProyectos();*/
        //CargarCentroCostos();
        //CargarAlmacen();
        $("#cboClaseArticulo").prop("disabled", false)
        $("#IdIndicadorImpuesto").val(1)
        if ($("#IdTipoDocumentoRef").val() == 11) {
            $("#IdIndicadorImpuesto").val(2)
        }
     
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
            <td><button type="button" class="btn  btn-danger btn-xs borrar fa fa-trash" onclick="EliminarAnexoEnMemoria(`+ contadorAnexo +`)"></button></td>
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
            <td><button class="btn btn-xs btn-danger fa fa-trash" onclick="EliminarAnexo(event, `+ Id + `,this)"></button></td>
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


function EliminarAnexo(event, Id, dato) {
    eventDefault(event);
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

let arrayItemsAgregados = new Array();


function AgregarLinea() {
    $("#txtOrigen").val("Factura")
    $("#txtOrigenId").val("0 ")

  
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
    console.log("impuesto arriba:"+IdIndicadorImpuesto )
    //txtReferenciaItem
    let TipoServicio 

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
    let ValidarImpuesto = $("#IdIndicadorImpuesto").val();
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


    if (ValidarImpuesto == 0) {
        swal("Informacion!", "Debe Seleccionar Indicador de Impuesto!");
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

    //for (var i = 0; i < arrayItemsAgregados.length; i++) {
    //    if (arrayItemsAgregados[i] == $("#txtCodigoItem").val()) {
    //        swal("Informacion!", "El Item Ya Fue Agregado!");
    //        return;
    //    }
    //}

    arrayItemsAgregados.push($("#txtCodigoItem").val())



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
    //if (limitador >= 30) {
    //    swal("Informacion!", "Solo se pueden agregar Hasta 30 items");
    //    return;
    //}
    for (var J = 0; J < valorfor; J++) {
        console.log("VUELTAAAAAAAAAAA: " + J)

        limitador++
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
            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" ></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" ></td>
            <td input>
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuestoDetalle[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
        tr += `  <option impuesto="0" value="0">Seleccione</option>`;
        for (var i = 0; i < IndicadorImpuesto.length; i++) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
<td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador + `)" id="cboCuadrillaTablaId` + contador + `"></select></td>
            <td><select class="form-control cboResponsableTabla" id="cboResponsableTablaId`+ contador + `"></select></td>
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



        $("#txtIdArticulo" + contador).val(IdItem);
        $("#txtCodigoArticulo" + contador).val(CodigoItem);
        $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
        $("#cboUnidadMedida" + contador).val(MedidaItem);
        //$("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
        $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();

        $("#cboProyecto" + contador).val(ProyectoItem);
        $("#cboAlmacen" + contador).val(AlmacenItem);
        $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
        $("#txtTipoServicio" + contador).val(TipoServicio);
        $("#cboCentroCostos" + contador).val(CentroCostoItem);
        $("#txtReferencia" + contador).val(ReferenciaItem);
        $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
        $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);
        console.log("impuesto" + IdIndicadorImpuesto)
        CalcularTotalDetalle(contador)
        LimpiarModalItem();
        NumeracionDinamica();
        $("#cboClaseArticulo").prop("disabled", true)
        if ($("#cboClaseArticulo").val() != 2) {
            for (var i = 0; i < 50; i++) {
                $("#txtDescripcionArticulo" + i).prop("disabled", true)
            }
        }
        ObtenerCuadrillasTabla(contador)
        ObtenerEmpleadosxIdCuadrillaTabla(contador)
        $(".cboCuadrillaTabla").select2()
        $(".cboResponsableTabla").select2()
        $("#cboCuadrillaTablaId").val($("#IdCuadrilla").val()).change()
        $("#cboResponsableTablaId").val($("#IdResponsable").val()).change()
    }
    changeTipoDocumento()
}


//function LimpiarDatosModalItems() {

//}
function restarLimitador() {
    limitador = limitador - 1
}

function borrartditem(contador) {
    $("#tritem" + contador).remove()
    CalcularTotales();
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
        if (lista[i].Documento == 6) {
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
    limpiarDatos();
    $("#cboClaseArticulo").prop("disabled", false)
    $("#IdTipoProducto").prop("disabled", false)
    $("#btn_agregar_item").prop("disabled", false)
    $("#IdProveedor").prop("disabled", false)
    $("#Direccion").prop("disabled", false)
    $("#IdCondicionPago").prop("disabled", false)
    $("#Telefono").prop("disabled", false)
    $("#cboClaseArticulo").val(0)
    $("#IdTipoProducto").val(0)
    arrayCboCuadrillaTabla = []
    arrayCboResponsableTabla = []

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
        //console.log(dato);
        $("#txtTipoCambio").val(dato.venta);

    });


    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
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

    if (validarseriescontableParaCrear() == false) {
        Swal.fire("Error!", "No Puede Crear este Documento en una Fecha No Habilitada", "error")
        return
    }

    UltimoTipoRegistro = $("#IdTipoRegistro").val()
    UltimaSemana = $("#IdSemana").val()
    UltimoGiro = $("#IdGiro").val()


    if ($("#IdTipoRegistro").val() != 4) {
        //function GuardarSolicitud() PROVISION Y CAJA CHICA

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

        if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Proveedor',
                'error'
            )
            return;
        }

        if ($("#SerieNumeroRef").val().length == 0) {
            Swal.fire(
                'Error!',
                'Complete el campo de Serie y Número',
                'error'
            )
            return;
        }

        if ($("#IdTipoDocumentoRef").val() == "2" || $("#IdTipoDocumentoRef").val() == "11" || $("#IdTipoDocumentoRef").val() == "13" || $("#IdTipoDocumentoRef").val() == "16") {
            let DocRef = $("#SerieNumeroRef").val()
            if (!DocRef.includes("-")) {
                Swal.fire(
                    'Error!',
                    'El Formato de Serie y Número para este Tipo de Doc. no es Correcto, debe contener [SERIE]-[NUM]',
                    'error'
                )
                return;
            }
            if (DocRef.split("-")[0].length != 4) {
                Swal.fire(
                    'Error!',
                    'El Formato de Serie no es Correcto, debe tener 4 caracteres. Emeplo F001',
                    'error'
                )
                return;
            }
            if (DocRef.split("-")[1].length  == 0) {
                Swal.fire(
                    'Error!',
                    'El Formato de Correlativo no es Correcto, debe tener al menos un valor',
                    'error'
                )
                return;
            }
        }

        //if ($("#divDetraccion").is(':visible')) {
        //    if ($("#TasaDet").val() == 0 || $("#TasaDet").val() == null) {
        //        Swal.fire(
        //            'Error!',
        //            'Complete el campo de Tasa Detracción',
        //            'error'
        //        )
        //        return;
        //    }
        //    if ($("#CondicionPagoDet").val() == 0 || $("#CondicionPagoDet").val() == null) {
        //        Swal.fire(
        //            'Error!',
        //            'Complete el campo de Condición Pago Detracción',
        //            'error'
        //        )
        //        return;
        //    }
        //    if ($("#GrupoDet").val() == 0 || $("#GrupoDet").val() == null) {
        //        Swal.fire(
        //            'Error!',
        //            'Complete el campo de Grupo detracción',
        //            'error'
        //        )
        //        return;
        //    }
        //}
        //if ($("#divServPub").is(':visible')) {
        //    if (($("#consumomM3").val() == 0 || $("#consumomM3").val() == null) && ($("#consumomHW").val() == 0 || $("#consumomHW").val() == null)  )    {
        //        Swal.fire(
        //            'Error!',
        //            'Complete al menos uno de los campos de Consumo',
        //            'error'
        //        )
        //        return;
        //    }

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
        let cantidadError = 0;
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
        let IdCentroCosto = $("#cboCentroCosto").val();
        let Comentario = $("#txtComentarios").val();
        let SubTotal = ($("#txtTotalAntesDescuento").val()).replace(/,/g, "");
        let Impuesto = ($("#txtImpuesto").val()).replace(/,/g, "");
        let Total = ($("#txtTotal").val()).replace(/,/g, "");
        let TablaOrigen = $("#txtOrigen").val();
        let IdOrigen = $("#txtOrigenId").val();
        IdOrigen = IdOrigen.substring(0, IdOrigen.length - 1);

        let Redondeo = StringReplace($("#txtRedondeo").val(), ',', '');


        let IdCuadrilla = $("#IdCuadrilla").val();

        let IdResponsable = $("#IdResponsable").val();
        let IdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
        let SerieNumeroRef = $("#SerieNumeroRef").val();

        let varConsumoM3 = $("#consumomM3").val()
        let varConsumoHW = $("#consumomHW").val()
        let varTasaDet = $("#TasaDet").val()
        let varGrupoDet = $("#GrupoDet").val()
        let varSerieSAP = $("#txtSerieSAP").val()
        let varCondPagoDet = $("#CondicionPagoDet").val()
        //END Cabecera

        if ($("#cboTipoDocumentoOperacion").val() == '1338') {
            for (var i = 0; i < arrayCboCuadrillaTabla.length; i++) {
                if (arrayCboCuadrillaTabla[i] == 0) {
                    Swal.fire("Advertencia", "Para Una Factura de Servicios el campo Cuadrilla es obligatorio para cada Item", "info")
                    return;
                }
            }
        }

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
                    'NombTablaOrigen': NombTablaOrigen,
                    'IdOrigen': arraytxtIdOrigen[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],
                    'TipoServicio': arrayTipoServicio[i],




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
            'IdCentroCosto': IdCentroCosto,
            'IdGlosaContable': $("#cboGlosaContable").val(),

            'Comentario': Comentario,
            'SubTotal': SubTotal,
            'Impuesto': Impuesto,
            'Redondeo': Redondeo,
            'Total': Total,
            'IdCuadrilla': 0,
            'IdResponsable': 0,
            'IdTipoDocumentoRef': IdTipoDocumentoRef,
            'NumSerieTipoDocumentoRef': SerieNumeroRef,
            'IdProveedor': $("#IdProveedor").val(),
            'IdCondicionPago': $("#IdCondicionPago").val(),
            'IdTipoRegistro': $("#IdTipoRegistro").val(),
            'IdSemana': $("#IdSemana").val(),
            'TablaOrigen': TablaOrigen,
            'IdOrigen': IdOrigen,
            'ConsumoM3': varConsumoM3,
            'ConsumoHW': varConsumoHW,
            'TasaDetraccion': varTasaDet,
            'GrupoDetraccion': varGrupoDet,
            'SerieSAP': varSerieSAP,
            'CondicionPagoDet': varCondPagoDet
        })


        $.ajax({
            url: "UpdateInsertMovimientoFacturaProveedorString",
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
                if (data > 0) {
                    $.post("ObtenerSerieOPCH", { 'IdOPCH': data }, function (data, status) {
                        Swal.fire(
                            'Correcto',
                            'Documento '+data+' Registrado Correctamente',
                            'success'
                        )
                        CerrarModal();                      
                        listarOpch()
                    });
                   

                } else if (data == -1) {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error al Grabar La Factura!',
                        'error'
                    )

                } else if (data == -2 || data == -3) {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error al Grabar los Detalles!',
                        'error'
                    )

                }
                else if (data == -5) {
                    Swal.fire(
                        'Información!',
                        'La Factura ya fue registrada previamente!',
                        'info'
                    )

                } else {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error!, ' + data,
                        'error'
                    )
                }



            }
        }).fail(function () {
            Swal.fire(
                'Error!',
                'Error en el Controlador </br> Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
                'error'
            )
        });
        arrayCboCuadrillaTabla = []
        arrayCboResponsableTabla = []

    } else {
        //function GuardarSolicitud() RENDICION

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

        if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Proveedor',
                'error'
            )
            return;
        }

        if ($("#SerieNumeroRef").val().length == 0) {
            Swal.fire(
                'Error!',
                'Complete el campo de Serie y Número',
                'error'
            )
            return;
        }
        if ($("#IdTipoDocumentoRef").val() == "2" || $("#IdTipoDocumentoRef").val() == "11" || $("#IdTipoDocumentoRef").val() == "13" || $("#IdTipoDocumentoRef").val() == "16") {
            let DocRef = $("#SerieNumeroRef").val()
            if (!DocRef.includes("-")) {
                Swal.fire(
                    'Error!',
                    'El Formato de Serie y Número para este Tipo de Doc. no es Correcto, debe contener [SERIE]-[NUM]',
                    'error'
                )
                return;
            }
            if (DocRef.split("-")[0].length != 4) {
                Swal.fire(
                    'Error!',
                    'El Formato de Serie no es Correcto, debe tener 4 caracteres. Emeplo F001',
                    'error'
                )
                return;
            }
            if (DocRef.split("-")[1].length == 0) {
                Swal.fire(
                    'Error!',
                    'El Formato de Correlativo no es Correcto, debe tener al menos un valor',
                    'error'
                )
                return;
            }
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
        let cantidadError = 0;
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

        let arrayTipoServicio = new Array();
        $("input[name='txtTipoServicio[]']").each(function (indice, elemento) {
            arrayTipoServicio.push($(elemento).val());
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
        let TablaOrigen = $("#txtOrigen").val();
        let IdOrigen = $("#txtOrigenId").val();
        IdOrigen = IdOrigen.substring(0, IdOrigen.length - 1);
        let Redondeo = StringReplace($("#txtRedondeo").val(), ',', '');


        let IdCuadrilla = $("#IdCuadrilla").val();

        let IdResponsable = $("#IdResponsable").val();
        let IdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
        let SerieNumeroRef = $("#SerieNumeroRef").val();


        let varConsumoM3 = $("#consumomM3").val()
        let varConsumoHW = $("#consumomHW").val()
        let varTasaDet = $("#TasaDet").val()
        let varGrupoDet = $("#GrupoDet").val()
        let varSerieSAP = $("#txtSerieSAP").val()
        let varCondPagoDet = $("#CondicionPagoDet").val()
        //END Cabecera

        if ($("#cboTipoDocumentoOperacion").val() == '1338') {
            for (var i = 0; i < arrayCboCuadrillaTabla.length; i++) {
                if (arrayCboCuadrillaTabla[i] == 0) {
                    Swal.fire("Advertencia", "Para Una Factura de Servicios el campo Cuadrilla es obligatorio para cada Item", "info")
                    return;
                }
            }
        }

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
                    'NombTablaOrigen': NombTablaOrigen,
                    'IdOrigen': arraytxtIdOrigen[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],
                    'TipoServicio': arrayTipoServicio[i],

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
            'IdCentroCosto': IdCentroCosto,
            'IdGlosaContable': $("#cboGlosaContable").val(),

            'Comentario': Comentario,
            'SubTotal': SubTotal,
            'Impuesto': Impuesto,
            'Redondeo': Redondeo,
            'Total': Total,
            'IdCuadrilla': 0,
            'IdResponsable': 0,
            'IdTipoDocumentoRef': IdTipoDocumentoRef,
            'NumSerieTipoDocumentoRef': SerieNumeroRef,
            'IdProveedor': $("#IdProveedor").val(),
            'IdCondicionPago': $("#IdCondicionPago").val(),
            'IdTipoRegistro': $("#IdTipoRegistro").val(),
            'IdSemana': $("#IdGiro").val(),
            'TablaOrigen': TablaOrigen,
            'IdOrigen': IdOrigen,
            'ConsumoM3': varConsumoM3,
            'ConsumoHW': varConsumoHW,
            'TasaDetraccion': varTasaDet,
            'GrupoDetraccion': varGrupoDet,
            'SerieSAP': varSerieSAP,
            'CondicionPagoDet': varCondPagoDet
        })

        $.ajax({
            url: "UpdateInsertMovimientoFacturaProveedorString",
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
                if (data > 0) {
                     $.post("ObtenerSerieOPCH", { 'IdOPCH': data }, function (data, status) {
                        Swal.fire(
                            'Correcto',
                            'Documento '+data+' Registrado Correctamente',
                            'success'
                        )
                        CerrarModal();                      
                        listarOpch()
                    });
                } else if (data == -1) {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error al Grabar La Factura!',
                        'error'
                    )
                } else if (data == -2 || data == -3) {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error al Grabar los Detalles!',
                        'error'
                    )
                }
                else if (data == -5) {
                    Swal.fire(
                        'Información!',
                        'La Factura ya fue registrada previamente!',
                        'info'
                    )
                } else {
                    Swal.fire(
                        'Error!',
                        'Ocurrio un Error!, ' + data,
                        'error'
                    )
                }
                        

            }
        }).fail(function () {
             Swal.fire(
                 'Error!',
                 'Error en el Controlador </br> Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
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
    $("#txtOrigen").val("");
    $("#txtOrigenId").val("");
    $("#consumomM3").val(0);
    $("#consumomHW").val(0);
    $("#TasaDet").val(0);
    $("#GrupoDet").val(0);
    $("#txtSerieSAP").val(0);
    $("#CondicionPagoDet").val(0);
    arrayItemsAgregados = []
    limitador = 0;
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

function todecimal(numero) {
    return numero ? parseFloat(numero) : 0
}


function CalcularTotales() {
    
    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array
    let redondeo = todecimal(StringReplace($("#txtRedondeo").val(), ',', ''),);

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
    let subTotalRedondeo = 0;

    let impuesto = 0;
    let total = 0;

    for (var i = 0; i < arrayPrecioInfo.length; i++) {
        subtotal += (arrayCantidadNecesaria[i] * arrayPrecioInfo[i]);
        total += arrayTotal[i];
    }
    impuesto = total - subtotal;
    total = total + redondeo;

    $("#txtTotalAntesDescuento").val(formatNumber(subtotal.toFixed(2)));
    $("#txtImpuesto").val(formatNumber(impuesto.toFixed(2)));
    $("#txtTotal").val(formatNumber(total.toFixed(2)));


    //changeTipoDocumento()
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
            <td style="display:none"><input style="200px" class="form-control txtIdFacturaDetalle" value="`+ detalle.IdOPCHDetalle+`" ></input></td>



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
            <button type="button" class="btn-sm btn btn-danger borrar fa fa-trash " disabled></button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);
    console.log("CONTADOR :" + contador)
    ObtenerCuadrillasTabla(contador)
    ObtenerEmpleadosxIdCuadrillaTabla(contador)
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
    $("#cboCuadrillaTablaId"+contador).val(detalle.IdCuadrilla).change()
    $("#cboResponsableTablaId"+contador).val(detalle.IdResponsable).change()
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
function SeleccionTrItem(ItemCodigo) {

    $("#rdSeleccionado" + ItemCodigo).prop("checked", true);

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
        if ($("#IdTipoDocumentoRef").val() == 11) {
            $("#IdIndicadorImpuesto").val(2)
        }
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
    if ($("#IdProveedor").val() == 0) {
        Swal.fire("Aviso", "Debe Seleccionar un Proveedor", "info")
        return
    }

    $("#ModalListadoPedido").modal();
    listarpedidosdt()
}

function AbrirModalEntregas() {
    if ($("#IdProveedor").val() == 0) {
        Swal.fire("Aviso", "Debe Seleccionar un Proveedor", "info")
        return
    }
    $("#cboMedidaItem").val(null);
    $("#ModalListadoEntrega").modal();
    listarentregadt()
}

function listarentregadt() {
    tableentrega = $('#tabla_listado_entregas').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '/EntradaMercancia/ListarOPDNDTModalOPCH',
            type: 'POST',
            data: {
                'IdProveedor' : $("#IdProveedor").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},

            {
                data:null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox" id="checkIdOpdn` + full.IdOPDN + `" value="` + full.IdOPDN + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckOPDN()" class="checkIdOpdn"> <label for="cbox2"></label>`
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaDocumento.split("T")[0].toString()
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<a style="color:blue !important;text-decoration:underline;cursor:pointer" onclick="AbrirOPDN(' + full.IdOPDN + ')" >'+ full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombProveedor
                },
            },
            {
                data: null,
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
        for (var i = 0; i < 50; i++) {
            borrartditem(i)
        }
        AgregarOPNDDetalle(data);
        console.log(data)
        $("#txtOrigen").val("Entrega")
        $("#txtOrigenId").val(data.IdOPDN + " ")
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
                'IdProveedor': $("#IdProveedor").val(),
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
                    return `
                        <input type="checkbox" id="CheckIdPedido`+ full.IdPedido + `" value="` + full.IdPedido + `" IdProveedor="` + full.IdProveedor + `" onchange="ValidacionesCheckPedido()" class="checkIdPedidos"> <label for="cbox2"></label>`
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<a style="color:blue !important;text-decoration:underline;cursor:pointer" onclick="VerOC(' + full.IdPedido + ')">' + full.NombSerie + '-' + full.Correlativo +'</a>' 
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombreProveedor.toUpperCase()
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.total_venta
                },
            }


        ],
        "bDestroy": true
    }).DataTable();

    //$('#tabla_listado_pedidos tbody').on('dblclick', 'tr', function () {
    //    var data = tablepedido.row(this).data();
    //    console.log(data);
    //    if (ultimaFila != null) {
    //        ultimaFila.css('background-color', colorOriginal)
    //    }
    //    colorOriginal = $("#" + data["DT_RowId"]).css('background-color');
    //    $("#" + data["DT_RowId"]).css('background-color', '#dde5ed');
    //    ultimaFila = $("#" + data["DT_RowId"]);
    //    AgregarPedidoToEntradaMercancia(data);
    //    $('#ModalListadoPedido').modal('hide');
    //    tablepedido.ajax.reload()
    //    //$("#tbody_detalle").find('tbody').empty();
    //    //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    //});
}

function AgregarPedidoToEntradaMercancia(data) {
    console.log(data)
    for (var i = 0; i < 50; i++) {
        borrartditem(i)
    }
    $("#IdPedido").val(data['IdPedido']);
    $("#cboMoneda").val(data.IdMoneda).change();
    if ($("#IdOPDN").val() != 0) {
        swal("Informacion!", "Solo puede agregar Entrega de Mercancia!");
        $("#IdPedido").val(0)
        return;
    }
    
    

    $("#IdBase").val(data['IdBase']).change();
    let TipoPedido = (data['NombTipoPedido'])
    $("#IdObra").val(data['IdObra']).change();
    $("#cboAlmacen").val(data['IdAlmacen']).change();
    $("#cboClaseArticulo").val(data['IdTipoPedido']).change();
    $("#IdProveedor").val(data['IdProveedor']).change();
    let serieynumero = data['NombSerie'] + '-' + data['Correlativo'];
    $.ajaxSetup({ async: false });
    $.post("/Pedido/ObtenerPedidoDetalle", { 'IdPedido': data['IdPedido'] }, function (data, status) {
        let datos = JSON.parse(data);
        let totalIGV = 0;
        let TotalFinal = 0;
        console.log(datos);
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
            let CodigoItem = datos[k]['CodigoProducto'];
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
            <td>
                <input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">
                 <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" >
                <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdPedidoDetalle'] + `" id="txtIdOrigen` + contador + `" >
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
            <td><select class="form-control cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador +`)" id="cboCuadrillaTablaId`+ contador +`"></select></td>
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
            <td ><input class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></td>
<td style="display:none"><input style="width:50px" class="form-control" type="text" value="" id="txtTipoServicio`+ contador + `" name="txtTipoServicio[]"></input></td>
<td><button class="btn btn-danger btn-xs  borrar fa fa-trash" onclick="borrartditem(`+ contador + `)">-</button></td>
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
            $("#txtCantidadNecesaria" + contador).val(((CantidadItem))).change();

            //$("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat(CantidadItem).toFixed(DecimalesCantidades))).change();
            $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto);
            $("#txtTipoServicio" + contador).val(TipoServicio);


            $("#txtItemTotal"+ contador).val((datos[k].total_item).toFixed(DecimalesPrecios))
            $("#cboCentroCostos" + contador).val(CentroCostoItem);
            $("#txtReferencia" + contador).val('BASADO EN PEDIDOS: ' + serieynumero);
            ObtenerCuadrillasTabla(contador)
            ObtenerEmpleadosxIdCuadrillaTabla(contador)
            $(".cboCuadrillaTabla").select2()
            $(".cboResponsableTabla").select2()
            totalIGV += datos[k].total_igv;
            TotalFinal += datos[k].total_item 
            LimpiarModalItem();
            NumeracionDinamica();
                ObtenerCuadrillasTabla(contador)
    ObtenerEmpleadosxIdCuadrillaTabla(contador)
    $(".cboCuadrillaTabla").select2()
    $(".cboResponsableTabla").select2()
        }
        $("#txtImpuesto").val(totalIGV);
        console.log(datos);
        $("#txtTotal").val((TotalFinal).toFixed(DecimalesPrecios))
        console.log("agregado total con ig")
        if (TipoPedido == 'Orden Compra') {
            $(".cboCuadrillaTabla").val(0).change()
        }
        /*AGREGAR LINEA*/

    });


}



function AgregarOPNDDetalle(data) {
    console.log(data)
    $("#IdOPDN").val(data['IdOPDN']);
    $("#IdBase").val(data['IdBase']).change();
    $("#cboMoneda").val(data.IdMoneda).change();
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

            console.log(datos);

            console.log(datos[k]['TipoServicio'].toUpperCase())

            if (datos[k]['TipoServicio'].toUpperCase() == 'NO APLICA') {
                $("#cboClaseArticulo").val(1).change()
            } else {
                $("#cboClaseArticulo").val(2).change()
            }

            console.log('VALOR TIPODOC')
            console.log($("#cboClaseArticulo").val())


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
            let CodigoItem = datos[k]['CodigoArticulo'];
            let MedidaItem = datos[k]['IdDefinicionGrupoUnidad'];
            let DescripcionItem = datos[k]['DescripcionArticulo'];
            let PrecioUnitarioItem = datos[k]['valor_unitario'];
            let CantidadItem = datos[k]['Cantidad'] - datos[k]['CantidadUsada'] - datos[k]['CantidadDevolucion'];

            let CantidadMaxima = datos[k]['Cantidad'] - datos[k]['CantidadUsada'] - datos[k]['CantidadDevolucion'];
            let ProyectoItem = datos[k]['IdArticulo'];
            let CentroCostoItem = datos[k]['IdArticulo'];
            let ReferenciaItem = datos[k]['IdArticulo'];
            let AlmacenItem = datos[k]['IdArticulo'];
            let PrioridadItem = datos[k]['IdArticulo'];
            let IdGrupoUnidadMedida = datos[k]['IdGrupoUnidadMedida'];
            let IdIndicadorImpuesto = datos[k]['IdIndicadorImpuesto'];
            let TipoServicio = datos[k]['TipoServicio']
            let IdCuadrillaParaTabla = 0
            let IdResponsableParaTabla = 0

            if (datos[k]['IdCuadrilla'] != 0) {
                IdCuadrillaParaTabla = datos[k]['IdCuadrilla']
            }
            if (datos[k]['IdResponsable'] != 0) {
                IdResponsableParaTabla = datos[k]['IdResponsable']
            }
            if (datos[k]['TipoServicio'] != 'No Aplica') {
                $("#cboClaseArticulo").val(2)
            }


            //txtReferenciaItem


            if (Number(CantidadItem) == 0) {
                continue;
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
            //let ValidartProducto = $("#cboMedidaItem").val();/*d*/
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
            //if (ValidartProducto == 0) { /*d*/
            //    swal("Informacion!", "Debe Seleccionar Producto!");
            //    return;
            //}

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
            <td>
                <input class="form-control" type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `);CalculaCantidadMaxima(` + contador + `)">
                <input type="hidden" name="txtCantidadNecesariaMaxima[]" value="`+ CantidadMaxima + `" id="txtCantidadNecesariaMaxima` + contador + `" disabled >
                <input type="hidden" name="txtIdOrigen[]" value="`+ datos[k]['IdOPDNDetalle'] + `" id="txtIdOrigen` + contador + `" disabled>
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
<td><select class=" cboCuadrillaTabla" onchange="SeleccionarEmpleadosTabla(`+ contador +`)" id="cboCuadrillaTablaId`+ contador + `"></select></td>
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
            <td style="display:none"><input style="width:50px" class="form-control" type="text" value="" id="txtTipoServicio`+ contador + `" name="txtTipoServicio[]"></input></td>
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartditem(`+ contador + `)">-</button></td>
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
            $("#txtCantidadNecesaria" + contador).val(((CantidadItem))).change();
            $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
            $("#cboProyecto" + contador).val(ProyectoItem);
            $("#cboAlmacen" + contador).val(AlmacenItem);
            $("#cboPrioridadDetalle" + contador).val(PrioridadItem);
            $("#cboIndicadorImpuestoDetalle" + contador).val(IdIndicadorImpuesto).change();;
            $("#txtTipoServicio" + contador).val(TipoServicio);
            ObtenerCuadrillasTabla(contador)
            ObtenerEmpleadosxIdCuadrillaTabla(contador)
            $(".cboCuadrillaTabla").select2()
            $(".cboResponsableTabla").select2()
            if (IdCuadrillaParaTabla != 0) {
                $("#cboCuadrillaTablaId" + contador).val(IdCuadrillaParaTabla).change();
                if (IdResponsableParaTabla != 0) {
                    $("#cboResponsableTablaId" + contador).val(IdResponsableParaTabla).change();
                }
            } else {
                $("#cboCuadrillaTablaId" + contador).val(0).change();
            }
            $("#cboCentroCostos" + contador).val(CentroCostoItem);


            LimpiarModalItem();
        }

        /*AGREGAR LINEA*/

    });
    NumeracionDinamica();

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



function listarOpch() {
    let varIdObraFiltro = $("#cboObraFiltro").val()
    let IdRegistro = $("#cboTipoRegistroFiltro").val()
    if (IdRegistro == 0) {
        Swal.fire("Seleccione un Tipo de Registro")
        return
    }
    let IdSemana = 0

    if (IdRegistro != 4) {
        IdSemana = $("#cboSemanaFiltro").val()
        if (IdSemana == 0) {
            Swal.fire("Seleccione una Semana")
            return
        }
    } else {
        IdSemana = $("#cboGiroFiltro").val()
        if (IdSemana == 0) {
            Swal.fire("Seleccione una Giro")
            return
        }
    }
    tablepedido = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarOPCHDT',
            type: 'POST',
            data: {
                'IdObra': varIdObraFiltro,
                'IdTipoRegistro': IdRegistro,
                'IdSemana': IdSemana,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let extrasBtn = ''
                    if (full.ValidadoSUNAT == 0) {
                        extrasBtn = `<button class="btn btn-primary juntos fa fa-share-square btn-xs" onclick="ValidarSUNAT(` + full.IdOPCH + `)"></button>`
                    }

                    return `<button class="btn btn-primary juntos fa fa-eye btn-xs" onclick="ObtenerDatosxIDOPCH(` + full.IdOPCH + `)"></button>
                            <button class="btn btn-primary juntos fa fa-file-text btn-xs" onclick="VerDocsOrigen(` + full.IdOPCH + `,'`+full.NombSerie+`',`+full.Correlativo+`)"></button>`+extrasBtn
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
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoDocumentoOperacion
                },
            },       
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdDocExtorno != 0) {
                        return '<p style="color:red">' + full.NombSerie + '-' + full.Correlativo +'</p>'
                    } else {
                        return '<a style="color:blue;text-decoration:underline;cursor:pointer" onclick="ImprimirFactura(' + full.IdOPCH + ')" > ' + full.NombSerie + '-' + full.Correlativo +'</a>'
                    }
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoDocumentoRef
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef

                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Proveedor
                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.Total, 2)
                },
            },
            {
                data: null,
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                data: null,
                targets: 10,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                data: null,
                targets: 11,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Moneda
                },
            },
            {
                data: null,
                targets: 12,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.ValidadoSUNAT == 0) {
                        return 'NO ENVIADO'
                    } else if (full.ValidadoSUNAT == 1) {
                        return 'VALIDADO'
                    } else {
                        return 'NO EXISTE'
                    }
                },
            },
             {
                data: null,
                targets: 13,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.DocNumCont == -1) {
                        return "<p style='color:red'>ERROR AL VALIDAR<p>"
                    } else if (full.DocNumCont == 0) {
                        return "NO CONTABILIZADO"
                    } else {
                    return "<p style='color:green'>CONTABILIZADO - " + full.DocNumCont+"</p>"
                    }
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
        $("#txtOrigen").val("Pedido")
        $("#txtOrigenId").val(data.IdPedido + " ")
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
        
        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });

    setTimeout(() => {
        changeTipoDocumento()
    },500)
}



function ObtenerDatosxIDOPCH(IdOpch) {
    $("#btnEditar").show()
    $("#txtId").val(IdOpch);
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


    let IdUsuario = 0;

    $("#lblTituloModal").html("Editar Factura Proveedor");
    AbrirModal("modal-form");


    Swal.fire({
        title: "Cargando Datos...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    setTimeout(() => {

    


    $.post('ObtenerDatosxIdOpch', {
        'IdOpch': IdOpch,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            console.log(movimiento);
            IdUsuario = movimiento.IdUsuario,
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(formatNumber(movimiento.SubTotal.toFixed(DecimalesPrecios), DecimalesPrecios))
            $("#txtImpuesto").val(formatNumber(movimiento.Impuesto.toFixed(DecimalesPrecios), DecimalesPrecios))
            $("#txtRedondeo").val(formatNumber(movimiento.Redondeo.toFixed(DecimalesPrecios), DecimalesPrecios))
            $("#txtSerieSAP").val(movimiento.SerieSAP)


            $("#txtTotal").val(formatNumberDecimales(movimiento.Total, DecimalesPrecios))


            $("#cboCentroCosto").val(movimiento.IdCentroCosto)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)
            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)

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
            //if ((movimiento.IdDocExtorno == 0 &&  IdUsuario == +$("#IdUsuarioSesion").val()) || $("#IdPerfilSesion").val() == "1" || $("#IdPerfilSesion").val() == "1022" ) {
            if (movimiento.IdDocExtorno == 0 && IdUsuario == +$("#IdUsuarioSesion").val()  ) {
                $("#btnEditar").show()
                $("#btnExtornar").show()
            } else {
             
                $("#btnEditar").hide()
                $("#btnExtornar").hide()
            }
            $("#IdCondicionPago").val(movimiento.idCondicionPago)
            
            $("#IdTipoRegistro").val(movimiento.IdTipoRegistro)
         
            esRendicion()
            setTimeout(() => {
                $("#IdGiro").val(movimiento.IdSemana)
                $("#IdSemana").val(movimiento.IdSemana)
                
            }, 200)

            console.log(movimiento.IdSemana)
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
            CargarCondPagoDet();

            if ($("#cboTipoDocumentoOperacion").val() == 1338) {
                $("#cboClaseArticulo").val(2)
                changeTipoDocumento()
            } else {
                $("#cboClaseArticulo").val(1)
                changeTipoDocumento()
            }
           
            $("#CondicionPagoDet").val(movimiento.CondicionPagoDet)

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
        Swal.close()

    });
    }, 50)
    disabledmodal(true);
    $("#cboTipoDocumentoOperacion").prop("disabled",false)
    $("#IdTipoDocumentoRef").prop("disabled",false)
    $("#SerieNumeroRef").prop("disabled",false)
    $("#IdCuadrilla").prop("disabled",false)
    $("#IdResponsable").prop("disabled",false)
    $("#IdCondicionPago").prop("disabled",false)
    $("#IdTipoRegistro").prop("disabled",false)
    $("#IdSemana").prop("disabled",false)
    $("#txtComentarios").prop("disabled",false)
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
    $("#txtOrigen").val("Entrega")
    let IdsOrigen = "";
    console.log("inicio borrar")
    for (var i = 0; i < 50; i++) {
        borrartditem(i)
    }
    console.log("fin borrar")
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
                IdsOrigen += data.IdOPDN + " "
                AgregarOPNDDetalle(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo'] + ' / ';
            }
        });

        $("#txtComentarios").html('BASADOS EN LAS ENTREGAS ' + numerospedidos)
        $('#ModalListadoEntrega').modal('hide');
        tableentrega.ajax.reload()
    }
    $("#cboClaseArticulo").prop("disabled", true)
    $("#IdTipoProducto").prop("disabled", true)
    $("#IdTipoProducto").val(0)
    $("#btn_agregar_item").prop("disabled", true)
    $("#IdProveedor").prop("disabled", true)
    //$("#IdCondicionPago").prop("disabled", true)
    $("#Direccion").prop("disabled", true)
    $("#Telefono").prop("disabled", true)
    $("#cboMoneda").prop("disabled", true)
    $("#txtOrigenId").val(IdsOrigen)
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
    let IdsOriginales = ""
    $("#txtOrigen").val("Pedido")
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
                IdsOriginales += data.IdPedido + " "
                AgregarPedidoToEntradaMercancia(data)
                numerospedidos += data['NombSerie'] + '-' + data['Correlativo'] + ' / ';
            }
        });

        $("#txtComentarios").html('BASADOS EN LOS PEDIDOS ' + numerospedidos)
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()
    }
    $("#cboClaseArticulo").prop("disabled", true)
    $("#IdTipoProducto").prop("disabled", true)
    $("#btn_agregar_item").prop("disabled", true)
    $("#IdProveedor").prop("disabled", true)
    //$("#IdCondicionPago").prop("disabled", true)
    $("#Direccion").prop("disabled", true)
    $("#Telefono").prop("disabled", true)
    $("#cboMoneda").prop("disabled", true)
    $("#IdTipoProducto").val(0)
    $("#txtOrigenId").val(IdsOriginales)

    
}

function disabledmodal(valorbolean) {
    $("#IdBase").prop('disabled', valorbolean);
    $("#IdObra").prop('disabled', valorbolean);
    $("#cboAlmacen").prop('disabled', valorbolean);
    $("#cboCentroCosto").prop('disabled', valorbolean);
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
        llenarComboIndicadorImppuesto(indicadorimpuesto, "IdIndicadorImpuesto", null)
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

/* tipoRegistro*/

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
/* semana*/
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

function StringReplace(text, oldText, newText, flags) {
    var r = text;
    if (oldText instanceof Array) for (var i = 0; i < oldText.length; i++) r = r.replace(new RegExp(oldText[i], flags || 'g'), newText);
    else r = r.replace(new RegExp(oldText, flags || 'g'), newText);
    return r;
}
function FormatMiles(num) {
    var a = num.toString().split('.');
    a[0] = a[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return a.join(".");
}
function KeyPressNumber(control) {
    var a = control;

    a.keydown(function (e) {
        var p = +e.which,
            q = (p > 47 && p < 58)
                || (p > 95 && p < 106)
                || (p > 36 && p < 41)
                || ((p == 110 || p == 190) && a.val().toString().indexOf('.') == -1)
                || p == 8 || p == 46;

        if (!q) e.preventDefault();
    });

    a.keyup(function (e) {

        var p = StringReplace(a.val(), ',', '');
        p = FormatMiles(p)

        a.val(p);
        CalcularTotales();
    });
}
function StringReplace(text, oldText, newText, flags) {
    var r = text;
    if (oldText instanceof Array) for (var i = 0; i < oldText.length; i++) r = r.replace(new RegExp(oldText[i], flags || 'g'), newText);
    else r = r.replace(new RegExp(oldText, flags || 'g'), newText);
    return r;
}
function FormatMiles(num) {
    var a = num.toString().split('.');
    a[0] = a[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return a.join(".");
}
function KeyPressNumber(control) {
    var a = control;

    a.keydown(function (e) {
        var p = +e.which,
            q = (p > 47 && p < 58)
                || (p > 95 && p < 106)
                || (p > 36 && p < 41)
                || ((p == 110 || p == 190) && a.val().toString().indexOf('.') == -1)
                || p == 8 || p == 46;

        if (!q) e.preventDefault();
    });

    a.keyup(function (e) {

        var p = StringReplace(a.val(), ',', '');
        p = FormatMiles(p)

        a.val(p);
        CalcularTotales();
    });
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
    $.post("/Empleado/ObtenerEmpleadosPorIdBase", { 'IdBase': $("#IdBase").val()} ,function (data, status) {
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
    //$("#" + idCombo).val(lista[ultimoindice].IdEmpleado).change();
    ObtenerCapataz()
}
function ObtenerCapataz() {
    let IdCuadrilla = $("#IdCuadrilla").val();
    //setTimeout(() => {
    $.post("/Empleado/ObtenerCapatazXCuadrilla", { 'IdCuadrilla': IdCuadrilla }, function (data, status) {
        try {
            let capataz = JSON.parse(data);
            $("#IdResponsable").select2("val", capataz[0].IdEmpleado);
        } catch { } 
    })
    /*  }, 1000);*/


}
function pad(str, max) { str = str.toString(); return str.length < max ? pad("0" + str, max) : str; }
function modalProveedor()
{
    console.log("hola")
    CargarTipoPersona()
    CargarTipoDocumento()
    CargarCondicionPagoMP()
    $("#ModalProveedores").modal();
    $("#txtCodigoMP").val("")
    $("#chkActivoMP").prop("checked", true)
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth()+1, 2) + '-' + f.getDate();
    $("#txtFechaIngresoMP").val(fecha)
}
function CrearCodigo() {
    $("#txtCodigoMP").val("P" + $("#txtNroDocumentoMP").val())
}
function CargarCondicionPagoMP() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarComboCondicionPagoMP(condicionpago, "cboCondicionPagoMP", "Seleccione")
    });
}

function llenarComboCondicionPagoMP(lista, idCombo, primerItem) {
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

function CargarTipoPersona() {
    $.ajaxSetup({ async: false });
    $.post("/TipoPersona/ObtenerTipoPersonas", function (data, status) {
        let tipopersona = JSON.parse(data);
        llenarComboTipoPersona(tipopersona, "cboTipoPersonaMP", "Seleccione")
    });
}

function CargarTipoDocumento() {
    $.ajaxSetup({ async: false });
    $.post("/TipoDocumento/ObtenerTipoDocumentos", function (data, status) {
        let tipodocumento = JSON.parse(data);
        llenarComboTipoDocumento(tipodocumento, "cboTipoDocumentoMP", "Seleccione")
    });
}
function llenarComboTipoPersona(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoPersona + "'>" + lista[i].TipoPersona + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboTipoDocumento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Codigo + "'>" + lista[i].TipoDocumento + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function GuardarProveedor() {
    
    let varIdProveedorMP = $("#txtIdMP").val();
    let varCodigoMP = $("#txtCodigoMP").val();
    let varTipoPersonaMP = $("#cboTipoPersonaMP").val();
    let varTipoDocumentoMP = $("#cboTipoDocumentoMP").val();
    let varNumeroDocumentoMP = $("#txtNroDocumentoMP").val();
    let varRazonSocialMP = $("#txtRazonSocialMP").val();
    let varEstadoContribuyenteMP = "ACTIVO"
    let varCondicionContribuyenteMP = "HABIDO"
    let varDireccionFiscalMP = $("#txtDireccionFiscalMP").val();
    let varTelefonoMP = $("#txtTlf1MP").val();
    let varComprobantesElectronicosMP = $("#txtComprobantesElectronicosMP").val();
    let varAfiliadoPLEMP = $("#txtAfiliadoPLEMP").val();
    let varLineaCreditoMP = $("#txtLineaCreditoMP").val();
    let varEmailMP = $("#txtEmailMP").val();
    let varWebMP = $("#txtWebMP").val();
    let varFaxMP = $("#txtFaxMP").val();
    let varNombreContactoMP = $("#txtNombreContactoMP").val();
    let varTelefonoContactoMP = $("#txtTlfContactoMP").val();
    let varEmailContactoMP = $("#txtEmailContactoMP").val();



    let varFechaIngresoMP = $("#txtFechaIngresoMP").val();


    let varObservacionMP = $("#txtObservacionMP").val();
    let varDepartamentoMP = $("#cboDepartamentoMP").val();
    let varProvinciaMP = $("#cboProvinciaMP").val();
    let varDistritoMP = $("#cboDistritoMP").val();
    let varPaisMP = $("#cboPaisMP").val();
    let varCondicionPagoMP = $("#cboCondicionPagoMP").val();
    let varTipoMP = 2; // proveedor
    let varEstadoMP = false;
    let varAfecto = false;
    let varDiasEntrega = $("#txtDiasEntregaMP").val();
    if ($('#chkActivoMP')[0].checked) {
        varEstadoMP = true;
    }
    if ($('#chk4taMP')[0].checked) {
        varAfecto = true;
    }

    $.post('/Proveedor/UpdateInsertProveedor', {
        'IdProveedor': varIdProveedorMP,
        'CodigoCliente': varCodigoMP,
        'TipoPersona': varTipoPersonaMP,
        'TipoDocumento': varTipoDocumentoMP,
        'NumeroDocumento': varNumeroDocumentoMP,
        'RazonSocial': varRazonSocialMP,
        'EstadoContribuyente': varEstadoContribuyenteMP,
        'CondicionContribuyente': varCondicionContribuyenteMP,
        'DireccionFiscal': varDireccionFiscalMP,
        'Departamento': varDepartamentoMP,
        'Provincia': varProvinciaMP,
        'Distrito': varDistritoMP,
        'Pais': varPaisMP,
        'TelefonoContacto': varTelefonoMP,
        'ComprobantesElectronicos': varComprobantesElectronicosMP,
        'AfiliadoPLE': varAfiliadoPLEMP,
        'CondicionPago': varCondicionPagoMP,
        'LineaCredito': varLineaCreditoMP,
        'EmailContacto': varEmailMP,
        'Web': varWebMP,
        'Fax': varFaxMP,
        'NombreContacto': varNombreContactoMP,
        'Telefono': varTelefonoContactoMP,
        'Email': varEmailContactoMP,
        'FechaIngreso': varFechaIngresoMP,
        'Observacion': varObservacionMP,
        'Tipo': varTipoMP,
        'Estado': varEstadoMP,
        'DiasEntrega': varDiasEntrega,
        'Afecto4ta': varAfecto
    }, function (data, status) {

        if (data != 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            limpiarDatosMP();
            CargarProveedor()
            $("#ModalProveedores").modal('hide');

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatosMP();
        }

    });
}
function soloEnteros(e, obj) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode == 13) {
        var tidx = parseInt(obj.getAttribute('tabindex')) + 1;
        elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) { elems[i].focus(); break; }
        }
    } else if (charCode == 46 || charCode > 31 && (charCode < 48 || charCode > 57)) {
        e.preventDefault();
        return false;
    }
    return true;
}
function limpiarDatosMP()
{
    $("#txtIdMP").val("");
    $("#txtCodigoMP").val("");
    $("#cboTipoPersonaMP").val(0);
    $("#cboTipoDocumentoMP").val(0);
    $("#txtNroDocumentoMP").val("");
    $("#txtRazonSocialMP").val("");
    $("#txtEstadoContribuyenteMP").val("");
    $("#txtCondicionContribuyenteMP").val("");
    $("#txtDireccionFiscalMP").val("");
    $("#txtTlf1MP").val("");
    $("#txtComprobantesElectronicosMP").val("");
    $("#txtAfiliadoPLEMP").val("");
    $("#txtLineaCreditoMP").val("");
    $("#txtEmailMP").val("");
    $("#txtWebMP").val("");
    $("#txtFaxMP").val("");
    $("#txtNombreContactoMP").val("");
    $("#txtTlfContactoMP").val("");
    $("#txtEmailContactoMP").val("");
    $("#txtFechaIngresoMP").val("");
    $("#txtObservacionMP").val("");

    $("#cboDepartamentoMP").val("");
    $("#cboProvinciaMP").val("");
    $("#cboDistritoMP").val("");
    $("#cboCondicionPagoMP").val("");

    $("#chkActivoMP").prop('checked', true);
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth()+1, 2) + '-' + f.getDate();
    $("#txtFechaIngresoMP").val(fecha)
}
function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}
function ResetRegistroSemana() {
    $("#IdTipoRegistro").prop("selectedIndex", 0)
    $("#IdSemana").prop("selectedIndex", 0)
}
function redondear() {
    let CantidadRedondeo = +($("#txtRedondeo").val().replace(/,/g, ""))
    let TotalSinImp = +($("#txtTotalAntesDescuento").val().replace(/,/g, ""))
    let Imp = +($("#txtImpuesto").val().replace(/,/g, ""))
    let TotalSinR = TotalSinImp + Imp
    let NuevoTotal = TotalSinR + CantidadRedondeo
    $("#txtTotal").val(formatNumberDecimales(NuevoTotal,2))
    
       
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


function ObtenerCuadrillasTabla(contador) {
    console.log("CONTADOR EN OBTENER CUADRILLA :"+contador)
    let IdObra = $("#IdObra").val()
    $.ajaxSetup({ async: false });
    $.post("/Cuadrilla/ObtenerCuadrillaxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let cuadrilla = JSON.parse(data);
        llenarComboCuadrillaTabla(cuadrilla, "cboCuadrillaTablaId" + contador, "Seleccione",contador)
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
    $.post("/Empleado/ObtenerEmpleadosPorIdBase", { 'IdBase': $("#IdBase").val()}, function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleadosTabla(empleados, "cboResponsableTablaId" + contador, "Seleccione",contador)
    });
}

function llenarComboEmpleadosTabla(lista, idCombo, primerItem,contador) {
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
        llenarComboEmpleadosTablaFila(empleados, "cboResponsableTablaId" + contador, "Seleccione", contador,valorActual)
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


    ObtenerCapatazTablaFila(contador,valorActual)
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
function Editar() {
    let varIdOPCH = $("#txtId").val();
    let varTipoDocumentoOperacion = $("#cboTipoDocumentoOperacion").val();
    let varIdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let varNumSerieTipoDocumentoRef = $("#SerieNumeroRef").val();
    let varIdCondicionPago = $("#IdCondicionPago").val();
    let varIdTipoRegistro = $("#IdTipoRegistro").val();
 
    let varComentario = $("#txtComentarios").val();

    let varIdSemana = $("#IdSemana").val();

    if (varIdTipoRegistro == 4) {
        varIdSemana = $("#IdGiro").val();
    }

    let varConsumoM3 = $("#consumomM3").val()
    let varConsumoHW = $("#consumomHW").val()
    let varTasaDet = $("#TasaDet").val()
    let varGrupoDet = $("#GrupoDet").val()
    let varSerieSAP = $("#txtSerieSAP").val()
    let varCondicionPagoDet = $("#CondicionPagoDet").val()

    //let varIdCuadrilla = $("#IdCuadrilla").val();
    //let varIdResponsable = $("#IdResponsable").val();

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
    if ($("#IdTipoRegistro").val() == 0 || $("#IdTipoRegistro").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo Registro',
            'error'
        )
        return;
    }
    if ($("#IdTipoRegistro").val() == '4') {
        if ($("#IdGiro").val() == 0 || $("#IdGiro").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Giro',
                'error'
            )
            return;
        }
    } else {
        if ($("#IdSemana").val() == 0 || $("#IdSemana").val() == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Semana',
                'error'
            )
            return;
        }

    }

    if ($("#IdProveedor").val() == 0 || $("#IdProveedor").val() == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Proveedor',
            'error'
        )
        return;
    }

    if ($("#SerieNumeroRef").val().length == 0) {
        Swal.fire(
            'Error!',
            'Complete el campo de Serie y Número',
            'error'
        )
        return;
    }



    //for (var i = 0; i < arrayCboCuadrillaTabla.length; i++) {
    //    if ($("#cboCuadrillaTablaId" + i).val() == 0 || $("#cboCuadrillaTablaId" + i).val() == null) {
    //        Swal.fire(
    //            'Error!',
    //            'Complete el campo de Cuadrilla de la Fila N°' + (i+1),
    //            'error'
    //        )
    //        return;
    //    }

    //}
    //for (var i = 0; i < arrayCboCuadrillaTabla.length; i++) {
    //    if ($("#cboResponsableTablaId" + i).val() == 0 || $("#cboResponsableTablaId" + i).val() == null) {
    //        Swal.fire(
    //            'Error!',
    //            'Complete el campo de Responsable de la Fila N°' + (i+1),
    //            'error'
    //        )
    //        return;
    //    }

    //}


    $.post('UpdateOPCH', {
        'IdOPCH': varIdOPCH,
        'IdTipoDocumentoRef': varIdTipoDocumentoRef,
        'NumSerieTipoDocumentoRef': varNumSerieTipoDocumentoRef,
        'Comentario': varComentario,
        'IdTipoDocumento': varTipoDocumentoOperacion,
        'IdCondicionPago': varIdCondicionPago,
        'IdTipoRegistro': varIdTipoRegistro,
        'IdSemana': varIdSemana,
        'ConsumoM3': varConsumoM3,
        'ConsumoHW': varConsumoHW,
        'TasaDetraccion': varTasaDet,
        'GrupoDetraccion': varGrupoDet,
        'SerieSAP': varSerieSAP,
        'CondicionPagoDet': varCondicionPagoDet

    }, function (data, status) {

        if (data != 0) {
            for (var i = 0; i < arraytxtIdFacturaDetalle.length; i++) {
                $.post('UpdateCuadrillas', {
                    'IdOPCHDetalle': arraytxtIdFacturaDetalle[i],
                    'IdCuadrilla': arrayCboCuadrillaTabla[i],
                    'IdResponsable': arrayCboResponsableTabla[i],

                }, function (data, status) {
                    if (data != 0) {
                    swal("Exito!", "Proceso Realizado Correctamente", "success")
                    CerrarModal()
                    listarOpch()
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
function VerDocsOrigen(IdOPCH,serie,correlativo) {
    AbrirModal("ModalDocsOrigen");
    $("#lblTituloModalDocsOrigen").html('Documentos de Origen de la Factura N° '+serie+'-'+correlativo)
    $.ajax({
        url: 'ObtenerOrigenesFactura',
        type: 'POST',
        dataType: 'json',
        data: { 'IdOPCH': IdOPCH },
        success: function (datos) {
            let tr = '';
            for (var i = 0; i < datos.length; i++) {
                tr += `<tr>`;
                tr += `<td>` + datos[i].TablaOrigen + `</td>`;
                tr += `<td>` + datos[i].NombSerie + `</td>`;
                if (datos[i].TablaOrigen == 'Pedido') {
                    tr += `<td><button class="btn btn-primary btn-xs" onclick=VerOC(`+datos[i].IdOrigen+`)>Ver Documento</button></td>`;
                } else if (datos[i].TablaOrigen == 'Entrega') {
                    tr += `<td><button class="btn btn-primary btn-xs" onclick=AbrirOPDN(` + datos[i].IdOrigen + `)>Ver Documento</button></td>`;
                } else {
                    tr += `<td>-</td>`;
                }
            
                tr += '</tr>';
            }

            $("#tbody_ModalDocsOrigen").html(tr);
        },
        error: function() {
            Swal("Error!","Ocurrió un Error","error");
        }
    })
}
function VerOC(IdPedido) {
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
function AbrirOPDN(IdOPDN) {
    Swal.fire({
        title: "Buscando Reporte Entrega...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {


        $.ajaxSetup({ async: false });
        $.post("/EntradaMercancia/GenerarReporteOPDN", { 'NombreReporte': 'EntregaMercancia', 'Formato': 'PDF', 'IdOPDN': IdOPDN }, function (data, status) {
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
    let IdOPCH = $("#txtId").val();
    let TablaOrigen = $("#txtOrigen").val();
    Swal.fire({
        title: 'DESEA GENERAR EXTORNO?',
        html: "Se validara si los productos ingresados se encuentran con Stock </br>" +
            "</br>" +
            "Serie Para Extorno </br>" +
            "<Select id='cboSerieExtorno' class='form-control'></select>" +
            "</br>" +
            "Fecha De Documento para Extorno  </br>" +
            "<input id='FechDocExtorno' type='date' class='form-control'/>" +
            "</br>" +
            "Fecha de Contabilizacion para Extorno  </br>" +
            "<input id='FechContExtorno' type='date' disabled class='form-control'/>" +
            "</br>" +
            "<p>* Las Fechas que se muestran por defecto son las mismas que el documento seleccionado</p>",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {

            //DATOS PARA SALIDA
            let Total = $("#txtTotal").val()
            let SubTotal = Total / (1.18)
            let Impuesto = Total - SubTotal
            let AnexoDetalle = [];
            let anioactual = new Date().getFullYear();

            let anio = (anioactual.toString()).substring(2, 4)

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
                        'TablaOrigen': 'facturas',
                        'IdCuadrilla': 0,
                        'IdResponsable': 0,

                    })
                }

            }

            console.log(detalles)
            console.log(1111)
            let TipoProductos
            //VALIDA SI ES SERVICIO O NO PARA CREAR LA SALIDA
            $.post('/EntradaMercancia/ValidaTipoProductoOPDN', { 'ArticuloMuestra': arrayIdArticulo[0] }, function (data, status) {

                let validacion = JSON.parse(data);
                console.log('VALIDACION')
                console.log(validacion)
                TipoProductos = validacion[0].TipoArticulos


            });

            console.log(TipoProductos)

            if (TablaOrigen != 'Entrega') {
                $.ajax({
                    url: "ValidarStockExtorno",
                    type: "POST",
                    async: true,
                    data: {
                        'IdOPCH': IdOPCH
                    },
                    success: function (data) {
                        if (data == 'bien') {
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
                                        'IdTipoDocumento': 1341,
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
                                        'IdCuadrilla': 0,
                                        'EntregadoA': 0,
                                        'IdTipoDocumentoRef': 10,
                                        'NumSerieTipoDocumentoRef': 'Extorno de la Factura ' + $("#cboSerie option:selected").text() + '-' + $("#txtNumeracion").val(),
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
                                                'IdOPCH': IdOPCH,
                                                'EsServicio': TipoProductos,
                                                'TablaOrigen': TablaOrigen,
                                            }, function (data, status) {

                                                if (data != 0) {
                                                    swal("Exito!", "Proceso Realizado Correctamente", "success")
                                                    CerrarModal()
                                                    listarOpch()
                                                } else {
                                                    swal("Error!", "Ocurrio un Error al Extornar el Pedido")
                                                    CerrarModal()
                                                }

                                            });


                                        } else {
                                            Swal.fire(
                                                'Error!',
                                                'Ocurrio un Error al Generar la Salida!',
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
                                    'IdOPCH': IdOPCH,
                                    'EsServicio': TipoProductos,
                                    'TablaOrigen': TablaOrigen,
                                }, function (data, status) {

                                    if (data != 0) {
                                        swal("Exito!", "Proceso Realizado Correctamente", "success")
                                        CerrarModal()
                                        listarOpch()
                                    } else {
                                        swal("Error!", "Ocurrio un Error")
                                        CerrarModal()
                                    }

                                });
                            }
                        } else {
                            Swal.fire(
                                'Error!',
                                'El Producto no cuenta con stock suficiente!',
                                'error'
                            )
                        }
                    }
                }).fail(function () {
                    Swal.fire(
                        'Error!',
                        'Error al Validar Stock!',
                        'error'
                    )
                });
            }
            else {
                $.post('ExtornoConfirmado', {
                    'IdOPCH': IdOPCH,
                    'EsServicio': TipoProductos,
                    'TablaOrigen':TablaOrigen,
                }, function (data, status) {

                    if (data != 0) {
                        swal("Exito!", "Proceso Realizado Correctamente", "success")
                        CerrarModal()
                        listarOpch()
                    } else {
                        swal("Error!", "Ocurrio un Error Al Extornar la Factura de Entrega")
                        CerrarModal()
                    }

                });
            }
         

        }
    })
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerieExtorno(series, "cboSerieExtorno", "Seleccione")
    });
}


function seve() {

    if ($("#divDetraccion").is(':visible')) {
        console.log("det")
    }
    if ($("#divServPub").is(':visible')) {
        console.log("serpub")
    }

}

function changeTipoPersona() {
    if ($("#cboTipoPersonaMP").val() != 1) {
        $("#div4taMP").hide();
    } else {
        $("#div4taMP").show();
    }
}
function changeTipoDocumento() {
    let TDocSelect = $("#IdTipoDocumentoRef").val()
    let totalValor = ($("#txtTotal").val()).replace(/,/g, "")


    if (TDocSelect == 2 || TDocSelect == 13 || TDocSelect == 17 || TDocSelect == 16) {
        //$("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
        //    $(elemento).val(1).change();
        //});

    } else if (TDocSelect == 11) {
        $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
            $(elemento).val(2).change();
        });
    } else {
        $("select[name='cboIndicadorImpuestoDetalle[]']").each(function (indice, elemento) {
            $(elemento).val(3).change();
        });
    }





    if (TDocSelect == 11) {
        $("#divDetraccion").hide()

        $("#cboClaseArticulo").val(2)
        CambiarClaseArticulo()
    } else {
        if ($("#cboClaseArticulo").val() == 2 && totalValor > 700) {
            $("#divDetraccion").show()

        } else {
            $("#divDetraccion").hide()
        }

    }
    if (TDocSelect == 16) {
        $("#divServPub").show()
        $("#divDetraccion").hide()
        $("#cboClaseArticulo").val(2)
        CambiarClaseArticulo()
    } else {
        $("#divServPub").hide()
    }

    if ($("#cboClaseArticulo").val() == '2') {

        $("#divGlosa").show()
    } else {
        $("#divGlosa").hide()
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
    //listarOpch();
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
    //listarOpch();
}

function CargarCondPagoDet() {
    let varGrupoDet = $("#TasaDet option:selected").text()
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerCondPagoDetSAP", { 'GrupoDet': varGrupoDet }, function (data, status) {
        try {
            let base = JSON.parse(data);
            llenarComboCondPagoDet(base, "CondicionPagoDet", "seleccione")

        }catch (e) {
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
    //listarOpch();
}

function ValidarSUNAT(Id) {
    $.post("ActualizarEstadoValidacionSUNAT", { 'IdOPCH': Id }, function (data, status) {

        if (data == 1) {
            swal("Resultado!", "Documento Validado por SUNAT", "success")
            listarOpch()
        } else if (data == 2) {
            swal("Resultado!", "El Documento no existe en SUNAT", "info")
            listarOpch()
        } else {
            swal("Resultado!", "Volver a Intentar", "error")
        }

    });
}


////NUEVOS FILTROS

function CargarObras() {
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let obras = JSON.parse(data);
        llenarComboObraFiltro(obras, "cboObraFiltro", "Seleccione")
    });
}

function llenarComboObraFiltro(lista, idCombo, primerItem) {
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

function CargarTipoRegistroFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerTipoRegistrosAjax", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboTipoRegistroFiltro(tipoRegistros, "cboTipoRegistroFiltro", "Seleccione")
    });
}


function llenarComboTipoRegistroFiltro(lista, idCombo, primerItem) {
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


function changeTipoRegistroFiltro() {

    let varIdTipoRegistro = $("#cboTipoRegistroFiltro").val();
    let varIdObra = $("#cboObraFiltro").val();
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanaAjax", { 'estado': 1, 'IdTipoRegistro': varIdTipoRegistro, 'IdObra': varIdObra }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboSemanaFiltro(tipoRegistros, "cboSemanaFiltro", "Seleccione")

    });

}

function llenarComboSemanaFiltro(lista, idCombo, primerItem) {
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

function esRendicionFiltro() {
    if ($("#cboTipoRegistroFiltro").val() != 4) {
        $("#DivGiro").hide()
        $("#DivSemana").show()
        if ($("#cboTipoRegistroFiltro").val() == 5) {
            $("#cboCondicionPagoFiltro").val(1)
        }
    } else {
        $("#DivGiro").show()
        $("#DivSemana").hide()
        CargarGirosFiltro()
    }
}
function CargarGirosFiltro() {
    let obraGiro = $("#cboObraFiltro").val()
    $.ajaxSetup({ async: false });
    $.post("/GestionGiro/ObtenerGiroAprobado", { 'IdObra': obraGiro }, function (data, status) {
        let base = JSON.parse(data);
        llenarComboGirosFiltro(base, "cboGiroFiltro", "seleccione")

    });

}


function llenarComboGirosFiltro(lista, idCombo, primerItem) {
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

function ImprimirFactura(Id) {
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        $.ajaxSetup({ async: false });
        $.post("/Pedido/GenerarReporte", { 'NombreReporte': 'RPTFacturaProv', 'Formato': 'PDF', 'Id': Id }, function (data, status) {
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
                Swal.close()
            } else {
                console.log("error");
            }
        });
    }, 200)

}

function ValidacionSUNATMasivo() {
    Swal.fire({
        title: 'Validación Masiva SUNAT',
        html: "Se validara los documentos pertenecientes al Tipo de Registro y Semana/Giro Seleccionado </br>" +
            "</br>" +
            "Este Proceso Puede tardar un poco, por lo que deberá esperar",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Validar!',
        reverseButtons: true 
    }).then((result) => {
        if (result.isConfirmed) {
            let varIdObraFiltro = $("#cboObraFiltro").val()
            let IdRegistro = $("#cboTipoRegistroFiltro").val()
            if (IdRegistro == 0) {
                Swal.fire("Seleccione un Tipo de Registro")
                return
            }
            let IdSemana = 0

            if (IdRegistro != 4) {
                IdSemana = $("#cboSemanaFiltro").val()
                if (IdSemana == 0) {
                    Swal.fire("Seleccione una Semana")
                    return
                }
            } else {
                IdSemana = $("#cboGiroFiltro").val()
                if (IdSemana == 0) {
                    Swal.fire("Seleccione una Giro")
                    return
                }
            }
            $.post("ValidacionSunatMasiva", { 'IdObra': varIdObraFiltro, 'IdTipoRegistro': IdRegistro, 'IdSemana': IdSemana }, function (data, status) {
                Swal.fire("Proceso Terminado")
                listarOpch();
            });
        }
    })
}

function CargarProveedorFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedorFiltro(proveedores, "cboProveedorFiltro", "Seleccione")
    });
}

function llenarComboProveedorFiltro(lista, idCombo, primerItem) {
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
    $("#cboProveedorFiltro").select2();
}

function listarOpchProveedor() {
    let varProveedor = $("#cboProveedorFiltro").val()
    let varNumSerie = $("#txtNumFTFiltro").val()
 
    tablepedido = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarOPCHDTxProveedor',
            type: 'POST',
            data: {
                'IdProveedor': varProveedor,
                'NumSerie': varNumSerie,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let extrasBtn = ''
                    if (full.ValidadoSUNAT == 0) {
                        extrasBtn = `<button class="btn btn-primary juntos fa fa-share-square btn-xs" onclick="ValidarSUNAT(` + full.IdOPCH + `)"></button>`
                    }

                    return `<button class="btn btn-primary juntos fa fa-eye btn-xs" onclick="ObtenerDatosxIDOPCH(` + full.IdOPCH + `)"></button>
                            <button class="btn btn-primary juntos fa fa-file-text btn-xs" onclick="VerDocsOrigen(` + full.IdOPCH + `)"></button>` + extrasBtn
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
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoDocumentoOperacion
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdDocExtorno != 0) {
                        return '<p style="color:red">' + full.NombSerie + '-' + full.Correlativo + '</p>'
                    } else {
                        return '<a style="color:blue;text-decoration:underline;cursor:pointer" onclick="ImprimirFactura(' + full.IdOPCH + ')" > ' + full.NombSerie + '-' + full.Correlativo + '</a>'
                    }
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoDocumentoRef
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef

                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Proveedor
                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.Total, 2)
                },
            },
            {
                data: null,
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                data: null,
                targets: 10,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                data: null,
                targets: 11,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Moneda
                },
            },
            {
                data: null,
                targets: 12,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.ValidadoSUNAT == 0) {
                        return 'NO ENVIADO'
                    } else if (full.ValidadoSUNAT == 1) {
                        return 'VALIDADO'
                    } else {
                        return 'NO EXISTE'
                    }
                },
            },
            {
                data: null,
                targets: 13,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.DocNumCont == -1) {
                        return "<p style='color:red'>ERROR AL VALIDAR<p>"
                    } else if (full.DocNumCont == 0) {
                        return "NO CONTABILIZADO"
                    } else {
                        return "<p style='color:green'>CONTABILIZADO - " + full.DocNumCont + "</p>"
                    }
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
        $("#txtOrigen").val("Pedido")
        $("#txtOrigenId").val(data.IdPedido + " ")
        $('#ModalListadoPedido').modal('hide');
        tablepedido.ajax.reload()

        //$("#tbody_detalle").find('tbody').empty();
        //AgregarItemTranferir(((table.row(this).index()) + 1), data["IdArticulo"], data["Descripcion"], (data["CantidadEnviada"] - data["CantidadTranferida"]), data["Stock"]);

    });

    setTimeout(() => {
        changeTipoDocumento()
    }, 500)
}

function ValidarFechaDoc() {
    let FechaActual = getFechaHoy()
    let FechaTXT = $("#txtFechaDocumento").val()

    if (FechaTXT > FechaActual) {
        swal("La Fecha no puede ser superior a la Actual")
        $("#txtFechaDocumento").val(FechaActual)
        return
    }
}
function getFechaHoy() {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    var day = ('0' + currentDate.getDate()).slice(-2);

    var formattedDate = year + '-' + month + '-' + day;
    return formattedDate;
}