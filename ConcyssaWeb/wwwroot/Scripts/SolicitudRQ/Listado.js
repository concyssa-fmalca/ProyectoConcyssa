let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;


function seleccionarAlmacenItem() {
    $("#cboAlmacenItem").val($("#cboAlmacen").val());
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
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


function CargarBase() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBase", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBase(base, "IdBase", "Seleccione")
    });
}



let TablaItemsDestruida = 0;
window.onload = function () {
    var url = "ObtenerSolicitudesRQ";
    ObtenerConfiguracionDecimales();
    ConsultaServidor(url);


    $.post('/Usuario/ObtenerUsuariosCreadores', function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let datos = JSON.parse(data);
        llenarComboCreadores(datos, "CboIdSolicitante", "Seleccione")
    });



    $("#FechaInicio").datepicker();
    $("#FechaFinal").datepicker();
    $("#FechaInicio").datepicker("option", "dateFormat", 'dd/mm/yy');
    $("#FechaFinal").datepicker("option", "dateFormat", 'dd/mm/yy');


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
                var errorEmpresa = validarEmpresa(datos);
                if (errorEmpresa) {
                    return;
                }

                let data = JSON.parse(datos);
                console.log(data);
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
};

function BuscarPorFechas() {
    table.destroy();
    $("#tbody_Solicitudes").html("");
    ConsultaServidor("ObtenerSolicitudesRQ");
}


function ConsultaServidor(url) {

    let FechaInicio = $("#FechaInicio").val();
    let FechaFinal = $("#FechaFinal").val();
    let Estado = $("#Estado").val();
    let IdSolicitante = $("#CboIdSolicitante").val();

    if (IdSolicitante == null || IdSolicitante == "" || IdSolicitante == "null") {
        IdSolicitante = 0;
    }

    var date = new Date();
    var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    var mes = date.getMonth() + 1;
    var year = date.getFullYear();



    if (FechaInicio == "") {
        //FechaInicio = "01-01-1999";
        FechaInicio = '01/' + (mes < 10 ? '0' : '') + mes + '/' + year;
    }
    if (FechaFinal == "") {
        //FechaFinal = "01-01-2025";
        FechaFinal = ultimoDia.getDate() + '/' + (mes < 10 ? '0' : '') + mes + '/' + year;
    }

    console.log(FechaInicio);
    console.log(FechaFinal);


    $.post(url, { 'FechaInicio': FechaInicio, 'FechaFinal': FechaFinal, 'Estado': Estado, 'IdSolicitante': IdSolicitante }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        //console.log(data);
        if (data == "error") {
            $("#tbody_Solicitudes").html("");
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let solicitudes = JSON.parse(data);
        let total_solicitudes = solicitudes.length;
        console.log("cabecera");
        console.log(solicitudes);

        for (var i = 0; i < solicitudes.length; i++) {



            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + solicitudes[i].Numero + '</td>' +
                '<td>' + solicitudes[i].Solicitante.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].NombreDepartamento.toUpperCase() + '</td>';
            //'<td>' + solicitudes[i].Serie.toUpperCase() + '</td>' +
            //'<td>' + solicitudes[i].Numero + '</td>' +
            //'<td>' + solicitudes[i].TotalAntesDescuento.toFixed(DecimalesImportes) + '</td>' +
            if (solicitudes[i].IdClaseArticulo == 1) {
                tr += '<td>PRODUCTO</td>';
            } else if (solicitudes[i].IdClaseArticulo == 2) {
                tr += '<td>SERVICIO</td>';
            } else {
                tr += '<td>ACTIVO</td>';
            }


            tr += '<td>' + solicitudes[i].IdMoneda + '</td>' +
                '<td>' + solicitudes[i].Total.toFixed(DecimalesImportes) + '</td>';

            if (solicitudes[i].Prioridad == 1) {
                tr += '<td>ALTA</td>';
            } else {
                tr += '<td>BAJA</td>';
            }

            tr += '<td>' + solicitudes[i].DetalleEstado.toUpperCase() + '</td>';

            var fechaSplit = (solicitudes[i].FechaDocumento.substring(0, 10)).split("-");
            //var fecha = fechaSplit[0] + "/" + fechaSplit[1] + "/" + fechaSplit[2];
            var fecha = fechaSplit[2] + "/" + fechaSplit[1] + "/" + fechaSplit[0];
            tr += '<td>' + fecha + '</td>';

            var fechaSplitvalidohasta = (solicitudes[i].FechaValidoHasta.substring(0, 10)).split("-");
            //var fechavalidohasta = fechaSplitvalidohasta[0] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[2];
            var fechavalidohasta = fechaSplitvalidohasta[2] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[0];
            tr += '<td>' + fechavalidohasta + '</td>';

            tr += '<td><a href="/SolicitudRQAutorizacion/GenerarPDF?Id=' + solicitudes[i].IdSolicitudRQ + '" target=”_blank” class="btn btn-primary btn-xs"><img src="/assets/img/fa_pdf.png" height ="15" width="15" /></a></td>';

            if (solicitudes[i].Estado == 1) {
                tr += '<td><button class="btn btn-primary btn-xs" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"><img src="/assets/img/fa_pencil.png" height ="15" width="15" /></button></td>';
            } else {
                tr += '<td><button class="btn btn-primary btn-xs" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"><img src="/assets/img/fa_eyes.png" height ="15" width="15" /></button></td>';
            }



            let IdPerfil = $("#IdPerfil").val();
            if ((IdPerfil == 1 || IdPerfil == 5) && solicitudes[i].Estado == 1) {
                tr += '<td><button class="btn btn-danger btn-xs" onclick="CerrarSolicitud(' + solicitudes[i].IdSolicitudRQ + ')"><img src="/assets/img/fa_times.png" height ="15" width="15" /></button></td>';
            } else {
                tr += '<td><button class="btn btn-xs" ><img src="/assets/img/fa_times.png" height ="15" width="15" disabled/></button></td>';
            }
            //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +

            tr += '</tr>';
        }

        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function llenarComboCreadores(lista, idCombo, primerItem) {
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

function ModalNuevo() {

    $("#lblTituloModal").html("Nueva Solicitud");
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }

    //AgregarLinea();
    CargarSeries();
    CargarSolicitante(1);
    //CargarSucursales();
    //CargarDepartamentos();
    CargarMoneda();

    CargarImpuestos();
    //4 es IGV para CROACIA
    $("#cboImpuesto").val(2).change(); //2 titan   //5 croacia
    $("#cboSerie").val(1).change(); //1  titan     //3 croacia

    $("#cboMoneda").val("SOL").change();
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#btnGrabar").show();
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
}


function OpenModalItem() {

    $('#checkstock').prop('checked', true);
    $("#btnBuscarListadoProyecto").prop('disabled', true); //bloquea boton de modal buscar proyectos

    let ClaseArticulo = $("#cboClaseArticulo").val();
    let Moneda = $("#cboMoneda").val();

    if (ClaseArticulo == 0) {
        swal("Informacion!", "Debe Seleccionar Tipo de Articulo!");
    } else if (Moneda == 0) {
        swal("Informacion!", "Debe Seleccionar Moneda!");
    }
    else {


        if (ClaseArticulo == 2) {
            $("#BtnBuscarListadoAlmacen").prop("disabled", true);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
            $("#txtDescripcionItem").prop("disabled", false);
        } else if (ClaseArticulo == 3) {
            $("#BtnBuscarListadoAlmacen").prop("disabled", true);
            $("#BtnBuscarCodigoProducto").prop("disabled", true);
            $("#txtDescripcionItem").prop("disabled", false);
        } else {
            $("#txtDescripcionItem").prop("disabled", true);
            $("#BtnBuscarListadoAlmacen").prop("disabled", false);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
        }


        $("#cboPrioridadItem").val(2);
        $("#txtTotal").val("");
        $("#cboClaseArticulo").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        CargarProyectos();
        CargarCentroCostos();
        CargarAlmacen();


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

            $("#txtPrecioUnitarioItem").val("0");
            $("#txtStockAlmacenItem").val("0");
            //$("#cboMedidaItem").val("11"); //11 titan
            $("#cboMedidaItem").val("NIU");
        }

        if (ClaseArticulo != 1) {
            //trae almacen activo y de servicio especificados en la configuracion



            let artIdSociedad = $("#artIdSociedad").val();
            let AlmacenActivoConfi = "";
            let AlmacenServicioConfi = "";
            $.post("/Sociedad/ObtenerDatosxID", { 'IdSociedad': Number(artIdSociedad) }, function (data, status) {

                var errorEmpresa = validarEmpresa(data);
                if (errorEmpresa) {
                    return;
                }


                if (data == "error") {
                    swal("Info!", "No se encontro Sociedad")
                } else {
                    let datos = JSON.parse(data);
                    AlmacenServicioConfi = datos[0].AlmacenServicio;
                    AlmacenActivoConfi = datos[0].AlmacenActivo;
                }
            });
            if (ClaseArticulo == 2) {
                $("#cboAlmacenItem").val(AlmacenServicioConfi);
            }
            if (ClaseArticulo == 3) {
                $("#cboAlmacenItem").val(AlmacenActivoConfi);
            }
            //trae almacen activo y de servicio especificados en la configuracion
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

    let ClaseArticulo = $("#cboClaseArticulo").val();

    if (PrioridadItem == 1) {
        $("#cboPrioridad").val(1);
    }
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
    let ValidartProducto = $("#txtCodigoItem").val();
    let ValidarCantidad = $("#txtCantidadItem").val();
    let ValidarPrecio = $("#txtPrecioUnitarioItem").val();

    

    console.log(ValidarCantidad);
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
    if (!ValidartProducto) {
        swal("Informacion!", "Debe Seleccionar Producto!");
        return;
    }

    if (ValidarCantidad<=0) {
        swal("Informacion!", "Cantidad mayor a 0!");
        return;
    }

    if (ValidarPrecio <= 0) {
        swal("Informacion!", "Precio mayor a 0!");
        return;
    }



    //trae el cc especificados en la configuracion
    let artIdSociedad = $("#artIdSociedad").val();
    let CentroCostoProyecto = "";
    $.post("/Sociedad/ObtenerDatosxID", { 'IdSociedad': Number(artIdSociedad) }, function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Sociedad")
        } else {
            let datos = JSON.parse(data);
            CentroCostoProyecto = datos[0].CentroCostoProyecto;
        }
    });
    //trae el cc especificados en la configuracion

    let validarAlmacen = $("#cboCentroCostoItem").val();
    if (validarAlmacen == CentroCostoProyecto) {
        if ($("#cboProyectoItem").val() == 0) {
            swal("Informacion!", "Debe Seleccionar Proyecto")
            return;
        }
    }


    //validaciones



    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacen", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Proveedor = JSON.parse(data);
    });

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    Proyecto = JSON.parse(data);
    //});

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Moneda = JSON.parse(data);
    });

    contador++;
    let tr = '';

    //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
    //    <option value="0">Seleccione</option>
    //</select>
    tr += `<tr id="tr` + contador + `">
            
            <td style="display:none;"><input class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            </td>

            <td><button class="btn btn-xs btn-danger borrar" onclick="borrartr('tr`+ contador +`')" ><img src="/assets/img/fa_trash.png" height ="15" width="15" /></button></td>
            <td><input class="form-control" type="text" id="txtCodigoArticulo`+ contador + `" name="txtCodigoArticulo[]" disabled/></td>
            <td><textarea class="form-control" type="text" id="txtDescripcionArticulo`+ contador + `" name="txtDescripcionArticulo[]" disabled></textarea></td>
            <td>
            <select class="form-control" id="cboUnidadMedida`+ contador + `" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Codigo + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" id="cboPrioridadDetalle`+ contador + `" name="cboPrioridadDetalle[]" onchange="ValidarPrioridadDetalle()">
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
            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" min="0" value="0" id="txtCantidadNecesaria`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>`;

    if (ClaseArticulo != 2 && (Number(PrecioUnitarioItem)) > 0) {
        tr += `<td><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="0" id="txtPrecioInfo` + contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>`;
    } else {
        tr += `<td><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="0" id="txtPrecioInfo` + contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>`;
    }



    tr += `<td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number"  name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
            <td>
            <select class="form-control"  id="cboAlmacen`+ contador + `" name="cboAlmacen[]">`;
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
    //if (LineaNegocio.count()>0) {
    //    for (var i = 0; i < LineaNegocio.length; i++) {
    //        tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
    //    }
    //}
   
    tr += `</select>
            </td>
            <td style="display:none">
            <select class="form-control" id="cboCentroCostos`+ contador + `" name="cboCentroCostos[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].IdCentroCosto + `</option>`;
    //}
    tr += `</select>
            </td>
            <td  style="display:none">
            <select class="form-control" id="cboProyecto`+ contador + `" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    //for (var i = 0; i < Proyecto.length; i++) {
    //    tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].IdProyecto + `</option>`;
    //}
    tr += `</select>
            </td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td input style="display:none;"><input class="form-control" type="text" value="0" disabled></td>
            <td><textarea class="form-control" type="text" value="" id="txtReferencia`+ contador + `" name="txtReferencia[]"></textarea></td>
            <td style="display:none;"><input class="form-control" type="text" value="1" id="txtEstadoDetalle`+ contador + `" name="txtEstadoDetalle[]"></td>
            
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

    let cantidadround = (Number(CantidadItem)).toFixed(DecimalesCantidades);
    let precioround = (Number(PrecioUnitarioItem)).toFixed(DecimalesPrecios);

    $("#txtIdArticulo" + contador).val(IdItem);
    $("#txtCodigoArticulo" + contador).val(CodigoItem);
    $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
    $("#cboUnidadMedida" + contador).val(MedidaItem);
    $("#txtCantidadNecesaria" + contador).val(cantidadround).change();
    $("#txtPrecioInfo" + contador).val(precioround).change();
    $("#cboProyecto" + contador).val(ProyectoItem);
    $("#cboAlmacen" + contador).val(AlmacenItem);
    $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

    $("#cboCentroCostos" + contador).val(CentroCostoItem);
    $("#txtReferencia" + contador).val(ReferenciaItem);


    if (ClaseArticulo == 3) {
        LimpiarModalItem(1);
    } else {
        LimpiarModalItem();
    }

}


//function LimpiarDatosModalItems() {

//}


function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}

function CargarImpuestos() {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let impuestos = JSON.parse(data);
        llenarComboImpuesto(impuestos, "cboImpuesto", "Seleccione")
    });
}

function CargarUnidadMedidaItem() {
    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let unidad = JSON.parse(data);
        llenarComboUnidadMedidaItem(unidad, "cboMedidaItem", "Seleccione")
    });
}

function CargarSolicitante(identificar) {
    $.ajaxSetup({ async: false });
    $.post("/Usuario/ObtenerUsuarios", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
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
//        var errorEmpresa = validarEmpresa(data);
//        if (errorEmpresa) {
//            return;
//        }
//        let sucursales = JSON.parse(data);
//        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
//    });
//}

function CargarDepartamentos() {
    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    });
}

function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
}

function CargarProyectos() {
    $.ajaxSetup({ async: false });
    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let proyecto = JSON.parse(data);
        llenarComboProyectoItem(proyecto, "cboProyectoItem", "Seleccione")
    });
}

function CargarCentroCostos() {
    $.ajaxSetup({ async: false });
    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let proyecto = JSON.parse(data);
        llenarComboCentroCostoItem(proyecto, "cboCentroCostoItem", "Seleccione")
    });
}

function CargarAlmacen() {
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUnidadMedida + "'>" + lista[i].Codigo + "</option>"; }
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProyecto + "'>" + lista[i].IdProyecto + "</option>"; }
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

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCentroCosto + "'>" + lista[i].IdCentroCosto + "</option>"; }
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


    if ($('table#tabla tbody tr').length > 0) {
    } else {
        $("#cboClaseArticulo").prop('disabled', false);
    }


    $('table#tabla tbody tr').each(function () {
        //console.log($(this).find("select[name='cboPrioridadDetalle[]']").val());

        if ($(this).find("select[name='cboPrioridadDetalle[]']").val() == 1) {
            $("#cboPrioridad").val(1);
            return false; //break
        } else {
            $("#cboPrioridad").val(2);
        }
        //if ($(this).find('input.codprod').val() == $("#codigoProd").val()) {
        //    alert('valor repetido');
        //    v_valor = 1;
        //}
    });




});

//function borrartr(contador) {

//    $("#tr" + contador).remove();

//}



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

    //let varMoneda = $("#cboMoneda").val();

    //$.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }

    //    if (data == "error") {
    //        alert('4654');
    //        swal("Error!", "Ocurrio un Error")
    //        return;
    //    } else {

    //        let datos = JSON.parse(data);
    //        if (datos[0].Base) {
    //            $("#txtTipoCambio").prop('disabled', true);
    //        } else {
    //            $("#txtTipoCambio").prop('disabled', false);
    //        }

    //    }
    //});
    //$(".MonedaDeCabecera").val(varMoneda).change();



    //let Moneda = $("#cboMoneda").val();
    //$.post("ObtenerTipoCambio", { 'Moneda': Moneda }, function (data, status) {

    //    if (data != "error") {
    //        let dato = JSON.parse(data);
    //        console.log(dato);
    //        if (dato == 0) {
    //            $("#txtTipoCambio").val(0);
    //        } else {
    //            $("#txtTipoCambio").val(dato[0].Rate);
    //        }
           
    //    } else {
    //        $("#txtTipoCambio").val(0);
    //    }


    //});


    //let varTipoCambio = $("#txtTipoCambio").val();
    //$(".TipoCambioDeCabecera").val(varTipoCambio).change();
}


function ObtenerNumeracion() {

    let varSerie = $("#cboSerie").val();



    //$.post("/Serie/ValidarNumeracionSerieSolicitudRQ", { 'IdSerie': varSerie }, function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }

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

    if ($('table#tabla tbody tr').length > 0) {
    } else {
        swal("Informacion!", "Debe ingresar minimo una linea de detalle")
        return;
    }

    let validatotal = $("#txtTotal").val();
    if (validatotal <= 0) {
        swal("Informacion!", "El total del documento no puede ser 0")
        return;
    }

    let ArrayGeneral = new Array();
    let arrayIdArticulo = new Array();
    let arrayDescripcionArticulo = new Array();
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
    let arrayEstadoDetalle = new Array();

    //let arrayIdAlmacen = new Array();
    let arrayPrioridad = new Array();

    $("input[name='txtIdArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });
    $("textarea[name='txtDescripcionArticulo[]']").each(function (indice, elemento) {
        arrayDescripcionArticulo.push($(elemento).val());
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
    $("textarea[name='txtReferencia[]']").each(function (indice, elemento) {
        arrayReferencia.push($(elemento).val());
    });
    $("input[name='txtIdSolicitudRQDetalle[]']").each(function (indice, elemento) {
        arrayIdSolicitudDetalle.push($(elemento).val());
    });

    $("select[name='cboPrioridadDetalle[]']").each(function (indice, elemento) {
        arrayPrioridad.push($(elemento).val());
    });

    $("input[name='txtEstadoDetalle[]']").each(function (indice, elemento) {
        arrayEstadoDetalle.push($(elemento).val());
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

    //let depa = $("#cboDepartamento").text();
    let varNombreDepartamento;

    $.post('/Departamento/ObtenerDatosxID', {
        'IdDepartamento': varDepartamento
    }, function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        let datos = JSON.parse(data);
        varNombreDepartamento = datos[0].Descripcion;
    });


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
            'NombreDepartamento': varNombreDepartamento,
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
            'Descripcion': arrayDescripcionArticulo,
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
            'EstadoDetalle': arrayEstadoDetalle,
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
            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }


            if (data == 1) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente RQ:' + varNumero,
                    'success'
                )
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                table.destroy();
                CerrarModal();
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
    //$("#txtEstado").val(1);

    $("#file").val('');
}


function ObtenerDatosxID(IdSolicitudRQ) {



    AbrirModal("modal-form");


    $.post('ObtenerDatosxID', {
        'IdSolicitudRQ': IdSolicitudRQ,
    }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let solicitudes = JSON.parse(data);

            if (solicitudes[0].Estado == 1) {
                $("#lblTituloModal").html("Editar Solicitud");
            } else {
                $("#lblTituloModal").html("Visualizacion");
            }


            //console.log("sda");
            //console.log(solicitudes);

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
            $("#txtEstado").val(solicitudes[0].Estado);

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

            //4 es IGV para CROACIA
            $("#cboImpuesto").val(1); //2 titan EXO  //5 CROACIA EXO

            //agrega detalle
            let tr = '';

            let Detalle = solicitudes[0].Detalle;
            console.log("Detalle");
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {

                AgregarLineaDetalle(Detalle[i].Descripcion, solicitudes[0].Numero, solicitudes[0].Estado, i, Detalle[i].Prioridad, Detalle[i].IdArticulo, Detalle[i].IdSolicitudRQDetalle, Detalle[i].IdUnidadMedida, Detalle[i].IdIndicadorImpuesto, Detalle[i].IdAlmacen, Detalle[i].IdProveedor, Detalle[i].IdLineaNegocio, Detalle[i].IdCentroCostos, Detalle[i].IdProyecto, Detalle[i].IdItemMoneda, Detalle[i].ItemTipoCambio, Detalle[i].CantidadNecesaria.toFixed(DecimalesCantidades), Detalle[i].PrecioInfo.toFixed(DecimalesPrecios), Detalle[i].ItemTotal.toFixed(DecimalesImportes), Detalle[i].NumeroFabricacion, Detalle[i].NumeroSerie, Detalle[i].FechaNecesaria, Detalle[i].Referencia, Detalle[i].DescripcionItem, Detalle[i].EstadoDetalle);
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
    $("#txtItemTotal" + contador).val(varTotal.toFixed(DecimalesImportes)).change();


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


function AgregarLineaDetalle(DescripcionServicio, Numero, EstadoCabecera, contador, Prioridad, IdArticulo, IdSolicitudRQDetalle, IdUnidadMedida, IdIndicadorImpuesto, IdAlmacen, IdProveedor, IdLineaNegocio, IdCentroCosto, IdProyecto, IdMoneda, ItemTipoCambio, CantidadNecesaria, PrecioInfo, ItemTotal, NumeroFabricacion, NumeroSerie, FechaNecesaria, Referencia, DescripcionItem, EstadoDetalle) {

    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    //console.log(EstadoCabecera);
    if (EstadoCabecera != 1) {
        //$("#btnGrabar").prop('disabled', true);
        $("#btnGrabar").hide();
    } else {
        $("#btnGrabar").show();
    }


    $.ajaxSetup({ async: false });
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Proveedor = JSON.parse(data);
    });

    //$.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    LineaNegocio = JSON.parse(data);
    //});

    //$.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    CentroCosto = JSON.parse(data);
    //});

    //$.post("/Proyecto/ObtenerProyectos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    Proyecto = JSON.parse(data);
    //});

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Moneda = JSON.parse(data);
    });



    let TipoItem = $("#cboClaseArticulo").val();
    if (TipoItem == 2) {

        DescripcionItem = DescripcionServicio;

    } else if (TipoItem == 3) {

        DescripcionItem = DescripcionServicio;

    } else {
        $.post('/Articulo/ObtenerArticuloxCodigoDescripcion', {
            'Codigo': IdArticulo,
            'TipoItem': TipoItem
        }, function (data, status) {

            var errorEmpresa = validarEmpresa(data);
            if (errorEmpresa) {
                return;
            }

            let articulos = JSON.parse(data);
            console.log("articulos");
            console.log(articulos);
            console.log("articulos");
            DescripcionItem = articulos[0].Descripcion1;
            //console.log(DescripcionItem);
        });
    }






    var fechaSplit = (FechaNecesaria.substring(0, 10)).split("-");
    var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];

    let tr = '';

    tr += `<tr>
            <td style="display:none;"><input  class="form-control" type="text" value="`+ IdSolicitudRQDetalle + `" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td style="display:none;">
            <input class="form-control" type="text" value="" id="txtCodigoArticulo" name="txtCodigoArticulo[]"/>
            </td>

            <td>`;
    if (EstadoCabecera == 1) {
        tr += `<button class="btn btn-xs btn-danger"  onclick="EliminarDetalle(` + IdSolicitudRQDetalle + `,this)"><img src="/assets/img/fa_trash.png" height ="15" width="15" /></button>`;
    }


    tr += `<button class="btn btn-xs btn-warning" onclick="BuscarHistorial('` + Numero + `','` + IdArticulo + `','` + IdAlmacen + `',` + TipoItem + `)">H</button>
            </td>

            <td><input class="form-control" type="text" value="`+ IdArticulo + `" id="txtIdArticulo" name="txtIdArticulo[]" disabled/></td>
            <td><textarea class="form-control" value="`+ DescripcionItem + `" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]" disabled>` + DescripcionItem + `</textarea></td>
            <td>
            <select class="form-control" name="cboUnidadMedida[]" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `" selected>` + UnidadMedida[i].Codigo + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Codigo + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" id="cboPrioridadDetalle`+ contador + `" name="cboPrioridadDetalle[]" onchange="ValidarPrioridadDetalle()">
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
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" min="0" value="`+ CantidadNecesaria + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>`;

    if (TipoItem != 2 && (Number(PrecioInfo)) > 0) {
        tr += `<td><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="` + PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled></td>`;
    } else {
        tr += `<td><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="` + PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" ></td>`;
    }



    tr += `<td style="display:none">
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
            <td><input class="form-control changeTotal" type="number" value="`+ ItemTotal + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled></td>
            <td>
            <select class="form-control"  name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == IdAlmacen) {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `" selected>` + Almacen[i].IdAlmacen + `</option>`;
        } else {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].IdAlmacen + `</option>`;
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
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `" selected>` + CentroCosto[i].IdCentroCosto + `</option>`;
        } else {
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].IdCentroCosto + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proyecto.length; i++) {
        if (Proyecto[i].IdProyecto == IdProyecto) {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `" selected>` + Proyecto[i].IdProyecto + `</option>`;
        } else {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].IdProyecto + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td ><textarea class="form-control" type="text" value="`+ Referencia + `"  name="txtReferencia[]">` + Referencia + `</textarea></td>
            <td style="display:none;"><input class="form-control" type="text" value="`+ EstadoDetalle + `"  name="txtEstadoDetalle[]"></td>
            
          </tr>`;

    $("#tabla").find('tbody').append(tr);
    $("#cboPrioridadDetalle" + contador).val(Prioridad);

}

function EliminarDetalle(IdSolicitudRQDetalle, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleSolicitud", { 'IdSolicitudRQDetalle': IdSolicitudRQDetalle }, function (data, status) {

            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
                CalcularTotales();


                $('table#tabla tbody tr').each(function () {
                    if ($(this).find("select[name='cboPrioridadDetalle[]']").val() == 1) {
                        $("#cboPrioridad").val(1);
                        return false; //break
                    } else {
                        $("#cboPrioridad").val(2);
                    }
                });



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





function BuscarCodigoProducto(stock) {




    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();



    if ($('#checkstock').prop('checked')) {
        stock = 1;
    } else {
        stock = 0;
    }



    if (Almacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    if (TablaItemsDestruida > 0) {  // destruir tabla por cambio de check

        tableItems.destroy();

        $.post("/Articulo/ObtenerArticulosConStockSolicitud",
            { 'Almacen': Almacen, 'Stock': stock, 'TipoItem': TipoItem, 'TipoProducto': $("#IdTipoProducto").val() },
            function (data, status) {

                var errorEmpresa = validarEmpresa(data);
                if (errorEmpresa) {
                    return;
                }


                if (data == "error") {
                    swal("Informacion!", "No se encontro Articulo")

                } else {
                    $("#ModalListadoItem").modal();

                    let items = JSON.parse(data);
                    //console.log(items);
                    let tr = '';

                    for (var i = 0; i < items.length; i++) {

                        tr += '<tr id="item' + items[i].IdArticulo + '" onclick="SeleccionTrItem(' + "'" + items[i].IdArticulo + "'" + ')" ondblclick="SeleccionarItemDoubleClick()">' +
                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                            '<td>' + items[i].Codigo + '</td>' +
                            '<td>' + items[i].Descripcion1 + '</td>';
                        if (TipoItem == 2) {
                            tr += '<td>-</td>';
                            tr += '<td>-</td>';
                        } else {
                            tr += '<td>' + items[i].Stock + '</td>';
                            tr += '<td>' + items[i].NombUnidadMedida + '</td>';
                        }
                        //if (items[i].PathImage != "") {
                        //    tr += '<td><a target="_blank" href="/SolicitudRQ/DownloadImagen?ImageName=' + items[i].PathImage + '">Ver</a></td>';
                        //} else {
                        //    tr += '<td><a>-</a></td>';
                        //}

                        tr += '</tr>';


                    }

                    $("#tbody_listado_items").html(tr);

                    tableItems = $("#tabla_listado_items").DataTable({
                        "iDisplayLength": 100,
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


    } else {

        if (tableItems) {
            console.log('exirte');
            $("#ModalListadoItem").modal();
        } else {
            $.post("/Articulo/ObtenerArticulosConStockSolicitud",
                { 'Almacen': Almacen, 'Stock': stock, 'TipoItem': TipoItem, 'TipoProducto': $("#IdTipoProducto").val() },
                function (data, status) {

                    var errorEmpresa = validarEmpresa(data);
                    if (errorEmpresa) {
                        return;
                    }


                    if (data == "error") {
                        swal("Informacion!", "No se encontro Articulo")

                    } else {
                        $("#ModalListadoItem").modal();

                        let items = JSON.parse(data);
                        //console.log(items);
                        let tr = '';

                        for (var i = 0; i < items.length; i++) {

                            tr += '<tr id="item' + items[i].IdArticulo + '" onclick="SeleccionTrItem(' + "'" + items[i].IdArticulo + "'" + ')" ondblclick="SeleccionarItemDoubleClick()">' +
                                '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
                                '<td>' + items[i].Codigo + '</td>' +
                                '<td>' + items[i].Descripcion1 + '</td>';
                            if (TipoItem == 2) {
                                tr += '<td>-</td>';
                                tr += '<td>-</td>';
                            } else {
                                tr += '<td>' + items[i].Stock + '</td>';
                                tr += '<td>' + items[i].NombUnidadMedida + '</td>';
                            }
                            //if (items[i].PathImage != "") {
                            //    tr += '<td><a target="_blank" href="/SolicitudRQ/DownloadImagen?ImageName=' + items[i].PathImage + '">Ver</a></td>';
                            //} else {
                            //    tr += '<td><a>-</a></td>';
                            //}

                            tr += '</tr>';


                        }

                        $("#tbody_listado_items").html(tr);

                        tableItems = $("#tabla_listado_items").DataTable({
                            "iDisplayLength": 100,
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









    //$.ajax({
    //    url: "/Articulo/ObtenerArticulosConStock",
    //    type: "POST",
    //    async: true,
    //    data: { 'Almacen': Almacen, 'Stock': stock, 'TipoItem': TipoItem },
    //    beforeSend: function () {

    //        Swal.fire({
    //            title: "Cargando...",
    //            text: "Por favor espere",
    //            showConfirmButton: false,
    //            allowOutsideClick: false
    //        });

    //    },
    //    success: function (data) {

    //        var errorEmpresa = validarEmpresa(data);
    //        if (errorEmpresa) {
    //            return;
    //        }


    //        if (data == "error") {
    //            swal("Informacion!", "No se encontro Articulo")

    //        } else {

    //            Sweetalert2.close();
    //            $("#ModalListadoItem").modal();

    //            let items = JSON.parse(data);
    //            //console.log(items);
    //            let tr = '';

    //            for (var i = 0; i < items.length; i++) {

    //                tr += '<tr id="item' + items[i].Codigo + '" onclick="SeleccionTrItem(' + "'" + items[i].Codigo + "'" + ')" ondblclick="SeleccionarItemDoubleClick()">' +
    //                    '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].Codigo + '"  name="rdSeleccionado"  value = "' + items[i].Codigo + '" ></td>' +
    //                    '<td>' + items[i].Codigo + '</td>' +
    //                    '<td>' + items[i].Descripcion1 + '</td>';
    //                if (TipoItem == 2) {
    //                    tr += '<td>-</td>';
    //                    tr += '<td>-</td>';
    //                } else {
    //                    tr += '<td>' + items[i].Stock + '</td>';
    //                    tr += '<td>' + items[i].CodigoUnidadMedida + '</td>';
    //                }
    //                if (items[i].PathImage != "") {
    //                    tr += '<td><a target="_blank" href="/SolicitudRQ/DownloadImagen?ImageName=' + items[i].PathImage + '">Ver</a></td>';
    //                } else {
    //                    tr += '<td><a>-</a></td>';
    //                }

    //                tr += '</tr>';


    //            }

    //            $("#tbody_listado_items").html(tr);

    //            tableItems = $("#tabla_listado_items").DataTable({
    //                "iDisplayLength": 100,
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
    //                },
    //            });

    //        }

    //    }
    //}).fail(function () {
    //    Swal.fire(
    //        'Error!',
    //        'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
    //        'error'
    //    )
    //});











}



var ultimaFila = null;
var colorOriginal;

function SeleccionarItemDoubleClick() {
    SeleccionarItemListado();
    $("#ModalListadoItem").modal('hide');
    //$("#checkstock").prop("checked", true);
}

function SeleccionTrItem(ItemCodigo) {

    $("#rdSeleccionado" + ItemCodigo).prop("checked", true);

    $('#tabla_listado_items').on('click', 'tr', function () {
        if (ultimaFila != null) {
            ultimaFila.css('background-color', colorOriginal)
        }
        colorOriginal = $("#item" + ItemCodigo).css('background-color');
        $("#item" + ItemCodigo).css('background-color', '#effafc');
        ultimaFila = $("#item" + ItemCodigo);
    });

}


function BuscarListadoProyecto() {

    $("#ModalListadoProyecto").modal();

    $.post("/Proyecto/ObtenerProyectos", function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Proyectos")
        } else {
            let proyectos = JSON.parse(data);
            console.log(proyectos);
            let tr = '';

            for (var i = 0; i < proyectos.length; i++) {

                tr += '<tr id="proyecto' + proyectos[i].IdProyecto + '" onclick="SeleccionTrProyecto(' + "'" + proyectos[i].IdProyecto + "'" + ')" ondblclick="SeleccionarProyectoDoubleClick()">' +
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


var ultimaFilaProyecto = null;
var colorOriginalProyecto;

function SeleccionarProyectoDoubleClick() {
    SeleccionarProyectoListado();
    $("#ModalListadoProyecto").modal('hide');
}

function SeleccionTrProyecto(IdProyecto) {

    $("#rdSeleccionadoProyecto" + IdProyecto).prop("checked", true);

    $('#tabla_listado_proyecto').on('click', 'tr', function () {
        if (ultimaFilaProyecto != null) {
            ultimaFilaProyecto.css('background-color', colorOriginalProyecto)
        }
        colorOriginalProyecto = $("#proyecto" + IdProyecto).css('background-color');
        $("#proyecto" + IdProyecto).css('background-color', '#effafc');
        ultimaFilaProyecto = $("#proyecto" + IdProyecto);
    });

}




function BuscarListadoCentroCosto() {

    $("#ModalListadoCentroCosto").modal();

    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Centro de Costo")
        } else {
            let centroCosto = JSON.parse(data);
            console.log(centroCosto);
            let tr = '';

            for (var i = 0; i < centroCosto.length; i++) {

                tr += '<tr id="centrocosto' + centroCosto[i].IdCentroCosto + '" onclick="SeleccionTrCentroCosto(' + "'" + centroCosto[i].IdCentroCosto + "'" + ')" ondblclick="SeleccionarCentroCostoDoubleClick()">' +
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


var ultimaFilaCentroCosto = null;
var colorOriginalCentroCosto;

function SeleccionarCentroCostoDoubleClick() {

    SeleccionarCentroCostoListado();
    $("#ModalListadoCentroCosto").modal('hide');
    $("#cboProyectoItem").val(0);
}

function SeleccionTrCentroCosto(IdCentroCosto) {

    $("#rdSeleccionadoCentroCosto" + IdCentroCosto).prop("checked", true);

    $('#tabla_listado_centroCosto').on('click', 'tr', function () {
        if (ultimaFilaCentroCosto != null) {
            ultimaFilaCentroCosto.css('background-color', colorOriginalCentroCosto)
        }
        colorOriginalCentroCosto = $("#centrocosto" + IdCentroCosto).css('background-color');
        $("#centrocosto" + IdCentroCosto).css('background-color', '#effafc');
        ultimaFilaCentroCosto = $("#centrocosto" + IdCentroCosto);
    });

}




function BuscarListadoAlmacen() {


    if (tableItems) {
        TablaItemsDestruida = 1;
    }


    $("#ModalListadoAlmacen").modal();

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Almacen")
        } else {
            let almacen = JSON.parse(data);
            console.log(almacen);
            let tr = '';

            for (var i = 0; i < almacen.length; i++) {

                tr += '<tr id="almacen' + almacen[i].IdAlmacen + '" onclick="SeleccionTrAlmacen(' + "'" + almacen[i].IdAlmacen + "'" + ')" ondblclick="SeleccionarAlmacenDoubleClick()">' +
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


var ultimaFilaAlmacen = null;
var colorOriginalAlmacen;

function SeleccionarAlmacenDoubleClick() {
    SeleccionarAlmacenListado();
    $("#ModalListadoAlmacen").modal('hide');
}

function SeleccionTrAlmacen(IdAlmacen) {

    $("#rdSeleccionadoAlmacen" + IdAlmacen).prop("checked", true);

    $('#tabla_listado_almacen').on('click', 'tr', function () {
        if (ultimaFilaAlmacen != null) {
            ultimaFilaAlmacen.css('background-color', colorOriginalAlmacen)
        }
        colorOriginalAlmacen = $("#almacen" + IdAlmacen).css('background-color');
        $("#almacen" + IdAlmacen).css('background-color', '#effafc');
        ultimaFilaAlmacen = $("#almacen" + IdAlmacen);
    });

}


function SeleccionarItemListado() {

    let IdArticulo = $('input:radio[name=rdSeleccionado]:checked').val();
    let TipoItem = $("#cboClaseArticulo").val();
    let Almacen = $("#cboAlmacenItem").val();


    $.post("/Articulo/ObtenerArticuloxIdArticuloRequerimiento", { 'IdArticulo': IdArticulo, 'TipoItem': TipoItem, 'IdAlmacen': Almacen }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Articulo")
            tableItems.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#txtCodigoItem").val(datos.Codigo);
            $("#txtIdItem").val(datos.IdArticulo);
            $("#txtDescripcionItem").val(datos.Descripcion1);
            $("#cboMedidaItem").val(datos.IdUnidadMedida);
            $("#txtPrecioUnitarioItem").val(datos.UltimoPrecioCompra);
            $("#txtStockAlmacenItem").val(datos.Stock);

            //tableItems.destroy();
        }
    });



}

function CerrarModalListadoItems() {
    //tableItems.destroy();
    //$('#checkstock').prop('checked', true);
}


function SeleccionarProyectoListado() {

    let IdProyecto = $('input:radio[name=rdSeleccionadoProyecto]:checked').val();

    $.post("/Proyecto/ObtenerDatosxID", { 'IdProyecto': IdProyecto }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

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
    let artIdSociedad = $("#artIdSociedad").val();
    console.log(artIdSociedad);
    //consulta configuracion de sociedad
    let CentroCostoProyecto = "";
    $.post("/Sociedad/ObtenerDatosxID", { 'IdSociedad': Number(artIdSociedad) }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Sociedad")
        } else {
            let datos = JSON.parse(data);
            CentroCostoProyecto = datos[0].CentroCostoProyecto;
            console.log("dat");
            console.log(datos);
            console.log("dat");
        }
    });
    console.log("CentroCostoProyecto");
    console.log(CentroCostoProyecto);
    console.log("CentroCostoProyecto");
    //consulta configuracion de sociedad

    $.post("/CentroCosto/ObtenerDatosxID", { 'IdCentroCosto': IdCentroCosto }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        if (data == "error") {
            swal("Info!", "No se encontro Centro Costo")
            tableCentroCosto.destroy();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#cboCentroCostoItem").val(datos[0].IdCentroCosto);
            tableCentroCosto.destroy();

            if (CentroCostoProyecto == datos[0].IdCentroCosto) {
                $("#btnBuscarListadoProyecto").prop('disabled', false);
            } else {
                $("#btnBuscarListadoProyecto").prop('disabled', true);
            }

        }
    });



}


function CerrarModalListadoCentroCosto() {
    tableCentroCosto.destroy();
}


function SeleccionarAlmacenListado() {

    let IdAlmacen = $('input:radio[name=rdSeleccionadoAlmacen]:checked').val();

    $.post("/Almacen/ObtenerDatosxID", { 'IdAlmacen': IdAlmacen }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


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

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

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


function CerrarSolicitud(IdSolicitudRQ) {


    alertify.confirm('Confirmar', 'Desea Cerrar Esta Solicitud', function () {


        $.ajax({
            url: "CerrarSolicitud",
            type: "POST",
            async: true,
            data: {
                'IdSolictudRQ': IdSolicitudRQ
            },
            beforeSend: function () {

                Swal.fire({
                    title: "Cargando...",
                    text: "Por favor espere",
                    showConfirmButton: false,
                    allowOutsideClick: false
                });

            },
            success: function (resultado) {

                var errorEmpresa = validarEmpresaUpdateInsert(resultado);
                if (errorEmpresa) {
                    return;
                }


                if (resultado == 1) {
                    Swal.fire(
                        'Correcto',
                        'Proceso Realizado Correctamente',
                        'success'
                    )
                    //swal("Exito!", "Proceso Realizado Correctamente", "success")
                    table.destroy();
                    CerrarModal();
                    ConsultaServidor("ObtenerSolicitudesRQ");
                } else {
                    Swal.fire(
                        'Error!',
                        'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
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



    }, function () {
    });

}


function LimpiarModalItem(EsActivo = 0) {

    if (EsActivo == 1) {

    } else {
        $("#txtCodigoItem").val("");
        $("#txtStockAlmacenItem").val("");
        $("#txtPrecioUnitarioItem").val("");
        $("#cboMedidaItem").val(0);
    }



    $("#txtDescripcionItem").val("");
    $("#txtCantidadItem").val("");
    $("#cboProyectoItem").val(0);
    //$("#cboCentroCostoItem").val(0);
    //$("#cboAlmacenItem").val(0);
    $("#txtReferenciaItem").val("");


    if ($('table#tabla tbody tr').length > 0) {
    } else {
        $("#cboClaseArticulo").prop('disabled', false); //hablita combo en caso este vacio los tr
    }

}


function ValidarPrioridadDetalle() {
    console.log("hola");

    $('table#tabla tbody tr').each(function () {
        console.log($(this).find("select[name='cboPrioridadDetalle[]']").val());
        if ($(this).find("select[name='cboPrioridadDetalle[]']").val() == 1) {
            $("#cboPrioridad").val(1);
            return false; //break
        } else {
            $("#cboPrioridad").val(2);
        }
    });

}





function BuscarHistorial(Numero, IdArticulo, IdAlmacen, IdClaseArticulo) {




    $.ajax({
        url: "/EnviarSAP/ObtenerHistorialSap",
        type: "POST",
        async: true,
        data: {
            'Numero': Numero,
            'IdArticulo': IdArticulo,
            'IdAlmacen': IdAlmacen,
            'IdClaseArticulo': IdClaseArticulo
        },
        beforeSend: function () {

            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });

        },
        success: function (resultado) {

            var errorEmpresa = validarEmpresa(resultado);
            if (errorEmpresa) {
                return;
            }
            //let rpta = JSON.parse(resultado);
            //console.log(rpta);

            if (resultado == "error") {
                swal("Informacion!", "No se Encontraron datos")
                Sweetalert2.close();
            } else {
                $("#ModalHistorial").modal();
                Sweetalert2.close();
                let rpta = JSON.parse(resultado);
                console.log(rpta);
                for (var i = 0; i < rpta.length; i++) {
                    AgregarLineaHistorial(rpta[i].Documento, rpta[i].DocNum, rpta[i].DocStatus, rpta[i].CANCELED, rpta[i].ItemCode, rpta[i].Dscription, rpta[i].Quantity, rpta[i].OpenQty, rpta[i].LineStatus);
                }
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



function AgregarLineaHistorial(Documento, DocNum, DocStatus, CANCELED, ItemCode, Dscription, Quantity, OpenQty, LineStatus) {

    let Estado = "";
    if (LineStatus == 'C') {
        Estado = "Cerrado"
    } else {
        Estado = "Abierto"
    }

    if (CANCELED == 'Y') {
        Estado = "Cancelado";
    }

    let tr = '';
    tr += `<tr>
            <td>
              `+ Documento + `
            </td>
            <td>
               `+ DocNum + `
            </td>
            <td>
               `+ Estado + `
            </td>
            <td>
               `+ ItemCode + `
            </td>
            <td>
               `+ Dscription + `
            </td>
            <td>
               `+ Quantity + `
            </td>
            <td>
               `+ OpenQty + `
            </td>
            </tr>`;

    $("#tabla_historial").find('tbody').append(tr);

}

function CerrarModalHistorial() {
    $("#tabla_historial").find('tbody').empty();
}




function validarEmpresa(rpta) {
    if (rpta == "SinBD") {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
}

function validarEmpresaUpdateInsert(rpta) {
    if (rpta == -999) {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
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

function borrartr(id) {
    $("#" + id).remove();
    CalcularTotales();
}