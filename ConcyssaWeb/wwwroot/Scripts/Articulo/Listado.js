let table = '';

window.onload = function () {
    var url = "ObtenerArticulosxSociedad";
    ConsultaServidor(url);
    listarUnidadMedida()
};

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
                    options += `<option value="` + datos[i].IdUnidadMedida +`">` + datos[i].Descripcion +`</option>`;
                }
                $("#cboIdUnidadMedida").html(options);
            }
        }
    });
}



function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let articulos = JSON.parse(data);
        let total_articulos = articulos.length;

        for (var i = 0; i < usuarios.length; i++) {
            console.log('ddddddddddddddddddd');
            console.log(usuarios[i]);
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + articulos[i].Nombre.toUpperCase() + '</td>' +
                '<td>' + articulos[i].Usuario.toUpperCase() + '</td>' +
                '<td>' + articulos[i].NombrePerfil.toUpperCase() + '</td>' +
                '<td>' + articulos[i].NombreSociedad.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + usuarios[i].IdUsuario + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + usuarios[i].IdUsuario + ')"></button></td >' +
                '</tr>';
        }
        
        $("#tbody_Articulos").html(tr);
        $("#tbody_Articulos").html(total_articulos);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Articulo");
    AbrirModal("modal-form");
    CargarPerfiles();
    CargarSociedades();
}






function GuardarArticulo() {

    let varIdArticulo = $("#txtIdArticulo").val();
    let varDescripcion1 = $("#txtDescripcion1").val();
    let varDescripcion2 = $("#txtDescripcion2").val();
    let varIdUnidadMedida = $("#cboIdUnidadMedida").val();
    let IdCodigoUbso = $("#cboIdCodigoUbso").val();
    let varEstadoActivoFijo = false;
    let varEstadoActivoCatalogo = false;

    if ($('#chkActivoFijo')[0].checked) {
        varEstadoActivoFijo = true;
    }
    if ($('#chkActivoCatalogo')[0].checked) {
        varEstadoActivoCatalogo = true;
    }

    $.post('UpdateInsertUsuario', {
        'IdArticulo': varIdArticulo,
        'Descripcion1': varDescripcion1,
        'Descripcion2': varDescripcion2,
        'IdUnidadMedida': varIdUnidadMedida,
        'IdCodigoUbso': IdCodigoUbso,
        'ActivoFijo': varEstadoActivoFijo,
        'ActivoCatalogo': varEstadoActivoCatalogo
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
            $("#txtNombre").val(usuarios[0].Nombre);
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



