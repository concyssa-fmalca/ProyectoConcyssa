let table = '';


window.onload = function () {
    var url = "ObtenerArea";
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

        let area = JSON.parse(data);
        let total_area = area.length;

        for (var i = 0; i < area.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + area[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + area[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + area[i].IdArea + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + area[i].IdArea + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_area").html(tr);
        $("#spnTotalRegistros").html(total_area);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nueva Area");
    AbrirModal("modal-form");
}




function GuardarArea() {
    let varIdArea = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertArea', {
        'IdArea': varIdArea,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerArea");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdArea) {
    $("#lblTituloModal").html("Editar Area");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdArea': varIdArea,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let area = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(area[0].IdArea);
            $("#txtCodigo").val(area[0].Codigo);
            $("#txtDescripcion").val(area[0].Descripcion);
            if (area[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Area?', function () {
        $.post("EliminarLineaNegocio", { 'IdLineaNegocio': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Area Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerArea");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', true);
}



