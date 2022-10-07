let table = '';

window.onload = function () {
    var url = "ObtenerEmpleados";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let empleados = JSON.parse(data);
        let total_empleados = empleados.length;

        for (var i = 0; i < empleados.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + empleados[i].NumeroDocumento.toUpperCase() + '</td>' +
                '<td>' + empleados[i].RazonSocial.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + empleados[i].IdEmpleado + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + empleados[i].IdEmpleado + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Empleados").html(tr);
        $("#spnTotalRegistros").html(total_empleados);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth(),2) +  '-' + f.getDate();
    $("#txtFechaIngreso").val(fecha)
    $("#lblTituloModal").html("Nuevo Empleado");
    AbrirModal("modal-form");
    CargarTipoPersona();
    CargarTipoDocumento();
    CargarCondicionPago();
    CargarPaises();
    CargarDepartamentos();
    $("#cboPais").val(193);
}


function pad(str, max) { str = str.toString(); return str.length < max ? pad("0" + str, max) : str; }



function GuardarEmpleado() {

    let varIdEmpleado = $("#txtId").val();
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
    let varTipo = 3; // empleado
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if (varFechaIngreso) {
        //console.log("lleno");
    } else {
        swal("Error!", "Debe llenar fecha de ingreso")
        return;
    }



    $.post('UpdateInsertEmpleado', {
        'IdEmpleado': varIdEmpleado,
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
            ConsultaServidor("ObtenerEmpleados");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdEmpleado) {
    $("#lblTituloModal").html("Editar Empleado");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdEmpleado': varIdEmpleado,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let empleados = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(empleados[0].IdEmpleado);
            $("#txtCodigo").val(empleados[0].CodigoCliente);

            CargarTipoPersona();
            $("#cboTipoPersona").val(empleados[0].TipoPersona);

            CargarTipoDocumento();
            $("#cboTipoDocumento").val(empleados[0].TipoDocumento);

            $("#txtNroDocumento").val(empleados[0].NumeroDocumento);
            $("#txtRazonSocial").val(empleados[0].RazonSocial);
            $("#txtEstadoContribuyente").val(empleados[0].EstadoContribuyente);
            $("#txtCondicionContribuyente").val(empleados[0].CondicionContribuyente);
            $("#txtDireccionFiscal").val(empleados[0].DireccionFiscal);
            $("#txtTlf1").val(empleados[0].Telefono);
            $("#txtComprobantesElectronicos").val(empleados[0].ComprobantesElectronicos);
            $("#txtAfiliadoPLE").val(empleados[0].AfiliadoPLE);
            $("#txtLineaCredito").val(empleados[0].LineaCredito);
            $("#txtEmail").val(empleados[0].Email);
            $("#txtWeb").val(empleados[0].Web);
            $("#txtFax").val(empleados[0].Fax);
            $("#txtNombreContacto").val(empleados[0].NombreContacto);
            $("#txtTlfContacto").val(empleados[0].TelefonoContacto);
            $("#txtEmailContacto").val(empleados[0].EmailContacto);

            var fechaSplit = (empleados[0].FechaIngreso.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            //console.log(fecha);

            $("#txtFechaIngreso").val(fecha);
            $("#txtObservacion").val(empleados[0].Observacion);


            if (empleados[0].Departamento.length > 0) {
                CargarDepartamentos();
                $("#cboDepartamento").val(empleados[0].Departamento);
            }
            if (empleados[0].Provincia.length > 0) {
                CargarProvincias();
                $("#cboProvincia").val(empleados[0].Provincia);
            }
            if (empleados[0].Distrito.length > 0) {
                CargarDistritos();
                $("#cboDistrito").val(empleados[0].Distrito);
            }
            if (empleados[0].Pais.length > 0) {
                CargarPaises();
                $("#cboPais").val(empleados[0].Pais);
            }
            if (empleados[0].CondicionPago.length > 0) {
                CargarCondicionPago();
                $("#cboCondicionPago").val(empleados[0].CondicionPago);
            }


            if (empleados[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdEmpleado) {


    alertify.confirm('Confirmar', '¿Desea eliminar este empleado?', function () {
        $.post("EliminarEmpleado", { 'IdEmpleado': varIdEmpleado }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Empleado Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerEmpleados");
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

    //limpiarDatos();

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
    var f = new Date();
    fecha = f.getFullYear() + '-' + pad(f.getMonth(), 2) + '-' + f.getDate();
    $("#txtFechaIngreso").val(fecha)
}