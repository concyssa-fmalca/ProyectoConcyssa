let table = '';


window.onload = function () {
    var url = "ObtenerMarca";
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

        let Marca = JSON.parse(data);
        let total_Marca = Marca.length;

        for (var i = 0; i < Marca.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Marca[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + Marca[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + Marca[i].IdMarca + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + Marca[i].IdMarca + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Marca").html(tr);
        $("#spnTotalRegistros").html(total_Marca);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Marca");
    AbrirModal("modal-form");
    $("#chkActivo").prop('checked',true)
}




function GuardarMarca() {
    let varIdMarca = $("#txtId").val();
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

    $.post('UpdateInsertMarca', {
        'IdMarca': varIdMarca,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerMarca");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdMarca) {
    $("#lblTituloModal").html("Editar Marca");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdMarca': varIdMarca,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let Marca = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(Marca[0].IdMarca);
            $("#txtCodigo").val(Marca[0].Codigo);
            $("#txtDescripcion").val(Marca[0].Descripcion);
            if (Marca[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdMarca) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Marca?', function () {
        $.post("EliminarMarca", { 'IdMarca': varIdMarca }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Marca Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerMarca");
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



