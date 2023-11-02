let table = '';
let contador = 0;
let table_series;

window.onload = function () {
    ObtenerTipoRegistro();
};



function ObtenerTipoRegistro() {
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerTipoRegistros',
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            { "className": "text-center", "targets": "_all" },
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-info  fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdTipoRegistro + `)"></button><button class="btn btn-danger btn-xs fa fa-trash" onclick="Eliminar(` + full.IdTipoRegistro + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    return button;

                },
            },
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoRegistro
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.Estado) {
                        return 'Visible'
                    } else {
                        return 'Desactivado'
                    }
                    
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}




function ObtenerDatosxID(Id) {
    $("#modaltiporegistro").modal('show')
    $("#lblTituloModal").html("Editar Tipo Registro");

    $.post('ObtenerDatosxID', {
        'IdTipoRegistro': Id,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let dato = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(Id);
            $("#NombTipoRegistro").val(dato.NombTipoRegistro);           
            if (dato.Estado) {
                $("#chkActivo").prop('checked', true);
            } else {
                $("#chkActivo").prop('checked', false);
            }

        }

    });

}

function Eliminar(Id) {

    alertify.confirm('Confirmar', '¿Desea eliminar esta Tipo Registro?', function () {
        $.post("EliminarTipoRegistro", { 'IdTipoRegistro': Id }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Tipo Registro Eliminado", "success")
                table.destroy();
                ObtenerTipoRegistro();
                limpiarDatos();
            }

        });

    }, function () { });

}


function ModalNuevo() {
    $("#modaltiporegistro").modal('show')
    $("#lblTituloModal").html("Nuevo Registro");
    $('#chkActivo').prop('checked', true)
}

function Grabar() {
    let NombTipoRegistro = $("#NombTipoRegistro").val();
    let Estado = false;
    if ($('#chkActivo')[0].checked) {
        Estado = true;
    }
    $.post('UpdateInsertTipoRegistro', {
        'IdTipoRegistro': +$("#txtId").val(),
        'Estado': Estado,
        'NombTipoRegistro': NombTipoRegistro
    }, function (data, status) {
        if (data > 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
        ObtenerTipoRegistro();
        closePopup();
    });
}
function limpiarDatos() {
    $("#NombTipoRegistro").val('');
}