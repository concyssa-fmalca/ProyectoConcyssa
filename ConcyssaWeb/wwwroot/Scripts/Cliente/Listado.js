let table = '';

window.onload = function () {
    var url = "ObtenerClientes";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let clientes = JSON.parse(data);
        let total_clientes = clientes.length;

        console.log(clientes);

        for (var i = 0; i < clientes.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + clientes[i].NumeroDocumento.toUpperCase() + '</td>' +
                '<td>' + clientes[i].RazonSocial.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + clientes[i].IdCliente + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + clientes[i].IdCliente + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Clientes").html(tr);
        $("#spnTotalRegistros").html(total_clientes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Cliente");
    AbrirModal("modal-form");
    CargarTipoPersona();
    CargarTipoDocumento();
    CargarCondicionPago();
    CargarPaises();
    CargarDepartamentos();
    $("#cboPais").val(193);
}


function GuardarCliente() {

    if (!$("#txtFechaIngreso").val().length > 0) {
        swal("Info!", "Debe elegir fecha de emision")
        return;
    }


    let varIdCliente = $("#txtId").val();
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
    let varTipo = 1; // cliente
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }


    $.post('UpdateInsertCliente', {
        'IdCliente': varIdCliente,
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
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerClientes");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCliente) {
    $("#lblTituloModal").html("Editar Cliente");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCliente': varIdCliente,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let clientes = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(clientes[0].IdCliente);

            $("#txtCodigo").val(clientes[0].CodigoCliente);

            CargarTipoPersona();
            $("#cboTipoPersona").val(clientes[0].TipoPersona);

            CargarTipoDocumento();
            $("#cboTipoDocumento").val(clientes[0].TipoDocumento);

            $("#txtNroDocumento").val(clientes[0].NumeroDocumento);
            $("#txtRazonSocial").val(clientes[0].RazonSocial);
            $("#txtEstadoContribuyente").val(clientes[0].EstadoContribuyente);
            $("#txtCondicionContribuyente").val(clientes[0].CondicionContribuyente);
            $("#txtDireccionFiscal").val(clientes[0].DireccionFiscal);
            $("#txtTlf1").val(clientes[0].Telefono);
            $("#txtComprobantesElectronicos").val(clientes[0].ComprobantesElectronicos);
            $("#txtAfiliadoPLE").val(clientes[0].AfiliadoPLE);
            $("#txtLineaCredito").val(clientes[0].LineaCredito);
            $("#txtEmail").val(clientes[0].Email);
            $("#txtWeb").val(clientes[0].Web);
            $("#txtFax").val(clientes[0].Fax);
            $("#txtNombreContacto").val(clientes[0].NombreContacto);
            $("#txtTlfContacto").val(clientes[0].TelefonoContacto);
            $("#txtEmailContacto").val(clientes[0].EmailContacto);

            var fechaSplit = (clientes[0].FechaIngreso.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            //console.log(fecha);

            $("#txtFechaIngreso").val(fecha);
            $("#txtObservacion").val(clientes[0].Observacion);




            if (clientes[0].Departamento.length > 0) {
                CargarDepartamentos();
                $("#cboDepartamento").val(clientes[0].Departamento);
            }
            if (clientes[0].Provincia.length > 0) {
                CargarProvincias();
                $("#cboProvincia").val(clientes[0].Provincia);
            }
            if (clientes[0].Distrito.length > 0) {
                CargarDistritos();
                $("#cboDistrito").val(clientes[0].Distrito);
            }
            if (clientes[0].Pais.length > 0) {
                CargarPaises();
                $("#cboPais").val(clientes[0].Pais);
            }
            if (clientes[0].CondicionPago.length > 0) {
                CargarCondicionPago();
                $("#cboCondicionPago").val(clientes[0].CondicionPago);
            }


            if (clientes[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCliente) {


    alertify.confirm('Confirmar', '¿Desea eliminar este cliente?', function () {
        $.post("EliminarCliente", { 'IdCliente': varIdCliente }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Cliente Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerClientes");
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
    $.post("ConsultarDocumento", { 'Tipo': varTipoDocumento, 'Documento': varDocumento }, function (data, status) {

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
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
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

    $("#chkActivo").prop('checked', false);
}