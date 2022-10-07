let table = '';


window.onload = function () {
    var url = "ObtenerCodigoUbso";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#dt_codigoubso").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let codigoubso = JSON.parse(data);
        let total_codigoubso = codigoubso.length;

        for (var i = 0; i < codigoubso.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + codigoubso[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + codigoubso[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + codigoubso[i].IdCodigoUbso + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + codigoubso[i].IdCodigoUbso + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_codigoUbso").html(tr);
        $("#spnTotalRegistros").html(total_codigoubso);

        table = $("#dt_codigoubso").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Unidad de Medida");
    AbrirModal("modal-form");
}




function GuardarCodigoUbso() {

    let varIdCodigoUbso = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCodigoUbso', {
        'IdCodigoUbso': varIdCodigoUbso,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCodigoUbso");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCodigoUbso) {
    $("#lblTituloModal").html("Editar Codigo Ubso");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCodigoUbso': varIdCodigoUbso,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let codigoUbso = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(codigoUbso[0].IdCodigoUbso);
            $("#txtCodigo").val(codigoUbso[0].Codigo);
            $("#txtDescripcion").val(codigoUbso[0].Descripcion);
            if (codigoUbso[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCodigoUbso) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Codigo?', function () {
        $.post("EliminarCodigoUbso", { 'IdCodigoUbso': varIdCodigoUbso }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Codigo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerCodigoUbso");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtCodigoSunat").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}



