let table = '';
let contartr = 0;

window.onload = function () {
    var url = "ObtenerUsuarios";
    ConsultaServidor(url);
    CargarEstadosGiro();
    
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
            console.log('ddddddddddddddddddd');
            console.log(usuarios[i]);
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + usuarios[i].Nombre.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].Usuario.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].NombrePerfil.toUpperCase() + '</td>' +
                '<td>' + usuarios[i].NombreSociedad.toUpperCase() + '</td>' +
                '<td>' +
                    `<div class="btn-group" role="group" aria-label="..." style="inline-size: max-content !important; ">
                        <button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-primary fa fa-pencil btn-xs" onclick = "ObtenerDatosxID(` + usuarios[i].IdUsuario + `)" ></button>
                        <button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(`+ usuarios[i].IdUsuario + `)"></button>
                        <button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-info btn-xs " onclick="AbrirModalBaseAlmacen(` + usuarios[i].IdUsuario + `)">BA</button>
                    </div>`+
                '</td > ' +
                '</tr>';
        }

        $("#tbody_Usuarios").html(tr);
        $("#spnTotalRegistros").html(total_usuarios);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Usuario");
    AbrirModal("modal-form");
    CargarPerfiles();
    CargarSociedades();
    CargarEstadosGiro();
    CargarDepartamentos();
    listarEmpleados();
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
        console.log(perfiles);
        llenarComboPerfil(perfiles, "cboPerfil", "Seleccione")

    });

}

function CargarSociedades() {

    $.post("ObtenerSociedades", function (data, status) {

        let sociedades = JSON.parse(data);
        llenarComboSociedad(sociedades, "cboSociedad", "Seleccione")

    });

}
function CargarEstadosGiro() {

    $.post("/Estados/ObtenerEstados", { Modulo: 2 }, function (data, status) {

        let sociedades = JSON.parse(data);
        llenarComboEstados(sociedades, "cboAbrobar", "NINGUNO")

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


function llenarComboEstados(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Id + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#"+idCombo).val(0)
}


function GuardarUsuario() {

    let varIdUsuario = $("#txtId").val();
    let varNombre = $("#txtNombre").val();
    let varUsuario = $("#txtUsuario").val();
    let varContraseña = $("#txtContraseña").val();
    let varPerfil = $("#cboPerfil").val();
    let varSociedad = $("#cboSociedad").val();
    let varAbrobar = $("#cboAbrobar").val();
    let varEstado = false;
    let MovimientoInventario = false;

    let varDepartamento = $("#cboDepartamento").val();
    let varUsuarioSAP = $("#txtUsuarioSap").val();
    let varContrasenaSAP = $("#txtContrasenaSap").val();
    let varCorreo = $("#txtCorreo").val();
    let varEmpleado = $("#cboEmpleado").val();

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if ($('#MovimientoInventario')[0].checked) {
        MovimientoInventario = true;
    }

    $.post('UpdateInsertUsuario', {
        'IdUsuario': varIdUsuario,
        'Nombre': varNombre,
        'Usuario': varUsuario,
        'Password': varContraseña,
        'IdPerfil': varPerfil,
        'IdSociedad': varSociedad,
        'Estado': varEstado,
        'SapUsuario': varUsuarioSAP,
        'SapPassword': varContrasenaSAP,
        'IdDepartamento': varDepartamento,
        'AprobarGiro': varAbrobar,
        'Correo': varCorreo,
        'IdEmpleado': varEmpleado
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
            console.log("ssssss");
            console.log(usuarios);
            console.log("ssssss");
            $("#txtId").val(usuarios[0].IdUsuario);
            $("#txtNombre").val(usuarios[0].Nombre);
            $("#txtUsuario").val(usuarios[0].Usuario);
            $("#txtContraseña").val(usuarios[0].Password);

            $("#txtUsuarioSap").val(usuarios[0].Password);
            $("#txtContrasenaSap").val(usuarios[0].SapPassword);
            $("#txtCorreo").val(usuarios[0].Correo);
            $("#cboDepartamento").val(usuarios[0].IdDepartamento);
            $("#cboAbrobar").val(usuarios[0].AprobarGiro);
            if (usuarios[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

            if (usuarios[0].MovimientoInventario) {
                $("#MovimientoInventario").prop('checked', true);
            }


            listarEmpleados();
            $("#cboEmpleado").val(usuarios[0].IdEmpleado);

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
    $("#MovimientoInventario").prop('checked', false);
}

function listartablealmacenbase(IdUsuario) {
    $("#table_base_almacen").html("");
    $.post("ObtenerBaseAlmacenxIdUsuario", { 'IdUsuario': IdUsuario }, function (data, status) {
        let basealmacenes = JSON.parse(data);
        let tr = "";
        for (var i = 0; i < basealmacenes.length; i++) {
            contartr++;
            tr = `<tr  id="trab` + contartr +`">
                <td>`+ (i + 1) + `</td>
                <td>
                    <select id="ab_IdBase`+ contartr + `" onchange="ObtenerObraxIdBase(` + contartr + `)" class="form-control input-sm">
                    </select>
                </td>
                <td>    
                    <select id="ab_IdObra`+ contartr + `" onchange="ObtenerAlmacenxIdObra(` + contartr + `)" class="form-control input-sm">
                    </select>
                </td>
                <td><select id="ab_IdAlmacen`+ contartr + `" class="form-control input-sm">
                    </select></td>
                <td>
                    <div class="btn-group" role="group" aria-label="..." style="inline-size: max-content !important; ">
                        <button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-info btn-xs fa"   onclick="savealmacenbase(`+ contartr + `,` + basealmacenes[i].IdUsuarioBase + `)">Guardar</button>
                        <button style="background-color:red;margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs fa fa-trash" onclick="eliminartralmacenbase(`+ basealmacenes[i].IdUsuarioBase + `,` + contartr +`)"></button>
                    </div>

                    
                </td>
            </tr>`;
            $("#table_base_almacen").append(tr);
            CargarBase(contartr);
            $("#ab_IdBase" + contartr).val(basealmacenes[i].IdBase).change();
            $("#ab_IdObra" + contartr).val(basealmacenes[i].IdObra).change();
            $("#ab_IdAlmacen" + contartr).val(basealmacenes[i].IdAlmacen);
        }
    });
}


function AbrirModalBaseAlmacen(IdUsuario) {
   
    $("#txtIdUsuarioAlmacenBase").val(IdUsuario)
    AbrirModal("modal-almacen-base");
    listartablealmacenbase(IdUsuario)
}


function CargarBase(countbase) {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBase", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBase(base, "ab_IdBase" + countbase, "Seleccione")
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





function ObtenerObraxIdBase(countbase) {
    let IdBase = $("#ab_IdBase" + countbase).val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdBase", { 'IdBase': IdBase }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "ab_IdObra" + countbase, "Seleccione")
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


function ObtenerAlmacenxIdObra(counttr) {
    let IdObra = $("#ab_IdObra" + counttr).val();
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        if (validadJson(data)) {
            let almacen = JSON.parse(data);
            llenarComboAlmacen(almacen, "ab_IdAlmacen" + counttr, "Seleccione")
        } else {
            $("#ab_IdAlmacen" + counttr).html('<option value="0">SELECCIONE</option>')

        }

    });
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

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function addtrAlmacenBase() {
    contartr++;
    tr = `<tr id="trab` + contartr +`">
                <td>-</td>
                <td>
                    <select id="ab_IdBase`+ contartr + `" onchange="ObtenerObraxIdBase(` + contartr + `)" class="form-control input-sm">
                    </select>
                </td>
                <td>    
                    <select id="ab_IdObra`+ contartr + `" onchange="ObtenerAlmacenxIdObra(` + contartr + `)" class="form-control input-sm">
                    </select>
                </td>
                <td><select id="ab_IdAlmacen`+ contartr + `" class="form-control input-sm">
                    </select></td>
                <td>
                    <div class="btn-group" role="group" aria-label="..." style="inline-size: max-content !important; ">
                        <button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-info btn-xs" onclick="savealmacenbase(`+ contartr +`,0)">GUARDAR</button>
                        <button style="background-color:red;margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs" onclick="eliminartralmacenbase(0,`+ contartr +`)">X</button>
                    </div>
                </td>
            </tr>`;
    $("#table_base_almacen").append(tr);
    CargarBase(contartr);
}

function savealmacenbase(contarr, IdUsuarioBase) {
    let IdBase = $("#ab_IdBase" + contarr).val();
    let IdObra = $("#ab_IdObra" + contarr).val();
    let IdAlmacen = $("#ab_IdAlmacen" + contarr).val();
    let IdUsuario = $("#txtIdUsuarioAlmacenBase").val();
    if (IdBase==0) {
        swal("Error!", "Seleccione una Base");
        return;
    }
    if (IdObra == 0) {
        swal("Error!", "Seleccione una Obra");
        return;
    }
    if (IdAlmacen == 0) {
        swal("Error!", "Seleccione un Almacen");
        return;
    }


    $.post("GuardarAlmacenBasexUsuario", { 'IdUsuarioBase': IdUsuarioBase,'IdBase': IdBase, 'IdObra': IdObra, 'IdAlmacen': IdAlmacen, 'IdUsuario': IdUsuario }, function (data, status) {
        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
        } else {
            swal("Error!", "Ocurrio un Error")
        }
        listartablealmacenbase(IdUsuario);
    });
}

function eliminartralmacenbase(IdEliminar,contartr) {
    if (IdEliminar == 0) {
        $("#trab" + contartr).remove();
    } else {
        $.post("EliminarUsuarioBase", { 'IdUsuarioBase': IdEliminar }, function (data, status) {
            if (data == 1) {
                swal("Exito!", "Proceso Realizado Correctamente", "success")
            } else {
                swal("Error!", "Ocurrio un Error")
            }
        });
    }
    listartablealmacenbase($("#txtIdUsuarioAlmacenBase").val());
}



function listarEmpleados() {
    $.ajaxSetup({ async: false });

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

            //console.log(datos);
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdEmpleado + `">` + datos[i].RazonSocial + `</option>`;
                }
                $("#cboEmpleado").html(options);
            }
        }
    });
}
