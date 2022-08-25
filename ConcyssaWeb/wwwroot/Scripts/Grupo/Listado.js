let table = '';


window.onload = function () {
    var url = "ObtenerGrupo";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {
    
    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let grupo = JSON.parse(data);
        let total_grupo = grupo.length;

        for (var i = 0; i < grupo.length; i++) {
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + grupo[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + grupo[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + grupo[i].IdGrupo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + grupo[i].IdGrupo + ')"></button>' +
                '</td > ' +
                '</tr>';
        }

        $("#tbody_grupo").html(tr);
        $("#spnTotalRegistros").html(total_grupo);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nueva Grupo");
    AbrirModal("modal-form");
}




function GuardarGrupo() {
    let varIdGrupo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertGrupo', {
        'IdGrupo': varIdGrupo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerGrupo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdGrupo) {
    $("#lblTituloModal").html("Editar Grupo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdGrupo': varIdGrupo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupo = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(grupo[0].IdGrupo);
            $("#txtCodigo").val(grupo[0].Codigo);
            $("#txtDescripcion").val(grupo[0].Descripcion);
            if (grupo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdGrupo) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Grupo?', function () {
        $.post("EliminarGrupo", { 'IdGrupo': varIdGrupo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Grupo Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerGrupo");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', true);
}



