let table = '';


window.onload = function () {
    var url = "ObtenerObra";
    ConsultaServidor(url);
    listarBase();
    listarTipoObra();
    listarDivision();
};

function listarDivision() {
    $.ajax({
        url: "../Division/ObtenerDivision",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdDivision").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdDivision + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdDivision").html(options);
            }
        }
    });
}

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

function listarTipoObra() {
    $.ajax({
        url: "../TipoObra/ObtenerTipoObra",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdTipoObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdTipoObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdTipoObra").html(options);
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

        let obra = JSON.parse(data);
        let total_obra = obra.length;

        for (var i = 0; i < obra.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + obra[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + obra[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + obra[i].DescripcionCorta.toUpperCase() + '</td>' +
                '<td>' + obra[i].DescripcionBase.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + obra[i].IdObra + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + obra[i].IdObra + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_obra").html(tr);
        $("#spnTotalRegistros").html(total_obra);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Obra");
    AbrirModal("modal-form");
}
Obra



function GuardarObra() {
    let varIdObra = $("#txtId").val();
    let IdBase = $("#IdBase").val();
    let txtCodigo = $("#txtCodigo").val();
    let txtDescripcion = $("#txtDescripcion").val();
    let txtDescripcionCorta = $("#txtDescripcionCorta").val();
    let IdTipoObra = $("#IdTipoObra").val();
    let IdDivision = $("#IdDivision").val();

  
    let VisibleInternet = false;
    if ($('#chkIntranet')[0].checked) {
        VisibleInternet = true;
    }

    let ContratoMantenimiento = false;
    if ($('#chkContrato')[0].checked) {
        ContratoMantenimiento = true;
    }

    let Estado = false;
    if ($('#chkActivo')[0].checked) {
        Estado = true;
    }

    $.post('UpdateInsertObra', {
        'IdObra': varIdObra,
        'IdBase': IdBase,
        'Codigo': txtCodigo,
        'Descripcion': txtDescripcion,
        'DescripcionCorta': txtDescripcionCorta,
        'IdTipoObra': IdTipoObra,
        'IdDivision': IdDivision,
        'VisibleInternet': VisibleInternet,
        'ContratoMantenimiento': ContratoMantenimiento,        
        'Estado': Estado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerObra");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdObra) {
    $("#lblTituloModal").html("Editar Obra");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdObra': varIdObra,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let obra = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(obra[0].IdObra);
            $("#IdBase").val(obra[0].IdBase);
            $("#txtCodigo").val(obra[0].Codigo);
            $("#txtDescripcion").val(obra[0].Descripcion);
            $("#txtDescripcionCorta").val(obra[0].DescripcionCorta);
            $("#IdTipoObra").val(obra[0].IdTipoObra);
            $("#IdDivision").val(obra[0].IdDivision);
            if (obra[0].VisibleInternet) {
                $("#chkIntranet").prop('checked', true);
            }
            if (obra[0].ContratoMantenimiento) {
                $("#chkContrato").prop('checked', true);
            }
            if (obra[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdObra) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Obra?', function () {
        $.post("EliminarObra", { 'IdObra': varIdObra }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Obra Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerObra");
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



