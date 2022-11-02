let contadorBase = 0;
let contadorObra = 0;
let contadorAlmacen = 0;

function ObtenerConfiguracionDecimales() {
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
    $.post("/Obra/ObtenerObraxIdBase", {'IdBase':IdBase }, function (data, status) {
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
    ObtenerConfiguracionDecimales();
    var url = "../Movimientos/ObtenerMovimientosIngresos";
   
    ObtenerConfiguracionDecimales();
    //ConsultaServidor(url);
    listarIngresosDT();

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
                '<td>' + formatNumber( movimientos[i].Total) + '</td>' +
                '<td>' + movimientos[i].DescCuadrilla + '</td>' +

                '<td>' + movimientos[i].NombObra + '</td>' +
                '<td>' + movimientos[i].NombAlmacen + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + movimientos[i].IdMovimiento + ')"></button>' +
                '<button class="btn btn-primary" onclick="GenerarReporte(' + movimientos[i].IdMovimiento + ')">R</button>'+
                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {

    disabledmodal(false);
    $("#lblTituloModal").html("Nuevo Ingreso");
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante=='false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }
    $("#btnExtorno").hide()
    CargarCentroCosto();
    listarEmpleados();
    ObtenerTiposDocumentos()


    //CargarAlmacen()
    //CargarBase()
    CargarTipoDocumentoOperacion()
    ObtenerCuadrillas()
    CargarSeries();
    

    //AgregarLinea();
    



    CargarSolicitante(1);
    CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();
    CargarImpuestos();
    $("#cboImpuesto").val(2).change();
    $("#cboSerie").val(1).change();

    $("#cboMoneda").val(1);
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#total_items").html("");
    $("#NombUsuario").html("");
    $("#CreatedAt").html("");
    $("#SerieNumeroRef").val("");


    validarseriescontable();
    
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
    let IdGrupoUnidadMedida= $("#cboGrupoUnidadMedida").val();
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
    if (CodigoItem.length==0) {
        swal("Informacion!", "Debe Especificar Un Producto!");
        return;
    }

    if (ValidarCantidad == "" || ValidarCantidad == null || ValidarCantidad == "0" || !$.isNumeric(ValidarCantidad) ) {
        swal("Informacion!", "Debe Especificar Cantidad!");
        return;
    }

    if (ValidarPrecioItem == "" || ValidarPrecioItem == null || ValidarPrecioItem == "0" || !$.isNumeric(ValidarPrecioItem)) {
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

    if ($("#txtPrecioUnitarioItem").val()<=0) {
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
    tr += `<tr id="tritem` + contador +`">
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
            <td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
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
            <td><button class="btn btn-xs btn-danger borrar" onclick="borrartditem(`+contador+`)">-</button></td>
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
    $("#txtCantidadNecesaria" + contador).val(formatNumber(parseFloat( CantidadItem).toFixed(DecimalesCantidades))).change();
    $("#txtPrecioInfo" + contador).val(PrecioUnitarioItem).change();
    $("#cboProyecto" + contador).val(ProyectoItem);
    $("#cboAlmacen" + contador).val(AlmacenItem);
    $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);

    LimpiarModalItem();
}


//function LimpiarDatosModalItems() {

//}

function borrartditem(contador) {
    $("#tritem" + contador).remove()
    CalcularTotales();
}



function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", {estado:1}, function (data, status) {
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
    $.post("/GrupoUnidadMedida/ObtenerGrupoUnidadMedida", {'estado':1}, function (data, status) {
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
    $.post("/TipoDocumentoOperacion/ObtenerTipoDocumentoOperacion", {estado:1}, function (data, status) {
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
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 1 && lista[i].Estado) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
            else { }
        }
        
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) {
            if (lista[i].CodeExt=="59") {
                contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>";

            }
        }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



llenarComboTipoDocumentoOperacion
$(document).on('click', '.borrar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();

    let filas = $("#tabla").find('tbody tr').length;
    console.log("filas");
    console.log(filas);
});



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

    //let varSerie = $("#cboSerie").val();

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
    //Validaciones
    if ($("#cboTipoDocumentoOperacion").val() == 0) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo de Movimiento',
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
    let IdTipoDocumentoRef = $("#IdTipoDocumentoRef").val();
    let SerieNumeroRef = $("#SerieNumeroRef").val();
    //END Cabecera

    //let oMovimientoDetalleDTO = {};
    //oMovimientoDetalleDTO.Total = arrayTotal
   
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
                'Referencia': arrayReferencia[i]
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
            'IdCuadrilla': IdCuadrilla,
            'IdResponsable': IdResponsable,
            'IdTipoDocumentoRef': IdTipoDocumentoRef,
            'NumSerieTipoDocumentoRef': SerieNumeroRef
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
            if (data> 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                CerrarModal();
                ObtenerDatosxID(data);
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
    $("#btnExtorno").show();
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
            $("#txtTotalAntesDescuento").val(formatNumber( movimiento.SubTotal.toFixed(DecimalesPrecios)))
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
            $("#txtComentarios").html(movimiento.Comentario)
            $("#txtNumeracion").val(movimiento.Correlativo);
            $("#txtTipoCambio").val(movimiento.TipoCambio);
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

            //if (validadJson(solicitudes[0].DetallesAnexo)) {
            //     let DetalleAnexo = solicitudes[0].DetallesAnexo;
            //for (var i = 0; i < DetalleAnexo.length; i++) {
            //    AgregarLineaDetalleAnexo(DetalleAnexo[i].IdSolicitudRQAnexos, DetalleAnexo[i].Nombre)
            //}
            //}
           


        }
    });

    /*DISABLE*/
    disabledmodal(true)

    /*DISABLE*/

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

    $("#txtTotalAntesDescuento").val(formatNumber(subtotal.toFixed(2)));
    $("#txtImpuesto").val(formatNumber(impuesto.toFixed(2)));
    $("#txtTotal").val(formatNumber(total.toFixed(2)));

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
        <td>
          <input class="form-control" type="text" value="`+ (contador+1) + `" disabled />
        </td>
        <td>
          <input class="form-control" type="text"  id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" value="` + detalle.DescripcionArticulo +`" disabled/>
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
            <input class="form-control" type="text" name="txtCantidadNecesaria[]" value="`+ formatNumber( detalle.Cantidad.toFixed(DecimalesCantidades) )+ `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control" type="text" name="txtPrecioInfo[]" value="`+ formatNumber( detalle.PrecioUnidadTotal.toFixed(DecimalesPrecios)) + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled>
        </td>
        <td>
            <input class="form-control changeTotal" type="text" style="width:100px" value="`+ formatNumber( detalle.Total.toFixed(DecimalesPrecios)) + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled>
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
            <button type="button" class="btn-sm btn btn-danger" disabled> - </button>   
         </td>
    </tr>`

    $("#tabla").find('tbody').append(tr);
    //$("#cboPrioridadDetalle" + contador).val(Prioridad);


    

}

function EliminarDetalle(IdSolicitudRQDetalle, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleSolicitud", { 'IdSolicitudRQDetalle': IdSolicitudRQDetalle }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
                CalcularTotales()
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

    $.post("/Articulo/ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", { 'IdTipoProducto': IdTipoProducto ,'IdAlmacen':IdAlmacen,'Estado':1, }, function (data, status) {

        if (data == "error") {
            swal("Informacion!", "No se encontro Articulo")
        } else {
            let items = JSON.parse(data);
            console.log(items);
            let tr = '';

            for (var i = 0; i < items.length; i++) {
               /* if (items[i].Inventario == TipoItem) {*/
                    tr += '<tr>' +
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

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();
    $.post("/Articulo/ListarArticulosxSociedadxAlmacenStockxProducto", { 'IdArticulo': IdArticulo,'IdAlmacen': Almacen,'Estado':1 }, function (data, status) {

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
            
            tableItems.destroy();
        }
    });

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
    let IdMovimiento=$("#txtId").val();
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
                        CerrarModal()
                        ModalNuevo()
                        
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



function CargarBasesObraAlmacenSegunAsignado() {
    let respuesta = 'false';
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerBaseAlmacenxIdUsuarioSession", function (data, status) {
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            console.log(datos[0]);
            contadorBase=datos[0].CountBase;
            contadorObra=datos[0].CountObra;
            contadorAlmacen = datos[0].CountAlmacen;
            AbrirModal("modal-form");
            CargarBase();
            if (contadorBase == 1 && contadorObra == 1 && contadorAlmacen == 1) {
                $.ajaxSetup({ async: false });
                $("#IdBase").val(datos[0].IdBase).change();
                $("#IdObra").val(datos[0].IdObra).change();
                $("#cboAlmacen").val(datos[0].IdAlmacen).change();
                $("#IdBase").prop('disabled',true);
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


            respuesta= 'true';
        } else {
            respuesta= 'false';
        }
    });
    return respuesta;
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

    if (datosrespuesta.FechaRelacion.length==0) {
        swal("Informacion!", "No  se encuentra Fecha de Contabilizacion Activa");
        return true;
    }

    if (datosrespuesta.FechaRelacion.length > 0) {
        for (var i = 0; i < datosrespuesta.FechaRelacion.length; i++) {
            console.log(datosrespuesta.FechaRelacion[i]);
            if (datosrespuesta.FechaRelacion[i].StatusPeriodo==1) {
                estado = 1;
            }
        }
       
    }

    if (estado==0) {
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

function GenerarReporte(id) {
    $.ajaxSetup({ async: false });
    $.post("GenerarReporte", { 'NombreReporte': 'EntradaMercancia', 'Formato': 'PDF', 'Id': id }, function (data, status) {
        let datos;
        if (validadJson(data)) {
            let datobase64;
            datobase64 = "data:application/octet-stream;base64,"
            datos = JSON.parse(data);
            datobase64+=datos.Base64ArchivoPDF;
            $("#reporteRPT").attr("download", 'Reporte.' + "pdf");
            $("#reporteRPT").attr("href", datobase64);
            $("#reporteRPT")[0].click();

        } else {
            respustavalidacion
        }
    });
}


function listarIngresosDT() {
    tableingresos = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: '../Movimientos/ObtenerMovimientosIngresosDT',
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
                    return `<button class= "btn btn-primary fa fa-pencil btn-xs" onclick = "ObtenerDatosxID(` + full.IdMovimiento + `)"></button>
                        <button class="btn btn-primary btn-xs" onclick="GenerarReporte(` + full.IdMovimiento + `)">R</button>`
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
                    return full.FechaDocumento.split('T')[0]
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return 'FACTURA PROVEEDOR'
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombSerie.toUpperCase() + '-' + full.Correlativo
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.Total) 
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.DescCuadrilla
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            }


        ],
        "bDestroy": true
    }).DataTable();
}

