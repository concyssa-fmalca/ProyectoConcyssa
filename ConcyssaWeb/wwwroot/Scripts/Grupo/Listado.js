let table = '';


window.onload = function () {
    listarObraFiltro()
};

function listarObraFiltro() {
    $.ajax({
        url: "/Obra/ObtenerObraxIdUsuarioSessionFiltro",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdObrafiltro").html(options);
            }
        }
    });
}

function listarObras() {
    $.ajax({
        url: "/Obra/ObtenerObraxIdUsuarioSessionFiltro",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdObra").html(options);
            }
        }
    });
}


function ConsultaServidor() {

    var url = "ObtenerGrupoxObra"
    $.post(url, { 'IdObra': $("#IdObrafiltro").val() }, function (data, status) {
        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            console.log("sssssssssssssss");
            return;
        }

        let tr = '';

        let grupo = JSON.parse(data);
        let total_grupo = grupo.length;

        

        for (var i = 0; i < grupo.length; i++) {
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + grupo[i].DescripcionObra.toUpperCase() + '</td>' +
                '<td>' + grupo[i].Codigo.toUpperCase() + '</td>' +
               
                '<td>' + grupo[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + grupo[i].IdGrupo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + grupo[i].IdGrupo + ')"></button>' +
                '</td> ' +
                '</tr>';
        }

        if (table) {
            table.destroy();
        }

        $("#tbody_grupo").html(tr);
        $("#spnTotalRegistros").html(total_grupo);

        table = $("#table_id").DataTable(lenguaje);
    });

}




function ModalNuevo() {
    listarObras();
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nueva Grupo");
    AbrirModal("modal-form");
}




function GuardarGrupo() {
    let varIdGrupo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let IdObra = $("#IdObra").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertGrupo', {
        'IdGrupo': varIdGrupo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado,
        'IdObra': IdObra
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerGrupo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdGrupo) {
    $("#lblTituloModal").html("Editar Grupo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdGrupo': varIdGrupo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupo = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(grupo[0].IdGrupo);
            $("#txtCodigo").val(grupo[0].Codigo);
            $("#txtDescripcion").val(grupo[0].Descripcion);
            $("#IdObra").val(grupo[0].IdObra);
            if (grupo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdGrupo) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Grupo?', function () {
        $.post("EliminarGrupo", { 'IdGrupo': varIdGrupo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Grupo Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerGrupo");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#IdObra").val("");
    $("#chkActivo").prop('checked', true);
}



