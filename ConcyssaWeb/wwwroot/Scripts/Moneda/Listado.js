let table = '';


window.onload = function () {
    var url = "ObtenerMonedas";
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

        let monedas = JSON.parse(data);
        let total_monedas = monedas.length;

        for (var i = 0; i < monedas.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + monedas[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + monedas[i].Descripcion.toUpperCase() + '</td>';
            if (monedas[i].Base) {
                tr += '<td>MONEDA BASE</td>';
            } else {
                tr += '<td>MONEDA</td>';
            }


            tr += '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + monedas[i].IdMoneda + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + monedas[i].IdMoneda + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Monedas").html(tr);
        $("#spnTotalRegistros").html(total_monedas);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Moneda");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarMoneda() {

    let varIdMoneda = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;
    let varBase = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if ($('#chkBase')[0].checked) {
        varBase = true;
    }


    $.post('UpdateInsertMoneda', {
        'IdMoneda': varIdMoneda,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Base': varBase,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerMonedas");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdMoneda) {
    $("#lblTituloModal").html("Editar Moneda");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdMoneda': varIdMoneda,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let moneda = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(moneda[0].IdMoneda);
            $("#txtCodigo").val(moneda[0].Codigo);
            $("#txtDescripcion").val(moneda[0].Descripcion);
            if (moneda[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }
            if (moneda[0].Base) {
                $("#chkBase").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdMoneda) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta moneda?', function () {
        $.post("EliminarMoneda", { 'IdMoneda': varIdMoneda }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Moneda Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerMonedas");
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
    $("#chkBase").prop('checked', false);
}



