let table = '';


window.onload = function () {
    var url = "ObtenerPerfiles";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let perfiles = JSON.parse(data);
        let total_perfiles = perfiles.length;

        for (var i = 0; i < perfiles.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + perfiles[i].Perfil.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + perfiles[i].IdPerfil + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + perfiles[i].IdPerfil + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Perfiles").html(tr);
        $("#spnTotalRegistros").html(total_perfiles);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Perfil");
    AbrirModal("modal-form");
}




function GuardarPerfil() {

    let varIdPerfil = $("#txtId").val();
    let varPerfil = $("#txtPerfil").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }
    if (varIdPerfil == "" || varIdPerfil == undefined) {
        Swal.fire("Error", "El Campo Perfil es Obligatorio", "info")
        return
    }
    $.post('UpdateInsertPerfil', {
        'IdPerfil': varIdPerfil,
        'Perfil': varPerfil,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerPerfiles");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdPerfil) {
    $("#lblTituloModal").html("Editar Perfil");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdPerfil': varIdPerfil,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let perfiles = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(perfiles[0].IdPerfil);
            $("#txtPerfil").val(perfiles[0].Perfil);
            if (perfiles[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdPerfil) {


    alertify.confirm('Confirmar', '¿Desea eliminar este perfil?', function () {
        $.post("EliminarPerfil", { 'IdPerfil': varIdPerfil }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Perfil Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerPerfiles");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtPerfil").val("");
    $("#chkActivo").prop('checked', false);
}



