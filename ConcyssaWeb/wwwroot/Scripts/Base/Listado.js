let table = '';


window.onload = function () {
    var url = "ObtenerBasexSociedad";
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

        let base = JSON.parse(data);
        let total_base = base.length;

        for (var i = 0; i < lineanegocios.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + base[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + base[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + base[i].IdLineaNegocio + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + base[i].IdLineaNegocio + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_base").html(tr);
        $("#spnTotalRegistros").html(total_base);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Linea de Negocio");
    AbrirModal("modal-form");
}




function GuardarLineaNegocio() {

    let varIdLineaNegocio = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertLineaNegocio', {
        'IdLineaNegocio': varIdLineaNegocio,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerLineaNegocios");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdLineaNegocio) {
    $("#lblTituloModal").html("Editar Linea de Negocio");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdLineaNegocio': varIdLineaNegocio,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let lineganegocio = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(lineganegocio[0].IdLineaNegocio);
            $("#txtCodigo").val(lineganegocio[0].Codigo);
            $("#txtDescripcion").val(lineganegocio[0].Descripcion);
            if (lineganegocio[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta linea de negocio?', function () {
        $.post("EliminarLineaNegocio", { 'IdLineaNegocio': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Linea de Negocio Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerLineaNegocios");
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



