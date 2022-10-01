let table = '';
alert('ddd');
window.onload = function () {
    var url = "ObtenerUsuarios";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let usuarios = JSON.parse(data);
        let total_usuarios = usuarios.length;

        for (var i = 0; i < usuarios.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + usuarios[i].NombreUsuario.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].Usuario.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].NombrePerfil.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].NombreSociedad.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + usuarios[i].IdUsuario + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + usuarios[i].IdUsuario + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Usuarios").html(tr);
        $("#spnTotalRegistros").html(total_usuarios);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Usuario");
    alert('eee');
    AbrirModal("modal-form");
    CargarPerfiles();
    CargarSociedades();
    CargarDepartamentos();
}


function CargarDepartamentos() {

    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        //var errorEmpresa = validarEmpresa(data);
        //if (errorEmpresa) {
        //    return;
        //}
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")

    });
}

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
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

function CargarPerfiles() {

    $.post("ObtenerPerfiles", function (data, status) {

        let perfiles = JSON.parse(data);
        llenarComboPerfil(perfiles, "cboPerfil", "Seleccione")

    });

}

function CargarSociedades() {

    $.post("ObtenerSociedades", function (data, status) {

        let sociedades = JSON.parse(data);
        llenarComboSociedad(sociedades, "cboSociedad", "Seleccione")

    });

}

function llenarComboPerfil(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdPerfil + "'>" + lista[i].Perfil + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboSociedad(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSociedad + "'>" + lista[i].NombreSociedad + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}



function GuardarUsuario() {

    let varIdUsuario = $("#txtId").val();
    let varNombre = $("#txtNombre").val();
    let varUsuario = $("#txtUsuario").val();
    let varContraseña = $("#txtContraseña").val();
    let varPerfil = $("#cboPerfil").val();
    let varSociedad = $("#cboSociedad").val();
    let varEstado = false;

    let varDepartamento = $("#cboDepartamento").val();
    let varUsuarioSAP = $("#txtUsuarioSap").val();
    let varContrasenaSAP = $("#txtContrasenaSap").val();
    let varCorreo = $("#txtCorreo").val();

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertUsuario', {
        'IdUsuario': varIdUsuario,
        'NombreUsuario': varNombre,
        'Usuario': varUsuario,
        'Password': varContraseña,
        'IdPerfil': varPerfil,
        'IdSociedad': varSociedad,
        'Estado': varEstado,
        'SapUsuario': varUsuarioSAP,
        'SapPassword': varContrasenaSAP,
        'IdDepartamento': varDepartamento,
        'Correo': varCorreo
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerUsuarios");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdUsuario) {
    $("#lblTituloModal").html("Editar Usuario");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdUsuario': varIdUsuario,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let usuarios = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(usuarios[0].IdUsuario);
            $("#txtNombre").val(usuarios[0].NombreUsuario);
            $("#txtUsuario").val(usuarios[0].Usuario);
            $("#txtContraseña").val(usuarios[0].Password);
            if (usuarios[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

            $.post("ObtenerSociedades", function (data, status) {
                let contenido;
                let sociedades = JSON.parse(data);
                for (var i = 0; i < sociedades.length; i++) {
                    if (sociedades[i].IdSociedad == usuarios[0].IdSociedad) {
                        contenido += "<option selected value='" + sociedades[i].IdSociedad + "'>" + sociedades[i].NombreSociedad + "</option>";
                    } else {
                        contenido += "<option value='" + sociedades[i].IdSociedad + "'>" + sociedades[i].NombreSociedad + "</option>";
                    }
                }
                let cbo = document.getElementById("cboSociedad");
                if (cbo != null) cbo.innerHTML = contenido;
            });

            $.post("ObtenerPerfiles", function (data, status) {
                let contenido;
                let perfiles = JSON.parse(data);
                //console.log(perfiles);
                for (var i = 0; i < perfiles.length; i++) {
                    if (perfiles[i].IdPerfil == usuarios[0].IdPerfil) {
                        contenido += "<option selected value='" + perfiles[i].IdPerfil + "'>" + perfiles[i].Perfil + "</option>";
                    } else {
                        contenido += "<option value='" + perfiles[i].IdPerfil + "'>" + perfiles[i].Perfil + "</option>";
                    }
                }
                let cbo = document.getElementById("cboPerfil");
                if (cbo != null) cbo.innerHTML = contenido;
            });



            $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
                //var errorEmpresa = validarEmpresa(data);
                //if (errorEmpresa) {
                //    return;
                //}

                let contenido;
                let departamentos = JSON.parse(data);
                //console.log(perfiles);
                for (var i = 0; i < departamentos.length; i++) {
                    if (departamentos[i].IdDepartamento == usuarios[0].IdDepartamento) {
                        contenido += "<option selected value='" + departamentos[i].IdDepartamento + "'>" + departamentos[i].Descripcion + "</option>";
                    } else {
                        contenido += "<option value='" + departamentos[i].IdDepartamento + "'>" + departamentos[i].Descripcion + "</option>";
                    }
                }
                let cbo = document.getElementById("cboDepartamento");
                if (cbo != null) cbo.innerHTML = contenido;
            });


        }

    });

}

function eliminar(varIdUsuario) {


    alertify.confirm('Confirmar', '¿Desea eliminar este usuario?', function () {
        $.post("EliminarUsuario", { 'IdUsuario': varIdUsuario }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Usuario Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerUsuarios");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtNombre").val("");
    $("#txtUsuario").val("");
    $("#txtContraseña").val("");
    $("#chkActivo").prop('checked', false);
}



