let table = '';

window.onload = function () {
    var url = "ObtenerSociedades";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let sociedades = JSON.parse(data);
        let total_sociedades = sociedades.length;

        for (var i = 0; i < sociedades.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + sociedades[i].NumeroDocumento.toUpperCase() + '</td>' +
                '<td>' + sociedades[i].NombreSociedad.toUpperCase() + '</td>' +
                '<td>' + sociedades[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + sociedades[i].IdSociedad + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + sociedades[i].IdSociedad + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Sociedades").html(tr);
        $("#spnTotalRegistros").html(total_sociedades);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Sociedad");
    AbrirModal("modal-form");
}



function GuardarSociedad() {
    
    let varIdSociedad = $("#txtId").val();
    let varNombreSociedad = $("#txtNombreSociedad").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varNumeroDocumento = $("#txtNumeroDocumento").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInserSociedad', {
        'IdSociedad': varIdSociedad,
        'NombreSociedad': varNombreSociedad,
        'Descripcion': varDescripcion,
        'NumeroDocumento': varNumeroDocumento,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerSociedades");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdSociedad) {
    $("#lblTituloModal").html("Editar Sociedad");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdSociedad': varIdSociedad,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let sociedades = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(sociedades[0].IdSociedad);
            $("#txtNombreSociedad").val(sociedades[0].NombreSociedad);
            $("#txtNumeroDocumento").val(sociedades[0].NumeroDocumento);
            $("#txtDescripcion").val(sociedades[0].Descripcion);
            if (sociedades[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }


        }

    });

}

function eliminar(varIdSociedad) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta sociedad?', function () {
        $.post("EliminarSociedad", { 'IdSociedad': varIdSociedad }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Sociedad Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerSociedades");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtNumeroDocumento").val("");
    $("#txtDescripcion").val("");
    $("#txtId").val("");
    $("#txtNombreSociedad").val("");
    $("#txtNombreBD").val("");
    $("#txtCadenaConexion").val("");
    $("#chkActivo").prop('checked', false);
}



