let table = '';


window.onload = function () {
    var url = "ObtenerRubroProveedor";
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

        let rubroproveedor = JSON.parse(data);
        let total_rubroproveedor = rubroproveedor.length;

        for (var i = 0; i < rubroproveedor.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rubroproveedor[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + rubroproveedor[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + rubroproveedor[i].IdRubroProveedor + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + rubroproveedor[i].IdRubroProveedor + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_rubroproveedor").html(tr);
        $("#spnTotalRegistros").html(total_rubroproveedor);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Rubro Proveedor");
    AbrirModal("modal-form");
    $("#chkActivo").prop('checked',true)
}




function GuardarRubroProveedor() {
    let varIdRubroProveedor = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertRubroProveedor', {
        'IdRubroProveedor': varIdRubroProveedor,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerRubroProveedor");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdRubroProveedor) {
    $("#lblTituloModal").html("Editar Rubro Proveedor");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdRubroProveedor': varIdRubroProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let rubroproveedor = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(rubroproveedor[0].IdRubroProveedor);
            $("#txtCodigo").val(rubroproveedor[0].Codigo);
            $("#txtDescripcion").val(rubroproveedor[0].Descripcion);
            if (rubroproveedor[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdRubroProveedor) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Rubro Proveedor?', function () {
        $.post("EliminarRubroProveedor", { 'IdRubroProveedor': varIdRubroProveedor }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Rubro Proveedor Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerRubroProveedor");
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



