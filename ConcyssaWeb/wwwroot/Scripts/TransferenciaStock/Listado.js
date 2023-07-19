function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}


let limitador = 0;
let valorfor = 1
function EsActivo() {
    let TipoArticulo = $("#cboClaseArticulo").val()
    if (TipoArticulo == 3) {
        $("#IdTipoProducto").hide()
    } else {
        $("#IdTipoProducto").show()
    }

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

let validarGuardado = 0;



/*USANDO */


//function CargarAlmacenDestino() {
//    $.ajaxSetup({ async: false });
//    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
//        let almacen = JSON.parse(data);
//        llenarComboAlmacen(almacen, "IdAlmacenDestino", "Seleccione")
//    });
//}

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
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
    });
    $("#cboAlmacen").prop("selectedIndex", 1)
    $("#cboAlmacenItem").prop("selectedIndex", 1)
}



function ObtenerAlmacenxIdObraDestino() {
    let IdObra = $("#IdObraDestino").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "IdAlmacenDestino", "Seleccione")
        //llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
    });


    var ObraInicio = $("#IdObra").val();
    var ObraFin = $("#IdObraDestino").val();


    if (ObraInicio == ObraFin) {
        $("#IdAlmacenDestino").prop('disabled', false);
        $("select[name='cboAlmacen[]']").prop('disabled', false);

    } else {
        $("#IdAlmacenDestino").prop('disabled', true);
        $("select[name='cboAlmacen[]']").prop('disabled', true);
        $("select[name='cboAlmacen[]']").val(0);
    }




}



function ObtenerObrasDestino() {

    //var BaseInicio = $("#IdBase").val();

    $.post("/Obra/ObtenerObra", { 'Estado': 1 }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObraDestino(obra, "IdObraDestino", "Seleccione")
    });

}


function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "IdObra", "Seleccione")
    });
}

//function ocultarCampos() {
//    if ($("#IdTipoDocumentoRef").val() = 1)
//}


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

function ObtenerObraTodas() {
    let IdBase = $("#IdBase").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObra", function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObraTodas(obra, "IdObra", "Seleccione")
    });
}



function llenarComboObraTodas(lista, idCombo, primerItem) {
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



function llenarComboObraDestino(lista, idCombo, primerItem) {
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
function CargarBaseTodas() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBase", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseTodas(base, "IdBase", "Seleccione")
    });
}

function llenarComboBaseTodas(lista, idCombo, primerItem) {
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




function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacen", "Seleccione")
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")

    });
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
/*END USANDO */






window.onload = function () {
    CargarBaseFiltro()
    ObtenerConfiguracionDecimales();
    $("#btnEditar").hide()
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
    ConsultaServidor()
        
}



function ConsultaServidor() {
    let varIdBaseFiltro = $("#cboObraFiltro").val()
    $.post("../Movimientos/ObtenerMovimientosTranferencias", {'IdBase' : varIdBaseFiltro}, function (data, status) {

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
            let varSNGE = "-"
            let varEstadoGE = "-"
            if (movimientos[i].TDocumento == "GUIA REMISION") varEstadoGE = 'NO ACEPTADO'
            if (movimientos[i].SerieGuiaElectronica) {
                varSNGE = movimientos[i].SerieGuiaElectronica.toUpperCase() + "-" + movimientos[i].NumeroGuiaElectronica
                if (movimientos[i].EstadoFE == 1) {
                    varEstadoGE = "ACEPTADO"
                } else {
                    varEstadoGE = "NO ACEPTADO"
                }
            }

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + movimientos[i].FechaDocumento.split("T")[0] + '</td>' +
                '<td>' + movimientos[i].NombUsuario + '</td>' +
                '<td>' + movimientos[i].NombTipoDocumentoOperacion.toUpperCase() + '</td>' +
                '<td style="color:blue;cursor:pointer;text-decoration:underline" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ')">' + movimientos[i].NombSerie.toUpperCase() + '-' + movimientos[i].Correlativo + '</td>' +
                '<td>' + movimientos[i].TDocumento.toUpperCase() + '</td>' +
                '<td>' + varSNGE + '</td>' +
                '<td>' + varEstadoGE + '</td>' +
                '<td>' + movimientos[i].NombObra + '</td>' +
                '<td>' + movimientos[i].NombAlmacen + '</td>' +  
                
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
              /*  '<button class="btn btn-primary" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ')">R</button>' +*/
                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '</tr>';
        }
        if (table) {
            table.destroy()
        }
        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    $("#MarcaVehiculo").val("");
    $("#NumIdentidadConductor").val("");
    $("#NombreConductor").val("");
    $("#ApellidoConductor").val("");
    $("#LicenciaConductor").val("");
    

    today = yyyy + '-' + mm + '-' + dd;
    $("#txtFechaDocumento").val(today)
    $("#txtFechaContabilizacion").val(today)
    $("#btnEditar").hide()
    validarGuardado = 0;

    disabledmodal(false);
    $("#lblTituloModal").html("Nuevo Transferencia");
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }

    //CargarAlmacenDestino();
    CargarCentroCosto();
    ObtenerCuadrillas();
    //CargarBase();
    //CargarAlmacen()
    CargarTipoDocumentoOperacion()
    CargarSeries();


    //AgregarLinea();


    CargarVehiculos();

    CargarSolicitante(1);
    CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();





    CargarImpuestos();
    $("#cboImpuesto").val(2).change();
    //$("#cboSerie").val(1).change();

    $("#cboMoneda").val("USD");
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#IdTipoProducto").prop("disabled", false);
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
    CargarMotivoTraslado();
    CargarProveedor();
    ObtenerTiposDocumentos();


    //validarseriescontable();
    ObtenerObrasDestino();

    $("#cboMoneda").val(1).change();
    $("#cboTipoDocumentoOperacion").val(1337);
    $("#cboTipoDocumentoOperacion").prop("disabled", true);


    $("#btnAgregarItem").prop("disabled", false);
    $("#IdObraDestino").prop("disabled", false);

    $("#SerieNumeroRef").val("");

    $("#IdTipoDocumentoRef").val(1);

    $("#IdDestinatario").val(24154).change();
    $("#IdTransportista").val(24154).change();
    $("#IdMotivoTraslado").val(09).change();

    $("#Peso").val(1);
    $("#Bulto").val(1);

    $("#btnGenerarPDF").hide();
}


function OpenModalItem() {
    //Cuando se abre agregar Item

    let IdObraDestino = $("#IdObraDestino").val();
    let TipoProd = $("#IdTipoProducto").val();
    if (IdObraDestino == "0") {
        swal("Informacion!", "Debe Seleccionar Obra Destino!");
        return;
    }

    if ($("#cboClaseArticulo").val() == 1 && TipoProd == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo Producto!");
        return;
    }


    let ClaseArticulo = $("#cboClaseArticulo").val();
    let Moneda = $("#cboMoneda").val();
    if (ClaseArticulo == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
    } else if (Moneda == 0) {
        swal("Informacion!", "Debe Seleccionar Moneda!");
    }
    else {

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
               <a href="/SolicitudRQ/Download?ImageName=`+ Nombre + `" >Descargar</a>
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


function EliminarAnexo(Id, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("/SolicitudRQ/EliminarAnexoSolicitud", { 'IdSolicitudRQAnexos': Id }, function (data, status) {

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
    //txtReferenciaItem

    let Stock = $("#txtStockAlmacenItem").val();

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;


    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let AlmacenFinal;
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
    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0" || !$.isNumeric(ValidarCantidad)) {
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



    var ObrDestino = $("#IdObraDestino").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': ObrDestino }, function (data, status) {
        AlmacenFinal = JSON.parse(data);
    });


    var ObrInicio = $("#IdObra").val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': ObrInicio }, function (data, status) {
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

    for (var J = 0; J < valorfor; J++) {
        console.log("VUELTAAAAAAAAAAA: " + J)

        limitador++
        contador++;
        let tr = '';

        //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
        //    <option value="0">Seleccione</option>
        //</select>
        tr += `<tr id="tr` + contador + `">
            <td style="display:none;"><input input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
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

            <td><input class="form-control"  type="number" name="txtStock[]" value="0" id="txtStock`+ contador + `" disabled></td>

            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled>`;
        tr += `  <option impuesto="0" value="0">Seleccione</option>`;
        for (var i = 0; i < IndicadorImpuesto.length; i++) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            
            <td>
            <select class="form-control" style="width:100px" id="cboAlmacenOrigen`+ contador + `" name="cboAlmacenOrigen[]" onchange="BuscarStockAlmacen(` + contador + `)">`;
        tr += `  <option value = "0"> Seleccione</option> `;
        for (var i = 0; i < Almacen.length; i++) {
            tr += `<option value="` + Almacen[i].IdAlmacen + `" > ` + Almacen[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>

            <td>
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" onchange="ValidaAlmacenIguales(` + contador + `)">`;
        tr += `  <option value="0">Seleccione</option>`;
        for (var i = 0; i < AlmacenFinal.length; i++) {
            tr += `  <option value="` + AlmacenFinal[i].IdAlmacen + `">` + AlmacenFinal[i].Descripcion + `</option>`;
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
            <td><button class="btn btn-xs btn-danger borrar fa fa-trash" onclick="borrartr('tr`+ contador + `');restarLimitador()"></button></td>
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


        $("#txtStock" + contador).val(Stock);

        $("#txtIdArticulo" + contador).val(IdItem);
        $("#txtCodigoArticulo" + contador).val(CodigoItem);
        $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
        $("#cboUnidadMedida" + contador).val(MedidaItem);
        $("#txtCantidadNecesaria" + contador).val(CantidadItem).change();
        $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
        $("#cboProyecto" + contador).val(ProyectoItem);
        //$("#cboAlmacen" + contador).val(AlmacenItem);
        $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

        $("#cboCentroCostos" + contador).val(CentroCostoItem);
        $("#txtReferencia" + contador).val(ReferenciaItem);


        var ObraInicio = $("#IdObra").val();
        var ObraFin = $("#IdObraDestino").val();

        if (ObraInicio == ObraFin) {
            $("#cboAlmacen" + contador).prop('disabled', false);
            //$("#cboAlmacen" + contador).val(0);
        } else {
            $("#cboAlmacen" + contador).prop('disabled', true);
            //$("#cboAlmacen" + contador).val(0);
        }

        let AlmacenOrigen = $("#cboAlmacen").val();
        let AlmacenDestino = $("#IdAlmacenDestino").val();


        $("#cboAlmacenOrigen" + contador).val(AlmacenOrigen);
        $("#cboAlmacen" + contador).val(AlmacenDestino);

        LimpiarModalItem();
        NumeracionDinamica();
    }
}

function restarLimitador() {
    limitador = limitador - 1
}

function ValidaAlmacenIguales(contador) {
    var AlmacenOrigen = $("#cboAlmacenOrigen" + contador).val();
    //var IdArticulo = $("#txtIdArticulo" + contador).val();
    var AlmacenDestino = $("#cboAlmacen" + contador).val();

    if (AlmacenOrigen == AlmacenDestino) {
        Swal.fire(
            'Error!',
            'Los almacenes no pueden ser iguales',
            'error'
        )
        $("#cboAlmacen" + contador).val(0);
        return;
    }
}





function BuscarStockAlmacen(contador) {

    var AlmacenOrigen = $("#cboAlmacenOrigen" + contador).val();
    var IdArticulo = $("#txtIdArticulo" + contador).val();
    var AlmacenDestino = $("#cboAlmacen" + contador).val();

    if (AlmacenOrigen == AlmacenDestino) {
        Swal.fire(
            'Error!',
            'Los almacenes no pueden ser iguales',
            'error'
        )
        $("#cboAlmacenOrigen" + contador).val(0);
        return;
    }

    $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': AlmacenOrigen }, function (data, status) {
        let datos = JSON.parse(data);
        console.log(datos);
        $("#txtStock" + contador).val(datos[0].Stock);
    });



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
        //let definicion = JSON.parse(data);
        //llenarComboDefinicionGrupoUnidadItem(definicion, "cboMedidaItem", "Seleccione")
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
    //$.ajaxSetup({ async: false });
    //$.post("/Sucursal/ObtenerSucursales", function (data, status) {
    //    let sucursales = JSON.parse(data);
    //    llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    //});
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
    var primerIndice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 3) {
            if (primerIndice == 0) {
                primerIndice = i;
            }
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#cboSerie").val(lista[primerIndice].IdSerie);

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
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) {
            if (lista[i].CodeExt == "58") {
                contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>";
            }

        }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



////llenarComboTipoDocumentoOperacion
//$(document).on('click', '.borrar', function (event) {
//    event.preventDefault();
//    $(this).closest('tr').remove();

//    let filas = $("#tabla").find('tbody tr').length;
//    console.log("filas");
//    console.log(filas);
//});


function borrartr(id) {
    $("#" + id).remove();
    CalcularTotales();
    NumeracionDinamica();
}



function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tabla_files").find('tbody').empty();
    $.magnificPopup.close();
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
    $.post("ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {
        let dato = JSON.parse(data);
        //console.log(dato);
        $("#txtTipoCambio").val(dato[0].Rate);

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


function GuardarSolicitud() {
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

    let arrayStock = new Array();
    $("input[name='txtStock[]']").each(function (indice, elemento) {
        arrayStock.push($(elemento).val());
    });


    let arrayPrecioInfo = new Array();
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });

    let arrayTotal = new Array();
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val());
    });

    let arrayAlmacenOrigen = new Array();
    $("select[name='cboAlmacenOrigen[]']").each(function (indice, elemento) {
        arrayAlmacenOrigen.push($(elemento).val());
    });

    let arrayAlmacenDestino = new Array();
    $("select[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacenDestino.push($(elemento).val());
    });

    let arrayReferencia = new Array();
    $("input[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });




    for (var i = 0; i < arrayCantidadNecesaria.length; i++) {
        if (Number(arrayCantidadNecesaria[i]) > Number(arrayStock[i])) {
            Swal.fire(
                'Error!',
                'Stock cae a negativo linea: ' + (i + 1),
                'error'
            )
            return;
        }
    }





    for (var i = 0; i < arrayAlmacenOrigen.length; i++) {
        if (arrayAlmacenOrigen[i] == 0) {
            Swal.fire(
                'Error!',
                'Debe colocar almacen origen detalle!, linea:' + (i + 1),
                'error'
            )
            return;
        }
    }



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
    let IdTipoProducto = $("#IdTipoProducto").val();
    let IdCuadrilla = $("#IdCuadrilla").val();
    let IdAlmacenDestino = $("#IdAlmacenDestino").val();

    //let IdObraDestino = $("#IdObraDestino").val();

    //END Cabecera

    //let oMovimientoDetalleDTO = {};
    //oMovimientoDetalleDTO.Total = arrayTotal


    var ObraInicio = $("#IdObra").val();
    var ObraFin = $("#IdObraDestino").val();
    var DatoValidarIngresoSalidaOAmbos = 0;
    var TranferenciaDirecta = 0;

    if (ObraInicio == ObraFin) {
        DatoValidarIngresoSalidaOAmbos = 2;
        TranferenciaDirecta = 1;
    } else {
        DatoValidarIngresoSalidaOAmbos = 2;

    }

    if ($("#IdTipoDocumentoRef").val() == 1) {
        if ($("#Peso").val() == 0) {
            Swal.fire(
                'Error!',
                'Peso debe ser mayor a 0',
                'error'
            )
            return;
        }
        if ($("#Bulto").val() == 0) {
            Swal.fire(
                'Error!',
                'Bulto debe ser mayor a 0',
                'error'
            )
            return;
        }
    }

   




    let detalles = [];
    if (arrayIdArticulo.length == arrayIdUnidadMedida.length && arrayCantidadNecesaria.length == arrayPrecioInfo.length) {

        for (var i = 0; i < arrayIdArticulo.length; i++) {
            detalles.push({
                'IdArticulo': parseInt(arrayIdArticulo[i]),
                'DescripcionArticulo': arraytxtDescripcionArticulo[i],
                'IdDefinicionGrupoUnidad': arrayIdUnidadMedida[i],
                'IdAlmacen': arrayAlmacenOrigen[i],
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
                'IdAlmacenDestino': arrayAlmacenDestino[i],
                'ValidarIngresoSalidaOAmbos': DatoValidarIngresoSalidaOAmbos
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



    if (validarGuardado == 0) {
        $.ajax({
            url: "UpdateInsertMovimiento",
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
                'Comentario': Comentario,
                'SubTotal': SubTotal,
                'Impuesto': Impuesto,
                'Total': Total,
                'IdTipoProducto': IdTipoProducto,
                'IdAlmacenDestino': IdAlmacenDestino,
                'IdCuadrilla': IdCuadrilla,

                'IdObraDestino': ObraFin,


                'IdTipoDocumentoRef': $("#IdTipoDocumentoRef").val(),
                'NumSerieTipoDocumentoRef': $("#SerieNumeroRef").val(),
                'IdDestinatario': $("#IdDestinatario").val(),
                'IdMotivoTraslado': $("#IdMotivoTraslado").val(),
                'IdTransportista': $("#IdTransportista").val(),
                'PlacaVehiculo': $("#PlacaVehiculo").val(),
                'MarcaVehiculo': $("#MarcaVehiculo").val(),
                'NumIdentidadConductor': $("#NumIdentidadConductor").val(),

                'NombreConductor': $("#NombreConductor").val(),
                'ApellidoConductor': $("#ApellidoConductor").val(),
                'LicenciaConductor': $("#LicenciaConductor").val(),
                'TipoTransporte': $("#IdTipoTransporte").val(),

                'Peso': $("#Peso").val(),
                'Bulto': $("#Bulto").val(),

                'ValidarIngresoSalidaOAmbos': DatoValidarIngresoSalidaOAmbos,
                'TranferenciaDirecta': TranferenciaDirecta
                //end cabecera

                //DETALLE
                /* 'detalles': JSON.parse(detalles)*/

                //END DETALLE
                //'IdSolicitudRQ': varIdSolicitud,
                //'IdAlmacen': varSerie,
                //'Serie': varSerieDescripcion,
                //'Numero': varNumero,
                //'IdSolicitante': varSolicitante,
                //'IdSucursal': varSucursal,
                //'IdDepartamento': varDepartamento,
                //'IdClaseArticulo': varClaseArticulo,
                //'IdTitular': varTitular,
                //'IdMoneda': varMoneda,
                //'TipoCambio': varTipoCambio,
                //'IndicadorImpuesto': 0,
                //'TotalAntesDescuento': varTotalAntesDescuento,
                //'Impuesto': varImpuesto,
                //'Total': varTotal,
                //'FechaContabilizacion': varFechaContabilizacion,
                //'FechaValidoHasta': varFechaValidoHasta,
                //'FechaDocumento': varFechaDocumento,
                //'Comentarios': varComentarios,
                //'Estado': varEstado,
                //'Prioridad': varPrioridad,
                //'IdArticulo': arrayIdArticulo,
                //'Prioridad': arrayPrioridad,
                //'IdUnidadMedida': arrayIdUnidadMedida,
                //'FechaNecesaria': arrayFechaNecesaria,
                //'IdItemMoneda': arrayIdMoneda,
                //'ItemTipoCambio': arrayTipoCambio,
                //'CantidadNecesaria': arrayCantidadNecesaria,
                //'PrecioInfo': arrayPrecioInfo,
                //'IdIndicadorImpuesto': arrayIndicadorImpuesto,
                //'ItemTotal': arrayTotal,
                //'IdAlmacen': arrayAlmacen,
                //'IdProveedor': arrayProveedor,
                //'NumeroFabricacion': arrayNumeroFabricacion,
                //'NumeroSerie': arrayNumeroSerie,
                //'IdLineaNegocio': arrayLineaNegocio,
                //'IdCentroCostos': arrayCentroCosto,
                //'IdProyecto': arrayProyecto,
                //'Referencia': arrayReferencia,
                //'IdSolicitudRQDetalle': arrayIdSolicitudDetalle,
                //'DetalleAnexo': arrayGeneralAnexo
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
                    ConsultaServidor();

                    CerrarModal();

                    validarGuardado++;

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
        CerrarModal();
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
    limitador = 0;
}

function ObtenerDatosxID(IdMovimiento) {
    $("#btnEditar").show()
    $("#txtId").val(IdMovimiento)
    CargarMotivoTraslado();
    CargarProveedor();
    ObtenerTiposDocumentos();
    CargarBaseFiltro();
    CargarCentroCosto();
    CargarAlmacen();
    CargarTipoDocumentoOperacion();
    CargarSeries();
    CargarSolicitante(1);
    CargarSucursales();
    CargarMoneda();

    CargarBaseTodas();
    ObtenerObrasDestino();
    setTimeout(() => {
        ObtenerObraTodas()
    },100)


    //CargarObra();
    CargarVehiculos();
    

    $("#lblTituloModal").html("Editar Transferencia");
    AbrirModal("modal-form");





    $.post('../Movimientos/ObtenerDatosxIdMovimientoOLD', {
        'IdMovimiento': IdMovimiento,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let movimiento = JSON.parse(data);
            console.log("aqui");
            console.log(movimiento);
            console.log("aqui");
            $("#cboAlmacen").val(movimiento.IdAlmacen);
            $("#cboSerie").val(movimiento.IdSerie);
            $("#cboMoneda").val(movimiento.IdMoneda);
            $("#TipoCambio").val(movimiento.TipoCambio);
            $("#txtTotalAntesDescuento").val(movimiento.SubTotal)
            $("#txtImpuesto").val(movimiento.Impuesto)
            $("#txtTotal").val(movimiento.Total)
           
            $("#txtNumeracion").val(movimiento.Correlativo)
            $("#IdBase").val(movimiento.IdBase)
            $("#IdObraDestino").val(movimiento.IdObraDestino)

            $("#IdObraDestino").prop('disabled', true)
            ObtenerAlmacenxIdObraDestino();

            $("#IdAlmacenDestino").val(movimiento.IdAlmacenDestino)
            $("#cboTipoDocumentoOperacion").val(movimiento.IdTipoDocumento)



            $("#IdTipoDocumentoRef").val(movimiento.IdTipoDocumentoRef)
            $("#SerieNumeroRef").val(movimiento.NumSerieTipoDocumentoRef)


            $("#IdDestinatario").val(movimiento.IdDestinatario).change()
            $("#IdMotivoTraslado").val(movimiento.IdMotivoTraslado).change()
            $("#IdTransportista").val(movimiento.IdTransportista).change()
            $("#PlacaVehiculo").val(movimiento.PlacaVehiculo).change()
            $("#MarcaVehiculo").val(movimiento.MarcaVehiculo)
            $("#NumIdentidadConductor").val(movimiento.NumIdentidadConductor)

            $("#NombreConductor").val(movimiento.NombreConductor)
            $("#ApellidoConductor").val(movimiento.ApellidoConductor)
            $("#LicenciaConductor").val(movimiento.LicenciaConductor)
            $("#IdTipoTransporte").val(movimiento.TipoTransporte)

            $("#SerieNumeroRef").val(movimiento.SerieGuiaElectronica + "-" + movimiento.NumeroGuiaElectronica)

            $("#cboEstadoFE").val(movimiento.EstadoFE);
            console.log("FECHA")
            console.log(movimiento.FechaDocumento)

            $("#txtFechaDocumento").val((movimiento.FechaDocumento).split("T")[0])

            $("#txtFechaContabilizacion").val((movimiento.FechaContabilizacion).split("T")[0])
            $("#txtComentarios").html(movimiento.Comentario)


            $("#Peso").val(movimiento.Peso)
            $("#Bulto").val(movimiento.Bulto)

            var TipDoc = $("#IdTipoDocumentoRef").val();
            if (TipDoc == "1") {
                $("#btnGenerarPDF").show();
                $("#btnGenerarGuia").show();
            } else {
                $("#btnGenerarGuia").hide();
                $("#btnGenerarPDF").hide();
            }

            if (movimiento.EstadoFE == 1 && TipDoc == "1") {
                $("#btnGenerarPDF").show();
                $("#btnGenerarGuia").hide();
                //extra
                $("#btnEditar").hide()
                $("#IdTipoDocumentoRef").prop("disabled", true)
                $("#IdCuadrilla").prop("disabled", true)
                $("#EntregadoA").prop("disabled", true)
                $("#SerieNumeroRef").prop("disabled", true)
                $("#txtComentarios").prop("disabled", true)

                $("#IdTipoTransporte").prop("disabled", true)
                $("#IdDestinatario").prop("disabled", true)
                $("#IdMotivoTraslado").prop("disabled", true)
                $("#IdTransportista").prop("disabled", true)
                $("#PlacaVehiculo").prop("disabled", true)
                $("#MarcaVehiculo").prop("disabled", true)
                $("#NumIdentidadConductor").prop("disabled", true)
                $("#NombreConductor").prop("disabled", true)
                $("#ApellidoConductor").prop("disabled", true)
                $("#LicenciaConductor").prop("disabled", true)
                $("#Peso").prop("disabled", true)
                $("#Bulto").prop("disabled", true)
                $("#cboEstadoFE").prop("disabled", true)
            }
            else if (movimiento.EstadoFE == 0 && TipDoc == "1") {

                $("#btnGenerarPDF").hide();
                $("#btnGenerarGuia").show();
                //extra
                $("#btnEditar").show()
                $("#IdTipoDocumentoRef").prop("disabled", false)
                $("#IdCuadrilla").prop("disabled", false)
                $("#EntregadoA").prop("disabled", false)
                $("#SerieNumeroRef").prop("disabled", false)
                $("#txtComentarios").prop("disabled", false)

                $("#IdTipoTransporte").prop("disabled", false)
                $("#IdDestinatario").prop("disabled", false)
                $("#IdMotivoTraslado").prop("disabled", false)
                $("#IdTransportista").prop("disabled", false)
                $("#PlacaVehiculo").prop("disabled", false)
                $("#MarcaVehiculo").prop("disabled", false)
                $("#NumIdentidadConductor").prop("disabled", false)
                $("#NombreConductor").prop("disabled", false)
                $("#ApellidoConductor").prop("disabled", false)
                $("#LicenciaConductor").prop("disabled", false)
                $("#Peso").prop("disabled", false)
                $("#Bulto").prop("disabled", false)
                $("#cboEstadoFE").prop("disabled", false)
            }
            else {
                $("#btnGenerarPDF").hide();
                $("#btnGenerarGuia").hide();
                //extra
                console.log("NO ES GUIAAA")
                $("#btnEditar").show()
                $("#IdTipoDocumentoRef").prop("disabled", false)
                $("#IdCuadrilla").prop("disabled", false)
                $("#EntregadoA").prop("disabled", false)
                $("#SerieNumeroRef").prop("disabled", false)
                $("#txtComentarios").prop("disabled", false)

                $("#IdTipoTransporte").prop("disabled", false)
                $("#IdDestinatario").prop("disabled", false)
                $("#IdMotivoTraslado").prop("disabled", false)
                $("#IdTransportista").prop("disabled", false)
                $("#PlacaVehiculo").prop("disabled", false)
                $("#MarcaVehiculo").prop("disabled", false)
                $("#NumIdentidadConductor").prop("disabled", false)
                $("#NombreConductor").prop("disabled", false)
                $("#ApellidoConductor").prop("disabled", false)
                $("#LicenciaConductor").prop("disabled", false)
                $("#Peso").prop("disabled", false)
                $("#Bulto").prop("disabled", false)
                $("#cboEstadoFE").prop("disabled", false)
            }


            setTimeout(() => {
                $("#IdObra").val(movimiento.IdObra)
            },200)

            //AnexoDetalle
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
            console.log(Detalle);
            console.log("Detalle");
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
    disabledmodal(true);

    $("#btnExtorno").hide();
    $("#btnAgregarItem").prop("disabled", true);
    OcultarCampos();
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

    console.log(total);
    console.log(subtotal);

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(subtotal.toFixed(2));
    $("#txtImpuesto").val(impuesto.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));

}


function AgregarLineaDetalle(contador, detalle) {

    let tr = '';
    let UnidadMedida;
    let Almacen;
    let IdUnidadMedida = detalle.IdUnidadMedida
    let IdAlmacen = detalle.IdAlmacen
    console.log(detalle);



    $.ajaxSetup({ async: false });
    $.post("/GrupoUnidadMedida/ListarDefinicionGrupoxIdDefinicionSelect", { 'IdDefinicionGrupo': detalle.IdDefinicionGrupoUnidad }, function (data, status) {
        UnidadMedida = JSON.parse(data);
    });


    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        Almacen = JSON.parse(data);
    });

    tr = `<tr>
        <td>`+ detalle.CodigoArticulo + `</td>
        <td>`+ detalle.CodigoArticulo + `</td>
        <td>
          <input style="display:none" class="form-control" type="text"  id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" value="` + detalle.IdArticulo + `" />
          <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo + `" disabled/>
        </td>
        <td>
             <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedidaAlt == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedidaAlt + `" selected>` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedidaAlt + `">` + UnidadMedida[i].DescUnidadMedidaAlt + `</option>`;
        }
    }
    tr += `</select>
        </td>
        <td>
            <input class="form-control" type="number" name="txtStock[]" value="0" id="txtStock` + contador + `" disabled>
        </td>
        <td>
            <input class="form-control" type="number" name="txtCantidadNecesaria[]" value="`+ detalle.Cantidad + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control" type="number" name="txtPrecioInfo[]" value="`+ detalle.PrecioUnidadTotal + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control changeTotal" type="number" style="width:100px" value="`+ detalle.Total + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
        </td>

        <td>
        <select class="form-control" style="width:100px" id="cboAlmacenOrigen`+ contador + `" name="cboAlmacenOrigen[]" disabled>`;
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
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == detalle.IdAlmacenDestino) {
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
            <button type="button" class="btn-sm btn btn-danger borrar fa fa-trash" disabled></button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);




    var AlmacenOrigen = $("#cboAlmacenOrigen" + contador).val();
    var IdArticulo = $("#txtIdArticulo" + contador).val();

    $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': AlmacenOrigen }, function (data, status) {
        let datos = JSON.parse(data);
        console.log(datos);
        $("#txtStock" + contador).val(datos[0].Stock);
    });



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
    if (TipoItem == 1) {
        $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto, 'IdAlmacen': IdAlmacen, 'Estado': 1, }, function (data, status) {

            if (data == "error") {
                swal("Informacion!", "No se encontro Articulo")
            } else {
                let items = JSON.parse(data);
                console.log(items);
                let tr = '';

                for (var i = 0; i < items.length; i++) {
                    //if (items[i].Inventario == TipoItem) {
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
                    //if (TipoItem == 2 && items[i].Inventario == false) {
                    //    tr += '<tr>' +
                    //        '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
                    //        '<td>' + items[i].Codigo + '</td>' +
                    //        '<td>' + items[i].Descripcion1 + '</td>' +
                    //        '<td>' + items[i].Stock + '</td>' +
                    //        '<td>' + items[i].UnidadMedida + '</td>' +
                    //        '</tr>';
                    //}
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
                        tr += '<tr>' +
                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                            '<td>' + items[i].Codigo + '</td>' +
                            '<td>' + items[i].Descripcion1 + '</td>' +
                            '<td>-</td>' +
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
    $.post("../Almacen/ObtenerAlmacenxIdUsuario", function (data, status) {

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

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();
    if (TipoItem == 3) {
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
    else {
        $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo, 'IdAlmacen': Almacen, 'Estado': 1 }, function (data, status) {

            if (data == "error") {
                swal("Info!", "No se encontro Articulo")
                tableItems.destroy();
            } else {
                let datos = JSON.parse(data);
                console.log(datos);
                $("#cboGrupoUnidadMedida").val(0).change();

                $("#cboGrupoUnidadMedida").val(29).change();
                $("#cboGrupoUnidadMedida").prop("disabled", true);




                $("#txtCodigoItem").val(datos[0].Codigo);
                $("#txtIdItem").val(datos[0].IdArticulo);
                $("#txtDescripcionItem").val(datos[0].Descripcion1);
                $("#cboMedidaItem").val(datos[0].IdUnidadMedida);
                $("#txtPrecioUnitarioItem").val(datos[0].UltimoPrecioCompra);
                $("#txtStockAlmacenItem").val(datos[0].Stock);
                $("#txtPrecioUnitarioItem").val(datos[0].PrecioPromedio)
                $("#txtPrecioUnitarioItemOriginal").val(datos[0].PrecioPromedio);
                $("#txtPrecioUnitarioItem").prop("disabled", true);



                $("#cboMedidaItem").val(29).change();

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

function ObtenerDatosDefinicion() {
    let IdDefinicionGrupo = $("#cboMedidaItem").val();
    $.post("/GrupoUnidadMedida/ObtenerDefinicionUnidadMedidaxIdDefinicionGrupo", { 'IdDefinicionGrupo': IdDefinicionGrupo }, function (data, status) {
        let datos = JSON.parse(data);
        $("#txtPrecioUnitarioItem").val($("#txtPrecioUnitarioItemOriginal").val() * datos.CantidadAlt);
    });
}

function ValidarIgual() {
    let IdAlmacenDestino = $("#IdAlmacenDestino").val();
    let IdAlmacenOrigen = $("#cboAlmacen").val();
    if (IdAlmacenOrigen == IdAlmacenDestino) {
        swal("Informacion!", "Seleccione un almacen diferente al Origen");
        $("#IdAlmacenDestino").val(0)
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


function CargarMotivoTraslado() {
    $.ajaxSetup({ async: false });
    $.post("/EntradaMercancia/ObtenerMotivoTraslado", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            let option = "";
            option = `<option value="0">SELECCIONE MOTIVO TRASLADO</option>`
            for (var i = 0; i < datos.length; i++) {
                option += `<option value="` + datos[i].IdMotivoTraslado + `">` + datos[i].CodigoSunat + '-' + datos[i].Descripcion + `</option>`
            }
            $("#IdMotivoTraslado").html(option);
            $("#IdMotivoTraslado").select2();
        } else {

        }
    });
}

function CargarProveedor() {
    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PROVEEDOR</option>`;
        console.log(Proveedor);
        for (var i = 0; i < Proveedor.length; i++) {
            option += `<option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].NumeroDocumento + `-` + Proveedor[i].RazonSocial + `</option>`
        }

        $("#IdTransportista").html(option);
        $("#IdDestinatario").html(option);
        $("#IdTransportista").select2();
        $("#IdDestinatario").select2();

    });
}

function GenerarGuia() {

    let IdMovimiento = $("#txtId").val();
    Swal.fire({
        title: 'DESEA GENERAR GUIA?',
        text: "Desea generar guia electronica",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar Guia Electronica!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Movimientos/GenerarGuia",
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
                    let respuetaguia = JSON.parse(data);
                    console.log(respuetaguia);
                    Swal.fire(
                        'Correcto',
                        respuetaguia.Message,
                        'success'
                    )

                    CerrarModal();
                    ObtenerDatosxID(IdMovimiento);


                    //if (data == 1) {
                    //    Swal.fire(
                    //        'Correcto',
                    //        'Proceso Realizado Correctamente',
                    //        'success'
                    //    )
                    //    //swal("Exito!", "Proceso Realizado Correctamente", "success")
                    //    table.destroy();


                    //} else {
                    //    Swal.fire(
                    //        'Error!',
                    //        'Ocurrio un Error!',
                    //        'error'
                    //    )

                    //}


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
    //$("#IdTipoDocumentoRef").prop('disabled', valorbolean);
    //$("#SerieNumeroRef").prop('disabled', valorbolean);
    $("#IdCuadrilla").prop('disabled', valorbolean);
    $("#IdResponsable").prop('disabled', valorbolean);
    $("#txtComentarios").prop('disabled', valorbolean)
    $("#btn_agregaritem").prop('disabled', valorbolean)
    $("#EntregadoA").prop('disabled', valorbolean)

    if (valorbolean) {
        $("#btnGrabar").hide()
        $("#btnExtorno").show();
        $("#total_editar").show();
        $("#total_nuevo").hide();
        $("#btnNuevo").show();

    } else {
        $("#btnExtorno").hide();
        $("#total_editar").hide();
        $("#total_nuevo").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
    }

}

function GenerarReporte(id) {
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {
        $.ajaxSetup({ async: false });
        $.post("/EntradaMercancia/GenerarReporte", { 'NombreReporte': 'TransferenciaMercancia', 'Formato': 'PDF', 'Id': id }, function (data, status) {
            let datos;
            if (validadJson(data)) {
                let datobase64;
                datobase64 = "data:application/octet-stream;base64,"
                datos = JSON.parse(data);
                //datobase64 += datos.Base64ArchivoPDF;
                //$("#reporteRPT").attr("download", 'Reporte.' + "pdf");
                //$("#reporteRPT").attr("href", datobase64);
                //$("#reporteRPT")[0].click();
                verBase64PDF(datos);
                Swal.fire(
                    'Correcto',
                    'Reporte Generado Correctamente',
                    'success'
                )
            } else {
                respustavalidacion
            }
        });
    }, 100)
}
function NumeracionDinamica() {
    var i = 1;
    $('#tabla > tbody  > tr').each(function (e) {
        $(this)[0].cells[0].outerHTML = '<td>' + i + '</td>';
        i++;
    });
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
function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}


function CargarVehiculos() {
    $.ajaxSetup({ async: false });
    $.post("/Vehiculo/ObtenerVehiculo", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            let option = '<option value="0">SELECCIONE</option>';
            for (var i = 0; i < datos.length; i++) {
                option += `<option value="` + datos[i].Placa + `">` + datos[i].Placa + `</option>`
            }
            $("#PlacaVehiculo").html(option);
            $("#PlacaVehiculo").select2();
        } else {

        }
    });
}


function BuscarVehiculoxPlaca() {

    var Placa = $("#PlacaVehiculo").val();
    $.ajaxSetup({ async: false });
    $.post("/Vehiculo/ObtenerDatosConductorxPlaca", { 'Placa': Placa }, function (data, status) {

        if (validadJson(data)) {
            let datos = JSON.parse(data);
            console.log(datos);
            $("#MarcaVehiculo").val(datos[0].MarcaDescripcion);
            $("#LicenciaConductor").val(datos[0].Licencia);
            $("#NumIdentidadConductor").val(datos[0].NumeroDocumento);

            var NombreApellido = datos[0].RazonSocial;
            var a = NombreApellido.split(" ");
            var Nombre = a[2];
            var Apellido = a[0] + " " + a[1];
            $("#NombreConductor").val(Nombre);
            $("#ApellidoConductor").val(Apellido);
        } else {

        }

    });


}




function GenerarPDF() {

    let IdMovimiento = $("#txtId").val();
    $.post("/Movimientos/GenerarPDF", { 'IdMovimiento': IdMovimiento }, function (data, status) {
        //CerrarModal();
        //ObtenerDatosxID(IdMovimiento);
        console.log(data.message);
        Swal.fire(
            'Correcto',
            data.message,
            'success')

        CerrarModal();
        ObtenerDatosxID(IdMovimiento);

        return;

        //let datos;
        //if (validadJson(data)) {
        //    console.log(data)
        //    Swal.fire(
        //        'Correcto',
        //        data.Message,
        //        'success'
        //    )
        //    return;
        //    //let datobase64;
        //    //datobase64 = "data:application/octet-stream;base64,"
        //    //datos = JSON.parse(data);
        //    //verBase64PDF(datos)
        //} else {
        //    //respustavalidacion
        //}
    });

}

function OcultarCampos() {
    if ($("#IdTipoDocumentoRef").val() == 1) {
        console.log("mostrars")
        $(".ocultate").show()
        $("#IdDestinatario").val(24154).change();
        $("#IdTransportista").val(24154).change();
        $("#IdMotivoTraslado").val(09).change();
        $("#PlacaVehiculo").val(0).change()
        $("#Peso").val(1);
        $("#Bulto").val(1);
        $("#IdTipoTransporte").val('02')
        $("#SerieNumeroRef").prop("disabled", true)
        $("#SerieNumeroRef").val("")
    } else {
        console.log("ocultars")
        $(".ocultate").hide()
        $("#SerieNumeroRef").prop("disabled", false)
    }

}
function Editar() {
    let varIdMovimiento = $("#txtId").val();
    let varIdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let varNumSerieTipoDocumentoRef = $("#SerieNumeroRef").val();
    let varComentario = $("#txtComentarios").val();
    let varTipoTransporte = $("#IdTipoTransporte").val();
    let varIdDestinatario = $("#IdDestinatario").val();
    let varIdMotivoTraslado = $("#IdMotivoTraslado").val();
    let varIdTransportista = $("#IdTransportista").val();
    let varPlacaVehiculo = $("#PlacaVehiculo").val();
    let varMarcaVehiculo = $("#MarcaVehiculo").val();
    let varNumIdentidadConductor = $("#NumIdentidadConductor").val();
    let varNombreConductor = $("#NombreConductor").val();
    let varApellidoConductor = $("#ApellidoConductor").val();
    let varLicenciaConductor = $("#LicenciaConductor").val();
    let varPeso = $("#Peso").val();
    let varBulto = $("#Bulto").val();

    var TipDoc = $("#IdTipoDocumentoRef").val();
    if (TipDoc != "1") {
        varTipoTransporte = null,
            varIdDestinatario = null,
            varIdMotivoTraslado = null,
            varIdTransportista = null,
            varPlacaVehiculo = null,
            varMarcaVehiculo = null,
            varNumIdentidadConductor = null,
            varNombreConductor = null,
            varApellidoConductor = null,
            varLicenciaConductor = null,
            varPeso = null,
            varBulto = null
    }

    $.post('/Movimientos/UpdateMovimientoTransferencia', {
        'IdMovimiento': varIdMovimiento,
        'IdTipoDocumentoRef': varIdTipoDocumentoRef,
        'NumSerieTipoDocumentoRef': varNumSerieTipoDocumentoRef,
        'Comentario': varComentario,
        'TipoTransporte': varTipoTransporte,
        'IdDestinatario': varIdDestinatario,
        'IdMotivoTraslado': varIdMotivoTraslado,
        'IdTransportista': varIdTransportista,
        'PlacaVehiculo': varPlacaVehiculo,
        'MarcaVehiculo': varMarcaVehiculo,
        'NumIdentidadConductor': varNumIdentidadConductor,
        'NombreConductor': varNombreConductor,
        'ApellidoConductor': varApellidoConductor,
        'LicenciaConductor': varLicenciaConductor,
        'Peso': varPeso,
        'Bulto': varBulto,

    }, function (data, status) {

        if (data != 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            CerrarModal()
            ConsultaServidor()
        } else {
            swal("Error!", "Ocurrio un Error")
            CerrarModal()
        }

    });

}
