let table = '';


window.onload = function () {
    var url = "ObtenerUnidadMedidas";
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

        let unidadmedidas = JSON.parse(data);
        let total_unidadmedidas = unidadmedidas.length;

        for (var i = 0; i < unidadmedidas.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + unidadmedidas[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + unidadmedidas[i].CodigoSunat.toUpperCase() + '</td>' +
                '<td>' + unidadmedidas[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + unidadmedidas[i].IdUnidadMedida + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + unidadmedidas[i].IdUnidadMedida + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Unidadmedidas").html(tr);
        $("#spnTotalRegistros").html(total_unidadmedidas);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Unidad de Medida");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true)
}




function GuardarUnidadMedida() {

    let varIdUnidadMedida = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varCodigoSunat = $("#txtCodigoSunat").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }


    if (varCodigo == "" || varCodigo == undefined) {
        Swal.fire("Error", "El Campo Codigo es Obligatorio", "info")
        return
    }
    if (varCodigoSunat == "" || varCodigoSunat == undefined) {
        Swal.fire("Error", "El Campo Codigo Sunat es Obligatorio", "info")
        return
    }
    if (varDescripcion == "" || varDescripcion == undefined) {
        Swal.fire("Error", "El Campo Descripcion es Obligatorio", "info")
        return
    }

    $.post('UpdateInsertUnidadMedida', {
        'IdUnidadMedida': varIdUnidadMedida,
        'Codigo': varCodigo,
        'CodigoSunat': varCodigoSunat,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerUnidadMedidas");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdUnidadMedia) {
    $("#lblTituloModal").html("Editar Unidad de Medida");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdUnidadMedida': varIdUnidadMedia,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let unidadmedida = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(unidadmedida[0].IdUnidadMedida);
            $("#txtCodigo").val(unidadmedida[0].Codigo);
            $("#txtCodigoSunat").val(unidadmedida[0].CodigoSunat);
            $("#txtDescripcion").val(unidadmedida[0].Descripcion);
            if (unidadmedida[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdUnidadMedida) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta unidad de medida?', function () {
        $.post("EliminarUnidadMedida", { 'IdUnidadMedida': varIdUnidadMedida }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Unidad de Medida Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerUnidadMedidas");
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



