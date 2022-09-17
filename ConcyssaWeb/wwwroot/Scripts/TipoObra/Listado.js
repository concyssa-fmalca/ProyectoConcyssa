let table = '';


window.onload = function () {
    var url = "ObtenerTipoObra";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {
    table = $('#table_id').dataTable({
        responsive: true,
        ajax: {
            url: url,
            type: 'POST',
            data: {
                
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Descripcion
                },
            }
            
        ],
        "bDestroy": true
    }).DataTable();


}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Linea de Negocio");
    AbrirModal("modal-form");
}




function GuardarTipoObra() {
    let varIdTipoObra = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertTipoObra', {
        'IdTipoObra': varIdTipoObra,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerTipoObra");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdTipoObra) {
    $("#lblTituloModal").html("Editar Linea de Negocio");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdTipoObra': varIdTipoObra,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let tipoobra = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(tipoobra[0].IdTipoObra);
            $("#txtCodigo").val(tipoobra[0].Codigo);
            $("#txtDescripcion").val(tipoobra[0].Descripcion);
            if (tipoobra[0].Estado) {
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
                ConsultaServidor("ObtenerTipoObra");
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



