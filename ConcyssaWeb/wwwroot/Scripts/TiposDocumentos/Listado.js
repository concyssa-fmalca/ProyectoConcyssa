let table = '';


window.onload = function () {
    var url = "ObtenerTiposDocumentos";
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

        let tiposDocumentos = JSON.parse(data);
        let total_tiposDocumentos = tiposDocumentos.length;

        for (var i = 0; i < tiposDocumentos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + tiposDocumentos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + tiposDocumentos[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + tiposDocumentos[i].IdTipoDocumento + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + tiposDocumentos[i].IdTipoDocumento + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_tiposDocumentos").html(tr);
        $("#spnTotalRegistros").html(total_tiposDocumentos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva TiposDocumentos");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked',true)
}




function GuardarTiposDocumentos() {
    let varIdTiposDocumentos = $("#txtId").val();
    let CodSunat = $("#CodigoSunat").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;


    if (CodSunat == "" || CodSunat == undefined) {
        Swal.fire("Error", "El Campo Codigo Sunat es Obligatorio", "info")
        return
    }
    if (varCodigo == "" || varCodigo == undefined) {
        Swal.fire("Error", "El Campo Codigo es Obligatorio", "info")
        return
    }
    if (varDescripcion == "" || varDescripcion == undefined) {
        Swal.fire("Error", "El Campo Descripcion es Obligatorio", "info")
        return
    }
    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertTiposDocumentos', {
        'IdTipoDocumento': varIdTiposDocumentos,
        'Codigo': varCodigo,
        'CodSunat': CodSunat,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerTiposDocumentos");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdTiposDocumentos) {
    $("#lblTituloModal").html("Editar TiposDocumentos");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdTipoDocumento': varIdTiposDocumentos,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let tiposDocumentos = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(varIdTiposDocumentos);
            $("#CodigoSunat").val(tiposDocumentos[0].CodSunat);
            $("#txtCodigo").val(tiposDocumentos[0].Codigo);
            $("#txtDescripcion").val(tiposDocumentos[0].Descripcion);
            if (tiposDocumentos[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Tipo Documento?', function () {
        $.post("EliminarTipoDocumento", { 'IdTipoDocumento': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "TiposDocumentos Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerTiposDocumentos");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#CodigoSunat").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}



