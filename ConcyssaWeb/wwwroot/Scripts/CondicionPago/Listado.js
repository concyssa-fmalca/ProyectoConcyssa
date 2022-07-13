let table = '';


window.onload = function () {
    var url = "ObtenerCondicionPagos";
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

        let condicionpagos = JSON.parse(data);
        let total_condicionpagos = condicionpagos.length;

        for (var i = 0; i < condicionpagos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + condicionpagos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + condicionpagos[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + condicionpagos[i].Dias + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + condicionpagos[i].IdCondicionPago + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + condicionpagos[i].IdCondicionPago + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_CondicionPagos").html(tr);
        $("#spnTotalRegistros").html(total_condicionpagos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Condicion de Pago");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarCondicionPago() {

    let varIdCondicionPago = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varDias = $("#txtDias").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCondicionPago', {
        'IdCondicionPago': varIdCondicionPago,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Dias': varDias,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCondicionPagos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCondicionPago) {
    $("#lblTituloModal").html("Editar Condicion de Pago");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCondicionPago': varIdCondicionPago,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let condicionpago = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(condicionpago[0].IdCondicionPago);
            $("#txtCodigo").val(condicionpago[0].Codigo);
            $("#txtDescripcion").val(condicionpago[0].Descripcion);
            $("#txtDias").val(condicionpago[0].Dias);
            if (condicionpago[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCondicionPago) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta condicion de pago?', function () {
        $.post("EliminarCondicionPago", { 'IdCondicionPago': varIdCondicionPago }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Condicion de Pago Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerCondicionPagos");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtDias").val("");
    $("#chkActivo").prop('checked', false);
}



