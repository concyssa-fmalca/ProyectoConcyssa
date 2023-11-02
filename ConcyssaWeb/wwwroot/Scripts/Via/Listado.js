let table = '';


window.onload = function () {
    var url = "ObtenerVia";
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

        let Via = JSON.parse(data);
        let total_Via = Via.length;

        for (var i = 0; i < Via.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Via[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + Via[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + Via[i].IdVia + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + Via[i].IdVia + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Via").html(tr);
        $("#spnTotalRegistros").html(total_Via);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Via");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true)
}




function GuardarVia() {
    let varIdVia = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertVia', {
        'IdVia': varIdVia,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerVia");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdVia) {
    $("#lblTituloModal").html("Editar Via");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdVia': varIdVia,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let Via = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(Via[0].IdVia);
            $("#txtCodigo").val(Via[0].Codigo);
            $("#txtDescripcion").val(Via[0].Descripcion);
            if (Via[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdVia) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Via?', function () {
        $.post("EliminarVia", { 'IdVia': varIdVia }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Via Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerVia");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}



