let table = '';


window.onload = function () {
    var url = "ObtenerModelo";
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

        let Modelo = JSON.parse(data);
        let total_Modelo = Modelo.length;

        for (var i = 0; i < Modelo.length; i++) {

            let estado = "INACTIVO"
            if (Modelo[i].Estado) estado= "ACTIVO"

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Modelo[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + Modelo[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + estado + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + Modelo[i].IdModelo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + Modelo[i].IdModelo + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Modelo").html(tr);
        $("#spnTotalRegistros").html(total_Modelo);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Modelo");
    AbrirModal("modal-form");
    $("#chkActivo").prop('checked', true)
}




function GuardarModelo() {
    let varIdModelo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

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

    $.post('UpdateInsertModelo', {
        'IdModelo': varIdModelo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerModelo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdModelo) {
    $("#lblTituloModal").html("Editar Modelo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdModelo': varIdModelo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let Modelo = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(Modelo[0].IdModelo);
            $("#txtCodigo").val(Modelo[0].Codigo);
            $("#txtDescripcion").val(Modelo[0].Descripcion);
            if (Modelo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdModelo) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Modelo?', function () {
        $.post("EliminarModelo", { 'IdModelo': varIdModelo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Modelo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerModelo");
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



