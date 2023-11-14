let table = '';


window.onload = function () {
    var url = "ObtenerTipoProducto";
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

        let TipoProducto = JSON.parse(data);
        let total_TipoProducto = TipoProducto.length;

        for (var i = 0; i < TipoProducto.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + TipoProducto[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + TipoProducto[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + TipoProducto[i].IdTipoProducto + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + TipoProducto[i].IdTipoProducto + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_TipoProducto").html(tr);
        $("#spnTotalRegistros").html(total_TipoProducto);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Tipo Producto");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true)
}




function GuardarTipoProducto() {
    let varIdTipoProducto = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if (varCodigo == "" || varCodigo == undefined) {
        Swal.fire("Error", "El Campo Codigo es Obligatorio", "info")
        return
    }
    if (varDescripcion == "" || varDescripcion == undefined) {
        Swal.fire("Error", "El Campo Descripcion es Obligatorio", "info")
        return
    }
    $.post('UpdateInsertTipoProducto', {
        'IdTipoProducto': varIdTipoProducto,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerTipoProducto");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdTipoProducto) {
    $("#lblTituloModal").html("Editar Tipo Producto");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdTipoProducto': varIdTipoProducto,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let TipoProducto = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(TipoProducto[0].IdTipoProducto);
            $("#txtCodigo").val(TipoProducto[0].Codigo);
            $("#txtDescripcion").val(TipoProducto[0].Descripcion);
            if (TipoProducto[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdTIpoProducto) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Tipo Producto?', function () {
        $.post("EliminarTipoProducto", { 'IdTipoProducto': varIdTIpoProducto }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Tipo Producto Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerTipoProducto");
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



