let table = '';


window.onload = function () {
    var url = "ObtenerSubGrupo";
    ConsultaServidor(url);
    listargrupos();
};


function listargrupos() {
    $.ajax({
        url: "ObtenerGrupo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdGrupo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdGrupo + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdGrupo").html(options);
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

        let grupo = JSON.parse(data);
        let total_grupo = grupo.length;

        for (var i = 0; i < grupo.length; i++) {
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + grupo[i].DescGrupo.toUpperCase() + '</td>' +
                '<td>' + grupo[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + grupo[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + grupo[i].IdSubGrupo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + grupo[i].IdSubGrupo + ')"></button>' +
                '</td > ' +
                '</tr>';
        }

        $("#tbody_grupo").html(tr);
        $("#spnTotalRegistros").html(total_grupo);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nueva SubGrupo");
    AbrirModal("modal-form");
}




function GuardarSubGrupo() {
    let varIdSubGrupo = $("#txtId").val();
    let varIdGrupo = $("#IdGrupo").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertSubGrupo', {
        'IdSubGrupo': varIdSubGrupo,
        'IdGrupo': varIdGrupo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerSubGrupo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdSubGrupo) {
    $("#lblTituloModal").html("Editar SubGrupo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxIdSubGrupo', {
        'IdSubGrupo': varIdSubGrupo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupo = JSON.parse(data);
            console.log(grupo);
            $("#txtId").val(grupo[0].IdSubGrupo);
            $("#IdGrupo").val(grupo[0].IdGrupo);
            $("#txtCodigo").val(grupo[0].Codigo);
            $("#txtDescripcion").val(grupo[0].Descripcion);
            if (grupo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdSubGrupo) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta SubGrupo?', function () {
        $.post("EliminarSubGrupo", { 'IdSubGrupo': varIdSubGrupo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "SubGrupo Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerSubGrupo");
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



