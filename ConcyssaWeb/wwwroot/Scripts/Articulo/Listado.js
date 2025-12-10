let table = '';

function ObtenerProveedores() {

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        let proveedor = JSON.parse(data);
        console.log(proveedor);
        let optionesselect = `<option value="0">SELECCIONE PROVEEDOR</option>`;
        for (var i = 0; i < proveedor.length; i++) {
            optionesselect += `<option value="` + proveedor[i].IdProveedor + `">` + proveedor[i].RazonSocial +`</option>`;

        }
        $("#txtProveedor").html(optionesselect);
    });
}


function addprecioproductoproveedor() {
    let varIdObra
    let IdArticulo=$("#txtId").val();
    let IdProveedor = $("#IdProveedor").val();
    let PrecioSoles = $("#PrecioSoles").val();
    let PrecioDolares = $("#PrecioDolares").val();
    let IdCondicionPagoProveedor = $("#IdCondicionPagoProveedor").val();
    let numeroentrega = $("#numeroentrega").val();

    if (IdProveedor == "" || IdProveedor == null || IdProveedor == "0" || !$.isNumeric(IdProveedor)) {
        swal("Informacion!", "Seleccione Proveedor!");
        return;
    }
    if (PrecioSoles == "" || PrecioSoles == null || PrecioSoles < "0" || !$.isNumeric(PrecioSoles)) {
        swal("Informacion!", "Ingrese Precio Soles!");
        return;
    }

    if (PrecioDolares == "" || PrecioDolares == null || PrecioDolares < "0" || !$.isNumeric(PrecioDolares)) {
        swal("Informacion!", "Ingrese Precio dolares!");
        return;
    }

    if (IdCondicionPagoProveedor == "" || IdCondicionPagoProveedor == null || IdCondicionPagoProveedor == "0" || !$.isNumeric(IdCondicionPagoProveedor)) {
        swal("Informacion!", "Seleccione condicion de pago!");
        return;
    }

    if (numeroentrega == "" || numeroentrega == null || numeroentrega == "0" || !$.isNumeric(numeroentrega)) {
        swal("Informacion!", "Ingrese numero de entrega!");
        return;
    }
    if ($("#ObraArticulo").val() == 0) {
        varIdObra = 0
    } else {
        varIdObra = $("#ObraArticulo").val()
    }

    $.post("/Articulo/SavePrecioProveedorNuevo", {
        "IdArticulo": IdArticulo,
        "IdProveedor": IdProveedor,
        "PrecioSoles": PrecioSoles,
        "PrecioDolares": PrecioDolares,
        "IdCondicionPagoProveedor": IdCondicionPagoProveedor,
        "numeroentrega": numeroentrega,
        "IdObra": varIdObra,
        

    }, function (data, status) {
        swal("Exito!", "Proceso Realizado Correctamente", "success")
        console.log(IdArticulo);
        listarPrecioProductoProveedor(IdArticulo);
        console.log(IdArticulo);
        LimpiarArticuloxProveedor()
    });
}

function LimpiarArticuloxProveedor() {
    $("#IdProveedor").val(0).change()
    $("#PrecioSoles").val("").change()
    $("#PrecioDolares").val("").change()
    $("#IdCondicionPagoProveedor").val(0).change()
    $("#numeroentrega").val("").change()
    $("#ObraArticulo").val(0).change()

}

function CargarCondicionPago() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarCondicionPago(condicionpago, "IdCondicionPagoProveedor", "Seleccione")
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



function CargarProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedor", "Seleccione")
        $("#IdProveedor").select2({
            dropdownParent: $("#modal-form")
        })
    });
}

function llenarComboProveedor(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

window.onload = function () {
    var url = "ObtenerArticulosxSociedad";
    ConsultaServidor(url);
    listarUnidadMedida();
    listarCodigoUbso();
    listarGrupoArticulo();
    CargarCateogiras() 
    CargarGrupoUnidadMedida();
    $("#cboIdCodigoUbso").select2({
        dropdownParent: $("#modal-form")
    });
    $("#divObraArticulo").hide();
    ObtenerObra();

    if ($("#ArtIdPerfil").val() == 1 || $("#ArtIdPerfil").val() == 1018 || $("#ArtIdPerfil").val() == 1022) {
        $("#btnGrabar").show()
        $("#btnNuevoArt").show()
    } else {
        $("#btnGrabar").hide()
        $("#btnNuevoArt").hide()
    }

    $("#CargarExcel").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData($("#CargarExcel")[0]);
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

                if (obtenerUltimaParteDespuesDelPunto(data[0]) != 'xlsx') {
                    Swal.fire("Error!", "Solo puede procesar archivos de Excel", "error")
                    return
                }

                NombreArchivo = data[0]

                $.post('ProcesarExcel', {
                    'archivo': data[0]
                }, function (data, status) {
                    if (data != 'error') {
                        try {

                            Swal.fire({
                                title: "Resultado",
                                html: data,
                                icon: "info"
                            });
                            
                     
                            $("#ModalImportarDatos").modal('hide')
                        }
                        catch (e) {
                            Swal.fire({
                                title: "Error",
                                html: data,
                                icon: "error"
                            });
                        }
                    } else {
                        Swal.fire('Error!', "No se pudo Procesar el Archivo Excel", "error")
                    }

                });
            }
        });
    });

    $("#CargarExcelPrecioProv").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData($("#CargarExcelPrecioProv")[0]);
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

                if (obtenerUltimaParteDespuesDelPunto(data[0]) != 'xlsx') {
                    Swal.fire("Error!", "Solo puede procesar archivos de Excel", "error")
                    return
                }

                NombreArchivo = data[0]

                $.post('ProcesarExcelProvPrecio', {
                    'archivo': data[0]
                }, function (data, status) {
                    if (data != 'error') {
                        try {

                            Swal.fire({
                                title: "Resultado",
                                html: data,
                                icon: "info"
                            });


                            $("#ModalImportarDatosPrecioProv").modal('hide')
                        }
                        catch (e) {
                            Swal.fire({
                                title: "Error",
                                html: data,
                                icon: "error"
                            });
                        }
                    } else {
                        Swal.fire('Error!', "No se pudo Procesar el Archivo Excel", "error")
                    }

                });
            }
        });
    });

  
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
                    options += `<option value="` + datos[i].IdUnidadMedida + `">` + datos[i].Descripcion + `</option>`;
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

    table = $('#table_id').dataTable({
        language: lenguaje_data,
        //responsive: true,
        //scrollX: true,
        ajax: {
            url: 'ObtenerArticulosxSociedad',
            type: 'POST',
            dataSrc: '',
            data: {

                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {


                    return `
                <div class="btn-group" role="group" aria-label="..." style="inline-size: max-content !important; ">
                    <button  style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-primary fa fa-pencil btn-xs" onclick = "ObtenerDatosxID(` + full.IdArticulo + `)" ></button > 
                    <button  style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + full.IdArticulo + `)"></button>
                </div>
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
                    return full.Codigo.toUpperCase()
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Descripcion1.toUpperCase()
                },
            },

            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Descripcion2.toUpperCase()
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUnidadMedida
                },
            },
        ],
        "bDestroy": true
    }).DataTable();

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Articulo");
    $("#TablaAlmacenes").hide();
    $(".radioitem").prop("disabled", false)

    AbrirModal("modal-form");
    ObtenerProveedores();
    CargarProveedor();
    CargarCondicionPago();
    //CargarPerfiles();
    //CargarSociedades();
    //CargarGrupoUnidadMedida();

    let IdPerfil = $("#ArtIdPerfil").val();
    //if (IdPerfil == "1014") {
    //    $('#chkArticulo').attr('disabled', true);
    //    $('#chkActivoFijo').attr('disabled', true);
    //} else {
    //    $('#chkArticulo').attr('disabled', false);
    //    $('#chkActivoFijo').attr('disabled', false);
    //}

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

}






function GuardarArticulo() {

    if ($("#txtDescripcion1").val() == "" || $("#txtDescripcion1").val() == undefined) {
        Swal.fire("Error!", "Complete el campo Descripción", "error")
        return
    }
    if ($("#IdGrupoUnidadMedida").val() == 0 || $("#IdGrupoUnidadMedida").val() == undefined) {
        Swal.fire("Error!", "Selecciona un Grupo Medida Base", "error")
        return
    }
    if ($("#IdUnidadMedidaInv").val() == 0 || $("#IdUnidadMedidaInv").val() == undefined) {
        Swal.fire("Error!", "Selecciona un Grupo Medida", "error")
        return
    }
    if ($("#cboGrupoArticulo").val() == 0 || $("#IdUnidadMedidaInv").val() == undefined) {
        Swal.fire("Error!", "Selecciona un Grupo Articulo", "error")
        return
    }

    let varIdArticulo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion1 = $("#txtDescripcion1").val();
    let varDescripcion2 = $("#txtDescripcion2").val();
    let varIdUnidadMedida = $("#cboIdUnidadMedida").val();
    let IdCodigoUbso = $("#cboIdCodigoUbso").val();
    let varEstadoActivoCatalogo = false;
    let varVenta = false;
    let varCompra = false;

    if ($('#chkArticuloVenta')[0].checked) {
        varVenta = true;
    }
    if ($('#chkArticuloCompra')[0].checked) {
        varCompra = true;
    }
    if ($('#chkActivoCatalogo')[0].checked) {
        varEstadoActivoCatalogo = true;
    }


    let varEstadoInvetario = false;
    let varEstadoActivoFijo = false;

    if ($("#ArtInventario").prop("checked")) {
        varEstadoInvetario = true;
    }
    if ($("#ArtServicio").prop("checked")) {
        varEstadoInvetario = false;
    }
    if ($("#ArtActivoFijo").prop("checked")) {
        varEstadoActivoFijo = true;
    }
   

    varEstado = false;
    if ($('input:radio[name=inlineRadioOptions]:checked').val() == 1) {
        varEstado = true;
    }

    let IdGrupoUnidadMedida = $("#IdGrupoUnidadMedida").val();
    let IdUnidadMedidaInv = $("#IdUnidadMedidaInv").val();


    if ($("#IdGrupoUnidadMedida").val() == 0 || $("#IdGrupoUnidadMedida").val() == undefined || $("#IdGrupoUnidadMedida").val() == null) {
        Swal.fire("Debe Llenar el Campo Grupo Unidad Medida Base", "DATOS INVENTARIO -> Grupo Unidad Medida Base", "info")
        return
    } 
    if ($("#IdUnidadMedidaInv").val() == 0 || $("#IdUnidadMedidaInv").val() == undefined || $("#IdUnidadMedidaInv").val() == null) {
        Swal.fire("Debe Llenar el Campo Unidad Medida", "DATOS INVENTARIO -> Unidad Medida", "info")
        return
    } 
        
    Swal.fire({
        title: "Cargando...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {
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
            'Estado': 1,
            'IdGrupoUnidadMedida': IdGrupoUnidadMedida,
            'IdUnidadMedidaInv': IdUnidadMedidaInv,
            'IdCategoria': $("#cboCategoria").val(),
            'IdProveedor': $("#txtProveedor").val()


        }, function (data, status) {

            if (data == 1) {
                Swal.fire("Exito!", "Proceso Realizado Correctamente", "success")
                table.destroy();
                ConsultaServidor("ObtenerArticulosxSociedad");
                limpiarDatos();
            } else {
                Swal.fire("Error!", "Ocurrio un Error","error")
                limpiarDatos();
            }

        });
    },200)
}

function ObtenerDatosxID(varIdArticulo) {
    limpiarDatos();
    $("#lblTituloModal").html("Editar Articulo");
    ObtenerProveedores();
    AbrirModal("modal-form");


    let IdPerfil = $("#ArtIdPerfil").val();
    //if (IdPerfil == "1014") {
    //    $('#chkArticulo').attr('disabled', true);
    //    $('#chkActivoFijo').attr('disabled', true);
    //} else {
    //    $('#chkArticulo').attr('disabled', false);
    //    $('#chkActivoFijo').attr('disabled', false);
    //}

    CargarProveedor();
    CargarCondicionPago();
    listarPrecioProductoProveedor(varIdArticulo);

    $(".radioitem").prop("disabled",true)

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
            //$("#idIdUnidadMedidaInv").val(articulos[0].IdUnidadMedidaInv);
            $("#txtProveedor").val(articulos[0].IdProveedor);
            if (articulos[0].Estado) {
                $("#chkActivoCatalogo").prop('checked', true);
            }
            if (articulos[0].Compra) {
                $("#chkArticuloCompra").prop('checked', true);
            }
            if (articulos[0].Venta) {
                $("#chkArticuloVenta").prop('checked', true);
            }
            if (articulos[0].ActivoFijo) {
                $("#ArtActivoFijo").prop('checked', true);
            }
            if (articulos[0].Inventario) {
                $("#ArtInventario").prop('checked', true);
            } else {
                $("#ArtServicio").prop('checked', true);
            }


            ObtenerAlmacenes();
            $("#TablaAlmacenes").show();
            console.log(articulos[0].IdUnidadMedidaInv)
            $("#IdUnidadMedidaInv").val(articulos[0].IdUnidadMedidaInv);
            $("#cboCategoria").val(articulos[0].IdCategoria);
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

function listarPrecioProductoProveedor(IdArticulo) {
    $.post("/Articulo/ListarPrecioProductoProveedorNuevo", { 'IdArticulo': IdArticulo }, function (data, status) {
        $("#tbody_tabledetalle_precioproveedor").html();
        let tr = "";
        if (validadJson(data)) {
            let datos = JSON.parse(data);
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    let ObraA
                    let TipoPrecio
                    if (datos[i].Obra == 0) {
                        ObraA = "-"
                        TipoPrecio = "General"
                    } else {
                        ObraA = datos[i].Obra
                        TipoPrecio = "Por Obra"
                    }
                    console.log("aaaaaaaaaaaaaa")
                    tr += `<tr>
    
                        <td>`+ datos[i].Proveedor + `</td>
                        <td><input class="form-control" type="number" id="precioSoles`+ datos[i].IdArticuloProveedor +`" value="`+ datos[i].PrecioSoles + `"/></td>
                        <td><input class="form-control" type="number" id="precioDolar`+ datos[i].IdArticuloProveedor +`" value="`+ datos[i].PrecioDolares + `"/></td>
                        <td>`+ datos[i].CondicionPago + `</td>
                        <td>`+ datos[i].numeroentrega + `</td>
                        <td>`+ TipoPrecio + `</td>
                        <td>`+ ObraA + `</td>
                        <td>
                             <button class="btn btn-sm btn-danger fa fa-floppy-o"  onclick="UpdateProveedorPrecio(`+ datos[i].IdArticuloProveedor +`)"></button>
                            <button class="btn btn-sm btn-danger fa fa-trash"  style="background-color:red" onclick="elimiarproductoproveedor(`+ datos[i].IdArticuloProveedor + `,` + datos[i].IdArticulo + `)"></button>
                       </td>
                    </tr>`;
                }

                $("#tbody_tabledetalle_precioproveedor").html(tr);
            }
        }
      
    });
}
function cerrarModalArticuloProveedor() {
    $("#tbody_tabledetalle_precioproveedor").empty();
}

function elimiarproductoproveedor(IdProductoProveedor,IdArticulo) {
    $.post("/Articulo/EliminarProductoProveedor", { 'IdProductoProveedor': IdProductoProveedor }, function (data, status) {
        if (data == 0) {
            swal("Error!", "Ocurrio un Error")

        } else {
            swal("Exito!", "Articulo Eliminado", "success")
      

        }

    })
    listarPrecioProductoProveedor(IdArticulo);
}


function UpdateProveedorPrecio(IdArticuloProveedor) {

    let PrecioSoles = $("#precioSoles" + IdArticuloProveedor).val();
    let PrecioDolares = $("#precioDolar" + IdArticuloProveedor).val()


    if (PrecioSoles == "" || PrecioSoles == null || PrecioSoles < "0" || !$.isNumeric(PrecioSoles)) {
        swal("Informacion!", "Ingrese Precio Soles!");
        return;
    }

    if (PrecioDolares == "" || PrecioDolares == null || PrecioDolares < "0" || !$.isNumeric(PrecioDolares)) {
        swal("Informacion!", "Ingrese Precio dolares!");
        return;
    }

    if (PrecioDolares == "0" && PrecioSoles == "0") {
        swal("Informacion!", "Ingrese al menos un monto!");
        return;
    }


    $.ajax({
        url: "/Pedido/UpdatePrecioProveedorArticulo",
        type: "POST",
        async: true,
        data: {
            'IdArticuloProveedor': +IdArticuloProveedor,
            'PrecioNacional': +PrecioSoles,
            'PrecioExtranjero': +PrecioDolares
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
                Swal.fire("Exito", "Precios Actualizados", "success")

            } else {
                Swal.fire("Error", "No se Pudieron actualizar los Precios", "error")

            }


        }
    }).fail(function () {
        Swal.fire("Error", "No se Pudieron actualizar los Precios", "error")
    });

}

function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion1").val("");
    $("#txtDescripcion2").val("");
    $("#cboCategoria").val(0);
    $("#chkActivo").prop('checked', false);
    $("#chkArticulo").prop('checked', false);
    $("#chkArticuloCompra").prop('checked', false);
    $("#chkArticuloVenta").prop('checked', false);
    $("#chkActivoFijo").prop('checked', false);
    $("#chkActivoCatalogo").prop('checked', false);
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
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
                '<td><input type="number" class="form-control" id="StockMinimo' + datos[i].IdAlmacen + '" name="StockMinimo[]" value="' + datos[i].StockMinimo + '"/></td>' +
                '<td><input type="number" class="form-control" id="StockMaximo' + datos[i].IdAlmacen + '" name="StockMaximo[]" value="' + datos[i].StockMaximo + '"/></td>' +
                '<td><input type="number" class="form-control" id="StockAlerta' + datos[i].IdAlmacen + '" name="StockAlerta[]" value="' + datos[i].StockAlerta + '"/></td>' +
                '<td>' + datos[i].StockAlmacen + '</td>' +
                '<td><button type="btn btn-primary btn-xs" onclick="GuardarStock(' + IdProducto + ',' + datos[i].IdAlmacen + ')">Guardar</button></td>' +
                '</tr>';
        }

        $("#tbody_inventario").html(tr);



    });

}


function GuardarStock(IdProducto, IdAlmacen) {
    let StockMinimo = $("#StockMinimo" + IdAlmacen).val();
    let StockMaximo = $("#StockMaximo" + IdAlmacen).val();
    let StockAlerta = $("#StockAlerta" + IdAlmacen).val();
    $.post("InsertStockArticuloAlmacen", { 'IdProducto': IdProducto, 'IdAlmacen': IdAlmacen, 'StockMinimo': StockMinimo, 'StockMaximo': StockMaximo, 'StockAlerta': StockAlerta },
        function (data) {

            if (data == "1") {
                swal("Exito!", "Datos Guardados", "success")
            } else {
                swal("Error!", "Ocurrio un Error")
            }


        })

    
}

function alertar() {
    if ($("#PrecioXGeneral").is(":checked")) {
        $("#divObraArticulo").hide()
        $("#ObraArticulo").val(0)
    } else if ($("#PrecioXObra").is(":checked")) {
        $("#divObraArticulo").show()
    }
}
function ObtenerObra() {
    $.ajaxSetup({ async: false });

    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "ObraArticulo", "Seleccione")
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





function OpenModalImportacion() {


    $("#ModalImportarDatos").modal('show')
}

function obtenerUltimaParteDespuesDelPunto(cadena) {

    var partes = cadena.split('.');

    if (partes.length >= 2) {

        var ultimaParte = partes[partes.length - 1];
        return ultimaParte;
    } else {

        return cadena;
    }
}

function DescargarPlantilla() {
    window.open("/Anexos/PlantillaImportacionProductosConcyssa.xlsx", '_blank', 'noreferrer');
}


function OpenModalImportacionArtProv() {


    $("#ModalImportarDatosPrecioProv").modal('show')
}

function CargarCateogiras() {
    $.ajaxSetup({ async: false });
    $.post("/Categoria/ObtenerCategoria", function (data, status) {
        let proveedores = JSON.parse(data);
        llenarcomboCategoria(proveedores, "cboCategoria", "NINGUNO")
    });
}

function llenarcomboCategoria(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCategoria + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}