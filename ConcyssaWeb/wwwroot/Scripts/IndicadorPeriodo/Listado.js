let table;
window.onload = function () {
    listarindicadordt();
}

function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Indicador de Periodo");
    AbrirModal("modal-form");
}

function GuardarIndicadorPeriodo() {
    if ($("#Indicador").val() == '' || $("#Indicador").val() == null) {
        Swal.fire(
            'Error!',
            'Registre un Indicador de Periodo',
            'error'
        )
        return;
    }


    $.ajax({
        url: "UpdateInsertIndicadorPeriodo",
        type: "POST",
        async: true,
        data: {
            'Indicador': $("#Indicador").val(),
            'IdIndicadorPeriodo': $("#IdIndicadorPeriodo").val(),
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                CerrarModal();
        
                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                listarindicadordt()

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });


}





function listarindicadordt() {
    table = $('#tableindicador').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerIndicadorPeriodoDT',
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
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" style="margin:0px 0px 0px 0px !important" onclick="ObtenerDatosxID(` + full.IdIndicadorPeriodo + `)"></button>
                        <button class="btn btn-danger btn-xs  fa fa-trash"  style="margin:0px 0px 0px 0px !important"  onclick="eliminar(` + full.IdIndicadorPeriodo + `)"></button>`
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
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Indicador
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}


function ObtenerDatosxID(IdIndicadorPeriodo) {
    $("#lblTituloModal").html("Editar Indicador Periodo");
    AbrirModal("modal-form");
    $.post('ObtenerIndicadorPeriodoxId', {
        'IdIndicadorPeriodo': IdIndicadorPeriodo,
    }, function (data, status) {
        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let IndicadorPeriodo = JSON.parse(data);
            $("#Indicador").val(IndicadorPeriodo.Indicador);
            $("#IdIndicadorPeriodo").val(IndicadorPeriodo.IdIndicadorPeriodo)
        }

    });
}


function CerrarModal() {
    $.magnificPopup.close();
    limpiarDatos();
}

function limpiarDatos() {
    $("#IdIndicadorPeriodo").val(0);
    $("#Indicador").val('');
}

function eliminar(Id) {
    $.ajax({
        url: "EliminarInidicadorPeriodo",
        type: "POST",
        async: true,
        data: {
            'IdIndicadorPeriodo': Id
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                listarindicadordt()

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });
}