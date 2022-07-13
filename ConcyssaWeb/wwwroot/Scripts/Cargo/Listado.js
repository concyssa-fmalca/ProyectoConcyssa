let table = '';


window.onload = function () {
    var url = "ObtenerCargo";
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

        for (var i = 0; i < base.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + base[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + base[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + base[i].IdCargo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + base[i].IdCargo + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_base").html(tr);
        $("#spnTotalRegistros").html(total_base);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Cargo");
    AbrirModal("modal-form");
}




function GuardarCargo() {
    let varIdCargo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCargo', {
        'IdCargo': varIdCargo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCargo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCargo) {
    $("#lblTituloModal").html("Editar Cargo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCargo': varIdCargo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let base = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(base[0].IdCargo);
            $("#txtCodigo").val(base[0].Codigo);
            $("#txtDescripcion").val(base[0].Descripcion);
            if (base[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta cargo?', function () {
        $.post("EliminarCargo", { 'IdCargo': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Cargo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerCargo");
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



