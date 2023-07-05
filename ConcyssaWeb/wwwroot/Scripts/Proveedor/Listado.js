let table = '';

window.onload = function () {
    var url = "ObtenerProveedores";
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
                //console.log(data);
                if (data.length > 0) {
                    AgregarLineaAnexo(data[0]);
                }

            }
        });
    });
};


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
            <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}



function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let proveedores = JSON.parse(data);
        let total_proveedores = proveedores.length;

        for (var i = 0; i < proveedores.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + proveedores[i].NumeroDocumento.toUpperCase() + '</td>' +
                '<td>' + proveedores[i].RazonSocial.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + proveedores[i].IdProveedor + ')"></button>' +
                '<button class="btn btn-primary btn-xs" onclick="ObtenerRubrosxID(' + proveedores[i].IdProveedor + ');ListarRubrosxID(' + proveedores[i].IdProveedor + ')">RUBRO</button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + proveedores[i].IdProveedor + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Proveedores").html(tr);
        $("#spnTotalRegistros").html(total_proveedores);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth(), 2) + '-' + f.getDate();
    $("#txtFechaIngreso").val(fecha)
    $("#lblTituloModal").html("Nuevo Proveedor");
    AbrirModal("modal-form");
    CargarTipoPersona();
    CargarTipoDocumento();
    CargarCondicionPago();
    CargarPaises();
    CargarDepartamentos();
    $("#cboPais").val(193);
}
function pad(str, max) { str = str.toString(); return str.length < max ? pad("0" + str, max) : str; }


function GuardarProveedor() {

    let varIdProveedor = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varTipoPersona = $("#cboTipoPersona").val();
    let varTipoDocumento = $("#cboTipoDocumento").val();
    let varNumeroDocumento = $("#txtNroDocumento").val();
    let varRazonSocial = $("#txtRazonSocial").val();
    let varEstadoContribuyente = $("#txtEstadoContribuyente").val();
    let varCondicionContribuyente = $("#txtCondicionContribuyente").val();
    let varDireccionFiscal = $("#txtDireccionFiscal").val();
    let varTelefono = $("#txtTlf1").val();
    let varComprobantesElectronicos = $("#txtComprobantesElectronicos").val();
    let varAfiliadoPLE = $("#txtAfiliadoPLE").val();
    let varLineaCredito = $("#txtLineaCredito").val();
    let varEmail = $("#txtEmail").val();
    let varWeb = $("#txtWeb").val();
    let varFax = $("#txtFax").val();
    let varNombreContacto = $("#txtNombreContacto").val();
    let varTelefonoContacto = $("#txtTlfContacto").val();
    let varEmailContacto = $("#txtEmailContacto").val();



    let varFechaIngreso = $("#txtFechaIngreso").val();


    let varObservacion = $("#txtObservacion").val();
    let varDepartamento = $("#cboDepartamento").val();
    let varProvincia = $("#cboProvincia").val();
    let varDistrito = $("#cboDistrito").val();
    let varPais = $("#cboPais").val();
    let varCondicionPago = $("#cboCondicionPago").val();
    let varDiasEntrega = $("#txtDiasEntrega").val();
    let varTipo = 2; // proveedor
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }
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

    $.post('UpdateInsertProveedor', {
        AnexoDetalle,
        'IdProveedor': varIdProveedor,
        'CodigoCliente': varCodigo,
        'TipoPersona': varTipoPersona,
        'TipoDocumento': varTipoDocumento,
        'NumeroDocumento': varNumeroDocumento,
        'RazonSocial': varRazonSocial,
        'EstadoContribuyente': varEstadoContribuyente,
        'CondicionContribuyente': varCondicionContribuyente,
        'DireccionFiscal': varDireccionFiscal,
        'Departamento': varDepartamento,
        'Provincia': varProvincia,
        'Distrito': varDistrito,
        'Pais': varPais,
        'Telefono': varTelefono,
        'ComprobantesElectronicos': varComprobantesElectronicos,
        'AfiliadoPLE': varAfiliadoPLE,
        'CondicionPago': varCondicionPago,
        'LineaCredito': varLineaCredito,
        'Email': varEmail,
        'Web': varWeb,
        'Fax': varFax,
        'NombreContacto': varNombreContacto,
        'TelefonoContacto': varTelefonoContacto,
        'EmailContacto': varEmailContacto,
        'FechaIngreso': varFechaIngreso,
        'Observacion': varObservacion,
        'Tipo': varTipo,
        'Estado': varEstado,
        'DiasEntrega' : varDiasEntrega
    }, function (data, status) {

        if (data !== 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerProveedores");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdProveedor) {
    $("#lblTituloModal").html("Editar Proveedor");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxIDNuevo', {
        'IdProveedor': varIdProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let proveedores = JSON.parse(data);
            console.log(proveedores)
            //console.log(usuarios);
            $("#txtId").val(proveedores.IdProveedor);
            $("#txtCodigo").val(proveedores.CodigoCliente);

            CargarTipoPersona();
            $("#cboTipoPersona").val(proveedores.TipoPersona);

            CargarTipoDocumento();
            $("#cboTipoDocumento").val(proveedores.TipoDocumento);

            $("#txtNroDocumento").val(proveedores.NumeroDocumento);
            $("#txtRazonSocial").val(proveedores.RazonSocial);
            $("#txtEstadoContribuyente").val(proveedores.EstadoContribuyente);
            $("#txtCondicionContribuyente").val(proveedores.CondicionContribuyente);
            $("#txtDireccionFiscal").val(proveedores.DireccionFiscal);
            $("#txtTlf1").val(proveedores.Telefono);
            $("#txtComprobantesElectronicos").val(proveedores.ComprobantesElectronicos);
            $("#txtAfiliadoPLE").val(proveedores.AfiliadoPLE);
            $("#txtLineaCredito").val(proveedores.LineaCredito);
            $("#txtEmail").val(proveedores.Email);
            $("#txtWeb").val(proveedores.Web);
            $("#txtFax").val(proveedores.Fax);
            $("#txtNombreContacto").val(proveedores.NombreContacto);
            $("#txtTlfContacto").val(proveedores.TelefonoContacto);
            $("#txtEmailContacto").val(proveedores.EmailContacto);
            $("#txtDiasEntrega").val(proveedores.DiasEntrega);

            var fechaSplit = (proveedores.FechaIngreso.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            //console.log(fecha);

            $("#txtFechaIngreso").val(fecha);
            $("#txtObservacion").val(proveedores.Observacion);
            //AnxoDetalle
            let AnexoDetalle = proveedores.AnexoDetalle;
            let trAnexo = '';
            for (var k = 0; k < AnexoDetalle.length; k++) {
                trAnexo += `
                <tr id="`+ AnexoDetalle[k].IdAnexo + `">
                    <td>
                       `+ AnexoDetalle[k].NombreArchivo + `
                    </td>
                    <td>
                       <a target="_blank" href="`+ AnexoDetalle[k].ruta + `"> Descargar </a>
                    </td>
                    <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `)"></button></td>
                </tr>`;
            }
            $("#tabla_files").find('tbody').append(trAnexo);



            if (proveedores.Departamento.length > 0) {
                CargarDepartamentos();
                $("#cboDepartamento").val(proveedores.Departamento);
            }
            if (proveedores.Provincia.length > 0) {
                CargarProvincias();
                $("#cboProvincia").val(proveedores.Provincia);
            }
            if (proveedores.Distrito.length > 0) {
                CargarDistritos();
                $("#cboDistrito").val(proveedores.Distrito);
            }
            if (proveedores.Pais.length > 0) {
                CargarPaises();
                $("#cboPais").val(proveedores.Pais);
            }
            //if (proveedores.CondicionPago.length > 0) {
                CargarCondicionPago();
                $("#cboCondicionPago").val(proveedores.CondicionPago);
            //}


            if (proveedores.Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}
function EliminarAnexo(idRow) {
    alertify.confirm('Confirmar', '¿Desea eliminar este Anexo?', function () {
        $.post("EliminarAnexo", { 'IdAnexo': idRow }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Anexo Eliminado", "success")
                $("#" + idRow).remove();
            }

        });

    }, function () { });
}

function eliminar(varIdProveedor) {


    alertify.confirm('Confirmar', '¿Desea eliminar este proveedor?', function () {
        $.post("EliminarProveedor", { 'IdProveedor': varIdProveedor }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Proveedor Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerProveedores");
                limpiarDatos();
            }

        });

    }, function () { });

}


function CargarTipoPersona() {
    $.ajaxSetup({ async: false });
    $.post("/TipoPersona/ObtenerTipoPersonas", function (data, status) {
        let tipopersona = JSON.parse(data);
        llenarComboTipoPersona(tipopersona, "cboTipoPersona", "Seleccione")
    });
}

function CargarTipoDocumento() {
    $.ajaxSetup({ async: false });
    $.post("/TipoDocumento/ObtenerTipoDocumentos", function (data, status) {
        let tipodocumento = JSON.parse(data);
        llenarComboTipoDocumento(tipodocumento, "cboTipoDocumento", "Seleccione")
    });
}

function CargarCondicionPago() {
    $.ajaxSetup({ async: false });
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarComboCondicionPago(condicionpago, "cboCondicionPago", "Seleccione")
    });
}

function CargarPaises() {
    $.ajaxSetup({ async: false });
    $.post("/Pais/ObtenerPaises", function (data, status) {
        let pais = JSON.parse(data);
        llenarComboPais(pais, "cboPais", "Seleccione")
    });
}

function CargarDepartamentos() {
    $.ajaxSetup({ async: false });
    $.post("/Ubigeo/ObtenerDepartamentos", function (data, status) {
        let departamento = JSON.parse(data);
        llenarComboDepartamento(departamento, "cboDepartamento", "Seleccione")
    });
}

function CargarProvincias() {
    let varDepartamento = $("#cboDepartamento").val();
    $.ajaxSetup({ async: false });
    $.post("/Ubigeo/ObtenerProvincias", { 'Departamento': varDepartamento.slice(0, 2) }, function (data, status) {
        let provincia = JSON.parse(data); console.log("4");
        llenarComboProvincia(provincia, "cboProvincia", "Seleccione")
    });
}

function CargarDistritos() {
    let varProvincia = $("#cboProvincia").val();
    $.ajaxSetup({ async: false });
    $.post("/Ubigeo/ObtenerDistritos", { 'Provincia': varProvincia.slice(0, 4) }, function (data, status) {
        let distrito = JSON.parse(data);
        llenarComboDistrito(distrito, "cboDistrito", "Seleccione")
    });
}

function Buscar() {
    let varTipoDocumento = $("#cboTipoDocumento").val();
    let varDocumento = $("#txtNroDocumento").val();
    $.post("/Cliente/ConsultarDocumento", { 'Tipo': varTipoDocumento, 'Documento': varDocumento }, function (data, status) {

        if (data == "error") {
            swal("Error!", "Ocurrio un al buscar este documento")
        }

        let datos = JSON.parse(data);
        let CantidadDatos = Object.keys(datos.persona).length;

        if (CantidadDatos > 3) {

            $("#txtRazonSocial").val(datos.persona.razonSocial);
            $("#txtEstadoContribuyente").val(datos.persona.estado);
            $("#txtCondicionContribuyente").val(datos.persona.condicion);
            $("#txtDireccionFiscal").val(datos.persona.direccion);

            let ubigeo = datos.persona.ubigeo;
            let obtenerdosdigitos = ubigeo.slice(0, 2);
            let obtenercuatrodigitos = ubigeo.slice(0, 4);
            let varDepartamento = obtenerdosdigitos + '0000';
            let varProvincia = obtenercuatrodigitos + '00';
            let varDistrito = ubigeo;

            $("#cboDepartamento").val(varDepartamento); console.log("1");
            CargarProvincias(); console.log("3");
            $("#cboProvincia").val(varProvincia);
            CargarDistritos();
            $("#cboDistrito").val(varDistrito);

        } else {
            $("#txtRazonSocial").val(datos.persona.razonSocial);
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


function ValidarBotonBuscar() {

    $("#btnBuscarReniec").show();
    let varTipoDocumento = $("#cboTipoDocumento").val();
    if (varTipoDocumento == 0) {
        $("#btnBuscarReniec").hide();
    }

    limpiarDatos();

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


function llenarComboCondicionPago(lista, idCombo, primerItem) {
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


function llenarComboPais(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdPais + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboProvincia(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboDistrito(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    //$("#cboTipoPersona").val("");
    //$("#cboTipoDocumento").val("");
    $("#txtNroDocumento").val("");
    $("#txtRazonSocial").val("");
    $("#txtEstadoContribuyente").val("");
    $("#txtCondicionContribuyente").val("");
    $("#txtDireccionFiscal").val("");
    $("#txtTlf1").val("");
    $("#txtComprobantesElectronicos").val("");
    $("#txtAfiliadoPLE").val("");
    $("#txtLineaCredito").val("");
    $("#txtEmail").val("");
    $("#txtWeb").val("");
    $("#txtFax").val("");
    $("#txtNombreContacto").val("");
    $("#txtTlfContacto").val("");
    $("#txtEmailContacto").val("");
    $("#txtFechaIngreso").val("");
    $("#txtObservacion").val("");

    $("#cboDepartamento").val("");
    $("#cboProvincia").val("");
    $("#cboDistrito").val("");
    //$("#cboPais").val("");
    $("#cboCondicionPago").val("");

    $("#chkActivo").prop('checked', true);
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth(), 2) + '-' + f.getDate();
    $("#txtFechaIngreso").val(fecha)
    $("#tabla_files").find('tbody').empty();
}

function CrearCodigo()
{
    $("#txtCodigo").val("P" + $("#txtNroDocumento").val())
}

function ObtenerRubrosxID(IdProveedor) {
    $("#ModalRubro").modal("show");
    CargarRubro()
    $.post('ObtenerDatosxIDNuevo', {
        'IdProveedor': IdProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let proveedores = JSON.parse(data);           
            $("#txtIdRSRubro").val(proveedores.IdProveedor);
            $("#txtRazonSocialRubro").val(proveedores.RazonSocial);
        
        }

    });
}

function CargarRubro() {
    $.ajaxSetup({ async: false });
    $.post("/RubroProveedor/ObtenerRubroProveedor", function (data, status) {
        let rubros = JSON.parse(data);
        llenarComboRubro(rubros, "cboRubro", "Seleccione")
    });
}
function llenarComboRubro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdRubroProveedor + "'>" + lista[i].Codigo + " - " + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function AgregarRubro() {
    if ($("#cboRubro").val() == 0) {
        swal("Informacion!", "Debe Seleccionar el Rubro!");
        return;
    }
    let varIdRubro = $("#cboRubro").val();
    let varIdProveedorR = $("#txtIdRSRubro").val();

    $.post('InsertRubroProveedor_X_Provedor', {
        'IdRubroProveedor': varIdRubro,
        'IdProveedor': varIdProveedorR,
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            ListarRubrosxID(varIdProveedorR)
            //ConsultaServidor("ObtenerArea");
            //limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
           /* limpiarDatos();*/
        }

    });

}
let tablePR
function ListarRubrosxID(IdProveedor) {
    try {
        $.post('ListarRubroProveedor_X_Provedor', {
            'IdProveedor': IdProveedor,
        }, function (data, status) {
            if (data == "Error") {
                tablePR = $("#tablaRubroProveedor").DataTable(lenguaje);
                return;
            } else {
                let tr = '';

                let provrubro = JSON.parse(data);


                for (var i = 0; i < provrubro.length; i++) {

                    tr += '<tr>' +
                        '<td>' + provrubro[i].RazonSocial.toUpperCase() + '</td>' +
                        '<td>' + provrubro[i].Descripcion.toUpperCase() + '</td>' +
                        '<td><button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminarProvRub(' + provrubro[i].Id + ',' + IdProveedor + ')"></button></td >' +
                        '</tr>';
                }

                if (tablePR) { tablePR.destroy(); }


                $("#tbody_RubroProveedor").html(tr);

                tablePR = $("#tablaRubroProveedor").DataTable(lenguaje);

            }

        });
    }
    catch (e) {
        tablePR.clear()
        tablePR = $("#tablaRubroProveedor").DataTable(lenguaje);
    }
}
function LimpiarModalRubro() {
    console.log(1)
}
function eliminarProvRub(Id, IdProveedor) {
    alertify.confirm('Confirmar', '¿Desea eliminar el Rubro para este Proveedor?', function () {
        $.post("EliminarRubroProveedor_X_Provedor", { 'Id': Id }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Proveedor Eliminado", "success")
                ListarRubrosxID(IdProveedor)
            }

        });

    }, function () { });
}

