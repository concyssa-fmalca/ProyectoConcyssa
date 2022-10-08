let table = '';


window.onload = function () {
    var url = "ObtenerTipoObraDT";
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
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID('` + full.IdTipoObra + `')"></button>
                            <button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar('`+ full.IdTipoObra + `')"></button>`
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets:1 ,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Codigo
                },
            },
            {
                targets: 2,
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
    $("#lblTituloModal").html("Nuevo Tipo de Obra");
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
            ConsultaServidor("ObtenerTipoObraDT");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdTipoObra) {
    $("#lblTituloModal").html("Editar Tipo Obra");
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

function eliminar(varIdTipoObra) {

    console.log(parseInt(varIdTipoObra));
    alertify.confirm('Confirmar', '¿Desea eliminar este Tipo de obra?', function () {
        $.post("EliminarTipoObra", { 'IdTipoObra': parseInt(varIdTipoObra) }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Tipo de Obra Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerTipoObraDT");
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



function soloEnteros(e, obj) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode == 13) {
        var tidx = parseInt(obj.getAttribute('tabindex')) + 1;
        elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) { elems[i].focus(); break; }
        }
    } else if (charCode == 46 || charCode > 31 && (charCode < 48 || charCode > 57)) {
        e.preventDefault();
        return false;
    }
    return true;
}