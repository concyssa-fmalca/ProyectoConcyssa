let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;






window.onload = function () {
    var url = "ObtenerSolicitudesRQ";
    //ObtenerConfiguracionDecimales();
    ConsultaServidor(url);


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



function ModalNuevo() {

    $("#lblTituloModal").html("Nueva Entrada Mercancia");
    //AgregarLinea();
    CargarSeries();
    CargarSolicitante(1);
    CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();

    CargarImpuestos();
    $("#cboImpuesto").val(2).change();
    $("#cboSerie").val(1).change();

    $("#cboMoneda").val("USD");
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
}



//function ConsultaServidor(url) {

//    $.post(url, function (data, status) {

//        //console.log(data);
//        if (data == "error") {
//            table = $("#table_id").DataTable(lenguaje);
//            return;
//        }

//        let tr = '';

//        let solicitudes = JSON.parse(data);
//        let total_solicitudes = solicitudes.length;
//        console.log("cabecera");
//        console.log(solicitudes);

//        for (var i = 0; i < solicitudes.length; i++) {


//            tr += '<tr>' +
//                '<td>' + (i + 1) + '</td>' +
//                '<td>' + solicitudes[i].Solicitante.toUpperCase() + '</td>' +
//                '<td>' + solicitudes[i].Serie.toUpperCase() + '</td>' +
//                '<td>' + solicitudes[i].Numero + '</td>' +
//                '<td>' + solicitudes[i].TotalAntesDescuento.toFixed(DecimalesImportes) + '</td>' +
//                '<td>' + solicitudes[i].Total.toFixed(DecimalesImportes) + '</td>' +
//                '<td>' + solicitudes[i].DetalleEstado.toUpperCase() + '</td>' +
//                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ',' + solicitudes[i].Estado + ')"></button>' +
//                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
//                '</tr>';
//        }

//        $("#tbody_Solicitudes").html(tr);
//        $("#spnTotalRegistros").html(total_solicitudes);

//        table = $("#table_id").DataTable(lenguaje);

//    });

//}





function OpenModalItem() {


    let ClaseArticulo = $("#cboClaseArticulo").val();
    let Moneda = $("#cboMoneda").val();
    if (ClaseArticulo == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
    } else if (Moneda == 0) {
        swal("Informacion!", "Debe Seleccionar Moneda!");
    }
    else {
        $("#cboPrioridadItem").val(2);
        $("#cboClaseArticulo").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarProyectos();
        CargarCentroCostos();
        CargarAlmacen();
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
               <a href="/SolicitudRQ/Download?ImageName=`+ Nombre + `" >Descargar</a>
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
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
    });

    $.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
        LineaNegocio = JSON.parse(data);
    });

    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
        CentroCosto = JSON.parse(data);
    });

    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
        Proyecto = JSON.parse(data);
    });

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
        tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" id="cboPrioridadDetalle`+ contador + `" name="cboPrioridadDetalle[]">
                <option value="0">Seleccione</option>
                <option value="1">ALTA</option>
                <option value="2">BAJA</option>
            </select>
            </td>
            <td input style="display:none;"><input class="form-control" type="date" value="`+ today + `" id="txtFechaNecesaria` + contador + `" name="txtFechaNecesaria[]"></td>
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
            <td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()"></td>
            <td>
            <select class="form-control" style="width:100px" id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td input style="display:none;">
            <select class="form-control" style="width:100px" name="cboProveedor[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proveedor.length; i++) {
        tr += `  <option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].RazonSocial + `</option>`;
    }
    tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td input style="display:none;"><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td input style="display:none;">
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < LineaNegocio.length; i++) {
        tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" id="cboCentroCostos`+ contador + `" name="cboCentroCostos[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < CentroCosto.length; i++) {
        tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" id="cboProyecto`+ contador + `" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proyecto.length; i++) {
        tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
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

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);

    LimpiarModalItem();
}


//function LimpiarDatosModalItems() {

//}


function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", function (data, status) {
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

function CargarProyectos() {
    $.ajaxSetup({ async: false });
    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
        let proyecto = JSON.parse(data);
        llenarComboProyectoItem(proyecto, "cboProyectoItem", "Seleccione")
    });
}

function CargarCentroCostos() {
    $.ajaxSetup({ async: false });
    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
        let proyecto = JSON.parse(data);
        llenarComboCentroCostoItem(proyecto, "cboCentroCostoItem", "Seleccione")
    });
}

function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdAlmacen + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

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
    let arrayIdArticulo = new Array();
    let arrayIdUnidadMedida = new Array();
    let arrayFechaNecesaria = new Array();
    let arrayIdMoneda = new Array();
    let arrayTipoCambio = new Array();
    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();
    let arrayAlmacen = new Array();
    let arrayProveedor = new Array();
    let arrayNumeroFabricacion = new Array();
    let arrayNumeroSerie = new Array();
    let arrayLineaNegocio = new Array();
    let arrayCentroCosto = new Array();
    let arrayProyecto = new Array();
    let arrayReferencia = new Array();
    let arrayIdSolicitudDetalle = new Array();

    //let arrayIdAlmacen = new Array();
    let arrayPrioridad = new Array();

    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });
    $("select[name='cboUnidadMedida[]']").each(function (indice, elemento) {
        arrayIdUnidadMedida.push($(elemento).val());
    });
    $("input[name='txtFechaNecesaria[]']").each(function (indice, elemento) {
        arrayFechaNecesaria.push($(elemento).val());
    });
    $("select[name='cboMoneda[]']").each(function (indice, elemento) {
        arrayIdMoneda.push($(elemento).val());
    });
    $("input[name='txtTipoCambio[]']").each(function (indice, elemento) {
        arrayTipoCambio.push($(elemento).val());
    });
    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($(elemento).val());
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val());
    });
    $("select[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacen.push($(elemento).val());
    });
    $("select[name='cboProveedor[]']").each(function (indice, elemento) {
        arrayProveedor.push($(elemento).val());
    });
    $("input[name='txtNumeroFacbricacion[]']").each(function (indice, elemento) {
        arrayNumeroFabricacion.push($(elemento).val());
    });
    $("input[name='txtNumeroSerie[]']").each(function (indice, elemento) {
        arrayNumeroSerie.push($(elemento).val());
    });
    $("select[name='cboLineaNegocio[]']").each(function (indice, elemento) {
        arrayLineaNegocio.push($(elemento).val());
    });
    $("select[name='cboCentroCostos[]']").each(function (indice, elemento) {
        arrayCentroCosto.push($(elemento).val());
    });
    $("select[name='cboProyecto[]']").each(function (indice, elemento) {
        arrayProyecto.push($(elemento).val());
    });
    $("input[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });
    $("input[name='txtIdSolicitudRQDetalle[]']").each(function (indice, elemento) {
        arrayIdSolicitudDetalle.push($(elemento).val());
    });

    $("select[name='cboPrioridadDetalle[]']").each(function (indice, elemento) {
        arrayPrioridad.push($(elemento).val());
    });



    let varIdSolicitud = $("#txtId").val();

    let varSerie = $("#cboSerie").val();
    let varSerieDescripcion = $("#cboSerie").find('option:selected').text();
    let varEstado = $("#txtEstado").val(); //Abierto
    let varNumero = $("#txtNumeracion").val();
    let varMoneda = $("#cboMoneda").val();
    let varTipoCambio = $("#txtTipoCambio").val();
    //let varClaseArticulo = $("#cboClaseArticulo").val();
    let varSolicitante = $("#cboEmpleado").val();
    let varFechaContabilizacion = $("#txtFechaContabilizacion").val();
    let varSucursal = $("#cboSucursal").val();
    let varFechaValidoHasta = $("#txtFechaValidoHasta").val();
    let varDepartamento = $("#cboDepartamento").val();
    let varFechaDocumento = $("#txtFechaDocumento").val();
    let varTitular = $("#cboTitular").val();
    let varTotalAntesDescuento = $("#txtTotalAntesDescuento").val();
    let varComentarios = $("#txtComentarios").val();
    let varImpuesto = $("#txtImpuesto").val();
    let varTotal = $("#txtTotal").val();
    let varPrioridad = $("#cboPrioridad").val();
    let varClaseArticulo = $("#cboClaseArticulo").val();



    arrayIdSolicitudRQAnexo = new Array();
    arrayNombreAnexo = new Array();
    arrayGeneralAnexo = new Array();
    $("input[name='txtIdSolicitudRQAnexo[]']").each(function (indice, elemento) {
        arrayIdSolicitudRQAnexo.push($(elemento).val());
    });
    $("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
        arrayNombreAnexo.push($(elemento).val());
    });
    for (var i = 0; i < arrayNombreAnexo.length; i++) {
        arrayGeneralAnexo.push({ 'IdSolicitudRQAnexos': arrayIdSolicitudRQAnexo[i], 'Nombre': arrayNombreAnexo[i] });
    }


    //$.post('UpdateInsertSolicitud', {
    //    'IdSolicitudRQ': varIdSolicitud,
    //    'IdSerie': varSerie,
    //    'Serie': varSerieDescripcion,
    //    'Numero': varNumero,
    //    'IdSolicitante': varSolicitante,
    //    'IdSucursal': varSucursal,
    //    'IdDepartamento': varDepartamento,
    //    'IdClaseArticulo': varClaseArticulo,
    //    'IdTitular': varTitular,
    //    'IdMoneda': varMoneda,
    //    'TipoCambio': varTipoCambio,
    //    'IndicadorImpuesto': 0,
    //    'TotalAntesDescuento': varTotalAntesDescuento,
    //    'Impuesto': varImpuesto,
    //    'Total': varTotal,
    //    'FechaContabilizacion': varFechaContabilizacion,
    //    'FechaValidoHasta': varFechaValidoHasta,
    //    'FechaDocumento': varFechaDocumento,
    //    'Comentarios': varComentarios,
    //    'Estado': varEstado,
    //    'Prioridad': varPrioridad,
    //    'IdArticulo': arrayIdArticulo,
    //    'IdUnidadMedida': arrayIdUnidadMedida,
    //    'FechaNecesaria': arrayFechaNecesaria,
    //    'IdItemMoneda': arrayIdMoneda,
    //    'ItemTipoCambio': arrayTipoCambio,
    //    'CantidadNecesaria': arrayCantidadNecesaria,
    //    'PrecioInfo': arrayPrecioInfo,
    //    'IdIndicadorImpuesto': arrayIndicadorImpuesto,
    //    'ItemTotal': arrayTotal,
    //    'IdAlmacen': arrayAlmacen,
    //    'IdProveedor': arrayProveedor,
    //    'NumeroFabricacion': arrayNumeroFabricacion,
    //    'NumeroSerie': arrayNumeroSerie,
    //    'IdLineaNegocio': arrayLineaNegocio,
    //    'IdCentroCostos': arrayCentroCosto,
    //    'IdProyecto': arrayProyecto,
    //    'Referencia':arrayReferencia,
    //    'IdSolicitudRQDetalle': arrayIdSolicitudDetalle,
    //    'DetalleAnexo': arrayGeneralAnexo

    //}, function (data, status) {

    //    if (data == 1) {
    //        swal("Exito!", "Proceso Realizado Correctamente", "success")
    //        table.destroy();
    //        ConsultaServidor("ObtenerSolicitudesRQ");
    //        //limpiarDatos();
    //    } else {
    //        swal("Error!", "Ocurrio un Error")
    //        //limpiarDatos();
    //    }

    //    });




    //$.ajax({
    //    url: "UpdateInsertSolicitud",
    //    data: {
    //        'IdSolicitudRQ': varIdSolicitud,
    //        'IdSerie': varSerie,
    //        'Serie': varSerieDescripcion,
    //        'Numero': varNumero,
    //        'IdSolicitante': varSolicitante,
    //        'IdSucursal': varSucursal,
    //        'IdDepartamento': varDepartamento,
    //        'IdClaseArticulo': varClaseArticulo,
    //        'IdTitular': varTitular,
    //        'IdMoneda': varMoneda,
    //        'TipoCambio': varTipoCambio,
    //        'IndicadorImpuesto': 0,
    //        'TotalAntesDescuento': varTotalAntesDescuento,
    //        'Impuesto': varImpuesto,
    //        'Total': varTotal,
    //        'FechaContabilizacion': varFechaContabilizacion,
    //        'FechaValidoHasta': varFechaValidoHasta,
    //        'FechaDocumento': varFechaDocumento,
    //        'Comentarios': varComentarios,
    //        'Estado': varEstado,
    //        'Prioridad': varPrioridad,
    //        'IdArticulo': arrayIdArticulo,
    //        'Prioridad': arrayPrioridad,
    //        'IdUnidadMedida': arrayIdUnidadMedida,
    //        'FechaNecesaria': arrayFechaNecesaria,
    //        'IdItemMoneda': arrayIdMoneda,
    //        'ItemTipoCambio': arrayTipoCambio,
    //        'CantidadNecesaria': arrayCantidadNecesaria,
    //        'PrecioInfo': arrayPrecioInfo,
    //        'IdIndicadorImpuesto': arrayIndicadorImpuesto,
    //        'ItemTotal': arrayTotal,
    //        'IdAlmacen': arrayAlmacen,
    //        'IdProveedor': arrayProveedor,
    //        'NumeroFabricacion': arrayNumeroFabricacion,
    //        'NumeroSerie': arrayNumeroSerie,
    //        'IdLineaNegocio': arrayLineaNegocio,
    //        'IdCentroCostos': arrayCentroCosto,
    //        'IdProyecto': arrayProyecto,
    //        'Referencia': arrayReferencia,
    //        'IdSolicitudRQDetalle': arrayIdSolicitudDetalle,
    //        'DetalleAnexo': arrayGeneralAnexo
    //    },
    //    type: "POST",
    //    cache: false,
    //    beforeSend: function () {
    //        Swal.fire({
    //            title: "Cargando...",
    //            text: "Por favor espere",
    //            showConfirmButton: false,
    //            allowOutsideClick: false
    //        });
    //    },
    //    success: function (data) {
    //        if (data == 1) {
    //            swal("Exito!", "Proceso Realizado Correctamente", "success")
    //            table.destroy();
    //            ConsultaServidor("ObtenerSolicitudesRQ");

    //        } else {
    //            swal("Error!", "Ocurrio un Error")

    //        }
    //    }
    //}).fail(function () {

    //    });








    $.ajax({
        url: "UpdateInsertSolicitud",
        type: "POST",
        async: true,
        data: {
            'IdSolicitudRQ': varIdSolicitud,
            'IdSerie': varSerie,
            'Serie': varSerieDescripcion,
            'Numero': varNumero,
            'IdSolicitante': varSolicitante,
            'IdSucursal': varSucursal,
            'IdDepartamento': varDepartamento,
            'IdClaseArticulo': varClaseArticulo,
            'IdTitular': varTitular,
            'IdMoneda': varMoneda,
            'TipoCambio': varTipoCambio,
            'IndicadorImpuesto': 0,
            'TotalAntesDescuento': varTotalAntesDescuento,
            'Impuesto': varImpuesto,
            'Total': varTotal,
            'FechaContabilizacion': varFechaContabilizacion,
            'FechaValidoHasta': varFechaValidoHasta,
            'FechaDocumento': varFechaDocumento,
            'Comentarios': varComentarios,
            'Estado': varEstado,
            'Prioridad': varPrioridad,
            'IdArticulo': arrayIdArticulo,
            'Prioridad': arrayPrioridad,
            'IdUnidadMedida': arrayIdUnidadMedida,
            'FechaNecesaria': arrayFechaNecesaria,
            'IdItemMoneda': arrayIdMoneda,
            'ItemTipoCambio': arrayTipoCambio,
            'CantidadNecesaria': arrayCantidadNecesaria,
            'PrecioInfo': arrayPrecioInfo,
            'IdIndicadorImpuesto': arrayIndicadorImpuesto,
            'ItemTotal': arrayTotal,
            'IdAlmacen': arrayAlmacen,
            'IdProveedor': arrayProveedor,
            'NumeroFabricacion': arrayNumeroFabricacion,
            'NumeroSerie': arrayNumeroSerie,
            'IdLineaNegocio': arrayLineaNegocio,
            'IdCentroCostos': arrayCentroCosto,
            'IdProyecto': arrayProyecto,
            'Referencia': arrayReferencia,
            'IdSolicitudRQDetalle': arrayIdSolicitudDetalle,
            'DetalleAnexo': arrayGeneralAnexo
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
                ConsultaServidor("ObtenerSolicitudesRQ");

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


function ObtenerDatosxID(IdSolicitudRQ, Estado) {
    $("#lblTituloModal").html("Editar Solicitud");
    AbrirModal("modal-form");


    $.post('ObtenerDatosxID', {
        'IdSolicitudRQ': IdSolicitudRQ,
    }, function (data, status) {


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let solicitudes = JSON.parse(data);
            console.log("sda");
            console.log(solicitudes);

            CargarSeries();
            CargarSolicitante(0);
            CargarSucursales();
            CargarDepartamentos();
            CargarMoneda();
            CargarImpuestos();

            $("#txtId").val(solicitudes[0].IdSolicitudRQ);
            $("#cboSerie").val(solicitudes[0].IdSerie);
            $("#txtNumeracion").val(solicitudes[0].Numero);
            $("#cboMoneda").val(solicitudes[0].IdMoneda);
            $("#txtTipoCambio").val(solicitudes[0].TipoCambio);
            $("#cboClaseArticulo").val(solicitudes[0].IdClaseArticulo);
            $("#cboEmpleado").val(solicitudes[0].IdSolicitante);
            //$("#cboClaseArticulo").val(solicitudes[0].ClaseArticulo);
            $("#cboPrioridad").val(solicitudes[0].Prioridad);
            $("#txtEstado").val(Estado);

            var fechaSplit = (solicitudes[0].FechaContabilizacion.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            $("#txtFechaContabilizacion").val(fecha);

            $("#cboSucursal").val(solicitudes[0].IdSucursal);

            var fechaSplit1 = (solicitudes[0].FechaValidoHasta.substring(0, 10)).split("-");
            var fecha1 = fechaSplit1[0] + "-" + fechaSplit1[1] + "-" + fechaSplit1[2];
            $("#txtFechaValidoHasta").val(fecha1);

            $("#cboDepartamento").val(solicitudes[0].IdDepartamento);

            var fechaSplit2 = (solicitudes[0].FechaDocumento.substring(0, 10)).split("-");
            var fecha2 = fechaSplit2[0] + "-" + fechaSplit2[1] + "-" + fechaSplit2[2];
            $("#txtFechaDocumento").val(fecha2);

            $("#cboTitular").val(solicitudes[0].IdTitular);
            $("#txtTotalAntesDescuento").val((Number(solicitudes[0].TotalAntesDescuento)).toFixed(DecimalesImportes));
            $("#txtComentarios").val(solicitudes[0].Comentarios);
            $("#txtImpuesto").val((Number(solicitudes[0].Impuesto)).toFixed(DecimalesImportes));
            $("#txtTotal").val((Number(solicitudes[0].Total)).toFixed(DecimalesImportes));



            //agrega detalle
            let tr = '';

            let Detalle = solicitudes[0].Detalle;
            console.log("Detalle");
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {

                AgregarLineaDetalle(i, Detalle[i].Prioridad, Detalle[i].IdArticulo, Detalle[i].IdSolicitudRQDetalle, Detalle[i].IdUnidadMedida, Detalle[i].IdIndicadorImpuesto, Detalle[i].IdAlmacen, Detalle[i].IdProveedor, Detalle[i].IdLineaNegocio, Detalle[i].IdCentroCostos, Detalle[i].IdProyecto, Detalle[i].IdItemMoneda, Detalle[i].ItemTipoCambio, Detalle[i].CantidadNecesaria.toFixed(DecimalesCantidades), Detalle[i].PrecioInfo.toFixed(DecimalesPrecios), Detalle[i].ItemTotal.toFixed(DecimalesImportes), Detalle[i].NumeroFabricacion, Detalle[i].NumeroSerie, Detalle[i].FechaNecesaria, Detalle[i].Referencia, Detalle[i].DescripcionItem);
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


function AgregarLineaDetalle(contador, Prioridad, IdArticulo, IdSolicitudRQDetalle, IdUnidadMedida, IdIndicadorImpuesto, IdAlmacen, IdProveedor, IdLineaNegocio, IdCentroCosto, IdProyecto, IdMoneda, ItemTipoCambio, CantidadNecesaria, PrecioInfo, ItemTotal, NumeroFabricacion, NumeroSerie, FechaNecesaria, Referencia, DescripcionItem) {

    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
    });

    $.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
        LineaNegocio = JSON.parse(data);
    });

    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
        CentroCosto = JSON.parse(data);
    });

    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
        Proyecto = JSON.parse(data);
    });

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        Moneda = JSON.parse(data);
    });



    let TipoItem = $("#cboClaseArticulo").val();
    $.post('/Articulo/ObtenerArticuloxCodigoDescripcion', {
        'Codigo': IdArticulo,
        'TipoItem': TipoItem
    }, function (data, status) {
        let articulos = JSON.parse(data);
        console.log("articulos");
        console.log(articulos);
        console.log("articulos");
        DescripcionItem = articulos[0].Descripcion1;
        //console.log(DescripcionItem);
    });





    var fechaSplit = (FechaNecesaria.substring(0, 10)).split("-");
    var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdSolicitudRQDetalle + `" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td style="display:none">
            <input class="form-control" type="text" value="" id="txtCodigoArticulo" name="txtCodigoArticulo[]"/>
            <input class="form-control" type="hidden" value="`+ IdArticulo + `" id="txtIdArticulo" name="txtIdArticulo[]"/>
            </td>
            <td ><input class="form-control" value="`+ DescripcionItem + `" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]" disabled/></td>
            <td>
            <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `" selected>` + UnidadMedida[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" id="cboPrioridadDetalle`+ contador + `" name="cboPrioridadDetalle[]">
                <option value="0">Seleccione</option>
                <option value="1">ALTA</option>
                <option value="2">BAJA</option>
            </select>
            </td>
            <td style="display:none"><input class="form-control" type="date" value="`+ fecha + `" id="txtFechaNecesaria` + contador + `" name="txtFechaNecesaria[]"></td>
            <td style="display:none">
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Moneda.length; i++) {
        if (Moneda[i].IdMoneda == IdMoneda) {
            tr += `  <option value="` + Moneda[i].IdMoneda + `" selected>` + Moneda[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td style="display:none"><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" value="`+ ItemTipoCambio + `" id="txtTipoCambioDetalle` + contador + `" disabled></td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" value="`+ CantidadNecesaria + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="`+ PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled></td>
            <td style="display:none">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        if (IndicadorImpuesto[i].IdIndicadorImpuesto == IdIndicadorImpuesto) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" selected>` + IndicadorImpuesto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" value="`+ ItemTotal + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled></td>
            <td>
            <select class="form-control" style="width:100px" name="cboAlmacen[]">`;
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
            <td style="display:none">
            <select class="form-control" style="width:100px" name="cboProveedor[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proveedor.length; i++) {
        if (Proveedor[i].IdProveedor == IdProveedor) {
            tr += `  <option value="` + Proveedor[i].IdProveedor + `" selected>` + Proveedor[i].RazonSocial + `</option>`;
        } else {
            tr += `  <option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].RazonSocial + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td style="display:none"><input class="form-control" type="text" value="`+ NumeroFabricacion + `" name="txtNumeroFacbricacion[]"></td>
            <td style="display:none"><input class="form-control" type="text" value="`+ NumeroSerie + `" name="txtNumeroSerie[]"></td>
            <td style="display:none">
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < LineaNegocio.length; i++) {
        if (LineaNegocio[i].IdLineaNegocio == IdLineaNegocio) {
            tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `" selected>` + LineaNegocio[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboCentroCostos[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < CentroCosto.length; i++) {
        if (CentroCosto[i].IdCentroCosto == IdCentroCosto) {
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `" selected>` + CentroCosto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proyecto.length; i++) {
        if (Proyecto[i].IdProyecto == IdProyecto) {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `" selected>` + Proyecto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td ><input class="form-control" type="text" value="`+ Referencia + `"  name="txtReferencia[]"></td>
            <td><button class="btn btn-xs btn-danger"  onclick="EliminarDetalle(`+ IdSolicitudRQDetalle + `,this)">-</button></td>
          <tr>`;

    $("#tabla").find('tbody').append(tr);
    $("#cboPrioridadDetalle" + contador).val(Prioridad);

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
    let Almacen = $("#cboAlmacenItem").val();

    if (Almacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    $("#ModalListadoItem").modal();

    $.post("/Articulo/ObtenerArticulos", { 'Almacen': Almacen, 'TipoItem': TipoItem }, function (data, status) {

        if (data == "error") {
            swal("Informacion!", "No se encontro Articulo")
        } else {
            let items = JSON.parse(data);
            console.log(items);
            let tr = '';

            for (var i = 0; i < items.length; i++) {

                tr += '<tr>' +
                    '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
                    '<td>' + items[i].Codigo + '</td>' +
                    '<td>' + items[i].Descripcion1 + '</td>' +
                    '<td>' + items[i].Stock + '</td>' +
                    '<td>' + items[i].DescripcionUnidadMedida + '</td>' +
                    '</tr>';
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



function BuscarListadoProyecto() {

    $("#ModalListadoProyecto").modal();

    $.post("/Proyecto/ObtenerProyectos", function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Proyectos")
        } else {
            let proyectos = JSON.parse(data);
            console.log(proyectos);
            let tr = '';

            for (var i = 0; i < proyectos.length; i++) {

                tr += '<tr>' +
                    '<td><input type="radio" clase="" id="rdSeleccionadoProyecto' + proyectos[i].IdProyecto + '"  name="rdSeleccionadoProyecto"  value = "' + proyectos[i].IdProyecto + '" ></td>' +
                    '<td>' + proyectos[i].Codigo + '</td>' +
                    '<td>' + proyectos[i].Descripcion + '</td>' +
                    '</tr>';
            }

            $("#tbody_listado_proyecto").html(tr);

            tableProyecto = $("#tabla_listado_proyecto").DataTable({
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



function BuscarListadoCentroCosto() {

    $("#ModalListadoCentroCosto").modal();

    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Centro de Costo")
        } else {
            let centroCosto = JSON.parse(data);
            console.log(centroCosto);
            let tr = '';

            for (var i = 0; i < centroCosto.length; i++) {

                tr += '<tr>' +
                    '<td><input type="radio" clase="" id="rdSeleccionadoCentroCosto' + centroCosto[i].IdCentroCosto + '"  name="rdSeleccionadoCentroCosto"  value = "' + centroCosto[i].IdCentroCosto + '" ></td>' +
                    '<td>' + centroCosto[i].Codigo + '</td>' +
                    '<td>' + centroCosto[i].Descripcion + '</td>' +
                    '</tr>';
            }

            $("#tbody_listado_centroCosto").html(tr);

            tableCentroCosto = $("#tabla_listado_centroCosto").DataTable({
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

function BuscarListadoAlmacen() {

    $("#ModalListadoAlmacen").modal();

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {

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

    let Codigo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();
    $.post("/Articulo/ObtenerArticuloxCodigo", { 'Codigo': Codigo, 'TipoItem': TipoItem, 'Almacen': Almacen }, function (data, status) {

        if (data == "error") {
            swal("Info!", "No se encontro Articulo")
            tableItems.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#txtCodigoItem").val(datos[0].Codigo);
            $("#txtIdItem").val(datos[0].IdArticulo);
            $("#txtDescripcionItem").val(datos[0].Descripcion1);
            $("#cboMedidaItem").val(datos[0].IdUnidadMedida);
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