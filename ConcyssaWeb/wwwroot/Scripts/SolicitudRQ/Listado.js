let table = '';
let tableItems = '';
let tableProyecto = '';
let tableCentroCosto = '';
let tableAlmacen = '';
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;
let tableStockObras;
let limitador = 0;
let valorfor = 1

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
            llenarComboAlmacen(almacen, "cboAlmacen","seleccione")
            llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
        } else {
            $("#cboAlmacen").html('<option value="0">SELECCIONE</option>')
            $("#cboAlmacenItem").html('<option value="0">SELECCIONE</option>')
        }
    });
    $("#cboAlmacen").prop("selectedIndex", 1)
    $("#cboAlmacenItem").prop("selectedIndex", 1)
}

function ObtenerObraxIdBase() {
    let IdBase = $("#IdBase").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "IdObra","seleccione")
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



let TablaItemsDestruida = 0;
window.onload = function () {
    comprobarSesion()
    CargarBaseFiltro()
    ObtenerConfiguracionDecimales();
    


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
    ConsultaServidor()
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

}
function comprobarSesion() {
    if ($("#IdPerfilSesion").val() == 1018) {

    } 
}
function BuscarPorFechas() {
    if (table) {
        table.destroy();
    }
    $("#tbody_Solicitudes").html("");
    ConsultaServidor();
}


function ConsultaServidor() {

    let FechaInicio = $("#FechaInicio").val();
    let FechaFinal = $("#FechaFinal").val();
    let Estado = $("#Estado").val();
    let IdSolicitante = $("#CboIdSolicitante").val();
    let varIdBaseFiltro = $("#cboObraFiltro").val()
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


    $.post("ObtenerSolicitudesRQ", {'IdBase':varIdBaseFiltro, 'FechaInicio': FechaInicio, 'FechaFinal': FechaFinal, 'Estado': Estado, 'IdSolicitante': IdSolicitante }, function (data, status) {

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
        $("#tbody_Solicitudes").html("");
        for (var i = 0; i < solicitudes.length; i++) {



            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                //'<td>' + solicitudes[i].Serie + '-' + solicitudes[i].Numero + '</td>' +
                '<td><a style="color:blue;text-decoration:underline;cursor:pointer" onclick = "ImprimitSolicitudRQ(' + solicitudes[i].IdSolicitudRQ + ')" > ' + solicitudes[i].Serie + '-' + solicitudes[i].Numero +'</a ></td>' +

                '<td>' + solicitudes[i].Solicitante.toUpperCase() + '</td>' +
                '<td>' + solicitudes[i].NombObra.toUpperCase() + '</td>';

            //'<td>' + solicitudes[i].NombreDepartamento.toUpperCase() + '</td>';
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


            //tr += '<td>' + solicitudes[i].NombMoneda + '</td>';
            //'<td>' + formatNumberDecimales( solicitudes[i].TotalCantidad ,2)+ '</td>';

            tr += '<td>' + solicitudes[i].Comentarios +'</td>'

            //tr += '<td>' + solicitudes[i].DetalleEstado.toUpperCase() + '</td>';
            tr += '<td> <button class="btn btn-sm-red estados fa fa-th-list fa-lg " onclick="modalHistorialEstado(' + solicitudes[i].IdSolicitudRQ + ')"></button> </td>';


            var fechaSplit = (solicitudes[i].FechaDocumento.substring(0, 10)).split("-");
            //var fecha = fechaSplit[0] + "/" + fechaSplit[1] + "/" + fechaSplit[2];
            var fecha = fechaSplit[2] + "/" + fechaSplit[1] + "/" + fechaSplit[0];
            tr += '<td>' + fecha + '</td>';

            //var fechaSplitvalidohasta = (solicitudes[i].FechaValidoHasta.substring(0, 10)).split("-");
            ////var fechavalidohasta = fechaSplitvalidohasta[0] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[2];
            //var fechavalidohasta = fechaSplitvalidohasta[2] + "/" + fechaSplitvalidohasta[1] + "/" + fechaSplitvalidohasta[0];
            //tr += '<td>' + fechavalidohasta + '</td>';

            /* tr += '<td><a href="/SolicitudRQAutorizacion/GenerarPDF?Id=' + solicitudes[i].IdSolicitudRQ + '" target=”_blank” class="btn btn-danger btn-xs reporte fa fa-file-pdf-o fa-lg "></a></td>';*/
            if (solicitudes[i].Usuario == solicitudes[i].IdSolicitante) {
                if (solicitudes[i].Estado == 1) {
                    tr += '<td><button class="btn btn-danger btn-xs editar fa fa-pencil" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"></button></td>';
                } else {
                    tr += '<td><button class="btn btn-danger btn-xs  mostrar fa fa-eye fa-lg" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"></button></td>';
                }
            } else {
                tr += '<td><button class="btn btn-danger btn-xs  mostrar fa fa-eye fa-lg" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"></button></td>';
            }
            if (solicitudes[i].Usuario == solicitudes[i].IdSolicitante) {
                if (solicitudes[i].EstadoSolicitud == 1) {
                    tr += '<td><button class="btn btn-danger btn-xs borrar fa fa-trash fa-lg" onclick="CerrarSolicitudEstado(' + solicitudes[i].IdSolicitudRQ + ')"></button></td>';
                } else {
                    tr += '<td> <span style="font-color:red">CANCELADO</span> </td>';
                }
            } else {
                if (solicitudes[i].EstadoSolicitud == 1) {
                    tr += '<td>-</td>';
                } else {
                    tr += '<td> <span style="font-color:red">CANCELADO</span> </td>';
                } }

            //let IdPerfil = $("#IdPerfil").val();
            //if ((IdPerfil == 1 || IdPerfil == 5) && solicitudes[i].Estado == 1) {
            //    tr += '<td><button class="btn btn-danger btn-xs" onclick="CerrarSolicitud(' + solicitudes[i].IdSolicitudRQ + ')"><img src="/assets/img/fa_times.png" height ="15" width="15" /></button></td>';
            //} else {
            //    tr += '<td><button class="btn btn-xs" ><img src="/assets/img/fa_times.png" height ="15" width="15" disabled/></button></td>';
            //}
            //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +

            tr += '</tr>';

        }
       
        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);
        table = $("#table_id").DataTable(lenguaje);
      
        
    });

}


function CerrarSolicitudEstado(IdSolicitud) {
    alertify.confirm('Confirmar', '¿Desea cancelar esta solicitud?', function () {

        $.post("CerrarSolicitudEstado", { 'IdSolicitud': IdSolicitud }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                if (data == "-1") {
                    swal("Error!", "No se puede cancelar la solicitud , debido a que tiene aprobaciones");
                } else {
                    swal("Exito!", "Solicitud Cancelada", "success")
                    table.destroy();
                    ConsultaServidor();
                }

            }

        });

    }, function () { });

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
    $("#OpenModalItem").show();
    $("#tbody_stockOtrosAlmacenes").empty()
    $("#btnGrabar").show();
    $("#lblTituloModal").html("Nueva Solicitud");
    $("#btnGrabar").text("CREAR")
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


    $("#cboMoneda").val("SOL").change();
    $("#cboPrioridad").val(2);
    $("#cboClaseArticulo").prop("disabled", false);
    $("#btnGrabar").show();
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
    $("#cboMoneda").val(1);
    $("#cboImpuesto").val(1);


    $(".HabilitarVisualizacion").hide();

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


        if (ClaseArticulo == 2) { //servicio
            $("#BtnBuscarListadoAlmacen").prop("disabled", false);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
            $("#txtDescripcionItem").prop("disabled", false);

            $("#IdTipoProducto").hide();
            $("#IdTipoProducto").val(0);

            $("#txtStockAlmacenItem").hide();
            $("#lblStockItem").hide();
            $("#divTipoServicio").show();
            $("#SNinguno").prop('checked',true)
            //$("#divStockObraPedido").hide();
        } else if (ClaseArticulo == 3) { //activo
            $("#BtnBuscarListadoAlmacen").prop("disabled", false);
            $("#BtnBuscarCodigoProducto").prop("disabled", false);
            $("#txtDescripcionItem").prop("disabled", true);

            $("#IdTipoProducto").show();
            //$("#IdTipoProducto").val(0);
            $("#IdTipoProducto").hide();
            $("#IdTipoProducto").val(0);
            $("#divTipoServicio").hide();
            $("#SNinguno").prop('checked', true)
            $("#SNinguno").prop('checked', false)
            $("#txtStockAlmacenItem").show();
            $("#lblStockItem").show();
        } else {//Producto
            if ($("#IdTipoProducto").val() == 0) {
                swal("Informacion!", "Debe Seleccionar Un Tipo de Producto!");
                return;
            }
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


        $("#cboPrioridadItem").val(2);
        $("#txtTotal").val("");
        $("#cboClaseArticulo").prop("disabled", true);
        $("#ModalItem").modal();
        CargarUnidadMedidaItem();
        //CargarProyectos();
        //CargarCentroCostos();



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
            //$("#txtCodigoItem").val("ACF00000000000000000");
            $("#txtIdItem").val("ACF00000000000000000");

            $("#txtPrecioUnitarioItem").val(1);
            $("#txtStockAlmacenItem").val("0");
            //$("#cboMedidaItem").val("11"); //11 titan
            $("#cboMedidaItem").val("NIU");
        }

        //if (ClaseArticulo != 1) {
        //    //trae almacen activo y de servicio especificados en la configuracion



        //    let artIdSociedad = $("#artIdSociedad").val();
        //    let AlmacenActivoConfi = "";
        //    let AlmacenServicioConfi = "";
        //    $.post("/Sociedad/ObtenerDatosxID", { 'IdSociedad': Number(artIdSociedad) }, function (data, status) {

        //        var errorEmpresa = validarEmpresa(data);
        //        if (errorEmpresa) {
        //            return;
        //        }


        //        if (data == "error") {
        //            swal("Info!", "No se encontro Sociedad")
        //        } else {
        //            let datos = JSON.parse(data);
        //            AlmacenServicioConfi = datos[0].AlmacenServicio;
        //            AlmacenActivoConfi = datos[0].AlmacenActivo;
        //        }
        //    });
        //    if (ClaseArticulo == 2) {
        //        $("#cboAlmacenItem").val(AlmacenServicioConfi);
        //    }
        //    if (ClaseArticulo == 3) {
        //        $("#cboAlmacenItem").val(AlmacenActivoConfi);
        //    }
        //    //trae almacen activo y de servicio especificados en la configuracion
        //}







    }


}



function CambiarClaseArticulo() {

    let ClaseArticulo = $("#cboClaseArticulo").val();

    if (ClaseArticulo == "2") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
    } else if (ClaseArticulo == "3") {
        $("#IdTipoProducto").hide();
        $("#IdTipoProducto").val(0);
    }
    else {
        $("#IdTipoProducto").show();
        $("#IdTipoProducto").val(0);
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
            <td><button type="button" class="btn btn-xs btn-danger borrar  fa fa-trash " onclick="EliminarAnexoEnMemoria(`+ contadorAnexo +`)"></button></td>
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
    let TipoServicio 

    if ($("#SNinguno").is(":checked")) {
        TipoServicio = 'Ninguno'
    } else if ($("#SPreventivo").is(":checked")) {
        TipoServicio = 'Preventivo'
        ReferenciaItem = ReferenciaItem+" (Servicio Preventivo)"
    } else if ($("#SCorrectivo").is(":checked")) {
        TipoServicio = 'Correctivo'
        ReferenciaItem = ReferenciaItem+ " (Servicio Correctivo)"
    } else {
        TipoServicio = 'No Aplica'
    }



    let arrayIdArticulo = new Array();
    $("input[name='txtCodigoArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });

    for (var i = 0; i < arrayIdArticulo.length; i++) {
        if (arrayIdArticulo[i] == CodigoItem) {
            console.log(arrayIdArticulo[i]);
            console.log(CodigoItem);
            swal("Informacion!", "Ya existe el producto en la solicitud!");
            return;
        } else {
            console.log(arrayIdArticulo[i]);
            console.log(CodigoItem);
        }
    }




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

    if (ValidarCantidad <= 0) {
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

    let IdObraR = $("#IdObra").val();

    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObraR }, function (data, status) {
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
            
            <td style="display:none;"><input class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td input style="display:none;">
            <input  class="form-control" type="text" id="txtIdArticulo`+ contador + `" name="txtIdArticulo[]" />
            </td>

            <td><button class="btn btn-xs btn-danger borrar  fa fa-trash" onclick="borrartr('tr`+ contador + `');restarLimitador()" ></button></td>
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

            <td><input class="form-control"  type="number" name="txtCantidadNecesaria[]" min="0" value="0" id="txtCantidadNecesaria`+ contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input disabled class="form-control" type="number" name="txtCantidadSolicitada[]" min="0" value="0" id="txtCantidadSolicitada`+ contador + `"></td>`;



        if (ClaseArticulo != 2 && (Number(PrecioUnitarioItem)) > 0) {
            tr += `<td  style="display:none"><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="0" id="txtPrecioInfo` + contador + `" onchange="CalcularTotalDetalle(` + contador + `)" disabled></td>`;
        } else {
            tr += `<td  style="display:none"><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="0" id="txtPrecioInfo` + contador + `" onchange="CalcularTotalDetalle(` + contador + `)"></td>`;
        }



        tr += `<td input style="display:none;">
            <select class="form-control ImpuestoCabecera" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
        tr += `  <option impuesto="0" value="0">Seleccione</option>`;
        for (var i = 0; i < IndicadorImpuesto.length; i++) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
        tr += `</select>
            </td>
            <td style="display:none"><input class="form-control changeTotal" type="number"  name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()" disabled></td>
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
            <td style="display:none" ><input style="width:50px" class="form-control" type="text" value="" id="txtTipoServicio`+ contador + `" name="txtTipoServicio[]"></input></td>
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

        let cantidadround = CantidadItem;
        let precioround = (Number(PrecioUnitarioItem)).toFixed(DecimalesPrecios);

        $("#txtIdArticulo" + contador).val(IdItem);
        $("#txtCodigoArticulo" + contador).val(CodigoItem);
        $("#txtDescripcionArticulo" + contador).val(DescripcionItem);
        $("#cboUnidadMedida" + contador).val(MedidaItem);

        $("#txtCantidadNecesaria" + contador).val(cantidadround).change();
        $("#txtCantidadSolicitada" + contador).val(0);

        $("#txtPrecioInfo" + contador).val(precioround).change();
        $("#cboProyecto" + contador).val(ProyectoItem);
        $("#cboAlmacen" + contador).val(AlmacenItem);
        $("#cboPrioridadDetalle" + contador).val(PrioridadItem);

        $("#cboCentroCostos" + contador).val(CentroCostoItem);
        $("#txtReferencia" + contador).val(ReferenciaItem);
        console.log(TipoServicio)
        $("#txtTipoServicio" + contador).val(TipoServicio);


        if (ClaseArticulo == 3) {
            LimpiarModalItem(1);
        } else {
            LimpiarModalItem();
        }
    }

    ValidarOCAbiertas(IdItem)
}

function restarLimitador() {
    limitador = limitador - 1
}

//function LimpiarDatosModalItems() {

//}


function CargarSeries() {
    $.ajaxSetup({ async: false });
    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
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
    //$.ajaxSetup({ async: false });
    //$.post("/Departamento/ObtenerDepartamentos", function (data, status) {
    //    var errorEmpresa = validarEmpresa(data);
    //    if (errorEmpresa) {
    //        return;
    //    }
    //    let departamentos = JSON.parse(data);
    //    llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    //});
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
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 7) {
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

    //let validatotal = $("#txtTotal").val();
    //if (validatotal <= 0) {
    //    swal("Informacion!", "El total del documento no puede ser 0")
    //    return;
    //}

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
    let arrayTipoServicio = new Array();
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

    $("input[name='txtTipoServicio[]']").each(function (indice, elemento) {
        arrayTipoServicio.push($(elemento).val());
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


    //console.log(AnexoDetalle);
    //return;


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
            'CantidadSolicitada': arrayCantidadNecesaria,
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
            'AnexoDetalle': AnexoDetalle,
            'TipoServicio' : arrayTipoServicio,
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


            if (data == '1') {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente RQ:' + varNumero,
                    'success'
                )
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                table.destroy();
                CerrarModal();
                ConsultaServidor();

            } else if (data == -99) {
                Swal.fire(
                    'Error!',
                    'No Cuenta Con Autorización para Crear este Documento!',
                    'error'
                )
            } else if (data == -98) {
                Swal.fire(
                    'Error!',
                    'El Usuario se encuentra como autor en mas de un modelo de Aut., Contacte a Soporte!',
                    'error'
                )
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
    let date = new Date();
    let mes = date.getMonth() + 1
    let fechaReset =  date.getFullYear()+
   
        "-" + (mes > 9 ? mes : "0" + mes) +
        "-" +   ((date.getDate()) > 9 ? date.getDate() : "0" + date.getDate()) 

    Date.prototype.addDays = function (days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }

    var dateHasta = new Date();
    dateHasta = dateHasta.addDays(15)
    var fechaHastaReset = dateHasta.getFullYear() +
        "-" + (mes > 9 ? mes : "0" + mes) +
        "-" + ((dateHasta.getDate()) > 9 ? dateHasta.getDate() : "0" + dateHasta.getDate()) 


    $("#txtId").val('');
    $("#cboSerie").val('');
    $("#txtNumeracion").val('');
    $("#cboMoneda").val('');
    $("#txtTipoCambio").val('');
    //$("#cboClaseArticulo").val('');
    //$("#cboEmpleado").val('');
    $("#txtFechaContabilizacion").val(fechaReset);
    $("#cboSucursal").val('');
    $("#txtFechaValidoHasta").val(fechaHastaReset);
    //$("#cboDepartamento").val('');
    $("#txtFechaDocumento").val(fechaReset);
    $("#cboTitular").val('');
    $("#txtTotalAntesDescuento").val('');
    $("#txtComentarios").val('');
    $("#txtImpuesto").val('');
    $("#txtTotal").val('');
    $("#txtEstado").val(1);

    $("#file").val('');
    limitador = 0;
    //------------ANTERIOR AL 12/06/23
    //$("#txtId").val('');
    //$("#cboSerie").val('');
    //$("#txtNumeracion").val('');
    //$("#cboMoneda").val('');
    //$("#txtTipoCambio").val('');
    ////$("#cboClaseArticulo").val('');
    ////$("#cboEmpleado").val('');
    ////$("#txtFechaContabilizacion").val(strDate);
    //$("#cboSucursal").val('');
    ////$("#txtFechaValidoHasta").val(strDate);
    ////$("#cboDepartamento").val('');
    ////$("#txtFechaDocumento").val(strDate);
    //$("#cboTitular").val('');
    //$("#txtTotalAntesDescuento").val('');
    //$("#txtComentarios").val('');
    //$("#txtImpuesto").val('');
    //$("#txtTotal").val('');
    ////$("#txtEstado").val(1);

    //$("#file").val('');
}


function ObtenerDatosxID(IdSolicitudRQ) {





    AbrirModal("modal-form");

    $("#btnGrabar").text("GRABAR")
    //$("#lblTituloModal").html("Nueva Solicitud");
    let seguiradelante = 'false';
    seguiradelante = CargarBasesObraAlmacenSegunAsignado();

    if (seguiradelante == 'false') {
        swal("Informacion!", "No tiene acceso a ninguna base, comunicarse con su administrador");
        return true;
    }


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
                $("#OpenModalItem").show();
                $(".HabilitarVisualizacion").hide();
            } else {
                $("#lblTituloModal").html("Visualizacion");
                $("#OpenModalItem").hide();
                $(".HabilitarVisualizacion").show();
            }


            //console.log("sda");
            //console.log(solicitudes);

            CargarSeries();
            //CargarSolicitante(0);
            //CargarSucursales();
            //CargarDepartamentos();
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

            //$("#cboDepartamento").val(solicitudes[0].IdDepartamento);

            var fechaSplit2 = (solicitudes[0].FechaDocumento.substring(0, 10)).split("-");
            var fecha2 = fechaSplit2[0] + "-" + fechaSplit2[1] + "-" + fechaSplit2[2];
            $("#txtFechaDocumento").val(fecha2);

            $("#cboTitular").val(solicitudes[0].IdTitular);
            $("#txtTotalAntesDescuento").val((Number(solicitudes[0].TotalAntesDescuento)).toFixed(DecimalesImportes));
            $("#txtComentarios").val(solicitudes[0].Comentarios);
            $("#txtImpuesto").val((Number(solicitudes[0].Impuesto)).toFixed(DecimalesImportes));
            $("#txtTotal").val((Number(solicitudes[0].Total)).toFixed(DecimalesImportes));

            


            $("#IdBase").val(solicitudes[0].Detalle[0].IdBase).change()
            $("#IdObra").val(solicitudes[0].Detalle[0].IdObra).change()
            $("#cboAlmacen").val(solicitudes[0].Detalle[0].IdAlmacen)
            $("#cboImpuesto").val(1); //2 titan EXO  //5 CROACIA EXO

            //agrega detalle
            let tr = '';

            let Detalle = solicitudes[0].Detalle;
            console.log("Detalle");
            console.log(Detalle);
            console.log("Detalle");
            for (var i = 0; i < Detalle.length; i++) {

                AgregarLineaDetalle(Detalle[i],
                    Detalle[i].Descripcion,
                    solicitudes[0].Numero,
                    solicitudes[0].Estado,
                    i, Detalle[i].Prioridad,
                    Detalle[i].IdArticulo,
                    Detalle[i].IdSolicitudRQDetalle,
                    Detalle[i].IdUnidadMedida,
                    Detalle[i].IdIndicadorImpuesto,
                    Detalle[i].IdAlmacen,
                    Detalle[i].IdProveedor,
                    Detalle[i].IdLineaNegocio,
                    Detalle[i].IdCentroCostos,
                    Detalle[i].IdProyecto,
                    Detalle[i].IdItemMoneda,
                    Detalle[i].ItemTipoCambio,
                    Detalle[i].CantidadNecesaria,
                    Detalle[i].CantidadSolicitada,
                    Detalle[i].PrecioInfo.toFixed(DecimalesPrecios),
                    Detalle[i].ItemTotal.toFixed(DecimalesImportes),
                    Detalle[i].NumeroFabricacion,
                    Detalle[i].NumeroSerie,
                    Detalle[i].FechaNecesaria,
                    Detalle[i].Referencia,
                    Detalle[i].DescripcionItem,
                    Detalle[i].EstadoDetalle,
                    Detalle[i].SeriePedido,
                    Detalle[i].FechaDocumentoPedido,
                    Detalle[i].ConformidadPedido,
                    Detalle[i].TipoServicio,
                );

                $("#cboImpuesto").val(Detalle[0].IdIndicadorImpuesto);
                $("#IdTipoProducto").val(Detalle[0].IdTipoProducto);
                contador++;
            }


            //AnxoDetalle
            let AnexoDetalle = solicitudes[0].AnexoDetalle;
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


            if (solicitudes[0].Estado == 1) {
                $(".HabilitarVisualizacion").hide();
            } else {
                $(".HabilitarVisualizacion").show();
            }

            if (solicitudes[0].Usuario != solicitudes[0].IdSolicitante) {
                $("#btnGrabar").hide()
            }
            if (solicitudes[0].Usuario != solicitudes[0].IdSolicitante) {
                for (var i = 0; i < Detalle.length; i++) {
                    $("#btnedit" + i).hide();
                }
            }

        }

    });
    //$("#btnGrabar").hide();
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



    $("#txtCantidadAprobada" + contador).val(varCantidadNecesaria);

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


function AgregarLineaDetalle(datos, DescripcionServicio, Numero, EstadoCabecera, contador, Prioridad, IdArticulo, IdSolicitudRQDetalle, IdUnidadMedida, IdIndicadorImpuesto, IdAlmacen, IdProveedor, IdLineaNegocio, IdCentroCosto, IdProyecto, IdMoneda, ItemTipoCambio, CantidadNecesaria, CantidadSolicitada, PrecioInfo, ItemTotal, NumeroFabricacion, NumeroSerie, FechaNecesaria, Referencia, DescripcionItem, EstadoDetalle,SeriePedido,FechaDocumentoPedido,ConformidadPedido,TipoServicio) {
    console.log(datos);
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
    //if (TipoItem == 2) {

    //    DescripcionItem = DescripcionServicio;

    //} else if (TipoItem == 3) {

    //    DescripcionItem = DescripcionServicio;

    //} else {
    //    $.post('/Articulo/ObtenerArticuloxCodigoDescripcion', {
    //        'Codigo': IdArticulo,
    //        'TipoItem': TipoItem
    //    }, function (data, status) {

    //        var errorEmpresa = validarEmpresa(data);
    //        if (errorEmpresa) {
    //            return;
    //        }

    //        let articulos = JSON.parse(data);
    //        console.log("articulos");
    //        console.log(articulos);
    //        console.log("articulos");
    //        DescripcionItem = articulos[0].Descripcion1;
    //        //console.log(DescripcionItem);
    //    });
    //}






    var fechaSplit = (FechaNecesaria.substring(0, 10)).split("-");
    var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];

    let tr = '';

    tr += `<tr>
            <td style="display:none;"><input  class="form-control" type="text" value="`+ IdSolicitudRQDetalle + `" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            

            <td style="display:none">`;
    if (EstadoCabecera == 1) {
        tr += `<button class="btn btn-xs btn-danger"  onclick="EliminarDetalle(` + IdSolicitudRQDetalle + `,this)"><img src="/assets/img/fa_trash.png" height ="15" width="15" /></button>`;
    }


    tr += `<button class="btn btn-xs btn-warning" onclick="BuscarHistorial('` + Numero + `','` + IdArticulo + `','` + IdAlmacen + `',` + TipoItem + `)">H</button>
            </td>
<td>
<button id="btnedit`+ contador + `" class="btn btn-xs btn-danger editar fa fa-edit"  onclick="editDetalle(` + datos.IdSolicitudCabecera + `,` + IdSolicitudRQDetalle + `,` + contador + `,this)"></button>
<button id="btnsave`+ contador + `" class="btn btn-xs btn-danger guardar fa fa-save"  onclick="saveEditDetalle(` + datos.IdSolicitudCabecera + `,` + IdSolicitudRQDetalle + `,` + contador + `,this)" style="display:none"></button>
<button id="btndeleted`+ contador + `" class="btn btn-xs btn-danger borrar fa fa-trash"  onclick="EliminarEditDetalle(` + datos.IdSolicitudCabecera + `,` + IdSolicitudRQDetalle + `,` + contador + `,this)" style="display:none"></button>

</td>

<td class="HabilitarVisualizacion PedidoOC"  onclick="AbrirOC(`+datos.IdPedido+`,'`+ConformidadPedido+`')" style="color:blue" ><u>`+ SeriePedido + `</u></td>`;
    if (FechaDocumentoPedido == "1999-01-01T00:00:00") {
        tr += `<td class="HabilitarVisualizacion" style="display:none">-</td>`;
    } else {
        tr += `<td class="HabilitarVisualizacion" style="display:none">` + FechaDocumentoPedido.split("T")[0] + `</td>`;
    }
    tr += `<td class="HabilitarVisualizacion" style="display:none">`+ ConformidadPedido +`</td>

            <td>
            <input disabled class="form-control" type="text" value="`+ datos.CodArticulo + `"  id="txtCodigoArticulo" name="txtCodigoArticulo[]"/>
            </td>
            <td style="display:none"><input class="form-control" type="text" value="`+ datos.IdArticulo + `" id="txtIdArticulo" name="txtIdArticulo[]" disabled/></td>
            <td><textarea class="form-control" value="`+ datos.Descripcion + `" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]" disabled>` + datos.Descripcion + `</textarea></td>
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
            <select class="form-control" id="cboPrioridadDetalle`+ contador + `" name="cboPrioridadDetalle[]" onchange="ValidarPrioridadDetalle()" disabled>
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
            <td><input class="form-control" type="number" name="txtCantidadSolicitada[]" min="0" value="`+ CantidadSolicitada + `" id="txtCantidadSolicitada` + contador + `" disabled></td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" min="0" value="`+ CantidadNecesaria + `" id="txtCantidadNecesaria` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled></td>`;

    if (TipoItem != 2 && (Number(PrecioInfo)) > 0) {
        tr += `<td style="display:none"><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="` + PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" disabled></td>`;
    } else {
        tr += `<td  style="display:none"><input class="form-control" type="number" name="txtPrecioInfo[]" min="0" value="` + PrecioInfo + `" id="txtPrecioInfo` + contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)" ></td>`;
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
            <td style="display:none"><input class="form-control changeTotal" type="number" value="`+ ItemTotal + `" name="txtItemTotal[]" id="txtItemTotal` + contador + `" onchange="CalcularTotales()" disabled></td>
            <td>
            <select class="form-control"  name="cboAlmacen[]" disabled>`;
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
    tr += `  <option value="0" selected>Seleccione</option>`;
    //for (var i = 0; i < LineaNegocio.length; i++) {
    //    if (LineaNegocio[i].IdLineaNegocio == IdLineaNegocio) {
    //        tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `" selected>` + LineaNegocio[i].Descripcion + `</option>`;
    //    } else {
    //        tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
    //    }
    //}
    tr += `</select>
            </td>
            <td style="display:none">
            <select class="form-control" name="cboCentroCostos[]">`;
    tr += `  <option value="0" selected>Seleccione</option>`;
    //for (var i = 0; i < CentroCosto.length; i++) {
    //    if (CentroCosto[i].IdCentroCosto == IdCentroCosto) {
    //        tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `" selected>` + CentroCosto[i].IdCentroCosto + `</option>`;
    //    } else {
    //        tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].IdCentroCosto + `</option>`;
    //    }
    //}
    tr += `</select>
            </td>
            <td style="display:none">
            <select class="form-control" name="cboProyecto[]">`;
    tr += `  <option value="0" selected>Seleccione</option>`;
    //for (var i = 0; i < Proyecto.length; i++) {
    //    if (Proyecto[i].IdProyecto == IdProyecto) {
    //        tr += `  <option value="` + Proyecto[i].IdProyecto + `" selected>` + Proyecto[i].IdProyecto + `</option>`;
    //    } else {
    //        tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].IdProyecto + `</option>`;
    //    }
    //}
    tr += `</select>
            </td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td style="display:none"><input class="form-control" type="text" value="0" disabled></td>
            <td ><textarea class="form-control" type="text" value="`+ Referencia + `"  name="txtReferencia[]" disabled>` + Referencia + `</textarea></td>
            <td style="display:none"><input style="width:50px" class="form-control" type="text" value="`+ TipoServicio + `" name="txtTipoServicio[]" disabled></input></td>
            <td style="display:none;"><input class="form-control" type="text" value="`+ EstadoDetalle + `"  name="txtEstadoDetalle[]"></td>
            
          </tr>`;

    $("#tabla").find('tbody').append(tr);
    $("#cboPrioridadDetalle" + contador).val(Prioridad);
    $(".PedidoOC").css('cursor', 'pointer')

}
function AbrirOC(IdPedido, conformidad) {
    Swal.fire({
        title: "Buscando Orden de Compra...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {
        if (IdPedido != 0) {
    
            if (conformidad == "CONFIRMADO") {
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
            } else {
                $.ajaxSetup({ async: false });
                $.post("/Pedido/GenerarReporte", { 'NombreReporte': 'OrdenCompraNoValido', 'Formato': 'PDF', 'Id': IdPedido}, function (data, status) {
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
                        console.log("error");
                    }
                });
            }
        }
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


    console.log(Almacen)
    if (Almacen == 0) {
        swal("Informacion!", "Debe Seleccionar Almacen!");
        return;
    }

    //if (TablaItemsDestruida > 0) {  // destruir tabla por cambio de check
    if (tableItems) {
        tableItems.destroy();
    }

    if (TipoItem == 3) {
        $.post("/Articulo/ObtenerArticulosActivoFijo",
           
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
    } else {

        //console.log("aqui");
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


        //} else {
        //    console.log("aqui1");
        //    if (tableItems) {

        //        $("#ModalListadoItem").modal();
        //    } else {
        //        console.log("aqui2");
        //        $.post("/Articulo/ObtenerArticulosConStockSolicitud",
        //            { 'Almacen': Almacen, 'Stock': stock, 'TipoItem': TipoItem, 'TipoProducto': $("#IdTipoProducto").val() },
        //            function (data, status) {

        //                var errorEmpresa = validarEmpresa(data);
        //                if (errorEmpresa) {
        //                    return;
        //                }


        //                if (data == "error") {
        //                    swal("Informacion!", "No se encontro Articulo")

        //                } else {
        //                    $("#ModalListadoItem").modal();

        //                    let items = JSON.parse(data);
        //                    //console.log(items);
        //                    let tr = '';

        //                    for (var i = 0; i < items.length; i++) {

        //                        tr += '<tr id="item' + items[i].IdArticulo + '" onclick="SeleccionTrItem(' + "'" + items[i].IdArticulo + "'" + ')" ondblclick="SeleccionarItemDoubleClick()">' +
        //                            '<td><input type="radio" clase="" id="rdSeleccionado' + items[i].IdArticulo + '"  name="rdSeleccionado"  value = "' + items[i].IdArticulo + '" ></td>' +
        //                            '<td>' + items[i].Codigo + '</td>' +
        //                            '<td>' + items[i].Descripcion1 + '</td>';
        //                        if (TipoItem == 2) {
        //                            tr += '<td>-</td>';
        //                            tr += '<td>-</td>';
        //                        } else {
        //                            tr += '<td>' + items[i].Stock + '</td>';
        //                            tr += '<td>' + items[i].NombUnidadMedida + '</td>';
        //                        }
        //                        //if (items[i].PathImage != "") {
        //                        //    tr += '<td><a target="_blank" href="/SolicitudRQ/DownloadImagen?ImageName=' + items[i].PathImage + '">Ver</a></td>';
        //                        //} else {
        //                        //    tr += '<td><a>-</a></td>';
        //                        //}

        //                        tr += '</tr>';


        //                    }

        //                    $("#tbody_listado_items").html(tr);

        //                    tableItems = $("#tabla_listado_items").DataTable({
        //                        "iDisplayLength": 100,
        //                        info: false, "language": {
        //                            "paginate": {
        //                                "first": "Primero",
        //                                "last": "Último",
        //                                "next": "Siguiente",
        //                                "previous": "Anterior"
        //                            },
        //                            "processing": "Procesando...",
        //                            "search": "Buscar:",
        //                            "lengthMenu": "Mostrar _MENU_ registros"
        //                        },
        //                    });

        //                }

        //            });


        //    }
        //}









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



    let IdObra = $("#IdObra").val();


    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {

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

    let Stock = $("#").val();

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
            $("#txtPrecioUnitarioItem").val(1);
            $("#txtStockAlmacenItem").val(datos.Stock);



        }
    });

    TablaItemsDestruida = 1;
    ObtenerItemsPendientes(IdArticulo)
    ListarStockTodasObras(IdArticulo)

}
function ListarStockTodasObras(IdArticulo) {
    if ($("#cboClaseArticulo").val() != 2) {
        $.post("/Articulo/ObtenerArticulosConStockObras", { 'IdArticulo': IdArticulo }, function (data, status) {

            //console.log(data);
            if (data == "error") {
                table = $("#table_id").DataTable(lenguaje);
                return;
            }

            let tr = '';

            let area = JSON.parse(data);

            for (var i = 0; i < area.length; i++) {


                tr += '<tr>' +
                    '<td>' + area[i].Obra + '</td>' +
                    '<td>' + area[i].Almacen + '</td>' +
                    '<td>' + area[i].Stock + '</td>' +
                    '</tr>';
            }
            if (table) {
                table.destroy();
            }
            $("#tbody_stockOtrosAlmacenes").html(tr);

            table = $("#tablaStockOtrosAlmacenes").DataTable(lenguaje);

        });
    } else {
    }

}

function ObtenerItemsPendientes(IdArticulo)
{
    var idObraPendiente = $("#IdObra").val()

    $.post("/Articulo/ObtenerStockArticuloXPendiente", {'IdObra':idObraPendiente, 'IdArticulo': IdArticulo}, function (data, status) {

        if (data == "error") {
            console.log("error")
        } else {
            let datos = JSON.parse(data);
            console.log(datos);

            $("#txtStockObraPedido").val(datos[0].CantidadPendiente);
          



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

    //let cantidad = $("#" + input).val();
    //let separar = cantidad.split(".");

    //if (separar.length > 1) {
    //    if ((separar[1]).length > DecimalesCantidades) {
    //        $("#" + input).val(Number(cantidad).toFixed(DecimalesCantidades));
    //    }
    //}
}

function ValidarDecimalesImportes(input) {
    //let cantidad = $("#" + input).val();
    //let separar = cantidad.split(".");

    //if (separar.length > 1) {
    //    if ((separar[1]).length > DecimalesCantidades) {
    //        $("#" + input).val(Number(cantidad).toFixed(DecimalesImportes));
    //    }
    //}
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
                    ConsultaServidor();
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
        $("#txtPrecioUnitarioItem").val(1);
        $("#cboMedidaItem").val(0);
        $("#tbody_stockOtrosAlmacenes").empty()
    }



    $("#txtDescripcionItem").val("");
    $("#txtCantidadItem").val("");
    $("#cboProyectoItem").val(0);
    //$("#cboCentroCostoItem").val(0);
    //$("#cboAlmacenItem").val(0);
    $("#txtReferenciaItem").val("");
    $("#txtStockObraPedido").val("")
    $("#tbody_stockOtrosAlmacenes").empty()

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
            if ($("#IdPerfilSesion").val() == 1018 || $("#IdPerfilSesion").val() == 1) {
                $("#IdBase").prop("disabled", false)
                $("#IdObra").prop("disabled", false)
                $("#cboAlmacen").prop("disabled", false)
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

function modalHistorialEstado(IdSolicitudRQ) {
    $("#ModalHistorialEstados").modal();
    $("#div_modelos_aprobaciones").html('');
    let tablee = "";
    $.post("DatosSolicitudModeloAprobaciones", { 'IdSolicitudRQ': IdSolicitudRQ }, function (data, status) {
        let datos = JSON.parse(data);
        console.log(datos.ListSolicitudRqModelo);

        if (datos.ListSolicitudRqModelo.length > 0) {
            for (var i = 0; i < datos.ListSolicitudRqModelo.length; i++) {
                tablee += ` <p>ETAPA:` + (i + 1) + ` </p>
                        <table class="table" id="tabla_modal_estado">
                                        <thead>
                                            <tr>
                                                <th>Nomb. Producto</th>
                                                <th>Aprobador</th>
                                                <th>Aprobacion</th>
                                            </tr>
                                        </thead>
                                    <tbody>`;
                if (datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO.length > 0) {
                    console.log(datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO)

                    for (var j = 0; j < datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO.length; j++) {
                        tablee += `<tr>
                            <td>`+ datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO[j].NombArticulo + `</td>
                            <td>`+ datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO[j].Autorizador.toUpperCase() + `</td>
                           <td>`+ datos.ListSolicitudRqModelo[i].ListModeloAprobacionesDTO[j].NombEstado + `</td>
                            </tr>`;
                    }
                }
                tablee += `</tbody></table>`;
                tablee += `</br>`;
            }

        }
        $("#div_modelos_aprobaciones").html(tablee);
        console.log(datos.ListSolicitudRqModelo);
    })
}





function editDetalle(IdSolicitud, IdSolicitudDetalle, contador, objeto) {
    $("#txtCantidadSolicitada" + contador).prop('disabled', false);
    $("#btnedit" + contador).hide();
    $("#btnsave" + contador).show();
    $("#btndeleted" + contador).show();

}

function saveEditDetalle(IdSolicitud, IdSolicitudDetalle, contador, objeto) {
    let Cantidad = $("#txtCantidadSolicitada" + contador).val();
    $.post("SaveDetalleSolicitud", { 'IdSolicitudDetalle': IdSolicitudDetalle, 'Cantidad': Cantidad }, function (data, status) {
        if (data == "0") {
            swal("Error!", "El Detalle se encuentra con aprobaciones")
        } else {
            swal("Exito!", "Se guardo correctamente");
        }
    });
    CerrarModal();
    ObtenerDatosxID(IdSolicitud)
}

function EliminarEditDetalle(IdSolicitud, IdSolicitudDetalle, contador, objeto) {
    alertify.confirm('Confirmar', '¿Desea eliminar el Item?', function () {

        //$.post("EliminarDetalleSolicitudNew", { 'IdSolicitudRQDetalle': IdSolicitudDetalle }, function (data, status) {

        //    if (data == "0") {
        //        swal("Error!", "El Detalle se encuentra con aprobaciones")
        //    } else {
        //        swal("Exito!", "Se elimino correctamente");
        //    }
        //    CerrarModal();
        //    ObtenerDatosxID(IdSolicitud)
        //});

        $.post("ValidaSolicitudDetalleAprobado", { 'IdSolicitudRQDetalle': IdSolicitudDetalle }, function (data, status) {


            if (data == "1") {
                swal("Error!", "El Detalle se encuentra con aprobaciones")
            } else {
                $(objeto).closest('tr').remove();
            }

            //CerrarModal();
            //ObtenerDatosxID(IdSolicitud)
        });

        

    }, function () { });
}

function LimpiarAlmacen() {
    $("#cboAlmacen").prop("selectedIndex", 0)
}

function ImprimitSolicitudRQ(Id) {
    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {

        $.ajaxSetup({ async: false });
        $.post("/Pedido/GenerarReporte", { 'NombreReporte': 'SolicitudRQ', 'Formato': 'PDF', 'Id': Id }, function (data, status) {
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

function ValidarOCAbiertas(IdItem) {
    $.post("/Pedido/ObtenerOcAbiertasxArtAlmacen", { 'IdArticulo': IdItem, 'IdAlmacen': $("#cboAlmacen").val() }, function (data, status) {
        if (data == "SIN DATOS" || data=="ERROR") {
            console.log(data)
        } else {
            let tr = "";
            let datos = JSON.parse(data)
            for (var i = 0; i < datos.length; i++) {
                tr += '<tr>' +
                  
                    '<td>' + datos[i].NombSerie +'-' +datos[i].Correlativo+ '</td>' +
                    '<td>' + datos[i].CantidadDisponible + '</td>';
            }
            console.log(tr)
            Swal.fire({
                title: 'Se Encontraron OC Abiertas para este Articulo y Almacen',
                html: `<table class="table" style="width:100%">
                    <thead>
                        <tr>
                            <th>OC</th>
                            <th>Cant. Por Entregar</th>                         
                        </tr>
                    </thead>
                    <tbody>`+ tr +`</tbody>
                </table>
                Este mensaje solo es informativo`,
                icon: 'warning'
            })
        }
    });

}