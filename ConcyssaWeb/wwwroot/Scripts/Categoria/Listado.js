let table = '';


window.onload = function () {
    var url = "ObtenerCategoria";
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

        let categoria = JSON.parse(data);
        let total_categoria = categoria.length;

        for (var i = 0; i < categoria.length; i++) {

           
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + categoria[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + categoria[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + categoria[i].IdCategoria + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + categoria[i].IdCategoria + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_categoria").html(tr);
        $("#spnTotalRegistros").html(total_categoria);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Categoria");
    AbrirModal("modal-form");
}




function GuardarCategoria() {
    let varIdCategoria = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCategoria', {
        'IdCategoria': varIdCategoria,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCategoria");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCategoria) {
    $("#lblTituloModal").html("Editar Categoria");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCategoria': varIdCategoria,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let categoria = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(categoria[0].IdCategoria);
            $("#txtCodigo").val(categoria[0].Codigo);
            $("#txtDescripcion").val(categoria[0].Descripcion);
            if (categoria[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCategoria) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Categoria?', function () {
        $.post("EliminarCategoria", { 'IdCategoria': varIdCategoria }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Categoria Eliminada", "success")
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



