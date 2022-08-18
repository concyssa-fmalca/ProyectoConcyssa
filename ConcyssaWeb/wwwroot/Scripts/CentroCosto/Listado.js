let table = '';


window.onload = function () {
    var url = "ObtenerCentroCosto";
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

        let centroCosto = JSON.parse(data);
        let total_centroCosto = centroCosto.length;

        for (var i = 0; i < centroCosto.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + centroCosto[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + centroCosto[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + centroCosto[i].IdCentroCosto + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + centroCosto[i].IdCentroCosto + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_centroCosto").html(tr);
        $("#spnTotalRegistros").html(total_centroCosto);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva CentroCosto");
    AbrirModal("modal-form");
}




function GuardarCategoria() {
    let varIdCentroCosto = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCentroCosto', {
        'IdCentroCosto': varIdCentroCosto,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCentroCosto");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCentroCosto) {
    $("#lblTituloModal").html("Editar CentroCosto");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCentroCosto': varIdCentroCosto,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let centroCosto = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(centroCosto[0].IdCentroCosto);
            $("#txtCodigo").val(centroCosto[0].Codigo);
            $("#txtDescripcion").val(centroCosto[0].Descripcion);
            if (centroCosto[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCentroCosto) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta CentroCosto?', function () {
        $.post("EliminarCategoria", { 'IdCentroCosto': varIdCentroCosto }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "CentroCosto Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerCategoria");
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



