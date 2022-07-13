let table = '';


window.onload = function () {
    var url = "ObtenerAlmacen";
    ConsultaServidor(url);
    listarBase();
};


function listarBase() {
    $.ajax({
        url: "../Base/ObtenerBase",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdBase").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdBase + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdBase").html(options);
            }
        }
    });
}

function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let almacen = JSON.parse(data);
        let total_almacen = almacen.length;

        for (var i = 0; i < almacen.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + almacen[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + almacen[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + almacen[i].IdAlmacen + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + almacen[i].IdAlmacen + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_almacen").html(tr);
        $("#spnTotalRegistros").html(total_almacen);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Almacen");
    AbrirModal("modal-form");
}




function GuardarAlmacen() {
    let varIdAlmacen = $("#txtId").val();
    let IdBase=$("#IdBase").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertAlmacen', {
        'IdAlmacen': varIdAlmacen,
        'IdBase': IdBase,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerAlmacen");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdAlmacen) {
    $("#lblTituloModal").html("Editar Almacen");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdAlmacen': varIdAlmacen,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let almacen = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(almacen[0].IdAlmacen);
            $("#IdBase").val(almacen[0].IdBase);
            $("#txtCodigo").val(almacen[0].Codigo);
            $("#txtDescripcion").val(almacen[0].Descripcion);
            if (almacen[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdAlmacen) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Almacen?', function () {
        $.post("EliminarAlmacen", { 'IdAlmacen': varIdAlmacen }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Almacen Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerAlmacen");
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



